using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class StatusChangeReport
    {
        public string transactionID { get; set; } = "STATUS_CHANGE_REPORT";
        public string chipSTKCID { get; set; }
        public string Status { get; set; }
    }
}
