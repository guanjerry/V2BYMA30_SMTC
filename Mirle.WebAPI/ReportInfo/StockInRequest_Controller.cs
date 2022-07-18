using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class StockInRequest_Controller
    {
        public string transactionID { get; set; } = "StockInRequest";
        public string reelID { get; set; }
        public string stockerID { get; set; }
    }
}
