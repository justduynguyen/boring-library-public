using BoringLibrary.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BoringLibrary.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BoringLibraryController : AbpControllerBase
{
    protected BoringLibraryController()
    {
        LocalizationResource = typeof(BoringLibraryResource);
    }
}