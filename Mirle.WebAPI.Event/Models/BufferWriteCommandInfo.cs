﻿using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.U2NMMA30.Models
{
    public class BufferWriteCommandInfo : BaseInfo
    {
        public string BufferID { get; set; }
        public string CommandID { get; set; }
    }
}