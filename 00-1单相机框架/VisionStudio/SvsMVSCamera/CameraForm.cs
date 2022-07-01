using HalconDotNet;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SvsMVSCamera
{
    public partial class CameraForm : Form
    {
        /// <summary>
        /// 相机方法类
        /// </summary>
        private  CameraAPI m_CameraMethod;

        /// <summary>
        /// 相机名
        /// </summary>
        private  string strName = "";

        /// <summary>
        /// 显示控件
        /// </summary>
        GVS.HalconDisp.Control.HWindow_Final m_Disp = new GVS.HalconDisp.Control.HWindow_Final();

        /// <summary>
        /// 入栈
        /// </summary>
        Queue<HObject> m_Queue = new Queue<HObject>();

        /// <summary>
        /// 线程
        /// </summary>
        Thread m_threadDe = null;

        /// <summary>
        /// 存储图片的索引命名
        /// </summary>
        int m_iImage = 1;
        HTuple m_second1 = null;
        HTuple m_second2 = null;

        /// <summary>
        /// 是否启用存图功能
        /// </summary>
        bool m_bSave = false;

        /// <summary>
        /// 延时
        /// </summary>
        int m_iSleepTime = 0;

        /// <summary>
        /// 存储路径
        /// </summary>
        DirectoryInfo m_SavePath = null;

        /// <summary>
        ///  获取根目录
        /// </summary>
        string m_strStartPath = Application.StartupPath.ToString();

        /// <summary>
        /// 选中的dgv的行索引
        /// </summary>
        int m_DgvIndex = -1;

        public CameraForm()
        {
            InitializeComponent();
        }

        private void CameraForm_Load(object sender, EventArgs e)
        {
            DgvFileUpdate();
            pnl_Disp1.Controls.Clear();
            pnl_Disp1.Controls.Add(m_Disp);
            m_Disp.Dock = DockStyle.Fill;
            pnl_param.Enabled = false;
     
            m_CameraMethod = new CameraAPI(out strName);
            if (strName.Contains("USB"))
            {
                tv_Camera.Nodes[1].Nodes.Add(strName);
                tv_Camera.ExpandAll();
            }
            else if (strName.Contains("GIE"))
            {
                tv_Camera.Nodes[0].Nodes.Add(strName);
                tv_Camera.ExpandAll();
            }
            m_CameraMethod.eventProcessHImage += M_CameraMethod_eventProcessHImage;
            m_threadDe = new Thread(new ThreadStart(GetImage));
            m_threadDe.IsBackground = true;
            m_threadDe.Start();

            m_CameraMethod.OpenCam();
            pnl_param.Enabled = true;
            float fExposure, fGain;
            m_CameraMethod.GetExposureTime(out fExposure);
            m_CameraMethod.GetGain(out fGain);
            num_Exposure.Value = (decimal)fExposure;
            num_Gain.Value = (decimal)fGain;
            cmb_Select.SelectedIndex = 2;
            cmb_Select_SelectedIndexChanged(null, null);
        }
 
        private void GetImage()
        {
            while (true)
            {
                if (m_Queue.Count > 0)
                {
                    HObject ho_image = null;
                    ho_image = (HObject)m_Queue.Dequeue();

                    if (m_SavePath != null)
                    {
                        HOperatorSet.WriteImage(ho_image, "bmp", 0, m_SavePath.FullName + "\\" + m_iImage);
                        m_iImage++;
                    }
                }
            }
        }

        private void M_CameraMethod_eventProcessHImage(HObject hImage,int CcdId)
        {
            if (m_bSave)
            {
                Thread.Sleep(m_iSleepTime);
                m_Queue.Enqueue(hImage);
            }
            else
            {
                HObject ho_EdgeAmplitude  = null;
                HTuple hv_Mean, hv_Deviation;
                m_Disp.HobjectToHimage(hImage);
                HOperatorSet.SobelAmp(hImage, out ho_EdgeAmplitude, "sum_abs", 3);
                HOperatorSet.Intensity(ho_EdgeAmplitude, ho_EdgeAmplitude, out hv_Mean, out hv_Deviation);
                m_Disp.DisplayMessage(hv_Mean[0].D.ToString(), 50, 10, "red", false,"微软雅黑","Normal",30);
                m_Disp.DisplayMessage(hv_Deviation[0].D.ToString(), 80, 10, "red", false, "微软雅黑", "Normal",30);
            }
        }

        private void tsp_OpenCamera_Click(object sender, EventArgs e)
        {
            m_iImage = 1;
            m_bSave = true;
            m_Disp.DisplayMessage("存图模式已开启！", 20, 10, "green", false, "微软雅黑", "Normal", 10);
            string strDateTime = DateTime.Now.ToString("yyyy_MM_dd_hh_mm");
            if (!Directory.Exists(m_strStartPath + "\\Image\\" + strDateTime))
            {
                m_SavePath = Directory.CreateDirectory(m_strStartPath + "\\Image\\" + strDateTime);
            }
            DgvFileUpdate();
        }

        /// <summary>
        /// 保存文件夹更新
        /// </summary>
        private void DgvFileUpdate()
        {
            dgv_File.Rows.Clear();
            if (Directory.Exists(m_strStartPath + "\\Image"))
            {
                string[] strDirectoryFile = Directory.GetDirectories(m_strStartPath + "\\Image");
                if (strDirectoryFile.Count() > 0)
                {
                    dgv_File.Rows.Add(strDirectoryFile.Count());
                    for (int i = 0; i < strDirectoryFile.Length; i++)
                    {
                        string[] strFile = Directory.GetFiles(strDirectoryFile[i]);
                        dgv_File.Rows[i].Cells[0].Value = strDirectoryFile[i];
                        dgv_File.Rows[i].Cells[1].Value = strFile.Count();
                    }
                    if (dgv_File.Rows.Count > 0)
                    {
                        dgv_File.Rows[0].Selected = false;
                        dgv_File.Rows[dgv_File.Rows.Count - 1].Selected = true;
                        m_DgvIndex = dgv_File.Rows.Count - 1;
                    }
                }
            }
        }


        private void tsp_Stop_Click(object sender, EventArgs e)
        {
            tsp_DispImage.Enabled = true;
            if (m_bSave)
            {
                m_bSave = false;
                m_Disp.DisplayMessage("已停止存图！", 20, 10, "green", false, "微软雅黑", "Normal", 10);
            }
            m_CameraMethod.StopGrab();
            DgvFileUpdate();
        }

        private void tsp_Start_Click(object sender, EventArgs e)
        {
            tsp_DispImage.Enabled = false;
            if (!m_CameraMethod.m_bGrabbing)
            {
                HOperatorSet.CountSeconds(out m_second1);
                m_CameraMethod.StartGrab();
            }
            else
            {
                MessageBox.Show("相机正在采集!");
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
              m_bSave = false;
        }

        private void num_Exposure_ValueChanged(object sender, EventArgs e)
        {
            m_CameraMethod.setExposureTime((long)num_Exposure.Value);
        }

        private void num_Gain_ValueChanged(object sender, EventArgs e)
        {
            m_CameraMethod.setGain((long)num_Gain.Value);
        }

        private void CameraForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_CameraMethod.StopGrab();
            m_CameraMethod.CloseCam();
            m_threadDe.Abort();
            Thread.Sleep(10);
            m_bSave = false;
        }

        private void cmb_Select_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_Select.SelectedIndex)
            {
                case 0:
                    m_iSleepTime = 3;
                    break;
                case 1:
                    m_iSleepTime =1 ;
                    break;
                case 2:
                    m_iSleepTime = 0;
                    break;
            }
        }

        private void tsp_DispImage_Click(object sender, EventArgs e)
        {
            toolStrip1.Enabled = false;
            string[] strDirectoryFile = Directory.GetDirectories(m_strStartPath + "\\Image");
            List<int> m_listIndex = new List<int>();
            if (strDirectoryFile.Count() > 0 && m_DgvIndex > -1)
            {
                string[] strImageNameNum = new string[10000];
                string[] strImageName = Directory.GetFileSystemEntries(strDirectoryFile[m_DgvIndex]);
                for (int i = 0; i < strImageName.Count(); i++)
                {
                    string[] strFileName = strImageName[i].Split('\\');
                    strImageNameNum = strFileName[strFileName.Count() - 1].Split('.');
                    m_listIndex.Add(int.Parse(strImageNameNum[0]));

                }
                m_listIndex.Sort();
                int iName = int.Parse(strImageNameNum[0]);
                int K = 1;
                for (int i = 0; i < m_listIndex.Count(); i++)
                {
                    HObject hoReadImage = null;
                    {
                        if (i >= (strImageName.Count() - 1))
                        {
                            m_Disp.DisplayMessage("显示完成！", 20, 10, "red", false, "微软雅黑", "Normal", 10);
                            toolStrip1.Enabled = true;
                            if (File.Exists(strDirectoryFile[m_DgvIndex] + "\\" + m_listIndex[i] + ".bmp"))
                            {
                                HOperatorSet.ReadImage(out hoReadImage, strDirectoryFile[m_DgvIndex] + "\\" + iName);
                            }
                            return;
                        }
                        if (File.Exists(strDirectoryFile[m_DgvIndex] + "\\" + m_listIndex[i] + ".bmp"))
                        {
                            HOperatorSet.ReadImage(out hoReadImage, strDirectoryFile[m_DgvIndex] + "\\" + m_listIndex[i]);
                        }
                        int iTime = m_iSleepTime * 10;
                        Thread.Sleep(iTime);
                        m_Disp.HobjectToHimage(hoReadImage);
                        m_Disp.DisplayMessage("图片名称：" + m_listIndex[i].ToString(), 10, 10, "red", false, "微软雅黑", "Normal", 10);
                    }
                }
            }
        }

        private void dgv_File_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             if (e.RowIndex > -1 )
            {
                 m_DgvIndex = dgv_File.CurrentRow.Index;
                dgv_File.Rows[m_DgvIndex].Selected =true;
            }
        }

        private void dgv_File_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                m_DgvIndex = dgv_File.CurrentRow.Index;
                dgv_File.Rows[m_DgvIndex].Selected = true;
            }
        }

        private void dgv_File_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                m_DgvIndex = dgv_File.CurrentRow.Index;
                dgv_File.Rows[m_DgvIndex].Selected = true;
            }
        }
    }
}
