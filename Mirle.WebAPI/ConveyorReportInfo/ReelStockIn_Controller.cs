using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorReportInfo
{
    public class ReelStockIn_Controller
    {
        public string jobID { get; set; }
        public string transactionID { get; set; } = "ReelStockIn";
        public string reelID { get; set; }
    }
}
