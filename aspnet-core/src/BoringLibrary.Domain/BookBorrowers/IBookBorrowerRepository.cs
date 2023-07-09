using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BoringLibrary.BookBorrowers;

public interface IBookBorrowerRepository : IRepository<BookBorrower, Guid>
{
    Task<List<BookBorrowerWithDetails>> GetListAsync(string filter, int skipCount, int maxResultCount, Guid? userId);
    Task<int> CountAsync();
    Task<List<BookBorrowerWithDetails>> GetListOverdueDate();
    Task<List<BookBorrowerWithDetails>> GetListBorrowedAsync(Guid? userId);
}