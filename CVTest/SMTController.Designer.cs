namespace CVTest
{
    partial class SMTController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMTController));
            this.timer1 = new System.Windows.Forms.Timer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.picMirle = new System.Windows.Forms.PictureBox();
            this.BTN_S1 = new System.Windows.Forms.Button();
            this.BTN_S2 = new System.Windows.Forms.Button();
            this.BTN_S3 = new System.Windows.Forms.Button();
            this.BTN_S4 = new System.Windows.Forms.Button();
            this.BTN_S5 = new System.Windows.Forms.Button();
            this.BTN_S6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMirle)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1382, 723);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Controls.Add(this.lblTimer, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.picMirle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_S1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_S2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_S3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_S4, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_S5, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_S6, 7, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1382, 109);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblTimer
            // 
            this.lblTimer.BackColor = System.Drawing.SystemColors.Control;
            this.lblTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTimer.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.Color.Black;
            this.lblTimer.Location = new System.Drawing.Point(176, 0);
            this.lblTimer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(164, 109);
            this.lblTimer.TabIndex = 269;
            this.lblTimer.Text = "yyyy/MM/dd hh:mm:ss";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picMirle
            // 
            this.picMirle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMirle.Image = ((System.Drawing.Image)(resources.GetObject("picMirle.Image")));
            this.picMirle.Location = new System.Drawing.Point(4, 4);
            this.picMirle.Margin = new System.Windows.Forms.Padding(4);
            this.picMirle.Name = "picMirle";
            this.picMirle.Size = new System.Drawing.Size(164, 101);
            this.picMirle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMirle.TabIndex = 268;
            this.picMirle.TabStop = false;
            // 
            // BTN_S1
            // 
            this.BTN_S1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_S1.Font = new System.Drawing.Font("新細明體", 12F);
            this.BTN_S1.Location = new System.Drawing.Point(347, 3);
            this.BTN_S1.Name = "BTN_S1";
            this.BTN_S1.Size = new System.Drawing.Size(166, 103);
            this.BTN_S1.TabIndex = 0;
            this.BTN_S1.Text = "Front-1,2,3";
            this.BTN_S1.UseVisualStyleBackColor = true;
            this.BTN_S1.Click += new System.EventHandler(this.BTN_S1_Click);
            // 
            // BTN_S2
            // 
            this.BTN_S2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_S2.Font = new System.Drawing.Font("新細明體", 12F);
            this.BTN_S2.Location = new System.Drawing.Point(519, 3);
            this.BTN_S2.Name = "BTN_S2";
            this.BTN_S2.Size = new System.Drawing.Size(166, 103);
            this.BTN_S2.TabIndex = 0;
            this.BTN_S2.Text = "Back-1,2,3";
            this.BTN_S2.UseVisualStyleBackColor = true;
            this.BTN_S2.Click += new System.EventHandler(this.BTN_S2_Click);
            // 
            // BTN_S3
            // 
            this.BTN_S3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_S3.Font = new System.Drawing.Font("新細明體", 12F);
            this.BTN_S3.Location = new System.Drawing.Point(691, 3);
            this.BTN_S3.Name = "BTN_S3";
            this.BTN_S3.Size = new System.Drawing.Size(166, 103);
            this.BTN_S3.TabIndex = 0;
            this.BTN_S3.Text = "Front-4,5,6";
            this.BTN_S3.UseVisualStyleBackColor = true;
            this.BTN_S3.Click += new System.EventHandler(this.BTN_S3_Click);
            // 
            // BTN_S4
            // 
            this.BTN_S4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_S4.Font = new System.Drawing.Font("新細明體", 12F);
            this.BTN_S4.Location = new System.Drawing.Point(863, 3);
            this.BTN_S4.Name = "BTN_S4";
            this.BTN_S4.Size = new System.Drawing.Size(166, 103);
            this.BTN_S4.TabIndex = 0;
            this.BTN_S4.Text = "Back-4,5,6";
            this.BTN_S4.UseVisualStyleBackColor = true;
            this.BTN_S4.Click += new System.EventHandler(this.BTN_S4_Click);
            // 
            // BTN_S5
            // 
            this.BTN_S5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_S5.Font = new System.Drawing.Font("新細明體", 12F);
            this.BTN_S5.Location = new System.Drawing.Point(1035, 3);
            this.BTN_S5.Name = "BTN_S5";
            this.BTN_S5.Size = new System.Drawing.Size(166, 103);
            this.BTN_S5.TabIndex = 0;
            this.BTN_S5.Text = "Front-7";
            this.BTN_S5.UseVisualStyleBackColor = true;
            this.BTN_S5.Click += new System.EventHandler(this.BTN_S5_Click);
            // 
            // BTN_S6
            // 
            this.BTN_S6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_S6.Font = new System.Drawing.Font("新細明體", 12F);
            this.BTN_S6.Location = new System.Drawing.Point(1207, 3);
            this.BTN_S6.Name = "BTN_S6";
            this.BTN_S6.Size = new System.Drawing.Size(172, 103);
            this.BTN_S6.TabIndex = 0;
            this.BTN_S6.Text = "Back-7";
            this.BTN_S6.UseVisualStyleBackColor = true;
            this.BTN_S6.Click += new System.EventHandler(this.BTN_S6_Click);
            // 
            // SMTController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 723);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SMTController";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMirle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.PictureBox picMirle;
        private System.Windows.Forms.Button BTN_S1;
        private System.Windows.Forms.Button BTN_S2;
        private System.Windows.Forms.Button BTN_S3;
        private System.Windows.Forms.Button BTN_S4;
        private System.Windows.Forms.Button BTN_S5;
        private System.Windows.Forms.Button BTN_S6;
    }
}

