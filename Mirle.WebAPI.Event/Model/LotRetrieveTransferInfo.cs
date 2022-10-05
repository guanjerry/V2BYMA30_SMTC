using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class LotRetrieveTransferInfo : BaseInfo
    {
        public string priority { get; set; }
        public List<LotRetrieveList> lotList { get; set; }
    }
}
