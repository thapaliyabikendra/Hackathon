using Volo.Abp.Modularity;

namespace Hackathon;

public abstract class HackathonApplicationTestBase<TStartupModule> : HackathonTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
