using BoringLibrary.Localization;
using Volo.Abp.Application.Services;

namespace BoringLibrary;

/* Inherit your application services from this class.
 */
public abstract class BoringLibraryAppService : ApplicationService
{
    protected BoringLibraryAppService()
    {
        LocalizationResource = typeof(BoringLibraryResource);
    }
}