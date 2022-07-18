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
        private CommandUpdate commandUpdate;
        private StockInRequest stockInRequest;
        private CmdDoubleStorageRequest cmdDoubleStorageRequest;
        private AlarmHappenUpdate alarmHappenUpdate;
        private BlockStsChangeInform blockStsChangeInform;
        private ControlChangeInform controlChangeInform;
        private StatusChangeInform statusChangeInform;
        private ForkStsChangeInform forkStsChangeInform;
        private ReelStockIn reelStockIn;
        private TrayCheckBeforeSTKC trayCheckBeforeSTKC;
        private TrayEmptyInform trayEmptyInform;
        private TrayLeaveInform trayLeaveInform;
        private PositionReportFunc positionReportFunc;
        private RackTurnRequest rackTurnRequest;
        private RackAwayInform rackAwayInform;
        private LocationDisableRequest locationDisableRequest;
        private StageEmptyInform stageEmptyInform;
        public clsHost(WebApiConfig Config)
        {
            _config = Config;
            commandUpdate = new CommandUpdate(_config);
            stockInRequest = new StockInRequest(_config);
            cmdDoubleStorageRequest = new CmdDoubleStorageRequest(_config);
            alarmHappenUpdate = new AlarmHappenUpdate(_config);
            blockStsChangeInform = new BlockStsChangeInform(_config);
            controlChangeInform = new ControlChangeInform(_config);
            statusChangeInform = new StatusChangeInform(_config);
            forkStsChangeInform = new ForkStsChangeInform(_config);
            reelStockIn = new ReelStockIn(_config);
            trayCheckBeforeSTKC = new TrayCheckBeforeSTKC(_config);
            trayEmptyInform = new TrayEmptyInform(_config);
            trayLeaveInform = new TrayLeaveInform(_config);
            positionReportFunc = new PositionReportFunc(_config);
            rackTurnRequest = new RackTurnRequest(_config);
            rackAwayInform = new RackAwayInform(_config);
            locationDisableRequest = new LocationDisableRequest(_config);
            stageEmptyInform = new StageEmptyInform(_config);
        }

        public CommandUpdate GetCommandUpdate()
        {
            return commandUpdate;
        }

        public StockInRequest GetStockInRequest()
        {
            return stockInRequest;
        }

        public CmdDoubleStorageRequest GetCmdDoubleStorageRequest()
        {
            return cmdDoubleStorageRequest;
        }

        public AlarmHappenUpdate GetAlarmHappenUpdate()
        {
            return alarmHappenUpdate;
        }

        public BlockStsChangeInform GetBlockStsChangeInform()
        {
            return blockStsChangeInform;
        }

        public ControlChangeInform GetControlChangeInform()
        {
            return controlChangeInform;
        }

        public StatusChangeInform GetStatusChangeInform()
        {
            return statusChangeInform;
        }
        public ForkStsChangeInform GetForkStsChangeInform()
        {
            return forkStsChangeInform;
        }
        public ReelStockIn GetReelStockIn()
        {
            return reelStockIn;
        }
        public TrayCheckBeforeSTKC GetTrayCheckBeforeSTKC()
        {
            return trayCheckBeforeSTKC;
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
        public RackTurnRequest GetRackTurnRequest()
        {
            return rackTurnRequest;
        }
        public RackAwayInform GetRackAwayInform()
        {
            return rackAwayInform;
        }
        public LocationDisableRequest GetLocationDisableRequest()
        {
            return locationDisableRequest;
        }
        public StageEmptyInform GetStageEmptyInform()
        {
            return stageEmptyInform;
        }
    }
}
