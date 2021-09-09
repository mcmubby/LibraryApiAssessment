using System;
using System.Collections.Generic;
using LibraryApi.Data.Entities;

namespace LibraryApi.Dtos
{
    public class CheckoutResponseDto
    {
        public string Message { get; set; }
        public List<GetBookDto> BooksCheckedOut { get; set; } = new List<GetBookDto>();
        public DateTime DueDate { get; set; }
    }
}