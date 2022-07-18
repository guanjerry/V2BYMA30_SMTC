using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class BlockStsChangeReport
    {
        public string transactionID { get; set; } = "BLOCK_STATUS_CHANGE";
        public string chipSTKCID { get; set; }
        public List<BlockList> blockList { get; set; }
    }
}
