using AutoMapper;
using Video.Application.Contract.Videos.Dtos;
using Video.Domain.Videos.Views;

namespace Video.Application.AutuMapperProfile;

public class BeanVermiceIIiAutoMapperProfile : Profile
{
    protected BeanVermiceIIiAutoMapperProfile()
    {
        CreateMap<ConcernUserListView, GetConcernListDto>();
    }
}
