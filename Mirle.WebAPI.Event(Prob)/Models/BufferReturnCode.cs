using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Models
{
    public class BufferReturnCode : ReturnCode
    {
        public string bufferId { get; set; }
    }
}
