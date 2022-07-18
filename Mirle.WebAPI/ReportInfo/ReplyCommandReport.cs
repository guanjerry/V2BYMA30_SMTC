using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class ReplyCommandReport
    {
        public string transactionID { get; set; } 
        public string CmdSno { get; set; }
        public string reelID { get; set; }
        public string CmdMode { get; set; }
        public string returnCode { get; set; }
        public string returnComment { get; set; }
    }
}
