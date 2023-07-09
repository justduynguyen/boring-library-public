using System;
using Volo.Abp.Application.Dtos;

namespace BoringLibrary.Books;

public class BookDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public int Credit { get; set; }
    public int Quantity { get; set; }
}