using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Hackathon.Data;

/* This is used if database provider does't define
 * IHackathonDbSchemaMigrator implementation.
 */
public class NullHackathonDbSchemaMigrator : IHackathonDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
