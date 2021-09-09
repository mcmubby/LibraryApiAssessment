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

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishYear { get; set; }

        [Required]
        [Column(TypeName ="Money")]
        public decimal CoverPrice { get; set; }

        public bool IsAvailable { get; set; }

        public virtual List<Checkout> BookCheckout { get; set; }

    }
}