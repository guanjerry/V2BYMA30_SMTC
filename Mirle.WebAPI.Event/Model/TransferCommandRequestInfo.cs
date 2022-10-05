using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class TransferCommandRequestInfo : BaseInfo
    {
        public string binId { get; set; }
        public string carrierType { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
    }
}
