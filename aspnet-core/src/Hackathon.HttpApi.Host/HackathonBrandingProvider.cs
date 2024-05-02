using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Hackathon;

[Dependency(ReplaceServices = true)]
public class HackathonBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Hackathon";
}
