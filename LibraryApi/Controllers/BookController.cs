using System;
using System.Threading.Tasks;
using LibraryApi.Dtos;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllBooksAsync()
        {
            var response = await _bookService.GetAllBooksAsync();

            LogInformation(nameof(GetAllBooksAsync), "");

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookByIdAsync(int id)
        {
            var response = await _bookService.GetBookByIdAsync(id);

            LogInformation(nameof(GetBookByIdAsync), "");

            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddBookAsync([FromBody]AddBookDto addBook)
        {
            if(addBook is null)
            {
                _logger.LogError(new ArgumentNullException(nameof(addBook)), $"Bad Request at Add Book endpoint at {DateTime.Now}");
                return BadRequest();
            }

            var response = await _bookService.AddBookAsync(addBook);
            LogInformation(nameof(AddBookAsync), "Book was added successfully");

            return CreatedAtAction(nameof(GetBookByIdAsync), new{id = response.Id}, response);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult> CheckOutAsync([FromBody]CheckoutRequestDto checkOutRequest)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogError(new ArgumentNullException(nameof(checkOutRequest)), $"Bad Request at Check-Out Book endpoint at {DateTime.Now}");
                return BadRequest();
            }

            var response = await _bookService.CheckoutAsync(checkOutRequest);

            if(response.BooksCheckedOut is null)
            {
                LogInformation(nameof(CheckOutAsync), response.Message);
                return Ok(response.Message);
            }

            return Created("/", response);
        }

        [HttpPost("checkin")]
        public async Task<ActionResult> CheckInAsync([FromBody]CheckInRequestDto checkInRequest)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogError(new ArgumentNullException(nameof(checkInRequest)), $"Bad Request at Check-In Book endpoint at {DateTime.Now}");
                return BadRequest();
            }

            var response = await _bookService.CheckInAsync(checkInRequest);

            if(response.BooksCheckedIn is null)
            {
                LogInformation(nameof(CheckInAsync), response.Message);
                return Ok(response.Message);
            }

            return Created("/", response);
        }

        [HttpGet("checkin/details")]
        public async Task<ActionResult> CheckInDetailsAsync([FromBody]CheckInRequestDto checkInRequest)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogError(new ArgumentNullException(nameof(checkInRequest)), $"Bad Request at Check-In Detail endpoint at {DateTime.Now}");
                return BadRequest();
            }

            var response = await _bookService.GetCheckIndetailsAsync(checkInRequest);

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery]string searchParam, [FromQuery]bool? available)
        {
            available = available.HasValue ? available.Value : null;

            var response = await _bookService.SearchAsync(searchParam, available);

            LogInformation(nameof(Search), "");

            return Ok(response);
        }

        private void LogInformation(string endpointName, string message)
        {
            _logger.LogInformation($"{endpointName} endpoint was accessed at {DateTime.Now}. {message}");
        }
    }
}