using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Config.Net;

namespace Mirle.SMTCVStart.Config
{
    public interface IConveyors
    {
        [Option(Alias = "S801_123CVFront")] bool S801_123CVFront { get; set; }
        [Option(Alias = "S802_123CVBack")] bool S802_123CVBack { get; set; }
        [Option(Alias = "S801_456CVFront")] bool S801_456CVFront { get; set; }
        [Option(Alias = "S802_456CVBack")] bool S802_456CVBack { get; set; }
        [Option(Alias = "S800CV")] bool S800CV { get; set; }
    }
}
