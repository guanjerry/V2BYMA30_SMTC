using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Mirle.Def;
using Mirle.SMTCV.Conveyor.V2BYMA30.View;
using Mirle.SMTCVStart;

namespace CVTest
{
    public partial class SMTController : Form
    {
        //箱子離開
        //空箱ready寫20000up命令
        //箱子進來
        //Mag離開
        //Mag進來
        //Mag掃描
        public clsBinLeaveCV_Proc _binLeaveCV_Procs = new clsBinLeaveCV_Proc();
        private static System.Timers.Timer timRead = new System.Timers.Timer();
        
        #region 建構函數
        public SMTController()
        {
            InitializeComponent();
            timRead.Elapsed += new System.Timers.ElapsedEventHandler(timRead_Elapsed);
            timRead.Enabled = false; timRead.Interval = 100;
        }

        public void FunInit()
        {
            clsInitSys.subInitSystem();
            //_Client.FunInitialIPC_Proc();
            clsSMTCVStart.FunInitalCVController(clsInitSys.CV_Config);
            //_stkcCommand_Procs.subStart(1, _Client);
            //_binLeaveCV_Procs.subStart();
            //_cvReelStockInProc.subStart();
            //_writeCmdToCV_Proc.subStart();
            ChangeSubForm(clsSMTCVStart.GetMainView());
        }

        public void FunEventInit()
        {

        }

        #endregion 建構函數
        #region Event
        private void Form1_Load(object sender, EventArgs e)
        {
            ChkAppIsAlreadyRunning();
            Text = Text + "  v " + ProductVersion;
            FunInit();
            FunEventInit();
            //ChangeSubForm(_bufferPlcInfoView);
            clsInitSys.FunWriTraceLog_CV("產線程式已開啟！");

            timRead.Enabled = true;
            timer1.Enabled = true;
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void timRead_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            timRead.Enabled = false;
            try
            {

                //_Client.FunSendHeartBeat_ID_011();
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsInitSys.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
            finally
            {
                timRead.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            try
            {
                
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsInitSys.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
            finally
            {
                timer1.Enabled = true;
            }
        }
        #endregion Event


        /// <summary>
        /// 檢查程式是否重複開啟
        /// </summary>
        public void ChkAppIsAlreadyRunning()
        {
            try
            {
                string aFormName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;
                string aProcName = System.IO.Path.GetFileNameWithoutExtension(aFormName);
                if (System.Diagnostics.Process.GetProcessesByName(aProcName).Length > 1)
                {
                    MessageBox.Show("程式已開啟", "Communication System", MessageBoxButtons.OK);
                    //Application.Exit();
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsInitSys.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                Environment.Exit(0);
            }
        }

        private void ChangeSubForm(Form subForm)
        {
            try
            {
                var children = splitContainer1.Panel2.Controls;
                foreach (Control c in children)
                {
                    if (c is Form)
                    {
                        var thisChild = c as Form;
                        //thisChild.Hide();
                        splitContainer1.Panel2.Controls.Remove(thisChild);
                        thisChild.Width = 0;
                    }
                }

                if (subForm != null)
                {
                    subForm.TopLevel = false;
                    subForm.Dock = DockStyle.Fill;//適應窗體大小
                    subForm.FormBorderStyle = FormBorderStyle.None;//隱藏右上角的按鈕
                    subForm.Parent = splitContainer1.Panel2;
                    splitContainer1.Panel2.Controls.Add(subForm);
                    subForm.Show();
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsInitSys.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
        }

        private void BTN_S1_Click(object sender, EventArgs e)
        {
            clsSMTCVStart.GetMainView_Object().SetCurController(0);
        }

        private void BTN_S2_Click(object sender, EventArgs e)
        {
            clsSMTCVStart.GetMainView_Object().SetCurController(1);
        }

        private void BTN_S3_Click(object sender, EventArgs e)
        {
            clsSMTCVStart.GetMainView_Object().SetCurController(2);
        }

        private void BTN_S4_Click(object sender, EventArgs e)
        {
            clsSMTCVStart.GetMainView_Object().SetCurController(3);
        }

        private void BTN_S5_Click(object sender, EventArgs e)
        {
            clsSMTCVStart.GetMainView_Object().SetCurController(4);
        }

        private void BTN_S6_Click(object sender, EventArgs e)
        {
            clsSMTCVStart.GetMainView_Object().SetCurController(5);
        }
    }
}
