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
        private TrayEmptyInform trayEmptyInform;
        private TrayLeaveInform trayLeaveInform;
        private PositionReportFunc positionReportFunc;
        private RackAwayInform rackAwayInform;
        public clsHost(WebApiConfig Config)
        {
            _config = Config;
            alarmHappenUpdate = new AlarmHappenUpdate(_config);
            trayEmptyInform = new TrayEmptyInform(_config);
            trayLeaveInform = new TrayLeaveInform(_config);
            positionReportFunc = new PositionReportFunc(_config);
            rackAwayInform = new RackAwayInform(_config);
        }

        public AlarmHappenUpdate GetAlarmHappenUpdate()
        {
            return alarmHappenUpdate;
        }
        public TrayEmptyInform GetTrayEmptyInform()
        {
            return trayEmptyInform;
        }
        public TrayLeaveInform GetTrayReadyGoInform()
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
    }
}
