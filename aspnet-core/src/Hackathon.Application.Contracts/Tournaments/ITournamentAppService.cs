using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Hackathon.Tournaments
{
    public interface ITournamentAppService : IApplicationService
    {
        public Task<TournamentDto> GetAsync(Guid id);
        public Task<PagedResultDto<TournamentDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, TournamentFilter filter);
        public Task<bool> CreateAsync(CreateUpdateTournamentDto input);
        public Task<bool> UpdateAsync(Guid id, CreateUpdateTournamentDto input);
        public Task<bool> DeleteAsync(Guid id);
        public Task<bool> GenerateMatchesAsync(GenerateMatchDto input);
    }
}
