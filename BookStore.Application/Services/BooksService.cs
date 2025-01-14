using System;
using BookStore.Core.Abstractions;
using BookStore.Core.Models;
using BookStore.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BookStore.Application.Services;

public class BooksService(IBooksRepository booksRepository): IBooksService
{
    private readonly IBooksRepository _booksRepository = booksRepository;

    public async Task<List<Book>> GetAllBooks()
    {
        return await _booksRepository.GetBooks();
    }

    public async Task<Book?> GetBookById(Guid id)
    {
        return await _booksRepository.GetBook(id);
    }

    public async Task<Guid> AddNewBook(Book book)
    {
        return await _booksRepository.AddBook(book);
    }

    public async Task<Guid> UpdateCurrentBook(Guid id, string title, string description, decimal price)
    {
        return await _booksRepository.UpdateBook(id, title, description, price);
    }

    public async Task<Guid> DeleteCurrentBook(Guid id)
    {
        return await _booksRepository.DeleteBook(id);
    }
}
