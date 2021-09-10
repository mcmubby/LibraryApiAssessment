using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos
{
    public class GetBookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public string PublishYear { get; set; }

        public decimal CoverPrice { get; set; }

        public string Availability { get; set; }
    }
}