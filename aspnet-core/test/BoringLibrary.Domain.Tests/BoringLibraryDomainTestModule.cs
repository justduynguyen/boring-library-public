using BoringLibrary.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace BoringLibrary;

[DependsOn(
    typeof(BoringLibraryEntityFrameworkCoreTestModule)
)]
public class BoringLibraryDomainTestModule : AbpModule
{
}