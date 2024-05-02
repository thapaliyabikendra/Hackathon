using Volo.Abp.Settings;

namespace Hackathon.Settings;

public class HackathonSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HackathonSettings.MySetting1));
    }
}
