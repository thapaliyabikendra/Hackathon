using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Hackathon;

[Dependency(ReplaceServices = true)]
public class HackathonBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Hackathon";
}
