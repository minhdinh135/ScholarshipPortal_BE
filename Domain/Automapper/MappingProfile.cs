using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Automapper
{
    public class MappingProfile : Profile   
    {
        public MappingProfile()
        {
            CreateMap<User, UserAddDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();

            CreateMap<Role, RoleAddDTO>().ReverseMap();
            CreateMap<Role, RoleUpdateDTO>().ReverseMap();

        }
    }
}
