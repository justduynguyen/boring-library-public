using System;
using Volo.Abp.Application.Dtos;

namespace BoringLibrary.BookBorrowers;

public class BookBorrowerDto : EntityDto<Guid>
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public DateTime DueDate { get; set; }
}