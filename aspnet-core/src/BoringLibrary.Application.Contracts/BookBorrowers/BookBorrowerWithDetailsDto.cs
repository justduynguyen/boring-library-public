using System;

namespace BoringLibrary.BookBorrowers;

public class BookBorrowerWithDetailsDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public string BookName { get; set; }
    public int BookCredit { get; set; }
    public DateTime DueDate { get; set; }
}