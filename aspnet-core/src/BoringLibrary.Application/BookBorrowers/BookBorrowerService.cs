using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoringLibrary.Books;
using BoringLibrary.CustomUsers;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace BoringLibrary.BookBorrowers;

[Authorize]
public class BookBorrowerService : BoringLibraryAppService
{
    private readonly IBookBorrowerRepository _bookBorrowerRepository;
    private readonly IRepository<Book, Guid> _bookRepository;
    private readonly IRepository<IdentityUser, Guid> _userRepository;

    public BookBorrowerService(IRepository<Book, Guid> bookRepository, IRepository<IdentityUser, Guid> userRepository,
        IBookBorrowerRepository bookBorrowerRepository)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _bookBorrowerRepository = bookBorrowerRepository;
    }

    public async Task CreateAsync(CreateBookBorrowerDto input)
    {
        var book = await _bookRepository.GetAsync(input.BookId);
        var user = await _userRepository.GetAsync(e => e.Id == CurrentUser.Id);
        var borrows = await _bookBorrowerRepository.GetListBorrowedAsync(CurrentUser.Id);
        var userCredit = user.GetCredit();
        if (book.Credit > userCredit || book.Quantity < 1 || borrows.Count >= BoringLibraryConsts.MaximumBorrow)
        {
            throw new BusinessException("Cannot borrowed");
        }

        book.Quantity -= 1;
        user.SetCredit(userCredit - book.Credit);
        var bookBorrower = new BookBorrower(Guid.NewGuid(), user.Id, book.Id);
        await _bookBorrowerRepository.InsertAsync(bookBorrower);
    }

    public async Task<PagedResultDto<BookBorrowerWithDetailsDto>> GetListAsync(string filter, int skipCount,
        int maxCount = 10)
    {
        var borrows = await _bookBorrowerRepository.GetListAsync(filter, skipCount, maxCount, CurrentUser.Id);
        var count = await _bookBorrowerRepository.CountAsync();
        return new PagedResultDto<BookBorrowerWithDetailsDto>(count,
            ObjectMapper.Map<List<BookBorrowerWithDetails>, List<BookBorrowerWithDetailsDto>>(borrows));
    }
}