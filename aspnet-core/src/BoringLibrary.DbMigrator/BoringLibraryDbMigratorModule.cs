using BoringLibrary.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace BoringLibrary.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BoringLibraryEntityFrameworkCoreModule),
    typeof(BoringLibraryApplicationContractsModule)
)]
public class BoringLibraryDbMigratorModule : AbpModule
{
}