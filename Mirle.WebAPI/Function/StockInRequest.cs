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
    public class StockInRequest
    {
        private WebApiConfig _config = new WebApiConfig();

        public StockInRequest(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(StockInRequest_Controller info, ref StockInRequest_WCS info_wcs)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriTraceLog_CV(strJson);
                string sLink = $"http://{_config.IP}/STOCKIN_REQUEST";
                clsWriLog.Log.FunWriTraceLog_CV($"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriTraceLog_CV(re);
                info_wcs = (StockInRequest_WCS)Newtonsoft.Json.Linq.JObject.Parse(re).ToObject(typeof(StockInRequest_WCS));

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
