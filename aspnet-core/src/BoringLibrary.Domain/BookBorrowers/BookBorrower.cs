using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace BoringLibrary.BookBorrowers;

public class BookBorrower : AuditedAggregateRoot<Guid>
{
    private BookBorrower()
    {
    }

    public BookBorrower(Guid id, Guid userId, Guid bookId) : base(id)
    {
        BookId = bookId;
        UserId = userId;
        DueDate = DateTime.Now.AddDays(BoringLibraryConsts.DefaultAddingDaysDueDate);
    }

    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public DateTime DueDate { get; private set; }
}