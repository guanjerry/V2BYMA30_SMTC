using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class BufferRollReturnCode : ReturnCode
    {
        public string bufferId { get; set; }
    }
}
