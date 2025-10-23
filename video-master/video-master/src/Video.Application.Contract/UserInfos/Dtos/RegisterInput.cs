namespace Video.Application.Contract.UserInfos.Dtos
{
    public class RegisterInput
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? Avatar { get; set; }
    }
}