using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.Def
{
    public class clsEnum
    {
        public class CmdType
        {
            public enum CraneType
            {
                Single,
                Dual
            }

            public enum ForkType
            {
                SingleFork,
                TwinFork
            }

            public enum LocType
            {
                SingleDeep,
                DoubleDeep,
                Skip //不判斷
            }
        }

        public enum CmdMaintence
        {
            Cancel, Complete
        }

        public enum SCState
        {
            Auto,
            Paused
        }

        public enum Style
        {
            ASRS_IN, ASRS_OUT, ASRS_CYCLE, ASRS_R2R, ASRS_S2S, 
            ASRS_ERROR, Initial
        }

        public class WmsApi
        {
            public enum CancelType
            {
                /// <summary>
                /// 取消入庫命令
                /// </summary>
                PUTAWAY,
                /// <summary>
                /// 取消出庫命令
                /// </summary>
                RETRIEVE,
                /// <summary>
                /// 取消庫對庫命令
                /// </summary>
                SHELF
            }

            public enum CarrierType
            {
                HWS, FOB
            }

            public enum IsEmpty
            {
                Y, N
            }
            public enum IsDouble
            {
                Y, N
            }

            public enum IsOnline
            {
                Y, N
            }

            public enum EqSts
            {
                Down, Run, StockOutOnly
            }
        }

        public enum CraneSts
        {
            None = 0, 
            WaitingHomeAction = 1, 
            HomeAction = 2,
            Idle = 3,
            Busy = 4,
            Stop = 5,
            Maintain = 6,
            Escape = 7,
            Nosts = 8,
            Waiting = 9,
            StopAndOffline = 10,
            DownAndDoorOpen = 11
        }

        public enum PortSts
        {
            Down = 1,
            Run = 2,
        }

        public enum UnitType
        {
            None = 0,
            RM = 1,
            TRU = 2,
            Vehicle = 3,
            Lifter = 4,
            CV = 5
        }

        public enum IOPortStatus
        {
            NONE,
            ERROR,
            NORMAL,
        }

        public enum LocType
        {
            Shelf,
            Port
        }

        public enum NeedL2L
        {
            Y,
            N
        }

        public enum PortMode
        {
            NoMode,
            InMode,
            OutMode
        }

        public enum Fork
        {
            None = 0,
            Left = 1,
            Right = 2
        }

        public enum TaskState
        {
            Queue = 0,
            Initialize = 1,
            Transferring = 2,
            Complete = 3,
            UpdateOK = 4
        }

        public enum TaskCmdState
        {
            Initialize = 0,
            STKCQueue = 1,
            CheckCMDFail_LCS = 2,
            WriteCMD2PLC = 3,
            CheckCMDFail_PLC = 4,
            Cycle1Start = 5,
            Active = 6,
            AtSource = 7,
            ScanComplete = 8,
            Fork1Start = 9,
            CSTPresentOnCrane = 10,
            Cycle2Start = 11,
            AtDestination = 12,
            Fork2Start = 13,
            CSTPresentOffCrane = 14,
            Finish = 15,
            AbnormalFinish = 16,
            CreateAltCmdFinish = 17,
            AbortCancel = 18,
            AlternateCmd = 19,
            NeedRollback = 20,
        }

        public enum TaskMode
        {
            None = 0,
            /// <summary>
            /// 移動
            /// </summary>
            Move = 1,
            /// <summary>
            /// 取物
            /// </summary>
            Pickup = 2,
            /// <summary>
            /// 置物
            /// </summary>
            Deposit = 3,
            /// <summary>
            /// 搬送
            /// </summary>
            Transfer = 4,
            /// <summary>
            /// 掃描
            /// </summary>
            Scan = 7
        }

        public enum AlarmSts
        {
            OnGoing = 0,
            Clear = 1
        }

        public enum BCRReadStatus
        {
            Success = 0,
            Failure = 1,
            Mismatch = 2,
            NoCST = 3,

            None = 9,
        }

        /// <summary>
        /// 序號類型
        /// </summary>
        public enum enuSnoType
        {
            /// <summary>
            /// 命令序號
            /// </summary>
            CMDSNO,

            /// <summary>
            /// 盤點單號
            /// </summary>
            CYCLENO,
            /// <summary>
            /// WCS命令序號
            /// </summary>
            CMDSUO,
            /// <summary>
            /// WCS_Trans_In交易流水號
            /// </summary>
            WCSTrxNo,
            LOCTXNO
        }

        public enum Ready
        {
            NoReady = 0,
            Leave = 1,
            Receive = 2,
            Empty = 3,
            MagLeave = 4,
            MagReceive = 5
        }

        public enum LocSts_Double
        {
            /// <summary>
            /// 非Double Deep
            /// </summary>
            None,
            NNNN,
            SNNS,
            //PNNP,
            ENNE,
            XNNX
        }

        public enum LocSts
        {
            /// <summary>
            /// 空儲位
            /// </summary>
            N,
            /// <summary>
            /// 空料盒
            /// </summary>
            E,
            /// <summary>
            /// 入庫預約
            /// </summary>
            I,
            /// <summary>
            /// 出庫預約
            /// </summary>
            O,
            /// <summary>
            /// 盤點預約
            /// </summary>
            C,
            /// <summary>
            /// 庫存儲位
            /// </summary>
            S,
            /// <summary>
            /// 禁用儲位
            /// </summary>
            X,
            /// <summary>
            /// 調帳預約
            /// </summary>
            P,
            /// <summary>
            /// Lock
            /// </summary>
            L
        }

        public enum Cmd_Abnormal
        {
            NA,
            /// <summary>
            /// 電腦強制完成
            /// </summary>
            CF,
            /// <summary>
            /// 電腦取消
            /// </summary>
            CC,
            /// <summary>
            /// 地上盤強制完成
            /// </summary>
            FF,
            /// <summary>
            /// 地上盤強制取消
            /// </summary>
            EF,
            /// <summary>
            /// 空出庫
            /// </summary>
            E2,
            /// <summary>
            /// 二重格
            /// </summary>
            EC
        }

        public enum LightType
        {
            Green, Orange, Red
        }

        public enum ReplyCodeType
        {
            OK, NG, CmdNG
        }

        public enum ControlType
        {
            Remote, Local
        }

        public enum StatusType
        {
            Idle, Busy, Alarm
        }

        public enum EQP_StatusType
        {
            Run, Down
        }

        public enum BitSignal
        {
            Off, On
        }

        public enum CmdModeType
        {
            None = 0,
            StockIn = 1,
            StockOut = 2,
            StnToStn = 4,
            ShelfToShelf = 5,
            Move = 6,
            PickUp = 8,
            Deposit = 9
        }

        public enum CompleteCodeType
        {
            NormalComplete, EmptyRetrival, DoubleStorage, ManualComplete, ManualCancel
        }

        public enum RequestType
        {
            NoRequest, UnloadRequest, LoadRequest
        }

        public enum InitializeStatus
        {
            //System Initialization的進行階段
            //初始
            Initial = 0,
            //Remote 007已發送
            StartRemote = 1,
            //Remote 007已回复
            DoneRemote = 2,
            //DateSynchronize 已發送
            StartSynchronize = 3,
            //DateSynchronize 已回复
            DoneSynchronize = 4,
            //IPCStatus 已發送
            StartIPCQuery = 5,
            //IPCStatus 已回复
            DoneIPCQuery = 6,
            //AlarmQuery 已發送
            StartAlarmQuery = 7,
            //AlarmQuery 已回复
            DoneAlarmQuery = 8,
            //InitialReq 已發送
            StartInitialReq = 9,
            //InitialReq 已回复
            DoneInitialReq = 10,
            //FirstPortStatus 已發送
            Start1stQuery = 11,
            //FirstPortStatus 已回复
            Done1stQuery = 12,
            //SecondPortStatus 已發送
            Start2ndQuery = 13,
            //SecondPortStatus 已回复
            Done2ndQuery = 14,
            //Done Initialize
            DoneInitialize = 15
        }

    }
}
