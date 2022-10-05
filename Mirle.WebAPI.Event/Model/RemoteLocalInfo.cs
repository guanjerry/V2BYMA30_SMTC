using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class RemoteLocalInfo : BaseInfo
    {
        public string chipSTKCId { get; set; }
        public string controlQuery { get; set; }
    }
}
