using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Hackathon.Groups
{
    public interface IGroupAppService : IApplicationService
    {
        public Task<GroupDto> GetAsync(Guid id);
        public Task<PagedResultDto<GroupDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, GroupFilter filter);
        public Task<bool> CreateAsync(CreateUpdateGroupDto input);
        public Task<bool> UpdateAsync(Guid id, CreateUpdateGroupDto input);
        public Task<bool> DeleteAsync(Guid id);
    }
}
