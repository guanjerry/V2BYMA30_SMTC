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
    public class clsMagLeaveSMT_Proc
    {
        private System.Timers.Timer timRead = new System.Timers.Timer();
        public clsMagLeaveSMT_Proc()
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
                    for (int i = 0; i <= 6; i ++)
                    {
                        if (clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).IsConnected)
                        {
                            int BcrBuffer = 6 * i + 1;
                            var PortBuffer = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BcrBuffer);
                            int BufferNo = 0;
                            if (CVNo % 2 == 1)
                            {
                                BufferNo = 6 * i + 2;
                                var leaveCVBuffer = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo);
                                //string sCmdSno = leaveCVBuffer.CommandID;
                                if (leaveCVBuffer.Presence && (!PortBuffer.Presence && string.IsNullOrWhiteSpace(PortBuffer.CommandID)))
                                    if (leaveCVBuffer.ReadBcrAck == 1 && leaveCVBuffer.ReadBcrReq_PC == 0 )
                                    {
                                        string TrayID = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BcrBuffer).GetTrayID.Trim();
                                        if (!string.IsNullOrWhiteSpace(TrayID))
                                        {
                                            clsInitSys.FunWriTraceLog_Remark($"S{CVNo}-{BufferNo.ToString().PadLeft(2, '0')}: <Mag ID>{TrayID} => 準備離開");
                                            SmtEmptyMagUnloadInfo info = new SmtEmptyMagUnloadInfo
                                            {
                                                carrierId = TrayID,
                                                location = $"S{CVNo}-{BufferNo.ToString().PadLeft(2, '0')}"
                                            };
                                            //if (clsWcsApi.GetApiProcess().GetSmtEmptyMagUnload().FunReport(info))
                                            {
                                                clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo).SetReadReq();
                                            }
                                            //BCRCheckRequestInfo info = new BCRCheckRequestInfo
                                            //{
                                            //    barcode = TrayID,
                                            //    ioType = "Mag",
                                            //    location = $"S{CVNo}-{BufferNo.ToString().PadLeft(3, '0')}"
                                            //};
                                            //if (clsWcsApi.GetApiProcess().GetTrayReadyGoInform().FunReport(info))
                                            //{
                                            //    clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BcrBuffer).SetReadReq();
                                            //    //string CommandID = info_wcs.CmdSno;
                                            //    ////填入Port
                                            //    //if (clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BcrBuffer).WriteCommandAndSetReadReqAsync(CommandID, 1, 20).Result)
                                            //    //    //填入觸發的buffer
                                            //    //    clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo).WriteCommandAndSetReadReqAsync(CommandID, 1, 20);
                                            //}
                                        }
                                    }
                            }
                            else
                            {
                                BufferNo = 6 * i + 6;
                                var leaveCVBuffer = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo);
                                //string sCmdSno = leaveCVBuffer.CommandID;
                                if (leaveCVBuffer.Presence && (!PortBuffer.Presence && string.IsNullOrWhiteSpace(PortBuffer.CommandID)))
                                {
                                    if (leaveCVBuffer.ReadBcrAck == 1 && leaveCVBuffer.ReadBcrReq_PC == 0)
                                    {
                                        string TrayID = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BcrBuffer).GetTrayID.Trim();
                                        if (!string.IsNullOrWhiteSpace(TrayID))
                                        {
                                            clsInitSys.FunWriTraceLog_Remark($"S{CVNo}-{BufferNo.ToString().PadLeft(2, '0')}: <Mag ID>{TrayID} => 準備離開");
                                            SmtEmptyMagUnloadInfo info = new SmtEmptyMagUnloadInfo
                                            {
                                                carrierId = TrayID,
                                                location = $"S{CVNo}-{BufferNo.ToString().PadLeft(2, '0')}"
                                            };
                                            //if (clsWcsApi.GetApiProcess().GetSmtEmptyMagUnload().FunReport(info))
                                            {
                                                clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BufferNo).SetReadReq();
                                            }
                                        }
                                    }
                                }
                            }
                            if (PortBuffer.Presence && PortBuffer.Ready == clsConstValue.Ready.Leave && !PortBuffer.GetSentPos())
                            {
                                clsInitSys.FunWriTraceLog_Remark($"S{CVNo}-{BufferNo.ToString().PadLeft(2, '0')}: <任務號>{PortBuffer.CommandID} => 抵達終點");
                                PositionReport info = new PositionReport
                                {
                                    jobId = PortBuffer.CommandID,
                                    carrierType = clsConstValue.carrierType.Mag,
                                    position = $"S{CVNo}-{BcrBuffer.ToString().PadLeft(2, '0')}"
                                };
                                if (clsWcsApi.GetApiProcess().GetPositionReportFunc().FunReport(info))
                                    clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BcrBuffer).SetSentPos(true);
                            }
                            if (!PortBuffer.Presence && PortBuffer.GetSentPos())
                                clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(BcrBuffer).SetSentPos(false);
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
