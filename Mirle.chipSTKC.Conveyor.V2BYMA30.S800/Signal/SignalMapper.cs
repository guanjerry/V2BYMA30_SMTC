using Mirle.MPLC;
using Mirle.MPLC.DataType;
using System.Collections.Generic;
using Mirle.MPLC.DataBlocks;
using Mirle.MPLC.DataBlocks.DeviceRange;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.S800.Signal
{
    public class SignalMapper
    {
        public static readonly List<BlockInfo> SignalBlocks = new List<BlockInfo>()
        {
            new BlockInfo(new DDeviceRange("D1001", "D5005"), "ReadWrite", 0),
        };

        public readonly static int BufferCount = 5;

        private readonly IMPLCProvider _mplc;
        private readonly Dictionary<int, BufferSignal> _Buffers = new Dictionary<int, BufferSignal>();
        private readonly ConveyorSignal _Signal = new ConveyorSignal();

        public SignalMapper(IMPLCProvider mplc)
        {
            _mplc = mplc;

            MappingConveyor();
            MappingBuffer();
        }

        public ConveyorSignal GetConveyorSignal()
        {
            return _Signal;
        }

        public BufferSignal GetBufferSignal(int bufferIndex)
        {
            _Buffers.TryGetValue(bufferIndex, out var buffer);
            return buffer;
        }

        public int GetBufferCount()
        {
            return BufferCount;
        }

        private void MappingConveyor()
        {
            int addr = 1000;
            _Signal.Handshake = new Word(_mplc, $"D{addr + 1}");
            _Signal.ErrorIndex = new Word(_mplc, $"D{addr + 8}");
            _Signal.ErrorCode = new Word(_mplc, $"D{addr + 9}");
            _Signal.ErrorStatus = new Word(_mplc, $"D{addr + 10}");
            //_Signal.AlarmBit = new BufferAlarmBitSignal();

            //for (int Err = 0; Err < 16; Err++)
            //{
            //    _Signal.AlarmBit.AlarmBit[Err].Checked = new Bit(_mplc, $"D{addr + 7}.{BitValue.BitOrder[Err]}");
            //}

            addr = 3000;
            _Signal.Controller = new ConveyorControllerSignal
            {
                HandShake = new Word(_mplc, $"D{addr + 1}"),
                SystemTimeCalibration = new WordBlock(_mplc, $"D{addr + 2}", 7),
                TimeSynchronize = new Word(_mplc, $"D{addr + 9}"),
                ErrorIndex = new Word(_mplc, $"D{addr + 10}")
            };
        }

        private void MappingBuffer()
        {
            int startAddress = 1010;
            int controllerStartAddress = startAddress + 2000;
            int bcrStartAddress = 5000;
            for (int i = 1; i <= BufferCount; i++)
            {
                int offset = (i - 1) * 10;
                var bufferSignal = new BufferSignal(i);
                bufferSignal.AckSignal = new BufferAckSignal();
                bufferSignal.BCRResultSignal = new BcrResultSignal();
                #region Buffer Signal

                #region PLC -> PC
                bufferSignal.CommandID = new Word(_mplc, $"D{startAddress + offset + 1}");
                bufferSignal.CommandMode = new Word(_mplc, $"D{startAddress + offset + 2}");
                bufferSignal.StatusSignal = new BufferStatusSignal
                {
                    InMode = new Bit(_mplc, $"D{startAddress + offset + 3}.1"),
                    OutMode = new Bit(_mplc, $"D{startAddress + offset + 3}.2"),
                    Error = new Bit(_mplc, $"D{startAddress + offset + 3}.3"),
                    Presence = new Bit(_mplc, $"D{startAddress + offset + 3}.4")
                };
                bufferSignal.Ready = new Word(_mplc, $"D{startAddress + offset + 4}");
                bufferSignal.AckSignal.ReadBcrSignal = new Word(_mplc, $"D{startAddress + offset + 5}");
                bufferSignal.PathNotice = new Word(_mplc, $"D{startAddress + offset + 6}");
                if (i == 1 || i == 4)
                {
                    bufferSignal.AckSignal.StartRollSignal = new Word(_mplc, $"D{startAddress + offset + 9}");
                }
                if (i == 5)
                {
                    bufferSignal.IsEmpty = new Word(_mplc, $"D{startAddress + offset + 9}");
                }
                bufferSignal.AckSignal.InitalAck = new Word(_mplc, $"D{startAddress + offset + 10}");
                #endregion PLC -> PC
                //BCR Reader
                if (i == 3)
                {
                    bufferSignal.BCRResultSignal.TrayID = new BCRResult();
                    bufferSignal.BCRResultSignal.TrayID.ID = new WordBlock(_mplc, $"D{bcrStartAddress + 1}", 5);
                }
                #endregion Buffer Signal


                bufferSignal.Controller = new BufferControllerSignal();
                bufferSignal.RequestController = new BufferRequestControllerSignal();
                //End with 1
                bufferSignal.Controller.CommandID = new Word(_mplc, $"D{controllerStartAddress + offset + 1}");
                //End with 2
                bufferSignal.Controller.CommandMode = new Word(_mplc, $"D{controllerStartAddress + offset + 2}");
                //End with 3

                //End with 4

                //End with 5
                bufferSignal.RequestController.ReadBcrDoneAck = new Word(_mplc, $"D{controllerStartAddress + offset + 5}");
                //End with 6
                bufferSignal.Controller.PathNotice = new Word(_mplc, $"D{controllerStartAddress + offset + 6}");
                //End with 7

                //End with 8
                bufferSignal.Controller.UndoRequest = new Word(_mplc, $"D{controllerStartAddress + offset + 8}");
                //if (i == 9)
                //    bufferSignal.RequestController.PickStartRequest = new Word(_mplc, $"D{controllerStartAddress + offset + 8}");
                //End with 9
                if (i == 1 || i == 4)
                    bufferSignal.RequestController.StartRollRequest = new Word(_mplc, $"D{controllerStartAddress + offset + 9}");
                //End with 0
                bufferSignal.RequestController.InitalReq = new Word(_mplc, $"D{controllerStartAddress + offset + 10}");

                _Buffers.Add(i, bufferSignal);
            }
        }
    }
}
