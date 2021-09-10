using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos
{
    public class CheckoutRequestDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string NationalIdentificationNumber { get; set; }

        [Required]
        public DateTime CheckoutDate { get; set; }

        [Required]
        public List<int> BooksId { get; set; }
    }
}