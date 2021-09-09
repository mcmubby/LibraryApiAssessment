using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.Data;
using LibraryApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task AddBookAsync(AddBookDto book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            var newBook = new Book
            {
                Title = book.Title,
                ISBN = book.ISBN,
                PublishYear = book.PublishYear,
                CoverPrice = book.CoverPrice,
                IsAvailable = true
            };

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetBookDto>> GetAllBooksAsync()
        {
            var books = new List<GetBookDto>();

            var response = await _context.Books.OrderBy(c => c.PublishYear).ToListAsync();

            if(response is null) return null;

            foreach (var book in response)
            {
                books.Add(book.AsGetBookDto());
            }

            return books;
        }

        public Task<GetBookWithHistoryDto> GetBookByIdAsync(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<GetBookDto>> SearchAsync(string searchParam)
        {
            throw new System.NotImplementedException();
        }
    }
}