using Mirle.MPLC.DataType;
using System.Collections;
using System.Collections.Generic;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.S800.Signal
{
    public class ConveyorSignal
    {
        public Word Handshake { get; internal set; }
        public Word ErrorIndex { get; internal set; }
        public Word ErrorCode { get; internal set; }
        public Word ErrorStatus { get; internal set; }
        public ConveyorControllerSignal Controller { get; internal set; }
    }
}
