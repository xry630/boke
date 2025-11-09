using AutoMapper;
using Video.Application.Contract;
using Video.Application.Contract.UserInfos.Dtos;
using Video.Domain;
using Video.Domain.Users;

namespace Video.Application.AutuMapperProfile
{
    public class UserInfoAutoMapperProfile : Profile
    {
        public UserInfoAutoMapperProfile()
        {
            CreateMap<UserInfo, UserInfoDto>().ReverseMap();

            CreateMap<UserInfoRoleView, UserInfoRoleDto>();
            CreateMap<Role, RoleDto>();

            CreateMap<UserInfo, UserInfoRoleDto>();
            CreateMap<RegisterInput, UserInfo>();
        }
    }
}
