namespace BookStoreApi.Contracts;

public record BookResponse(
    Guid Id, 
    string Title, 
    string Description, 
    decimal Price
    );

public record BookRequest(
    string Title, 
    string Description, 
    decimal Price
    );