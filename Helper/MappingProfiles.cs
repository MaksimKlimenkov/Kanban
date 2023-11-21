using AutoMapper;
using Kanban.Dto;
using Kanban.Dto.Auth;
using Kanban.Models;

namespace Kanban.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<RegisterDto, User>();
    }
}