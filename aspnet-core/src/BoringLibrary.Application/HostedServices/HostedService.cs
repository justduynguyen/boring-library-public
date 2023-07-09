using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BoringLibrary.BookBorrowers;
using BoringLibrary.CustomUsers;
using Hangfire;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace BoringLibrary.HostedServices;

public class HostedService : IHostedService
{
    private readonly IBookBorrowerRepository _bookBorrowerRepository;
    private readonly IRepository<IdentityUser, Guid> _userRepository;

    public HostedService(IRepository<IdentityUser, Guid> userRepository, IBookBorrowerRepository bookBorrowerRepository)
    {
        _userRepository = userRepository;
        _bookBorrowerRepository = bookBorrowerRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        RecurringJob.RemoveIfExists("HostedService.CheckAndCalculatingBookBorrowerAsync");
        RecurringJob.AddOrUpdate(() => CheckAndCalculatingBookBorrowerAsync(),
            BoringLibraryConsts.IsTestDev
                ? BoringLibraryConsts.CronEveryMinuteExpression
                : BoringLibraryConsts.CronDailyExpression);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task CheckAndCalculatingBookBorrowerAsync()
    {
        var listOverdueDate =
            await _bookBorrowerRepository.GetListOverdueDate();
        var users = await _userRepository.GetListAsync(e => listOverdueDate.Select(b => b.UserId).Any(b => b == e.Id));
        listOverdueDate.ForEach(item =>
        {
            var user = users.FirstOrDefault(e => e.Id == item.UserId);
            var currentCredit = user.GetCredit();
            currentCredit -= (double)item.BookCredit * BoringLibraryConsts.DeductionRate / 100;
            user.SetCredit(currentCredit);
        });
        await _userRepository.UpdateManyAsync(users);
    }
}