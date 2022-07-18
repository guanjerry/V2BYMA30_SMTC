using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class ForkStatusReport
    {
        public string transactionID { get; set; } = "CONTROL_CHANGE_REPORT";
        public string chipSTKCID { get; set; }
        public List<ForkStatusList> forkStatusList { get; set; }
    }
}
