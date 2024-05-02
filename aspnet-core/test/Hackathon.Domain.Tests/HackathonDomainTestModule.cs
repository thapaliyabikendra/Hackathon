using Volo.Abp.Modularity;

namespace Hackathon;

[DependsOn(
    typeof(HackathonDomainModule),
    typeof(HackathonTestBaseModule)
)]
public class HackathonDomainTestModule : AbpModule
{

}
