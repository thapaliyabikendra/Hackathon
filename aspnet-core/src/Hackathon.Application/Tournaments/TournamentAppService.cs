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

namespace Hackathon.Tournaments
{
    [Authorize(HackathonPermissions.Tournaments.Default)]
    public class TournamentAppService : ApplicationService, ITournamentAppService
    {
        #region Properties
        private readonly IRepository<Tournament, Guid> _tournamentRepository;
        private readonly ILogger<TournamentAppService> _logger;
        #endregion

        #region Constructor
        public TournamentAppService(
            IRepository<Tournament, Guid> tournamentRepository,
            ILogger<TournamentAppService> logger
        )
        {
            _tournamentRepository = tournamentRepository;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        [Authorize(HackathonPermissions.Tournaments.Default)]
        public async Task<TournamentDto> GetAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"::TournamentAppService:: - GetAsync - ::Started::");

                var tournament = await _tournamentRepository.FindAsync(s => s.Id == id);
                var response = ObjectMapper.Map<Tournament, TournamentDto>(tournament);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TournamentAppService:: - GetAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::TournamentAppService:: - GetAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Tournaments.Default)]
        public async Task<PagedResultDto<TournamentDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, TournamentFilter filter)
        {
            try
            {
                _logger.LogInformation($"::TournamentAppService:: - GetListByFilterAsync - ::Started::");

                if (input.Sorting.IsNullOrWhiteSpace())
                {
                    input.Sorting = $"DisplayName";
                }

                var tournaments = await _tournamentRepository.GetQueryableAsync();

                var query = (from s in tournaments
                             select new TournamentDto()
                             {
                                 Id = s.Id,
                                 DisplayName = s.DisplayName,
                                 StartDate = s.StartDate,
                                 EndDate = s.EndDate,
                                 CreationTime = s.CreationTime
                             })
                            .WhereIf(!string.IsNullOrEmpty(filter.DisplayName), s => s.DisplayName.Contains(filter.DisplayName, StringComparison.OrdinalIgnoreCase)).WhereIf(filter.StartDate != null, s => s.StartDate == filter.StartDate)
                            .WhereIf(filter.EndDate != null, s => s.EndDate == filter.EndDate);

                var dtos = await AsyncExecuter.ToListAsync(
                     query
                     .OrderBy(input.Sorting)
                     .Skip(input.SkipCount)
                     .Take(input.MaxResultCount)
                 );
                var totalCount = query.Count();

                return new PagedResultDto<TournamentDto>(
                    totalCount,
                    dtos
                );
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TournamentAppService:: - GetListByFilterAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::TournamentAppService:: - GetListByFilterAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Tournaments.Create)]
        public async Task<bool> CreateAsync(CreateUpdateTournamentDto input)
        {
            try
            {
                _logger.LogInformation($"::TournamentAppService:: - CreateAsync - ::Started::");

                var tournament = ObjectMapper.Map<CreateUpdateTournamentDto, Tournament>(input);
                await _tournamentRepository.InsertAsync(tournament);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TournamentAppService:: - CreateAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::TournamentAppService:: - CreateAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Tournaments.Edit)]
        public async Task<bool> UpdateAsync(Guid id, CreateUpdateTournamentDto input)
        {
            try
            {
                _logger.LogInformation($"::TournamentAppService:: - UpdateAsync - ::Started::");

                var tournament = await _tournamentRepository.FindAsync(x => x.Id == id);
                if (tournament == null)
                {
                    var msg = "Tournament Not Found.";
                    _logger.LogInformation($"::TournamentAppService:: - UpdateAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                ObjectMapper.Map(input, tournament);
                await _tournamentRepository.UpdateAsync(tournament);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TournamentAppService:: - UpdateAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::TournamentAppService:: - UpdateAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Tournaments.Delete)]
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"::TournamentAppService:: - DeleteAsync - ::Started::");

                var tournament = await _tournamentRepository.FirstOrDefaultAsync(s => s.Id == id);
                if (tournament == null)
                {
                    var msg = "Tournament Not Found.";
                    _logger.LogInformation($"::TournamentAppService:: - DeleteAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                await _tournamentRepository.DeleteAsync(id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TournamentAppService:: - DeleteAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::TournamentAppService:: - DeleteAsync - ::Ended::");
            }
        }
        #endregion
    }
}
