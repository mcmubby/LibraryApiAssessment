using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public interface IBookService
    {
        Task AddBookAsync(AddBookDto book);

        Task<List<GetBookDto>> GetAllBooksAsync();

        Task<GetBookWithHistoryDto> GetBookByIdAsync(int bookId);

        Task<List<GetBookDto>> SearchAsync(string searchParam);
        
        //Task<BookCheckoutDto> CheckOutBook(List<int> bookIds);
    }
}