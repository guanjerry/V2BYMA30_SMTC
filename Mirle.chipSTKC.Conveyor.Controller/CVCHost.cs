using Mirle.SMTCV.Conveyor.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.SMTCV.Conveyor.Controller
{
    public class CVCHost
    {
        private readonly CVCManager_8F _cvcManager_8F;
        private readonly CVCManager_8F[] _cVCManager_8F = new CVCManager_8F[4];
        private readonly CVCManager_S800 _cVCManager_S800;
        public CVCHost(ConveyorConfig configE1)
        {
            _cvcManager_8F = new CVCManager_8F(configE1);
            _cvcManager_8F.Start();
        }
        public CVCHost(ConveyorConfig[] configE1, ConveyorConfig cVCManager_S800)
        {
            for (int i = 0; i < 4; i++)
            {
                _cVCManager_8F[i] = new CVCManager_8F(configE1[i]);
                _cVCManager_8F[i].Start();
            }

            _cVCManager_S800 = new CVCManager_S800(cVCManager_S800);
            _cVCManager_S800.Start();
        }
        public CVCManager_8F GetCVCManager()
        {
            return _cvcManager_8F;
        }
        public CVCManager_8F GetCVCManager(int CVNo)
        {
            return _cVCManager_8F[CVNo - 1];
        }
        public CVCManager_S800 GetS800Manager()
        {
            return _cVCManager_S800;
        }
    }
}
