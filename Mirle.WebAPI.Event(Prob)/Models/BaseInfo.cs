using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Models
{
    public class BaseInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "";
    }
}
