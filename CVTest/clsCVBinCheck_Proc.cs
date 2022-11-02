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
using Mirle.SMTCV.Conveyor.Controller.View;

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
                            clsInitSys.FunWriTraceLog_Remark($"S0-{TrayBuffer.ToString().PadLeft(2, '0')}: 成功接收TrayID...");
                            BCRCheckRequestInfo bcrinfo = new BCRCheckRequestInfo
                            {
                                location = "S0-03",
                                barcode = TrayID
                            };
                            #region 測試
                            //if (clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(TrayBuffer).SetReadReq().Result)
                            //{
                            //    string Remark = $"S0-{TrayBuffer.ToString().PadLeft(2, '0')}:<TrayID> {TrayID}, leave start";
                            //    clsInitSys.FunWriTraceLog_Remark(Remark);
                            //}
                            //else
                            //{
                            //    string Remark = $"S0-{TrayBuffer.ToString().PadLeft(2, '0')}:<Buffer> S0-03 fail to write to PLC...";
                            //    clsInitSys.FunWriTraceLog_Remark(Remark);
                            //}
                            #endregion 測試
                            #region 正式
                            if (clsWcsApi.GetApiProcess().GetBcrCheckRequest().FunReport(bcrinfo))
                            {
                                if (clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(TrayBuffer).SetReadReq().Result)
                                {
                                    string Remark = $"<TrayID> {TrayID}, leave start";
                                    clsInitSys.FunWriTraceLog_CV(Remark);
                                }
                                else
                                {
                                    string Remark = $"<Buffer> S0-03 fail to write to PLC...";
                                    clsInitSys.FunWriTraceLog_CV(Remark);
                                }
                            }
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
