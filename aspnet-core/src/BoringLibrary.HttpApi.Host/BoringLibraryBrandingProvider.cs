using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace BoringLibrary;

[Dependency(ReplaceServices = true)]
public class BoringLibraryBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BoringLibrary";
}