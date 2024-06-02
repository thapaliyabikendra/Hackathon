using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Hackathon.Teams
{
    public interface ITeamAppService : IApplicationService
    {
        public Task<TeamDto> GetAsync(Guid id);
        public Task<PagedResultDto<TeamDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, TeamFilter filter);
        public Task<bool> CreateAsync(CreateUpdateTeamDto input);
        public Task<bool> UpdateAsync(Guid id, CreateUpdateTeamDto input);
        public Task<bool> DeleteAsync(Guid id);
    }
}
