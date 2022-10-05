using System;
using System.Data;
using Mirle.Def;
using Mirle.WebAPI.Event.Models;
using System.Web.Http;
using Newtonsoft.Json;
using Mirle.SMTCVStart;

namespace Mirle.WebAPI.Event
{
    public class WCSController : ApiController
    {
        public WCSController()
        {
        }
        //37,41,45
        //40,44,48
        //1,7,13,19,25,31
        [Route("SMTC/BUFFER_ROLL_INFO")]
        [HttpPost]
        public IHttpActionResult BUFFER_ROLL_INFO([FromBody] BufferRollInfo Body)
        {
            clsWriLog.Log.FunWriTraceLog_CV($"<BUFFER_ROLL_INFO> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");
            BufferReturnCode rMsg = new BufferReturnCode
            {
                bufferId = Body.BufferID,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriTraceLog_CV($"<{Body.jobId}>BUFFER_ROLL_INFO start!");
            try
            {
                int plcNo = Convert.ToInt32(Body.BufferID.Substring(1, 1));
                int bufferNo = Convert.ToInt32(Body.BufferID.Substring(3, 2));
                if (plcNo != 0)
                {
                    if (clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).IsConnected)
                    {
                        if (!clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).SetStartRollAsync().Result)
                        {
                            string exMessage = $"<Buffer> {Body.BufferID} fail to write START ROLL...";
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
                            string exMessage = $"<Buffer> {Body.BufferID} fail to write START ROLL...";
                            throw new Exception(exMessage);
                        }
                    }
                    else
                    {
                        throw new Exception($"<{Body.jobId}>BUFFER_ROLL_INFO <PLC>{plcNo} not connected!!!");
                    }
                }
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                clsWriLog.Log.FunWriTraceLog_CV($"<{Body.jobId}>BUFFER_ROLL_INFO end!");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        //37,41,45
        //1,7,13,19,25,31
        [Route("SMTC/CV_RECEIVE_NEW_BIN_CMD")]
        [HttpPost]
        public IHttpActionResult CV_RECEIVE_NEW_BIN_CMD([FromBody] CVReceiveNewBinCmdInfo Body)
        {
            clsWriLog.Log.FunWriTraceLog_CV($"<CV_RECEIVE_NEW_BIN_CMD> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");
            ReturnCode rMsg = new ReturnCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriTraceLog_CV($"<{Body.jobId}>CV_RECEIVE_NEW_BIN_CMD start!");
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
                                if (!string.IsNullOrWhiteSpace(clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).CommandID))
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
                clsWriLog.Log.FunWriTraceLog_CV($"<{Body.jobId}>CV_RECEIVE_NEW_BIN_CMD end!");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("SMTC/BUFFER_STATUS_QUERY")]
        [HttpPost]
        public IHttpActionResult BUFFER_STATUS_QUERY([FromBody] BufferStatusInfo Body)
        {
            clsWriLog.Log.FunWriTraceLog_CV($"<BUFFER_STATUS_QUERY> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");
            BufferStatusReturnCode rMsg = new BufferStatusReturnCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId,
                bufferId = Body.bufferId
            };
            clsWriLog.Log.FunWriTraceLog_CV($"<{Body.jobId}>BUFFER_STATUS_QUERY record start!");
            try
            {
                int plcNo = Convert.ToInt32(Body.bufferId.Substring(1, 1));
                int bufferNo = Convert.ToInt32(Body.bufferId.Substring(3, 2));
                if (plcNo != 0)
                {
                    if (clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).IsConnected)
                    {
                        if (!string.IsNullOrWhiteSpace(clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).CommandID))
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
                clsWriLog.Log.FunWriTraceLog_CV($"<{Body.jobId}>CV_RECEIVE_NEW_BIN_CMD end!");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }
    }
}
