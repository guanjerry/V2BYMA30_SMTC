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
    //39,43,47
    public class clsEmptyBinLeaveCV_Proc
    {
        private System.Timers.Timer timRead = new System.Timers.Timer();
        //private int EmptyCount = 0;
        public clsEmptyBinLeaveCV_Proc()
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
                for (int CVNo = 1; CVNo <= 5; CVNo += 2)
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        int bufferNo = 39 + 4 * i;
                        if (clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).IsConnected)
                        {
                            var leaveCVBuffer = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo);
                            string sCmdSno = leaveCVBuffer.CommandID;
                            if (leaveCVBuffer.Presence && leaveCVBuffer.Ready == (int)clsEnum.Ready.Empty)
                            {
                                if (string.IsNullOrWhiteSpace(sCmdSno))
                                {
                                    TrayEmpty_Controller info = new TrayEmpty_Controller
                                    {
                                        TrayID = "EMPTY",
                                        Position = $"S{CVNo}-{bufferNo}"
                                    };
                                    TrayEmpty_WCS info_wcs = new TrayEmpty_WCS();
                                    if (clsWcsApi.GetApiProcess().GetTrayEmptyInform().FunReport(info, ref info_wcs))
                                    {
                                        string CommandID = info_wcs.CmdSno;
                                        clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetBuffer(bufferNo).WriteCommandAsync(CommandID, 1, 10);
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
