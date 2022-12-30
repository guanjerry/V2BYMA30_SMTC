using Mirle.SMTCV.Conveyor.V2BYMA30.S800.Signal;
using Mirle.SMTCV.Conveyor.Config;
using Mirle.MPLC;
using Mirle.MPLC.DataBlocks;
using Mirle.MPLC.MCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using Mirle.MPLC.DataType;

namespace Mirle.SMTCV.Conveyor.Controller
{
    public class CVCManager_S800 : IDisposable
    {
        private readonly LoggerService _LoggerService;
        private readonly ConveyorConfig _ConveyorConfig;
        private readonly SignalMapper _Signal;
        private readonly Dictionary<int, V2BYMA30.S800.Buffer> _Buffers = new Dictionary<int, V2BYMA30.S800.Buffer>();
        private PLCHost _plcHost;
        private IMPLCProvider _mplc;

        private ThreadWorker _Heartbeat;
        private ThreadWorker _CalibrateSystemTime;
        private ThreadWorker _Refresh;
        private ThreadWorker _Buffer;
        public int[] OpcData => _Signal.GetConveyorSignal().OpcData.GetValue();
        public bool IsConnected => _plcHost.IsConnected;
        public bool PLCViewisConnected => _mplc.IsConnected;
        public CVCManager_S800(ConveyorConfig config)
        {
            _ConveyorConfig = config;
            _LoggerService = new LoggerService(config.ConveyorId);
            if (config.IsMemorySimulator == false)
            {
                if (config.IsUseMCProtocol)
                {
                    InitialMCPLCR();
                }
                else
                {
                    //InitialPLCR();
                }

                //IMPLCProvider mplcWriter = new MPLCService(_plcHost.GetMPLCProvider(), _LoggerService);

                //_mplc = new ReadWriteWrapper(reader: _plcHost as IMPLCProvider, writer: mplcWriter);
            }
            //else
            //{
            //    var smReader = new SMReadOnlyCachedReader();
            //    var smWirter = new SMReadWriter();
            //    foreach (var block in SignalMapper.SignalBlocks)
            //    {
            //        smReader.AddDataBlock(new SMDataBlockInt32(block.DeviceRange, $@"Global\{_ConveyorConfig.ConveyorId}-{block.SharedMemoryName}"));
            //        smWirter.AddDataBlock(new SMDataBlockInt32(block.DeviceRange, $@"Global\{_ConveyorConfig.ConveyorId}-{block.SharedMemoryName}"));
            //    }
            //    _mplc = new ReadWriteWrapper(smReader, smWirter);
            //}

            _Signal = new SignalMapper(_plcHost);
            InitialBuffer();
        }

        public CVCManager_S800(IMPLCProvider mplc)
        {
            _mplc = mplc;
            _Signal = new SignalMapper(_mplc);

            InitialBuffer();
        }

        private void InitialMCPLCR()
        {
            var plcHostInfo = new MPLC.PLCHostInfo(_ConveyorConfig.ConveyorId, _ConveyorConfig.IPAddress, _ConveyorConfig.Port, SignalMapper.SignalBlocks.Select(b => new BlockInfo(b.DeviceRange, $@"Global\{_ConveyorConfig.ConveyorId}-{b.SharedMemoryName}", b.PLCRawdataIndex)));
            //plcHostInfo.IPAddress = _ConveyorConfig.IPAddress;
            //plcHostInfo.Port = _ConveyorConfig.Port;
            //plcHostInfo.HostId = _ConveyorConfig.ConveyorId;
            //plcHostInfo.BlockInfos = SignalMapper.SignalBlocks.Select(b => new BlockInfo(b.DeviceRange, $@"Global\{_ConveyorConfig.ConveyorId}-{b.SharedMemoryName}", b.PLCRawdataIndex));

            _plcHost = new MPLC.MCProtocol.PLCHost(plcHostInfo);
            _plcHost.Interval = 200;
            _plcHost.MPLCTimeout = 5000;
            _plcHost.EnableWriteRawData = true;
            _plcHost.EnableAutoReconnect = true;
            _plcHost.LogBaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "LOG";
            _plcHost.Start();
        }

        //private void InitialPLCR()
        //{
        //    var plcHostInfo = new PLCHostInfo();
        //    plcHostInfo.ActLogicalStationNo = _ConveyorConfig.MPLCNo;
        //    plcHostInfo.HostId = _ConveyorConfig.ConveyorId;
        //    plcHostInfo.BlockInfos = SignalMapper.SignalBlocks.Select(b => new BlockInfo(b.DeviceRange, $@"Global\{_ConveyorConfig.ConveyorId}-{b.SharedMemoryName}", b.PLCRawdataIndex));

        //    _plcHost = new PLCHost(plcHostInfo);
        //    _plcHost.Interval = 200;
        //    _plcHost.EnableWriteRawData = true;
        //    _plcHost.EnableAutoReconnect = true;
        //    _plcHost.LogBaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "LOG";
        //    _plcHost.Start();
        //}

        public V2BYMA30.S800.Buffer GetBuffer(int bufferIndex)
        {
            _Buffers.TryGetValue(bufferIndex, out var buffer);
            return buffer;
        }
        public int GetErrorIndex()
        {
            return _Signal.GetConveyorSignal().ErrorIndex.GetValue();
        }
        public int GetErrorCode()
        {
            return _Signal.GetConveyorSignal().ErrorCode.GetValue();
        }
        public int GetErrorStatus()
        {
            return _Signal.GetConveyorSignal().ErrorStatus.GetValue();
        }
        public int GetCtrlErrorIndex()
        {
            return _Signal.GetConveyorSignal().Controller.ErrorIndex.GetValue();
        }
        public void SetErrorIndex(int errorIndex)
        {
            _Signal.GetConveyorSignal().Controller.ErrorIndex.SetValue(errorIndex);
        }
        public V2BYMA30.S800.Signal.SignalMapper GetSignalMapper()
        {
            return _Signal;
        }

        public void Pause()
        {
            _Heartbeat?.Pause();
            _CalibrateSystemTime?.Pause();
            _Refresh?.Pause();
            _Buffer?.Pause();
        }

        public void Start()
        {
            Pause();

            _Heartbeat = new ThreadWorker(Heartbeat, 300, true);
            _CalibrateSystemTime = new ThreadWorker(CalibrateSystemTime, 1000, true);
            _Refresh = new ThreadWorker(Refresh, 500, true);
            _Buffer = new ThreadWorker(RefreshBuffer, 500, true);
        }

        public void Heartbeat()
        {
            if (!IsConnected)
                return;
            var plc = _Signal.GetConveyorSignal();
            var ctrl = _Signal.GetConveyorSignal().Controller;
            if (plc.Handshake.GetValue() == ctrl.HandShake.GetValue())
            {
                if (plc.Handshake.GetValue() == 0)
                    ctrl.HandShake.SetValue(1);
                else
                    ctrl.HandShake.SetValue(0);
            }
        }

        public void Refresh()
        {
            if (!IsConnected)
                return;
        }

        private void RefreshBuffer()
        {
            if (!IsConnected)
                return;
            foreach (var buffer in _Buffers.Values)
            {
                buffer.Refresh();
            }
        }

        private void CalibrateSystemTime()
        {
            if (!IsConnected)
                return;
            var ctrl = _Signal.GetConveyorSignal().Controller;
            var now = DateTime.Now;
            int[] bcdDatetime = new int[3];
            bcdDatetime[0] = (now.Year % 100 * 256 + now.Month);
            bcdDatetime[1] = (now.Day * 256 + now.Hour);
            bcdDatetime[2] = (now.Minute * 256 + now.Second);

            ctrl.SystemTimeCalibration.SetValue(bcdDatetime);
        }

        private void InitialBuffer()
        {
            foreach (int i in Enumerable.Range(1, SignalMapper.BufferCount))
            {
                var buffer = new V2BYMA30.S800.Buffer(_Signal.GetBufferSignal(i), "CV");
                _Buffers.Add(i, buffer);
            }
        }
        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                Pause();

                _Heartbeat?.Dispose();
                _CalibrateSystemTime?.Dispose();
                _Refresh?.Dispose();
                _Buffer?.Dispose();

                disposedValue = true;
            }
        }

        ~CVCManager_S800()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
