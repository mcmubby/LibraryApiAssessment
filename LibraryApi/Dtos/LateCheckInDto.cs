using System;

namespace LibraryApi
{
    public class LateCheckInDto
    {
        public DateTime CheckoutDate { get; set; }
        
        public DateTime ExpectedReturnDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int CheckoutId { get; set; }

        public int NumberOfDaysLate { get; set; }
        
        public decimal PenaltyFees { get; set; }
    }
}