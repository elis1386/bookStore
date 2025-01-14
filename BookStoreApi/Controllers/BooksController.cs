using BookStore.Core.Abstractions;
using BookStore.Core.Models;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponse>> GetBook(Guid id)
        {
            var book = await _booksService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var response = new BookResponse(book.Id, book.Title, book.Description, book.Price);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddBook([FromBody] BookRequest request)
        {
            var (book, error) = Book.Create(Guid.NewGuid(), request.Title, request.Description, request.Price);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            var id = await _booksService.AddNewBook(book);
            return Ok(id);
        }
       
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BookRequest request)
        {
           var bookId = await _booksService.UpdateCurrentBook(id, request.Title, request.Description, request.Price);
           return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            var bookId = await _booksService.DeleteCurrentBook(id);
            return Ok(bookId);
        }
    }
}