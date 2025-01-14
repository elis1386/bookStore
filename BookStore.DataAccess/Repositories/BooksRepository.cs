using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDBContext _context;

        public BooksRepository(BookStoreDBContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooks()
        {
            var bookEntity = await _context.Books.AsNoTracking().ToListAsync();
            var books = bookEntity
                .Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price).Book)
                .ToList();

            return books;
        }

        public async Task<Book?> GetBook(Guid id)
        {
            var bookEntity = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            if (bookEntity == null)
            {
                return null;
            }
            var book = Book.Create(bookEntity.Id, bookEntity.Title, bookEntity.Description, bookEntity.Price).Book;
            return book;
        }

        public async Task<Guid> AddBook(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price
            };
            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();
            return bookEntity.Id;
        }

        public async Task<Guid> UpdateBook(Guid id, string title, string description, decimal price)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Title, b => title)
                .SetProperty(b => b.Description, b => description)
                .SetProperty(b => b.Price, b => price));

            return id;
        }

        public async Task<Guid> DeleteBook(Guid id)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();
            return id;
        }
    }
}