using System;

namespace Framework.Test.Common
{
    public class MockBuilder<TBuilder, TEntity> : IMockBuilder<TBuilder, TEntity>
        where TBuilder : class, IMockBuilder<TBuilder, TEntity>
        where TEntity : class, new()
    {
        public string Key { get; set; }

        public TEntity Value { get; set; }

        public static TBuilder Create(string key = null)
        {
            var ret = Activator.CreateInstance<TBuilder>();
            ret.Key = key;
            ret.Value = MockHelper.CreateModel<TEntity>(ret.Key);

            return ret;
        }

        public TEntity Build()
        {
            return Value;
        }
    }
}
