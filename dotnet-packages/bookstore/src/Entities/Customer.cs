using Framework.Core.Entities;

namespace Bookstore.Entities
{
    public class Customer : EFEntity
    {
        public string UserId { get; set; }

        public string DocumentId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
