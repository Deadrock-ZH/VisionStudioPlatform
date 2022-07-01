namespace VisionStudio
{
    partial class ImageSaveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageSaveForm));
            this.rad_All = new System.Windows.Forms.RadioButton();
            this.rad_NG = new System.Windows.Forms.RadioButton();
            this.rad_OK = new System.Windows.Forms.RadioButton();
            this.rad_NO = new System.Windows.Forms.RadioButton();
            this.图片保存 = new System.Windows.Forms.GroupBox();
            this.num_SaveDay = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.图片保存.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_SaveDay)).BeginInit();
            this.SuspendLayout();
            // 
            // rad_All
            // 
            this.rad_All.AutoSize = true;
            this.rad_All.Location = new System.Drawing.Point(19, 25);
            this.rad_All.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rad_All.Name = "rad_All";
            this.rad_All.Size = new System.Drawing.Size(40, 21);
            this.rad_All.TabIndex = 0;
            this.rad_All.TabStop = true;
            this.rad_All.Text = "All";
            this.rad_All.UseVisualStyleBackColor = true;
            this.rad_All.CheckedChanged += new System.EventHandler(this.rad_All_CheckedChanged);
            // 
            // rad_NG
            // 
            this.rad_NG.AutoSize = true;
            this.rad_NG.Location = new System.Drawing.Point(195, 25);
            this.rad_NG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rad_NG.Name = "rad_NG";
            this.rad_NG.Size = new System.Drawing.Size(45, 21);
            this.rad_NG.TabIndex = 0;
            this.rad_NG.TabStop = true;
            this.rad_NG.Text = "NG";
            this.rad_NG.UseVisualStyleBackColor = true;
            this.rad_NG.CheckedChanged += new System.EventHandler(this.rad_NG_CheckedChanged);
            // 
            // rad_OK
            // 
            this.rad_OK.AutoSize = true;
            this.rad_OK.Location = new System.Drawing.Point(104, 25);
            this.rad_OK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rad_OK.Name = "rad_OK";
            this.rad_OK.Size = new System.Drawing.Size(44, 21);
            this.rad_OK.TabIndex = 0;
            this.rad_OK.TabStop = true;
            this.rad_OK.Text = "OK";
            this.rad_OK.UseVisualStyleBackColor = true;
            this.rad_OK.CheckedChanged += new System.EventHandler(this.rad_OK_CheckedChanged);
            // 
            // rad_NO
            // 
            this.rad_NO.AutoSize = true;
            this.rad_NO.Location = new System.Drawing.Point(294, 25);
            this.rad_NO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rad_NO.Name = "rad_NO";
            this.rad_NO.Size = new System.Drawing.Size(46, 21);
            this.rad_NO.TabIndex = 0;
            this.rad_NO.TabStop = true;
            this.rad_NO.Text = "NO";
            this.rad_NO.UseVisualStyleBackColor = true;
            this.rad_NO.CheckedChanged += new System.EventHandler(this.rad_NO_CheckedChanged);
            // 
            // 图片保存
            // 
            this.图片保存.Controls.Add(this.num_SaveDay);
            this.图片保存.Controls.Add(this.label2);
            this.图片保存.Controls.Add(this.label1);
            this.图片保存.Controls.Add(this.rad_All);
            this.图片保存.Controls.Add(this.rad_NO);
            this.图片保存.Controls.Add(this.rad_NG);
            this.图片保存.Controls.Add(this.rad_OK);
            this.图片保存.Dock = System.Windows.Forms.DockStyle.Fill;
            this.图片保存.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.图片保存.Location = new System.Drawing.Point(0, 0);
            this.图片保存.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.图片保存.Name = "图片保存";
            this.图片保存.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.图片保存.Size = new System.Drawing.Size(390, 102);
            this.图片保存.TabIndex = 1;
            this.图片保存.TabStop = false;
            this.图片保存.Text = "图片保存设置";
            // 
            // num_SaveDay
            // 
            this.num_SaveDay.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.num_SaveDay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num_SaveDay.Location = new System.Drawing.Point(85, 54);
            this.num_SaveDay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.num_SaveDay.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.num_SaveDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_SaveDay.Name = "num_SaveDay";
            this.num_SaveDay.Size = new System.Drawing.Size(227, 23);
            this.num_SaveDay.TabIndex = 2;
            this.num_SaveDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_SaveDay.ValueChanged += new System.EventHandler(this.num_SaveDay_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(320, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "天";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "保存最近";
            // 
            // ImageSaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(390, 102);
            this.Controls.Add(this.图片保存);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ImageSaveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageSaveForm";
            this.Load += new System.EventHandler(this.ImageSaveForm_Load);
            this.图片保存.ResumeLayout(false);
            this.图片保存.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_SaveDay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rad_All;
        private System.Windows.Forms.RadioButton rad_NG;
        private System.Windows.Forms.RadioButton rad_OK;
        private System.Windows.Forms.RadioButton rad_NO;
        private System.Windows.Forms.GroupBox 图片保存;
        private System.Windows.Forms.NumericUpDown num_SaveDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}