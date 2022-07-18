using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class StockInRequest_WCS
    {
        public string transactionID { get; set; }
        public string reelID { get; set; }
        public string stockerID { get; set; }
        public string CmdSno { get; set; }
        public string locationID { get; set; }
        public string returnCode { get; set; }
        public string returnComment { get; set; }
    }
}
