using Book.Core.Enums;
using System;

namespace Book.Models.Proxy
{
    public class BookProxy 
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public Language Language { get; set; }
    }
}
