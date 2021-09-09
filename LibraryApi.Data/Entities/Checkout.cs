using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Data.Entities
{
    public class Checkout
    {
        public int Id { get; set; }

        public virtual List<Book> Books { get; set; } = new List<Book>();

        public virtual User User { get; set; }

        public int UserId { get; set; }

        public DateTime CheckoutDate { get; set; }

        public DateTime ExpectedReturnDate { get; set; }

        public DateTime ReturnDate { get; set; }
        
        [Column(TypeName = "Money")]
        public decimal PenaltyFees { get; set; }
    }
}