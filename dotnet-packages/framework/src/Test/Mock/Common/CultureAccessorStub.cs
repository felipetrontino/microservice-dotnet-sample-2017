using Framework.Core.Common;
using Framework.Test.Common;

namespace Framework.Test.Mock.Common
{
    public class CultureAccessorStub : ICultureAccessor
    {
        public CultureAccessorStub(string culture = null)
        {
            Culture = culture ?? FakeHelper.GetCulture();
        }

        public string Culture { get; }

        public static ICultureAccessor Create(string culture = null)
        {
            return new CultureAccessorStub(culture);
        }
    }
}