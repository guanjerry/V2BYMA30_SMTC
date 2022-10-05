using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirle.WebAPI.Event.Model;

namespace Mirle.WebAPI.Event.U2NMMA3.Models
{
    public class PositionReportInfo : BaseInfo
    {
        public string carrierType { get; set; }
        public string id { get; set; }
        public string position { get; set; }
    }
}
