using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorReportInfo
{
    public class TrayLeave_Controller
    {
        public string jobID { get; set; }
        public string transactionID { get; set; } = "TrayReadyGo";
        public string TrayID { get; set; }
        public string IOType { get; set; }
        public string Position { get; set; }
    }
}
