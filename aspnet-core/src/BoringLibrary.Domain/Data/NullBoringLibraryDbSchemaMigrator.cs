using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BoringLibrary.Data;

/* This is used if database provider does't define
 * IBoringLibraryDbSchemaMigrator implementation.
 */
public class NullBoringLibraryDbSchemaMigrator : IBoringLibraryDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}