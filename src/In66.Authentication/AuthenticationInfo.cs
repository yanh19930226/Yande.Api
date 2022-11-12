namespace In66.Authentication
{
    public sealed class UserContext
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// ExationId
        /// </summary>
        public long ExationId { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleIds { get; set; } = string.Empty;
        /// <summary>
        /// 设备
        /// </summary>
        public string Device { get; set; } = string.Empty;
        /// <summary>
        /// 远程地址
        /// </summary>
        public string RemoteIpAddress { get; set; } = string.Empty;
    }
}
