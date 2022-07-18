using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorReportInfo
{
    public class StageEmptyReport
    {
        public string jobID { get; set; }
        public string transactionID { get; set; } = "STAGE_EMPTY";
        public string stagePosition { get; set; }
    }
}
