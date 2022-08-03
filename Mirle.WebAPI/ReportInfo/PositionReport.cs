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
        public string transactionId { get; set; } = "POSITION_REPORT";
        public string jobId { get; set; }
        public string ioType { get; set; }
        public string id { get; set; }
        public string position { get; set; }
    }
}
