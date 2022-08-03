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
    public class UnknownBinLeaveInfo
    {
        private WebApiConfig _config = new WebApiConfig();

        public UnknownBinLeaveInfo(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(UnknownBinLeaveReport info, ref TrayEmpty_WCS info_wcs)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriTraceLog_CV(strJson);
                string sLink = $"http://{_config.IP}/TRAY_EMPTY_INFORM";
                clsWriLog.Log.FunWriTraceLog_CV($"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriTraceLog_CV(re);
                info_wcs = (TrayEmpty_WCS)Newtonsoft.Json.Linq.JObject.Parse(re).ToObject(typeof(TrayEmpty_WCS));

                return true;
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
