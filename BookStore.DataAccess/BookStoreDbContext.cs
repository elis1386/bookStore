using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess;

public class BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : DbContext(options)
{
    public required DbSet<BookEntity> Books { get; set; }
}
