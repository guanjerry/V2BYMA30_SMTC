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
        private BCRCheckRequest bcrCheckRequest;
        private PositionReportFunc positionReportFunc;
        private RackAwayInform rackAwayInform;
        private SmtEmptyMagLoadRequest smtEmptyMagLoadRequest;
        private SmtEmptyMagUnload smtEmptyMagUnload;
        private SmtMagLoadRequest smtMagLoadRequest;
        public clsHost(WebApiConfig Config)
        {
            _config = Config;
            alarmHappenUpdate = new AlarmHappenUpdate(_config);
            trayEmptyInform = new UnknownBinLeaveInfo(_config);
            bcrCheckRequest = new BCRCheckRequest(_config);
            positionReportFunc = new PositionReportFunc(_config);
            rackAwayInform = new RackAwayInform(_config);
            smtEmptyMagLoadRequest = new SmtEmptyMagLoadRequest(_config);
            smtEmptyMagUnload = new SmtEmptyMagUnload(_config);
            smtMagLoadRequest = new SmtMagLoadRequest(_config);
        }

        public AlarmHappenUpdate GetAlarmHappenUpdate()
        {
            return alarmHappenUpdate;
        }
        public UnknownBinLeaveInfo GetTrayEmptyInform()
        {
            return trayEmptyInform;
        }
        public BCRCheckRequest GetBcrChechRequest()
        {
            return bcrCheckRequest;
        }
        public PositionReportFunc GetPositionReportFunc()
        {
            return positionReportFunc;
        }
        public RackAwayInform GetRackAwayInform()
        {
            return rackAwayInform;
        }
        public SmtEmptyMagLoadRequest GetEmptyMagLoadRequest()
        {
            return smtEmptyMagLoadRequest;
        }
        public SmtEmptyMagUnload GetSmtEmptyMagUnload()
        {
            return smtEmptyMagUnload;
        }
        public SmtMagLoadRequest GetSmtMagLoadRequest()
        {
            return smtMagLoadRequest;
        }
    }
}
