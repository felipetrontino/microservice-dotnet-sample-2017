using Framework.Core.Entities;

namespace Library.Entities
{
    public class Copy : EFEntity
    {
        public Book Book { get; set; }

        public string Number { get; set; }
    }
}
