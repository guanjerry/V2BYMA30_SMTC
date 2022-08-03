using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorReportInfo
{
    public class UnknownBinLeaveReport
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "UNKNOWN_BIN_EMPTY_INFO";
        public string position { get; set; }
    }
}
