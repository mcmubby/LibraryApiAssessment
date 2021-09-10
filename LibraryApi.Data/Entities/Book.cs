using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Data.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public string PublishYear { get; set; }

        [Column(TypeName ="Money")]
        public decimal CoverPrice { get; set; }

        public bool IsAvailable { get; set; }

        public virtual List<Checkout> CheckoutHistory { get; set; }

    }
}