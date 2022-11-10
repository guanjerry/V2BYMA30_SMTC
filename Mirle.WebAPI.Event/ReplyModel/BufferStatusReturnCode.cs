﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.Model
{
    public class BufferStatusReturnCode : ReturnCode
    {
        public string bufferId { get; set; }
        public string ready { get; set; }
        public string isLoad {get; set;}
        public string isEmpty { get; set;}
    }
}
