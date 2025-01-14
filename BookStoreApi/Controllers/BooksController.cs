using BookStore.Core.Abstractions;
using BookStoreApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookResponse>>> GetBooks()
        {
            var books = await _booksService.GetAllBooks();
            var response = books.Select(book => new BookResponse(book.Id, book.Title, book.Description, book.Price)).ToList();
            return Ok(response);
        }
        
    }
}