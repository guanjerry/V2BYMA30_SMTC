using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorReportInfo
{
    public class EmptyBinLoadRequestInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "EMPTY_BIN_LOAD_REQUEST";
        public string location { get; set; }
        public int reqQty { get; set; }
    }
}
