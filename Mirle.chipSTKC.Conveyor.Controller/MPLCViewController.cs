using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mirle.MPLC;
using Mirle.MPLC.FileData;
using Mirle.MPLC.DataType;
using Mirle.MPLC.DataBlocks;
using Mirle.MPLC.DataBlocks.DeviceRange;
using Mirle.SMTCV.Conveyor.V2BYMA30.SMT.Signal;
using Mirle.SMTCV.Conveyor.Controller.MPLCView;


namespace Mirle.SMTCV.Conveyor.Controller
{
    public class MPLCViewController : IMPLCViewController
    {
        private FileReader _fReader = new FileReader();
        private Form _defaultViewn;
        private Dictionary<Control, IDataType> _controlSignalMap = new Dictionary<Control, IDataType>();
        
        public MPLCViewController()
        {
            foreach (var block in SignalMapper.SignalBlocks)
            {
                _fReader.AddDataBlock(new FileDataBlock(block.DeviceRange, columnIndex: block.PLCRawdataIndex));
            }
            _defaultViewn = new MonitorMainForm(_fReader);
        }

        public void SetMPLCSource(IMPLCProvider provider)
        {
        }

        public Form DefaultView()
        {
            return _defaultViewn;
        }
        public string Title { get; } = "SMT MPLC Log Viewer";

        private IDataType _currentFocusedSignal = null;

        public IDataType CurrentFocusedSignal
        {
            get
            {
                var signal = _currentFocusedSignal;
                _currentFocusedSignal = null;
                return signal;
            }
            internal set
            {
                _currentFocusedSignal = value;
            }
        }

        public FileReader GetFileReader()
        {
            return _fReader;
        }

        public void MappingControlAndSignal(Control control, IDataType signal)
        {
            if (!_controlSignalMap.ContainsKey(control))
            {
                _controlSignalMap.Add(control, signal);
                control.Click += ControlOnClick;
            }
            else
            {
                _controlSignalMap[control] = signal;
            }
        }

        private void ControlOnClick(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                if (_controlSignalMap.TryGetValue(control, out var signal))
                {
                    CurrentFocusedSignal = signal;
                }
            }
        }
    }
}
