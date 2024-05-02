using Volo.Abp.Modularity;

namespace Hackathon;

[DependsOn(
    typeof(HackathonApplicationModule),
    typeof(HackathonDomainTestModule)
)]
public class HackathonApplicationTestModule : AbpModule
{

}
