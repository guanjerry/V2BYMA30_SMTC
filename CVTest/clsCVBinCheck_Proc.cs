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
    //S0-03
    public class clsCVBinCheck_Proc
    {
        private System.Timers.Timer timRead = new System.Timers.Timer();
        private bool SentApi = false;
        private int ErrCount = 0;
        public clsCVBinCheck_Proc()
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
                if (clsSMTCVStart.GetControllerHost().GetS800Manager().IsConnected)
                {
                    int TrayBuffer = 3;
                    var stockInCV = clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(TrayBuffer);
                    if (stockInCV.Presence && stockInCV.ReadBcrAck == 1 && stockInCV.ReadBcrReq_PC == 0)
                    {
                        var TrayID = stockInCV.GetTrayID.Trim();
                        if (!string.IsNullOrEmpty(TrayID))
                        {
                            clsInitSys.FunWriTraceLog_CV("成功接收TrayID...");
                            BCRCheckRequestInfo bcrinfo = new BCRCheckRequestInfo
                            {
                                location = "S0-03",
                                barcode = TrayID
                            };
                            #region 測試
                            if (clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(TrayBuffer).WriteCommandAndSetReadReqAsync("11111", 1, 10).Result)
                            {
                                string Remark = $"<TrayID> {TrayID}, leave start";
                                clsInitSys.FunWriTraceLog_CV(Remark);
                            }
                            else
                            {
                                string Remark = $"<Buffer> S0-03 fail to write to PLC...";
                                clsInitSys.FunWriTraceLog_CV(Remark);
                            }
                            #endregion 測試
                            #region 正式
                            //if (clsWcsApi.GetApiProcess().GetBCRCheckRequest().FunReport(bcrinfo))
                            //{
                            //    if (clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(TrayBuffer).WritePathAndReadReqAsync(10).Result)
                            //    {
                            //        string Remark = $"<TrayID> {TrayID}, leave start";
                            //        clsInitSys.FunWriTraceLog_CV(Remark);
                            //    }
                            //    else
                            //    {
                            //        string Remark = $"<Buffer> S0-03 fail to write to PLC...";
                            //        clsInitSys.FunWriTraceLog_CV(Remark);
                            //    }
                            //}
                            #endregion 正式

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

        public void FunNextCount()
        {
            if (ErrCount >= 999) ErrCount = 0;
            else ErrCount++;
        }
    }
}
