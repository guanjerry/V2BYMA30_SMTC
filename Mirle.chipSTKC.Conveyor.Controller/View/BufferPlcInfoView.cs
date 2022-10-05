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
        int[][] InvisibleItem = new int[7][];
        private int[] RollBuffer = new int[] { 1, 7, 13, 19, 25, 31, 37, 40, 41, 44, 45, 48 };
        private int[] S800RollBuffer = new int[] { 1, 4 };
        private int[] TrayIndexBuffer = new int[] { 1, 7, 13, 19, 25, 31 };
        string[] showBuff = new string[] { "6", "5", "3", "2", "49" };
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
            InvisibleItem[6] = new int[] { 1, 4, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                                            31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 50};
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
            if (CurController != 6)
            {
                for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
                {
                    if (!InvisibleItem[CurController].Contains(i))
                        comboBoxBufferIndex.Items.Add($"{i}:S{CurController + 1}-{i.ToString().PadLeft(2, '0')}");
                }
            }
            else
            {
                for (int i = 0; i < showBuff.Length; i++)
                {
                    comboBoxBufferIndex.Items.Add($"{i + 1}:S0-{(i + 1).ToString().PadLeft(2, '0')}");
                }
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
                if (CurController != 6)
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
                else
                {
                    if (_cvcHost.GetS800Manager().IsConnected)
                    {
                        if (comboBoxBufferIndex.SelectedIndex != -1)
                        {
                            int StnIdx = Convert.ToInt32(comboBoxBufferIndex.Text.Split(':')[0]);
                            clsTool.Signal_Show(_cvcHost.GetS800Manager().GetBuffer(StnIdx).Error, ref label_Error);
                            clsTool.Signal_Show(_cvcHost.GetS800Manager().GetBuffer(StnIdx).InMode, ref label_InMode);
                            clsTool.Signal_Show(_cvcHost.GetS800Manager().GetBuffer(StnIdx).OutMode, ref lblOutMode);
                            clsTool.Signal_Show(_cvcHost.GetS800Manager().GetBuffer(StnIdx).Presence, ref label_Load);
                            label_Command.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).CommandID;
                            label_Mode.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).CommandMode.ToString();
                            label_Ready.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).Ready.ToString();
                            label_ReadSignal.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).ReadBcrAck.ToString();
                            label_Path.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).PathNotice.ToString();
                            label_Initial.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).InitialNotice.ToString();
                            if (S800RollBuffer.Contains(StnIdx))
                                label_Start_Roll_Signal.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).StartRollAck.ToString();
                            if (StnIdx == 3)
                                lblTrayID.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).GetTrayID;


                            lblCmd_PC.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).CommandID_PC;
                            lblMode_PC.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).CommandMode_PC.ToString();
                            lblRead_Req.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).ReadBcrReq_PC.ToString();
                            lblPath_PC.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).PathNotice_PC.ToString();
                            lblInitial_Req.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).InitialNotice_PC.ToString();
                            if (S800RollBuffer.Contains(StnIdx))
                                lbl_Start_Roll_Req.Text = _cvcHost.GetS800Manager().GetBuffer(StnIdx).StartRollReq.ToString();

                        }
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
            if (CurController != 6)
            {
                for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
                {
                    if (!InvisibleItem[CurController].Contains(i))
                        comboBoxBufferIndex.Items.Add($"{i}:S{CurController + 1}-{i.ToString().PadLeft(2, '0')}");
                }
            }
            else
            {
                for (int i = 0; i < showBuff.Length; i++)
                {
                    comboBoxBufferIndex.Items.Add($"{i+1}:S0-{(i+1).ToString().PadLeft(2, '0')}");
                }
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
                if (CurController != 6)
                {
                    if (comboBoxBufferIndex.SelectedIndex > -1 && _cvcHost.GetCVCManager(CurController + 1).IsConnected)
                    {
                        int StnIdx = Convert.ToInt32(comboBoxBufferIndex.Text.Split(':')[0]);
                        _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).WriteCommandAsync("00000", 0, 0);
                        if (StnIdx == 37 || StnIdx == 40 || StnIdx == 41 || StnIdx == 44 || StnIdx == 45 || StnIdx == 48 ||
                        StnIdx == 1 || StnIdx == 7 || StnIdx == 13 || StnIdx == 19 || StnIdx == 25 || StnIdx == 31)
                        {
                            _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).SetStopRollAsync();
                        }
                        _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).SetReadReq(0);
                        _LoggerService.WriteLog($"手動按下CV初始PC -> PLC按鈕：<Buffer> {comboBoxBufferIndex.Text.Split(':')[1]}");
                    }
                }
                else
                {
                    if (comboBoxBufferIndex.SelectedIndex > -1 && _cvcHost.GetS800Manager().IsConnected)
                    {
                        int StnIdx = Convert.ToInt32(comboBoxBufferIndex.Text.Split(':')[0]);
                        _cvcHost.GetS800Manager().GetBuffer(StnIdx).WriteCommandAsync("00000", 0, 0);
                        if (StnIdx == 1 || StnIdx == 4)
                        {
                            _cvcHost.GetS800Manager().GetBuffer(StnIdx).SetStopRollAsync();
                        }
                        _cvcHost.GetS800Manager().GetBuffer(StnIdx).SetReadReq(0);
                        _LoggerService.WriteLog($"手動按下CV初始PC -> PLC按鈕：<Buffer> {comboBoxBufferIndex.Text.Split(':')[1]}");
                    }
                    
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
                if (CurController != 6)
                {
                    if (comboBoxBufferIndex.SelectedIndex > -1 && _cvcHost.GetCVCManager(CurController + 1).IsConnected)
                    {
                        int StnIdx = Convert.ToInt32(comboBoxBufferIndex.Text.Split(':')[0]);
                        _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).NoticeInital();

                        _LoggerService.WriteLog($"手動按下CV初始通知按鈕： <Buffer> {comboBoxBufferIndex.Text.Split(':')[1]}");
                    }
                }
                else
                {
                    if (comboBoxBufferIndex.SelectedIndex > -1 && _cvcHost.GetS800Manager().IsConnected)
                    {
                        int StnIdx = Convert.ToInt32(comboBoxBufferIndex.Text.Split(':')[0]);
                        _cvcHost.GetS800Manager().GetBuffer(StnIdx).NoticeInital();

                        _LoggerService.WriteLog($"手動按下CV初始通知按鈕： <Buffer> {comboBoxBufferIndex.Text.Split(':')[1]}");
                    }
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

        private void btnRoll_PcToPlc_Click(object sender, EventArgs e)
        {
            btnRoll_PcToPlc.Enabled = false;
            try
            {
                if (CurController != 6)
                {
                    if (comboBoxBufferIndex.SelectedIndex > -1 && _cvcHost.GetCVCManager(CurController + 1).IsConnected)
                    {
                        int StnIdx = Convert.ToInt32(comboBoxBufferIndex.Text.Split(':')[0]);
                        _LoggerService.WriteLog($"手動按下滾動通知： <Buffer> {comboBoxBufferIndex.Text}");
                        if (StnIdx == 37 || StnIdx == 40 || StnIdx == 41 || StnIdx == 44 || StnIdx == 45 || StnIdx == 48 ||
                        StnIdx == 1 || StnIdx == 7 || StnIdx == 13 || StnIdx == 19 || StnIdx == 25 || StnIdx == 31)
                        {
                            if (!_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).SetStartRollAsync().Result)
                            {
                                string exMessage = $"<Buffer> {comboBoxBufferIndex.Text} fail to write START ROLL...";
                                throw new Exception(exMessage);
                            }
                        }
                    }
                }
                else
                {
                    if (comboBoxBufferIndex.SelectedIndex > -1 && _cvcHost.GetS800Manager().IsConnected)
                    {
                        int StnIdx = Convert.ToInt32(comboBoxBufferIndex.Text.Split(':')[0]);
                        _LoggerService.WriteLog($"手動按下滾動通知： <Buffer> {comboBoxBufferIndex.Text}");
                        if (StnIdx == 1 || StnIdx == 4)
                        {
                            if (!_cvcHost.GetS800Manager().GetBuffer(StnIdx).SetStartRollAsync().Result)
                            {
                                string exMessage = $"<Buffer> {comboBoxBufferIndex.Text} fail to write START ROLL...";
                                throw new Exception(exMessage);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                btnRoll_PcToPlc.Enabled = true;
            }
        }
    }
}
