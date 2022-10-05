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

namespace Mirle.SMTCVStart
{
    public class clsSMTCVStart
    {
        private static CVCHost controllerHost;
        private static CVCHost[] CVcontrollerHost = new CVCHost[4];
        private static MainPLCView _mainView;

        public static void FunInitalCVController(clsPlcConfig[] CVConfig, clsPlcConfig S800Config)
        {
            var ConfigCV = new ConveyorConfig[4];
            for (int i = 0; i < CVConfig.Length; i++)
            {
                ConfigCV[i] = new ConveyorConfig($"CV{i+1}", CVConfig[i].MPLCNo, CVConfig[i].InMemorySimulator, CVConfig[i].UseMCProtocol);
                ConfigCV[i].SetIPAddress(CVConfig[i].MPLCIP);
                ConfigCV[i].SetPort(CVConfig[i].MPLCPort);
                //CVcontrollerHost[i] = new CVCHost(Config_CV);
                //CVcontrollerHost[i].GetCVCManager_E1().Start();
            }
            var ConfigS800 = new ConveyorConfig($"CV_S800", S800Config.MPLCNo, S800Config.InMemorySimulator, S800Config.UseMCProtocol);
            ConfigS800.SetIPAddress(S800Config.MPLCIP);
            ConfigS800.SetPort(S800Config.MPLCPort);
            controllerHost = new CVCHost(ConfigCV, ConfigS800);
            _mainView = new MainPLCView(controllerHost);

        }
        //public static void FunInitalCVController(clsPlcConfig[] CVConfig)
        //{
        //    for (int i = 0; i < CVConfig.Length; i++)
        //    {
        //        var Config_CV = new ConveyorConfig($"CV{i + 1}", CVConfig[i].MPLCNo, CVConfig[i].InMemorySimulator, CVConfig[i].UseMCProtocol);
        //        Config_CV.SetIPAddress(CVConfig[i].MPLCIP);
        //        Config_CV.SetPort(CVConfig[i].MPLCPort);
        //        CVcontrollerHost[i] = new CVCHost(Config_CV);
        //        //CVcontrollerHost[i].GetCVCManager_E1().Start();
        //    }
        //    _mainView = new MainView(CVcontrollerHost);

        //}
        public static CVCHost GetControllerHost()
        {
            return controllerHost;
        }

        public static CVCHost GetCVControllerHost(int CVNo)
        {
            return CVcontrollerHost[CVNo - 1];
        }
        public static Form GetMainView()
        {
            return _mainView;
        }
        public static MainPLCView GetMainView_Object()
        {
            return _mainView;
        }
    }
}
