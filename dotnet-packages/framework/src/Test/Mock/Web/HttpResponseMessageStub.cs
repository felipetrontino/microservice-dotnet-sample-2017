using Framework.Web.Common;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Framework.Test.Mock.Web
{
    public static class HttpResponseMessageStub
    {
        public static HttpResponseMessage GetSuccess(object responseValue = null)
        {
            var ret = new HttpResponseMessage(HttpStatusCode.OK);

            if (responseValue == null)
                responseValue = string.Empty;

            ret.Content = responseValue.ToStringContent();

            return ret;
        }

        public static HttpResponseMessage GetError(object responseValue = null)
        {
            var ret = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            if (responseValue == null)
                responseValue = string.Empty;

            ret.Content = responseValue.ToStringContent();

            return ret;
        }

        public static HttpResponseMessage GetSuccessWithList<T>(params T[] values)
        {
            var ret = new HttpResponseMessage(HttpStatusCode.OK);

            var list = new List<T>();

            if (values != null)
                list.AddRange(values);

            ret.Content = list.ToStringContent();

            return ret;
        }
    }
}
