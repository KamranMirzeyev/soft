using AutoMapper;
using Core.Model;
using SoftAPI.DTO.UserDto;

namespace SoftAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<EditUserDto, User>();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
