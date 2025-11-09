using FreeRedis;
using Video.Application.Contract.Code;
using Video.Application.Contract.Code.Dtos;

namespace Video.Application.Code;

public class CodeService : ICodeService
{
    private readonly RedisClient _redisClient;

    public CodeService(RedisClient redisClient)
    {
        _redisClient = redisClient;
    }

    public async Task<string> GetAsync(CodeInput input)
    {
        var value = new Random().Next(9999).ToString("0000");
        await _redisClient.SetExAsync($"{input.Type}:{input.Value}", 60, value);

        return value;
    }
}