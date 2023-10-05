using AutoMapper;
using Entitites.Concrete;
using Entitites.Dtos.User;

namespace Buissnes.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDetailDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserAddDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<UserDto, UserUpdateDto>().ReverseMap();

        }
    }
}
