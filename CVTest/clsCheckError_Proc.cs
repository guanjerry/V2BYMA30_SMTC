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
    //Error檢查
    public class clsCheckError_Proc
    {
        private System.Timers.Timer timRead = new System.Timers.Timer();
        private bool[] SentPosition = new bool[3];
        public clsCheckError_Proc()
        {
            timRead.Elapsed += new System.Timers.ElapsedEventHandler(timRead_Elapsed);
            timRead.Enabled = false; timRead.Interval = 500;
            SentPosition = new bool[] { false, false, false};
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
                for (int CVNo = 1; CVNo <= 6; CVNo ++)
                {
                    if (clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).IsConnected)
                    {
                        int ErrorIndex = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetErrorIndex();
                        int CtrlErrorIndex = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetCtrlErrorIndex();
                        if (ErrorIndex != CtrlErrorIndex)
                        {
                            string ErrorCode = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetErrorCode().ToString().PadLeft(5, '0');
                            int ErrorStatus = clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).GetErrorStatus();
                            string HappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            AlarmReport info = new AlarmReport
                            {
                                deviceId = "SMTCV",
                                alarmCode = ErrorCode,
                                status = ErrorStatus.ToString(),
                                happenTime = HappenTime
                            };
                            if (clsWcsApi.GetApiProcess().GetAlarmHappenUpdate().FunReport(info))
                            {
                                clsSMTCVStart.GetControllerHost().GetCVCManager(CVNo).SetErrorIndex(ErrorIndex);
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
