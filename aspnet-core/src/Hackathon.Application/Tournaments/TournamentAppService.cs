using Hackathon.Groups;
using Hackathon.Matchs;
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
        private readonly IRepository<Match, Guid> _matchRepository;
        private readonly IRepository<Group, Guid> _groupRepository;
        private readonly ILogger<TournamentAppService> _logger;
        #endregion

        #region Constructor
        public TournamentAppService(
            IRepository<Tournament, Guid> tournamentRepository,
            IRepository<Match, Guid> matchRepository,
            IRepository<Group, Guid> groupRepository,
            ILogger<TournamentAppService> logger
        )
        {
            _tournamentRepository = tournamentRepository;
            _matchRepository = matchRepository;
            _groupRepository = groupRepository;
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

        [Authorize(HackathonPermissions.Tournaments.Default)]
        public async Task<bool> GenerateMatchesAsync(GenerateMatchDto input)
        {
            try
            {
                _logger.LogInformation($"::TournamentAppService:: - GenerateMatchesAsync - ::Started::");

                var tournament = await _tournamentRepository.FirstOrDefaultAsync(s => s.Id == input.TournamentId);
                if (tournament == null)
                {
                    var msg = "Tournament Not Found.";
                    _logger.LogInformation($"::TournamentAppService:: - GenerateMatchesAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                var matches = new List<Match>();

                // Step 1: Group Stage
                var groups = DivideIntoGroups(input.TeamIds);

                var groupsNew = groups.Select(s => new Group {
                    TournamentId =  input.TournamentId,
                    GroupName = $"Group {s.Id}"
                }).ToList();

                await _matchRepository.InsertManyAsync(matches);

                foreach (var group in groups)
                {
                    var groupMatches = GenerateGroupStageMatches(group.Teams, input.StadiumIds, tournament);
                    matches.AddRange(groupMatches);
                }

                await _matchRepository.InsertManyAsync(matches);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::TournamentAppService:: - GenerateMatchesAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
        }


        private List<GroupTeamDto> DivideIntoGroups(List<Guid> teams)
        {
            // Example: Divide into 2 groups
            int numGroups = 2;
            int groupSize = (int)Math.Ceiling((double)teams.Count / numGroups);
            return teams.Select((x, i) => new { Index = i, Value = x })
                        .GroupBy(x => x.Index / groupSize)
                        .Select(x => new GroupTeamDto { Id = x.Key, Teams = x.Select(v => v.Value).ToList() })
                        .ToList();
        }

   
        private List<Match> GenerateGroupStageMatches(List<Guid> group, List<Guid> stadiums, Tournament tournament)
        {
            var matches = new List<Match>();

            for (int i = 0; i < group.Count - 1; i++)
            {
                for (int j = i + 1; j < group.Count; j++)
                {
                    var match = new Match
                    {
                        TeamAId = group[i],
                        TeamBId = group[j],
                        TournamentId = tournament.Id,
                        StadiumId = GetRandomStadium(stadiums),
                        MatchDate = GenerateRandomDate(tournament.StartDate, tournament.EndDate)
                    };
                    matches.Add(match);
                }
            }

            return matches;
        }

        public DateTime GenerateRandomDate(DateTime startDate, DateTime endDate)
        {
            long minTicks = startDate.Ticks;
            long maxTicks = endDate.Ticks;

            // Generate a random number of ticks within the range
            long randomTicks = (long)(new Random().NextDouble() * (maxTicks - minTicks)) + minTicks;

            // Convert the ticks to a DateTime object
            return new DateTime(randomTicks);
        }

        private Guid GetRandomStadium(List<Guid> stadiums)
        {
            return stadiums[new Random().Next(stadiums.Count)];
        }
        #endregion
    }
}
public class GroupTeamDto
{
    public int Id { get; set; }
    public List<Guid> Teams { get; set; }
}