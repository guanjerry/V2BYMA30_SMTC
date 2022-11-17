﻿using System;
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
        int[][] VisibleItem = new int[7][];
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
            VisibleItem[1] = new int[31];
            VisibleItem[3] = new int[32];
            SetVisible(ref VisibleItem[1], 0, 6, 7);
            SetVisible(ref VisibleItem[1], 6, 12, -5);
            SetVisible(ref VisibleItem[1], 12, 18, 1);
            SetVisible(ref VisibleItem[1], 18, 24, 13);
            SetVisible(ref VisibleItem[1], 24, 30, 1);
            VisibleItem[1][30] = 49;
            SetVisible(ref VisibleItem[3], 0, 6, 7);
            SetVisible(ref VisibleItem[3], 6, 12, -5);
            SetVisible(ref VisibleItem[3], 12, 18, 7);
            SetVisible(ref VisibleItem[3], 18, 24, -5);
            SetVisible(ref VisibleItem[3], 24, 30, 1);
            VisibleItem[3][30] = 49;
            VisibleItem[3][31] = 50;
            SetCurController(curC);
            _LoggerService = Log;
        }
        protected void SetVisible(ref int[] array, int start, int end, int request)
        {
            for (int i = start; i < end; i++)
            {
                array[i] = i + request;
            }
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
                    {
                        if (CurController == 3)
                        {
                            int index = 0;
                            index = Array.IndexOf(VisibleItem[CurController], i);
                            if (index != -1)
                            {
                                if (index < 30)
                                    comboBoxBufferIndex.Items.Add($"{(index + 1)}:S{CurController + 1}-{(index + 1).ToString().PadLeft(2, '0')}");
                                if (index >= 30)
                                    comboBoxBufferIndex.Items.Add($"{i}:S{CurController + 1}-{i.ToString().PadLeft(2, '0')}");
                            }
                        }
                        else if(CurController == 1)
                        {
                            int index = 0;
                            index = Array.IndexOf(VisibleItem[CurController], i);
                            if(index != -1)
                            {
                                if (index < 30)
                                {
                                    if (index >= 18 && index < 24)
                                        comboBoxBufferIndex.Items.Add($"{(index + 13)}:S{CurController + 1}-{(index + 13).ToString().PadLeft(2, '0')}");
                                    else
                                        comboBoxBufferIndex.Items.Add($"{(index + 1)}:S{CurController + 1}-{(index + 1).ToString().PadLeft(2, '0')}");
                                }
                                if (index == 30)
                                    comboBoxBufferIndex.Items.Add($"{i}:S{CurController + 1}-{i.ToString().PadLeft(2, '0')}");
                            }
                        }
                        else
                            comboBoxBufferIndex.Items.Add($"{i}:S{CurController + 1}-{i.ToString().PadLeft(2, '0')}");
                    }
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
                            int BCRIdx = ((StnIdx - 1) / 6) * 6 + 1;
                            clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).Error, ref label_Error);
                            clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).InMode, ref label_InMode);
                            clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).OutMode, ref lblOutMode);
                            clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).Presence, ref label_Load);
                            label_IsEmpty.BackColor = Color.Transparent;
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
                            if (CurController == 0 || CurController == 2)
                            {
                                if (StnIdx == 2 || StnIdx == 8 || StnIdx == 14 || StnIdx == 20 || StnIdx == 26 || StnIdx == 32)
                                {
                                    lblTrayID.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(BCRIdx).GetTrayID;
                                }
                            }
                            if (CurController == 1 || CurController == 3)
                            {
                                if (StnIdx == 6 || StnIdx == 12 || StnIdx == 18 || StnIdx == 24 || StnIdx == 30 || StnIdx == 36)
                                {
                                    lblTrayID.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(BCRIdx).GetTrayID;
                                }
                            }
                            if (StnIdx == 1 || StnIdx == 7 || StnIdx == 13 || StnIdx == 19 || StnIdx == 25 || StnIdx == 31)
                                lblTrayID.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).GetTrayID;


                            if (!lbl_NgGet.Visible)
                            {
                                lbl_NgGet.Visible = true;
                                lbl_NGTime.Visible = true;
                                lbl_AskLeave.Visible = true;
                            }
                            if (StnIdx == 38 || StnIdx == 42 || StnIdx == 46)
                            {
                                if (!label_AskEmpty.Visible)
                                {
                                    label_AskEmpty.Visible = true;
                                    label_EmptyLabel.Visible = true;
                                    lbl_BinReach.Visible = true;
                                    lbl_BinReachLabel.Visible = true;
                                }
                                label_AskEmpty.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).EmptyBinCall.ToString();
                                lbl_BinReach.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).EmptyBinCall_PC.ToString();
                            }
                            clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).GetSentPos(), ref lbl_SentPos);
                            clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).GetAskLeave(), ref lbl_AskLeave);
                            clsTool.Signal_Show(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).GetNGCheck(), ref lbl_NgGet);
                            lbl_NGTime.Text = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).GetRecordTime().ToString("hh:mm:ss.fff");
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
                            if (StnIdx == 5)
                                label_IsEmpty.BackColor = _cvcHost.GetS800Manager().GetBuffer(StnIdx).IsEmpty == 1 ? Color.Yellow : Color.Transparent;

                            clsTool.Signal_Show(_cvcHost.GetS800Manager().GetBuffer(StnIdx).GetSentPos(), ref lbl_SentPos);
                            if (lbl_NgGet.Visible)
                            {
                                lbl_NgGet.Visible = false;
                                lbl_NGTime.Visible = false;
                                lbl_AskLeave.Visible = false;
                                label_AskEmpty.Visible = false;
                                label_EmptyLabel.Visible = false;
                                lbl_BinReach.Visible = false;
                                lbl_BinReachLabel.Visible = false;
                            }
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
                    {
                        if (CurController == 3)
                        {
                            int index = 0;
                            index = Array.IndexOf(VisibleItem[CurController], i);
                            if(index != -1) 
                            {
                                if (index < 30)
                                    comboBoxBufferIndex.Items.Add($"{(index + 1)}:S{CurController + 1}-{(index + 1).ToString().PadLeft(2, '0')}");
                                if (index >= 30)
                                    comboBoxBufferIndex.Items.Add($"{i}:S{CurController + 1}-{i.ToString().PadLeft(2, '0')}");
                            }
                        }
                        else if (CurController == 1)
                        {
                            int index = 0;
                            index = Array.IndexOf(VisibleItem[CurController], i);
                            if (index != -1)
                            {
                                if (index < 30)
                                {
                                    if(index >= 18 && index < 24)
                                        comboBoxBufferIndex.Items.Add($"{(index + 13)}:S{CurController + 1}-{(index + 13).ToString().PadLeft(2, '0')}");
                                    else
                                        comboBoxBufferIndex.Items.Add($"{(index + 1)}:S{CurController + 1}-{(index + 1).ToString().PadLeft(2, '0')}");
                                }
                                    
                                if (index == 30)
                                    comboBoxBufferIndex.Items.Add($"{i}:S{CurController + 1}-{i.ToString().PadLeft(2, '0')}");
                            }
                        }
                        else
                            comboBoxBufferIndex.Items.Add($"{i}:S{CurController + 1}-{i.ToString().PadLeft(2, '0')}");
                    }
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
                        _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).SetAskLeave(false);
                        _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).SetNGCheck(false);
                        _cvcHost.GetCVCManager(CurController + 1).GetBuffer(StnIdx).SetSentPos(false);
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
