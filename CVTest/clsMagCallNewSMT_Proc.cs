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
using Mirle.WebAPI.ConveyorReportInfo;

namespace CVTest
{
    //1,7,13,19,25,31
    public class clsMagCallNewSMT_Proc
    {
        private System.Timers.Timer timRead = new System.Timers.Timer();
        private string[] UnavailableBuffer = new string[] { "S1-19", "S2-19", "S4-31" };
        public bool BypassSts = false;
        public clsMagCallNewSMT_Proc()
        {
            timRead.Elapsed += new System.Timers.ElapsedEventHandler(timRead_Elapsed);
            timRead.Enabled = false; timRead.Interval = 500;
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
                for (int CVNo = 1; CVNo <= 4; CVNo ++)
                {
                    for (int i = 0; i < 6; i ++)
                    {
                        if (clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).IsConnected)
                        {
                            int BufferNo = 0;
                            int BufferDD = 0;
                            if (CVNo % 2 == 1)
                            {
                                BufferNo = 6 * i + 6;
                                BufferDD = 6 * i + 5;
                            }
                            else
                            {
                                BufferNo = 6 * i + 2;
                                BufferDD = 6 * i + 3;
                            }
                            int BcrBuffer = 6 * i + 1;
                            if (UnavailableBuffer.Contains($"S{CVNo}-{BcrBuffer.ToString().PadLeft(2, '0')}"))
                                continue;
                            var BufferNoCV = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo);
                            var BufferDDCV = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferDD);
                            var PortBufferCV = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BcrBuffer);
                            if (PortBufferCV.Ready == clsConstValue.Ready.Receive && !BypassSts)
                            {
                                if (!BufferNoCV.GetNGCheck() ||
                                (BufferNoCV.GetNGCheck() && ((DateTime.Now - BufferNoCV.GetRecordTime()).Minutes >= 5)))
                                {
                                    if (!BufferNoCV.Presence && !BufferDDCV.Presence)
                                    {
                                        if (string.IsNullOrWhiteSpace(BufferNoCV.CommandID) && string.IsNullOrWhiteSpace(BufferDDCV.CommandID) && !BufferNoCV.GetAskLeave())
                                        {
                                            clsInitSys.FunWriTraceLog_Remark($"S{CVNo}-{BcrBuffer.ToString().PadLeft(2, '0')}: 呼叫新Magazine...");
                                            if (CVNo % 2 == 1)
                                            {
                                                SmtMagLoadRequestInfo info = new SmtMagLoadRequestInfo
                                                {
                                                    location = $"S{CVNo}-{BcrBuffer.ToString().PadLeft(2, '0')}"
                                                };
                                                if (clsWcsApi.GetApiProcess().GetSmtMagLoadRequest().FunReport(info))
                                                {
                                                    clsInitSys.FunWriTraceLog_Remark($"S{CVNo}-{BcrBuffer.ToString().PadLeft(2, '0')}: 已呼叫實Mag來料");
                                                    clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo).SetAskLeave(true);
                                                }
                                                else
                                                {
                                                    clsInitSys.FunWriTraceLog_Remark($"S{CVNo}-{BcrBuffer.ToString().PadLeft(2, '0')}: 實Mag來料呼叫失敗");
                                                    clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo).SetNGCheckWithTime(true, DateTime.Now);
                                                }
                                            }
                                            else
                                            {
                                                SmtEmptyMagLoadRequestInfo info = new SmtEmptyMagLoadRequestInfo
                                                {
                                                    location = $"S{CVNo}-{BcrBuffer.ToString().PadLeft(2, '0')}"
                                                };
                                                if (clsWcsApi.GetApiProcess().GetSmtEmptyMagLoadRequest().FunReport(info))
                                                {
                                                    clsInitSys.FunWriTraceLog_Remark($"S{CVNo}-{BcrBuffer.ToString().PadLeft(2, '0')}: 已呼叫空Mag來料");
                                                    clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo).SetAskLeave(true);
                                                }
                                                else
                                                {
                                                    clsInitSys.FunWriTraceLog_Remark($"S{CVNo}-{BcrBuffer.ToString().PadLeft(2, '0')}: 空Mag來料呼叫失敗");
                                                    clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo).SetNGCheckWithTime(true, DateTime.Now);
                                                }
                                            }
                                        }
                                    }
                                }

                            }

                            if (BufferNoCV.Presence && BufferNoCV.GetAskLeave())
                            {
                                clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo).SetAskLeave(false);
                            }
                            if (BufferNoCV.Presence && BufferNoCV.GetNGCheck())
                            {
                                clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo).SetNGCheck(false);
                            }
                        }
                        SpinWait.SpinUntil(() => false, 50);
                    }
                }
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
