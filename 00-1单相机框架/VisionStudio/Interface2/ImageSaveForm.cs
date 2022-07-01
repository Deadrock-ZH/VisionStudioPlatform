using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionStudio
{
    /// <summary>
    /// 内 容:本类是图片保存界面
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public partial class ImageSaveForm : Form
    {
        /// <summary>
        /// 方法类主要使用参数
        /// </summary>
        public Method m_Method = null;

        // 构造函数
        public ImageSaveForm()
        {
            InitializeComponent();
        }

        // 窗体加载
        private void ImageSaveForm_Load(object sender, EventArgs e)
        {
            switch (m_Method.Paras.ParasImageSave.ImageSaveTypes)
            {
                case ParaImageSave.ImageSaveType.ALL:
                    rad_All.Checked = true;
                    break;
                case ParaImageSave.ImageSaveType.NG:
                    rad_NG.Checked = true;
                    break;
                case ParaImageSave.ImageSaveType.OK:
                    rad_OK.Checked = true;
                    break;
                case ParaImageSave.ImageSaveType.NO:
                    rad_NO.Checked = true;
                    break;

            }
            num_SaveDay.Value = (decimal)m_Method.Paras.ParasImageSave.SaveDay;
        }

        // 保存全部图片
        private void rad_All_CheckedChanged(object sender, EventArgs e)
        {
            m_Method.Paras.ParasImageSave.ImageSaveTypes = ParaImageSave.ImageSaveType.ALL;
        }

        // 保存OK图片
        private void rad_OK_CheckedChanged(object sender, EventArgs e)
        {
            m_Method.Paras.ParasImageSave.ImageSaveTypes = ParaImageSave.ImageSaveType.OK;
        }

        // 保存NG图片
        private void rad_NG_CheckedChanged(object sender, EventArgs e)
        {
            m_Method.Paras.ParasImageSave.ImageSaveTypes = ParaImageSave.ImageSaveType.NG;
        }

        // 不保存
        private void rad_NO_CheckedChanged(object sender, EventArgs e)
        {
            m_Method.Paras.ParasImageSave.ImageSaveTypes = ParaImageSave.ImageSaveType.NO;
        }

        // 保存天数
        private void num_SaveDay_ValueChanged(object sender, EventArgs e)
        {
            m_Method.Paras.ParasImageSave.SaveDay = (int)num_SaveDay.Value;
        }
    }
}
