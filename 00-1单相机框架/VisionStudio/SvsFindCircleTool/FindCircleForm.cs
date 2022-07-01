using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ParaResultAll;
using System.Globalization;

namespace SvsFindCircleTool
{
    public partial class FindCircleForm : Form
    {
        /// <summary>
        /// 找圆方法类
        /// </summary>
        public FindCircleMethod m_method = null;

        /// <summary>
        /// 圆卡尺
        /// </summary>
        private GVS.HalconDisp.ViewWindow.Config.CircleCalliper m_circleCalliperCurrent = new GVS.HalconDisp.ViewWindow.Config.CircleCalliper();

        /// <summary>
        /// 显示控件
        /// </summary>
        private GVS.HalconDisp.Control.HWindow_Final m_Disp = new GVS.HalconDisp.Control.HWindow_Final();

        /// <summary>
        /// 存储ROI
        /// </summary>
        private List<GVS.HalconDisp.ViewWindow.Model.ROI> m_listRoi = new List<GVS.HalconDisp.ViewWindow.Model.ROI>();

        // 输入图像
        private HObject m_hoImage = null;

        /// <summary>
        /// 是否统一软件使用
        /// </summary>
        public bool m_bTool = true;

        /// <summary>
        /// 构造方法
        /// </summary>
        public FindCircleForm()
        {
            InitializeComponent();
        }

        // 窗体加载
        private void FindCircleForm_Load(object sender, EventArgs e)
        {
            pnl_Disp.Controls.Add(m_Disp);
            m_Disp.Dock = DockStyle.Fill;
            cmb_Disp.SelectedIndex = 0;

            // 模块化软件使用
            if (m_bTool)
            {
                UpdateInputeData();
                cmb_Row1.SelectedIndexChanged += Cmb_Row1_SelectedIndexChanged;
                cmb_Row.SelectedIndexChanged += Cmb_Row_SelectedIndexChanged;
                cmb_Col1.SelectedIndexChanged += Cmb_Col1_SelectedIndexChanged;
                cmb_Radius.SelectedIndexChanged += Cmb_Radius_SelectedIndexChanged;
                for (int i = 0; i < cmb_Row1.Items.Count; i++)
                {
                    if (m_method.Para.AffineHomSelectIndexName.Length > 0)
                    {
                        if (cmb_Row1.Items[i].ToString().Contains(m_method.Para.AffineHomSelectIndexName))
                        {
                            cmb_Row1.SelectedIndex = i;
                            Cmb_Row1_SelectedIndexChanged(null, null);
                        }
                    }
                }

                for (int i = 0; i < cmb_Row.Items.Count; i++)
                {
                    if (m_method.Para.RowSelectIndexName.Length > 0)
                    {
                        if (cmb_Row.Items[i].ToString().Contains(m_method.Para.RowSelectIndexName))
                        {
                            cmb_Row.SelectedIndex = i;
                            Cmb_Row_SelectedIndexChanged(null, null);
                        }
                    }
                }

                for (int i = 0; i < cmb_Col1.Items.Count; i++)
                {
                    if (m_method.Para.ColSelectIndexName.Length > 0)
                    {
                        if (cmb_Col1.Items[i].ToString().Contains(m_method.Para.ColSelectIndexName))
                        {
                            cmb_Col1.SelectedIndex = i;
                            Cmb_Col1_SelectedIndexChanged(null, null);
                        }
                    }
                }
                for (int i = 0; i < cmb_Radius.Items.Count; i++)
                {
                    if (m_method.Para.RadiusSelectIndexName.Length > 0)
                    {
                        if (cmb_Radius.Items[i].ToString().Contains(m_method.Para.RadiusSelectIndexName))
                        {
                            cmb_Radius.SelectedIndex = i;
                            Cmb_Radius_SelectedIndexChanged(null, null);
                        }
                    }
                }
            }
            if (null != m_method.InputImage)
            {
                HOperatorSet.CopyImage(m_method.InputImage, out m_hoImage);
                m_Disp.HobjectToHimage(m_hoImage);
            }
            m_circleCalliperCurrent.CopyFrom(m_method.Para.CircleCalliper, m_method.AffineHom);
            UpdateParaToCtrl();
            m_Disp.ViewWindowMethod.GenCircleCalliper(m_circleCalliperCurrent, ref m_listRoi);
            m_Disp.HWindowCtrl.MouseUp += HWindowCtrl_MouseUp;
            lbl_Time.Text = "运行时间：" + m_method.RunTime + "ms";
            cmb_Disp.SelectedIndex = 0;
            pnl_Tool.Visible = m_bTool;
        }

        private void Cmb_Radius_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Radius.Text.Trim();
            decimal d = (decimal)m_method.Para.CircleCalliper.Radius;
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
                for (int item = 0; item < m_method.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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

            numUD_Col2.Value = d;
            if (cmb_Radius.Text.Trim() != "null")
            {
                m_method.Para.RadiusSelectIndexName = cmb_Radius.Text.Trim().Substring(0, cmb_Radius.Text.Trim().LastIndexOf('_'));
            }
            else
            {
                m_method.Para.RadiusSelectIndexName = "null";
            }
        }

        // 仿射矩阵
        private void Cmb_Row1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Row1.Text.Trim();
            if (strName == "null")
            {
                m_method.AffineHom = null;
                m_method.Para.AffineHomSelectIndexName = "null";
            }
            else
            {
                foreach (ParasResultAll item in m_method.ListParaResultAll)
                {
                    if (item.ToolName.Contains("匹配工具"))
                    {
                        m_method.AffineHom = item.PatMaxResultPara.Affinehom;
                    }
                }
                m_method.Para.AffineHomSelectIndexName = cmb_Row1.Text.Trim().Substring(0, cmb_Row1.Text.Trim().LastIndexOf('_'));
            }
        }

        // 中心Col
        private void Cmb_Col1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox ComBoxName = (ComboBox)sender;
            string strName = cmb_Col1.Text.Trim();
            decimal d = (decimal)m_method.Para.CircleCalliper.ColCenter;
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
                for (int item = 0; item < m_method.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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

            numUD_Col1.Value = d;
            if (cmb_Col1.Text.Trim() != "null")
            {
                m_method.Para.ColSelectIndexName = cmb_Col1.Text.Trim().Substring(0, cmb_Col1.Text.Trim().LastIndexOf('_'));
            }
            else
            {
                m_method.Para.ColSelectIndexName = "null";
            }
        }

        // 中心Row
        private void Cmb_Row_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox ComBoxName = (ComboBox)sender;
            string strName = cmb_Row.Text.Trim();
            decimal d = (decimal)m_method.Para.CircleCalliper.RowCenter;
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
                for (int item = 0; item < m_method.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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

            numUD_Row1.Value = d;
            if (cmb_Row.Text.Trim() != "null")
            {
                m_method.Para.RowSelectIndexName = cmb_Row.Text.Trim().Substring(0, cmb_Row.Text.Trim().LastIndexOf('_'));
            }
            else
            {
                m_method.Para.RowSelectIndexName = "null";
            }
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
            if (m_method.ListParaResultAll.Count > 0)
            {
                for (int item = 0; item < m_method.ListParaResultAll.Count; item++)
                {
                    if (m_method.ListParaResultAll[item].ToolName.Contains("匹配工具" + item))
                    {
                        cmb_Row1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Affinehom_" + "item.PatMaxResultPara.Affinehom");

                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Row_" + m_method.ListParaResultAll[item].PatMaxResultPara.Row);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Col_" + m_method.ListParaResultAll[item].PatMaxResultPara.Col);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Angle_" + m_method.ListParaResultAll[item].PatMaxResultPara.Angle);

                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Row_" + m_method.ListParaResultAll[item].PatMaxResultPara.Row);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Col_" + m_method.ListParaResultAll[item].PatMaxResultPara.Col);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Angle_" + m_method.ListParaResultAll[item].PatMaxResultPara.Angle);

                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Row_" + m_method.ListParaResultAll[item].PatMaxResultPara.Row);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Col_" + m_method.ListParaResultAll[item].PatMaxResultPara.Col);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Angle_" + m_method.ListParaResultAll[item].PatMaxResultPara.Angle);
                    }
                    if (m_method.ListParaResultAll[item].ToolName.Contains("Blob工具" + item))
                    {
                        for (int i = 0; i < m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; i++)
                        {
                            cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Row_" + m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Col_" + m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Area_" + m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Area);
                            cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Row_" + m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Col_" + m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Area_" + m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Area);

                            cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Row_" + m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Col_" + m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Area_" + m_method.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Area);
                        }
                    }
                    if (m_method.ListParaResultAll[item].ToolName.Contains("找线工具" + item))
                    {
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowStart_" + m_method.ListParaResultAll[item].FindLineResultPara.RowStart);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColStart_" + m_method.ListParaResultAll[item].FindLineResultPara.ColStart);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowEnd_" + m_method.ListParaResultAll[item].FindLineResultPara.RowEnd);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColEnd_" + m_method.ListParaResultAll[item].FindLineResultPara.ColEnd);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowMiddle_" + m_method.ListParaResultAll[item].FindLineResultPara.RowMiddle);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColMiddle_" + m_method.ListParaResultAll[item].FindLineResultPara.ColMiddle);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowStart_" + m_method.ListParaResultAll[item].FindLineResultPara.RowStart);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColStart_" + m_method.ListParaResultAll[item].FindLineResultPara.ColStart);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowEnd_" + m_method.ListParaResultAll[item].FindLineResultPara.RowEnd);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColEnd_" + m_method.ListParaResultAll[item].FindLineResultPara.ColEnd);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowMiddle_" + m_method.ListParaResultAll[item].FindLineResultPara.RowMiddle);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColMiddle_" + m_method.ListParaResultAll[item].FindLineResultPara.ColMiddle);

                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowStart_" + m_method.ListParaResultAll[item].FindLineResultPara.RowStart);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColStart_" + m_method.ListParaResultAll[item].FindLineResultPara.ColStart);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowEnd_" + m_method.ListParaResultAll[item].FindLineResultPara.RowEnd);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColEnd_" + m_method.ListParaResultAll[item].FindLineResultPara.ColEnd);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowMiddle_" + m_method.ListParaResultAll[item].FindLineResultPara.RowMiddle);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColMiddle_" + m_method.ListParaResultAll[item].FindLineResultPara.ColMiddle);
                    }
                    if (m_method.ListParaResultAll[item].ToolName.Contains("找圆工具" + item))
                    {
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Row_" + m_method.ListParaResultAll[item].FindCircleResultPara.Row);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Col_" + m_method.ListParaResultAll[item].FindCircleResultPara.Col);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Radius_" + m_method.ListParaResultAll[item].FindCircleResultPara.Radius);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Row_" + m_method.ListParaResultAll[item].FindCircleResultPara.Row);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Col_" + m_method.ListParaResultAll[item].FindCircleResultPara.Col);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Radius_" + m_method.ListParaResultAll[item].FindCircleResultPara.Radius);

                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Row_" + m_method.ListParaResultAll[item].FindCircleResultPara.Row);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Col_" + m_method.ListParaResultAll[item].FindCircleResultPara.Col);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Radius_" + m_method.ListParaResultAll[item].FindCircleResultPara.Radius);
                    }
                    if (m_method.ListParaResultAll[item].ToolName.Contains("点线距离工具" + item))
                    {
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Row_" + m_method.ListParaResultAll[item].ParaDistancePointLines.Row);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Col_" + m_method.ListParaResultAll[item].ParaDistancePointLines.Col);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Distance_" + m_method.ListParaResultAll[item].ParaDistancePointLines.Distance);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Row_" + m_method.ListParaResultAll[item].ParaDistancePointLines.Row);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Col_" + m_method.ListParaResultAll[item].ParaDistancePointLines.Col);
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Distance_" + m_method.ListParaResultAll[item].ParaDistancePointLines.Distance);

                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Row_" + m_method.ListParaResultAll[item].ParaDistancePointLines.Row);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Col_" + m_method.ListParaResultAll[item].ParaDistancePointLines.Col);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Distance_" + m_method.ListParaResultAll[item].ParaDistancePointLines.Distance);
                    }
                    if (m_method.ListParaResultAll[item].ToolName.Contains("点点距离工具" + item))
                    {
                        cmb_Col1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Distance_" + m_method.ListParaResultAll[item].ParaDistancePointPoints.Distance);
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Distance_" + m_method.ListParaResultAll[item].ParaDistancePointPoints.Distance);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Distance_" + m_method.ListParaResultAll[item].ParaDistancePointPoints.Distance);
                    }
                    if (m_method.ListParaResultAll[item].ToolName.Contains("AngleLX工具" + item))
                    {
                        cmb_Row.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParasAngleLXResult.Angle_" + m_method.ListParaResultAll[item].ParasAngleLXResult.Angle);
                        cmb_Radius.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.ParasAngleLXResult.Angle_" + m_method.ListParaResultAll[item].ParasAngleLXResult.Angle);
                    }
                }
            }
            cmb_Row1.Items.Add("null");
            cmb_Row.Items.Add("null");
            cmb_Col1.Items.Add("null");
            cmb_Radius.Items.Add("null");
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
                    m_method.InputImage = m_hoImage;
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

        // 鼠标抬起事件
        void HWindowCtrl_MouseUp(object sender, MouseEventArgs e)
        {
            numUD_Col1.Value = (decimal)m_circleCalliperCurrent.ColCenter;
            numUD_Col2.Value = (decimal)m_circleCalliperCurrent.Radius;
            numUD_Row1.Value = (decimal)m_circleCalliperCurrent.RowCenter;
            numUD_CalliperHeight.Value = (decimal)m_circleCalliperCurrent.HeightCircle;
            numUD_CalliperWidth.Value = (decimal)m_circleCalliperCurrent.WidthCircle;
            numUd_CalliperNum.Value = (decimal)m_circleCalliperCurrent.CalliperCircleNum;
        }

        /// <summary>
        /// 将数据更新至控件
        /// </summary>
        private void UpdateParaToCtrl()
        {
            numUD_CalliperHeight.Value = (decimal)m_circleCalliperCurrent.HeightCircle;
            numUd_CalliperNum.Value = m_circleCalliperCurrent.CalliperCircleNum;
            numUD_CalliperWidth.Value = (decimal)m_circleCalliperCurrent.WidthCircle;
            numUD_Contrast.Value = m_method.Para.Contrast;
            num_PointNum.Value = (decimal)m_method.Para.FitCircleNum;
            switch (m_method.Para.Polarity)
            {
                case "negative":
                    radBtn_Negative.Checked = true;
                    break;
                case "positive":
                    radBtn_Positive.Checked = true;
                    break;
                case "all":
                    radBtn_All.Checked = true;
                    break;
            }
            switch (m_method.Para.Select)
            {
                case "first":
                    radBtn_First.Checked = true;
                    break;
                case "last":
                    radBtn_Last.Checked = true;
                    break;
                case "max":
                    radBtn_Max.Checked = true;
                    break;
            }
            numUD_Row1.Value = (decimal)m_circleCalliperCurrent.RowCenter;
            numUD_Col1.Value = (decimal)m_circleCalliperCurrent.ColCenter;
            numUD_Col2.Value = (decimal)m_circleCalliperCurrent.Radius;
            chk_calliper.Checked = m_method.Para.Circle;
            chk_line.Checked = m_method.Para.CirclePoint;
        }

        // 卡尺个数
        private void numUd_CalliperNum_ValueChanged(object sender, EventArgs e)
        {
            m_circleCalliperCurrent.CalliperCircleNum = (int)numUd_CalliperNum.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 卡尺宽度
        private void numUD_CalliperWidth_ValueChanged(object sender, EventArgs e)
        {
            m_circleCalliperCurrent.WidthCircle = (int)numUD_CalliperWidth.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 卡尺高度
        private void numUD_CalliperHeight_ValueChanged(object sender, EventArgs e)
        {
            m_circleCalliperCurrent.HeightCircle = (int)numUD_CalliperHeight.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 对比度
        private void numUD_Contrast_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.Contrast = (int)numUD_Contrast.Value;
        }

        // 选择
        private void radBtn_First_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.Select = "first";
        }

        private void radBtn_Last_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.Select = "last";
        }

        private void radBtn_Max_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.Select = "max";
        }

        // 极性
        private void radBtn_Positive_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.Polarity = "positive";
        }

        // 极性
        private void radBtn_Negative_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.Polarity = "negative";
        }

        private void radBtn_All_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.Polarity = "all";
        }

        private void numUD_Col1_ValueChanged(object sender, EventArgs e)
        {
            m_circleCalliperCurrent.ColCenter = (double)numUD_Col1.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        private void numUD_Row1_ValueChanged(object sender, EventArgs e)
        {
            m_circleCalliperCurrent.RowCenter = (double)numUD_Row1.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        private void numUD_Col2_ValueChanged(object sender, EventArgs e)
        {
            m_circleCalliperCurrent.Radius = (double)numUD_Col2.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        private void chk_line_CheckedChanged(object sender, EventArgs e)
        {
            cmb_Disp.SelectedIndex = 1;
            m_method.Para.Circle = chk_line.Checked;
            UpdateGraphic();
        }

        private void chk_calliper_CheckedChanged(object sender, EventArgs e)
        {
            cmb_Disp.SelectedIndex = 1;
            m_method.Para.CirclePoint = chk_calliper.Checked;
            UpdateGraphic();
        }

        // 运行按钮
        private void tsp_Run_Click(object sender, EventArgs e)
        {
            cmb_Disp.SelectedIndex = 1;
            HTuple hvaffinehom = null;
            if (null != m_method.AffineHom)
            {
                HOperatorSet.HomMat2dInvert(m_method.AffineHom, out hvaffinehom);
            }
            m_method.Para.CircleCalliper.CopyFrom(m_circleCalliperCurrent, hvaffinehom);
            m_method.Run();
            UpdateGraphic();
            lbl_Time.Text = "运行时间：" + m_method.RunTime + "ms";
            txt_msg.Text = m_method.RunMsg;
            UpdateData();
        }

        /// <summary>
        /// 结果图像更新
        /// </summary>
        private void UpdateGraphic()
        {
            m_Disp.HobjectToHimage(m_hoImage);
            if (m_method.Para.Circle)
            {
                m_Disp.DispObj(m_method.RegCircle, "green");
            }
            if (m_method.Para.CirclePoint)
            {
                m_Disp.DispObj(m_method.RegPoint, "green");
            }
        }

        /// <summary>
        /// 结果数据更新
        /// </summary>
        private void UpdateData()
        {
            dgv_result.Rows.Clear();
            for (int i = 0; i < m_method.ListCols.Count; i++)
            {
                int iNum = dgv_result.Rows.Add();
                dgv_result.Rows[iNum].Cells[0].Value = iNum + 1;
                dgv_result.Rows[iNum].Cells[1].Value = m_method.ListCols[i].ToString("0");
                dgv_result.Rows[iNum].Cells[2].Value = m_method.ListRows[i].ToString("0");
                dgv_result.Rows[iNum].Cells[3].Value = m_method.ListContrasts[i].ToString("0");
            }
            num_X.Value = (decimal)m_method.ColCircle;
            num_Y.Value = (decimal)m_method.RowCircle;
            num_radius.Value = (decimal)m_method.RadiusCircle;
        }

        // 角度设置
        private void btn_Phi_Click(object sender, EventArgs e)
        {
            m_circleCalliperCurrent.PhiCircle = -m_circleCalliperCurrent.PhiCircle + 180;
            m_Disp.ViewWindowMethod.Repaint();
        }

        //编辑
        private void tsp_edite_Click(object sender, EventArgs e)
        {
            cmb_Disp.SelectedIndex = 0;
            m_Disp.HobjectToHimage(m_hoImage);
            m_Disp.ViewWindowMethod.GenCircleCalliper(m_circleCalliperCurrent, ref m_listRoi);
        }

        // 窗口选择
        private void cmb_Disp_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Disp.HobjectToHimage(m_hoImage);
            switch (cmb_Disp.SelectedIndex)
            {
                case 0:
                    m_Disp.ViewWindowMethod.GenCircleCalliper(m_circleCalliperCurrent, ref m_listRoi);
                    break;
                case 1:
                    UpdateGraphic();
                    break;
            }
        }

        // 结果dgv内容选择
        private void dgv_result_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (m_method.ListRows.Count > 0)
            {
                if (null != dgv_result.CurrentRow)
                {
                    int dNum = dgv_result.CurrentRow.Index;
                    HObject hoCross = null;
                    HOperatorSet.GenCrossContourXld(out hoCross, m_method.ListRows[dNum], m_method.ListCols[dNum], 10, 1);
                    UpdateGraphic();
                    m_Disp.DispObj(hoCross, "red");
                }
            }
        }

        // 窗体关闭
        private void FindCircleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HTuple hvaffinehom = null;
            if (null != m_method.AffineHom)
            {
                HOperatorSet.HomMat2dInvert(m_method.AffineHom, out hvaffinehom);
            }
            m_method.Para.CircleCalliper.CopyFrom(m_circleCalliperCurrent, hvaffinehom);
        }

        private void num_PointNum_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.FitCircleNum = (int)num_PointNum.Value;
        }
    }
}
