using System;
using System.Collections.Generic;
using LibraryApi.Data.Entities;
using LibraryApi.Dtos;

namespace LibraryApi
{
    public static class Extensions
    {
        public static GetBookDto AsGetBookDto(this Book book)
        {
            return new GetBookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishYear = book.PublishYear,
                CoverPrice = book.CoverPrice,
                Availability = book.IsAvailable ? "Available" : "Checked Out"
            };
        }

        public static GetBookDetailDto AsGetBookdetailDto(this Book book)
        {
            return new GetBookDetailDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishYear = book.PublishYear,
                CoverPrice = book.CoverPrice,
                Availability = book.IsAvailable ? "Available" : "Checked Out",
                CheckoutHistory = book.CheckoutHistory is null ? null : book.CheckoutHistory.AsListOfCheckoutHistoryDto()
            };
        }

        public static List<CheckoutHistoryDto> AsListOfCheckoutHistoryDto(this List<Checkout> checkOutHistory)
        {
            var history = new List<CheckoutHistoryDto>();

            foreach (var checkout in checkOutHistory)
            {
                history.Add(checkout.AsCheckoutHistoryDto());
            }

            return history;
        }

        public static LateCheckIn AsLateCheckInDto(this Checkout checkout, int daysLate)
        {
            return new LateCheckIn
            {
                CheckoutId = checkout.Id,
                ReturnDate = DateTime.Now,
                CheckoutDate = checkout.CheckoutDate,
                ExpectedReturnDate = checkout.ExpectedReturnDate,
                NumberOfDaysLate = daysLate,
                PenaltyFees = daysLate * 200
            };
        }

        public static CheckInDetailsDto AsCheckInDetailsDto(this Checkout checkout, int daysLate)
        {
            return new CheckInDetailsDto
            {
                BookId = checkout.BookId,
                BookTitle = checkout.Book.Title,
                CheckoutId = checkout.Id,
                CheckoutDate = checkout.CheckoutDate,
                ExpectedReturnDate = checkout.ExpectedReturnDate,
                NumberOfDaysLate = daysLate,
                PenaltyFees = daysLate < 1 ? 0 : daysLate * 200
            };
        }

        private static CheckoutHistoryDto AsCheckoutHistoryDto(this Checkout checkout)
        {
            return new CheckoutHistoryDto
            {
                Id = checkout.Id,
                FullName = checkout.FullName,
                CheckoutDate = checkout.CheckoutDate,
                ExpectedReturnDate = checkout.ExpectedReturnDate
            };
        }
    }
}