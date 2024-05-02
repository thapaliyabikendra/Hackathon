using Volo.Abp.Modularity;

namespace Hackathon;

/* Inherit from this class for your domain layer tests. */
public abstract class HackathonDomainTestBase<TStartupModule> : HackathonTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
