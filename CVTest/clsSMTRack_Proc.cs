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
    //40,44,48
    public class clsSMTRack_Proc
    {
        private System.Timers.Timer timRead = new System.Timers.Timer();
        private bool RequestAway = false;
        public clsSMTRack_Proc()
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
                    int RackBuffer = 5;
                    var SMTBuffer = clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(RackBuffer);
                    string sCmdSno = SMTBuffer.CommandID;
                    if (SMTBuffer.Presence && SMTBuffer.Ready == (int)clsEnum.Ready.Leave)
                    {
                        if (RequestAway == false)
                        {
                            RackAwayInfo info = new RackAwayInfo
                            {
                                stagePosition = "S0-05",
                                rackId = "UNKNOWN"
                            };
                            if (clsWcsApi.GetApiProcess().GetRackAwayInform().FunReport(info))
                            {
                                RequestAway = true;
                            }
                        }
                    }
                    if (!SMTBuffer.Presence && RequestAway == true)
                    {
                        RequestAway = false;
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
