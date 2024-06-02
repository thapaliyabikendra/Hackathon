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

namespace Hackathon.Teams
{
    [Authorize(HackathonPermissions.Teams.Default)]
    public class TeamAppService : ApplicationService, ITeamAppService
    {
        #region Properties
        private readonly IRepository<Team, Guid> _teamRepository;
        private readonly ILogger<TeamAppService> _logger;
        #endregion

        #region Constructor
        public TeamAppService(
            IRepository<Team, Guid> teamRepository,
            ILogger<TeamAppService> logger
        )
        {
            _teamRepository = teamRepository;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        [Authorize(HackathonPermissions.Teams.Default)]
        public async Task<TeamDto> GetAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"::TeamAppService:: - GetAsync - ::Started::");

                var team = await _teamRepository.FindAsync(s => s.Id == id);
                var response = ObjectMapper.Map<Team, TeamDto>(team);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TeamAppService:: - GetAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::TeamAppService:: - GetAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Teams.Default)]
        public async Task<PagedResultDto<TeamDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, TeamFilter filter)
        {
            try
            {
                _logger.LogInformation($"::TeamAppService:: - GetListByFilterAsync - ::Started::");

                if (input.Sorting.IsNullOrWhiteSpace())
                {
                    input.Sorting = $"DisplayName";
                }

                var teams = await _teamRepository.GetQueryableAsync();

                var query = (from s in teams
                             select new TeamDto()
                             {
                                 Id = s.Id,
                                 DisplayName = s.DisplayName,
                                 CreationTime = s.CreationTime
                             })
                            .WhereIf(!string.IsNullOrEmpty(filter.DisplayName), s => s.DisplayName.Contains(filter.DisplayName, StringComparison.OrdinalIgnoreCase));

                var dtos = await AsyncExecuter.ToListAsync(
                     query
                     .OrderBy(input.Sorting)
                     .Skip(input.SkipCount)
                     .Take(input.MaxResultCount)
                 );
                var totalCount = query.Count();

                return new PagedResultDto<TeamDto>(
                    totalCount,
                    dtos
                );
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TeamAppService:: - GetListByFilterAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::TeamAppService:: - GetListByFilterAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Teams.Create)]
        public async Task<bool> CreateAsync(CreateUpdateTeamDto input)
        {
            try
            {
                _logger.LogInformation($"::TeamAppService:: - CreateAsync - ::Started::");

                var team = ObjectMapper.Map<CreateUpdateTeamDto, Team>(input);
                await _teamRepository.InsertAsync(team);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TeamAppService:: - CreateAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::TeamAppService:: - CreateAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Teams.Edit)]
        public async Task<bool> UpdateAsync(Guid id, CreateUpdateTeamDto input)
        {
            try
            {
                _logger.LogInformation($"::TeamAppService:: - UpdateAsync - ::Started::");

                var team = await _teamRepository.FindAsync(x => x.Id == id);
                if (team == null)
                {
                    var msg = "Team Not Found.";
                    _logger.LogInformation($"::TeamAppService:: - UpdateAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                ObjectMapper.Map(input, team);
                await _teamRepository.UpdateAsync(team);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TeamAppService:: - UpdateAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::TeamAppService:: - UpdateAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Teams.Delete)]
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"::TeamAppService:: - DeleteAsync - ::Started::");

                var team = await _teamRepository.FirstOrDefaultAsync(s => s.Id == id);
                if (team == null)
                {
                    var msg = "Team Not Found.";
                    _logger.LogInformation($"::TeamAppService:: - DeleteAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                await _teamRepository.DeleteAsync(id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TeamAppService:: - DeleteAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::TeamAppService:: - DeleteAsync - ::Ended::");
            }
        }
        #endregion
    }
}
