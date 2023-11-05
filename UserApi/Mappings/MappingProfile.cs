using AutoMapper;
using Models;

namespace UserApi.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}
