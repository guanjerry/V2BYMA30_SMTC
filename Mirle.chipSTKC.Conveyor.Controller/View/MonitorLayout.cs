﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.SMTCV.Conveyor.Controller.View
{
    public partial class MonitorLayout : Form
    {
        private CVCHost _cvcHost;
        //private CVCManager_8F[] cvController = new CVCManager_8F[6];
        private int CurController = 0;
        int[][] InvisibleItem = new int[6][];
        public MonitorLayout()
        {
            InitializeComponent();
        }

        public MonitorLayout(CVCHost controller, int curC)
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
            for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
            {
                uclBuffer BufferControl = Controls.Find("ucl" + i, true).FirstOrDefault() as uclBuffer;
                if (i == 49 && (CurController == 1 || CurController == 2))
                {
                    BufferControl = Controls.Find("ucl" + i + "b", true).FirstOrDefault() as uclBuffer;
                }
                BufferControl.BufferName = $"S{CurController + 1}-{i.ToString().PadLeft(2, '0')}";
            }
        }
        public int GetCurController()
        {
            return CurController;
        }
        public void SetCurController(int curC)
        {
            CurController = curC;
            ChangeVisibility(curC);
        }
        private void MonitorLayout_Load(object sender, EventArgs e)
        {
            refresherTimer.Enabled = true;
        }

        private void refresherTimer_Tick(object sender, EventArgs e)
        {
            refresherTimer.Enabled = false;
            if (!_cvcHost.GetCVCManager(CurController + 1).IsConnected)
            {
                for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
                {
                    uclBuffer BufferControl = Controls.Find("ucl" + i, true).FirstOrDefault() as uclBuffer;
                    if (i == 49 && (CurController == 1 || CurController == 2))
                    {
                        BufferControl = Controls.Find("ucl" + i + "b", true).FirstOrDefault() as uclBuffer;
                    }
                    BufferControl.funReadPLCError();
                }
            }
            else
            {
                for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
                {
                    uclBuffer BufferControl = Controls.Find("ucl" + i, true).FirstOrDefault() as uclBuffer;
                    if (i == 49 && (CurController == 1 || CurController == 2))
                    {
                        BufferControl = Controls.Find("ucl" + i + "b", true).FirstOrDefault() as uclBuffer;
                    }
                    BufferControl.Auto = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Auto;
                    BufferControl.bLoad = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Presence;
                    BufferControl.CmdMode = clsTool.funGetEnumValue<uclBuffer.enuCmdMode>(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).CommandMode);
                    BufferControl.CmdSno = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).CommandID;
                    BufferControl.Error = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Error;
                    BufferControl.PathNotice = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).PathNotice;
                    //BufferControl.Position = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Position;
                    BufferControl.ReadNotice = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Signal.AckSignal.ReadBcrSignal.GetValue();
                    BufferControl.Ready = clsTool.funGetEnumValue<uclBuffer.enuReady>(_cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Ready);
                    //BufferControl.Done = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Signal.StatusSignal.Finish.IsOn();
                    BufferControl.InitialNotice = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Signal.AckSignal.InitalAck.GetValue();
                }
            }
            refresherTimer.Enabled = true;
        }

        private void ChangeVisibility(int curC)
        {
            ucl49.Visible = false;
            ucl49b.Visible = false;
            for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
            {
                uclBuffer BufferControl = Controls.Find("ucl" + i, true).FirstOrDefault() as uclBuffer;
                if (i == 49 && (CurController == 1 || CurController == 2))
                {
                    BufferControl = Controls.Find("ucl" + i + "b", true).FirstOrDefault() as uclBuffer;
                }
                BufferControl.Visible = !InvisibleItem[curC].Contains(i);
                if (BufferControl.Visible)
                {
                    BufferControl.BufferName = $"S{CurController + 1}-{i.ToString().PadLeft(2, '0')}";
                }
            }
        }
    }
}