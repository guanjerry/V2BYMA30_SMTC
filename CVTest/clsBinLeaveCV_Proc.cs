using Mirle.SMTCVStart;
using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mirle.SMTCV;
using Mirle.WebAPI.ReportInfo;

namespace CVTest
{
    //40,44,48
    public class clsBinLeaveCV_Proc
    {
        private System.Timers.Timer timRead = new System.Timers.Timer();
        private bool[] SentPosition = new bool[3];
        private bool[] MagSentPos = new bool[6];
        public clsBinLeaveCV_Proc()
        {
            timRead.Elapsed += new System.Timers.ElapsedEventHandler(timRead_Elapsed);
            timRead.Enabled = false; timRead.Interval = 500;
            SentPosition = new bool[] { false, false, false };
            MagSentPos = new bool[] { false, false, false, false, false, false };
        }
        public void subStart()
        {
            timRead.Enabled = true;
        }
        private void timRead_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            timRead.Enabled = false;
            try
            {
                for (int CVNo = 1; CVNo <= 4; CVNo++)
                {
                    for (int i = 0; i <= 2; i ++)
                    {
                        //上報離開
                        int bufferNo = 40 + 4 * i;
                        if (clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).IsConnected)
                        {
                            var leaveCVBuffer = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo);
                            string sCmdSno = leaveCVBuffer.CommandID;
                            if (leaveCVBuffer.Presence && leaveCVBuffer.Ready == (int)clsEnum.Ready.Leave)
                            {
                                if (!string.IsNullOrWhiteSpace(sCmdSno) && leaveCVBuffer.GetSentPos() == false)
                                {
                                    PositionReport info = new PositionReport
                                    {
                                        jobId = sCmdSno,
                                        carrierType = "BIN",
                                        position = $"S{CVNo}-{bufferNo.ToString().PadLeft(2, '0')}"
                                    };
                                    if (clsWcsApi.GetApiProcess().GetPositionReportFunc().FunReport(info))
                                    {
                                        clsInitSys.FunWriTraceLog_Remark($"S{CVNo}-{bufferNo.ToString().PadLeft(2, '0')}: <任務號>{sCmdSno} => 抵達終點");
                                        clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo).SetSentPos(true);
                                    }
                                        
                                }
                            }
                            if (!leaveCVBuffer.Presence && leaveCVBuffer.GetSentPos() == true)
                                clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo).SetSentPos(false);
                        }
                    }
                }
                SpinWait.SpinUntil(() => false, 10);
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsInitSys.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
            finally
            {
                timRead.Enabled = true;
            }
        }
    }
}
