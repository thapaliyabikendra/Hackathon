using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Hackathon.Matchs
{
    public interface IMatchAppService : IApplicationService
    {
        public Task<MatchDto> GetAsync(Guid id);
        public Task<PagedResultDto<MatchDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, MatchFilter filter);
        public Task<bool> CreateAsync(CreateUpdateMatchDto input);
        public Task<bool> UpdateAsync(Guid id, CreateUpdateMatchDto input);
        public Task<bool> DeleteAsync(Guid id);
    }
}
