using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Hackathon.Data;
using Volo.Abp.DependencyInjection;

namespace Hackathon.EntityFrameworkCore;

public class EntityFrameworkCoreHackathonDbSchemaMigrator
    : IHackathonDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreHackathonDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the HackathonDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<HackathonDbContext>()
            .Database
            .MigrateAsync();
    }
}
