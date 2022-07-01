using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using ParaResultAll;
using SvMask;

namespace SvBlobTool
{
    public partial class BlobToolForm : Form
    {
        /// <summary>
        /// blob方法
        /// </summary>
        public BlobToolMethod m_BlobMethod;

        /// <summary>
        /// 显示窗口
        /// </summary>
        private GVS.HalconDisp.Control.HWindow_Final m_disp = new GVS.HalconDisp.Control.HWindow_Final();

        /// <summary>
        /// 图像
        /// </summary>
        private HObject m_hoImage = null;

        /// <summary>
        /// 创建区域组合的界面
        /// </summary>
        GVS.HalconDisp.Control.RegionTypeCtrl m_roiCreateType = new GVS.HalconDisp.Control.RegionTypeCtrl();

        /// <summary>
        /// 存储ROI
        /// </summary>
        private List<GVS.HalconDisp.ViewWindow.Model.ROI> m_listRoi = new List<GVS.HalconDisp.ViewWindow.Model.ROI>();

        /// <summary>
        /// 掩模方法类
        /// </summary>
        private SvMaskMethod m_MaskMethod = new SvMaskMethod();

        /// <summary>
        /// 是否统一软件使用
        /// </summary>
        public bool m_bTool = true;

        /// <summary>
        /// 是否第一次连接
        /// </summary>
        private bool m_first = false;

        /// <summary>
        /// 当前区域组合
        /// </summary>
        private GVS.HalconDisp.Control.RegionTypeParas m_CurrentRegionTypePara = new GVS.HalconDisp.Control.RegionTypeParas();

        public BlobToolForm()
        {
            InitializeComponent();
        }

        private void cmb_Modelthreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_Polarity.Enabled = true;
            switch (cmb_Modelthreshold.SelectedIndex)
            {
                case 0:
                    m_BlobMethod.ParaBlob.EnumSegmentMode = EnumModeSegment.硬阈值固定;
                    lbl_MaxThreshold.Visible = false;
                    num_MaxThreshold.Visible = false;
                    lbl_Threshold.Text = "阈值";
                    num_Threshold.Value = m_BlobMethod.ParaBlob.Threshold;
                    break;
                case 1:
                    m_BlobMethod.ParaBlob.EnumSegmentMode = EnumModeSegment.硬阈值相对;
                    cmb_Polarity.Enabled = false;
                    lbl_MaxThreshold.Visible = true;
                    num_MaxThreshold.Visible = true;
                    lbl_Threshold.Text = "低阈值";
                    num_Threshold.Value = m_BlobMethod.ParaBlob.MinThreshold;
                    num_MaxThreshold.Value = m_BlobMethod.ParaBlob.MaxThreshold;
                    break;
                case 2:
                    m_BlobMethod.ParaBlob.EnumSegmentMode = EnumModeSegment.软阈值固定;
                    lbl_MaxThreshold.Visible = true;
                    num_MaxThreshold.Visible = true;
                    lbl_Threshold.Text = "低阈值";
                    num_Threshold.Value = m_BlobMethod.ParaBlob.MinThreshold;
                    num_MaxThreshold.Value = m_BlobMethod.ParaBlob.MaxThreshold;
                    break;
            }
        }

        private void BlobToolForm_Load(object sender, EventArgs e)
        {
            pnl_disp.Controls.Clear();
            pnl_disp.Controls.Add(m_disp);
            m_disp.Dock = DockStyle.Fill; // 模块化软件使用

            if (m_BlobMethod.InputImage != null)
            {
                m_hoImage = m_BlobMethod.InputImage;
                m_disp.HobjectToHimage(m_hoImage);
            }
            UpdateParaToCtrl();
            cmb_disp.SelectedIndex = 0;
            cmb_disp_SelectedIndexChanged(null, null);

            // 模块化软件使用
            if (m_bTool)
            {
                UpdateInputeData();
                cmb_Row1.SelectedIndexChanged += Cmb_Row1_SelectedIndexChanged;
                for (int i = 0; i < cmb_Row1.Items.Count; i++)
                {
                    if (m_BlobMethod.ParaBlob.AffineHomSelectIndexName.Length > 0)
                    {
                        if (cmb_Row1.Items[i].ToString().Contains(m_BlobMethod.ParaBlob.AffineHomSelectIndexName))
                        {
                            cmb_Row1.SelectedIndex = i;
                            Cmb_Row1_SelectedIndexChanged(null, null);
                        }
                    }
                }

                // 区域类型使用 
                m_roiCreateType.SetRegTypeGroup(new GVS.HalconDisp.Control.RegionType[]
                                        { GVS.HalconDisp.Control.RegionType.矩形1,
                                      GVS.HalconDisp.Control.RegionType.矩形2,
                                      GVS.HalconDisp.Control.RegionType.圆,
                                      GVS.HalconDisp.Control.RegionType.椭圆,
                                      GVS.HalconDisp.Control.RegionType.圆环,
                                      GVS.HalconDisp.Control.RegionType.多边形,});
                if (null != m_BlobMethod.ParaBlob.BlobRoiPara.Reg)
                {
                    m_CurrentRegionTypePara.CopyFrom(m_BlobMethod.ParaBlob.BlobRoiPara, m_BlobMethod.AffineHom);
                    // m_roiCreateType.RegPara = m_CurrentRegionTypePara;
                }
                else
                {
                    m_CurrentRegionTypePara = new GVS.HalconDisp.Control.RegionTypeParas();
                    m_roiCreateType.RegPara = m_CurrentRegionTypePara;
                    m_roiCreateType.RegPara.RegType = GVS.HalconDisp.Control.RegionType.圆;
                    m_roiCreateType.RegPara.MyCircle.Row = 300;
                    m_roiCreateType.RegPara.MyCircle.Column = 300;
                }

                cmb_Row.SelectedIndexChanged += Cmb_Row_SelectedIndexChanged;
                cmb_Col1.SelectedIndexChanged += Cmb_Col1_SelectedIndexChanged;
                cmb_Radius.SelectedIndexChanged += Cmb_Radius_SelectedIndexChanged;
                for (int i = 0; i < cmb_Row.Items.Count; i++)
                {
                    if (m_BlobMethod.ParaBlob.RowSelectIndexName.Length > 0)
                    {
                        if (cmb_Row.Items[i].ToString().Contains(m_BlobMethod.ParaBlob.RowSelectIndexName))
                        {
                            cmb_Row.SelectedIndex = i;
                            Cmb_Row_SelectedIndexChanged(null, null);
                        }
                    }
                }

                for (int i = 0; i < cmb_Col1.Items.Count; i++)
                {
                    if (m_BlobMethod.ParaBlob.ColSelectIndexName.Length > 0)
                    {
                        if (cmb_Col1.Items[i].ToString().Contains(m_BlobMethod.ParaBlob.ColSelectIndexName))
                        {
                            cmb_Col1.SelectedIndex = i;
                            Cmb_Col1_SelectedIndexChanged(null, null);
                        }
                    }
                }
                for (int i = 0; i < cmb_Radius.Items.Count; i++)
                {
                    if (m_BlobMethod.ParaBlob.RadiusSelectIndexName.Length > 0)
                    {
                        if (cmb_Radius.Items[i].ToString().Contains(m_BlobMethod.ParaBlob.RadiusSelectIndexName))
                        {
                            cmb_Radius.SelectedIndex = i;
                            Cmb_Radius_SelectedIndexChanged(null, null);
                        }
                    }
                }
            }
            m_roiCreateType.RegPara = m_CurrentRegionTypePara;
            m_roiCreateType.HoImg = m_hoImage;
            m_roiCreateType.DispCtrl = this.m_disp;
            pnl_RegGroup.Controls.Add(m_roiCreateType);
            m_roiCreateType.Dock = DockStyle.Fill;
            m_roiCreateType.ListROI = m_listRoi;
            m_roiCreateType.DisplayEditReg = true;
            m_roiCreateType.UpdatePara();
            m_roiCreateType.GenReg();

            // blob检测区域
            m_BlobMethod.RegBlobReg = m_BlobMethod.ParaBlob.BlobRoiPara.Reg;
            m_first = false;
            pnl_Tool.Visible = m_bTool;
        }

        /// <summary>
        /// 更新传入数据
        /// </summary>
        private void UpdateInputeData()
        {
            cmb_Row1.Items.Clear();
            cmb_Row.Items.Clear();
            cmb_Radius.Items.Clear();
            cmb_Col1.Items.Clear();
            if (m_BlobMethod.ListClassParaResultAll.Count > 0)
            {
                for (int item = 0; item < m_BlobMethod.ListClassParaResultAll.Count; item++)
                {
                    if (m_BlobMethod.ListClassParaResultAll[item].ToolName.Contains("匹配工具" + item))
                    {
                        cmb_Row1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.PatMaxResultPara.Affinehom_" + "item.PatMaxResultPara.Affinehom");

                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.PatMaxResultPara.Row_" + m_BlobMethod.ListClassParaResultAll[item].PatMaxResultPara.Row);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.PatMaxResultPara.Col_" + m_BlobMethod.ListClassParaResultAll[item].PatMaxResultPara.Col);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.PatMaxResultPara.Angle_" + m_BlobMethod.ListClassParaResultAll[item].PatMaxResultPara.Angle);

                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.PatMaxResultPara.Row_" + m_BlobMethod.ListClassParaResultAll[item].PatMaxResultPara.Row);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.PatMaxResultPara.Col_" + m_BlobMethod.ListClassParaResultAll[item].PatMaxResultPara.Col);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.PatMaxResultPara.Angle_" + m_BlobMethod.ListClassParaResultAll[item].PatMaxResultPara.Angle);

                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.PatMaxResultPara.Row_" + m_BlobMethod.ListClassParaResultAll[item].PatMaxResultPara.Row);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.PatMaxResultPara.Col_" + m_BlobMethod.ListClassParaResultAll[item].PatMaxResultPara.Col);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.PatMaxResultPara.Angle_" + m_BlobMethod.ListClassParaResultAll[item].PatMaxResultPara.Angle);
                    }
                    if (m_BlobMethod.ListClassParaResultAll[item].ToolName.Contains("Blob工具" + item))
                    {
                        for (int i = 0; i < m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; i++)
                        {
                            cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Row_" + m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Col_" + m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Area_" + m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Area);
                            cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Row_" + m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Col_" + m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Area_" + m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Area);

                            cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Row_" + m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Col_" + m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Area_" + m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Area);
                        }
                    }
                    if (m_BlobMethod.ListClassParaResultAll[item].ToolName.Contains("找线工具" + item))
                    {
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.RowStart_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.RowStart);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.ColStart_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.ColStart);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.RowEnd_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.RowEnd);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.ColEnd_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.ColEnd);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.RowMiddle_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.RowMiddle);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.ColMiddle_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.ColMiddle);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.RowStart_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.RowStart);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.ColStart_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.ColStart);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.RowEnd_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.RowEnd);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.ColEnd_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.ColEnd);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.RowMiddle_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.RowMiddle);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.ColMiddle_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.ColMiddle);

                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.RowStart_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.RowStart);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.ColStart_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.ColStart);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.RowEnd_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.RowEnd);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.ColEnd_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.ColEnd);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.RowMiddle_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.RowMiddle);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindLineResultPara.ColMiddle_" + m_BlobMethod.ListClassParaResultAll[item].FindLineResultPara.ColMiddle);
                    }
                    if (m_BlobMethod.ListClassParaResultAll[item].ToolName.Contains("找圆工具" + item))
                    {
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindCircleResultPara.Row_" + m_BlobMethod.ListClassParaResultAll[item].FindCircleResultPara.Row);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindCircleResultPara.Col_" + m_BlobMethod.ListClassParaResultAll[item].FindCircleResultPara.Col);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindCircleResultPara.Radius_" + m_BlobMethod.ListClassParaResultAll[item].FindCircleResultPara.Radius);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindCircleResultPara.Row_" + m_BlobMethod.ListClassParaResultAll[item].FindCircleResultPara.Row);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindCircleResultPara.Col_" + m_BlobMethod.ListClassParaResultAll[item].FindCircleResultPara.Col);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindCircleResultPara.Radius_" + m_BlobMethod.ListClassParaResultAll[item].FindCircleResultPara.Radius);

                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindCircleResultPara.Row_" + m_BlobMethod.ListClassParaResultAll[item].FindCircleResultPara.Row);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindCircleResultPara.Col_" + m_BlobMethod.ListClassParaResultAll[item].FindCircleResultPara.Col);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.FindCircleResultPara.Radius_" + m_BlobMethod.ListClassParaResultAll[item].FindCircleResultPara.Radius);
                    }
                    if (m_BlobMethod.ListClassParaResultAll[item].ToolName.Contains("点线距离工具" + item))
                    {
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointLines.Row_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointLines.Row);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointLines.Col_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointLines.Col);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointLines.Distance_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointLines.Distance);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointLines.Row_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointLines.Row);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointLines.Col_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointLines.Col);
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointLines.Distance_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointLines.Distance);

                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointLines.Row_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointLines.Row);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointLines.Col_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointLines.Col);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointLines.Distance_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointLines.Distance);
                    }
                    if (m_BlobMethod.ListClassParaResultAll[item].ToolName.Contains("点点距离工具" + item))
                    {
                        cmb_Col1.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Distance_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointPoints.Distance);
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Distance_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointPoints.Distance);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Distance_" + m_BlobMethod.ListClassParaResultAll[item].ParaDistancePointPoints.Distance);
                    }
                    if (m_BlobMethod.ListClassParaResultAll[item].ToolName.Contains("AngleLX工具" + item))
                    {
                        cmb_Row.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParasAngleLXResult.Angle_" + m_BlobMethod.ListClassParaResultAll[item].ParasAngleLXResult.Angle);
                        cmb_Radius.Items.Add(m_BlobMethod.ListClassParaResultAll[item].ToolName + "item.ParasAngleLXResult.Angle_" + m_BlobMethod.ListClassParaResultAll[item].ParasAngleLXResult.Angle);
                    }
                }
            }
            cmb_Row1.Items.Add("null");
            cmb_Row.Items.Add("null");
            cmb_Col1.Items.Add("null");
            cmb_Radius.Items.Add("null");
        }

        private void Cmb_Radius_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Radius.Text.Trim();
            decimal d = 0;
            if (null != m_CurrentRegionTypePara.Reg)
            {
                switch (m_CurrentRegionTypePara.RegType)
                {
                    case GVS.HalconDisp.Control.RegionType.图片:
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形1:
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形2:
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆:
                        d = (decimal)m_CurrentRegionTypePara.MyCircle.Radius;
                        break;
                    case GVS.HalconDisp.Control.RegionType.多边形:
                        break;
                    case GVS.HalconDisp.Control.RegionType.椭圆:
                        d = (decimal)m_CurrentRegionTypePara.MyEllipse.Radius1;
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆环:
                        d = (decimal)m_CurrentRegionTypePara.MyCircularAnnulusSection.InnerRadius;
                        break;
                    default:
                        break;
                }
            }
            if (strName != "null" && strName.Contains("匹配工具"))
            {
                if (strName.Contains("item.PatMaxResultPara.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.PatMaxResultPara.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.PatMaxResultPara.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("找线工具"))
            {
                if (strName.Contains("item.FindLineResultPara.RowStart"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.RowEnd"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColEnd"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColStart"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColMiddle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.RowMiddle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("找圆工具"))
            {
                if (strName.Contains("item.FindCircleResultPara.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindCircleResultPara.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindCircleResultPara.Radius"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_BlobMethod.ListClassParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
                    {
                        string str = "item.BlobResultPara.ListBlobResultPara[" + iCount + "].Col";
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Row"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Col"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Area"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                    }
                }
            }
            if (strName != "null" && strName.Contains("点线距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointLines.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointLines.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointLines.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (null != m_CurrentRegionTypePara.Reg)
            {
                switch (m_CurrentRegionTypePara.RegType)
                {
                    case GVS.HalconDisp.Control.RegionType.图片:
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形1:

                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形2:
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆:
                        m_CurrentRegionTypePara.MyCircle.Radius = (double)d;
                        break;
                    case GVS.HalconDisp.Control.RegionType.多边形:
                        break;
                    case GVS.HalconDisp.Control.RegionType.椭圆:
                        m_CurrentRegionTypePara.MyEllipse.Radius1 = (double)d;
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆环:
                        m_CurrentRegionTypePara.MyCircularAnnulusSection.InnerRadius = (double)d;
                        break;
                    default:
                        break;
                }
            }
            //if (!m_first)
            {
                m_roiCreateType.UpdatePara();
                m_roiCreateType.GenReg();
                AffineParaToBefore();
            }
            if (cmb_Radius.Text.Trim() != "null")
            {
                m_BlobMethod.ParaBlob.RadiusSelectIndexName = cmb_Radius.Text.Trim().Substring(0, cmb_Radius.Text.Trim().LastIndexOf('_'));
            }
            else
            {
                m_BlobMethod.ParaBlob.RadiusSelectIndexName = "null";
            }
        }

        // 仿射矩阵
        private void Cmb_Row1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Row1.Text.Trim();
            if (strName == "null")
            {
                m_BlobMethod.AffineHom = null;
                m_BlobMethod.ParaBlob.AffineHomSelectIndexName = "null";
            }
            else
            {
                foreach (ParasResultAll item in m_BlobMethod.ListClassParaResultAll)
                {
                    if (item.ToolName.Contains("匹配工具"))
                    {
                        m_BlobMethod.AffineHom = item.PatMaxResultPara.Affinehom;
                    }
                }
                m_BlobMethod.ParaBlob.AffineHomSelectIndexName = cmb_Row1.Text.Trim().Substring(0, cmb_Row1.Text.Trim().LastIndexOf('_'));
            }
        }

        // 中心Col
        private void Cmb_Col1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox ComBoxName = (ComboBox)sender;
            string strName = cmb_Col1.Text.Trim();
            decimal d = 0;
            if (null != m_CurrentRegionTypePara.Reg)
            {
                switch (m_CurrentRegionTypePara.RegType)
                {
                    case GVS.HalconDisp.Control.RegionType.图片:
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形1:
                        d = (decimal)m_CurrentRegionTypePara.MyRect1.ArrayCol[0];
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形2:
                        d = (decimal)m_CurrentRegionTypePara.MyRect2.ArrayCol[0];
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆:
                        d = (decimal)m_CurrentRegionTypePara.MyCircle.Column;
                        break;
                    case GVS.HalconDisp.Control.RegionType.多边形:
                        break;
                    case GVS.HalconDisp.Control.RegionType.椭圆:
                        d = (decimal)m_CurrentRegionTypePara.MyEllipse.Column;
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆环:
                        d = (decimal)m_CurrentRegionTypePara.MyCircularAnnulusSection.CenterCol;
                        break;
                    default:
                        break;
                }
            }
            if (strName != "null" && strName.Contains("匹配工具"))
            {
                if (strName.Contains("item.PatMaxResultParaBlob.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.PatMaxResultPara.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.PatMaxResultPara.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("找线工具"))
            {
                if (strName.Contains("item.FindLineResultPara.RowStart"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.RowEnd"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColEnd"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColStart"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColMiddle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.RowMiddle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("找圆工具"))
            {
                if (strName.Contains("item.FindCircleResultPara.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindCircleResultPara.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindCircleResultPara.Radius"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_BlobMethod.ListClassParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
                    {
                        string str = "item.BlobResultPara.ListBlobResultPara[" + iCount + "].Col";
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Row"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Col"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Area"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                    }
                }
            }
            if (strName != "null" && strName.Contains("点线距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointLines.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointLines.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointLines.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (null != m_CurrentRegionTypePara.Reg)
            {
                switch (m_CurrentRegionTypePara.RegType)
                {
                    case GVS.HalconDisp.Control.RegionType.图片:
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形1:
                        m_CurrentRegionTypePara.MyRect1.ArrayCol[0] = (double)d;
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形2:
                        m_CurrentRegionTypePara.MyRect2.ArrayCol[0] = (double)d;
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆:
                        m_CurrentRegionTypePara.MyCircle.Column = (double)d;
                        break;
                    case GVS.HalconDisp.Control.RegionType.多边形:
                        break;
                    case GVS.HalconDisp.Control.RegionType.椭圆:
                        m_CurrentRegionTypePara.MyEllipse.Column = (double)d;
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆环:
                        m_CurrentRegionTypePara.MyCircularAnnulusSection.CenterCol = (double)d;
                        break;
                    default:
                        break;
                }
            }
           // if (!m_first)
            {
                m_roiCreateType.UpdatePara();
                m_roiCreateType.GenReg();
                AffineParaToBefore();
            }
            if (cmb_Col1.Text.Trim() != "null")
            {
                m_BlobMethod.ParaBlob.ColSelectIndexName = cmb_Col1.Text.Trim().Substring(0, cmb_Col1.Text.Trim().LastIndexOf('_'));
            }
            else
            {
                m_BlobMethod.ParaBlob.ColSelectIndexName = "null";
            }
        }

        // 中心Row
        private void Cmb_Row_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox ComBoxName = (ComboBox)sender;
            string strName = cmb_Row.Text.Trim();
            decimal d = 0;
            if (null != m_CurrentRegionTypePara.Reg)
            {
                switch (m_CurrentRegionTypePara.RegType)
                {
                    case GVS.HalconDisp.Control.RegionType.图片:
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形1:
                        d = (decimal)m_CurrentRegionTypePara.MyRect1.ArrayRow[0];
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形2:
                        d = (decimal)m_CurrentRegionTypePara.MyRect2.ArrayRow[0];
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆:
                        d = (decimal)m_CurrentRegionTypePara.MyCircle.Row;
                        break;
                    case GVS.HalconDisp.Control.RegionType.多边形:
                        break;
                    case GVS.HalconDisp.Control.RegionType.椭圆:
                        d = (decimal)m_CurrentRegionTypePara.MyEllipse.Row;
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆环:
                        d = (decimal)m_CurrentRegionTypePara.MyCircularAnnulusSection.CenterRow;
                        break;
                    default:
                        break;
                }
            }
            if (strName != "null" && strName.Contains("匹配工具"))
            {
                if (strName.Contains("item.PatMaxResultPara.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.PatMaxResultPara.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.PatMaxResultPara.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("找线工具"))
            {
                if (strName.Contains("item.FindLineResultPara.RowStart"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.RowEnd"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColEnd"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColStart"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColMiddle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.RowMiddle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("找圆工具"))
            {
                if (strName.Contains("item.FindCircleResultPara.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindCircleResultPara.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindCircleResultPara.Radius"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_BlobMethod.ListClassParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_BlobMethod.ListClassParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
                    {
                        string str = "item.BlobResultPara.ListBlobResultPara[" + iCount + "].Col";
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Row"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Col"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Area"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                    }
                }
            }
            if (strName != "null" && strName.Contains("点线距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointLines.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointLines.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointLines.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != "null" && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }

            if (null != m_CurrentRegionTypePara.Reg)
            {
                switch (m_CurrentRegionTypePara.RegType)
                {
                    case GVS.HalconDisp.Control.RegionType.图片:
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形1:
                        m_CurrentRegionTypePara.MyRect1.ArrayRow[0] = (double)d;
                        break;
                    case GVS.HalconDisp.Control.RegionType.矩形2:
                        m_CurrentRegionTypePara.MyRect2.ArrayRow[0] = (double)d;
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆:
                        m_CurrentRegionTypePara.MyCircle.Row = (double)d;
                        break;
                    case GVS.HalconDisp.Control.RegionType.多边形:
                        break;
                    case GVS.HalconDisp.Control.RegionType.椭圆:
                        m_CurrentRegionTypePara.MyEllipse.Row = (double)d;
                        break;
                    case GVS.HalconDisp.Control.RegionType.圆环:
                        m_CurrentRegionTypePara.MyCircularAnnulusSection.CenterRow = (double)d;
                        break;
                    default:
                        break;
                }
            }
            //if (!m_first)
            {
                m_roiCreateType.UpdatePara();
                m_roiCreateType.GenReg();
                AffineParaToBefore();
            }
            if (cmb_Row.Text.Trim() != "null")
            {
                m_BlobMethod.ParaBlob.RowSelectIndexName = cmb_Row.Text.Trim().Substring(0, cmb_Row.Text.Trim().LastIndexOf('_'));
            }
            else
            {
                m_BlobMethod.ParaBlob.RowSelectIndexName = "null";
            }
        }

        /// <summary>
        /// 将参数仿射回初始值
        /// </summary>
        private void AffineParaToBefore()
        {
            HTuple hvAffineInvert = null;
            if (m_BlobMethod.AffineHom != null)
            {
                HOperatorSet.HomMat2dInvert(m_BlobMethod.AffineHom, out hvAffineInvert);
            }
            m_BlobMethod.ParaBlob.BlobRoiPara = new GVS.HalconDisp.Control.RegionTypeParas();
            m_BlobMethod.ParaBlob.BlobRoiPara.CopyFrom(m_CurrentRegionTypePara, hvAffineInvert);
        }

        /// <summary>
        /// 将数据更新至控件
        /// </summary>
        private void UpdateParaToCtrl()
        {
            num_MinArea.Value = (decimal)m_BlobMethod.ParaBlob.MinArea;
            switch (m_BlobMethod.ParaBlob.EnumConnectionClear)
            {
                case EnumClear.无:
                    cmb_ConnectionDelete.SelectedIndex = 0;
                    break;
                case EnumClear.修剪:
                    cmb_ConnectionDelete.SelectedIndex = 1;
                    break;
                case EnumClear.填充:
                    cmb_ConnectionDelete.SelectedIndex = 2;
                    break;
            }
            cmb_Polarity.Enabled = true;
            switch (m_BlobMethod.ParaBlob.EnumSegmentMode)
            {
                case EnumModeSegment.硬阈值固定:
                    cmb_Modelthreshold.SelectedIndex = 0;
                    lbl_Threshold.Text = "阈值";
                    num_Threshold.Value = m_BlobMethod.ParaBlob.Threshold;
                    lbl_MaxThreshold.Visible = false;
                    num_MaxThreshold.Visible = false;
                    break;
                case EnumModeSegment.硬阈值相对:
                    cmb_Polarity.Enabled = false;
                    cmb_Modelthreshold.SelectedIndex = 1;
                    lbl_Threshold.Text = "低阈值";
                    num_Threshold.Value = m_BlobMethod.ParaBlob.MinThreshold;
                    lbl_MaxThreshold.Visible = true;
                    num_MaxThreshold.Visible = true;
                    num_MaxThreshold.Value = m_BlobMethod.ParaBlob.MaxThreshold;
                    break;
                case EnumModeSegment.软阈值固定:
                    cmb_Modelthreshold.SelectedIndex = 2;
                    lbl_Threshold.Text = "低阈值";
                    num_Threshold.Value = m_BlobMethod.ParaBlob.MinThreshold;
                    lbl_MaxThreshold.Visible = true;
                    num_MaxThreshold.Visible = true;
                    num_MaxThreshold.Value = m_BlobMethod.ParaBlob.MaxThreshold;
                    break;
            }

            switch (m_BlobMethod.ParaBlob.EnumSegmentPolarity)
            {
                case EnumPolarity.白底黑点:
                    cmb_Polarity.SelectedIndex = 1;
                    break;
                case EnumPolarity.黑底白点:
                    cmb_Polarity.SelectedIndex = 0;
                    break;
            }

            switch (m_BlobMethod.ParaBlob.EnumConnectionMode)
            {
                case EnumModeConnection.灰度:
                    cmb_ConnectionMode.SelectedIndex = 0;
                    break;
            }

            UpdateMorphology();

            chk_SelectPoint.Checked = m_BlobMethod.ParaBlob.SelectPoint;
            chk_PointCover.Checked = m_BlobMethod.ParaBlob.PointConver;
            chk_DispAllPoint.Checked = m_BlobMethod.ParaBlob.AllPoint;
            chk_Center.Checked = m_BlobMethod.ParaBlob.AreaCenter;
            chk_DispBorder.Checked = m_BlobMethod.ParaBlob.Border;
            chk_显示未过滤斑点数据.Checked = m_BlobMethod.ParaBlob.DispSelectData;
            tsp_msg.Text = m_BlobMethod.RunMsg;
            tsp_Time.Text = m_BlobMethod.RunTime + "ms";
        }

        private void UpdateMorphology()
        {
            dgv_Morphology.Rows.Clear();
            foreach (var item in m_BlobMethod.ParaBlob.ListMorPhology)
            {
                string strMorphology = item.ToString();
                string[] cMorphology = strMorphology.Split('.');
                dgv_Morphology.Rows.Add(cMorphology[0]);
            }
        }

        private void cmb_Polarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_Polarity.SelectedIndex)
            {
                case 0:
                    m_BlobMethod.ParaBlob.EnumSegmentPolarity = EnumPolarity.黑底白点;
                    break;
                case 1:
                    m_BlobMethod.ParaBlob.EnumSegmentPolarity = EnumPolarity.白底黑点;
                    break;
            }
        }

        private void num_Threshold_ValueChanged(object sender, EventArgs e)
        {
            switch (m_BlobMethod.ParaBlob.EnumSegmentMode)
            {
                case EnumModeSegment.硬阈值固定:
                    m_BlobMethod.ParaBlob.Threshold = (int)num_Threshold.Value;
                    break;
                case EnumModeSegment.软阈值固定:
                    m_BlobMethod.ParaBlob.MinThreshold = (int)num_Threshold.Value;
                    break;
                case EnumModeSegment.硬阈值相对:
                    m_BlobMethod.ParaBlob.MinThreshold = (int)num_Threshold.Value;
                    break;

            }
        }

        private void num_MaxThreshold_ValueChanged(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.MaxThreshold = (int)num_MaxThreshold.Value;
        }

        private void cmb_ConnectionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_ConnectionMode.SelectedIndex)
            {
                case 0:
                    m_BlobMethod.ParaBlob.EnumConnectionMode = EnumModeConnection.灰度;
                    break;
            }
        }

        private void cmb_ConnectionDelete_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_minArea.Enabled = false;
            num_MinArea.Enabled = false;
            switch (cmb_ConnectionDelete.SelectedIndex)
            {
                case 0:
                    m_BlobMethod.ParaBlob.EnumConnectionClear = EnumClear.无;
                    break;
                case 1:
                    m_BlobMethod.ParaBlob.EnumConnectionClear = EnumClear.修剪;
                    lbl_minArea.Enabled = true;
                    num_MinArea.Enabled = true;
                    break;
                case 2:
                    m_BlobMethod.ParaBlob.EnumConnectionClear = EnumClear.填充;
                    lbl_minArea.Enabled = true;
                    num_MinArea.Enabled = true;
                    break;

            }
        }

        private void num_MinArea_ValueChanged(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.MinArea = (double)num_MinArea.Value;
        }

        /// <summary>
        /// 更新形态学列表
        /// </summary>
        private void UpdateDataGridView()
        {
            for (int item = 0; item < m_BlobMethod.ParaBlob.ListMorPhology.Count; item++)
            {
                dgv_Morphology.Rows[item].Selected = false;
            }
            dgv_Morphology.Rows[dgv_Morphology.RowCount - 1].Selected = true;
        }

        private void 侵蚀水平面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.侵蚀水平面;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("侵蚀水平面");
            UpdateDataGridView();
        }

        private void 侵蚀垂直面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.侵蚀垂直面;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("侵蚀垂直面");
            UpdateDataGridView();
        }

        private void 侵蚀正方形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.侵蚀正方形;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("侵蚀正方形");
            UpdateDataGridView();
        }

        private void 扩大水平面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.扩大水平面;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("扩大水平面");
            UpdateDataGridView();
        }

        private void 扩大垂直面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.扩大垂直面;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("扩大垂直面");
            UpdateDataGridView();
        }

        private void 扩大正方形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.扩大正方形;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("扩大正方形");
            UpdateDataGridView();
        }

        private void 关闭水平面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.关闭水平面;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("关闭水平面");
            UpdateDataGridView();
        }

        private void 关闭垂直面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.关闭垂直面;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("关闭垂直面");
            UpdateDataGridView();
        }

        private void 关闭正方形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.关闭正方形;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("关闭正方形");
            UpdateDataGridView();
        }

        private void 打开水平面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.打开水平面;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("打开水平面");
            UpdateDataGridView();
        }

        private void 打开垂直面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.打开垂直面;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("打开垂直面");
            UpdateDataGridView();
        }

        private void 打开正方形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.EnumMorphologys = EnumMorphology.打开正方形;
            m_BlobMethod.ParaBlob.ListMorPhology.Add(m_BlobMethod.ParaBlob.EnumMorphologys);
            dgv_Morphology.Rows.Add("打开正方形");
            UpdateDataGridView();
        }

        private void chk_DispBorder_CheckedChanged(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.Border = chk_DispBorder.Checked;
            UpdateResult();
        }

        private void chk_Center_CheckedChanged(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.AreaCenter = chk_Center.Checked;
            UpdateResult();
        }

        private void chk_DispAllPoint_CheckedChanged(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.AllPoint = chk_DispAllPoint.Checked;
            UpdateResult();
        }

        private void chk_SelectPoint_CheckedChanged(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.SelectPoint = chk_SelectPoint.Checked;
            UpdateResult();
        }

        private void chk_PointCover_CheckedChanged(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.PointConver = chk_PointCover.Checked;
            UpdateResult();
        }

        private void tsp_Mask_Click(object sender, EventArgs e)
        {
            AffineParaToBefore();
            m_BlobMethod.RegBlobReg = m_BlobMethod.ParaBlob.BlobRoiPara.Reg;
            SvMask.MaskForm dlg = new SvMask.MaskForm();
            m_MaskMethod.Para = m_BlobMethod.ParaBlob.MaskPara;
            m_MaskMethod.InputImage = m_hoImage;
            m_MaskMethod.ModelReg = m_BlobMethod.ParaBlob.BlobRoiPara.Reg;
            dlg.m_method = this.m_MaskMethod;
            dlg.m_method.AffineHom = m_BlobMethod.AffineHom;
            dlg.ShowDialog();
        }

        private void tsp_delete_Click(object sender, EventArgs e)
        {
            if (dgv_Morphology.CurrentRow != null)
            {
                m_BlobMethod.ParaBlob.ListMorPhology.RemoveAt(dgv_Morphology.CurrentRow.Index);
                dgv_Morphology.Rows.RemoveAt(dgv_Morphology.CurrentRow.Index);
            }
        }

        private void tsp_MoveUp_Click(object sender, EventArgs e)
        {
            if (dgv_Morphology.CurrentRow.Index != 0)
            {
                int iIndex = dgv_Morphology.CurrentRow.Index - 1;
                EnumMorphology EnumMorphologys1 = m_BlobMethod.ParaBlob.ListMorPhology[dgv_Morphology.CurrentRow.Index];
                EnumMorphology EnumMorphologys2 = m_BlobMethod.ParaBlob.ListMorPhology[dgv_Morphology.CurrentRow.Index - 1];
                m_BlobMethod.ParaBlob.ListMorPhology[dgv_Morphology.CurrentRow.Index - 1] = EnumMorphologys1;
                m_BlobMethod.ParaBlob.ListMorPhology[dgv_Morphology.CurrentRow.Index] = EnumMorphologys2;
                UpdateMorphology();
                for (int item = 0; item < m_BlobMethod.ParaBlob.ListMorPhology.Count; item++)
                {
                    dgv_Morphology.Rows[item].Selected = false;
                }
                dgv_Morphology.Rows[iIndex].Selected = true;
            }
        }

        private void tsp_MoveDown_Click(object sender, EventArgs e)
        {
            if (dgv_Morphology.CurrentRow.Index != m_BlobMethod.ParaBlob.ListMorPhology.Count - 1)
            {
                int iIndex = dgv_Morphology.CurrentRow.Index + 1;
                EnumMorphology EnumMorphologys1 = m_BlobMethod.ParaBlob.ListMorPhology[dgv_Morphology.CurrentRow.Index];
                EnumMorphology EnumMorphologys2 = m_BlobMethod.ParaBlob.ListMorPhology[dgv_Morphology.CurrentRow.Index + 1];
                m_BlobMethod.ParaBlob.ListMorPhology[dgv_Morphology.CurrentRow.Index + 1] = EnumMorphologys1;
                m_BlobMethod.ParaBlob.ListMorPhology[dgv_Morphology.CurrentRow.Index] = EnumMorphologys2;
                UpdateMorphology();
                for (int item = 0; item < m_BlobMethod.ParaBlob.ListMorPhology.Count; item++)
                {
                    dgv_Morphology.Rows[item].Selected = false;
                }
                dgv_Morphology.Rows[iIndex].Selected = true;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AffineParaToBefore();
            bool bState = m_BlobMethod.Run();
            cmb_disp.SelectedIndex = 1;
            UpdateResult();
            UpdateResultDgv();
            tsp_msg.Text = m_BlobMethod.RunMsg;
            tsp_Time.Text = m_BlobMethod.RunTime + "ms";
        }

        private void BlobToolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AffineParaToBefore();
        }

        private void cmb_disp_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_disp.HobjectToHimage(m_hoImage);
            switch (cmb_disp.SelectedIndex)
            {
                case 0:
                    m_roiCreateType.GenReg();
                    break;
                case 1:
                    UpdateResult();
                    break;
            }
        }
        private void UpdateResult()
        {
            cmb_disp.SelectedIndex = 1;
            m_disp.HobjectToHimage(m_hoImage);
            if (m_BlobMethod.ParaBlob.Border)
            {
                foreach (ParaResult item in m_BlobMethod.ListParaResultAll)
                {
                    m_disp.DispObj(item.HoPointReg, "green", "margin");
                }
                foreach (ParaResult item in m_BlobMethod.ListParaResultSelect)
                {
                    m_disp.DispObj(item.HoPointReg, "blue", "margin");
                }
            }
            if (m_BlobMethod.ParaBlob.AllPoint)
            {
                foreach (ParaResult item in m_BlobMethod.ListParaResultAll)
                {
                    m_disp.DispObj(item.HoPointReg, "cyan", "margin");
                }
            }

            if (m_BlobMethod.ParaBlob.AreaCenter)
            {
                foreach (ParaResult item in m_BlobMethod.ListParaResultAll)
                {
                    m_disp.DispObj(item.HoCross, "red", "margin");
                }
            }
            if (m_BlobMethod.ParaBlob.PointConver)
            {
                foreach (ParaResult item in m_BlobMethod.ListParaResultAll)
                {
                    m_disp.DispObj(item.HoPointReg, "orange", "fill");
                }
                foreach (ParaResult item in m_BlobMethod.ListParaResultSelect)
                {
                    m_disp.DispObj(item.HoPointReg, "blue", "margin");
                }
            }
            if (m_BlobMethod.ParaBlob.SelectPoint)
            {
                foreach (ParaResult item in m_BlobMethod.ListParaResultSelect)
                {
                    m_disp.DispObj(item.HoPointReg, "blue", "margin");
                    m_disp.DispObj(item.HoCross, "red", "margin");
                    m_disp.DispObj(item.HoCross, "green", "fill");

                }
            }
        }

        private void UpdateResultDgv()
        {
            int i = 0;
            dgv_result.Rows.Clear();
            if (m_BlobMethod.ParaBlob.DispSelectData)
            {
                foreach (ParaResult item in m_BlobMethod.ListParaResultSelect)
                {
                    i++;
                    dgv_result.Rows.Add(i, item.Area.ToString("0.0"), item.Row.ToString("0.0"), item.Col.ToString("0.0"));
                }
            }
            else
            {
                foreach (ParaResult item in m_BlobMethod.ListParaResultAll)
                {
                    i++;
                    dgv_result.Rows.Add(i, item.Area.ToString("0.0"), item.Row.ToString("0.0"), item.Col.ToString("0.0"));
                }
            }
        }

        private void chk_显示未过滤斑点数据_CheckedChanged(object sender, EventArgs e)
        {
            m_BlobMethod.ParaBlob.DispSelectData = chk_显示未过滤斑点数据.Checked;
            UpdateResultDgv();
        }

        private void dgv_result_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_disp.HobjectToHimage(m_hoImage);
            if (dgv_result != null && dgv_result.Rows!=null&&dgv_result.Rows.Count > 0)
            {
                if (m_BlobMethod.ParaBlob.DispSelectData)
            {
                dgv_result.Rows[dgv_result.CurrentRow.Index].Selected = true;
                m_disp.DispObj(m_BlobMethod.ListParaResultSelect[dgv_result.CurrentRow.Index].HoPointReg, "magenta");
                m_disp.DispObj(m_BlobMethod.ListParaResultSelect[dgv_result.CurrentRow.Index].HoCross, "yellow");
                m_disp.DisplayMessage("面积：" + m_BlobMethod.ListParaResultSelect[dgv_result.CurrentRow.Index].Area.ToString("0.0"),
                                     (int)m_BlobMethod.ListParaResultSelect[dgv_result.CurrentRow.Index].Row,
                                     (int)m_BlobMethod.ListParaResultSelect[dgv_result.CurrentRow.Index].Col,
                                     "blue", false);
            }
            else
            {
                
                    dgv_result.Rows[dgv_result.CurrentRow.Index].Selected = true;
                    m_disp.DispObj(m_BlobMethod.ListParaResultAll[dgv_result.CurrentRow.Index].HoPointReg, "magenta");
                    m_disp.DispObj(m_BlobMethod.ListParaResultAll[dgv_result.CurrentRow.Index].HoCross, "yellow");
                    m_disp.DisplayMessage("面积：" + m_BlobMethod.ListParaResultAll[dgv_result.CurrentRow.Index].Area.ToString("0.0"),
                                     (int)m_BlobMethod.ListParaResultAll[dgv_result.CurrentRow.Index].Row,
                                     (int)m_BlobMethod.ListParaResultAll[dgv_result.CurrentRow.Index].Col,
                                     "blue", false);
                }
            }
        }
    }
}
