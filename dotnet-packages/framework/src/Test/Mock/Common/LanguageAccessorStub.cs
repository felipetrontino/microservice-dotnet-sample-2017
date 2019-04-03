using Framework.Core.Common;
using Framework.Test.Common;

namespace Framework.Test.Mock.Common
{
    public class LanguageAccessorStub : ILanguageAccessor
    {
        public LanguageAccessorStub(string language = null)
        {
            Language = language ?? FakeHelper.GetLanguage();
        }

        public string Language { get; }

        public static ILanguageAccessor Create(string language = null)
        {
            return new LanguageAccessorStub(language);
        }
    }
}
