using Newtonsoft.Json;
using OfficeOpenXml.Attributes;
using SqlSugar;
namespace In66.Chat.Api.Models.Entities.Business.Chat
{
    [SugarTable("tb_chat_friends")]
    public class Friends : BusinessBase
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(ColumnName = "uesr_guid")]
        public long UserGuId { get; set; }

        /// <summary>
        /// 好友id
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(ColumnName = "friends_guid")]
        public long FriendsGuId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnDescription = "备注", IsNullable = true, ColumnName = "friends_note")]
        public string FriendsNote { get; set; }

    }


}
