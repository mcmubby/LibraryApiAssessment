using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApi.Dtos;

namespace LibraryApi.Services
{
    public interface IBookService
    {
        Task AddBookAsync(AddBookDto book);

        Task<List<GetBookDto>> GetAllBooksAsync();

        Task<GetBookDetailDto> GetBookByIdAsync(int bookId);

        Task<List<GetBookDto>> SearchAsync(string searchParam, bool? isAvailable);

        Task<CheckoutResponseDto> CheckoutAsync(CheckoutRequestDto checkoutRequest);

        Task<CheckInResponseDto> CheckInAsync(CheckInRequestDto checkInRequest);

        Task<List<CheckInDetailsDto>> GetCheckIndetailsAsync(CheckInRequestDto checkInRequest);
    }
}