using Framework.Core.Enums;

namespace Bookstore.Core.Enums
{
    public enum Language
    {
        [EnumInfo("", "Unknown")]      
        Unknown = 0,

        [EnumInfo("PT", "Portuguese")]
        Portuguese = 1,

        [EnumInfo("EN", "English")]
        English = 2,

        [EnumInfo("ES", "Spanish")]
        Spanish = 3
    }
}
