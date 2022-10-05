using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class CarrierTransferInfo : BaseInfo
    {
        public string carrierId { get; set; }
        public string stagePosition { get; set; }
    }
}
