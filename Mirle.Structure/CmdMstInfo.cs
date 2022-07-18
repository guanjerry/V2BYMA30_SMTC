using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.Structure
{
    public class CmdMstInfo
    {
        public string Cmd_Sno { get; set; }
        public string Cmd_Sts { get; set; }
        public clsEnum.CmdModeType Cmd_Mode { get; set; }
        public string Priority { get; set; }
        public string Cmd_Abnormal { get; set; } = "";
        public string PortNo { get; set; }
        public string StickNo { get; set; }
        public string Loc { get; set; }
        public string Reel_ID { get; set; }
        public string EquNo { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string CurLoc { get; set; }
        public string Largest { get; set; }
        public string CanDo { get; set; } = "N";
        public string Remark { get; set; } = "";
    }
}
