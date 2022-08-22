using Mirle.MPLC.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.S800.Signal
{
    public class BcrResultSignal
    {
        public BCRResult ReelID { get; internal set; }
        public BCRResult TrayID { get; internal set; }
        public BcrResultSignal()
        {
            
        }


    }
}
