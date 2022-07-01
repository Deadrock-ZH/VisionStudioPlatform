namespace Interface2
{
    partial class FileSet
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
            this.cmb_GWSelect = new System.Windows.Forms.ComboBox();
            this.txt_FileName = new System.Windows.Forms.TextBox();
            this.btn_NewFile = new System.Windows.Forms.Button();
            this.btn_OpenFile = new System.Windows.Forms.Button();
            this.btn_SaveFile = new System.Windows.Forms.Button();
            this.btn_SaveAsFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_DeleteFile = new System.Windows.Forms.Button();
            this.label_FreshFold = new System.Windows.Forms.Label();
            this.cmb_ParaName = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // cmb_GWSelect
            // 
            this.cmb_GWSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GWSelect.FormattingEnabled = true;
            this.cmb_GWSelect.Location = new System.Drawing.Point(9, 61);
            this.cmb_GWSelect.Name = "cmb_GWSelect";
            this.cmb_GWSelect.Size = new System.Drawing.Size(121, 20);
            this.cmb_GWSelect.TabIndex = 0;
            this.cmb_GWSelect.SelectedIndexChanged += new System.EventHandler(this.cmb_GWSelect_SelectedIndexChanged);
            // 
            // txt_FileName
            // 
            this.txt_FileName.Location = new System.Drawing.Point(148, 60);
            this.txt_FileName.Name = "txt_FileName";
            this.txt_FileName.Size = new System.Drawing.Size(100, 21);
            this.txt_FileName.TabIndex = 1;
            this.txt_FileName.TextChanged += new System.EventHandler(this.txt_FileName_TextChanged);
            // 
            // btn_NewFile
            // 
            this.btn_NewFile.Location = new System.Drawing.Point(290, 12);
            this.btn_NewFile.Name = "btn_NewFile";
            this.btn_NewFile.Size = new System.Drawing.Size(75, 23);
            this.btn_NewFile.TabIndex = 2;
            this.btn_NewFile.Text = "新建";
            this.btn_NewFile.UseVisualStyleBackColor = true;
            this.btn_NewFile.Click += new System.EventHandler(this.btn_NewFile_Click);
            // 
            // btn_OpenFile
            // 
            this.btn_OpenFile.Location = new System.Drawing.Point(290, 45);
            this.btn_OpenFile.Name = "btn_OpenFile";
            this.btn_OpenFile.Size = new System.Drawing.Size(75, 23);
            this.btn_OpenFile.TabIndex = 3;
            this.btn_OpenFile.Text = "打开/读取";
            this.btn_OpenFile.UseVisualStyleBackColor = true;
            this.btn_OpenFile.Click += new System.EventHandler(this.btn_OpenFile_Click);
            // 
            // btn_SaveFile
            // 
            this.btn_SaveFile.Location = new System.Drawing.Point(290, 74);
            this.btn_SaveFile.Name = "btn_SaveFile";
            this.btn_SaveFile.Size = new System.Drawing.Size(75, 23);
            this.btn_SaveFile.TabIndex = 4;
            this.btn_SaveFile.Text = "保存";
            this.btn_SaveFile.UseVisualStyleBackColor = true;
            this.btn_SaveFile.Click += new System.EventHandler(this.btn_SaveFile_Click);
            // 
            // btn_SaveAsFile
            // 
            this.btn_SaveAsFile.Location = new System.Drawing.Point(290, 103);
            this.btn_SaveAsFile.Name = "btn_SaveAsFile";
            this.btn_SaveAsFile.Size = new System.Drawing.Size(75, 23);
            this.btn_SaveAsFile.TabIndex = 5;
            this.btn_SaveAsFile.Text = "另存为";
            this.btn_SaveAsFile.UseVisualStyleBackColor = true;
            this.btn_SaveAsFile.Click += new System.EventHandler(this.btn_SaveAsFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "工位选择";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "命名";
            // 
            // btn_DeleteFile
            // 
            this.btn_DeleteFile.Location = new System.Drawing.Point(290, 132);
            this.btn_DeleteFile.Name = "btn_DeleteFile";
            this.btn_DeleteFile.Size = new System.Drawing.Size(75, 23);
            this.btn_DeleteFile.TabIndex = 8;
            this.btn_DeleteFile.Text = "删除";
            this.btn_DeleteFile.UseVisualStyleBackColor = true;
            this.btn_DeleteFile.Click += new System.EventHandler(this.btn_DeleteFile_Click);
            // 
            // label_FreshFold
            // 
            this.label_FreshFold.AutoSize = true;
            this.label_FreshFold.Location = new System.Drawing.Point(11, 93);
            this.label_FreshFold.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_FreshFold.Name = "label_FreshFold";
            this.label_FreshFold.Size = new System.Drawing.Size(72, 13);
            this.label_FreshFold.TabIndex = 135;
            this.label_FreshFold.Text = "已有文件：";
            this.label_FreshFold.Click += new System.EventHandler(this.label_FreshFold_Click);
            this.label_FreshFold.DoubleClick += new System.EventHandler(this.label_FreshFold_DoubleClick);
            // 
            // cmb_ParaName
            // 
            this.cmb_ParaName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ParaName.FormattingEnabled = true;
            this.cmb_ParaName.Items.AddRange(new object[] {
            "圆形",
            "矩形"});
            this.cmb_ParaName.Location = new System.Drawing.Point(9, 117);
            this.cmb_ParaName.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_ParaName.Name = "cmb_ParaName";
            this.cmb_ParaName.Size = new System.Drawing.Size(254, 20);
            this.cmb_ParaName.TabIndex = 134;
            // 
            // FileSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 159);
            this.Controls.Add(this.label_FreshFold);
            this.Controls.Add(this.cmb_ParaName);
            this.Controls.Add(this.btn_DeleteFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_SaveAsFile);
            this.Controls.Add(this.btn_SaveFile);
            this.Controls.Add(this.btn_OpenFile);
            this.Controls.Add(this.btn_NewFile);
            this.Controls.Add(this.txt_FileName);
            this.Controls.Add(this.cmb_GWSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FileSet";
            this.Text = "FileSet";
            this.Load += new System.EventHandler(this.FileSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_GWSelect;
        private System.Windows.Forms.TextBox txt_FileName;
        private System.Windows.Forms.Button btn_NewFile;
        private System.Windows.Forms.Button btn_OpenFile;
        private System.Windows.Forms.Button btn_SaveFile;
        private System.Windows.Forms.Button btn_SaveAsFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_DeleteFile;
        private System.Windows.Forms.Label label_FreshFold;
        private System.Windows.Forms.ComboBox cmb_ParaName;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}