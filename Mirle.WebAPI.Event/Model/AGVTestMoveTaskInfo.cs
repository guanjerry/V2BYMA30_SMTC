using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class AGVTestMoveTaskInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "";
        public string fromLoc { get; set; }
        public string toLoc { get; set; }
        public string carrierType { get; set; }
    }
}
