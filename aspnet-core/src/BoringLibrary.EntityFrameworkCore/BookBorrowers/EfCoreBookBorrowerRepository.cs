using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoringLibrary.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;

namespace BoringLibrary.BookBorrowers;

public class EfCoreBookBorrowerRepository : EfCoreRepository<BoringLibraryDbContext, BookBorrower, Guid>,
    IBookBorrowerRepository
{
    private readonly ICurrentUser _currentUser;

    public EfCoreBookBorrowerRepository(IDbContextProvider<BoringLibraryDbContext> dbContextProvider,
        ICurrentUser currentUser) : base(
        dbContextProvider)
    {
        _currentUser = currentUser;
    }

    public async Task<List<BookBorrowerWithDetails>> GetListAsync(string filter, int skipCount, int maxResultCount,
        Guid? userId)
    {
        var query = await ApplyFilterAsync(userId);
        var currentDate = BoringLibraryConsts.IsTestDev
            ? DateTime.Now.AddDays(BoringLibraryConsts.DefaultAddingDaysDueDate)
            : DateTime.Now;

        switch (filter)
        {
            case BoringLibraryConsts.ApproachingExpDate:
            {
                query = query.Where(e => e.DueDate >= currentDate && e.DueDate <= currentDate);
                break;
            }
            case BoringLibraryConsts.Overdue:
            {
                query = query.Where(e => e.DueDate < currentDate);
                break;
            }
            case BoringLibraryConsts.WithinDueDate:
            {
                query = query.Where(e => e.DueDate > currentDate);
                break;
            }
        }

        return await query
            .OrderBy(e => e.Id)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(); //bug
    }

    public async Task<List<BookBorrowerWithDetails>> GetListBorrowedAsync(Guid? userId)
    {
        var query = await ApplyFilterAsync(userId);
        return await query.ToListAsync(); //bug
    }

    public async Task<int> CountAsync()
    {
        var bookBorrowerQuery = await GetQueryableAsync();
        var currentUserId = _currentUser.Id;
        return await bookBorrowerQuery
            .Where(e => e.UserId == currentUserId)
            .CountAsync();
    }

    public async Task<List<BookBorrowerWithDetails>> GetListOverdueDate()
    {
        var currentDate = BoringLibraryConsts.IsTestDev
            ? DateTime.Now.AddDays(BoringLibraryConsts.DefaultAddingDaysDueDate)
            : DateTime.Now;
        var query = await ApplyFilterAsync(null);
        return await query.Where(e => currentDate > e.DueDate)
            .ToListAsync();
    }

    public async Task<IQueryable<BookBorrowerWithDetails>> ApplyFilterAsync(Guid? userId)
    {
        var dbContext = await GetDbContextAsync();
        var bookBorrowerQuery = await GetQueryableAsync();
        if (userId != null)
        {
            bookBorrowerQuery = bookBorrowerQuery
                .Where(e => e.UserId == userId);
        }

        return bookBorrowerQuery
            .Join(dbContext.Books,
                borrow => borrow.BookId,
                book => book.Id,
                (borrow, book) => new { borrow, book })
            .Select(item => new BookBorrowerWithDetails
            {
                Id = item.borrow.Id,
                UserId = item.borrow.UserId,
                BookId = item.borrow.BookId,
                BookName = item.book.Name,
                BookCredit = item.book.Credit,
                DueDate = item.borrow.DueDate
            }); //.AsNoTracking();
    }
}