using AutoMapper;
using Kanban.Dto;
using Kanban.Models;

namespace Kanban.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateTeamDto, Team>();
        CreateMap<Team, TeamDto>();
    }
}