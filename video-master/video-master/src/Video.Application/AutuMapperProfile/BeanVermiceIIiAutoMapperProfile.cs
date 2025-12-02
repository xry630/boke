using AutoMapper;
using Video.Application.Contract.Videos.Dtos;
using Video.Domain.Videos.Views;

namespace Video.Application.AutuMapperProfile;

public class BeanVermiceIIiAutoMapperProfile : Profile
{
    public BeanVermiceIIiAutoMapperProfile()
    {
        CreateMap<ConcernUserListView, GetConcernListDto>();
    }
}
