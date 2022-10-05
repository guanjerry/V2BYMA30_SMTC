using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mirle.Logger;

namespace Mirle.WebAPI.Event
{
    public class clsWriLog
    {
        public static Log gobjLog = new Log();

        public static void FunWriTraceLog_CV(string sValue)
        {
            try
            {
                gobjLog.WriteLogFile("Mirle.WebAPI.Event_Trace_" + DateTime.Now.ToString("yyyyMMddHH") + ".log", sValue);
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }

        public static void subWriteExLog(string strFunSubName, string strMsg)
        {
            try
            {
                gobjLog.WriteLogFile("Mirle.WebAPI.Event_Exception.log", strFunSubName.PadRight(60, ' ') + ":" + strMsg);
            }
            catch (FieldAccessException exFile)
            {
                MessageBox.Show("clsComSubFun.subWriteExLog Exception:" + exFile.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
