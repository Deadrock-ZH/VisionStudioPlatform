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

namespace SvDistancePointLineTool
{  
    /// <summary>
    /// 内 容:本类是点线距离计算界面
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public partial class SvDistancePointLineToolForm : Form
    {
        /// <summary>
        /// 方法类
        /// </summary>
        public SvDistancePointLineToolMethod m_SvDistancePointLineToolMethod = null;// new SvAngleLXMethod();

        /// <summary>
        /// 显示控件
        /// </summary>
        private GVS.HalconDisp.Control.HWindow_Final m_Disp = new GVS.HalconDisp.Control.HWindow_Final();

        /// <summary>
        /// 读取图像
        /// </summary>
        private HObject m_hoImage = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SvDistancePointLineToolForm()
        {
            InitializeComponent();
        }

        // 窗体加载
        private void SvDistancePointLineToolForm_Load(object sender, EventArgs e)
        {
            pnl_disp.Controls.Clear();
            pnl_disp.Controls.Add(m_Disp);
            m_Disp.Dock = DockStyle.Fill;

            if (null != m_SvDistancePointLineToolMethod.InputImage)
            {
                m_hoImage = m_SvDistancePointLineToolMethod.InputImage;
                //HOperatorSet.CopyImage(m_SvDistancePointLineToolMethod.InputImage, out m_hoImage);
                m_Disp.HobjectToHimage(m_hoImage);
            }

            // 将参数更新至界面
            UpdateParaToCtrl();

            // 将模块输出类数据信息更新至下拉框中
            UpdateInputeData();
          
            // 数据与下拉框对应
            ConnectParaToCmbSelect();
            cmb_Disp.SelectedIndex = 0;
            cmb_Disp_SelectedIndexChanged(null, null);
            tsp_time.Text = m_SvDistancePointLineToolMethod.RunTime.ToString() + "ms";
            tsp_Msg.Text = m_SvDistancePointLineToolMethod.RunMsg;
        }

        /// <summary>
        /// 将数据更新至控件
        /// </summary>
        private void UpdateParaToCtrl()
        {
            chk_Point1.Checked = m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Point1State;
            chk_Point2.Checked = m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Point2State;
            chk_Line.Checked = m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.LineState;
            chk_Standard.Checked = m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.PointState;
            chk_Proj.Checked = m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.ProjPointState;
    }

        /// <summary>
        /// 更新传入数据
        /// </summary>
        private void UpdateInputeData()
        {
            cmb_Row1.Items.Clear();
            cmb_Col1.Items.Clear();
            cmb_Row2.Items.Clear();
            cmb_Col2.Items.Clear();
            cmb_Row.Items.Clear();
            cmb_Col.Items.Clear();
            if (m_SvDistancePointLineToolMethod.ListParaResultAll.Count > 0)
            {
                foreach (ParasResultAll item in m_SvDistancePointLineToolMethod.ListParaResultAll)
                {
                    if (item.ToolName.Contains("匹配工具"))
                    {
                        cmb_Row1.Items.Add(item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row);
                        cmb_Row1.Items.Add(item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col);
                        cmb_Row1.Items.Add(item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle);
                        cmb_Col1.Items.Add(item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row);
                        cmb_Col1.Items.Add(item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col);
                        cmb_Col1.Items.Add(item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle);
                        cmb_Row2.Items.Add(item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row);
                        cmb_Row2.Items.Add(item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col);
                        cmb_Row2.Items.Add(item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle);
                        cmb_Col2.Items.Add(item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row);
                        cmb_Col2.Items.Add(item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col);
                        cmb_Col2.Items.Add(item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle);
                        cmb_Row.Items.Add(item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row);
                        cmb_Row.Items.Add(item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col);
                        cmb_Row.Items.Add(item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle);
                        cmb_Col.Items.Add(item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row);
                        cmb_Col.Items.Add(item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col);
                        cmb_Col.Items.Add(item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle);
                    }
                    if (item.ToolName.Contains("Blob工具"))
                    {
                        for (int i = 0; i < item.BlobResultPara.ListBlobResultPara.Count; i++)
                        {
                            cmb_Row1.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Row1.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Row1.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[i].Area);
                            cmb_Col1.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Col1.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Col1.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[i].Area);
                            cmb_Row2.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Row2.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Row2.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[i].Area);
                            cmb_Col2.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Col2.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Col2.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[i].Area);
                            cmb_Row.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Row.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Row.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[i].Area);
                            cmb_Col.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Col.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Col.Items.Add(item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[i].Area);
                        }
                    }
                    if (item.ToolName.Contains("线线角度工具"))
                    {
                        cmb_Row1.Items.Add(item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row);
                        cmb_Row1.Items.Add(item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col);
                        cmb_Row1.Items.Add(item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle);
                        cmb_Col1.Items.Add(item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row);
                        cmb_Col1.Items.Add(item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col);
                        cmb_Col1.Items.Add(item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle);
                        cmb_Row2.Items.Add(item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row);
                        cmb_Row2.Items.Add(item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col);
                        cmb_Row2.Items.Add(item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle);
                        cmb_Col2.Items.Add(item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row);
                        cmb_Col2.Items.Add(item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col);
                        cmb_Col2.Items.Add(item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle);
                        cmb_Row.Items.Add(item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row);
                        cmb_Row.Items.Add(item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col);
                        cmb_Row.Items.Add(item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle);
                        cmb_Col.Items.Add(item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row);
                        cmb_Col.Items.Add(item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col);
                        cmb_Col.Items.Add(item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle);
                    }
                    if (item.ToolName.Contains("找线工具"))
                    {
                        cmb_Row1.Items.Add(item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart);
                        cmb_Row1.Items.Add(item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart);
                        cmb_Row1.Items.Add(item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd);
                        cmb_Row1.Items.Add(item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd);
                        cmb_Row1.Items.Add(item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle);
                        cmb_Row1.Items.Add(item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle);

                        cmb_Col1.Items.Add(item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart);
                        cmb_Col1.Items.Add(item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart);
                        cmb_Col1.Items.Add(item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd);
                        cmb_Col1.Items.Add(item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd);
                        cmb_Col1.Items.Add(item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle);
                        cmb_Col1.Items.Add(item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle);

                        cmb_Row2.Items.Add(item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart);
                        cmb_Row2.Items.Add(item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart);
                        cmb_Row2.Items.Add(item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd);
                        cmb_Row2.Items.Add(item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd);
                        cmb_Row2.Items.Add(item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle);
                        cmb_Row2.Items.Add(item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle);

                        cmb_Col2.Items.Add(item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart);
                        cmb_Col2.Items.Add(item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart);
                        cmb_Col2.Items.Add(item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd);
                        cmb_Col2.Items.Add(item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd);
                        cmb_Col2.Items.Add(item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle);
                        cmb_Col2.Items.Add(item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle);

                        cmb_Row.Items.Add(item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart);
                        cmb_Row.Items.Add(item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart);
                        cmb_Row.Items.Add(item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd);
                        cmb_Row.Items.Add(item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd);
                        cmb_Row.Items.Add(item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle);
                        cmb_Row.Items.Add(item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle);

                        cmb_Col.Items.Add(item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart);
                        cmb_Col.Items.Add(item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart);
                        cmb_Col.Items.Add(item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd);
                        cmb_Col.Items.Add(item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd);
                        cmb_Col.Items.Add(item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle);
                        cmb_Col.Items.Add(item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle);
                    }
                    if (item.ToolName.Contains("找圆工具"))
                    {
                        cmb_Row1.Items.Add(item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row);
                        cmb_Row1.Items.Add(item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col);
                        cmb_Row1.Items.Add(item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius);
                        cmb_Col1.Items.Add(item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row);
                        cmb_Col1.Items.Add(item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col);
                        cmb_Col1.Items.Add(item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius);
                        cmb_Row2.Items.Add(item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row);
                        cmb_Row2.Items.Add(item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col);
                        cmb_Row2.Items.Add(item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius);
                        cmb_Col2.Items.Add(item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row);
                        cmb_Col2.Items.Add(item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col);
                        cmb_Col2.Items.Add(item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius);
                        cmb_Row.Items.Add(item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row);
                        cmb_Row.Items.Add(item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col);
                        cmb_Row.Items.Add(item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius);
                        cmb_Col.Items.Add(item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row);
                        cmb_Col.Items.Add(item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col);
                        cmb_Col.Items.Add(item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius);
                    }
                }
                cmb_Row1.SelectedIndexChanged += Cmb_Row1_SelectedIndexChanged;
                cmb_Row.SelectedIndexChanged += Cmb_Row_SelectedIndexChanged;
                cmb_Row2.SelectedIndexChanged += Cmb_Row2_SelectedIndexChanged;
                cmb_Col1.SelectedIndexChanged += Cmb_Col1_SelectedIndexChanged;
                cmb_Col2.SelectedIndexChanged += Cmb_Col2_SelectedIndexChanged;
                cmb_Col.SelectedIndexChanged += Cmb_Col_SelectedIndexChanged;
            }
        }

        /// <summary>
        ///根据保存名称链接到对应的下拉框索引
        /// </summary>
        private void ConnectParaToCmbSelect()
        {
            for (int i = 0; i < cmb_Row1.Items.Count; i++)
            {
                if (cmb_Row1.Items[i].ToString().Contains(m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Row1SelectIndexName))
                {
                    cmb_Row1.SelectedIndex = i;
                }
            }
            for (int i = 0; i < cmb_Row2.Items.Count; i++)
            {
                if (cmb_Row2.Items[i].ToString().Contains(m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Row2SelectIndexName))
                {
                    cmb_Row2.SelectedIndex = i;
                }
            }
            for (int i = 0; i < cmb_Col1.Items.Count; i++)
            {
                if (cmb_Col1.Items[i].ToString().Contains(m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Col1SelectIndexName))
                {
                    cmb_Col1.SelectedIndex = i;
                }
            }
            for (int i = 0; i < cmb_Col2.Items.Count; i++)
            {
                if (cmb_Col2.Items[i].ToString().Contains(m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Col2SelectIndexName))
                {
                    cmb_Col2.SelectedIndex = i;
                }
            }
            for (int i = 0; i < cmb_Col.Items.Count; i++)
            {
                if (cmb_Col.Items[i].ToString().Contains(m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.ColSelectIndexName))
                {
                    cmb_Col.SelectedIndex = i;
                }
            }
            for (int i = 0; i < cmb_Row.Items.Count; i++)
            {
                if (cmb_Row.Items[i].ToString().Contains(m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.RowSelectIndexName))
                {
                    cmb_Row.SelectedIndex = i;
                }
            }
        }

        // Row数据选择
        private void Cmb_Row_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Row.Text.Trim();
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
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Area"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            num_Row.Value = d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Row = (double)d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.RowSelectIndexName = cmb_Row.Text.Trim().Substring(0, cmb_Row.Text.Trim().LastIndexOf('_'));
        }

        // Row1数据选择
        private void Cmb_Row1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Row1.Text.Trim();
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
            if (strName != null && strName.Contains("线线角度工具"))
            {
                if (strName.Contains("item.ParaParaAngleLL.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Angle"))
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
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Area"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            num_Row1.Value = d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Row1 = (double)d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Row1SelectIndexName = cmb_Row1.Text.Trim().Substring(0, cmb_Row1.Text.Trim().LastIndexOf('_'));
        }

        // Row2数据选择
        private void Cmb_Row2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Row2.Text;
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
            if (strName != null && strName.Contains("线线角度工具"))
            {
                if (strName.Contains("item.ParaParaAngleLL.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Angle"))
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
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Area"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            num_Row2.Value = d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Row2 = (double)d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Row2SelectIndexName = cmb_Row2.Text.Trim().Substring(0, cmb_Row2.Text.Trim().LastIndexOf('_'));
        }


        // Col数据选择
        private void Cmb_Col_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Col.Text.Trim();
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
            if (strName != null && strName.Contains("线线角度工具"))
            {
                if (strName.Contains("item.ParaParaAngleLL.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Angle"))
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
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Area"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            num_Col.Value = d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Col = (double)d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.ColSelectIndexName = cmb_Col.Text.Trim().Substring(0, cmb_Col.Text.Trim().LastIndexOf('_'));
        }

        // Col1数据选择
        private void Cmb_Col1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Col1.Text;
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
            if (strName != null && strName.Contains("线线角度工具"))
            {
                if (strName.Contains("item.ParaParaAngleLL.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Angle"))
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
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Area"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            num_Col1.Value = d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Col1 = (double)d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Col1SelectIndexName = cmb_Col1.Text.Trim().Substring(0, cmb_Col1.Text.Trim().LastIndexOf('_'));
        }

        // Col2数据选择
        private void Cmb_Col2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Col2.Text;
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
            if (strName != null && strName.Contains("线线角度工具"))
            {
                if (strName.Contains("item.ParaParaAngleLL.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Angle"))
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
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.BlobResultPara.ListBlobResultPara[i].Area"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            num_Col2.Value = d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Col2 = (double)d;
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Col2SelectIndexName = cmb_Col2.Text.Trim().Substring(0, cmb_Col2.Text.Trim().LastIndexOf('_'));
        }

        // run
        private void tsp_Run_Click(object sender, EventArgs e)
        {
            bool bstate = m_SvDistancePointLineToolMethod.Run();
            cmb_Disp.SelectedIndex = 1;
            cmb_Disp_SelectedIndexChanged(null, null);
            tsp_time.Text = m_SvDistancePointLineToolMethod.RunTime.ToString() + "ms";
            tsp_Msg.Text = m_SvDistancePointLineToolMethod.RunMsg;
            if (bstate)
            {
                UpdateResultPara();
            }
        }

        /// <summary>
        /// 结果数据更新
        /// </summary>
        private void UpdateResultPara()
        {
            dgv_Result.Rows.Clear();
            int i = dgv_Result.Rows.Add();
            dgv_Result.Rows[0].Cells[0].Value = i;
            dgv_Result.Rows[0].Cells[1].Value = m_SvDistancePointLineToolMethod.Row.ToString("F0");
            dgv_Result.Rows[0].Cells[2].Value = m_SvDistancePointLineToolMethod.Col.ToString("F0");
            dgv_Result.Rows[0].Cells[3].Value = m_SvDistancePointLineToolMethod.Distance.ToString("F1");
        }

        // 窗口选择
        private void cmb_Disp_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_Disp.SelectedIndex)
            {
                case 0:
                    m_Disp.HobjectToHimage(m_hoImage);
                    break;
                case 1:
                    UpdateRegResult();
                    break;
            }
        }

        /// <summary>
        /// 更新结果区域
        /// </summary>
        private void UpdateRegResult()
        {
            m_Disp.HobjectToHimage(m_hoImage);
            if (m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Point1State)
            {
                m_Disp.DispObj(m_SvDistancePointLineToolMethod.Cross1, "red");
            }
            if (m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Point2State)
            {
                m_Disp.DispObj(m_SvDistancePointLineToolMethod.Cross2, "red");
            }
            if (m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.LineState)
            {
                m_Disp.DispObj(m_SvDistancePointLineToolMethod.Line, "red");
            }
            if (m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.PointState)
            {
                m_Disp.DispObj(m_SvDistancePointLineToolMethod.Cross, "green");
            }
            if (m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.ProjPointState)
            {
                m_Disp.DispObj(m_SvDistancePointLineToolMethod.ProjCross, "green");
            }
        }

        // Point1
        private void chk_Point1_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Point1State = chk_Point1.Checked;
            UpdateRegResult();
        }

        // Point2
        private void chk_Point2_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.Point2State = chk_Point2.Checked;
            UpdateRegResult();
        }

        // Line
        private void chk_Line_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.LineState = chk_Line.Checked;
            UpdateRegResult();
        }

        // 显示输入点
        private void chk_Standard_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.PointState = chk_Standard.Checked;
            UpdateRegResult();
        }

        // 垂足
        private void chk_Proj_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.ProjPointState = chk_Proj.Checked;
            UpdateRegResult();
        }

        private void chk_MiddleCcenter_CheckedChanged(object sender, EventArgs e)
        {
            //m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas.MiddlePointState = chk_MiddleCcenter.Checked;
            UpdateRegResult();
        }
    }
}
