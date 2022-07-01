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
    public partial class FileSet : Form
    {

        int numgw = 4;
        public FileSet()
        {
            InitializeComponent();
            //Set1();

        }

        private void FileSet_Load(object sender, EventArgs e)
        {
            this.Text = "参数操作";
            numgw = PassData.NumDisp;
            Set1();
            toolTip();
            UpdateFileList();
        }

        void toolTip()
        {
            #region 提示信息0902

            toolTip1.IsBalloon = true;
            toolTip1.AutoPopDelay = 32000;

            //******************************************
            toolTip1.SetToolTip(this.label_FreshFold,
                "鼠标左键单击此处可刷新,右键单击可显示当前所用文件");

            #endregion


        }

        void Set1()
        {
            try
            {
                if (numgw <= 4)
                {
                    cmb_GWSelect.Items.Add("工位1");
                    cmb_GWSelect.Items.Add("工位2");
                    cmb_GWSelect.Items.Add("工位3_1");
                    cmb_GWSelect.Items.Add("工位4");

                }
                else
                {
                    cmb_GWSelect.Items.Add("工位1");
                    cmb_GWSelect.Items.Add("工位2");
                    cmb_GWSelect.Items.Add("工位3_1");
                    cmb_GWSelect.Items.Add("工位3_2");
                    cmb_GWSelect.Items.Add("工位4");

                }

                cmb_GWSelect.Text = (string)cmb_GWSelect.Items[0];



            }
            catch (Exception)
            {

            }


        }

        private void btn_NewFile_Click(object sender, EventArgs e)
        {
            switch (cmb_GWSelect.Text)
            {
                case "工位1":
                    PassData.cameraSetAbout = "工位1新建";
                    break;

                case "工位2":
                    PassData.cameraSetAbout = "工位2新建";
                    break;

                case "工位3_1":
                    PassData.cameraSetAbout = "工位3_1新建";
                    break;

                case "工位3_2":
                    PassData.cameraSetAbout = "工位3_2新建";
                    break;

                case "工位4":
                    PassData.cameraSetAbout = "工位4新建";
                    break;

                default:
                    break;
            }
            PassData.cameraSet++;

            txt_FileName.Text = null;
            PassData.strContent = string.Empty;

            //刷新列表
            UpdateFileList();
        }

        private void btn_OpenFile_Click(object sender, EventArgs e)
        { //读取文件
            PassData.cameraSetAbout = "打开文件";

            switch (cmb_GWSelect.Text)
            {

                case "工位1":
                    PassData.strContent = "工位1打开";
                        break;

                case "工位2":
                    PassData.strContent = "工位2打开";
                    break;

                case "工位3_1":
                    PassData.strContent = "工位3_1打开";
                    break;

                case "工位3_2":
                    PassData.strContent = "工位3_2打开";
                    break;

                case "工位4":
                    PassData.strContent = "工位4打开";
                    break;

                default:
                    break;

            }

            if (MessageBox.Show("确定" + cmb_GWSelect.Text + "读取文件:" + cmb_ParaName.Text + "?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
              
                PassData.strContent2 = cmb_ParaName.Text;
                PassData.cameraSet++;

            }

            PassData.strContent = string.Empty;
            PassData.strContent2 = string.Empty;


        }

        private void btn_SaveFile_Click(object sender, EventArgs e)
        {

            PassData.cameraSetAbout = "保存文件";
            PassData.cameraSet++;


        }

        private void btn_SaveAsFile_Click(object sender, EventArgs e)
        {

            if (txt_FileName.Text==""|| txt_FileName.Text==null || txt_FileName.Text==string.Empty)
            {
                MessageBox.Show("请先命名");
                return;
            }

            PassData.cameraSetAbout = "另存为";

            switch (cmb_GWSelect.Text)
            {

                case "工位1":
                    PassData.strContent = "工位1另存为";

                    break;

                case "工位2":
                    PassData.strContent = "工位2另存为";
                    break;

                case "工位3_1":
                    PassData.strContent = "工位3_1另存为";
                    break;

                case "工位3_2":
                    PassData.strContent = "工位3_2另存为";
                    break;

                case "工位4":
                    PassData.strContent = "工位4另存为";
                    break;

                default:
                    break;

            }
            

            PassData.strContent2 = txt_FileName.Text;
            PassData.strContent3 = cmb_ParaName.Text;

            PassData.cameraSet++;

            PassData.strContent2 = string.Empty;
            PassData.strContent3 = string.Empty;

            //重新刷新
            UpdateFileList();

        }

        private void txt_FileName_TextChanged(object sender, EventArgs e)
        {
            PassData.strContent = txt_FileName.Text;
        }

        private void label_FreshFold_DoubleClick(object sender, EventArgs e)
        {
          

        }

        private void label_FreshFold_Click(object sender, EventArgs e)
        {
            MouseEventArgs Mouse_e = (MouseEventArgs)e;
            //判断点击鼠标左键或右键
            if (Mouse_e.Button == MouseButtons.Left)
            {

                UpdateFileList();

            }
            else//获取当前工位所用参数文件
            {

                GetCurrentFile(true);

            }
        }


        void UpdateLieBiao(List<string> m_AllparaName)
        {

            try
            {

                cmb_ParaName.Items.Clear();
                foreach (var item in m_AllparaName)
                {
                    cmb_ParaName.Items.Add(item.ToString());
                }

                cmb_ParaName.Text = m_AllparaName[0];
            }
            catch (Exception)
            {

               
            }



        }

        void GetCurrentFile(bool IsNeedShow)
        {
            try
            {
                PassData.cameraSetAbout = "获取当前工位参数文件";
                switch (cmb_GWSelect.Text)
                {
                    case "工位1":
                        PassData.strContent = "获取工位1参数文件";
                        PassData.cameraSet++;
                        if (IsNeedShow)
                        {
                            MessageBox.Show("工位1当前参数文件:" + PassData.strContent.ToString());
                        }


                        break;

                    case "工位2":
                        PassData.strContent = "获取工位2参数文件";
                        PassData.cameraSet++;
                        if (IsNeedShow) 
                        {
                            MessageBox.Show("工位2当前参数文件:" + PassData.strContent.ToString());

                        }

                        break;

                    case "工位3_1":
                        PassData.strContent = "获取工位3_1参数文件";
                        PassData.cameraSet++;
                        if (IsNeedShow)
                        {
                            MessageBox.Show("工位3_1当前参数文件:" + PassData.strContent.ToString());

                        }

                        break;

                    case "工位3_2":
                        PassData.strContent = "获取工位3_2参数文件";
                        PassData.cameraSet++;
                        if (IsNeedShow)
                        {
                            MessageBox.Show("工位3_2当前参数文件:" + PassData.strContent.ToString());
                        }
                        break;

                    case "工位4":
                        PassData.strContent = "获取工位4参数文件";
                        PassData.cameraSet++;
                        if (IsNeedShow)
                        {
                            MessageBox.Show("工位4当前参数文件:" + PassData.strContent.ToString());
                        }

                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            strCurrentFile = PassData.strContent;
            PassData.strContent = string.Empty;


        }

        string strCurrentFile;
        private void btn_DeleteFile_Click(object sender, EventArgs e)
        {

            //获取当前该工位所用文件
            GetCurrentFile(false);
            if (strCurrentFile== cmb_ParaName.Text)
            {
                MessageBox.Show("该文件为当前所用，不可删除");
                return;
            }

            PassData.cameraSetAbout = "删除";



            try
            {

                switch (cmb_GWSelect.Text)
                {

                    case "工位1":
                        PassData.strContent = "工位1";
                        break;

                    case "工位2":
                        PassData.strContent = "工位2";
                        break;

                    case "工位3_1":
                        PassData.strContent = "工位3_1";
                        break;

                    case "工位3_2":
                        PassData.strContent = "工位3_2";
                        break;

                    case "工位4":
                        PassData.strContent = "工位4";
                        break;

                    default:
                        break;

                }


            }
            catch (Exception)
            {

               
            }


            //需要删除的文件名称
            PassData.strContent2 = cmb_ParaName.Text;

            PassData.cameraSet++;

            PassData.strContent = string.Empty;
            PassData.strContent2 = string.Empty;

            //重新刷新
            UpdateFileList();

          



        }


        void UpdateFileList()
        {

            //刷新对应工位，已有文件
            try
            {
                PassData.cameraSetAbout = "获取参数文件";
                switch (cmb_GWSelect.Text)
                {
                    case "工位1":
                        PassData.strContent = "工位1列举参数文件";
                        PassData.cameraSet++;
                        //更新列表
                        UpdateLieBiao(PassData.m_AllparaName);

                        break;

                    case "工位2":
                        PassData.strContent = "工位2列举参数文件";
                        PassData.cameraSet++;
                        //更新列表
                        UpdateLieBiao(PassData.m_AllparaName);
                        break;

                    case "工位3_1":
                        PassData.strContent = "工位3_1列举参数文件";
                        PassData.cameraSet++;
                        //更新列表
                        UpdateLieBiao(PassData.m_AllparaName);
                        break;

                    case "工位3_2":
                        PassData.strContent = "工位3_2列举参数文件";
                        PassData.cameraSet++;
                        //更新列表
                        UpdateLieBiao(PassData.m_AllparaName);
                        break;

                    case "工位4":
                        PassData.strContent = "工位4列举参数文件";
                        PassData.cameraSet++;
                        //更新列表
                        UpdateLieBiao(PassData.m_AllparaName);
                        break;

                    default:
                        break;
                }
                PassData.cameraSet++;




            }
            catch (Exception ex)
            {

                cmb_ParaName.Items.Clear();
                MessageBox.Show(ex.ToString());

            }

            PassData.strContent = string.Empty;

        }

        private void cmb_GWSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
         UpdateFileList();
        }
    }
}
