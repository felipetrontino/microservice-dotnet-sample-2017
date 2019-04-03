using Framework.Core.Enums;

namespace Bookstore.Core.Enums
{
    public enum StatusStock
    {
        [EnumInfo("OUT", "Out")]       
        Out = 0,

        [EnumInfo("IN", "In")]
        In = 1
    }
}
