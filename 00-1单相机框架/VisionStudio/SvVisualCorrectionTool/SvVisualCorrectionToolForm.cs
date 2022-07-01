using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ParaResultAll;

namespace SvVisualCorrectionTool
{
    public partial class SvVisualCorrectionToolForm : Form
    {
        /// <summary>
        /// 方法类
        /// </summary>
        public SvVisualCorrectionToolMethod m_VisualCorrectionToolMethod = null;

        public SvVisualCorrectionToolForm()
        {
            InitializeComponent();
        }

        private void SvJudgeResultParaToolForm_Load(object sender, EventArgs e)
        {
            UpdateInputeData(cmb_Data1, Cmb1_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data2, Cmb2_Data_SelectedIndexChanged);
            UpdateCmbSelect(cmb_Data1, 0);
            UpdateCmbSelect(cmb_Data2, 1);
            // 将数据更新至控件
            UpdateParaToCtrl();
            tsp_Msg.Text = m_VisualCorrectionToolMethod.RunMsg;
            tsp_Time.Text = m_VisualCorrectionToolMethod.RunTime.ToString("F1");
        }

        /// <summary>
        /// 将数据更新至控件
        /// </summary>
        private void UpdateParaToCtrl()
        {
            chk_输入数据2.Checked = m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[1].SelectState;
            chk_输入数据1.Checked = m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[0].SelectState;
            UpdateSelectCCD();
            num_b1.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ColB1;
            num_b2.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ColB2;
            num_b3.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ColB3;
            num_b4.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ColB4;
            num_b5.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ColB5;
            num_k1.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.RowK1;
            num_k2.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.RowK2;
            num_k3.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.RowK3;
            num_k4.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.RowK4;
            num_K5.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.RowK5;
            num_m1.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.XM1;
            num_m2.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.YM2;
            num_m3.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.XM3;
            num_m4.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.YM4;
            num_m5.Value = (decimal)m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.XM5;
            UpdateUseState();
        }

        private void UpdateSelectCCD()
        {
            switch (m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.EnumTypeChecks)
            {
                case EnumTypeCheck.吸嘴下影像:
                    rad_Use下.Checked = true;
                    rad_Use组立上.Checked = false;
                    rad_Use来料上.Checked = false;
                    break;
                case EnumTypeCheck.来料上影像:
                    rad_Use下.Checked = false;
                    rad_Use组立上.Checked = false;
                    rad_Use来料上.Checked = true;
                    break;
                case EnumTypeCheck.组立上影像:
                    rad_Use组立上.Checked = true;
                    rad_Use下.Checked = false;
                    rad_Use来料上.Checked = false;
                    break;
            }
        }

        /// <summary>
        /// 更新启用状态
        /// </summary>
        private void UpdateUseState()
        {            
            cmb_Data1.Enabled = chk_输入数据1.Checked;
            num_Data1.Enabled = chk_输入数据1.Checked;
            cmb_Data2.Enabled = chk_输入数据2.Checked;
            num_Data2.Enabled = chk_输入数据2.Checked;  
        }

        /// <summary>
        /// 更新下拉框对应索引
        /// </summary>
        /// <param name="cmb_Data"></param>
        /// <param name="iRangeIndex"></param>
        private void UpdateCmbSelect(System.Windows.Forms.ComboBox cmb_Data, int iRangeIndex)
        {
            for (int i = 0; i < cmb_Data.Items.Count; i++)
            {
                if (cmb_Data.Items[i].ToString().Contains(m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[iRangeIndex].SelectName))
                {
                    cmb_Data.SelectedIndex = i;
                }
            }
        }

        /// <summary>
        /// 更新传入数据
        /// </summary>
        private void UpdateInputeData(System.Windows.Forms.ComboBox cmb_Data, EventHandler Cmb_Data_SelectedIndexChanged)
        {
            cmb_Data.Items.Clear();
            if (m_VisualCorrectionToolMethod.ListParaResultAll.Count > 0)
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    if (m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName.Contains("匹配工具" + item))
                    {
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Row_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].PatMaxResultPara.Row);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Col_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].PatMaxResultPara.Col);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Angle_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].PatMaxResultPara.Angle);
                    }
                    if (m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName.Contains("Blob工具" + item))
                    {
                        for (int i = 0; i < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; i++)
                        {
                            cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Row_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Col_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Area_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Area);
                        }
                    }
                    if (m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName.Contains("找线工具" + item))
                    {
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowStart_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].FindLineResultPara.RowStart);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColStart_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].FindLineResultPara.ColStart);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowEnd_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].FindLineResultPara.RowEnd);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColEnd_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].FindLineResultPara.ColEnd);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowMiddle_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].FindLineResultPara.RowMiddle);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColMiddle_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].FindLineResultPara.ColMiddle);
                    }
                    if (m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName.Contains("找圆工具" + item))
                    {
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Row_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].FindCircleResultPara.Row);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Col_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].FindCircleResultPara.Col);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Radius_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].FindCircleResultPara.Radius);
                    }
                    if (m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName.Contains("点线距离工具" + item))
                    {
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Row_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].ParaDistancePointLines.Row);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Col_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].ParaDistancePointLines.Col);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Distance_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].ParaDistancePointLines.Distance);
                    }
                    if (m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName.Contains("点点距离工具" + item))
                    {
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Distance_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].ParaDistancePointPoints.Distance);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Row_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].ParaDistancePointPoints.Row);
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Col_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].ParaDistancePointPoints.Col);
                    }
                    if (m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName.Contains("AngleLX工具" + item))
                    {
                        cmb_Data.Items.Add(m_VisualCorrectionToolMethod.ListParaResultAll[item].ToolName + "item.ParasAngleLXResult.Angle_" + m_VisualCorrectionToolMethod.ListParaResultAll[item].ParasAngleLXResult.Angle);
                    }
                }
                cmb_Data.SelectedIndexChanged += Cmb_Data_SelectedIndexChanged;
            }
        }

        // 数据1选择
        private void Cmb1_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            if (strName != null && strName.Contains("匹配工具"))
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
            if (strName != null && strName.Contains("找线工具"))
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
            if (strName != null && strName.Contains("找圆工具"))
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
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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
            if (strName != null && strName.Contains("点线距离工具"))
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
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointPoints.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointPoints.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            num_Data1.Value = d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[0].Data = (double)d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[0].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据2选择
        private void Cmb2_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            if (strName != null && strName.Contains("匹配工具"))
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
            if (strName != null && strName.Contains("找线工具"))
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
            if (strName != null && strName.Contains("找圆工具"))
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
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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
            if (strName != null && strName.Contains("点线距离工具"))
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
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointPoints.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointPoints.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            num_Data2.Value = d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[1].Data = (double)d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[1].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据3
        private void Cmb3_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("匹配工具"))
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
            if (strName != null && strName.Contains("找线工具"))
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
            if (strName != null && strName.Contains("找圆工具"))
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
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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
            if (strName != null && strName.Contains("点线距离工具"))
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
            num_Data3.Value = d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[2].Data = (double)d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[2].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据4
        private void Cmb4_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("匹配工具"))
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
            if (strName != null && strName.Contains("找线工具"))
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
            if (strName != null && strName.Contains("找圆工具"))
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
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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
            if (strName != null && strName.Contains("点线距离工具"))
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
            num_Data4.Value = d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[3].Data = (double)d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[3].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据5
        private void Cmb5_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("匹配工具"))
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
            if (strName != null && strName.Contains("找线工具"))
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
            if (strName != null && strName.Contains("找圆工具"))
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
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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
            if (strName != null && strName.Contains("点线距离工具"))
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
            num_Data5.Value = d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[4].Data = (double)d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[4].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据6
        private void Cmb6_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("匹配工具"))
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
            if (strName != null && strName.Contains("找线工具"))
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
            if (strName != null && strName.Contains("找圆工具"))
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
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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
            if (strName != null && strName.Contains("点线距离工具"))
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
            num_Data6.Value = d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[5].Data = (double)d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[5].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据7
        private void Cmb7_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("匹配工具"))
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
            if (strName != null && strName.Contains("找线工具"))
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
            if (strName != null && strName.Contains("找圆工具"))
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
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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
            if (strName != null && strName.Contains("点线距离工具"))
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
            num_Data7.Value = d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[6].Data = (double)d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[6].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据8
        private void Cmb8_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("匹配工具"))
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
            if (strName != null && strName.Contains("找线工具"))
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
            if (strName != null && strName.Contains("找圆工具"))
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
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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
            if (strName != null && strName.Contains("点线距离工具"))
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
            num_Data8.Value = d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[7].Data = (double)d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[7].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据9
        private void Cmb9_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("匹配工具"))
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
            if (strName != null && strName.Contains("找线工具"))
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
            if (strName != null && strName.Contains("找圆工具"))
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
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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
            if (strName != null && strName.Contains("点线距离工具"))
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
            num_Data9.Value = d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[8].Data = (double)d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[8].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据10
        private void Cmb10_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("匹配工具"))
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
            if (strName != null && strName.Contains("找线工具"))
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
            if (strName != null && strName.Contains("找圆工具"))
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
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_VisualCorrectionToolMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_VisualCorrectionToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
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
            if (strName != null && strName.Contains("点线距离工具"))
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
            num_Data10.Value = d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[9].Data = (double)d;
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[9].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        private void chk_输入数据1_CheckedChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[0].SelectState = chk_输入数据1.Checked;
            UpdateUseState();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ListSelectRangePara[1].SelectState = chk_输入数据2.Checked;
            UpdateUseState();
        }

        private void tsp_run_Click(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.Run();
            tsp_Msg.Text = m_VisualCorrectionToolMethod.RunMsg;
            tsp_Time.Text = m_VisualCorrectionToolMethod.RunTime.ToString("F1");
            UpdateResultData();
        }

        /// <summary>
        /// 更新结果
        /// </summary>
        private void UpdateResultData()
        {
            dgv_result.Rows.Clear();
            if (m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.EnumTypeChecks == EnumTypeCheck.吸嘴下影像)
            {
                dgv_result.Rows.Add(2);
                dgv_result.Rows[0].Cells[0].Value = m_VisualCorrectionToolMethod.X1;
                dgv_result.Rows[0].Cells[1].Value = m_VisualCorrectionToolMethod.Y1;
                dgv_result.Rows[1].Cells[0].Value = m_VisualCorrectionToolMethod.X2;
                dgv_result.Rows[1].Cells[1].Value = m_VisualCorrectionToolMethod.Y2;
            }
            else
            {
                dgv_result.Rows.Add(1);
                dgv_result.Rows[0].Cells[0].Value = m_VisualCorrectionToolMethod.X1;
                dgv_result.Rows[0].Cells[1].Value = m_VisualCorrectionToolMethod.Y1;
            }
        }

        private void rad_Use来料上_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_Use来料上.Checked)
            {
                m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.EnumTypeChecks = EnumTypeCheck.来料上影像;
            }
            UpdateSelectCCD();
        }

        private void num_m1_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.XM1 = (double)num_m1.Value;
        }

        private void num_k1_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.RowK1 = (double)num_k1.Value;
        }

        private void num_b1_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ColB1 = (double)num_b1.Value;
        }

        private void num_m2_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.YM2 = (double)num_m2.Value;
        }

        private void num_k2_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.RowK2 = (double)num_k2.Value;
        }

        private void num_b2_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ColB2 = (double)num_b2.Value;
        }

        private void num_m3_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.XM3 = (double)num_m3.Value;
        }

        private void num_k3_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.RowK3 = (double)num_k3.Value;

        }

        private void num_b3_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ColB3 = (double)num_b3.Value;
        }

        private void num_m4_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.YM4 = (double)num_m4.Value;
        }

        private void num_k4_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.RowK4 = (double)num_k4.Value;
        }

        private void num_b4_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ColB4 = (double)num_b4.Value;
        }

        private void num_m5_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.XM5 = (double)num_m5.Value;
        }

        private void num_K5_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.RowK5 = (double)num_K5.Value;
        }

        private void num_b5_ValueChanged(object sender, EventArgs e)
        {
            m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.ColB5 = (double)num_b5.Value;
        }

        private void rad_Use组立上_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_Use组立上.Checked)
            {
                m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.EnumTypeChecks = EnumTypeCheck.组立上影像;
            }
            UpdateSelectCCD();
        }

        private void rad_Use下_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_Use下.Checked)
            {
                m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara.EnumTypeChecks = EnumTypeCheck.吸嘴下影像;
            }
            UpdateSelectCCD();
        }
    }
}
