﻿using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.ReportInfo
{
    public class AlarmReport
    {
        public string transactionID { get; set; } = "ALARM_HAPPEN";
        public string EqpID { get; set; }
        public string AlarmCode { get; set; }
        public string Status { get; set; }
        public string HappenTime { get; set; }
    }
}