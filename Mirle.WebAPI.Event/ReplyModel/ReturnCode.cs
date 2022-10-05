using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class ReturnCode : BaseInfo
    {
        public string returnCode { get; set; }
        public string returnComment { get; set; }
    }
}
