using Mirle.SMTCV.Conveyor.Config;
using Mirle.SMTCV.Conveyor.Controller;
using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirle.SMTCV.Conveyor.Controller.View;
using System.Windows.Forms;
using Mirle.OpcClient;
using Mirle.SMTCVStart.Config;
using Config.Net;
using System.IO;

namespace Mirle.SMTCVStart
{
    public class clsOPCStart
    {
        public OpcClientManager Manager = new OpcClientManager();
        private Timer OPCTimer = new Timer();
        Dictionary<int, string> ConveyorGroupIndexList = new Dictionary<int, string>();
        private IOPC ini;
        int TimeInterval = 3000;

        public clsOPCStart()
        {
            GetConfig();
            //Manager = new OpcClientManager();
        }
        public void Initialize()
        {
            Manager.Start();
            OPCTimer.Interval = TimeInterval;
            OPCTimer.Tick += new EventHandler(SendToOpc);
            OPCTimer.Enabled = true;
        }

        public void GetConfig()
        {
            string startpoint = Application.StartupPath + "\\Config\\OPC.ini";
            ini = new ConfigurationBuilder<IOPC>().UseIniFile(startpoint).Build();
            //ini = new ConfigurationBuilder<IOPC>().UseIniFile($@"\Config\OPC.ini").Build();
            clsWriLog.Log.FunWriTraceLog_CV("Start Config: " + startpoint);
            //var name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            //if (name != "WCS")
            //{
            //    if (File.Exists($@"Config\{name}.ini"))
            //    {
            //        ini = new ConfigurationBuilder<IOPC>().UseIniFile($@"Config\{name}.ini").Build();
            //    }
            //    else
            //    {
            //        ini = new ConfigurationBuilder<IOPC>().UseIniFile(@"Config\OPC.ini").Build();
            //    }
            //}
            //else
            //{
            //    ini = new ConfigurationBuilder<IOPC>().UseIniFile(@"Config\OPC.ini").Build();
            //}

            TimeInterval = ini.TimeInterval.TimeInterval > 0 ? ini.TimeInterval.TimeInterval : TimeInterval;
            clsWriLog.Log.FunWriTraceLog_CV("Time Interval: " + ini.TimeInterval.TimeInterval);
            if (ini.Conveyors.S801_123CVFront == true)
            {
                ConveyorGroupIndexList.Add(1, "S801_123CVFront");
                clsWriLog.Log.FunWriTraceLog_CV("Load: S801_123CVFront");
            }
            if (ini.Conveyors.S802_123CVBack == true)
            {
                ConveyorGroupIndexList.Add(2, "S802_123CVBack");
                clsWriLog.Log.FunWriTraceLog_CV("Load: S802_123CVBack");
            }
            if (ini.Conveyors.S801_456CVFront == true)
            {
                ConveyorGroupIndexList.Add(3, "S801_456CVFront");
                clsWriLog.Log.FunWriTraceLog_CV("Load: S801_456CVFront");
            }
            if (ini.Conveyors.S802_456CVBack == true)
            {
                ConveyorGroupIndexList.Add(4, "S802_456CVBack");
                clsWriLog.Log.FunWriTraceLog_CV("Load: S802_456CVBack");
            }
            if (ini.Conveyors.S800CV == true)
            {
                ConveyorGroupIndexList.Add(0, "S800CV");
                clsWriLog.Log.FunWriTraceLog_CV("Load: S800CV");
            }


            //要取得資料的 ConveyorGroup
            //var ConveyorGroupIndexNum = ConveyorGroupIndexList.Select(q => q.Value);

            //foreach (var _ConveyorGroupIndex in ConveyorGroupIndexNum)
            //{
            //    var newClass = new ConveyorGroupController(_ConveyorGroupIndex, true, true);
            //    newClass.Start();
            //    GroupIndexList.Add(newClass);
            //}

            //foreach (var _SorterIndex in SorterList)
            //{
            //    //_SorterConfig = new SorterConfig();
            //    _SorterConfig.SetSorterId(_SorterIndex);
            //    _SorterConfig.SetUseShareMemory();
            //    _SorterConfig.SetOnlyReader();
            //    //_SorterConfig.SetPlcConfig(new PLCConfig("127.0.0.1", 5000));

            //    var SorterClass = new SorterController(_SorterConfig);
            //    SorterClass.Start();
            //    SorterIndexList.Add(SorterClass);
            //}
        }

        public void SendToOpc(object sender, EventArgs e)
        {
            for (int i = 1; i <= 4; i++)
            {
                if (ConveyorGroupIndexList.ContainsKey(i))
                {
                    string sendValue = string.Join(",", clsSMTCVStart.GetControllerHost().GetCVCManager(i).OpcData);
                    Manager.Add(ConveyorGroupIndexList[i], sendValue);
                }
            }
            if (ConveyorGroupIndexList.ContainsKey(0))
            {
                string sendValue = string.Join(",", clsSMTCVStart.GetControllerHost().GetS800Manager().OpcData);
                Manager.Add(ConveyorGroupIndexList[0], sendValue);
            }
            //foreach (var _GroupIndexList in GroupIndexList)
            //{
            //    foreach (var Conveyor in _GroupIndexList.Conveyors)
            //    {
            //        string sendValue = string.Join(",", Conveyor.GetConveorySystem().OpcData);
            //        Manager.Add(Conveyor.ConveyorId, sendValue);
            //    }
            //}

            //foreach (var item in SorterIndexList)
            //{
            //    string sendValue = string.Join(",", item.OpcData);
            //    for (int i = 0; i < SorterList.Count; i++)
            //    {
            //        Manager.Add(SorterList[i], sendValue);
            //    }
            //}
        }
    }
}
