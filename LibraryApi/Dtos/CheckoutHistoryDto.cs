using System;

namespace LibraryApi.Dtos
{
    public class CheckoutHistoryDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime CheckoutDate { get; set; }

        public DateTime ExpectedReturnDate { get; set; }

    }
}