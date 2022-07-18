using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class CommandReport
    {
        public string transactionID { get; set; } = "CommandReport";
        public string CmdSno { get; set; }
        public string reelID { get; set; }
        public string CmdMode { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string CmdSts { get; set; }
        public string Remark { get; set; } = "";
        public string EmptyRetreival { get; set; } = clsEnum.WmsApi.IsEmpty.N.ToString();
    }
}
