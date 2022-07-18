using Mirle.MPLC.DataType;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.Signal
{
    public class BufferControllerSignal
    {
        public Word CommandID { get; internal set; }
        public Word CommandMode { get; internal set; }
        public Word PathNotice { get; internal set; }
        public Word PlacePathNotice { get; internal set; }
    }
}
