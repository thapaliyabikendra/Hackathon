using AutoMapper;

using Hackathon.Tournaments;
using Hackathon.Stadiums;
using Hackathon.Groups;
using Hackathon.Matchs;
using Hackathon.Teams;
namespace Hackathon;

public class HackathonApplicationAutoMapperProfile : Profile
{
    public HackathonApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
		CreateMap<CreateUpdateTeamDto, Team>();
		CreateMap<Team, TeamDto>();
		CreateMap<CreateUpdateMatchDto, Match>();
		CreateMap<Match, MatchDto>();
		CreateMap<CreateUpdateGroupDto, Group>();
		CreateMap<Group, GroupDto>();
		CreateMap<CreateUpdateStadiumDto, Stadium>();
		CreateMap<Stadium, StadiumDto>();
		CreateMap<CreateUpdateTournamentDto, Tournament>();
		CreateMap<Tournament, TournamentDto>();
    }
}
