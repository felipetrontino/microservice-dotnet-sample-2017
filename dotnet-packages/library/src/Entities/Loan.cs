using Framework.Core.Entities;
using System;

namespace Library.Entities
{
    public class Loan : EFEntity, IValueEntity
    {
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }       
        public Book Book { get; set; }
        public Copy Copy { get; set; }
    }
}
