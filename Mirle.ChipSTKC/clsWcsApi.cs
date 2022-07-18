using Mirle.Def;
using Mirle.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.SMTCV
{
    public class clsWcsApi
    {
        private static clsHost report;

        public static void FunInit(WebApiConfig config)
        {
            report = new clsHost(config);

        }

        public static clsHost GetApiProcess()
        {
            return report;
        }
    }
}
