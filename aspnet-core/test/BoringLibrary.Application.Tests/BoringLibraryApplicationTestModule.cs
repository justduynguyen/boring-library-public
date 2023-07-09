using Volo.Abp.Modularity;

namespace BoringLibrary;

[DependsOn(
    typeof(BoringLibraryApplicationModule),
    typeof(BoringLibraryDomainTestModule)
)]
public class BoringLibraryApplicationTestModule : AbpModule
{
}