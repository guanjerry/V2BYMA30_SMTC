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
        private static CVCHost[] CVcontrollerHost = new CVCHost[6];
        private static MainPLCView _mainView;

        public static void FunInitalCVController(clsPlcConfig[] CVConfig)
        {
            var ConfigCV = new ConveyorConfig[6];
            for (int i = 0; i < CVConfig.Length; i++)
            {
                ConfigCV[i] = new ConveyorConfig($"CV{i+1}", CVConfig[i].MPLCNo, CVConfig[i].InMemorySimulator, CVConfig[i].UseMCProtocol);
                ConfigCV[i].SetIPAddress(CVConfig[i].MPLCIP);
                ConfigCV[i].SetPort(CVConfig[i].MPLCPort);
                //CVcontrollerHost[i] = new CVCHost(Config_CV);
                //CVcontrollerHost[i].GetCVCManager_E1().Start();
            }
            controllerHost = new CVCHost(ConfigCV);
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
