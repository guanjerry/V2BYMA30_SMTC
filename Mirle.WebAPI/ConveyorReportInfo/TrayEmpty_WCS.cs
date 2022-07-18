using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorReportInfo
{
    public class TrayEmpty_WCS
    {
        public string jobID { get; set; }
        public string transactionID { get; set; }
        public string TrayID { get; set; }
        public string CmdSno { get; set; }
        public string returnCode { get; set; }
        public string returnComment { get; set; }
    }
}
