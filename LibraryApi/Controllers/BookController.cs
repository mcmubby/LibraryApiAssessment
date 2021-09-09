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
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddBookAsync([FromBody]AddBookDto addBook)
        {
            throw new NotImplementedException();
        }

        [HttpPost("checkout")]
        public async Task<ActionResult> CheckOutAsync([FromBody]CheckoutRequestDto checkOutRequest)
        {
            throw new NotImplementedException();
        }

        [HttpPost("checkin")]
        public async Task<ActionResult> CheckInAsync([FromBody]CheckInRequestDto checkInRequest)
        {
            throw new NotImplementedException();
        }

        [HttpGet("checkin/details")]
        public async Task<ActionResult> CheckInDetailsAsync([FromBody]CheckInRequestDto checkInRequest)
        {
            throw new NotImplementedException();
        }

        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery]string searchParam, [FromQuery]bool? available)
        {
            throw new NotImplementedException();
        }
    }
}