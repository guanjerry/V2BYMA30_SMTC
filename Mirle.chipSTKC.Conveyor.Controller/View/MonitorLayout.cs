using System;
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
        int[][] InvisibleItem = new int[7][];
        int[][] VisibleItem = new int[7][];
        string[] showBuff = new string[] { "6", "5", "3", "2", "49" };

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
            //InvisibleItem[3] = new int[] { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48 };
            InvisibleItem[3] = new int[] { 7, 8, 9, 10, 11, 12, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48 };
            InvisibleItem[4] = new int[] { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
                                            34, 35, 36, 41, 42, 43, 44, 45, 46, 47, 48, 50 };
            InvisibleItem[5] = new int[] { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
                                            34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };
            InvisibleItem[6] = new int[] { 1, 4, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                                            31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 50};
            VisibleItem[1] = new int[31];
            VisibleItem[3] = new int[32];
            SetVisible(ref VisibleItem[1], 0, 6, 31); //31~36
            SetVisible(ref VisibleItem[1], 6, 12, 19); //25~30
            SetVisible(ref VisibleItem[1], 12, 18, 1); //13~18
            SetVisible(ref VisibleItem[1], 18, 24, -11); //7~12
            SetVisible(ref VisibleItem[1], 24, 30, -23); //1~6
            VisibleItem[1][30] = 49;
            SetVisible(ref VisibleItem[3], 0, 6, 31); //31~36
            SetVisible(ref VisibleItem[3], 6, 12, 19); //25~30
            SetVisible(ref VisibleItem[3], 12, 18, 7); //19~24
            SetVisible(ref VisibleItem[3], 18, 24, -5); //13~18
            SetVisible(ref VisibleItem[3], 24, 30, -23); //1~6
            VisibleItem[3][30] = 49;
            VisibleItem[3][31] = 50;
            SetCurController(curC);
            if (CurController != 6)
            {
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
            else
            {
                for (int i = 0; i < showBuff.Length; i++)
                {
                    uclBuffer BufferControl = Controls.Find("ucl" + showBuff[i], true).FirstOrDefault() as uclBuffer;
                    BufferControl.BufferName = $"S0-{(i + 1).ToString().PadLeft(2, '0')}";
                }
            }
        }

        protected void SetVisible(ref int[] array, int start, int end, int request)
        {
            for(int i = start; i < end; i++)
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
            ChangeVisibility(curC);
        }
        private void MonitorLayout_Load(object sender, EventArgs e)
        {
            refresherTimer.Enabled = true;
        }

        private void refresherTimer_Tick(object sender, EventArgs e)
        {
            refresherTimer.Enabled = false;
            if (CurController != 6)
            {
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
                    if (!(CurController == 3 || CurController == 1))
                    {
                        for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
                        {
                            uclBuffer BufferControl = Controls.Find("ucl" + i, true).FirstOrDefault() as uclBuffer;
                            if (CurController == 2 && i ==49)
                                BufferControl = Controls.Find("ucl" + i + "b", true).FirstOrDefault() as uclBuffer;

                            GetSignal(BufferControl, i);
                        }
                    }
                    else if(CurController == 3)
                    {
                        for(int i = 1; i <= VisibleItem[CurController].Length; i++)
                        {
                            int index = 0;
                            uclBuffer BufferControl = Controls.Find("ucl" + VisibleItem[CurController][i - 1], true).FirstOrDefault() as uclBuffer;
                            if ((VisibleItem[CurController][i - 1] == 49 || (VisibleItem[CurController][i - 1] == 50)))
                                index = VisibleItem[CurController][i - 1];
                            else
                                index = i;
                            GetSignal(BufferControl, index);
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= VisibleItem[CurController].Length; i++)
                        {
                            int index = 0;
                            uclBuffer BufferControl = Controls.Find("ucl" + VisibleItem[CurController][i - 1], true).FirstOrDefault() as uclBuffer;
                            if (i == VisibleItem[CurController].Length)
                            {
                                BufferControl = Controls.Find("ucl" + VisibleItem[CurController][i - 1] + "b", true).FirstOrDefault() as uclBuffer;
                                index = 49;
                            }
                            else if (i >= 19 && i < VisibleItem[CurController].Length)
                                index = i + 6;
                            else
                                index = i;
                            GetSignal(BufferControl, index);
                            
                        }
                    }
                }
            }
            else
            {
                if (!_cvcHost.GetS800Manager().IsConnected)
                {
                    for (int i = 1; i <= _cvcHost.GetS800Manager().GetSignalMapper().GetBufferCount(); i++)
                    {
                        uclBuffer BufferControl = Controls.Find("ucl" + showBuff[i-1], true).FirstOrDefault() as uclBuffer;
                        BufferControl.funReadPLCError();
                    }
                }
                else
                {
                    for (int i = 1; i <= _cvcHost.GetS800Manager().GetSignalMapper().GetBufferCount(); i++)
                    {
                        uclBuffer BufferControl = Controls.Find("ucl" + showBuff[i - 1], true).FirstOrDefault() as uclBuffer;
                        //BufferControl.Auto = _cvcHost.GetS800Manager().GetBuffer(i).Auto;
                        BufferControl.bLoad = _cvcHost.GetS800Manager().GetBuffer(i).Presence;
                        BufferControl.CmdMode = clsTool.funGetEnumValue<uclBuffer.enuCmdMode>(_cvcHost.GetS800Manager().GetBuffer(i).CommandMode);
                        BufferControl.CmdSno = _cvcHost.GetS800Manager().GetBuffer(i).CommandID;
                        BufferControl.Error = _cvcHost.GetS800Manager().GetBuffer(i).Error;
                        BufferControl.PathNotice = _cvcHost.GetS800Manager().GetBuffer(i).PathNotice;
                        //BufferControl.Position = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Position;
                        BufferControl.ReadNotice = _cvcHost.GetS800Manager().GetBuffer(i).Signal.AckSignal.ReadBcrSignal.GetValue();
                        BufferControl.Ready = clsTool.funGetEnumValue<uclBuffer.enuReady>(_cvcHost.GetS800Manager().GetBuffer(i).Ready);
                        //BufferControl.Done = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Signal.StatusSignal.Finish.IsOn();
                        BufferControl.InitialNotice = _cvcHost.GetS800Manager().GetBuffer(i).Signal.AckSignal.InitalAck.GetValue();
                    }
                }
            }
            refresherTimer.Enabled = true;
        }

        private void ChangeVisibility(int curC)
        {
            ucl49.Visible = false;
            ucl49b.Visible = false;
            ucl50.Visible = false;
            int line = 0;
            if (curC != 6)
            {
                if (curC != 3 && curC !=1) {
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
                else if(curC == 3)
                {
                    for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
                    {
                        uclBuffer BufferControl = Controls.Find("ucl" + i, true).FirstOrDefault() as uclBuffer;
                        BufferControl.Visible = !InvisibleItem[curC].Contains(i);
                    }
                    for (int i = 1; i <= VisibleItem[curC].Length; i++)
                    {
                        uclBuffer BufferControl = Controls.Find("ucl" + VisibleItem[curC][i - 1], true).FirstOrDefault() as uclBuffer;
                        BufferControl.BufferName = $"S{CurController + 1}-{i.ToString().PadLeft(2, '0')}";

                        if ((VisibleItem[curC][i - 1] == 49 || VisibleItem[curC][i - 1] == 50))
                        {
                            BufferControl = Controls.Find("ucl" + VisibleItem[curC][i - 1], true).FirstOrDefault() as uclBuffer;
                            BufferControl.BufferName = $"S{CurController + 1}-{VisibleItem[curC][i - 1].ToString().PadLeft(2, '0')}";
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= _cvcHost.GetCVCManager(CurController + 1).GetSignalMapper().GetBufferCount(); i++)
                    {
                        uclBuffer BufferControl = Controls.Find("ucl" + i, true).FirstOrDefault() as uclBuffer;
                        if(i ==49)
                            BufferControl = Controls.Find("ucl" + i + "b", true).FirstOrDefault() as uclBuffer;
                        BufferControl.Visible = !InvisibleItem[curC].Contains(i);
                    }
                    for (int i = 1; i <= VisibleItem[curC].Length; i++)
                    {
                        int index = 0;
                        uclBuffer BufferControl = Controls.Find("ucl" + VisibleItem[curC][i - 1], true).FirstOrDefault() as uclBuffer;
                        if (i >= 19 && i < VisibleItem[curC].Length)
                            index = i + 6;
                        else if(i == VisibleItem[curC].Length)
                        {
                            BufferControl = Controls.Find("ucl" + VisibleItem[curC][i - 1] + "b", true).FirstOrDefault() as uclBuffer;
                            BufferControl.BufferName = $"S{CurController + 1}-{VisibleItem[curC][i - 1].ToString().PadLeft(2, '0')}";
                        }
                        else
                            index = i;
                        
                        BufferControl.BufferName = $"S{CurController + 1}-{index.ToString().PadLeft(2, '0')}";

                        
                    }
                }
            }
            else
            {
                foreach (var i in InvisibleItem[6])
                {
                    uclBuffer BufferControl = Controls.Find("ucl" + i, true).FirstOrDefault() as uclBuffer;
                    BufferControl.Visible = false;
                }
                for (int i = 0; i < showBuff.Length; i++)
                {
                    uclBuffer BufferControl = Controls.Find("ucl" + showBuff[i], true).FirstOrDefault() as uclBuffer;
                    BufferControl.Visible = true;
                    BufferControl.BufferName = $"S0-{(i+1).ToString().PadLeft(2, '0')}";
                }
            }
            if (curC == 0 || curC == 1)
                line = 1;
            else if(curC == 2 || curC == 3)
                line = 4;
            else
                line = 7;

            if(curC % 2 == 0 && curC != 4)
            {
                label1.Text = $"Line {line}";
                label2.Text = (line != 7) ? $"Line {line + 1}" : string.Empty;
                label3.Text = (line != 7) ? $"Line {line + 2}" : string.Empty;
            }
            else if(curC % 2 == 1)
            {
                label1.Text = $"Line {line+2}";
                label2.Text = (line != 7) ? $"Line {line + 1}" : string.Empty;
                label3.Text = (line != 7) ? $"Line {line}" : string.Empty;
            }
            else
            {
                label1.Text = $"Line{line}";
                label2.Text = string.Empty;
                label3.Text = string.Empty;
            }
            
        }

        private void GetSignal(uclBuffer BufferControl, int index)
        {
            V2BYMA30.SMT.Buffer buffer = _cvcHost.GetCVCManager(CurController + 1).GetBuffer(index);
            BufferControl.Auto = true;//_cvcHost.GetCVCManager(CurController + 1).GetBuffer(i).Auto;
            BufferControl.bLoad = buffer.Presence;
            BufferControl.CmdMode = clsTool.funGetEnumValue<uclBuffer.enuCmdMode>(buffer.CommandMode);
            BufferControl.CmdSno = buffer.CommandID;
            BufferControl.Error = buffer.Error;
            BufferControl.PathNotice = buffer.PathNotice;
            BufferControl.ReadNotice = buffer.Signal.AckSignal.ReadBcrSignal.GetValue();
            BufferControl.Ready = clsTool.funGetEnumValue<uclBuffer.enuReady>(buffer.Ready);
            BufferControl.InitialNotice = buffer.Signal.AckSignal.InitalAck.GetValue();
        }
    }
}
