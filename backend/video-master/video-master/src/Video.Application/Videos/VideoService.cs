using AutoMapper;
using Simple.EntityFrameworkCore.Core.Base;
using Video.Application.Contract.Base;
using Video.Application.Contract.Videos;
using Video.Application.Contract.Videos.Dtos;
using Video.Application.Manage;
using Video.Domain.Videos;
using Video.Domain.Videos.Views;

namespace Video.Application.Videos;

public class VideoService : IVideoService
{
    private readonly IVideoRepository _videoRepository;
    private readonly IMapper _mapper;
    private readonly CurrentManage _currentManage;
    private readonly IUnitOfWork _unitOfWork;

    public VideoService(IVideoRepository videoRepository, IMapper mapper, CurrentManage currentManage, IUnitOfWork unitOfWork)
    {
        _videoRepository = videoRepository;
        _mapper = mapper;
        _currentManage = currentManage;
        _unitOfWork = unitOfWork;
    }

    public async Task AdminDeleteAsync(Guid id)
    {
        var video = await _videoRepository.FirstOfDefaultAsync(x => x.Id == id);

        if (video != null)
        {
            await _videoRepository.DeleteAsync(video);

            await _unitOfWork.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task CreateAsync(CreateVideoInput input)
    {
        var userId = _currentManage.GetId();

        var data = _mapper.Map<Domain.Videos.Video>(input);
        data.UserId = userId;
        await _videoRepository.InsertAsync(data);

        await _unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id)
    {
        var video = await _videoRepository.FirstOfDefaultAsync(x => x.UserId == _currentManage.GetId() && x.Id == id);

        if (video != null)
        {
            await _videoRepository.DeleteAsync(video);

            await _unitOfWork.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task<PageResultDto<GetVideoListDto>> GetListAsync(GetVideoListInput input)
    {
        var data = await _videoRepository.GetListAsync(input.UserId, input.ClassifyId, input.Keywords, input.StartTime, input.EndTime, input.SkipCount, input.MaxResultCount);

        var count = await _videoRepository.GetCountAsync(input.UserId, input.ClassifyId, input.Keywords, input.StartTime, input.EndTime);

        var dto = _mapper.Map<List<GetVideoListDto>>(data);

        return new PageResultDto<GetVideoListDto>(count, dto);
    }

    /// <inheritdoc/>
    public async Task CreateClassifyAsync(string name)
    {
        await _videoRepository.CreateClassifyAsync(name);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteClassifyAsync(Guid id)
    {
        await _videoRepository.DeleteClassifyAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<List<ClassifyDto>> GetListClassifyDtoAsync(string? name)
    {
        var data = await _videoRepository.GetClassifyListAsync(name);

        var dto = _mapper.Map<List<ClassifyDto>>(data);

        return dto;
    }

    /// <inheritdoc/>
    public async Task UpdateClassifyAsync(ClassifyDto dto)
    {
        await _videoRepository.UpdateClassifyAsync(_mapper.Map<Classify>(dto));
        await _unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<bool> WhetheritCanBeDeleted(string path)
    {
        var data = await _videoRepository.FirstOfDefaultAsync(x => x.VideoUrl == path);

        // 如果当前用户是作者允许删除
        if (data.UserId == _currentManage.GetId())
        {
            return true;
        }
        else 
            return false;
    }

    /// <inheritdoc/>
    public async Task<int> StatisticalConcernAsync(StatisticalConcernInput input)
    {
        return await _videoRepository.StatisticalConcernAsync(_currentManage.GetId(),input.StartTime,input.EndTime);
    }

    /// <inheritdoc/>
    public async Task<int> ReleaseCountasync(VideoInput input)
    {
        return await _videoRepository.ReleaseCountasync(_currentManage.GetId(), input.StartTime, input.EndTime);
    }
}
