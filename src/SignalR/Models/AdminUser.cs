using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    [SugarTable("SYS_USER", TableDescription = "用户表")]
    public class AdminUser
    {
        [SugarColumn(ColumnDescription = "id", IsPrimaryKey = true, ColumnName = "id")]
        public int Id { get; set; }
        [SugarColumn(ColumnName = "SYS_USER_ID")]
        public string SysUserId { get; set; }
        [SugarColumn(ColumnDescription = "USER_NAME", ColumnName = "USER_NAME")]
        public string UserName { get; set; }
        [SugarColumn(ColumnDescription = "ROLE_ID", ColumnName = "ROLE_ID")]
        public string RoleId { get; set; }
        [SugarColumn(ColumnDescription = "SYS_TYPE", ColumnName = "SYS_TYPE")]
        public string SysType { get; set; }
        [SugarColumn(ColumnDescription = "CORP_ID", ColumnName = "CORP_ID")]
        public string CorpId { get; set; }
        [SugarColumn(ColumnDescription = "DEPARTMENT_ID", ColumnName = "DEPARTMENT_ID")]
        public string DepartmentId { get; set; }
        [SugarColumn(ColumnDescription = "USER_POWER_LIST", ColumnName = "USER_POWER_LIST")]
        public string UserPowerList { get; set; }
        [SugarColumn(ColumnDescription = "PASSWORD", ColumnName = "PASSWORD")]
        public string Password { get; set; }
        [SugarColumn(ColumnDescription = "PIC_URL", ColumnName = "PIC_URL")]
        public string PicUrl { get; set; }
        [SugarColumn(ColumnDescription = "REAL_NAME", ColumnName = "REAL_NAME")]
        public string RealName { get; set; }
        [SugarColumn(ColumnDescription = "TELEPHONE", ColumnName = "TELEPHONE")]
        public string Telephone { get; set; }
        [SugarColumn(ColumnDescription = "EMAIL", ColumnName = "EMAIL")]
        public string Email { get; set; }
        [SugarColumn(ColumnDescription = "SEX", ColumnName = "SEX")]
        public string Sex { get; set; }
    }
}
