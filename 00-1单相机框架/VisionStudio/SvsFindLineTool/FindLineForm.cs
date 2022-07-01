using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GVS.HalconDisp;
using HalconDotNet;
using System.IO;
using ParaResultAll;

namespace SvsFindLineTool
{
    public partial class FindLineForm : Form
    {
        /// <summary>
        /// 显示控件
        /// </summary>
        GVS.HalconDisp.Control.HWindow_Final m_Disp = new GVS.HalconDisp.Control.HWindow_Final();

        /// <summary>
        /// 当前显示线卡尺工具
        /// </summary>

        GVS.HalconDisp.ViewWindow.Config.LineCalliper m_currentLine = new GVS.HalconDisp.ViewWindow.Config.LineCalliper();

        /// <summary>
        /// 存储ROI
        /// </summary>
        List<GVS.HalconDisp.ViewWindow.Model.ROI> m_listROI = new List<GVS.HalconDisp.ViewWindow.Model.ROI>();

        /// <summary>
        /// 方法类
        /// </summary>
        public FindLineMethod m_method;

        /// <summary>
        /// 图像
        /// </summary>
        private HObject m_hoImage = null;

        /// <summary>
        /// 是否模块使用
        /// </summary>
        public bool m_bModual = false;

        /// <summary>
        /// 是否统一软件使用
        /// </summary>
        public bool m_bTool = true;

        /// <summary>
        /// 构造方法
        /// </summary>
        public FindLineForm()
        {
            InitializeComponent();
        }

        // 窗体加载
        private void FindLineForm_Load(object sender, EventArgs e)
        {
            // 模块化软件使用
            if (m_bTool)
            {
                UpdateInputeData();
                cmb_Row1.SelectedIndexChanged += Cmb_Row1_SelectedIndexChanged;
                for (int i = 0; i < cmb_Row1.Items.Count; i++)
                {
                    if (m_method.Para.Row1SelectIndexName.Length > 0)
                    {
                        if (cmb_Row1.Items[i].ToString().Contains(m_method.Para.Row1SelectIndexName))
                        {
                            cmb_Row1.SelectedIndex = i;
                            Cmb_Row1_SelectedIndexChanged(null, null);
                        }
                    }
                }
            }
            m_currentLine.CopyFrom(m_method.Para.LineCalliperParas, m_method.AffineTans);
            pnl_Disp.Controls.Add(m_Disp);
            m_Disp.Dock = DockStyle.Fill;
            if (null != m_method.InputImage)
            {
                //HOperatorSet.CopyImage(m_method.InputImage, out m_hoImage);
                m_hoImage = m_method.InputImage;
                m_Disp.HobjectToHimage(m_hoImage);
            }
            m_Disp.ViewWindowMethod.GenLineCalliper(m_currentLine, ref m_listROI);

            // 将数据更新到控件
            UpdateParaToCtrl();

            // 默认为输入窗口
            cmb_Disp.SelectedIndex = 0;
            m_Disp.HWindowCtrl.MouseUp += HWindowCtrl_HMouseUp;
            lbl_Time.Text = "运行时间：" + m_method.RunTime + "ms";
            lbl_Msg.Text = m_method.RunMsg;
            tsp_Save.Visible = m_bModual;
            pnl_Tool.Visible = m_bTool;
        }

        private void Cmb_Row1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cmb_Row1.Text.Trim();
            if (strName == "null")
            {
                m_method.AffineTans = null;
                m_method.Para.Row1SelectIndexName = "null";
            }
            else
            {
                for (int item = 0; item < m_method.ListParaResultAll.Count; item++)
                {
                    string CmbName = "匹配工具" + item;
                    if (strName.Contains(CmbName))
                    {
                        m_method.AffineTans = m_method.ListParaResultAll[item].PatMaxResultPara.Affinehom;
                        break;
                    }
                }
                m_method.Para.Row1SelectIndexName = cmb_Row1.Text.Trim().Substring(0, cmb_Row1.Text.Trim().LastIndexOf('_'));
            }
        }

        /// <summary>
        /// 更新传入数据
        /// </summary>
        private void UpdateInputeData()
        {
            cmb_Row1.Items.Clear();
            cmb_Row1.Items.Clear();
            if (m_method.ListParaResultAll.Count > 0)
            {
                foreach (ParasResultAll item in m_method.ListParaResultAll)
                {
                    for (int i = 0; i < m_method.ListParaResultAll.Count; i++)
                    {
                        if (item.ToolName.Contains("匹配工具" + i))
                        {
                            cmb_Row1.Items.Add(item.ToolName + "item.PatMaxResultPara.Affinehom_" + "item.PatMaxResultPara.Affinehom");
                        }
                    }
                }
            }
            // if (m_method.ListParaResultAll.Count > 0)
            //{
            //    for (int item = 0; item < m_method.ListParaResultAll.Count; item++)
            //    {
            //        if (m_method.ListParaResultAll[item].ToolName.Contains("匹配工具" + item))
            //        {
            //            cmb_Row1.Items.Add(m_method.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Affinehom_" + "item.PatMaxResultPara.Affinehom");
            //        }
            //    }
            //}
            cmb_Row1.Items.Add("null");
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

        // 鼠标抬起事件，将数据更新到控件
        private void HWindowCtrl_HMouseUp(object sender, MouseEventArgs e)
        {
            numUD_Col1.Value = (decimal)m_currentLine.ColumnBegin;
            numUD_Col2.Value = (decimal)m_currentLine.ColumnEnd;
            numUD_phi.Value = (decimal)m_currentLine.Phi;
            numUD_Row1.Value = (decimal)m_currentLine.RowBegin;
            numUD_Row2.Value = (decimal)m_currentLine.RowEnd;
            numUD_CalliperHeight.Value = (decimal)m_currentLine.RecHeight;
            numUD_CalliperWidth.Value = (decimal)m_currentLine.RecWidth;
            numUd_CalliperNum.Value = (decimal)m_currentLine.CalliperCount;
        }

        /// <summary>
        /// 将数据更新到控件
        /// </summary>
        private void UpdateParaToCtrl()
        {
            numUD_CalliperHeight.Value = (decimal)m_currentLine.RecHeight;
            numUD_CalliperWidth.Value = (decimal)m_currentLine.RecWidth;
            numUD_Contrast.Value = (decimal)m_method.Para.LineCalliperParas.GrayContrast;
            numUd_CalliperNum.Value = (decimal)m_currentLine.CalliperCount;
            numUD_Col1.Value = (decimal)m_currentLine.ColumnBegin;
            numUD_Col2.Value = (decimal)m_currentLine.ColumnEnd;
            numUD_phi.Value = (decimal)m_method.Para.Phi;
            numUD_Row1.Value = (decimal)m_currentLine.RowBegin;
            numUD_Row2.Value = (decimal)m_currentLine.RowEnd;
            chk_calliper.Checked = m_method.Para.LineCtrlParas.Calliper;
            chk_line.Checked = m_method.Para.LineCtrlParas.Line;
            UpdateSelect();
            UpdateTransition();
        }

        /// <summary>
        /// 加载选择方式
        /// </summary>
        private void UpdateSelect()
        {
            radBtn_First.Checked = false;
            radBtn_Last.Checked = false;
            radBtn_Max.Checked = false;
            switch (m_method.Para.LineCalliperParas.SelectType)
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
        }

        /// <summary>
        /// 更新极性
        /// </summary>
        private void UpdateTransition()
        {
            radBtn_Positive.Checked = false;
            radBtn_Negative.Checked = false;
            radBtn_All.Checked = false;
            switch (m_method.Para.LineCalliperParas.Direction)
            {
                case "all":
                    radBtn_All.Checked = true;
                    break;
                case "positive":
                    radBtn_Positive.Checked = true;
                    break;
                case "negative":
                    radBtn_Negative.Checked = true;
                    break;
            }
        }

        // 卡尺个数
        private void numUd_CalliperNum_ValueChanged(object sender, EventArgs e)
        {
            m_currentLine.CalliperCount = (int)numUd_CalliperNum.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 卡尺宽度
        private void numUD_CalliperWidth_ValueChanged(object sender, EventArgs e)
        {
            m_currentLine.RecWidth = (double)numUD_CalliperWidth.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 卡尺高度
        private void numUD_CalliperHeight_ValueChanged(object sender, EventArgs e)
        {
            m_currentLine.RecHeight = (double)numUD_CalliperHeight.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 对比度
        private void numUD_Contrast_ValueChanged(object sender, EventArgs e)
        {
            m_currentLine.GrayContrast = (int)numUD_Contrast.Value;
            m_method.Para.LineCalliperParas.GrayContrast = (int)numUD_Contrast.Value;
        }

        // 第一条线
        private void radBtn_First_CheckedChanged(object sender, EventArgs e)
        {
            m_currentLine.SelectType = "first";
            m_method.Para.LineCalliperParas.SelectType = "first";
        }

        // 最后一个点
        private void radBtn_Last_CheckedChanged(object sender, EventArgs e)
        {
            m_currentLine.SelectType = "last";
            m_method.Para.LineCalliperParas.SelectType = "last";
        }

        // 最大的点
        private void radBtn_Max_CheckedChanged(object sender, EventArgs e)
        {
            m_currentLine.SelectType = "max";
            m_method.Para.LineCalliperParas.SelectType = "max";
        }

        // 由黑到白
        private void radBtn_Positive_CheckedChanged(object sender, EventArgs e)
        {
            m_currentLine.Direction = "positive";
        }

        // 由白到黑
        private void radBtn_Negative_CheckedChanged(object sender, EventArgs e)
        {
            m_currentLine.Direction = "negative";
        }

        // all
        private void radBtn_All_CheckedChanged(object sender, EventArgs e)
        {
            m_currentLine.Direction = "all";
        }

        // 起始列坐标
        private void numUD_Col1_ValueChanged(object sender, EventArgs e)
        {
            m_currentLine.ColumnBegin = (double)numUD_Col1.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 起始行坐标
        private void numUD_Row1_ValueChanged(object sender, EventArgs e)
        {
            m_currentLine.RowBegin = (double)numUD_Row1.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 终止列坐标
        private void numUD_Col2_ValueChanged(object sender, EventArgs e)
        {
            m_currentLine.ColumnEnd = (double)numUD_Col2.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 终止行坐标
        private void numUD_Row2_ValueChanged(object sender, EventArgs e)
        {
            m_currentLine.RowEnd = (double)numUD_Row2.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 角度
        private void numUD_phi_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.Phi = (double)numUD_phi.Value;
            m_Disp.ViewWindowMethod.Repaint();
        }

        // 角度
        private void btn_Phi_Click(object sender, EventArgs e)
        {
            m_currentLine.Phi = -m_currentLine.Phi;
            m_Disp.ViewWindowMethod.Repaint();
            cmb_Disp.SelectedIndex = 0;
        }

        // 线
        private void chk_line_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.LineCtrlParas.Line = chk_line.Checked;
            cmb_Disp.SelectedIndex = 1;
            UpdateGraphic();
        }

        // 卡尺
        private void chk_calliper_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.LineCtrlParas.Calliper = chk_calliper.Checked;
            cmb_Disp.SelectedIndex = 1;
            UpdateGraphic();
        }

        // 编辑
        private void tsp_Edite_Click(object sender, EventArgs e)
        {
            cmb_Disp.SelectedIndex = 0;
        }

        // 运行
        private void tsp_run_Click(object sender, EventArgs e)
        {
            if (null != m_method.AffineTans)
            {
                HTuple AffineTans = null;
                HOperatorSet.HomMat2dInvert(m_method.AffineTans, out AffineTans);
                m_method.Para.LineCalliperParas.CopyFrom(m_currentLine, AffineTans);
            }
            else
            {
                m_method.Para.LineCalliperParas.CopyFrom(m_currentLine);
            }
            HTuple Second1 = null, Second2 = null;
            HOperatorSet.CountSeconds(out Second1);
            m_method.Run();
            cmb_Disp.SelectedIndex = 1;
            cmb_Disp_SelectedIndexChanged(null, null);
            HOperatorSet.CountSeconds(out Second2);
            double dTime = (Second2[0].D - Second1[0].D) * 1000;
            lbl_Time.Text = "运行时间：" + dTime + "ms";
            lbl_Msg.Text = m_method.RunMsg;
            UpdateGraphic();
            UpdateResultPara();
        }

        /// <summary>
        /// 更新图像结果
        /// </summary>
        private void UpdateGraphic()
        {
            HObject RegionAll = null;
            HOperatorSet.GenEmptyObj(out RegionAll);
            m_Disp.HobjectToHimage(m_hoImage);
            if (m_method.Para.LineCtrlParas.Line)
            {
                if (null != m_method.HoLine)
                {
                    HOperatorSet.ConcatObj(RegionAll, m_method.HoLine,
                                           out RegionAll);
                }
            }
            if (m_method.Para.LineCtrlParas.Calliper)
            {
                if (m_method.ListRows.Count > 0)
                {
                    HObject hoCross = null;
                    HOperatorSet.GenEmptyObj(out hoCross);
                    for (int i = 0; i < m_method.ListRows.Count; i++)
                    {
                        HOperatorSet.GenCrossContourXld(out hoCross, m_method.ListRows[i],
                                                        m_method.ListCols[i], 10, 1);
                        HOperatorSet.ConcatObj(RegionAll, hoCross, out RegionAll);
                    }
                }
            }
            m_Disp.DispObj(RegionAll, "green");
        }

        /// <summary>
        /// 更新结果数据
        /// </summary>
        private void UpdateResultPara()
        {
            dgv_result.Rows.Clear();
            if (m_method.ListRows.Count > 0)
            {
                for (int i = 0; i < m_method.ListRows.Count; i++)
                {
                    int dNum = dgv_result.Rows.Add();
                    dgv_result.Rows[dNum].Cells[0].Value = dNum;
                    dgv_result.Rows[dNum].Cells[1].Value = m_method.ListCols[i].ToString("F3");
                    dgv_result.Rows[dNum].Cells[2].Value = m_method.ListRows[i].ToString("F3");
                    dgv_result.Rows[dNum].Cells[3].Value = m_method.ListApptims[i].ToString("F3");
                }
            }
        }

        // 窗口显示
        private void cmb_Disp_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Disp.HobjectToHimage(m_hoImage);
            switch (cmb_Disp.SelectedIndex)
            {
                case 0:
                    m_Disp.ViewWindowMethod.GenLineCalliper(m_currentLine,
                                                            ref m_listROI);
                    break;
                case 1:
                    UpdateGraphic();
                    break;
            }
        }

        // 窗体关闭
        private void FindLineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != m_method.AffineTans)
            {
                HTuple AffineTans = null;
                HOperatorSet.HomMat2dInvert(m_method.AffineTans, out AffineTans);
                m_method.Para.LineCalliperParas.CopyFrom(m_currentLine, AffineTans);
            }
            else
            {
                m_method.Para.LineCalliperParas.CopyFrom(m_currentLine);
            }
        }

        // 结果dgv内容点击
        private void dgv_result_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (m_method.ListRows.Count > 0)
            {
                if (null != dgv_result.CurrentRow)
                {
                    int dNum = dgv_result.CurrentRow.Index;
                    HObject hoCross = null;
                    HOperatorSet.GenCrossContourXld(out hoCross, m_method.ListRows[dNum],
                                                    m_method.ListCols[dNum], 10, 1);
                    UpdateGraphic();
                    m_Disp.DispObj(hoCross, "red");
                }
            }
        }

        private void tsp_Save_Click(object sender, EventArgs e)
        {
            m_method.Save(m_method.Para, "D:\\FindLine.Xml");
        }
    }
}
