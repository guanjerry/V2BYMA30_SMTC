using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorReportInfo
{
    public class RackCommandDoneInfo
    {
        public string jobID { get; set; }
        public string transactionID { get; set; } = "RACK_COMMAND_DONE";
        public string stagePosition { get; set; }
        public string RackID { get; set; }
    }
}
