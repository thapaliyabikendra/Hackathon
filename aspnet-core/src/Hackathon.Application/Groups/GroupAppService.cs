using Hackathon.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Hackathon.Groups
{
    [Authorize(HackathonPermissions.Groups.Default)]
    public class GroupAppService : ApplicationService, IGroupAppService
    {
        #region Properties
        private readonly IRepository<Group, Guid> _groupRepository;
        private readonly ILogger<GroupAppService> _logger;
        #endregion

        #region Constructor
        public GroupAppService(
            IRepository<Group, Guid> groupRepository,
            ILogger<GroupAppService> logger
        )
        {
            _groupRepository = groupRepository;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        [Authorize(HackathonPermissions.Groups.Default)]
        public async Task<GroupDto> GetAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"::GroupAppService:: - GetAsync - ::Started::");

                var group = await _groupRepository.FindAsync(s => s.Id == id);
                var response = ObjectMapper.Map<Group, GroupDto>(group);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::GroupAppService:: - GetAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::GroupAppService:: - GetAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Groups.Default)]
        public async Task<PagedResultDto<GroupDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, GroupFilter filter)
        {
            try
            {
                _logger.LogInformation($"::GroupAppService:: - GetListByFilterAsync - ::Started::");

                if (input.Sorting.IsNullOrWhiteSpace())
                {
                    input.Sorting = $"TournamentId";
                }

                var groups = await _groupRepository.GetQueryableAsync();

                var query = (from s in groups
                             select new GroupDto()
                             {
                                 Id = s.Id,
                                 TournamentId = s.TournamentId,
                                 GroupName = s.GroupName,
                                 CreationTime = s.CreationTime
                             })
                            .WhereIf(filter.TournamentId != null, s => s.TournamentId == filter.TournamentId).WhereIf(!string.IsNullOrEmpty(filter.GroupName), s => s.GroupName.Contains(filter.GroupName, StringComparison.OrdinalIgnoreCase));

                var dtos = await AsyncExecuter.ToListAsync(
                     query
                     .OrderBy(input.Sorting)
                     .Skip(input.SkipCount)
                     .Take(input.MaxResultCount)
                 );
                var totalCount = query.Count();

                return new PagedResultDto<GroupDto>(
                    totalCount,
                    dtos
                );
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::GroupAppService:: - GetListByFilterAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::GroupAppService:: - GetListByFilterAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Groups.Create)]
        public async Task<bool> CreateAsync(CreateUpdateGroupDto input)
        {
            try
            {
                _logger.LogInformation($"::GroupAppService:: - CreateAsync - ::Started::");

                var group = ObjectMapper.Map<CreateUpdateGroupDto, Group>(input);
                await _groupRepository.InsertAsync(group);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::GroupAppService:: - CreateAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::GroupAppService:: - CreateAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Groups.Edit)]
        public async Task<bool> UpdateAsync(Guid id, CreateUpdateGroupDto input)
        {
            try
            {
                _logger.LogInformation($"::GroupAppService:: - UpdateAsync - ::Started::");

                var group = await _groupRepository.FindAsync(x => x.Id == id);
                if (group == null)
                {
                    var msg = "Group Not Found.";
                    _logger.LogInformation($"::GroupAppService:: - UpdateAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                ObjectMapper.Map(input, group);
                await _groupRepository.UpdateAsync(group);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::GroupAppService:: - UpdateAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::GroupAppService:: - UpdateAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Groups.Delete)]
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"::GroupAppService:: - DeleteAsync - ::Started::");

                var group = await _groupRepository.FirstOrDefaultAsync(s => s.Id == id);
                if (group == null)
                {
                    var msg = "Group Not Found.";
                    _logger.LogInformation($"::GroupAppService:: - DeleteAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                await _groupRepository.DeleteAsync(id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::GroupAppService:: - DeleteAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::GroupAppService:: - DeleteAsync - ::Ended::");
            }
        }
        #endregion
    }
}
