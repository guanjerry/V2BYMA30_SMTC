using Mirle.SMTCV.Conveyor.V2BYMA30.S800.Signal;
using Mirle.SMTCV.Conveyor.V2BYMA30.SMT.Signal;
using System.Windows.Forms;
using Mirle.OpcClient;
using System;
using Mirle.OpcClient.V2BYMA30.Config;
using System.IO;
using Config.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mirle.SMTCV.Conveyor.Controller;
using Mirle.SMTCVStart;
//using Mirle.SorterSystem.Config;
//using Mirle.SorterSystem.Sorter;
//using Mirle.SorterSystem.Manager;

namespace Mirle.OpcClient.V2BYMA30
{
    public partial class Form1 : Form
    {
        public readonly OpcClientManager Manager = new OpcClientManager();
        private Timer OPCTimer = new Timer();
        Dictionary<int, string> ConveyorGroupIndexList = new Dictionary<int, string>();
        private IOPC ini;
        int TimeInterval = 0;

        public Form1()
        {
            InitializeComponent();
            Manager.Start();
            GetConfig();
            OPCTimer.Tick += new EventHandler(SendToOpc);
            OPCTimer.Interval = TimeInterval;
            OPCTimer.Enabled = true;
        }
        public void GetConfig()
        {
            
            var name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            if (name != "WCS")
            {
                if (File.Exists($@"Config\{name}.ini"))
                {
                    ini = new ConfigurationBuilder<IOPC>().UseIniFile($@"Config\{name}.ini").Build();
                }
                else
                {
                    ini = new ConfigurationBuilder<IOPC>().UseIniFile(@"Config\OPC.ini").Build();
                }
            }
            else
            {
                ini = new ConfigurationBuilder<IOPC>().UseIniFile(@"Config\OPC.ini").Build();
            }

            TimeInterval = ini.TimeInterval.TimeInterval;
            if (ini.Conveyors.S801_123CVFront == true)
            {
                ConveyorGroupIndexList.Add(1, "S801_123CVFront");
            }
            if (ini.Conveyors.S802_123CVBack == true)
            {
                ConveyorGroupIndexList.Add(2, "S802_123CVBack");
            }
            if (ini.Conveyors.S801_456CVFront == true)
            {
                ConveyorGroupIndexList.Add(3, "S801_456CVFront");
            }
            if (ini.Conveyors.S801_456CVBack == true)
            {
                ConveyorGroupIndexList.Add(4, "S801_456CVBack");
            }
            if (ini.Conveyors.S800CV == true)
            {
                ConveyorGroupIndexList.Add(0, "S800CV");
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

        private void Form1_Load(object sender, EventArgs e)
        {
            //Form1_Resize(sender, e);
        }
        //private void Form1_Resize(object sender, EventArgs e)
        //{
        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        notifyIcon1.Visible = true;
        //        this.Hide();
        //    }
        //    else if (this.WindowState == FormWindowState.Normal)
        //    {
        //        notifyIcon1.Visible = false;
        //    }
        //}
        //private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        //{
        //    this.Show();
        //    this.WindowState = FormWindowState.Normal;
        //}
    }
}
