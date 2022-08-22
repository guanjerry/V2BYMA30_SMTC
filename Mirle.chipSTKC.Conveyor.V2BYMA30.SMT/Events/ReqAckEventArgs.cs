using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.SMT.Events
{
    public class ReqAckEventArgs : EventArgs
    {
        public int BufferIndex { get; }

        public ReqAckEventArgs(int bufferIndex)
        {
            BufferIndex = bufferIndex;
        }
    }
}
