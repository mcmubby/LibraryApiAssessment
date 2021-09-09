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

        public async Task<CheckoutResponseDto> CheckoutAsync(CheckoutRequestDto checkoutRequest)
        {
            if(checkoutRequest == null || checkoutRequest.BooksId.Count == 0) 
                return new CheckoutResponseDto{ Message = "Invalid request body", BooksCheckedOut = null };

            var booksToCheckout = new List<Book>();
            booksToCheckout = await _context.Books.Where(b => checkoutRequest.BooksId.Contains(b.Id) && b.IsAvailable == true)
                                                      .ToListAsync();

            if(booksToCheckout.Count == 0) 
                return new CheckoutResponseDto{ Message = "Book(s) not found or unavailable for check-out", BooksCheckedOut = null };

            var booksCheckedOutList = new List<Checkout>();
            var response = new CheckoutResponseDto();

            for (int i = 0; i < booksToCheckout.Count; i++)
            {
                booksCheckedOutList.Add(CreateCheckout(booksToCheckout[i].Id, checkoutRequest));
                booksToCheckout[i].IsAvailable = false;
                response.BooksCheckedOut.Add(booksToCheckout[i].AsGetBookDto());
            }

            await _context.Checkouts.AddRangeAsync(booksCheckedOutList);
            await _context.SaveChangesAsync();

            response.Message = "Successfully checked-out book(s)";
            response.DueDate = new WorkingDayHelper().FuturWorkingDays(DateTime.Now, 10);

            return response;
        }

        public async Task<CheckInResponseDto> CheckInAsync(CheckInRequestDto checkInRequest)
        {
            if(checkInRequest == null || checkInRequest.BooksId.Count == 0) 
                return new CheckInResponseDto{ Message = "Invalid request body", BooksCheckedIn = null };

            var checkoutDetails = new List<Checkout>();
            checkoutDetails = await _context.Checkouts.Where(c => checkInRequest.BooksId.Contains(c.Id) && c.NationalIdentificationNumber == checkInRequest.NationalIdentificationNumber)
                                                     .Include(b => b.Book)
                                                     .ToListAsync();

            if(checkoutDetails.Count == 0) 
                return new CheckInResponseDto{ Message = "Book(s) not found or already checked-in", BooksCheckedIn = null };

            var response = new CheckInResponseDto();
            var penaltyDetails = new List<LateCheckIn>();
            int daysLate;

            for (int i = 0; i < checkoutDetails.Count; i++)
            {
                checkoutDetails[i].Book.IsAvailable = true;
                response.BooksCheckedIn.Add(checkoutDetails[i].Book.AsGetBookDto());

                //Calculate are record check-in over due details
                daysLate = CalculateOverDueDays(checkoutDetails[i]);
                if(daysLate > 0) { penaltyDetails.Add(checkoutDetails[i].AsLateCheckInDto(daysLate)); }
            }

            await _context.LateCheckIns.AddRangeAsync(penaltyDetails);
            await _context.SaveChangesAsync();

            response.Message = "Successfully checked-out book(s)";
            return response;
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

        public async Task<List<CheckInDetailsDto>> GetCheckIndetailsAsync(CheckInRequestDto checkInRequest)
        {
            if(checkInRequest == null || checkInRequest.BooksId.Count == 0) 
                return null;

            var checkoutDetails = new List<Checkout>();
            checkoutDetails = await _context.Checkouts.Where(c => checkInRequest.BooksId.Contains(c.Id) && c.NationalIdentificationNumber == checkInRequest.NationalIdentificationNumber)
                                                     .Include(b => b.Book)
                                                     .ToListAsync();

            if(checkoutDetails.Count == 0) 
                return null;

            var response = new List<CheckInDetailsDto>();
            int daysLate;

            for (int i = 0; i < checkoutDetails.Count; i++)
            {
                daysLate = CalculateOverDueDays(checkoutDetails[i]);
                response.Add(checkoutDetails[i].AsCheckInDetailsDto(daysLate));
            }

            return response;
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

        private static int CalculateOverDueDays(Checkout checkout)
        {
            var workingDays = new WorkingDayHelper();

            var dateDifference = DateTime.Now - checkout.ExpectedReturnDate;

            if(dateDifference.Days < 0) return 0;
            
            var workingDaysOverDue = workingDays.GetSpanDays(DateTime.Now, dateDifference);

            return workingDaysOverDue;
        }

    }
}