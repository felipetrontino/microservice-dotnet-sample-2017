using Bookstore.Core.Enums;
using Framework.Core.Entities;
using System.Collections.Generic;

namespace Bookstore.Entities
{
    public class Book : EFEntity
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public Language Language { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
