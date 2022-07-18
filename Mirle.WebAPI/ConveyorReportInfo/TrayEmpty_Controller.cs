using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorReportInfo
{
    public class TrayEmpty_Controller
    {
        public string jobID { get; set; }
        public string transactionID { get; set; } = "TrayEmpty";
        public string TrayID { get; set; }
        public string Position { get; set; }
    }
}
