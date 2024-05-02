using Hackathon.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Hackathon.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HackathonEntityFrameworkCoreModule),
    typeof(HackathonApplicationContractsModule)
    )]
public class HackathonDbMigratorModule : AbpModule
{
}
