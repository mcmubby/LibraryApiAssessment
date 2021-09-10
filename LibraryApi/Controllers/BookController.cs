using System;
using System.Threading.Tasks;
using LibraryApi.Dtos;
using LibraryApi.Helper;
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

        private static readonly string[] UserType = new[]
        {
            "Unauthorized user", "Admin", "Authenticated User"
        };

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllBooksAsync()
        {
            var userId = CheckUser.GetAuthenticatedUser(HttpContext);

            if(userId == 0)
            {
                LogInformation(nameof(GetAllBooksAsync), UserType[userId], "Access denied");
                return Unauthorized();
            }

            var response = await _bookService.GetAllBooksAsync();

            LogInformation(nameof(GetAllBooksAsync), UserType[userId], "Access granted, response returned");

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookByIdAsync(int id)
        {
            var userId = CheckUser.GetAuthenticatedUser(HttpContext);

            if(userId == 0)
            {
                LogInformation(nameof(GetBookByIdAsync), UserType[userId], "Access denied");
                return Unauthorized();
            }

            var response = await _bookService.GetBookByIdAsync(id);

            LogInformation(nameof(GetBookByIdAsync), UserType[userId], "Access granted, response returned");

            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddBookAsync([FromBody]AddBookDto addBook)
        {
            var userId = CheckUser.GetAuthenticatedUser(HttpContext);

            if(userId != 1)
            {
                LogInformation(nameof(GetBookByIdAsync), UserType[userId], "Access denied");
                return Unauthorized();
            }

            if(addBook is null)
            {
                _logger.LogError(new ArgumentNullException(nameof(addBook)), $"Bad Request at Add Book endpoint at {DateTime.Now}");
                return BadRequest();
            }

            var response = await _bookService.AddBookAsync(addBook);
            if(response is null)
            {
                LogInformation(nameof(AddBookAsync), UserType[userId], "Access granted. \nBook already exist");
                return Ok(new {Message = "Book already exist"});  
            } 
            
            LogInformation(nameof(AddBookAsync), UserType[userId], "Access granted. \nBook was added successfully");

            return CreatedAtAction(nameof(GetBookByIdAsync), new{id = response.Id}, response);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult> CheckOutAsync([FromBody]CheckoutRequestDto checkOutRequest)
        {
            var userId = CheckUser.GetAuthenticatedUser(HttpContext);

            if(userId != 1)
            {
                LogInformation(nameof(GetBookByIdAsync), UserType[userId], "Access denied");
                return Unauthorized();
            }

            if(!ModelState.IsValid)
            {
                _logger.LogError(new ArgumentNullException(nameof(checkOutRequest)), $"Bad Request at Check-Out Book endpoint at {DateTime.Now}");
                return BadRequest();
            }

            var response = await _bookService.CheckoutAsync(checkOutRequest);

            if(response.BooksCheckedOut is null)
            {
                LogInformation(nameof(CheckOutAsync), UserType[userId], response.Message);
                return Ok(response.Message);
            }

            return Created("/", response);
        }

        [HttpPost("checkin")]
        public async Task<ActionResult> CheckInAsync([FromBody]CheckInRequestDto checkInRequest)
        {
            var userId = CheckUser.GetAuthenticatedUser(HttpContext);

            if(userId != 1)
            {
                LogInformation(nameof(GetBookByIdAsync), UserType[userId], "Access denied");
                return Unauthorized();
            }

            if(!ModelState.IsValid)
            {
                _logger.LogError(new ArgumentNullException(nameof(checkInRequest)), $"Bad Request at Check-In Book endpoint at {DateTime.Now}");
                return BadRequest();
            }

            var response = await _bookService.CheckInAsync(checkInRequest);

            if(response.BooksCheckedIn is null)
            {
                LogInformation(nameof(CheckInAsync), UserType[userId], response.Message);
                return Ok(response.Message);
            }

            LogInformation(nameof(CheckInAsync), UserType[userId], response.Message);
            return Created("/", response);
        }

        [HttpGet("checkin/details/{nationalIdentificationNumber}/{bookId}")]
        public async Task<ActionResult> CheckInDetailsAsync(string nationalIdentificationNumber, int bookId)
        {
            var userId = CheckUser.GetAuthenticatedUser(HttpContext);

            if(userId != 1)
            {
                LogInformation(nameof(GetBookByIdAsync), UserType[userId], "Access denied");
                return Unauthorized();
            }

            var response = await _bookService.GetCheckIndetailsAsync(nationalIdentificationNumber, bookId);

            LogInformation(nameof(CheckInDetailsAsync), UserType[userId], "");

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery]string searchParam, [FromQuery]bool? available)
        {
            var userId = CheckUser.GetAuthenticatedUser(HttpContext);

            if(userId == 0)
            {
                LogInformation(nameof(GetBookByIdAsync), UserType[userId], "Access denied");
                return Unauthorized();
            }

            available = available.HasValue ? available.Value : null;

            var response = await _bookService.SearchAsync(searchParam, available);

            LogInformation(nameof(Search), UserType[userId], "Access granted.");

            return Ok(response);
        }

        private void LogInformation(string endpointName, string user, string message)
        {
            _logger.LogInformation($"{endpointName} endpoint was requested by {user} at {DateTime.Now}. {message}");
        }
    }
}