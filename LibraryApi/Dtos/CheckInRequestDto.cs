using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos
{
    public class CheckInRequestDto
    {
        [Required]
        public string NationalIdentificationNumber { get; set; }

        [Required]
        public List<int> BooksId { get; set; }
    }
}