using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class LocationDisableInfo
    {
        public string transactionID { get; set; } = "LOCATION_DISABLE_REQUEST";
        public string Loc { get; set; }
    }
}
