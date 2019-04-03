using Framework.Core.Exceptions;
using Framework.Core.Logging;
using Framework.Core.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Core.Config.Consul
{
    public class ConsulConfigurationProvider : ConfigurationProvider
    {
        private const string ConsulIndexHeader = "X-Consul-Index";
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly IConsulConfigurationSource _source;
        private readonly HttpClient _httpClient;
        private readonly Uri _url;
        private int _consulConfigurationIndex;

        public ConsulConfigurationProvider(IConsulConfigurationSource source)
        {
            _source = source;
            _url = new Uri(source.Url, $"v1/kv/{_source.Path}");

            _httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }, true);
        }

        public override void Load()
        {
            LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            Task.Run(() => ListenToConfigurationChanges(), _cancellationTokenSource.Token);
        }

        private async Task LoadAsync()
        {
            Data = await ExecuteQueryAsync();

            if (!_source.Optional && Data.Count == 0)
            {
                throw new HBException($"The configuration for key {_source.Path} was not found and is not optional.");
            }
        }

        private async Task ListenToConfigurationChanges()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(2));

                    Data = await ExecuteQueryAsync(true);

                    OnReload();
                }
                catch
                {
                    //Ignore
                }
            }
        }

        private async Task<IDictionary<string, string>> ExecuteQueryAsync(bool isBlocking = false)
        {
            try
            {
                async Task<IDictionary<string, string>> Changes() =>
                    await new FuncRetrier<Exception, IDictionary<string, string>>
                    {
                        Attempts = 3,
                        Delay = 1500,
                        TaskAsync = async () =>
                        {
                            var requestUri = isBlocking ? $"?recurse=true&index={_consulConfigurationIndex}&wait=60s" : "?recurse=true";

                            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_url, requestUri)))
                            {
                                using (var response = await _httpClient.SendAsync(request))
                                {
                                    response.EnsureSuccessStatusCode();
                                    if (response.Headers.Contains(ConsulIndexHeader))
                                    {
                                        var indexValue = response.Headers.GetValues(ConsulIndexHeader).FirstOrDefault();
                                        int.TryParse(indexValue, out _consulConfigurationIndex);
                                    }

                                    var tokens = JToken.Parse(await response.Content.ReadAsStringAsync());

                                    return tokens
                                       .SelectMany(GetKV)
                                       .ToDictionary(v => ConfigurationPath.Combine(v.Key.Split('/')), v => v.Value, StringComparer.OrdinalIgnoreCase);
                                }
                            }
                        },
                        OnAttemptError = e =>
                        {
                            LogHelper.Debug("Retrying Consul ...");
                        }
                    }.RunAsync();

                return await Changes();

            }
            catch (Exception ex)
            {
                throw new HBException("ConsulConfigurationProvider cannot communicate.", ex);
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetKV(JToken token)
        {
            var key = token.Value<string>("Key").Substring(_source.Path.Length + 1);

            if (string.IsNullOrEmpty(key)) yield break;

            var value = token.Value<string>("Value");

            if (string.IsNullOrEmpty(value)) yield break;

            var xValue = value != null ? Encoding.UTF8.GetString(Convert.FromBase64String(value)) : null;
            TryParse(xValue, out JToken vToken);

            var items = new List<KeyValuePair<string, string>>();

            if (vToken != null)
            {
                items.AddRange(Flatten(new KeyValuePair<string, JToken>(key, vToken)));
            }

            if (items.Count == 0)
                items.Add(new KeyValuePair<string, string>(key, xValue));

            foreach (var item in items)
            {
                yield return item;
            }
        }

        private void TryParse(string value, out JToken token)
        {
            token = null;

            try
            {
                token = JToken.Parse(value);
            }
            catch
            {
                //Ignore
            }
        }

        private IEnumerable<KeyValuePair<string, string>> Flatten(KeyValuePair<string, JToken> tuple)
        {
            if (!(tuple.Value is JObject value))
                yield break;

            foreach (var property in value)
            {
                var propertyKey = $"{tuple.Key}/{property.Key}";
                switch (property.Value.Type)
                {
                    case JTokenType.Object:
                        foreach (var item in Flatten(new KeyValuePair<string, JToken>(propertyKey, property.Value)))
                            yield return item;
                        break;

                    case JTokenType.Array:
                        break;

                    default:
                        yield return new KeyValuePair<string, string>(propertyKey, property.Value.Value<string>());
                        break;
                }
            }
        }
    }
}
