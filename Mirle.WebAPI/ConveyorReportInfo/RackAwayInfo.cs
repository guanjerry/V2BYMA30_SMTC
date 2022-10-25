using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorReportInfo
{
    public class RackAwayInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "RACK_AWAY_INFO";
        public string stagePosition { get; set; }
        public string rackId { get; set; }
    }
}
