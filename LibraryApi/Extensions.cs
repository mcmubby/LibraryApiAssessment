using LibraryApi.Data.Entities;

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
    }
}