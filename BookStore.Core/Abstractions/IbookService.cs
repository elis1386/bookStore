using System;
using BookStore.Core.Models;

namespace BookStore.Core.Abstractions;

public interface IBooksService
{
    Task<List<Book>> GetAllBooks();
    Task<Book?> GetBookById(Guid id);
    Task<Guid> AddNewBook(Book book);
    Task<Guid> UpdateCurrentBook(Guid id, string title, string description, decimal price);
    Task<Guid> DeleteCurrentBook(Guid id);

}
