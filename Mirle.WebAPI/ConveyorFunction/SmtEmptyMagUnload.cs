using Mirle.Def;
using Mirle.WebAPI.ConveyorReportInfo;
using Mirle.WebAPI.ReportInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorFunction
{
    public class SmtEmptyMagUnload
    {
        private WebApiConfig _config = new WebApiConfig();

        public SmtEmptyMagUnload(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(SmtEmptyMagUnloadInfo info)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriTraceLog_CV(strJson);
                string sLink = $"http://{_config.IP}/WCS/SMTC_EMPTY_MAGAZINE_UNLOAD";
                clsWriLog.Log.FunWriTraceLog_CV($"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriTraceLog_CV(re);
                ReturnMsgInfo info_wcs = (ReturnMsgInfo)Newtonsoft.Json.Linq.JObject.Parse(re).ToObject(typeof(ReturnMsgInfo));
                if (info_wcs.returnCode == clsConstValue.ApiReturnCode.Success) return true;
                else return false;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }
    }
}
