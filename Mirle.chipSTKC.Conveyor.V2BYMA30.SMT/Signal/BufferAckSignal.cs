using Mirle.MPLC.DataType;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.SMT.Signal
{
    public class BufferAckSignal
    {
        public Word InitalAck { get; internal set; }
        public Word ReadBcrSignal { get; internal set; }
        public Word StartRollSignal { get; internal set; }
        public Word PickSignal { get; internal set; }
        public Word PlaceAckSignal { get; internal set; }
        public Word DoneReadAckSignal { get; internal set; }
        public Word DoneCommandAckSignal { get; internal set; }
        public Word DonePlaceSignal { get; internal set; }
    }
}
