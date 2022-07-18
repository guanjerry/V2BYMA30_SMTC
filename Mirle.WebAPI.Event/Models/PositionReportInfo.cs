using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.U2NMMA30.Models
{
    public class PositionReportInfo : BaseInfo
    {
        public string CmdSno { get; set; }
        public string IOType { get; set; }
        public string Id { get; set; }
        public string Position { get; set; }
    }
}
