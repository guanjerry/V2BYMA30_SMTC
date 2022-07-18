using Mirle.SMTCV.Conveyor.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.SMTCV.Conveyor.Controller.View
{
    public partial class MainPLCView : Form
    {
        private LoggerService _loggerService = new LoggerService("CV");
        private CVCHost _cvcHost;
        //private CVCManager_8F[] cvController = new CVCManager_8F[6];
        private int CurController = 0;
        private MonitorLayout _monitorLayout;
        private BufferPlcInfoView _bufferPlcInfoView;
        public MainPLCView()
        {
            InitializeComponent();
        }

        public MainPLCView(CVCHost controller)
        {
            InitializeComponent();
            _cvcHost = controller;
            _monitorLayout = new MonitorLayout(controller, CurController);
            _bufferPlcInfoView = new BufferPlcInfoView(controller, CurController);
        }
        private void SubPLCView_Load(object sender, EventArgs e)
        {
            subInitialLayout();

            butMain_Layout.PerformClick();
            //Start Timer
            timMainProc.Interval = 300;
            timMainProc.Enabled = true;
        }
        public int GetCurController()
        {
            return CurController;
        }
        public void SetCurController(int curC)
        {
            CurController = curC;
            _monitorLayout.SetCurController(curC);
            _bufferPlcInfoView.SetCurController(curC);
        }
        public MonitorLayout GetMonitor()
        {
            return _monitorLayout;
        }

        private void timMainProc_Tick(object sender, EventArgs e)
        {
            timMainProc.Enabled = false;
            try
            {
                //Check PLC
                if (_cvcHost.GetCVCManager(CurController + 1).IsConnected)
                {
                    lblPLCConnSts.BackColor = Color.Lime;
                }
                else
                {
                    lblPLCConnSts.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                _loggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                timMainProc.Enabled = true;
            }
        }
        private void ChangeSubForm(Form subForm)
        {
            try
            {
                butMain_Layout.BackColor = subForm == _monitorLayout ? Color.Aqua : Color.Gainsboro;
                butMain_BufferPLCInfo.BackColor = subForm == _bufferPlcInfoView ? Color.Aqua : Color.Gainsboro;

                var children = splitContainer1.Panel1.Controls;
                foreach (Control c in children)
                {
                    if (c is Form)
                    {
                        var thisChild = c as Form;
                        //thisChild.Hide();
                        splitContainer1.Panel1.Controls.Remove(thisChild);
                        thisChild.Width = 0;
                    }
                }

                Form newChild = subForm;

                if (newChild != null)
                {
                    newChild.TopLevel = false;
                    newChild.Dock = DockStyle.Fill;//適應窗體大小
                    newChild.FormBorderStyle = FormBorderStyle.None;//隱藏右上角的按鈕
                    newChild.Parent = splitContainer1.Panel1;
                    splitContainer1.Panel1.Controls.Add(newChild);
                    newChild.Show();
                }
            }
            catch (Exception ex)
            {
                _loggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
        }
        private void subInitialLayout()
        {
            butMain_Layout.BackColor = Color.Aqua;
            butMain_BufferPLCInfo.BackColor = Color.Gainsboro;
        }

        private void butMain_Layout_Click(object sender, EventArgs e)
        {
            try
            {
                if (_monitorLayout == null)
                {
                    _monitorLayout = new MonitorLayout(_cvcHost, CurController);
                }

                ChangeSubForm(_monitorLayout);
            }
            catch (Exception ex)
            {
                _loggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butMain_BufferPLCInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (_bufferPlcInfoView == null)
                {
                    _bufferPlcInfoView = new BufferPlcInfoView(_cvcHost, CurController);
                }

                ChangeSubForm(_bufferPlcInfoView);
            }
            catch (Exception ex)
            {
                _loggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
