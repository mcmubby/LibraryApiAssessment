using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.Data;
using LibraryApi.Data.Entities;
using LibraryApi.Dtos;
using Microsoft.EntityFrameworkCore;
using WorkingDaysManagement;

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

        public async Task<BookCheckoutDto> CheckoutAsync(CheckoutRequestDto checkoutRequest)
        {
            if(checkoutRequest == null || checkoutRequest.BooksId.Count == 0) return null;

            var booksToCheckout = new List<Book>();
            booksToCheckout = await _context.Books.Where(b => checkoutRequest.BooksId.Contains(b.Id) && b.IsAvailable == true)
                                                      .ToListAsync();

            if(booksToCheckout.Count == 0) return null;

            var checkoutList = new List<Checkout>();

            for (int i = 0; i < booksToCheckout.Count; i++)
            {
                checkoutList.Add(CreateCheckout(booksToCheckout[i].Id, checkoutRequest));
                booksToCheckout[i].IsAvailable = false;
            }

            Add and save changes
        }

        public async Task<List<GetBookDto>> GetAllBooksAsync()
        {
            var books = new List<GetBookDto>();

            var collection = await _context.Books.OrderBy(b => b.PublishYear).ToListAsync();

            if(collection is null) return null;

            foreach (var book in collection)
            {
                books.Add(book.AsGetBookDto());
            }

            return books;
        }

        public async Task<GetBookDetailDto> GetBookByIdAsync(int bookId)
        {
            var bookDetails = await _context.Books.Where(b => b.Id == bookId)
                                                  .Include(c => c.CheckoutHistory.OrderByDescending(h => h.Id))
                                                  .FirstOrDefaultAsync();
            
            if(bookDetails is null) return null;

            return bookDetails.AsGetBookdetailDto();
        }

        public async Task<List<GetBookDto>> SearchAsync(string searchParam, bool? isAvailable)
        {
            if(string.IsNullOrWhiteSpace(searchParam)) return await GetAllBooksAsync();

            var searchResult = new List<GetBookDto>();
            var collection = _context.Books as IQueryable<Book>;

            if (isAvailable is null)
            {
                collection = collection.AsQueryable()
                                       .Where(b => b.Title.Contains(searchParam) || b.ISBN.Contains(searchParam))
                                       .OrderBy(c => c.PublishYear);
            }
            else
            {
                collection = collection.AsQueryable()
                                       .Where(b => b.IsAvailable == isAvailable.Value && (b.Title.Contains(searchParam) || b.ISBN.Contains(searchParam)))
                                       .OrderBy(c => c.PublishYear);
            }

            var books = await collection.ToListAsync();

            if (books is null) return null;

            foreach (var book in books)
            {
                searchResult.Add(book.AsGetBookDto());
            }

            return searchResult;
        }

        private static Checkout CreateCheckout(int bookId, CheckoutRequestDto customerDetail)
        {
            var workingDays = new WorkingDayHelper();
            return new Checkout
            {
                BookId = bookId,
                FullName = customerDetail.FullName,
                Email = customerDetail.Email,
                PhoneNumber = customerDetail.PhoneNumber,
                NationalIdentificationNumber = customerDetail.NationalIdentificationNumber,
                CheckoutDate = DateTime.Now,
                ExpectedReturnDate = workingDays.FuturWorkingDays(DateTime.Now, 10)
            };
        }
    }
}