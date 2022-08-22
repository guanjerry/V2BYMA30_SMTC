using Mirle.Def;
using Mirle.WebAPI.Function;
using Mirle.WebAPI.ConveyorFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI
{
    public class clsHost
    {
        private WebApiConfig _config = new WebApiConfig();
        private AlarmHappenUpdate alarmHappenUpdate;
        private UnknownBinLeaveInfo trayEmptyInform;
        private BCRCheckRequest trayLeaveInform;
        private PositionReportFunc positionReportFunc;
        private RackAwayInform rackAwayInform;
        private BCRCheckRequest bCRCheckRequest;
        public clsHost(WebApiConfig Config)
        {
            _config = Config;
            alarmHappenUpdate = new AlarmHappenUpdate(_config);
            trayEmptyInform = new UnknownBinLeaveInfo(_config);
            trayLeaveInform = new BCRCheckRequest(_config);
            positionReportFunc = new PositionReportFunc(_config);
            rackAwayInform = new RackAwayInform(_config);
            bCRCheckRequest = new BCRCheckRequest(_config);
        }

        public AlarmHappenUpdate GetAlarmHappenUpdate()
        {
            return alarmHappenUpdate;
        }
        public UnknownBinLeaveInfo GetTrayEmptyInform()
        {
            return trayEmptyInform;
        }
        public BCRCheckRequest GetTrayReadyGoInform()
        {
            return trayLeaveInform;
        }
        public PositionReportFunc GetPositionReportFunc()
        {
            return positionReportFunc;
        }
        public RackAwayInform GetRackAwayInform()
        {
            return rackAwayInform;
        }
        public BCRCheckRequest GetBCRCheckRequest()
        {
            return bCRCheckRequest;
        }
    }
}
