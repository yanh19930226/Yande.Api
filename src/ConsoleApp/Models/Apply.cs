
using Newtonsoft.Json;
using SqlSugar;

namespace In66.Chat.Api.Models.Entities.Business.Chat
{
    [SugarTable("tb_chat_apply")]
    public class Apply : BusinessBase
    {
        /// <summary>
        /// 发起人id
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(ColumnDescription = "发起人id", ColumnName = "sender_guid")]
        public long SenderGuId { get; set; }

        /// <summary>
        /// 接收人id
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(ColumnDescription = "接收人id", ColumnName = "receiver_guid")]
        public long ReceiverGuId { get; set; }

        /// <summary>
        /// 附言
        /// </summary>
        [SugarColumn(ColumnDescription = "附言", IsNullable = true, ColumnName = "postscript")]
        public string Postscript { get; set; }

        /// <summary>
        /// 回复
        /// </summary>
        [SugarColumn(ColumnDescription = "回复", IsNullable = true, ColumnName = "reply")]
        public string Reply { get; set; }

        /// <summary>
        /// 是否同意
        /// </summary>
        [SugarColumn(ColumnDescription = "是否同意", IsNullable = true, ColumnName = "isAgree")]
        public bool? IsAgree { get; set; }

        /// <summary>
        /// 是否群申请
        /// </summary>
        [SugarColumn(ColumnDescription = "是否群申请", IsNullable = true, ColumnName = "isGroupApply")]
        public bool? IsGroupApply { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        [SugarColumn(ColumnDescription = "是否已读", ColumnName = "isRead")]
        public bool IsRead { get; set; }

    }
}
