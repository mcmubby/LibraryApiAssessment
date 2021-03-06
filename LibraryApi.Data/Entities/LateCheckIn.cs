using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Data.Entities
{
    public class LateCheckIn
    {
        [Key]
        public int Id { get; set; }

        public DateTime ExpectedReturnDate { get; set; }

        public DateTime ReturnDate { get; set; } 
                                                        
        public Checkout Checkout { get; set; }

        [ForeignKey("Checkout")]
        public int CheckoutId { get; set; }

        public DateTime CheckoutDate { get; set; }

        public int NumberOfDaysLate { get; set; }

        [Column(TypeName = "Money")]
        public decimal PenaltyFees { get; set; }
    }
}