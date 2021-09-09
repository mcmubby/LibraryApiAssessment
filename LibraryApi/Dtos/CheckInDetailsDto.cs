using System;

namespace LibraryApi
{
    public class CheckInDetailsDto
    {
        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public DateTime CheckoutDate { get; set; }
        
        public DateTime ExpectedReturnDate { get; set; }

        public int CheckoutId { get; set; }

        public int NumberOfDaysLate { get; set; }
        
        public decimal PenaltyFees { get; set; }
    }
}