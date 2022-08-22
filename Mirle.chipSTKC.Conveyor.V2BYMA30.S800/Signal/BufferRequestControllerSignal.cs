using Mirle.MPLC.DataType;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.S800.Signal
{
    public class BufferRequestControllerSignal
    {
        public Word InitalReq { get; internal set; }
        public Word ReadBcrDoneAck { get; internal set; }
        public Word StartRollRequest { get; internal set; }
        public Word PickStartRequest { get; internal set; }
        public Word CommandDoneRequest { get; internal set; }
        public Word BCRReadRequest { get; internal set; }
    }
}
