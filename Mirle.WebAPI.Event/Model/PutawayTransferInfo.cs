using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class PutawayTransferInfo : BaseInfo
    {
        public string reelId { get; set; }
        public string toShelfId { get; set; }
        public string lotSize { get; set; }
    }
}
