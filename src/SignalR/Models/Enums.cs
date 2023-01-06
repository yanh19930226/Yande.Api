namespace SignalR.Models
{
    /// <summary>
    /// ChatMessageTypeEnum
    /// </summary>
    public enum ChatMessageTypeEnum
    {
        /// <summary>
        ///普通聊天
        /// </summary>
        Chat = 1,
        /// <summary>
        /// 获取客服
        /// </summary>
        CustomerService = 2,
        /// <summary>
        /// 获取单号
        /// </summary>
        GetOrder = 3,
        /// <summary>
        /// 结束会话
        /// </summary>
        CustomerServiceFinish = 4,
        /// <summary>
        /// CustomerFinish
        /// </summary>
        Test = 5,
        /// <summary>
        /// 评价
        /// </summary>
        Comment = 6,
        /// <summary>
        /// 机器人提问
        /// </summary>
        Question = 7
    }
}
