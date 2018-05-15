using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Dto
{
    /// <summary>
    /// 表格数据返回格式 -layui.table
    /// </summary>
    public class PublicTableOutput<TEntity>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public TEntity data { get; set; }
    }
}