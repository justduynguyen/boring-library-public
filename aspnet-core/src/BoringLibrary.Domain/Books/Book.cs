using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace BoringLibrary.Books;

public class Book : AuditedAggregateRoot<Guid>
{
    public Book(Guid id, string name, int credit = BoringLibraryConsts.DefaultBookCredit,
        int quantity = BoringLibraryConsts.DefaultQuantity) : base(id)
    {
        Name = name;
        Credit = credit;
        Quantity = quantity;
    }

    public Book()
    {
    }

    public string Name { get; set; }
    public int Credit { get; set; }
    public int Quantity { get; set; }
}