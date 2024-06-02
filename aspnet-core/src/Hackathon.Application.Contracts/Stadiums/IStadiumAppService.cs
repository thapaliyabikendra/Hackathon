using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Hackathon.Stadiums
{
    public interface IStadiumAppService : IApplicationService
    {
        public Task<StadiumDto> GetAsync(Guid id);
        public Task<PagedResultDto<StadiumDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, StadiumFilter filter);
        public Task<bool> CreateAsync(CreateUpdateStadiumDto input);
        public Task<bool> UpdateAsync(Guid id, CreateUpdateStadiumDto input);
        public Task<bool> DeleteAsync(Guid id);
    }
}
