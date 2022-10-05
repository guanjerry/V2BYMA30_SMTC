using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class ReceiveNewBinInfo : BufferInfo
    {
        public string carrierType { get; set; }
    }
}
