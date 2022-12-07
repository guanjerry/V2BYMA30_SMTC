using System;
using System.Windows.Forms;
using Mirle.MPLC;

namespace Mirle.SMTCV.Conveyor.Controller.MPLCView
{
    public partial class MonitorMainForm : Form
    {
        private BufferPlcInfoView frmBufferInfo;
        private MonitorLayout frmLayout;
        private CVCManager_8F _CVCManager;
        private CVCManager_S800 _CVCManager_S800;
        private int curController = 1;

        public MonitorMainForm(IMPLCProvider mplc)
        {
            InitializeComponent();
            _CVCManager = new CVCManager_8F(mplc);
            _CVCManager_S800 = new CVCManager_S800(mplc);
            frmLayout = new MonitorLayout(_CVCManager, _CVCManager_S800, curController);
            frmBufferInfo = new BufferPlcInfoView(_CVCManager, _CVCManager_S800, curController);
            cbo_Initial(6);
        }
        private void cbo_Initial(int PLCnumber)
        {
            for(int i = 1; i <= PLCnumber + 1; i++)
            {
                if (i == 5 || i == 6)
                    continue;
                else
                    cboPLCNo.Items.Add(i.ToString());
            }
        }

        private void ShowChildForm(Form child)
        {
            this.splitContainer1.Panel2.Controls.Clear();
            child.TopLevel = false;
            child.Dock = DockStyle.Fill;//適應窗體大小
            child.FormBorderStyle = FormBorderStyle.None;//隱藏右上角的按鈕
            child.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(child);
            child.Show();
        }
        private void MonitorMainForm_Load(object sender, EventArgs e)
        {
            ShowChildForm(frmLayout);
            this.RefreshTimer.Enabled = true;
        }
        private void btnLayout_Click(object sender, EventArgs e)
        {
            ShowChildForm(frmLayout);
        }

        private void btnBPI_Click(object sender, EventArgs e)
        {
            ShowChildForm(frmBufferInfo);
        }
        public void SetCurController(int curC)
        {
            curController = curC;
            frmLayout.SetCurController(curC);
            frmBufferInfo.SetCurController(curC);
        }
        

        private void cboPLCNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCurController(Convert.ToInt32(cboPLCNo.SelectedItem) - 1);
        }
    }
}
