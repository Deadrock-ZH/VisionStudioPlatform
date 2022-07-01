using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface2
{
    public partial class Administrator : Form
    {

        public Administrator()
        {
            InitializeComponent();
        }

        private void Administrator_Load(object sender, EventArgs e)
        {
            if (PassData.NumDisp<=4)
            {
                label3.Text = "BI200登录界面";
            }
            else
            {
                label3.Text = "BI300登录界面";
            }
        }

        private void user_login_Click(object sender, EventArgs e)
        {
            label_Administrator.Text = "";
            if (user_Name.Text != "" && user_password.Text != "")
            {
                if (user_Name.Text != "user1")
                {
                    label_Administrator.Text = "输入用户名错误";
                    Form1.IsRegister = false;

                }
                else if (user_password.Text != "123")//02382
                {
                    label_Administrator.Text = "输入密码错误";
                    Form1.IsRegister = false;
                }
                else
                {
                    label_Administrator.Text = "登录成功";
                    user_password.Text = "";
                    this.Hide();
                    Form1.IsRegister = true;
                }
            }
            else
            {
                label_Administrator.Text = "请输入用户名跟密码";
                Form1.IsRegister = false;

            }
        }
    }
}
