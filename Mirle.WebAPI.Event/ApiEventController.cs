using System;
using System.Data;
using System.Windows.Forms;
using Mirle.Def;
//using Mirle.WebAPI.Event.U2NMMA30.Models;
using System.Web.Http;
using Mirle.Structure;
using Newtonsoft.Json;
using Mirle.WebAPI.Event.Model;
using Mirle.SMTCVStart;
using System.Threading;
using static Mirle.Def.clsConstValue;

namespace Mirle.WebAPI.Event
{
    public class WCSController : ApiController
    {
        public WCSController()
        {
        }

        [Route("SMTC/BUFFER_ROLL_INFO")]
        [HttpPost]
        public IHttpActionResult BUFFER_ROLL_INFO([FromBody] BufferInfo Body)
        {
            clsWriLog.FunWriTraceLog_CV($"<BUFFER_ROLL_INFO> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");
            BufferRollReturnCode rMsg = new BufferRollReturnCode
            {
                bufferId = Body.bufferId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.FunWriTraceLog_CV($"<{Body.jobId}>BUFFER_ROLL_INFO start!");
            try
            {
                int plcNo = Convert.ToInt32(Body.bufferId.Substring(1, 1));
                int bufferNo = Convert.ToInt32(Body.bufferId.Substring(3, 2));
                if (bufferNo != 49 && bufferNo != 50)
                {
                    if (plcNo != 0)
                    {
                        if (clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).IsConnected)
                        {
                            if (!clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).SetStartRollAsync().Result)
                            {
                                string exMessage = $"<Buffer> {Body.bufferId} fail to write START ROLL...";
                                throw new Exception(exMessage);
                            }
                            SpinWait.SpinUntil(() => false, 300);
                            if (clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).StartRollAck != 1)
                            {
                                string exMessage = $"<Buffer> {Body.bufferId} fail for PLC START ROLL...";
                                throw new Exception(exMessage);
                            }
                        }
                        else
                        {
                            throw new Exception($"<{Body.jobId}>BUFFER_ROLL_INFO <PLC>{plcNo} not connected!!!");
                        }
                    }
                    else
                    {
                        if (clsSMTCVStart.GetControllerHost().GetS800Manager().IsConnected)
                        {
                            if (!clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(bufferNo).SetStartRollAsync().Result)
                            {
                                string exMessage = $"<Buffer> {Body.bufferId} fail to write START ROLL...";
                                throw new Exception(exMessage);
                            }
                            SpinWait.SpinUntil(() => false, 300);
                            if (clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(bufferNo).StartRollAck != 1)
                            {
                                string exMessage = $"<Buffer> {Body.bufferId} fail for PLC START ROLL...";
                                throw new Exception(exMessage);
                            }
                        }
                        else
                        {
                            throw new Exception($"<{Body.jobId}>BUFFER_ROLL_INFO <PLC>{plcNo} not connected!!!");
                        }
                    }
                }
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                clsWriLog.FunWriTraceLog_CV($"<{Body.jobId}>BUFFER_ROLL_INFO end!");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        //37,41,45
        //1,7,13,19,25,31
        [Route("SMTC/CV_RECEIVE_NEW_BIN_CMD")]
        [HttpPost]
        public IHttpActionResult CV_RECEIVE_NEW_BIN_CMD([FromBody] ReceiveNewBinInfo Body)
        {
            clsWriLog.FunWriTraceLog_CV($"<CV_RECEIVE_NEW_BIN_CMD> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");
            ReturnCode rMsg = new ReturnCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.FunWriTraceLog_CV($"<{Body.jobId}>CV_RECEIVE_NEW_BIN_CMD start!");
            try
            {
                int plcNo = Convert.ToInt32(Body.bufferId.Substring(1, 1));
                int bufferNo = Convert.ToInt32(Body.bufferId.Substring(3, 2));
                if (plcNo != 0)
                {
                    if (clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).IsConnected)
                    {
                        if (bufferNo < 37)
                        {
                            if (bufferNo % 6 == 1)
                            {
                                if (!string.IsNullOrWhiteSpace(clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).CommandID) ||
                                    !string.IsNullOrWhiteSpace(clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).CommandID_PC))
                                {
                                    string exMessage = $"<Buffer> {Body.bufferId} already have other command, FAIL to insert.";
                                    throw new Exception(exMessage);
                                }
                                //path 10 是送輸送板機
                                if (!clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).WriteCommandAsync(Body.jobId, 2, 10).Result)
                                {
                                    string exMessage = $"<Buffer> {Body.bufferId} fail to write Command Info...";
                                    throw new Exception(exMessage);
                                }
                            }
                            else
                            {
                                int agvPort = ((bufferNo - 1)/ 6);
                                agvPort = (agvPort * 6) + 1;
                                //AGV已有命令
                                if (!string.IsNullOrWhiteSpace(clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(agvPort).CommandID))
                                {
                                    string exMessage = $"AGVPort <Buffer> {Body.bufferId} already have other command, FAIL to insert.";
                                    throw new Exception(exMessage);
                                }
                                //定位已有命令
                                if (!string.IsNullOrWhiteSpace(clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).CommandID))
                                {
                                    string exMessage = $"<Buffer> {Body.bufferId} already have other command, FAIL to insert.";
                                    throw new Exception(exMessage);
                                }
                                if (clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(agvPort).WriteCommandAsync(Body.jobId, 2, 20).Result)
                                {
                                    if (!clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).WriteCommandAsync(Body.jobId, 2, 20).Result)
                                    {
                                        string exMessage = $"<Buffer> {Body.bufferId} fail to write Command Info...";
                                        throw new Exception(exMessage);
                                    }
                                }
                                else
                                {
                                    string exMessage = $"AGVPort <Buffer> {Body.bufferId} fail to write Command Info...";
                                    throw new Exception(exMessage);
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).CommandID))
                            {
                                string exMessage = $"<Buffer> {Body.bufferId} already have other command, FAIL to insert.";
                                throw new Exception(exMessage);
                            }
                            //path 10 是送輸送板機
                            if (!clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).WriteCommandAsync(Body.jobId, 1, 10).Result)
                            {
                                string exMessage = $"<Buffer> {Body.bufferId} fail to write Command Info...";
                                throw new Exception(exMessage);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"<{Body.jobId}>CV_RECEIVE_NEW_BIN_CMD <PLC>{plcNo} not connected!!!");
                    }
                }
                else
                {
                    if (clsSMTCVStart.GetControllerHost().GetS800Manager().IsConnected)
                    {
                        if (!string.IsNullOrWhiteSpace(clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(bufferNo).CommandID))
                        {
                            string exMessage = $"<Buffer> {Body.bufferId} already have other command, FAIL to insert.";
                            throw new Exception(exMessage);
                        }
                        //path 10 是送輸送板機
                        if (!clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(bufferNo).WriteCommandAsync(Body.jobId, 2, 10).Result)
                        {
                            string exMessage = $"<Buffer> {Body.bufferId} fail to write Command Info...";
                            throw new Exception(exMessage);
                        }
                    }
                    else
                    {
                        throw new Exception($"<{Body.jobId}>CV_RECEIVE_NEW_BIN_CMD <PLC>{plcNo} not connected!!!");
                    }
                }
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                clsWriLog.FunWriTraceLog_CV($"<{Body.jobId}>CV_RECEIVE_NEW_BIN_CMD end!");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("SMTC/BUFFER_STATUS_QUERY")]
        [HttpPost]
        public IHttpActionResult BUFFER_STATUS_QUERY([FromBody] BufferInfo Body)
        {
            clsWriLog.FunWriTraceLog_CV($"<BUFFER_STATUS_QUERY> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");
            BufferStatusReturnCode rMsg = new BufferStatusReturnCode
            {
                jobId = "",
                transactionId = Body.transactionId,
                bufferId = Body.bufferId
            };
            clsWriLog.FunWriTraceLog_CV($"<{Body.jobId}>BUFFER_STATUS_QUERY record start!");
            try
            {
                int plcNo = Convert.ToInt32(Body.bufferId.Substring(1, 1));
                int bufferNo = Convert.ToInt32(Body.bufferId.Substring(3, 2));
                string CmdSno = "";
                string bufferId = Body.bufferId;
                bool isLoad = false;
                string readySts = "";
                if (plcNo != 0)
                {
                    if (clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).IsConnected)
                    {
                        readySts = clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).Ready.ToString();
                        CmdSno = clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).CommandID;
                        isLoad = clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).Presence;
                    }
                    else
                    {
                        throw new Exception($"<{Body.jobId}>BUFFER_STATUS_QUERY <PLC>{plcNo} not connected!!!");
                    }
                }
                else
                {
                    if (clsSMTCVStart.GetControllerHost().GetS800Manager().IsConnected)
                    {
                        readySts = clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(bufferNo).Ready.ToString();
                        CmdSno = clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(bufferNo).CommandID;
                        isLoad = clsSMTCVStart.GetControllerHost().GetS800Manager().GetBuffer(bufferNo).Presence;
                    }
                    else
                    {
                        throw new Exception($"<{Body.jobId}>BUFFER_STATUS_QUERY <PLC>{plcNo} not connected!!!");
                    }
                }
                rMsg.ready = readySts;
                rMsg.jobId = CmdSno;
                rMsg.isLoad = isLoad == true ? "Y" : "N";
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                clsWriLog.FunWriTraceLog_CV($"<BUFFER_STATUS_QUERY> <Reply Send>\n{JsonConvert.SerializeObject(rMsg)}");
                clsWriLog.FunWriTraceLog_CV($"<{Body.jobId}>BUFFER_STATUS_QUERY record end!");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("SMTC/EMPTY_BIN_LOAD_DONE")]
        [HttpPost]
        public IHttpActionResult EMPTY_BIN_LOAD_DONE([FromBody] EmptyBinLoadDoneInfo Body)
        {
            clsWriLog.FunWriTraceLog_CV($"<EMPTY_BIN_LOAD_DONE> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");
            ReturnCode rMsg = new ReturnCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.FunWriTraceLog_CV($"<{Body.jobId}>EMPTY_BIN_LOAD_DONE record start!");
            try
            {

                int plcNo = Convert.ToInt32(Body.location.Substring(1, 1));
                int bufferNo = Convert.ToInt32(Body.location.Substring(3, 2));
                if (clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).IsConnected)
                {
                    var CallCVBuffer = clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo);
                    int CallBin = CallCVBuffer.EmptyBinCall;
                    int CallBin_PC = CallCVBuffer.EmptyBinCall_PC;
                    if (CallBin > CallBin_PC)
                    {
                        if (!clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).WriteEmptyBinDone(CallBin_PC + 1).Result)
                            throw new Exception($"<{Body.jobId}>EMPTY_BIN_LOAD_DONE <location>{Body.location} fail to write!!!");
                    }
                    if (CallCVBuffer.GetSentPos())
                    {
                        clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).SetSentPos(false);
                    }
                }
                else
                {
                    throw new Exception($"<{Body.jobId}>EMPTY_BIN_LOAD_DONE <PLC>{plcNo} not connected!!!");
                }
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                clsWriLog.FunWriTraceLog_CV($"<EMPTY_BIN_LOAD_DONE> <Reply Send>\n{JsonConvert.SerializeObject(rMsg)}");
                clsWriLog.FunWriTraceLog_CV($"<{Body.jobId}>EMPTY_BIN_LOAD_DONE record end!");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }
    }
}
