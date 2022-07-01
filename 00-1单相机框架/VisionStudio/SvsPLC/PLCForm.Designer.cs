namespace SvsPLC
{
    partial class PLCForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLCForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.paraPLCip = new System.Windows.Forms.TextBox();
            this.paraPLCport = new System.Windows.Forms.NumericUpDown();
            this.groupBox_Brand = new System.Windows.Forms.GroupBox();
            this.radioButton_欧姆龙 = new System.Windows.Forms.RadioButton();
            this.radioButton_三菱 = new System.Windows.Forms.RadioButton();
            this.radioButton_基恩士 = new System.Windows.Forms.RadioButton();
            this.groupBox_IP = new System.Windows.Forms.GroupBox();
            this.paraPCip = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.panel_right = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Camera_dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button减少工位 = new System.Windows.Forms.Button();
            this.button添加工位 = new System.Windows.Forms.Button();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tsp_add = new System.Windows.Forms.ToolStripButton();
            this.tsp_delete = new System.Windows.Forms.ToolStripButton();
            this.panel_left = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsp_time = new System.Windows.Forms.ToolStripLabel();
            this.tsp_Msg = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsp_Run = new System.Windows.Forms.ToolStripButton();
            this.tsp_connect = new System.Windows.Forms.ToolStripButton();
            this.保存 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.paraPLCport)).BeginInit();
            this.groupBox_Brand.SuspendLayout();
            this.groupBox_IP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paraPCip)).BeginInit();
            this.panel_right.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Camera_dataGridView)).BeginInit();
            this.panel2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.panel_left.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 82;
            this.label1.Text = "PLC_Port：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(11, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 80;
            this.label4.Text = "PLC_IP：";
            // 
            // paraPLCip
            // 
            this.paraPLCip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paraPLCip.Location = new System.Drawing.Point(91, 22);
            this.paraPLCip.Margin = new System.Windows.Forms.Padding(2);
            this.paraPLCip.Name = "paraPLCip";
            this.paraPLCip.Size = new System.Drawing.Size(282, 23);
            this.paraPLCip.TabIndex = 79;
            this.paraPLCip.Text = "192.168.1.10";
            this.paraPLCip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.paraPLCip.TextChanged += new System.EventHandler(this.paraPLCip_TextChanged);
            // 
            // paraPLCport
            // 
            this.paraPLCport.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paraPLCport.Location = new System.Drawing.Point(91, 52);
            this.paraPLCport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.paraPLCport.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.paraPLCport.Name = "paraPLCport";
            this.paraPLCport.Size = new System.Drawing.Size(282, 23);
            this.paraPLCport.TabIndex = 84;
            this.paraPLCport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.paraPLCport.Value = new decimal(new int[] {
            8501,
            0,
            0,
            0});
            this.paraPLCport.ValueChanged += new System.EventHandler(this.paraPLCport_ValueChanged);
            // 
            // groupBox_Brand
            // 
            this.groupBox_Brand.Controls.Add(this.radioButton_欧姆龙);
            this.groupBox_Brand.Controls.Add(this.radioButton_三菱);
            this.groupBox_Brand.Controls.Add(this.radioButton_基恩士);
            this.groupBox_Brand.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Brand.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_Brand.Location = new System.Drawing.Point(0, 0);
            this.groupBox_Brand.Name = "groupBox_Brand";
            this.groupBox_Brand.Size = new System.Drawing.Size(391, 78);
            this.groupBox_Brand.TabIndex = 1;
            this.groupBox_Brand.TabStop = false;
            this.groupBox_Brand.Text = "PLC品牌";
            // 
            // radioButton_欧姆龙
            // 
            this.radioButton_欧姆龙.AutoSize = true;
            this.radioButton_欧姆龙.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton_欧姆龙.Location = new System.Drawing.Point(311, 40);
            this.radioButton_欧姆龙.Name = "radioButton_欧姆龙";
            this.radioButton_欧姆龙.Size = new System.Drawing.Size(62, 21);
            this.radioButton_欧姆龙.TabIndex = 2;
            this.radioButton_欧姆龙.Text = "欧姆龙";
            this.radioButton_欧姆龙.UseVisualStyleBackColor = true;
            this.radioButton_欧姆龙.CheckedChanged += new System.EventHandler(this.Brand_CheckedChanged);
            // 
            // radioButton_三菱
            // 
            this.radioButton_三菱.AutoSize = true;
            this.radioButton_三菱.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton_三菱.Location = new System.Drawing.Point(167, 40);
            this.radioButton_三菱.Name = "radioButton_三菱";
            this.radioButton_三菱.Size = new System.Drawing.Size(50, 21);
            this.radioButton_三菱.TabIndex = 1;
            this.radioButton_三菱.Text = "三菱";
            this.radioButton_三菱.UseVisualStyleBackColor = true;
            this.radioButton_三菱.CheckedChanged += new System.EventHandler(this.Brand_CheckedChanged);
            // 
            // radioButton_基恩士
            // 
            this.radioButton_基恩士.AutoSize = true;
            this.radioButton_基恩士.Checked = true;
            this.radioButton_基恩士.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton_基恩士.Location = new System.Drawing.Point(26, 40);
            this.radioButton_基恩士.Name = "radioButton_基恩士";
            this.radioButton_基恩士.Size = new System.Drawing.Size(62, 21);
            this.radioButton_基恩士.TabIndex = 0;
            this.radioButton_基恩士.TabStop = true;
            this.radioButton_基恩士.Text = "基恩士";
            this.radioButton_基恩士.UseVisualStyleBackColor = true;
            this.radioButton_基恩士.CheckedChanged += new System.EventHandler(this.Brand_CheckedChanged);
            // 
            // groupBox_IP
            // 
            this.groupBox_IP.Controls.Add(this.paraPCip);
            this.groupBox_IP.Controls.Add(this.label4);
            this.groupBox_IP.Controls.Add(this.label6);
            this.groupBox_IP.Controls.Add(this.paraPLCport);
            this.groupBox_IP.Controls.Add(this.label1);
            this.groupBox_IP.Controls.Add(this.paraPLCip);
            this.groupBox_IP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_IP.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_IP.Location = new System.Drawing.Point(0, 0);
            this.groupBox_IP.Name = "groupBox_IP";
            this.groupBox_IP.Size = new System.Drawing.Size(391, 270);
            this.groupBox_IP.TabIndex = 92;
            this.groupBox_IP.TabStop = false;
            this.groupBox_IP.Text = "IP设置";
            // 
            // paraPCip
            // 
            this.paraPCip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paraPCip.Location = new System.Drawing.Point(91, 81);
            this.paraPCip.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.paraPCip.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.paraPCip.Name = "paraPCip";
            this.paraPCip.Size = new System.Drawing.Size(282, 23);
            this.paraPCip.TabIndex = 92;
            this.paraPCip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.paraPCip.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.paraPCip.ValueChanged += new System.EventHandler(this.paraPCip_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(9, 83);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 91;
            this.label6.Text = "PC_IP_末位：";
            // 
            // panel_right
            // 
            this.panel_right.Controls.Add(this.groupBox2);
            this.panel_right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_right.Location = new System.Drawing.Point(391, 0);
            this.panel_right.Name = "panel_right";
            this.panel_right.Size = new System.Drawing.Size(377, 398);
            this.panel_right.TabIndex = 91;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 398);
            this.groupBox2.TabIndex = 89;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工位设置";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Camera_dataGridView);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(371, 349);
            this.panel3.TabIndex = 8;
            // 
            // Camera_dataGridView
            // 
            this.Camera_dataGridView.AllowUserToAddRows = false;
            this.Camera_dataGridView.AllowUserToResizeColumns = false;
            this.Camera_dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Camera_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Camera_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Camera_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Camera_dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.Camera_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Camera_dataGridView.Location = new System.Drawing.Point(0, 0);
            this.Camera_dataGridView.Name = "Camera_dataGridView";
            this.Camera_dataGridView.RowHeadersVisible = false;
            this.Camera_dataGridView.RowTemplate.Height = 23;
            this.Camera_dataGridView.Size = new System.Drawing.Size(371, 349);
            this.Camera_dataGridView.TabIndex = 6;
            this.Camera_dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Camera_dataGridView_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 88.01751F;
            this.dataGridViewTextBoxColumn1.HeaderText = "工位";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.FillWeight = 105.296F;
            this.dataGridViewTextBoxColumn4.HeaderText = "寄存器地址";
            this.dataGridViewTextBoxColumn4.Items.AddRange(new object[] {
            "D",
            "DM",
            "CIO"});
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.FillWeight = 95.01136F;
            this.dataGridViewTextBoxColumn5.HeaderText = "起始地址";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.FillWeight = 111.6751F;
            this.dataGridViewTextBoxColumn2.HeaderText = "发送数据个数";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button减少工位);
            this.panel2.Controls.Add(this.button添加工位);
            this.panel2.Controls.Add(this.toolStrip3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(371, 27);
            this.panel2.TabIndex = 7;
            // 
            // button减少工位
            // 
            this.button减少工位.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button减少工位.Location = new System.Drawing.Point(213, -2);
            this.button减少工位.Name = "button减少工位";
            this.button减少工位.Size = new System.Drawing.Size(75, 23);
            this.button减少工位.TabIndex = 1;
            this.button减少工位.Text = "减少工位";
            this.button减少工位.UseVisualStyleBackColor = true;
            this.button减少工位.Visible = false;
            this.button减少工位.Click += new System.EventHandler(this.button减少工位_Click);
            // 
            // button添加工位
            // 
            this.button添加工位.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button添加工位.Location = new System.Drawing.Point(132, 0);
            this.button添加工位.Name = "button添加工位";
            this.button添加工位.Size = new System.Drawing.Size(75, 23);
            this.button添加工位.TabIndex = 0;
            this.button添加工位.Text = "添加工位";
            this.button添加工位.UseVisualStyleBackColor = true;
            this.button添加工位.Visible = false;
            this.button添加工位.Click += new System.EventHandler(this.button添加工位_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsp_add,
            this.tsp_delete});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(371, 27);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tsp_add
            // 
            this.tsp_add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsp_add.Image = global::SvsPLC.Properties.Resources.添加;
            this.tsp_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_add.Name = "tsp_add";
            this.tsp_add.Size = new System.Drawing.Size(23, 24);
            this.tsp_add.Text = "添加";
            this.tsp_add.Click += new System.EventHandler(this.button添加工位_Click);
            // 
            // tsp_delete
            // 
            this.tsp_delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsp_delete.Image = global::SvsPLC.Properties.Resources.删除;
            this.tsp_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_delete.Name = "tsp_delete";
            this.tsp_delete.Size = new System.Drawing.Size(23, 24);
            this.tsp_delete.Text = "删除";
            this.tsp_delete.Click += new System.EventHandler(this.button减少工位_Click);
            // 
            // panel_left
            // 
            this.panel_left.Controls.Add(this.panel1);
            this.panel_left.Controls.Add(this.toolStrip2);
            this.panel_left.Controls.Add(this.toolStrip1);
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 0);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(391, 398);
            this.panel_left.TabIndex = 92;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.groupBox_Brand);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(391, 348);
            this.panel1.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox_IP);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 78);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(391, 270);
            this.panel4.TabIndex = 2;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsp_time,
            this.tsp_Msg});
            this.toolStrip2.Location = new System.Drawing.Point(0, 373);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(391, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsp_time
            // 
            this.tsp_time.Name = "tsp_time";
            this.tsp_time.Size = new System.Drawing.Size(23, 22);
            this.tsp_time.Text = "---";
            // 
            // tsp_Msg
            // 
            this.tsp_Msg.Name = "tsp_Msg";
            this.tsp_Msg.Size = new System.Drawing.Size(23, 22);
            this.tsp_Msg.Text = "---";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsp_Run,
            this.tsp_connect,
            this.保存});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(391, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsp_Run
            // 
            this.tsp_Run.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsp_Run.Image = global::SvsPLC.Properties.Resources.运行1;
            this.tsp_Run.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_Run.Name = "tsp_Run";
            this.tsp_Run.Size = new System.Drawing.Size(23, 22);
            this.tsp_Run.Text = "运行";
            this.tsp_Run.Click += new System.EventHandler(this.tsp_Run_Click);
            // 
            // tsp_connect
            // 
            this.tsp_connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsp_connect.Image = global::SvsPLC.Properties.Resources.连接;
            this.tsp_connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_connect.Name = "tsp_connect";
            this.tsp_connect.Size = new System.Drawing.Size(23, 22);
            this.tsp_connect.Text = "连接";
            this.tsp_connect.Click += new System.EventHandler(this.tsp_connect_Click);
            // 
            // 保存
            // 
            this.保存.Image = ((System.Drawing.Image)(resources.GetObject("保存.Image")));
            this.保存.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存.Name = "保存";
            this.保存.Size = new System.Drawing.Size(52, 22);
            this.保存.Text = "保存";
            this.保存.Visible = false;
            this.保存.Click += new System.EventHandler(this.保存_Click);
            // 
            // PLCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(768, 398);
            this.Controls.Add(this.panel_right);
            this.Controls.Add(this.panel_left);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PLCForm";
            this.Text = "PLCForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PLCForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.paraPLCport)).EndInit();
            this.groupBox_Brand.ResumeLayout(false);
            this.groupBox_Brand.PerformLayout();
            this.groupBox_IP.ResumeLayout(false);
            this.groupBox_IP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paraPCip)).EndInit();
            this.panel_right.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Camera_dataGridView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel_left.ResumeLayout(false);
            this.panel_left.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox paraPLCip;
        private System.Windows.Forms.NumericUpDown paraPLCport;
        private System.Windows.Forms.Panel panel_right;
        private System.Windows.Forms.Panel panel_left;
        private System.Windows.Forms.GroupBox groupBox_IP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox_Brand;
        private System.Windows.Forms.RadioButton radioButton_欧姆龙;
        private System.Windows.Forms.RadioButton radioButton_三菱;
        private System.Windows.Forms.RadioButton radioButton_基恩士;
        private System.Windows.Forms.NumericUpDown paraPCip;
        private System.Windows.Forms.Button button减少工位;
        private System.Windows.Forms.Button button添加工位;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView Camera_dataGridView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsp_Run;
        private System.Windows.Forms.ToolStripButton 保存;
        private System.Windows.Forms.ToolStripButton tsp_connect;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel tsp_time;
        private System.Windows.Forms.ToolStripLabel tsp_Msg;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton tsp_add;
        private System.Windows.Forms.ToolStripButton tsp_delete;
    }
}