using Mirle.MPLC.DataType;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.S800.Signal
{
    public class ConveyorControllerSignal
    {
        public Word HandShake { get; internal set; }
        public WordBlock SystemTimeCalibration { get; internal set; }
        public Word TimeSynchronize { get; internal set; }
        public Word ErrorIndex { get; internal set; }
    }
}
