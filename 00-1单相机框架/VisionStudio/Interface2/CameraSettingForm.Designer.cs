namespace VisionStudio
{
    partial class CameraSettingForm
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
            this.triggermode_comboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ccd_index_comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CCD_ExposeTime = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.CCD_Gain = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.CCD_ExposeTime)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CCD_Gain)).BeginInit();
            this.SuspendLayout();
            // 
            // triggermode_comboBox
            // 
            this.triggermode_comboBox.FormattingEnabled = true;
            this.triggermode_comboBox.Items.AddRange(new object[] {
            "硬触发",
            "软触发"});
            this.triggermode_comboBox.Location = new System.Drawing.Point(99, 37);
            this.triggermode_comboBox.Name = "triggermode_comboBox";
            this.triggermode_comboBox.Size = new System.Drawing.Size(62, 20);
            this.triggermode_comboBox.TabIndex = 3;
            this.triggermode_comboBox.SelectedIndexChanged += new System.EventHandler(this.triggermode_comboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "相机触发模式";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "相机编号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ccd_index_comboBox
            // 
            this.ccd_index_comboBox.FormattingEnabled = true;
            this.ccd_index_comboBox.Location = new System.Drawing.Point(62, 3);
            this.ccd_index_comboBox.Name = "ccd_index_comboBox";
            this.ccd_index_comboBox.Size = new System.Drawing.Size(62, 20);
            this.ccd_index_comboBox.TabIndex = 6;
            this.ccd_index_comboBox.SelectedIndexChanged += new System.EventHandler(this.ccd_index_comboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "增益";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "曝光时间";
            // 
            // CCD_ExposeTime
            // 
            this.CCD_ExposeTime.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.CCD_ExposeTime.Location = new System.Drawing.Point(189, 3);
            this.CCD_ExposeTime.Maximum = new decimal(new int[] {
            2500000,
            0,
            0,
            0});
            this.CCD_ExposeTime.Minimum = new decimal(new int[] {
            46,
            0,
            0,
            0});
            this.CCD_ExposeTime.Name = "CCD_ExposeTime";
            this.CCD_ExposeTime.Size = new System.Drawing.Size(56, 21);
            this.CCD_ExposeTime.TabIndex = 9;
            this.CCD_ExposeTime.Value = new decimal(new int[] {
            46,
            0,
            0,
            0});
            this.CCD_ExposeTime.ValueChanged += new System.EventHandler(this.CCD_ExposeTime_ValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.ccd_index_comboBox);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.CCD_ExposeTime);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.CCD_Gain);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(14, 82);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(377, 35);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // CCD_Gain
            // 
            this.CCD_Gain.DecimalPlaces = 1;
            this.CCD_Gain.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.CCD_Gain.Location = new System.Drawing.Point(286, 3);
            this.CCD_Gain.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.CCD_Gain.Name = "CCD_Gain";
            this.CCD_Gain.Size = new System.Drawing.Size(56, 21);
            this.CCD_Gain.TabIndex = 11;
            this.CCD_Gain.ValueChanged += new System.EventHandler(this.CCD_Gain_ValueChanged);
            // 
            // CameraSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 315);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.triggermode_comboBox);
            this.Name = "CameraSettingForm";
            this.Text = "相机设置";
            this.Load += new System.EventHandler(this.CameraSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CCD_ExposeTime)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CCD_Gain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox triggermode_comboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ccd_index_comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown CCD_ExposeTime;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.NumericUpDown CCD_Gain;
    }
}