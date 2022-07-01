namespace SvMask
{
    /// <summary>
    /// 本类是掩模界面
    /// </summary>
    partial class MaskForm
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
            this.tsp_all = new System.Windows.Forms.ToolStrip();
            this.tsp_Rect2 = new System.Windows.Forms.ToolStripButton();
            this.tsp_Circle = new System.Windows.Forms.ToolStripButton();
            this.tsp_polygon = new System.Windows.Forms.ToolStripButton();
            this.tsp_OK = new System.Windows.Forms.ToolStripButton();
            this.tsp_Complete = new System.Windows.Forms.ToolStripButton();
            this.tsp_remove = new System.Windows.Forms.ToolStripButton();
            this.pnl_Disp = new System.Windows.Forms.Panel();
            this.pnl_Window = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_disp = new System.Windows.Forms.ComboBox();
            this.tsp_all.SuspendLayout();
            this.pnl_Disp.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsp_all
            // 
            this.tsp_all.AutoSize = false;
            this.tsp_all.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsp_Rect2,
            this.tsp_Circle,
            this.tsp_polygon,
            this.tsp_OK,
            this.tsp_Complete,
            this.tsp_remove});
            this.tsp_all.Location = new System.Drawing.Point(0, 0);
            this.tsp_all.Name = "tsp_all";
            this.tsp_all.Size = new System.Drawing.Size(517, 41);
            this.tsp_all.TabIndex = 0;
            this.tsp_all.Text = "toolStrip1";
            // 
            // tsp_Rect2
            // 
            this.tsp_Rect2.Image = global::SvMask.Properties.Resources.矩形;
            this.tsp_Rect2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_Rect2.Name = "tsp_Rect2";
            this.tsp_Rect2.Size = new System.Drawing.Size(52, 38);
            this.tsp_Rect2.Text = "矩形";
            this.tsp_Rect2.ToolTipText = "带方向的矩形";
            this.tsp_Rect2.Click += new System.EventHandler(this.tsp_Rect2_Click);
            // 
            // tsp_Circle
            // 
            this.tsp_Circle.Image = global::SvMask.Properties.Resources.圆;
            this.tsp_Circle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_Circle.Name = "tsp_Circle";
            this.tsp_Circle.Size = new System.Drawing.Size(40, 38);
            this.tsp_Circle.Text = "圆";
            this.tsp_Circle.Click += new System.EventHandler(this.tsp_Circle_Click);
            // 
            // tsp_polygon
            // 
            this.tsp_polygon.Image = global::SvMask.Properties.Resources.多边形;
            this.tsp_polygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_polygon.Name = "tsp_polygon";
            this.tsp_polygon.Size = new System.Drawing.Size(64, 38);
            this.tsp_polygon.Text = "多边形";
            this.tsp_polygon.ToolTipText = "点击完成按钮完成多边形绘制，点的个数必须大于4";
            this.tsp_polygon.Click += new System.EventHandler(this.tsp_polygon_Click);
            // 
            // tsp_OK
            // 
            this.tsp_OK.Image = global::SvMask.Properties.Resources.确定;
            this.tsp_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_OK.Name = "tsp_OK";
            this.tsp_OK.Size = new System.Drawing.Size(52, 38);
            this.tsp_OK.Text = "完成";
            this.tsp_OK.Click += new System.EventHandler(this.tsp_OK_Click);
            // 
            // tsp_Complete
            // 
            this.tsp_Complete.Image = global::SvMask.Properties.Resources.play;
            this.tsp_Complete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_Complete.Name = "tsp_Complete";
            this.tsp_Complete.Size = new System.Drawing.Size(52, 38);
            this.tsp_Complete.Text = "确定";
            this.tsp_Complete.Click += new System.EventHandler(this.tsp_Complete_Click);
            // 
            // tsp_remove
            // 
            this.tsp_remove.Image = global::SvMask.Properties.Resources.删除;
            this.tsp_remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_remove.Name = "tsp_remove";
            this.tsp_remove.RightToLeftAutoMirrorImage = true;
            this.tsp_remove.Size = new System.Drawing.Size(52, 38);
            this.tsp_remove.Text = "清空";
            this.tsp_remove.ToolTipText = "清空ROI";
            this.tsp_remove.Click += new System.EventHandler(this.tsp_remove_Click);
            // 
            // pnl_Disp
            // 
            this.pnl_Disp.Controls.Add(this.pnl_Window);
            this.pnl_Disp.Controls.Add(this.panel1);
            this.pnl_Disp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Disp.Location = new System.Drawing.Point(0, 41);
            this.pnl_Disp.Name = "pnl_Disp";
            this.pnl_Disp.Size = new System.Drawing.Size(517, 350);
            this.pnl_Disp.TabIndex = 2;
            // 
            // pnl_Window
            // 
            this.pnl_Window.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Window.Location = new System.Drawing.Point(0, 26);
            this.pnl_Window.Name = "pnl_Window";
            this.pnl_Window.Size = new System.Drawing.Size(517, 324);
            this.pnl_Window.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmb_disp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 26);
            this.panel1.TabIndex = 0;
            // 
            // cmb_disp
            // 
            this.cmb_disp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_disp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_disp.FormattingEnabled = true;
            this.cmb_disp.Items.AddRange(new object[] {
            "输入窗口",
            "输出窗口"});
            this.cmb_disp.Location = new System.Drawing.Point(0, 0);
            this.cmb_disp.Name = "cmb_disp";
            this.cmb_disp.Size = new System.Drawing.Size(517, 20);
            this.cmb_disp.TabIndex = 0;
            this.cmb_disp.SelectedIndexChanged += new System.EventHandler(this.cmb_disp_SelectedIndexChanged);
            // 
            // MaskForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 391);
            this.Controls.Add(this.pnl_Disp);
            this.Controls.Add(this.tsp_all);
            this.Name = "MaskForm";
            this.Text = "MaskForm1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MaskForm_FormClosing);
            this.Load += new System.EventHandler(this.MaskForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            this.tsp_all.ResumeLayout(false);
            this.tsp_all.PerformLayout();
            this.pnl_Disp.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsp_all;
        private System.Windows.Forms.ToolStripButton tsp_Rect2;
        private System.Windows.Forms.ToolStripButton tsp_Circle;
        private System.Windows.Forms.ToolStripButton tsp_polygon;
        private System.Windows.Forms.ToolStripButton tsp_OK;
        private System.Windows.Forms.Panel pnl_Disp;
        private System.Windows.Forms.ToolStripButton tsp_Complete;
        private System.Windows.Forms.Panel pnl_Window;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmb_disp;
        private System.Windows.Forms.ToolStripButton tsp_remove;
    }
}