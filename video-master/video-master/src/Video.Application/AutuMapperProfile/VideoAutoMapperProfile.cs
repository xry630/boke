using AutoMapper;
using Video.Application.Contract.Videos.Dtos;
using Video.Domain.Videos;

namespace Video.Application.AutuMapperProfile;

public class VideoAutoMapperProfile : Profile
{
    public VideoAutoMapperProfile()
    {
        CreateMap<CreateVideoInput, Domain.Videos.Video>();
        CreateMap<Domain.Videos.Video, GetVideoListDto>();

        CreateMap<ClassifyDto, Classify>().ReverseMap();
    }
}
