using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.Events
{
    public class StkLabelClickArgs : EventArgs
    {
        public int StockerID { get; }

        public StkLabelClickArgs(int ID)
        {
            StockerID = ID;
        }
    }
}
