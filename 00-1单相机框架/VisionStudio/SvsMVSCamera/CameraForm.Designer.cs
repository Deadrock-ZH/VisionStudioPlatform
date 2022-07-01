namespace SvsMVSCamera
{
    partial class CameraForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("GigE");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("USB");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tv_Camera = new System.Windows.Forms.TreeView();
            this.设备 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pnl_Disp1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsp_Start = new System.Windows.Forms.ToolStripButton();
            this.tsp_OpenCamera = new System.Windows.Forms.ToolStripButton();
            this.tsp_Stop = new System.Windows.Forms.ToolStripButton();
            this.tsp_DispImage = new System.Windows.Forms.ToolStripButton();
            this.Pnl_Disp = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgv_File = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnl_param = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.num_Gamma = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.num_Gain = new System.Windows.Forms.NumericUpDown();
            this.num_Exposure = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmb_Select = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.Pnl_Disp.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_File)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.pnl_param.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Gamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Gain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Exposure)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 522);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tv_Camera);
            this.panel4.Controls.Add(this.设备);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(266, 346);
            this.panel4.TabIndex = 1;
            // 
            // tv_Camera
            // 
            this.tv_Camera.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tv_Camera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_Camera.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tv_Camera.ForeColor = System.Drawing.Color.Black;
            this.tv_Camera.Location = new System.Drawing.Point(0, 44);
            this.tv_Camera.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tv_Camera.Name = "tv_Camera";
            treeNode1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            treeNode1.ForeColor = System.Drawing.Color.Black;
            treeNode1.Name = "节点0";
            treeNode1.Text = "GigE";
            treeNode2.Name = "节点1";
            treeNode2.Text = "USB";
            this.tv_Camera.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.tv_Camera.Size = new System.Drawing.Size(266, 302);
            this.tv_Camera.TabIndex = 2;
            // 
            // 设备
            // 
            this.设备.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.设备.Dock = System.Windows.Forms.DockStyle.Top;
            this.设备.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.设备.Location = new System.Drawing.Point(0, 0);
            this.设备.Name = "设备";
            this.设备.Size = new System.Drawing.Size(266, 44);
            this.设备.TabIndex = 0;
            this.设备.Text = "设备信息";
            this.设备.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(0, 346);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(266, 176);
            this.panel3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.Pnl_Disp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(266, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(892, 522);
            this.panel2.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.pnl_Disp1);
            this.panel6.Controls.Add(this.toolStrip1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(633, 522);
            this.panel6.TabIndex = 1;
            // 
            // pnl_Disp1
            // 
            this.pnl_Disp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Disp1.Location = new System.Drawing.Point(0, 114);
            this.pnl_Disp1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnl_Disp1.Name = "pnl_Disp1";
            this.pnl_Disp1.Size = new System.Drawing.Size(633, 408);
            this.pnl_Disp1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsp_Start,
            this.tsp_OpenCamera,
            this.tsp_Stop,
            this.tsp_DispImage});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(633, 114);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsp_Start
            // 
            this.tsp_Start.Image = global::SvsMVSCamera.Properties.Resources.resizeApi730377JY开始;
            this.tsp_Start.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsp_Start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_Start.Name = "tsp_Start";
            this.tsp_Start.Size = new System.Drawing.Size(76, 111);
            this.tsp_Start.Text = "开始采集";
            this.tsp_Start.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsp_Start.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsp_Start.Click += new System.EventHandler(this.tsp_Start_Click);
            // 
            // tsp_OpenCamera
            // 
            this.tsp_OpenCamera.AutoSize = false;
            this.tsp_OpenCamera.Image = global::SvsMVSCamera.Properties.Resources.图像保存;
            this.tsp_OpenCamera.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsp_OpenCamera.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_OpenCamera.Name = "tsp_OpenCamera";
            this.tsp_OpenCamera.Size = new System.Drawing.Size(76, 114);
            this.tsp_OpenCamera.Text = "启用存图";
            this.tsp_OpenCamera.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsp_OpenCamera.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsp_OpenCamera.Click += new System.EventHandler(this.tsp_OpenCamera_Click);
            // 
            // tsp_Stop
            // 
            this.tsp_Stop.Image = global::SvsMVSCamera.Properties.Resources.resizeApiUCXJX1RY停止;
            this.tsp_Stop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsp_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_Stop.Name = "tsp_Stop";
            this.tsp_Stop.Size = new System.Drawing.Size(76, 111);
            this.tsp_Stop.Text = "  停止采集";
            this.tsp_Stop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsp_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsp_Stop.Click += new System.EventHandler(this.tsp_Stop_Click);
            // 
            // tsp_DispImage
            // 
            this.tsp_DispImage.Image = global::SvsMVSCamera.Properties.Resources.resizeApi21;
            this.tsp_DispImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsp_DispImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_DispImage.Name = "tsp_DispImage";
            this.tsp_DispImage.Size = new System.Drawing.Size(76, 111);
            this.tsp_DispImage.Text = "循环显示";
            this.tsp_DispImage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsp_DispImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsp_DispImage.Click += new System.EventHandler(this.tsp_DispImage_Click);
            // 
            // Pnl_Disp
            // 
            this.Pnl_Disp.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Pnl_Disp.Controls.Add(this.tabControl1);
            this.Pnl_Disp.Controls.Add(this.label1);
            this.Pnl_Disp.Dock = System.Windows.Forms.DockStyle.Right;
            this.Pnl_Disp.Location = new System.Drawing.Point(633, 0);
            this.Pnl_Disp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Pnl_Disp.Name = "Pnl_Disp";
            this.Pnl_Disp.Size = new System.Drawing.Size(259, 522);
            this.Pnl_Disp.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 44);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(259, 478);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabPage3.Controls.Add(this.dgv_File);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(251, 448);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "存储文件夹";
            // 
            // dgv_File
            // 
            this.dgv_File.AllowUserToAddRows = false;
            this.dgv_File.AllowUserToDeleteRows = false;
            this.dgv_File.AllowUserToResizeRows = false;
            this.dgv_File.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgv_File.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_File.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_File.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_File.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgv_File.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_File.EnableHeadersVisualStyles = false;
            this.dgv_File.Location = new System.Drawing.Point(3, 3);
            this.dgv_File.MultiSelect = false;
            this.dgv_File.Name = "dgv_File";
            this.dgv_File.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_File.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_File.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_File.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_File.RowTemplate.Height = 23;
            this.dgv_File.Size = new System.Drawing.Size(245, 442);
            this.dgv_File.TabIndex = 0;
            this.dgv_File.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_File_CellClick);
            this.dgv_File.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_File_CellContentClick);
            this.dgv_File.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_File_CellContentDoubleClick);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabPage1.Controls.Add(this.pnl_param);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(254, 375);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常用属性";
            // 
            // pnl_param
            // 
            this.pnl_param.Controls.Add(this.label2);
            this.pnl_param.Controls.Add(this.num_Gamma);
            this.pnl_param.Controls.Add(this.label3);
            this.pnl_param.Controls.Add(this.num_Gain);
            this.pnl_param.Controls.Add(this.num_Exposure);
            this.pnl_param.Controls.Add(this.label4);
            this.pnl_param.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_param.Location = new System.Drawing.Point(3, 4);
            this.pnl_param.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnl_param.Name = "pnl_param";
            this.pnl_param.Size = new System.Drawing.Size(248, 367);
            this.pnl_param.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "曝光：";
            // 
            // num_Gamma
            // 
            this.num_Gamma.Location = new System.Drawing.Point(66, 85);
            this.num_Gamma.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.num_Gamma.Name = "num_Gamma";
            this.num_Gamma.Size = new System.Drawing.Size(177, 23);
            this.num_Gamma.TabIndex = 1;
            this.num_Gamma.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "增益：";
            // 
            // num_Gain
            // 
            this.num_Gain.Location = new System.Drawing.Point(66, 47);
            this.num_Gain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.num_Gain.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num_Gain.Name = "num_Gain";
            this.num_Gain.Size = new System.Drawing.Size(177, 23);
            this.num_Gain.TabIndex = 1;
            this.num_Gain.ValueChanged += new System.EventHandler(this.num_Gain_ValueChanged);
            // 
            // num_Exposure
            // 
            this.num_Exposure.Location = new System.Drawing.Point(66, 8);
            this.num_Exposure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.num_Exposure.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.num_Exposure.Name = "num_Exposure";
            this.num_Exposure.Size = new System.Drawing.Size(177, 23);
            this.num_Exposure.TabIndex = 1;
            this.num_Exposure.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.num_Exposure.ValueChanged += new System.EventHandler(this.num_Exposure_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Gamma：";
            this.label4.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabPage2.Controls.Add(this.cmb_Select);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(254, 375);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "采集设置";
            // 
            // cmb_Select
            // 
            this.cmb_Select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Select.FormattingEnabled = true;
            this.cmb_Select.Items.AddRange(new object[] {
            "200",
            "400",
            "700"});
            this.cmb_Select.Location = new System.Drawing.Point(73, 11);
            this.cmb_Select.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmb_Select.Name = "cmb_Select";
            this.cmb_Select.Size = new System.Drawing.Size(173, 25);
            this.cmb_Select.TabIndex = 2;
            this.cmb_Select.SelectedIndexChanged += new System.EventHandler(this.cmb_Select_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "帧率选择：";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "属性设置";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "文件夹名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "图片个数";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 522);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CameraForm";
            this.Text = "CameraForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraForm_FormClosing);
            this.Load += new System.EventHandler(this.CameraForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Pnl_Disp.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_File)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.pnl_param.ResumeLayout(false);
            this.pnl_param.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Gamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Gain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Exposure)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TreeView tv_Camera;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label 设备;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel pnl_Disp1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsp_OpenCamera;
        private System.Windows.Forms.ToolStripButton tsp_Start;
        private System.Windows.Forms.Panel Pnl_Disp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown num_Gamma;
        private System.Windows.Forms.NumericUpDown num_Gain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown num_Exposure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton tsp_Stop;
        private System.Windows.Forms.Panel pnl_param;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cmb_Select;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton tsp_DispImage;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgv_File;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}