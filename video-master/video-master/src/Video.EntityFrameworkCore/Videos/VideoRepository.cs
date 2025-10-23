using Microsoft.EntityFrameworkCore;
using Simple.EntityFrameworkCore.Core;
using Simple.EntityFrameworkCore.Extensions;
using Video.Domain.Videos;

namespace Video.Domain.Videos;

public class VideoRepository : Repository<VideoDbContext, Domain.Videos.Video>, IVideoRepository
{
    public VideoRepository(VideoDbContext dbContext) : base(dbContext)
    {
    }

    ///<inheritdoc/>
    public async Task<int> GetCountAsync(Guid? userId, Guid? classifyId, string? keywords, DateTime? startTime, DateTime? endTime)
    {
        var query = CreateQuery(userId, classifyId, keywords, startTime, endTime);

        return await query.CountAsync();
    }

    ///<inheritdoc/>
    public async Task<List<Video>> GetListAsync(Guid? userId, Guid? classifyId, string? keywords, DateTime? startTime, DateTime? endTime, int skipCount, int maxResultCount)
    {
        var query = CreateQuery(userId, classifyId, keywords, startTime, endTime);

        return await query.PageBy(skipCount, maxResultCount).ToListAsync();
    }

    ///<inheritdoc/>
    public async Task CreateClassifyAsync(string name)
    {
        await _dbContext.Classify.AddAsync(new Classify()
        {
            Name = name
        });
    }

    private IQueryable<Video> CreateQuery(Guid? userId, Guid? classifyId, string? keywords, DateTime? startTime, DateTime? endTime)
    {
        var query =
            _dbContext.Video
            .WhereIf(userId.HasValue, x => x.UserId == userId)
            .WhereIf(classifyId.HasValue, x => x.ClassifyId == classifyId)
            .WhereIf(!string.IsNullOrEmpty(keywords), x => EF.Functions.Like(x.Title, $"%{keywords}%"))
            .WhereIf(startTime.HasValue, x => x.CreatedTime >= startTime)
            .WhereIf(endTime.HasValue, x => x.CreatedTime <= endTime);

        return query;
    }

    ///<inheritdoc/>
    public async Task DeleteClassifyAsync(Guid id)
    {
        var data = await _dbContext.Classify.FirstOrDefaultAsync(x => x.Id == id);
        if (data != null)
        {
            _dbContext.Classify.Remove(data);
        }
    }

    ///<inheritdoc/>
    public async Task<List<Classify>> GetClassifyListAsync(string? name)
    {
        return await _dbContext.Classify.WhereIf(!string.IsNullOrEmpty(name), x => EF.Functions.Like(x.Name, $"%{name}%")).ToListAsync();
    }

    ///<inheritdoc/>
    public async Task UpdateClassifyAsync(Classify classify)
    {
        _dbContext.Classify.Update(classify);
        await Task.CompletedTask;
    }

    ///<inheritdoc/>
    public async Task<int> StatisticalConcernAsync(Guid userId, DateTime? startTime, DateTime? endTime)
    {
        return await _dbContext.BeanVermicelli
            .WhereIf(startTime.HasValue, x => x.CreatedTime >= startTime)
            .WhereIf(endTime.HasValue, x => x.CreatedTime <= endTime)
            .Where(x => x.UserId == userId)
            .CountAsync();
    }

    public async Task<int> ReleaseCountasync(Guid userId, DateTime? startTime, DateTime? endTime)
    {
        return await _dbContext.Video
            .WhereIf(startTime.HasValue, x => x.CreatedTime >= startTime)
            .WhereIf(endTime.HasValue, x => x.CreatedTime <= endTime)
            .Where(x => x.UserId == userId)
            .CountAsync();
    }
}
