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
using System.ComponentModel.Design;

namespace CVTest
{
    //39,43,47
    public class clsEmptyBinLeaveBinCallCV_Proc
    {
        private System.Timers.Timer timRead = new System.Timers.Timer();
        //private int EmptyCount = 0;
        public clsEmptyBinLeaveBinCallCV_Proc()
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
                for (int CVNo = 1; CVNo <= 3; CVNo += 2)
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        //BinLeaveAsk
                        int bufferNo = 39 + 4 * i;
                        if (clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).IsConnected)
                        {
                            var leaveCVBuffer = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo);
                            string sCmdSno = leaveCVBuffer.CommandID;
                            if (leaveCVBuffer.Presence && leaveCVBuffer.Ready == (int)clsEnum.Ready.Leave && string.IsNullOrWhiteSpace(sCmdSno))
                            {
                                //string CommandID = "11111";
                                //if (!clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo).SetReadReq().Result)
                                //{
                                //    clsInitSys.FunWriTraceLog_Remark("空箱離開...");
                                //}
                                if (string.IsNullOrWhiteSpace(sCmdSno) && !leaveCVBuffer.GetAskLeave())
                                {
                                    UnknownBinLeaveReport info = new UnknownBinLeaveReport
                                    {
                                        position = $"S{CVNo}-{bufferNo}"
                                    };
                                    TrayEmpty_WCS info_wcs = new TrayEmpty_WCS();
                                    if (clsWcsApi.GetApiProcess().GetTrayEmptyInform().FunReport(info, ref info_wcs))
                                    {
                                        clsInitSys.FunWriTraceLog_Remark($"S{CVNo}-{bufferNo.ToString().PadLeft(2, '0')}: 已呼叫空箱離開...");
                                        clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo).SetAskLeave(true);
                                    }
                                }
                            }
                            if (!leaveCVBuffer.Presence && leaveCVBuffer.GetAskLeave() == true)
                                clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo).SetAskLeave(false);
                        }

                        //BinComeCall
                        bufferNo = 38 + 4 * i;
                        if (clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).IsConnected)
                        {
                            var CallCVBuffer = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo);
                            int CallBin = CallCVBuffer.EmptyBinCall;
                            int CallBin_PC = CallCVBuffer.EmptyBinCall_PC;
                            if (CallBin > 0 && CallBin_PC == 0)
                            {
                                if (!CallCVBuffer.GetSentPos())
                                {
                                    EmptyBinLoadRequestInfo info = new EmptyBinLoadRequestInfo
                                    {
                                        jobId = "EMPTY" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                        location = $"S{CVNo}-{bufferNo}",
                                        reqQty = CallBin
                                    };
                                    if (clsWcsApi.GetApiProcess().GetEmptyBinLoadRequest().FunReport(info))
                                    {
                                        clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo).SetSentPos(true);
                                    }
                                }
                            }
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
