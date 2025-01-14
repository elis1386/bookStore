using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories;

public interface IBooksRepository
{
    Task<Guid> AddBook(Book book);
    Task<Guid> DeleteBook(Guid id);
    Task<Book?> GetBook(Guid id);
    Task<List<Book>> GetBooks();
    Task<Guid> UpdateBook(Guid id, string title, string description, decimal price);
}
