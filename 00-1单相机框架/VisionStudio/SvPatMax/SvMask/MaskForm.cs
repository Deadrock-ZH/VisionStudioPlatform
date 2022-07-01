using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvMask
{
    public partial class MaskForm : Form
    {
        /// <summary>
        /// 显示控件
        /// </summary>
        GVS.HalconDisp.Control.HWindow_Final m_Disp = new GVS.HalconDisp.Control.HWindow_Final();

        /// <summary>
        /// 方法类
        /// </summary>
        public SvMaskMethod m_method;

        /// <summary>
        /// 存储ROI
        /// </summary>
        List<GVS.HalconDisp.ViewWindow.Model.ROI> m_listRoi = new List<GVS.HalconDisp.ViewWindow.Model.ROI>();

        /// <summary>
        /// 矩形2
        /// </summary>
        GVS.HalconDisp.ViewWindow.Config.Rectangle2 m_rectangle2 = new GVS.HalconDisp.ViewWindow.Config.Rectangle2();

        /// <summary>
        /// 存储矩形2
        /// </summary>
        private List<GVS.HalconDisp.ViewWindow.Config.Rectangle2> m_listRectPara = new List<GVS.HalconDisp.ViewWindow.Config.Rectangle2>();

        /// <summary>
        /// 圆
        /// </summary>
        GVS.HalconDisp.ViewWindow.Config.Circle m_Circle = new GVS.HalconDisp.ViewWindow.Config.Circle();

        /// <summary>
        /// 存储圆
        /// </summary>
        private List<GVS.HalconDisp.ViewWindow.Config.Circle> m_listCirclePara = new List<GVS.HalconDisp.ViewWindow.Config.Circle>();

        /// <summary>
        /// 多边形
        /// </summary>
        GVS.HalconDisp.ViewWindow.Config.Polygon m_polygon = new GVS.HalconDisp.ViewWindow.Config.Polygon();

        /// <summary>
        /// 存储多边形
        /// </summary>
        private List<GVS.HalconDisp.ViewWindow.Config.Polygon> m_listPolygonPara = new List<GVS.HalconDisp.ViewWindow.Config.Polygon>();

        /// <summary>
        /// 是否绘制
        /// </summary>
        private bool m_bPaint = false;

        /// <summary>
        /// 输入图像
        /// </summary>
        private HObject m_hoImage = null;

        /// <summary>
        /// 掩模区域
        /// </summary>
        private HObject m_reg = null;

        /// <summary>
        /// 模板区域
        /// </summary>
        HObject m_ModelRegAffine = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public MaskForm()
        {
            InitializeComponent();
        }

        // load
        private void MaskForm_Load(object sender, EventArgs e)
        {
            tsp_OK.Enabled = false;
            pnl_Window.Controls.Add(m_Disp);
            m_Disp.Dock = DockStyle.Fill;
            m_Disp.EventMouseUpPoint += GetMousePosition;
            HOperatorSet.GenEmptyObj(out m_reg);
            HOperatorSet.SetSystem("clip_region", "true");
            if (m_method != null)
            {
                if (m_method.InputImage != null)
                {
                    m_hoImage = m_method.InputImage;
                   // HOperatorSet.CopyImage(m_method.InputImage, out m_hoImage);
                    m_Disp.HobjectToHimage(m_hoImage);
                    if (m_method.AffineHom != null && m_method.ModelReg != null)
                    {
                        HOperatorSet.AffineTransRegion(m_method.ModelReg, out m_ModelRegAffine, m_method.AffineHom, "nearest_neighbor");
                    }
                    else if (m_method.ModelReg != null)
                    {
                        HOperatorSet.CopyObj(m_method.ModelReg, out m_ModelRegAffine, 1, -1);
                    }
                    m_Disp.DispObj(m_ModelRegAffine, "green");
                    m_Disp.DispObj(m_method.MaskReg, "red");
                }
                cmb_disp.SelectedIndex = 0;
            }
        }

        // 接受拖动到该界面的数据
        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            string m_strPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (File.Exists(m_strPath))
            {
                try
                {
                    HOperatorSet.ReadImage(out m_hoImage, m_strPath);
                    m_Disp.HobjectToHimage(m_hoImage);
                    if (m_method.AffineHom != null && m_method.ModelReg != null)
                    {
                        HOperatorSet.AffineTransRegion(m_method.ModelReg, out m_ModelRegAffine, m_method.AffineHom, "nearest_neighbor");
                    }
                    else if (m_method.ModelReg != null)
                    {
                        HOperatorSet.CopyObj(m_method.ModelReg, out m_ModelRegAffine, 1, -1);
                    }
                    m_Disp.DispObj(m_ModelRegAffine, "green");
                    m_Disp.DispObj(m_method.MaskReg, "red");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请检查是否为图片类型！" + "\r" + ex.ToString());
                }
            }
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // 获取鼠标当前位置
        private void GetMousePosition(object sender, GVS.HalconDisp.ViewROI.Config.PointEventArgs e)
        {
            if (m_bPaint)
            {
                if ("ROIPolygon" == m_listRoi[m_listRoi.Count - 1].Type)
                {
                    m_polygon.ListPointRowPeak.Add(e.Row);
                    m_polygon.ListPointColPeak.Add(e.Col);
                    m_Disp.ViewWindowMethod.Repaint();
                }
            }
        }

        // 矩形
        private void tsp_Rect2_Click(object sender, EventArgs e)
        {
            tsp_OK.Enabled = false;
            m_rectangle2 = new GVS.HalconDisp.ViewWindow.Config.Rectangle2();
            m_Disp.ViewWindowMethod.GenRectangle2(m_rectangle2, ref m_listRoi);
            m_listRectPara.Add(m_rectangle2);
            cmb_disp.SelectedIndex = 0;
        }

        // 圆
        private void tsp_Circle_Click(object sender, EventArgs e)
        {
            tsp_OK.Enabled = false;
            m_Circle = new GVS.HalconDisp.ViewWindow.Config.Circle();
            m_Disp.ViewWindowMethod.GenCircle(m_Circle, ref m_listRoi);
            m_listCirclePara.Add(m_Circle);
            cmb_disp.SelectedIndex = 0;
        }

        // 多边形
        private void tsp_polygon_Click(object sender, EventArgs e)
        {
            cmb_disp.SelectedIndex = 0;
            m_bPaint = true;
            m_polygon = new GVS.HalconDisp.ViewWindow.Config.Polygon();
            m_Disp.ViewWindowMethod.GenPolygon(m_polygon, ref m_listRoi);
            UpdateCtrl();
        }

        // 完成绘制
        private void tsp_OK_Click(object sender, EventArgs e)
        {
            m_bPaint = false;
            tsp_polygon.Enabled = true;
            tsp_Circle.Enabled = true;
            tsp_Rect2.Enabled = true;
            tsp_OK.Enabled = true;
            tsp_Complete.Enabled = true;
            tsp_remove.Enabled = true;
            if (m_polygon.ListPointColPeak.Count() > 4)
            {
                m_listPolygonPara.Add(m_polygon);
            }
            tsp_OK.Enabled = false;
        }

        /// <summary>
        /// 更新控件状态
        /// </summary>
        private void UpdateCtrl()
        {
            tsp_polygon.Enabled = false;
            tsp_Circle.Enabled = false;
            tsp_Rect2.Enabled = false;
            tsp_OK.Enabled = true;
            tsp_Complete.Enabled = false;
            tsp_remove.Enabled = false;
        }

        // 确定
        private void tsp_Complete_Click(object sender, EventArgs e)
        {
            m_method.Para.ListRectPara = new List<GVS.HalconDisp.ViewWindow.Config.Rectangle2>();
            m_method.Para.ListPolygonPara = new List<GVS.HalconDisp.ViewWindow.Config.Polygon>();
            m_method.Para.ListCirclePara = new List<GVS.HalconDisp.ViewWindow.Config.Circle>();
            HTuple hvAffineHomInvert = null;
            if (null != m_method.AffineHom)
            {
                HOperatorSet.HomMat2dInvert(m_method.AffineHom, out hvAffineHomInvert);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Rectangle2 item in m_listRectPara)
            {
                m_method.Para.ParaROI.Rect2 = new GVS.HalconDisp.ViewWindow.Config.Rectangle2();
                m_method.Para.ParaROI.Rect2.CopyFrom(item, hvAffineHomInvert);
                m_method.Para.ListRectPara.Add(m_method.Para.ParaROI.Rect2);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Polygon item in m_listPolygonPara)
            {
                m_method.Para.ParaROI.Polygon = new GVS.HalconDisp.ViewWindow.Config.Polygon();
                m_method.Para.ParaROI.Polygon.CopyFrom(item, hvAffineHomInvert);
                m_method.Para.ListPolygonPara.Add(m_method.Para.ParaROI.Polygon);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Circle item in m_listCirclePara)
            {
                m_method.Para.ParaROI.Circle = new GVS.HalconDisp.ViewWindow.Config.Circle();
                m_method.Para.ParaROI.Circle.CopyFrom(item, hvAffineHomInvert);
                m_method.Para.ListCirclePara.Add(m_method.Para.ParaROI.Circle);
            }

            m_method.Run();
            cmb_disp.SelectedIndex = 1;
            cmb_disp_SelectedIndexChanged(null, null);
        }

        // 窗口选择
        private void cmb_disp_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Disp.HobjectToHimage(m_hoImage);
            m_Disp.DispObj(m_ModelRegAffine, "green");
            if (cmb_disp.SelectedIndex == 0)
            {
                m_listRoi = new List<GVS.HalconDisp.ViewWindow.Model.ROI>();
                m_listRectPara = new List<GVS.HalconDisp.ViewWindow.Config.Rectangle2>();
                m_listPolygonPara = new List<GVS.HalconDisp.ViewWindow.Config.Polygon>();
                m_listCirclePara = new List<GVS.HalconDisp.ViewWindow.Config.Circle>();
                foreach (GVS.HalconDisp.ViewWindow.Config.Rectangle2 item in m_method.Para.ListRectPara)
                {
                    m_rectangle2 = new GVS.HalconDisp.ViewWindow.Config.Rectangle2();
                    m_rectangle2.CopyFrom(item, m_method.AffineHom);
                    m_Disp.ViewWindowMethod.GenRectangle2(m_rectangle2, ref m_listRoi);
                    m_listRectPara.Add(m_rectangle2);
                }
                foreach (GVS.HalconDisp.ViewWindow.Config.Polygon item in m_method.Para.ListPolygonPara)
                {
                    if (item.ListPointColPeak.Count() > 4)
                    {
                        m_polygon = new GVS.HalconDisp.ViewWindow.Config.Polygon();
                        m_polygon.CopyFrom(item, m_method.AffineHom);
                        m_listPolygonPara.Add(m_polygon);
                        m_Disp.ViewWindowMethod.GenPolygon(m_polygon, ref m_listRoi);
                    }
                }
                foreach (GVS.HalconDisp.ViewWindow.Config.Circle item in m_method.Para.ListCirclePara)
                {
                    m_Circle = new GVS.HalconDisp.ViewWindow.Config.Circle();
                    m_Circle.CopyFrom(item, m_method.AffineHom);
                    m_Disp.ViewWindowMethod.GenCircle(m_Circle, ref m_listRoi);
                    m_listCirclePara.Add(m_Circle);
                }
            }
            else if (cmb_disp.SelectedIndex == 1)
            {
                if (null != m_method.MaskReg && m_method.MaskReg.CountObj() > 0)
                {
                    m_Disp.DispObj(m_method.MaskReg, "red");
                }
            }
        }

        // 清空
        private void tsp_remove_Click(object sender, EventArgs e)
        {
            m_Disp.ViewWindowMethod.RemoveAllROI(ref m_listRoi);
            m_listCirclePara.Clear();
            m_listPolygonPara.Clear();
            m_listRectPara.Clear();
            m_method.Para.ListRectPara = new List<GVS.HalconDisp.ViewWindow.Config.Rectangle2>();
            m_method.Para.ListPolygonPara = new List<GVS.HalconDisp.ViewWindow.Config.Polygon>();
            m_method.Para.ListCirclePara = new List<GVS.HalconDisp.ViewWindow.Config.Circle>();
            HTuple hvAffineHomInvert = null;
            if (null != m_method.AffineHom)
            {
                HOperatorSet.HomMat2dInvert(m_method.AffineHom, out hvAffineHomInvert);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Rectangle2 item in m_listRectPara)
            {
                m_method.Para.ParaROI.Rect2 = new GVS.HalconDisp.ViewWindow.Config.Rectangle2();
                m_method.Para.ParaROI.Rect2.CopyFrom(item, hvAffineHomInvert);
                m_method.Para.ListRectPara.Add(m_method.Para.ParaROI.Rect2);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Polygon item in m_listPolygonPara)
            {
                m_method.Para.ParaROI.Polygon = new GVS.HalconDisp.ViewWindow.Config.Polygon();
                m_method.Para.ParaROI.Polygon.CopyFrom(item, hvAffineHomInvert);
                m_method.Para.ListPolygonPara.Add(m_method.Para.ParaROI.Polygon);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Circle item in m_listCirclePara)
            {
                m_method.Para.ParaROI.Circle = new GVS.HalconDisp.ViewWindow.Config.Circle();
                m_method.Para.ParaROI.Circle.CopyFrom(item, hvAffineHomInvert);
                m_method.Para.ListCirclePara.Add(m_method.Para.ParaROI.Circle);
            }
            m_method.Run();
            cmb_disp.SelectedIndex = 0;
        }

        private void MaskForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            m_method.Para.ListRectPara = new List<GVS.HalconDisp.ViewWindow.Config.Rectangle2>();
            m_method.Para.ListPolygonPara = new List<GVS.HalconDisp.ViewWindow.Config.Polygon>();
            m_method.Para.ListCirclePara = new List<GVS.HalconDisp.ViewWindow.Config.Circle>();
            HTuple hvAffineHomInvert = null;
            if (null != m_method.AffineHom)
            {
                HOperatorSet.HomMat2dInvert(m_method.AffineHom, out hvAffineHomInvert);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Rectangle2 item in m_listRectPara)
            {
                m_method.Para.ParaROI.Rect2 = new GVS.HalconDisp.ViewWindow.Config.Rectangle2();
                m_method.Para.ParaROI.Rect2.CopyFrom(item, hvAffineHomInvert);
                m_method.Para.ListRectPara.Add(m_method.Para.ParaROI.Rect2);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Polygon item in m_listPolygonPara)
            {
                m_method.Para.ParaROI.Polygon = new GVS.HalconDisp.ViewWindow.Config.Polygon();
                m_method.Para.ParaROI.Polygon.CopyFrom(item, hvAffineHomInvert);
                m_method.Para.ListPolygonPara.Add(m_method.Para.ParaROI.Polygon);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Circle item in m_listCirclePara)
            {
                m_method.Para.ParaROI.Circle = new GVS.HalconDisp.ViewWindow.Config.Circle();
                m_method.Para.ParaROI.Circle.CopyFrom(item, hvAffineHomInvert);
                m_method.Para.ListCirclePara.Add(m_method.Para.ParaROI.Circle);
            }
        }
    }
}
