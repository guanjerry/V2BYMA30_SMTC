using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class PositionReport
    {
        public string transactionID { get; set; } = "POSITION_REPORT";
        public string jobId { get; set; }
        public string CmdSno { get; set; }
        public string IOType { get; set; }
        public string Id { get; set; }
        public string Position { get; set; }
    }
}
