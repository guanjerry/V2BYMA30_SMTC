using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class BlockStatusReturnCode : ReturnCode
    {
        public string chipSTKCId { get; set; }
        public List<BlockListReply> blockList { get; set; }
    }
}
