﻿using Mirle.MPLC.DataType;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.S800.Signal
{
    public class BufferSignal
    {
        public BufferSignal(int bufferIndex)
        {
            BufferIndex = bufferIndex;
        }

        public int BufferIndex { get; }
        public Word CommandID { get; internal set; }
        public Word CommandMode { get; internal set; }
        public BufferStatusSignal StatusSignal { get; internal set; }
        public Word Ready { get; internal set; }
        public Word LoadType { get; internal set; }
        public BufferAckSignal AckSignal { get; internal set; }
        public Word PathNotice { get; internal set; }
        public Word PlacePathNotice { get; internal set; }
        public Word IsEmpty { get; internal set; }
        public BufferControllerSignal Controller { get; internal set; }
        public BufferRequestControllerSignal RequestController { get; internal set; }
        public BcrResultSignal BCRResultSignal { get; internal set; }
        public BcrResultSignal BCRInformSignal { get; internal set; }
    }
}
