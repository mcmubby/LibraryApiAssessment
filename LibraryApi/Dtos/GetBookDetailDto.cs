using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryApi.Data.Entities;
using LibraryApi.Dtos;

namespace LibraryApi.Dtos
{
    public class GetBookDetailDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public string PublishYear { get; set; }

        public decimal CoverPrice { get; set; }

        public string Availability { get; set; }

        public List<CheckoutHistoryDto> CheckoutHistory { get; set; } = new List<CheckoutHistoryDto>();
    }
}