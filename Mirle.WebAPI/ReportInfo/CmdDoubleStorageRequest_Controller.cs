using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class CmdDoubleStorageRequest_Controller
    { 
        public string transactionID { get; set; } = "CommandDoubleStorageReport";
        public string CmdSno { get; set; }
        public string reelID { get; set; }
        public string CmdMode { get; set; }
        public string Loc { get; set; }
        public string Remark { get; set; }
        public string DoubleStorage { get; set; } = clsEnum.WmsApi.IsDouble.Y.ToString();
    }
}
