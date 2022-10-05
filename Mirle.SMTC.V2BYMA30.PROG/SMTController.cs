using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Mirle.Def;
using Mirle.SMTCV;
using Mirle.SMTCVStart;
using Mirle.WebAPI.Event;
using Unity;

namespace Mirle.SMTC.V2BYMA30.PROG
{
    public partial class SMTController : Form
    {
        private WebApiHost _webApiHost;
        private UnityContainer _unityContainer;


        #region 建構函數
        public SMTController()
        {
            InitializeComponent();
        }

        public void FunInit()
        {
            clsInitSys.subInitSystem();
            _unityContainer = new UnityContainer();
            _unityContainer.RegisterInstance(new WCSController());
            _webApiHost = new WebApiHost(new Startup(_unityContainer), "127.0.0.1:9000");
            //_Client.FunInitialIPC_Proc();
            clsSMTCVStart.FunInitalCVController(clsInitSys.CV_Config, clsInitSys.S800_Config);
            //_emptyBinLeaveCV_Proc.subStart();
            //_cvBinCheck_Proc.subStart();
            //_stkcCommand_Procs.subStart(1, _Client);
            //_binLeaveCV_Procs.subStart();
            //_cvReelStockInProc.subStart();
            //_writeCmdToCV_Proc.subStart();
            
            ChangeSubForm(clsSMTCVStart.GetMainView());
            clsWcsApi.FunInit(clsInitSys.WCSApi_Config);
        }

        public void FunEventInit()
        {

        }

        #endregion 建構函數

        private void SMTController_Load(object sender, EventArgs e)
        {
            ChkAppIsAlreadyRunning();
            Text = Text + "  v " + ProductVersion;
            FunInit();
            
            clsInitSys.FunWriTraceLog_CV("產線程式已開啟！");

            //timRead.Enabled = true;
            timer1.Enabled = true;
        }
        #region Event

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

        private void BTN_S800_Click(object sender, EventArgs e)
        {
            clsSMTCVStart.GetMainView_Object().SetCurController(6);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            try
            {
                lblTimer.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
    }
}
