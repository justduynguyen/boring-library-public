using Volo.Abp.Settings;

namespace BoringLibrary.Settings;

public class BoringLibrarySettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BoringLibrarySettings.MySetting1));
    }
}