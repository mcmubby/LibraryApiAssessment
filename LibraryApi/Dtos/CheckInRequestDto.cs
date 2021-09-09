using System.Collections.Generic;

namespace LibraryApi.Dtos
{
    public class CheckInRequestDto
    {
        public string NationalIdentificationNumber { get; set; }
        public List<int> BooksId { get; set; }
    }
}