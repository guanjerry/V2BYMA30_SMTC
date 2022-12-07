namespace Mirle.SMTCV.Conveyor.Controller.MPLCView
{
    partial class MonitorMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLayout = new System.Windows.Forms.Button();
            this.btnBPI = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPLCNo = new System.Windows.Forms.ComboBox();
            this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer1.Size = new System.Drawing.Size(955, 546);
            this.splitContainer1.SplitterDistance = 42;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.tableLayoutPanel1.Controls.Add(this.btnLayout, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBPI, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboPLCNo, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(955, 42);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnLayout
            // 
            this.btnLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.btnLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLayout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLayout.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayout.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnLayout.Location = new System.Drawing.Point(3, 3);
            this.btnLayout.Name = "btnLayout";
            this.btnLayout.Size = new System.Drawing.Size(165, 36);
            this.btnLayout.TabIndex = 0;
            this.btnLayout.Text = "Layout";
            this.btnLayout.UseVisualStyleBackColor = false;
            this.btnLayout.Click += new System.EventHandler(this.btnLayout_Click);
            // 
            // btnBPI
            // 
            this.btnBPI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.btnBPI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBPI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBPI.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBPI.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBPI.Location = new System.Drawing.Point(174, 3);
            this.btnBPI.Name = "btnBPI";
            this.btnBPI.Size = new System.Drawing.Size(165, 36);
            this.btnBPI.TabIndex = 1;
            this.btnBPI.Text = "Buffer PLC Info";
            this.btnBPI.UseVisualStyleBackColor = false;
            this.btnBPI.Click += new System.EventHandler(this.btnBPI_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(345, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 42);
            this.label1.TabIndex = 2;
            this.label1.Text = "PLC No.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPLCNo
            // 
            this.cboPLCNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPLCNo.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPLCNo.FormattingEnabled = true;
            this.cboPLCNo.Location = new System.Drawing.Point(440, 7);
            this.cboPLCNo.Name = "cboPLCNo";
            this.cboPLCNo.Size = new System.Drawing.Size(70, 27);
            this.cboPLCNo.TabIndex = 3;
            this.cboPLCNo.SelectedIndexChanged += new System.EventHandler(this.cboPLCNo_SelectedIndexChanged);
            // 
            // RefreshTimer
            // 
            this.RefreshTimer.Interval = 200;
            // 
            // MonitorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(955, 546);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MonitorMainForm";
            this.Text = "MonitorMainForm";
            this.Load += new System.EventHandler(this.MonitorMainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnLayout;
        private System.Windows.Forms.Button btnBPI;
        private System.Windows.Forms.Timer RefreshTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPLCNo;
    }
}