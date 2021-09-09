using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos
{
    public class GetBookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishYear { get; set; }

        public decimal CoverPrice { get; set; }

        public string Availability { get; set; }
    }
}