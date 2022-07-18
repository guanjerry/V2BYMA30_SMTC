using Mirle.Def;
using Mirle.WebAPI.ConveyorReportInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ConveyorFunction
{
    public class TrayCheckBeforeSTKC
    {
        private WebApiConfig _config = new WebApiConfig();

        public TrayCheckBeforeSTKC(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(TrayCheck_Controller info)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriTraceLog_CV(strJson);
                string sLink = $"http://{_config.IP}/TRAY_CHECK";
                clsWriLog.Log.FunWriTraceLog_CV($"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriTraceLog_CV(re);
                TrayCheck_WCS info_wcs = (TrayCheck_WCS)Newtonsoft.Json.Linq.JObject.Parse(re).ToObject(typeof(TrayCheck_WCS));
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
