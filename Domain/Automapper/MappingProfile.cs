using AutoMapper;
using Domain.DTOs.Account;
using Domain.DTOs.Role;
using Domain.Entities;

namespace Domain.Automapper
{
    public class MappingProfile : Profile   
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountAddDTO>().ReverseMap();
            CreateMap<Account, AccountUpdateDTO>().ReverseMap();

            CreateMap<Role, RoleAddDTO>().ReverseMap();
            CreateMap<Role, RoleUpdateDTO>().ReverseMap();

        }
    }
}
