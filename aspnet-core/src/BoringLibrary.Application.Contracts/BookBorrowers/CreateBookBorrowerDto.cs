using System;
using System.ComponentModel.DataAnnotations;

namespace BoringLibrary.BookBorrowers;

public class CreateBookBorrowerDto
{
    [Required] public Guid BookId { get; set; }
}