using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace VisionStudio
{
    /// <summary>
    /// 内 容:本类是用户登录界面
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public partial class UserForm : Form
    {
        /// <summary>
        /// 登录界面
        /// </summary>
        public EventHandler<ParaUserArgs> eventParaUserArgs = null;

        /// <summary>
        /// 传递参数
        /// </summary>
        private ParaUserArgs m_UserPara = new ParaUserArgs();

        /// <summary>
        /// 构造方法
        /// </summary>
        public UserForm()
        {
            InitializeComponent();
        }

        // 登录按钮
        private void btn_Login_Click(object sender, EventArgs e)
        {
            m_UserPara.Login = false;
            if (txt_passWord.Text.Trim() == "123456")
            {
                m_UserPara.Login = true;
                MessageBox.Show("登录成功!", "Tip");
            }
            else
            {
                MessageBox.Show("登录失败，请检查密码!", "TIP");
            }
            if (eventParaUserArgs != null)
            {
                eventParaUserArgs(null, m_UserPara);
            }
            this.Close();
        }

        // 加载
        private void UserForm_Load(object sender, EventArgs e)
        {
            cmb_Mode.SelectedIndex = 1;
            cmb_Mode_SelectedIndexChanged(null, null);
        }

        //取消
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            m_UserPara.Login = false;
            if (eventParaUserArgs != null)
            {
                eventParaUserArgs(null, m_UserPara);
            }
            this.Close();
        }

        // 退出
        private void btn_Exist_Click(object sender, EventArgs e)
        {
            txt_passWord.Clear();
            m_UserPara.Login = false;
            if (eventParaUserArgs != null)
            {
                eventParaUserArgs(null, m_UserPara);
            }
        }

        // 登录角色选择
        private void cmb_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Mode.SelectedIndex == 0)
            {
                txt_passWord.Enabled = false;
            }
            else
            {
                txt_passWord.Enabled = true;
            }
        }
    }

    /// <summary>
    /// 登录传递参数
    /// </summary>
    [XmlRoot("ParaUserArgs")]
    [Serializable]
    public class ParaUserArgs : EventArgs
    {
        private bool m_bLogin = false;
        public bool Login
        {
            get
            {
                return m_bLogin;
            }
            set
            {
                m_bLogin = value;
            }
        }
    }
}
