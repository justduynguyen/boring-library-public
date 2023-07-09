using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BoringLibrary.Books;

public class BookService :
    CrudAppService<
        Book, //The entity
        BookDto, //Used to show
        Guid, //Primary key of the entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateBookDto>, //Used to create/update
    IBookService //implement the Entity AppService Interface
{
    public BookService(IRepository<Book, Guid> repository)
        : base(repository)
    {
    }
}