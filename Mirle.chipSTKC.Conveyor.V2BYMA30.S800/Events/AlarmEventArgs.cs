using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.S800.Events
{
    public class AlarmEventArgs : EventArgs
    {
        public bool Signal { get; }
        public AlarmEventArgs(bool signal)
        {
            Signal = signal;
        }
    }
}
