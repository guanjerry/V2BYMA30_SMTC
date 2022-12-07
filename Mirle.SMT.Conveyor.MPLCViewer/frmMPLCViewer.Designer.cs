namespace Mirle.SMTC.Conveyor.MPLCViewer
{
    partial class frmMPLCViewer
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMPLCViewer));
            this.timerRawCaching = new System.Windows.Forms.Timer(this.components);
            this.tsslFilePath = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbCaching = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslCache = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTotalRow = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCurrentRow = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCurrentRowTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstRawData = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tlp2Main = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dudInterval = new System.Windows.Forms.DomainUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.butStart = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.butLoadFiles = new System.Windows.Forms.Button();
            this.MonitorPanel = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timerPlayer = new System.Windows.Forms.Timer(this.components);
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.tlp2Main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerRawCaching
            // 
            this.timerRawCaching.Interval = 200;
            this.timerRawCaching.Tick += new System.EventHandler(this.timerRawCaching_Tick);
            // 
            // tsslFilePath
            // 
            this.tsslFilePath.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslFilePath.Name = "tsslFilePath";
            this.tsslFilePath.Size = new System.Drawing.Size(75, 16);
            this.tsslFilePath.Text = "FilePath: ";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(13, 16);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // tspbCaching
            // 
            this.tspbCaching.Name = "tspbCaching";
            this.tspbCaching.Size = new System.Drawing.Size(133, 14);
            // 
            // tsslCache
            // 
            this.tsslCache.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslCache.Name = "tsslCache";
            this.tsslCache.Size = new System.Drawing.Size(130, 16);
            this.tsslCache.Text = "RawData Cached:";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(13, 16);
            this.toolStripStatusLabel3.Text = "|";
            // 
            // tsslTotalRow
            // 
            this.tsslTotalRow.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslTotalRow.Name = "tsslTotalRow";
            this.tsslTotalRow.Size = new System.Drawing.Size(95, 16);
            this.tsslTotalRow.Text = "Total Row: 0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(13, 16);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // tsslCurrentRow
            // 
            this.tsslCurrentRow.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslCurrentRow.Name = "tsslCurrentRow";
            this.tsslCurrentRow.Size = new System.Drawing.Size(58, 16);
            this.tsslCurrentRow.Text = "Row: 0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 16);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // tsslCurrentRowTime
            // 
            this.tsslCurrentRowTime.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslCurrentRowTime.Name = "tsslCurrentRowTime";
            this.tsslCurrentRowTime.Size = new System.Drawing.Size(227, 16);
            this.tsslCurrentRowTime.Text = "Current Time : 00:00:00.00000";
            // 
            // lstRawData
            // 
            this.lstRawData.AllowDrop = true;
            this.lstRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRawData.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lstRawData.FormattingEnabled = true;
            this.lstRawData.ItemHeight = 25;
            this.lstRawData.Location = new System.Drawing.Point(1422, 4);
            this.lstRawData.Margin = new System.Windows.Forms.Padding(4);
            this.lstRawData.Name = "lstRawData";
            this.lstRawData.Size = new System.Drawing.Size(259, 796);
            this.lstRawData.TabIndex = 32;
            this.lstRawData.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstRawData_DragDrop);
            this.lstRawData.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstRawData_DragEnter);
            // 
            // statusStrip1
            // 
            this.tlp2Main.SetColumnSpan(this.statusStrip1, 2);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCurrentRowTime,
            this.toolStripStatusLabel1,
            this.tsslCurrentRow,
            this.toolStripStatusLabel2,
            this.tsslTotalRow,
            this.toolStripStatusLabel3,
            this.tsslCache,
            this.tspbCaching,
            this.toolStripStatusLabel4,
            this.tsslFilePath});
            this.statusStrip1.Location = new System.Drawing.Point(0, 804);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1685, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 33;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tlp2Main
            // 
            this.tlp2Main.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tlp2Main.ColumnCount = 2;
            this.tlp2Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp2Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tlp2Main.Controls.Add(this.lstRawData, 1, 0);
            this.tlp2Main.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlp2Main.Controls.Add(this.statusStrip1, 0, 1);
            this.tlp2Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp2Main.Location = new System.Drawing.Point(0, 0);
            this.tlp2Main.Margin = new System.Windows.Forms.Padding(4);
            this.tlp2Main.Name = "tlp2Main";
            this.tlp2Main.RowCount = 2;
            this.tlp2Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp2Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlp2Main.Size = new System.Drawing.Size(1685, 826);
            this.tlp2Main.TabIndex = 70;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.MonitorPanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.90892F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.09108F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1410, 796);
            this.tableLayoutPanel1.TabIndex = 31;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.dudInterval, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.label9, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1410, 47);
            this.tableLayoutPanel5.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.butStart);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.butLoadFiles);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1148, 39);
            this.panel1.TabIndex = 0;
            // 
            // dudInterval
            // 
            this.dudInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dudInterval.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dudInterval.Items.Add("1000");
            this.dudInterval.Items.Add("500");
            this.dudInterval.Items.Add("100");
            this.dudInterval.Items.Add("50");
            this.dudInterval.Items.Add("15");
            this.dudInterval.Items.Add("10");
            this.dudInterval.Location = new System.Drawing.Point(1272, 6);
            this.dudInterval.Margin = new System.Windows.Forms.Padding(4);
            this.dudInterval.Name = "dudInterval";
            this.dudInterval.ReadOnly = true;
            this.dudInterval.Size = new System.Drawing.Size(134, 34);
            this.dudInterval.TabIndex = 439;
            this.dudInterval.Text = "1000";
            this.dudInterval.Wrap = true;
            this.dudInterval.SelectedItemChanged += new System.EventHandler(this.dudInterval_SelectedItemChanged);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(277, 4);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(680, 38);
            this.textBox1.TabIndex = 448;
            // 
            // butStart
            // 
            this.butStart.Image = ((System.Drawing.Image)(resources.GetObject("butStart.Image")));
            this.butStart.Location = new System.Drawing.Point(965, 1);
            this.butStart.Margin = new System.Windows.Forms.Padding(4);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(52, 41);
            this.butStart.TabIndex = 444;
            this.butStart.Tag = "Start";
            this.butStart.UseVisualStyleBackColor = true;
            this.butStart.Click += new System.EventHandler(this.butStart_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1160, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 47);
            this.label9.TabIndex = 441;
            this.label9.Text = "Interval:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(177, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 26);
            this.label10.TabIndex = 442;
            this.label10.Text = "FilePath:";
            // 
            // butLoadFiles
            // 
            this.butLoadFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.butLoadFiles.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butLoadFiles.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butLoadFiles.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.butLoadFiles.Location = new System.Drawing.Point(4, 2);
            this.butLoadFiles.Margin = new System.Windows.Forms.Padding(4);
            this.butLoadFiles.Name = "butLoadFiles";
            this.butLoadFiles.Size = new System.Drawing.Size(165, 38);
            this.butLoadFiles.TabIndex = 438;
            this.butLoadFiles.Text = "Load PLC Log";
            this.butLoadFiles.UseVisualStyleBackColor = false;
            this.butLoadFiles.Click += new System.EventHandler(this.butLoadFiles_Click);
            // 
            // MonitorPanel
            // 
            this.MonitorPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.MonitorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MonitorPanel.Location = new System.Drawing.Point(4, 51);
            this.MonitorPanel.Margin = new System.Windows.Forms.Padding(4);
            this.MonitorPanel.Name = "MonitorPanel";
            this.MonitorPanel.Size = new System.Drawing.Size(1402, 741);
            this.MonitorPanel.TabIndex = 32;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "play_pause_resume.ico");
            this.imageList1.Images.SetKeyName(1, "pause.ico");
            // 
            // timerPlayer
            // 
            this.timerPlayer.Interval = 1000;
            this.timerPlayer.Tick += new System.EventHandler(this.timerPlayer_Tick);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // frmMPLCViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1685, 826);
            this.Controls.Add(this.tlp2Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMPLCViewer";
            this.Text = "OSMTC MPLCViewer";
            this.Load += new System.EventHandler(this.frmMPLCViewer_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tlp2Main.ResumeLayout(false);
            this.tlp2Main.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerRawCaching;
        private System.Windows.Forms.ToolStripStatusLabel tsslFilePath;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripProgressBar tspbCaching;
        private System.Windows.Forms.ToolStripStatusLabel tsslCache;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tsslTotalRow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentRow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentRowTime;
        private System.Windows.Forms.ListBox lstRawData;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tlp2Main;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timerPlayer;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel MonitorPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DomainUpDown dudInterval;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button butStart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button butLoadFiles;
    }
}

