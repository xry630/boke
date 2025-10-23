using AutoMapper;
using Video.Application.Contract.Base;
using Video.Application.Contract.Videos;
using Video.Application.Contract.Videos.Dtos;
using Video.Application.Manage;
using Video.Domain.Videos;
using Video.Domain.Videos.Views;

namespace Video.Application.Videos;

public class BeanVermicelliService : IBeanVermicelliService
{
    private readonly IBeanVermicelliRepository _beanVermicelliRepository;
    private readonly CurrentManage _currentManage;
    private readonly IMapper _mapper;


    public BeanVermicelliService(IBeanVermicelliRepository beanVermicelliRepository, CurrentManage currentManage, IMapper mapper)
    {
        _beanVermicelliRepository = beanVermicelliRepository;
        _currentManage = currentManage;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task ConcernAsync(Guid userId)
    {
        var id = _currentManage.GetId();
        var data = await _beanVermicelliRepository.FirstOfDefaultAsync(x => x.UserId == id && x.BeFocusedId == userId);

        // 如果存在则取消关注 不存在则添加关注
        if (data != null)
        {
            await _beanVermicelliRepository.DeleteAsync(data.Id);
        }
        else
        {
            await _beanVermicelliRepository.InsertAsync(new BeanVermicelli()
            {
                BeFocusedId = userId,
                UserId = id
            });
        }
    }

    /// <inheritdoc/>
    public async Task<PageResultDto<GetConcernListDto>> GetListAsync(GetListInput input)
    {
        var data = await _beanVermicelliRepository.GetConcernUserLists(input.UserId ?? _currentManage.GetId(), input.Keywords, input.Concern, input.SkipCount, input.MaxResultCount);

        var count = await _beanVermicelliRepository.GetConcernUserCount(input.UserId ?? _currentManage.GetId(), input.Keywords, input.Concern);

        var dto = _mapper.Map<List<ConcernUserListView>, List<GetConcernListDto>>(data);

        return new PageResultDto<GetConcernListDto>(count, dto);
    }
}
