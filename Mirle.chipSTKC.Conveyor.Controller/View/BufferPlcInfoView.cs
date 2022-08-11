using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Mirle.MPLC;
using Mirle.SMTCV.Conveyor.Config;

namespace Mirle.SMTCV.Conveyor.Controller.View
{
    public partial class BufferPlcInfoView : Form
    {
        private CVCHost _cvcHost;
        private readonly LoggerService _LoggerService;
        //private CVCManager_8F[] cvController = new CVCManager_8F[6];
        private int CurController = 0;
        int[][] InvisibleItem = new int[6][];
        private int[] RollBuffer = new int[] { 1, 7, 13, 19, 25, 31, 37, 40, 41, 44, 45, 48 };
        private int[] TrayIndexBuffer = new int[] { 1, 7, 13, 19, 25, 31 };
        public BufferPlcInfoView(CVCHost controller, int curC, LoggerService Log)
        {
            InitializeComponent();
            _cvcHost = controller;
            InvisibleItem[0] = new int[] { 19, 20, 21, 22, 23, 24 };
            InvisibleItem[1] = new int[] { 19, 20, 21, 22, 23, 24, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 50 };
            InvisibleItem[2] = new int[] { 50 };
            InvisibleItem[3] = new int[] { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48 };
            InvisibleItem[4] = new int[] { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
                                            34, 35, 36, 41, 42, 43, 44, 45, 46, 47, 48, 50 };
            InvisibleItem[5] = new int[] { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
                                            34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };
            SetCurController(curC);
            _LoggerService = Log;
        }
        public int GetCurController()
        {
            return CurController;
        }
        public void SetCurController(int curC)
        {
            CurController = curC;
            ChangeItem();
        }
        private void BufferPlcInfoView_Load(object sender, EventArgs e)
        {
            comboBoxBufferIndex.Items.Clear();
            for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
            {
                if (!InvisibleItem[CurController].Contains(i))
                    comboBoxBufferIndex.Items.Add($"{i}:S{CurController + 1}-{i.ToString().PadLeft(2, '0')}");
            }

            #region 調整Combobox寬度
            int cWidth = 0;
            Graphics g = comboBoxBufferIndex.CreateGraphics();
            for (int i = 0; i < comboBoxBufferIndex.Items.Count; i++)
            {
                int cTemp = (int)g.MeasureString(comboBoxBufferIndex.Items[i].ToString(), comboBoxBufferIndex.Font).Width;
                if (cTemp > cWidth)
                    cWidth = cTemp;
            }
            comboBoxBufferIndex.DropDownWidth = cWidth;
            #endregion 調整Combobox寬度

            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            refreshTimer.Enabled = false;
            try
            {
                if (_cvcHost.GetCVCManager(CurController + 1).IsConnected)
                {
                    if (comboBoxBufferIndex.SelectedIndex != -1)
                    {
                        int StnIdx = Convert.ToInt32(comboBoxBufferIndex.Text.Split(':')[0]);
                        clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).Error, ref label_Error);
                        clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).InMode, ref label_InMode);
                        clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).OutMode, ref lblOutMode);
                        clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).Presence, ref label_Load);
                        label_Command.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).CommandID;
                        label_Mode.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).CommandMode.ToString();
                        label_Ready.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).Ready.ToString();
                        label_ReadSignal.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).ReadBcrAck.ToString();
                        label_Path.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).PathNotice.ToString();
                        label_Initial.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).InitialNotice.ToString();
                        if (RollBuffer.Contains(StnIdx))
                            label_Start_Roll_Signal.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).StartRollAck.ToString();
                        if (TrayIndexBuffer.Contains(StnIdx))
                            lblTrayID.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).GetTrayID;
                        

                        lblCmd_PC.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).CommandID_PC;
                        lblMode_PC.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).CommandMode_PC.ToString();
                        lblRead_Req.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).ReadBcrReq_PC.ToString();
                        lblPath_PC.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).PathNotice_PC.ToString();
                        lblInitial_Req.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).InitialNotice_PC.ToString();
                        if (RollBuffer.Contains(StnIdx))
                            lbl_Start_Roll_Req.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).StartRollReq.ToString();
                        
                    }
                }
            }
            finally
            {
                refreshTimer.Enabled = true;
            }
        }

        private void ChangeItem()
        {
            refreshTimer.Enabled = false;
            comboBoxBufferIndex.Items.Clear();
            for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
            {
                if (!InvisibleItem[CurController].Contains(i))
                    comboBoxBufferIndex.Items.Add($"{i}:S{CurController + 1}-{i.ToString().PadLeft(2, '0')}");
            }

            #region 調整Combobox寬度
            int cWidth = 0;
            Graphics g = comboBoxBufferIndex.CreateGraphics();
            for (int i = 0; i < comboBoxBufferIndex.Items.Count; i++)
            {
                int cTemp = (int)g.MeasureString(comboBoxBufferIndex.Items[i].ToString(), comboBoxBufferIndex.Font).Width;
                if (cTemp > cWidth)
                    cWidth = cTemp;
            }
            comboBoxBufferIndex.DropDownWidth = cWidth;
            #endregion 調整Combobox寬度

            refreshTimer.Enabled = true;
        }

        private void btn_Initial_PC_Click(object sender, EventArgs e)
        {
            btn_Initial_PC.Enabled = false;
            try
            {
                if (comboBoxBufferIndex.SelectedIndex > -1 && _cvcHost.GetCVCManager(CurController + 1).IsConnected)
                {
                    int StnIdx = Convert.ToInt32(comboBoxBufferIndex.Text.Split(':')[0]);
                    _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).WriteCommandAsync("00000", 0, 0);
                    _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).SetReadReq(0);
                    _LoggerService.WriteLog($"手動按下CV初始PC -> PLC按鈕：<Buffer> {comboBoxBufferIndex.Text.Split(':')[1]}");
                }
            }
            catch (Exception ex)
            {
                _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                btn_Initial_PC.Enabled = true;
            }
        }

        private void btn_Initial_PLC_Click(object sender, EventArgs e)
        {
            btn_Initial_PLC.Enabled = false;
            try
            {
                if (comboBoxBufferIndex.SelectedIndex > -1 && _cvcHost.GetCVCManager(CurController + 1).IsConnected)
                {
                    int StnIdx = Convert.ToInt32(comboBoxBufferIndex.Text.Split(':')[0]);
                    _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).NoticeInital();

                    _LoggerService.WriteLog($"手動按下CV初始通知按鈕： <Buffer> {comboBoxBufferIndex.Text.Split(':')[1]}");
                }
            }
            catch (Exception ex)
            {
                _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                btn_Initial_PLC.Enabled = true;
            }
        }
    }
}
