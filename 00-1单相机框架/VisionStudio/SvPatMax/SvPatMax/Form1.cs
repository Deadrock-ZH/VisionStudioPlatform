using HalconDotNet;
using SvMask;
using SvsPatMax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvPatMax
{
    public partial class Form1 : Form
    {
        HObject m_hoImage = null;

        /// <summary>
        /// 方法类
        /// </summary>
        private SvsPatMaxMethod m_method = new SvsPatMaxMethod();

        /// <summary>
        /// 参数类
        /// </summary>
        SvsPatMaxPara m_para = new SvsPatMaxPara();

        // 对象
        object paras = null;

        /// <summary>
        /// 显示控件
        /// </summary>
        private GVS.HalconDisp.Control.HWindow_Final m_Disp = new GVS.HalconDisp.Control.HWindow_Final();

        /// <summary>
        /// 方法类
        /// </summary>
        public SvMaskMethod m_methodMask = new SvMaskMethod();

        /// <summary>
        /// 图像位置
        /// </summary>
        OpenFileDialog opnDlg = null;

        /// <summary>
        /// 图像索引
        /// </summary>
        int i = 0;

        // load
        private void Form1_Load(object sender, EventArgs e)
        {
            // 大窗口
            pnl_Disp.Controls.Add(m_Disp);
            m_Disp.Dock = DockStyle.Fill;
            m_method.PatMaxName = "Model";
            paras = m_method.Load(m_para.GetType(), Application.StartupPath+"\\"+ m_method.PatMaxName);
            m_method.Para = (SvsPatMaxPara)paras;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PatMaxForm dlg = new PatMaxForm();
            dlg.m_method = m_method;
            dlg.m_bModual = true;
            dlg.ShowDialog();
        }

        // 图片
        private void button2_Click(object sender, EventArgs e)
        {
            opnDlg = new OpenFileDialog();
            opnDlg.Filter = "所有图像文件 | *.bmp; *.pcx; *.png; *.jpg; *.gif;" +
                "*.tif; *.ico; *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf";
            opnDlg.Title = "打开图像文件";
            opnDlg.ShowHelp = true;
            opnDlg.Multiselect = false;
            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                // 文件夹位置
                string srFileName = opnDlg.FileName;

                // 读取显示图片
                HOperatorSet.GenEmptyObj(out this.m_hoImage);

                // 读取、显示图片
                HOperatorSet.ReadImage(out this.m_hoImage, srFileName);
                m_Disp.HobjectToHimage(m_hoImage);
                m_methodMask.InputImage = m_hoImage;
                m_method.InputImage = m_hoImage;
            }
        }

        // 运行按钮
        private void button3_Click(object sender, EventArgs e)
        {
            HTuple hv_ImageFiles = null;
            if (opnDlg != null)  
            {
                HOperatorSet.ListFiles(opnDlg.FileName.Substring(0, opnDlg.FileName.LastIndexOf("\\")), (new HTuple("files")).TupleConcat(
                "follow_links"), out hv_ImageFiles);
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, 
                (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).
                 TupleConcat("ignore_case"), out hv_ImageFiles);
                if (i < hv_ImageFiles.Length)
                {
                    HOperatorSet.ReadImage(out m_hoImage, hv_ImageFiles.TupleSelect(i));
                    m_methodMask.InputImage = m_hoImage;
                    m_method.InputImage = m_hoImage;
                    bool bstate = false;
                    bstate = m_method.Run();
                    if (bstate)
                    {
                        m_Disp.HobjectToHimage(m_hoImage);
                        m_Disp.DispObj(m_method.FindContourModel, "red");
                    }
                    i++;
                }
                else
                {
                    MessageBox.Show("全部完成，从0开始！");
                    i = 0;
                }
            }
            else
            {
                MessageBox.Show("请确认文件是否存在！");
            }
        }

        // 掩膜
        private void button4_Click(object sender, EventArgs e)
        {
            SvMask.MaskForm dlg = new SvMask.MaskForm();
            dlg.m_method = m_methodMask;
            dlg.ShowDialog();
        }
    }
}
