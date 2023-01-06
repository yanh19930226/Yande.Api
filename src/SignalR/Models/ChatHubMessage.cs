namespace ConsoleApp.Models
{
    /// <summary>
    /// Message
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// ChatMessageType
        /// </summary>
        public int ChatMessageType { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        public int Source { get; set; }
        /// <summary>
        /// SenderId
        /// </summary>
        public string SenderId { get; set; }
        /// <summary>
        /// ReceiverId
        /// </summary>
        public string ReceiverId { get; set; }
        /// <summary>
        /// Sender
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// Receiver
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        public string ChatLogContent { get; set; }
        /// <summary>
        /// ChatLogSendTime
        /// </summary>
        public string ChatLogSendTime { get; set; }
        /// <summary>
        /// UnReadCount
        /// </summary>
        public int UnReadCount { get; set; }
    }

    /// <summary>
    /// HubUser
    /// </summary>
    public class HubUser
    {
        /// <summary>
        /// 客户端连接Id
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 登入时间
        /// </summary>
        public string LoginTime { get; set; }
    }

    /// <summary>
    /// CustomerUser
    /// </summary>
    public class CustomerUser : HubUser
    {
        /// <summary>
        /// 角色
        /// </summary>
        public List<string> Roles { get; set; } = new List<string>();
        /// <summary>
        /// 服务的产品
        /// </summary>
        public List<int> ServicePrintSource = new List<int>();

        //public string CustomerStatus { get; set; }
    }

    /// <summary>
    /// GuestUser
    /// </summary>
    public class GuestUser : HubUser
    {
        /// <summary>
        /// 来源
        /// </summary>
        public int Source { get; set; }
    }
}
