using System;
using System.Threading.Tasks;
using BoringLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace BoringLibrary.EntityFrameworkCore;

public class EntityFrameworkCoreBoringLibraryDbSchemaMigrator
    : IBoringLibraryDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBoringLibraryDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BoringLibraryDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BoringLibraryDbContext>()
            .Database
            .MigrateAsync();
    }
}