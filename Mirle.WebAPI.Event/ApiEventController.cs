using System;
using System.Data;
using Mirle.Def;
using Mirle.WebAPI.Event.U2NMMA30.Models;
using System.Web.Http;
using Newtonsoft.Json;
using Mirle.SMTCVStart;

namespace Mirle.WebAPI.Event.U2NMMA30
{
    public class WCSController : ApiController
    {
        public WCSController()
        {
        }
        //37,41,45
        //40,44,48
        //1,7,13,19,25,31
        [Route("SMTCVC/BUFFER_ROLL_INFO")]
        [HttpPost]
        public IHttpActionResult BUFFER_ROLL_INFO([FromBody] BufferRollInfo Body)
        {
            clsWriLog.Log.FunWriTraceLog_CV($"<BUFFER_ROLL_INFO> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");
            BufferReturnCode rMsg = new BufferReturnCode
            {
                BufferID = Body.BufferID,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriTraceLog_CV($"<{Body.jobId}>BUFFER_ROLL_INFO start!");
            try
            {
                int plcNo = Convert.ToInt32(Body.BufferID.Substring(1, 1));
                int bufferNo = Convert.ToInt32(Body.BufferID.Substring(3, 2));
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
        [Route("SMTCVC/BUFFER_WRITE_COMMAND_INFO")]
        [HttpPost]
        public IHttpActionResult BUFFER_WRITE_COMMAND_INFO([FromBody] BufferWriteCommandInfo Body)
        {
            clsWriLog.Log.FunWriTraceLog_CV($"<BUFFER_WRITE_COMMAND_INFO> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");
            ReturnCode rMsg = new ReturnCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriTraceLog_CV($"<{Body.jobId}>BUFFER_WRITE_COMMAND_INFO start!");
            try
            {
                int plcNo = Convert.ToInt32(Body.BufferID.Substring(1, 1));
                int bufferNo = Convert.ToInt32(Body.BufferID.Substring(3, 2));
                if (clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).IsConnected)
                {
                    if (!string.IsNullOrWhiteSpace(clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).CommandID))
                    {
                        string exMessage = $"<Buffer> {Body.BufferID} already have other command, FAIL to insert.";
                        throw new Exception(exMessage);
                    }
                    //path 10 是送輸送板機
                    if (!clsSMTCVStart.GetControllerHost().GetCVCManager(plcNo).GetBuffer(bufferNo).WriteCommandAsync(Body.CommandID, 2, 10).Result)
                    {
                        string exMessage = $"<Buffer> {Body.BufferID} fail to write Command Info...";
                        throw new Exception(exMessage);
                    }
                }
                else
                {
                    throw new Exception($"<{Body.jobId}>BUFFER_WRITE_COMMAND_INFO <PLC>{plcNo} not connected!!!");
                }
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                clsWriLog.Log.FunWriTraceLog_CV($"<{Body.jobId}>BUFFER_WRITE_COMMAND_INFO end!");
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
