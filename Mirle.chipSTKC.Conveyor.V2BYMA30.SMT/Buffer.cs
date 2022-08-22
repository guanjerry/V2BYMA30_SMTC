using Mirle.SMTCV.Conveyor.V2BYMA30.SMT.Events;
using Mirle.SMTCV.Conveyor.V2BYMA30.SMT.Signal;
using Mirle.MPLC.DataType;
using System;
using System.Reflection;
using Mirle.SMTCV.Conveyor.Config;
using System.Threading.Tasks;
using Mirle.Def;

namespace Mirle.SMTCV.Conveyor.V2BYMA30.SMT
{
    public class Buffer
    {
        public delegate void NoticeReleaseEventHandler(object sender, ReqAckEventArgs e);
        public delegate void BufferStatusEventHandler(object sender, BufferEventArgs e);
        public delegate void BufferPresenceEventHandler(object sender, BufferEventArgs e);

        public event NoticeReleaseEventHandler OnIniatlNotice;
        public event BufferStatusEventHandler OnStatusChanged;
        public event BufferPresenceEventHandler OnPresenceChanged;

        private bool _InitalReport = false;
        private clsEnum.WmsApi.EqSts _lastStatus = clsEnum.WmsApi.EqSts.StockOutOnly;
        private bool _lastPresence = false;
        private bool SentPos = false;
        public BufferSignal Signal { get; }
        public bool GetSentPos()
        { 
            return SentPos; 
        }
        public void SetSentPos(bool info)
        {
            SentPos = info;
        }
        public string CommandID
        {
            get
            {
                if (Signal.CommandID.GetValue() == 0) return "";
                else return Signal.CommandID.GetValue().ToString().PadLeft(5, '0');
            }
        }

        public string CommandID_PC
        {
            get
            {
                if (Signal.Controller.CommandID.GetValue() == 0) return "";
                else return Signal.Controller.CommandID.GetValue().ToString().PadLeft(5, '0');
            }
        }

        public int CommandMode => Signal.CommandMode.GetValue();
        public int CommandMode_PC => Signal.Controller.CommandMode.GetValue();
        public int PathNotice => Signal.PathNotice.GetValue();
        public int PathNotice_PC => Signal.Controller.PathNotice.GetValue();
        public int Ready => Signal.Ready.GetValue();
        public int ReadBcrAck => Signal.AckSignal.ReadBcrSignal.GetValue();
        public int ReadBcrReq_PC => Signal.RequestController.ReadBcrDoneAck.GetValue();
        public int InitialNotice => Signal.AckSignal.InitalAck.GetValue();
        public int InitialNotice_PC => Signal.RequestController.InitalReq.GetValue();
        /// <summary>
        /// 手動入庫通知 (0: 自動入庫，1: 手動入庫)
        /// </summary>
        public bool InMode => Signal.StatusSignal.InMode.IsOn();
        public bool OutMode => Signal.StatusSignal.OutMode.IsOn();
        public bool Position => Signal.StatusSignal.Position.IsOn();
        public bool Presence => Signal.StatusSignal.Presence.IsOn();
        public bool Error => Signal.StatusSignal.Error.IsOn();
        public bool Auto => Signal.StatusSignal.Auto.IsOn();
        public bool Manual => Signal.StatusSignal.Manual.IsOn();
        public bool EMO => Signal.StatusSignal.EMO.IsOn();
        public bool Finish => Signal.StatusSignal.Finish.IsOn();
        public bool Online => Signal.StatusSignal.Online.IsOn();
        public bool Offline => Signal.StatusSignal.Offline.IsOn();
        public int StartRollReq => Signal.RequestController.StartRollRequest.GetValue();
        public int StartRollAck => Signal.AckSignal.StartRollSignal.GetValue();

        public string GetReelID
        {
            get
            {
                try
                {
                    if (Signal.BCRResultSignal.ReelID.ID != null)
                    {
                        return Signal.BCRResultSignal.ReelID.ID.GetValue().ToASCII();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch
                {
                    return "";
                }
            }
        }

        public string GetTrayID
        {
            get
            {
                try
                {
                    if (Signal.BCRResultSignal.TrayID.ID != null)
                    {
                        return Signal.BCRResultSignal.TrayID.ID.GetValue().ToASCII();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch
                {
                    return "";
                }
            }
        }


        private readonly LoggerService _LoggerService;

        public Buffer(BufferSignal signal, string ConveyorId)
        {
            _LoggerService = new LoggerService(ConveyorId);
            Signal = signal;
        }

        public clsEnum.WmsApi.EqSts Status
        {
            get
            {
                if (!Auto || Error) return clsEnum.WmsApi.EqSts.Down;
                else return clsEnum.WmsApi.EqSts.Run;
            }
        }


        public Task<bool> WriteCommandAsync(string Command, int commandMode, int Path)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(CommandID)) return false;

                    Signal.Controller.CommandMode.SetValue(commandMode);
                    Signal.Controller.PathNotice.SetValue(Path);
                    Signal.Controller.CommandMode.SetValue(2);
                    Signal.Controller.CommandID.SetValue(Convert.ToInt32(Command));

                    Task.Delay(500).Wait();
                    return true;
                }
                catch (Exception ex)
                {
                    _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
                    return false;
                }
            });
        }

        public Task<bool> WriteCommandAndSetReadReqAsync(string Command, int commandMode, int Path)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(CommandID)) return false;

                    Signal.Controller.CommandMode.SetValue(commandMode);
                    Signal.Controller.PathNotice.SetValue(Path);
                    Signal.RequestController.ReadBcrDoneAck.SetValue(1);
                    Signal.Controller.CommandID.SetValue(Convert.ToInt32(Command));

                    Task.Delay(500).Wait();
                    return true;
                }
                catch (Exception ex)
                {
                    _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
                    return false;
                }
            });
        }

        public Task<bool> WritePathAndReadReqAsync(int Path)
        {
            return Task.Run(() =>
            {
                try
                {
                    Signal.Controller.PathNotice.SetValue(Path);
                    Signal.RequestController.ReadBcrDoneAck.SetValue(1);

                    Task.Delay(500).Wait();
                    return true;
                }
                catch (Exception ex)
                {
                    _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
                    return false;
                }
            });
        }


        public Task<bool> SetStartRollAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    Signal.RequestController.StartRollRequest.SetValue(1);
                    Task.Delay(500).Wait();
                    return true;
                }
                catch (Exception ex)
                {
                    _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
                    return false;
                }
            });
        }
        /// <summary>
        /// 通知初始
        /// </summary>
        /// <returns></returns>
        public Task<bool> NoticeInital()
        {
            return Task.Run(() =>
            {
                try
                {
                    Signal.RequestController.InitalReq.SetValue(1);
                    Task.Delay(500).Wait();
                    return true;
                }
                catch(Exception ex)
                {
                    _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
                    return false;
                }
            });
        }

        public Task<bool> SetReadReq()
        {
            return Task.Run(() =>
            {
                try
                {
                    Signal.RequestController.ReadBcrDoneAck.SetValue(1);
                    Task.Delay(500).Wait();
                    return true;
                }
                catch (Exception ex)
                {
                    _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
                    return false;
                }
            });
        }

        public Task<bool> SetReadReq(int value)
        {
            return Task.Run(() =>
            {
                try
                {
                    Signal.RequestController.ReadBcrDoneAck.SetValue(value);
                    Task.Delay(500).Wait();
                    return true;
                }
                catch (Exception ex)
                {
                    _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
                    return false;
                }
            });
        }

        public void Refresh()
        {
            try
            {
                ClearController();
            }
            catch(Exception ex)
            {
                _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }

            CheckReqAckStatus(ref _InitalReport, OnIniatlNotice, Signal.RequestController.InitalReq, Signal.AckSignal.InitalAck);
            //CheckStatus();
            CheckPresence();

        }

        private void CheckStatus()
        {
            var newStatus = this.Status;
            if (_lastStatus != newStatus)
            {
                _lastStatus = newStatus;
                var args = new BufferEventArgs(Signal.BufferIndex) { NewStatus = newStatus };
                OnStatusChanged?.Invoke(this, args);
            }
        }

        private void CheckPresence()
        {
            var newPresence = Presence || !string.IsNullOrWhiteSpace(CommandID);
            if(_lastPresence != newPresence)
            {
                _lastPresence = newPresence;
                var args = new BufferEventArgs(Signal.BufferIndex) { Presence = newPresence };
                OnPresenceChanged?.Invoke(this, args);
            }
        }

        private void ClearController()
        {
            //Clear Command ID
            try
            {
                if (Signal.CommandID.GetValue() > 0 && Signal.CommandID.GetValue() == Signal.Controller.CommandID.GetValue())
                {
                    Signal.Controller.CommandID.SetValue(0);
                }
            }
            catch (Exception ex)
            {
                _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
            //Clear Command Mode
            try
            {
                if (Signal.CommandMode.GetValue() > 0 && Signal.CommandMode.GetValue() == Signal.Controller.CommandMode.GetValue())
                {
                    Signal.Controller.CommandMode.SetValue(0);
                }
            }
            catch (Exception ex)
            {
                _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
            //Clear Path Notice
            try
            {
                if (Signal.PathNotice.GetValue() > 0 && Signal.PathNotice.GetValue() == Signal.Controller.PathNotice.GetValue())
                {
                    Signal.Controller.PathNotice.SetValue(0);
                }
            }
            catch (Exception ex)
            {
                _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
            //Clear Read BCR
            try
            {
                if (Signal.RequestController.ReadBcrDoneAck.GetValue() != 0 && Signal.AckSignal.ReadBcrSignal.GetValue() == 0)
                {
                    Signal.RequestController.ReadBcrDoneAck.SetValue(0);
                }
            }
            catch (Exception ex)
            {
                _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
            //Clear Pickup Request
            try
            {
                if (Signal.RequestController.PickStartRequest != null && Signal.AckSignal.PickSignal != null)
                    if (Signal.RequestController.PickStartRequest.GetValue() != 0 && Signal.AckSignal.PickSignal.GetValue() != 0)
                    {
                        Signal.RequestController.PickStartRequest.SetValue(0);
                    }
            }
            catch (Exception ex)
            {
                _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }
            //Clear Roll Request
            try
            {
                if (Signal.RequestController.StartRollRequest != null && Signal.AckSignal.StartRollSignal != null)
                    if (Signal.RequestController.StartRollRequest.GetValue() != 0 && Signal.AckSignal.StartRollSignal.GetValue() != 0)
                    {
                        Signal.RequestController.StartRollRequest.SetValue(0);
                    }
            }
            catch (Exception ex)
            {
                _LoggerService.WriteExceptionLog(MethodBase.GetCurrentMethod(), $"{ex.Message}\n{ex.StackTrace}");
            }

        }

        

        private void CheckReqAckStatus(ref bool reportedFlag, NoticeReleaseEventHandler eventHandler, Word request, Word ack)
        {
            if (request.GetValue() == 1 && ack.GetValue() == 1)
            {
                if (reportedFlag == false)
                {
                    reportedFlag = true;
                    var args = new ReqAckEventArgs(Signal.BufferIndex);
                    eventHandler?.Invoke(this, args);
                }

                AckSignal(request, ack);
            }
            else if (request.GetValue() == 0 && ack.GetValue() == 0)
            {
                reportedFlag = false;
            }
        }

        private void AckSignal(Word req, Word ack)
        {
            if (ack.GetValue() > 0)
            {
                req.SetValue(0);
            }
        }
    }
}
