namespace ConsoleApp.Models
{
    /// <summary>
    /// 在线用户
    /// </summary>
    public class OnlineUsersForChat
    {
        /// <summary>
        /// 客户端连接Id
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public int Source { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登入时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// OnlineUsersForChat
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="name"></param>
        /// <param name="userguid"></param>
        public OnlineUsersForChat(string clientid, string name, int? userid, int source)
        {
            Source = source;
            ConnectionId = clientid;
            UserId = userid;
            Name = name;
            LoginTime = DateTime.Now;
        }
    }
}
