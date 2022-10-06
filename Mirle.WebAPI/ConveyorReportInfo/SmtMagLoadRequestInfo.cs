using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorReportInfo
{
    public class SmtMagLoadRequestInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "SMTC_MAGAZINE_LOAD_REQUEST";
        public string location { get; set; }
    }
}
