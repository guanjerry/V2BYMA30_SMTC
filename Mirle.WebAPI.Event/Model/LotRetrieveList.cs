﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class LotRetrieveList
    {
        public string cmdSno { get; set; }
        public string lotId { get; set; }
        public string fromShelfId { get; set; }
        public string toPortId { get; set; }
        public string rackLocation { get; set; }
        public string largest { get; set; }
    }
}
