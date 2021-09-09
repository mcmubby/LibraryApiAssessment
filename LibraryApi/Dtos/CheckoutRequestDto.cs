using System;
using System.Collections.Generic;

namespace LibraryApi.Dtos
{
    public class CheckoutRequestDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }

        public string NationalIdentificationNumber { get; set; }

        public DateTime CheckoutDate { get; set; }

        public DateTime DueDate { get; set; }

        public List<int> BooksId { get; set; }
    }
}