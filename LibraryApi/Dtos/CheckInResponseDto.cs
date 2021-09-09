using System.Collections.Generic;
using LibraryApi.Data.Entities;

namespace LibraryApi.Dtos
{
    public class CheckInResponseDto
    {
        public string Message { get; set; }
        public List<GetBookDto> BooksCheckedIn { get; set; } = new List<GetBookDto>();
    }
}