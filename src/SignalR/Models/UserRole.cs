using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Models
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    [SugarTable("USER_ROLE", TableDescription = "用户角色表")]
    public class UserRole
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// RoleId
        /// </summary>
        [SugarColumn(ColumnName = "role_id")]
        public int RoleId { get; set; }
    }
}
