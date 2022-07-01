namespace Interface2
{
    partial class Administrator
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
            this.label_Administrator = new System.Windows.Forms.Label();
            this.user_login = new System.Windows.Forms.Button();
            this.user_password = new System.Windows.Forms.TextBox();
            this.user_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Administrator
            // 
            this.label_Administrator.AutoSize = true;
            this.label_Administrator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_Administrator.Location = new System.Drawing.Point(26, 232);
            this.label_Administrator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Administrator.Name = "label_Administrator";
            this.label_Administrator.Size = new System.Drawing.Size(0, 13);
            this.label_Administrator.TabIndex = 27;
            // 
            // user_login
            // 
            this.user_login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.user_login.ForeColor = System.Drawing.Color.White;
            this.user_login.Location = new System.Drawing.Point(134, 173);
            this.user_login.Margin = new System.Windows.Forms.Padding(4);
            this.user_login.Name = "user_login";
            this.user_login.Size = new System.Drawing.Size(239, 41);
            this.user_login.TabIndex = 25;
            this.user_login.Text = "登录";
            this.user_login.UseVisualStyleBackColor = false;
            this.user_login.Click += new System.EventHandler(this.user_login_Click);
            // 
            // user_password
            // 
            this.user_password.Location = new System.Drawing.Point(134, 122);
            this.user_password.Margin = new System.Windows.Forms.Padding(4);
            this.user_password.Name = "user_password";
            this.user_password.PasswordChar = '*';
            this.user_password.Size = new System.Drawing.Size(237, 21);
            this.user_password.TabIndex = 24;
            // 
            // user_Name
            // 
            this.user_Name.Location = new System.Drawing.Point(134, 63);
            this.user_Name.Margin = new System.Windows.Forms.Padding(4);
            this.user_Name.Name = "user_Name";
            this.user_Name.Size = new System.Drawing.Size(237, 21);
            this.user_Name.TabIndex = 23;
            this.user_Name.Text = "user1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(50, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "用户名:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(50, 126);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "密  码:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(152, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 19);
            this.label3.TabIndex = 26;
            this.label3.Text = "BI300登录界面";
            // 
            // Administrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 257);
            this.Controls.Add(this.label_Administrator);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.user_login);
            this.Controls.Add(this.user_password);
            this.Controls.Add(this.user_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Administrator";
            this.Text = "Administrator";
            this.Load += new System.EventHandler(this.Administrator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Administrator;
        private System.Windows.Forms.Button user_login;
        private System.Windows.Forms.TextBox user_password;
        private System.Windows.Forms.TextBox user_Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}