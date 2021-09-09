using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public interface IBookService
    {
        Task AddBookAsync(AddBookDto book);

        Task<List<GetBookDto>> GetAllBooksAsync();

        Task<GetBookDetailDto> GetBookByIdAsync(int bookId);

        Task<List<GetBookDto>> SearchAsync(string searchParam, bool? isAvailable);

        //Task<BookCheckoutDto> CheckoutAsync(List<int> bookIds);
    }
}