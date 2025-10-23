using AutoMapper;
using FreeRedis;
using Simple.EntityFrameworkCore.Core.Base;
using Video.Application.Contract.Base;
using Video.Application.Contract.UserInfos;
using Video.Application.Contract.UserInfos.Dtos;
using Video.Application.Manage;
using Video.Domain.Shared;
using Video.Domain.Users;

namespace Video.Application.UserInfos
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IMapper _mapper;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly CurrentManage _currentManage;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RedisClient _redisClient;

        public UserInfoService(IMapper mapper, IUserInfoRepository userInfoRepository, CurrentManage currentManage, IUnitOfWork unitOfWork, RedisClient redisClient)
        {
            _mapper = mapper;
            _userInfoRepository = userInfoRepository;
            _currentManage = currentManage;
            _unitOfWork = unitOfWork;
            _redisClient = redisClient;
        }

        /// <inheritdoc/>
        public async Task<UserInfoRoleDto> LoginAsync(LoginInput input)
        {
            if (input.Username == null || input.Password == null)
            {
                throw new ArgumentNullException("Username and Password cannot be null");
            }

            var data = await _userInfoRepository.GetUserInfoRoleAsync(input.Username, input.Password);

            var dto = _mapper.Map<UserInfoRoleDto>(data);

            return dto;
        }

        /// <inheritdoc/>
        public async Task<UserInfoRoleDto> GetAsync()
        {
            var data = await _userInfoRepository.GetAsync(_currentManage.GetId());

            var dto = _mapper.Map<UserInfoRoleDto>(data);

            return dto;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(UpdateUserInfoInput input)
        {
            var userInfo = await _userInfoRepository.FirstOfDefaultAsync(x => x.Id == _currentManage.GetId());

            if (userInfo == null)
            {
                throw new BusinessException("用户信息不存在");
            }

            userInfo.Avatar = input.Avatar;
            userInfo.Name = input.Name;
            userInfo.Password = input.Password;

            await _userInfoRepository.UpdateAsync(userInfo);

            await _unitOfWork.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<UserInfoRoleDto> RegisterAsync(RegisterInput input)
        {
            var code = await _redisClient.GetAsync<string>($"{CodeType.Register}:{input.Username}");
            var s = $"{CodeType.Register}:{input.Username}";
            if (code != input.Code)
            {
                throw new BusinessException("验证码错误");
            }

            if (await _userInfoRepository.IsExistAsync(x => x.Username == input.Username))
            {
                throw new BusinessException("用户名已存在");
            }

            var data = _mapper.Map<UserInfo>(input);

            data = await _userInfoRepository.InsertAsync(data);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserInfoRoleDto>(data);
        }

        /// <inheritdoc/>
        public async Task<PageResultDto<UserInfoDto>> GetListAsync(GetListInput input)
        {
            var data = await _userInfoRepository.GetListAsync(input.Keywords, input.StartTime, input.EndTime, input.SkipCount,
                input.MaxResultCount);

            var count = await _userInfoRepository.GetCountAsync(input.Keywords, input.StartTime, input.EndTime);

            var dto = _mapper.Map<List<UserInfoDto>>(data);

            return new PageResultDto<UserInfoDto>(count, dto);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(IEnumerable<Guid> ids)
        {
            await _userInfoRepository.DeleteAsync(ids);
        }

        /// <inheritdoc/>
        public async Task EnableAsync(IEnumerable<Guid> ids, bool enable = true)
        {
            await _userInfoRepository.EnableAsync(ids, enable);
        }

    }
}
