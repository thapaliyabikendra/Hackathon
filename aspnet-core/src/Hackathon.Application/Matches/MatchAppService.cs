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

namespace Hackathon.Matchs
{
    [Authorize(HackathonPermissions.Matchs.Default)]
    public class MatchAppService : ApplicationService, IMatchAppService
    {
        #region Properties
        private readonly IRepository<Match, Guid> _matchRepository;
        private readonly ILogger<MatchAppService> _logger;
        #endregion

        #region Constructor
        public MatchAppService(
            IRepository<Match, Guid> matchRepository,
            ILogger<MatchAppService> logger
        )
        {
            _matchRepository = matchRepository;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        [Authorize(HackathonPermissions.Matchs.Default)]
        public async Task<MatchDto> GetAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"::MatchAppService:: - GetAsync - ::Started::");

                var match = await _matchRepository.FindAsync(s => s.Id == id);
                var response = ObjectMapper.Map<Match, MatchDto>(match);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::MatchAppService:: - GetAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::MatchAppService:: - GetAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Matchs.Default)]
        public async Task<PagedResultDto<MatchDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, MatchFilter filter)
        {
            try
            {
                _logger.LogInformation($"::MatchAppService:: - GetListByFilterAsync - ::Started::");

                if (input.Sorting.IsNullOrWhiteSpace())
                {
                    input.Sorting = $"TournamentId";
                }

                var matchs = await _matchRepository.GetQueryableAsync();

                var query = (from s in matchs
                             select new MatchDto()
                             {
                                 Id = s.Id,
                                 TournamentId = s.TournamentId,
                                 GroupId = s.GroupId,
                                 TeamAId = s.TeamAId,
                                 TeamBId = s.TeamBId,
                                 MatchDate = s.MatchDate,
                                 TeamAScore = s.TeamAScore,
                                 TeamBScore = s.TeamBScore,
                                 CreationTime = s.CreationTime
                             })
                            .WhereIf(filter.TournamentId != null, s => s.TournamentId == filter.TournamentId).WhereIf(filter.GroupId != null, s => s.GroupId == filter.GroupId)
                            .WhereIf(filter.TeamAId != null, s => s.TeamAId == filter.TeamAId)
                            .WhereIf(filter.TeamBId != null, s => s.TeamBId == filter.TeamBId)
                            .WhereIf(filter.MatchDate != null, s => s.MatchDate == filter.MatchDate)
                            .WhereIf(filter.TeamAScore != null, s => s.TeamAScore == filter.TeamAScore)
                            .WhereIf(filter.TeamBScore != null, s => s.TeamBScore == filter.TeamBScore);

                var dtos = await AsyncExecuter.ToListAsync(
                     query
                     .OrderBy(input.Sorting)
                     .Skip(input.SkipCount)
                     .Take(input.MaxResultCount)
                 );
                var totalCount = query.Count();

                return new PagedResultDto<MatchDto>(
                    totalCount,
                    dtos
                );
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::MatchAppService:: - GetListByFilterAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::MatchAppService:: - GetListByFilterAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Matchs.Create)]
        public async Task<bool> CreateAsync(CreateUpdateMatchDto input)
        {
            try
            {
                _logger.LogInformation($"::MatchAppService:: - CreateAsync - ::Started::");

                var match = ObjectMapper.Map<CreateUpdateMatchDto, Match>(input);
                await _matchRepository.InsertAsync(match);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::MatchAppService:: - CreateAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::MatchAppService:: - CreateAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Matchs.Edit)]
        public async Task<bool> UpdateAsync(Guid id, CreateUpdateMatchDto input)
        {
            try
            {
                _logger.LogInformation($"::MatchAppService:: - UpdateAsync - ::Started::");

                var match = await _matchRepository.FindAsync(x => x.Id == id);
                if (match == null)
                {
                    var msg = "Match Not Found.";
                    _logger.LogInformation($"::MatchAppService:: - UpdateAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                ObjectMapper.Map(input, match);
                await _matchRepository.UpdateAsync(match);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::MatchAppService:: - UpdateAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::MatchAppService:: - UpdateAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Matchs.Delete)]
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"::MatchAppService:: - DeleteAsync - ::Started::");

                var match = await _matchRepository.FirstOrDefaultAsync(s => s.Id == id);
                if (match == null)
                {
                    var msg = "Match Not Found.";
                    _logger.LogInformation($"::MatchAppService:: - DeleteAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                await _matchRepository.DeleteAsync(id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::MatchAppService:: - DeleteAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::MatchAppService:: - DeleteAsync - ::Ended::");
            }
        }
        #endregion
    }
}
