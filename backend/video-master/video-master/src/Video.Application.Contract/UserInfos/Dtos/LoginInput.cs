namespace Video.Application.Contract.UserInfos.Dtos
{
    public class LoginInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }
    }
}
