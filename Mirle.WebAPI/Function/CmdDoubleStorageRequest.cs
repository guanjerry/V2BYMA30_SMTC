using Mirle.Def;
using Mirle.WebAPI.ReportInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Function
{
    public class CmdDoubleStorageRequest
    {
        private WebApiConfig _config = new WebApiConfig();

        public CmdDoubleStorageRequest(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(CmdDoubleStorageRequest_Controller info, ref CmdDoubleStorageRequest_WCS info_wcs)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriTraceLog_CV(strJson);
                string sLink = $"http://{_config.IP}/COMMAND_UPDATE";
                clsWriLog.Log.FunWriTraceLog_CV($"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriTraceLog_CV(re);
                info_wcs = (CmdDoubleStorageRequest_WCS)Newtonsoft.Json.Linq.JObject.Parse(re).ToObject(typeof(CmdDoubleStorageRequest_WCS));
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
