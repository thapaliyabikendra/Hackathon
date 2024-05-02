using System.Threading.Tasks;

namespace Hackathon.Data;

public interface IHackathonDbSchemaMigrator
{
    Task MigrateAsync();
}
