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

namespace Hackathon.Stadiums
{
    [Authorize(HackathonPermissions.Stadiums.Default)]
    public class StadiumAppService : ApplicationService, IStadiumAppService
    {
        #region Properties
        private readonly IRepository<Stadium, Guid> _stadiumRepository;
        private readonly ILogger<StadiumAppService> _logger;
        #endregion

        #region Constructor
        public StadiumAppService(
            IRepository<Stadium, Guid> stadiumRepository,
            ILogger<StadiumAppService> logger
        )
        {
            _stadiumRepository = stadiumRepository;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        [Authorize(HackathonPermissions.Stadiums.Default)]
        public async Task<StadiumDto> GetAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"::StadiumAppService:: - GetAsync - ::Started::");

                var stadium = await _stadiumRepository.FindAsync(s => s.Id == id);
                var response = ObjectMapper.Map<Stadium, StadiumDto>(stadium);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::StadiumAppService:: - GetAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::StadiumAppService:: - GetAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Stadiums.Default)]
        public async Task<PagedResultDto<StadiumDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, StadiumFilter filter)
        {
            try
            {
                _logger.LogInformation($"::StadiumAppService:: - GetListByFilterAsync - ::Started::");

                if (input.Sorting.IsNullOrWhiteSpace())
                {
                    input.Sorting = $"DisplayName";
                }

                var stadiums = await _stadiumRepository.GetQueryableAsync();

                var query = (from s in stadiums
                             select new StadiumDto()
                             {
                                 Id = s.Id,
                                 DisplayName = s.DisplayName,
                                 Location = s.Location,
                                 TimeZoneId = s.TimeZoneId,
                                 TimeZoneDstOffset = s.TimeZoneDstOffset,
                                 TimeZoneRawOffset = s.TimeZoneRawOffset,
                                 CreationTime = s.CreationTime
                             })
                            .WhereIf(!string.IsNullOrEmpty(filter.DisplayName), s => s.DisplayName.Contains(filter.DisplayName, StringComparison.OrdinalIgnoreCase)).WhereIf(!string.IsNullOrEmpty(filter.Location), s => s.Location.Contains(filter.Location, StringComparison.OrdinalIgnoreCase))
                            .WhereIf(!string.IsNullOrEmpty(filter.TimeZoneId), s => s.TimeZoneId.Contains(filter.TimeZoneId, StringComparison.OrdinalIgnoreCase))
                            .WhereIf(filter.TimeZoneDstOffset != null, s => s.TimeZoneDstOffset == filter.TimeZoneDstOffset)
                            .WhereIf(filter.TimeZoneRawOffset != null, s => s.TimeZoneRawOffset == filter.TimeZoneRawOffset);

                var dtos = await AsyncExecuter.ToListAsync(
                     query
                     .OrderBy(input.Sorting)
                     .Skip(input.SkipCount)
                     .Take(input.MaxResultCount)
                 );
                var totalCount = query.Count();

                return new PagedResultDto<StadiumDto>(
                    totalCount,
                    dtos
                );
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::StadiumAppService:: - GetListByFilterAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::StadiumAppService:: - GetListByFilterAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Stadiums.Create)]
        public async Task<bool> CreateAsync(CreateUpdateStadiumDto input)
        {
            try
            {
                _logger.LogInformation($"::StadiumAppService:: - CreateAsync - ::Started::");

                var stadium = ObjectMapper.Map<CreateUpdateStadiumDto, Stadium>(input);
                await _stadiumRepository.InsertAsync(stadium);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::StadiumAppService:: - CreateAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::StadiumAppService:: - CreateAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Stadiums.Edit)]
        public async Task<bool> UpdateAsync(Guid id, CreateUpdateStadiumDto input)
        {
            try
            {
                _logger.LogInformation($"::StadiumAppService:: - UpdateAsync - ::Started::");

                var stadium = await _stadiumRepository.FindAsync(x => x.Id == id);
                if (stadium == null)
                {
                    var msg = "Stadium Not Found.";
                    _logger.LogInformation($"::StadiumAppService:: - UpdateAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                ObjectMapper.Map(input, stadium);
                await _stadiumRepository.UpdateAsync(stadium);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::StadiumAppService:: - UpdateAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::StadiumAppService:: - UpdateAsync - ::Ended::");
            }
        }

        [Authorize(HackathonPermissions.Stadiums.Delete)]
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"::StadiumAppService:: - DeleteAsync - ::Started::");

                var stadium = await _stadiumRepository.FirstOrDefaultAsync(s => s.Id == id);
                if (stadium == null)
                {
                    var msg = "Stadium Not Found.";
                    _logger.LogInformation($"::StadiumAppService:: - DeleteAsync - ::{msg}::");
                    throw new AbpValidationException(msg,
                        new List<ValidationResult>
                        {
                            new ValidationResult(msg, new []{ "id" })
                        }
                    );
                }

                await _stadiumRepository.DeleteAsync(id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"::StadiumAppService:: - DeleteAsync - ::Exception:: - {ex.Message}");
                throw new UserFriendlyException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"::StadiumAppService:: - DeleteAsync - ::Ended::");
            }
        }
        #endregion
    }
}
