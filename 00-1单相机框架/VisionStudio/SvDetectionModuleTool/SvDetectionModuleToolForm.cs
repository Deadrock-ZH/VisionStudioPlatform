using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GVS.HalconDisp.ViewROI.Config;
using HalconDotNet;
using ParaResultAll;
using SvAngleLineLineTool;
using SvAngleLX;
using SvBlobTool;
using SvDistancePointPointTool;
using SvsFindCircleTool;
using SvVisualCorrectionTool;

namespace SvDetectionModuleTool
{
    /// <summary>
    /// 内 容:本类是检测模块界面
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public partial class SvDetectionModuleToolForm : Form
    {
        /// <summary>
        /// 模块方法类
        /// </summary>
        public SvDetectionModuleToolMethod m_SvDetectionModuleToolMethod = new SvDetectionModuleToolMethod();

        /// <summary>
        /// 显示控件
        /// </summary>
        private GVS.HalconDisp.Control.HWindow_Final m_Disp = new GVS.HalconDisp.Control.HWindow_Final();

        /// <summary>
        /// 输入图像
        /// </summary>
        private HObject m_hoImage = null;

        public SvDetectionModuleToolForm()
        {
            InitializeComponent();
        }

        // Form加载
        private void SvDetectionModuleToolForm_Load(object sender, EventArgs e)
        {
            // 显示控件添加
            pnl_disp.Controls.Clear();
            pnl_disp.Controls.Add(m_Disp);
            m_Disp.Dock = DockStyle.Fill;

            tsp_RunMsg.Text = m_SvDetectionModuleToolMethod.RunMsg;
            tsp_RunTime.Text = m_SvDetectionModuleToolMethod.RunTime.ToString("F2") + "ms";

            // 显示图像
            m_hoImage = m_SvDetectionModuleToolMethod.InputImage;
            m_Disp.HobjectToHimage(m_hoImage);
            cmb_Disp.SelectedIndex = 0;
            cmb_Disp_SelectedIndexChanged(null, null);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
            UpdateParaToCtrl();
        }

        #region 接受拖动到该界面的数据
        private void SvDetectionModuleToolForm_DragDrop(object sender, DragEventArgs e)
        {
            string m_strPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (File.Exists(m_strPath))
            {
                try
                {
                    HOperatorSet.ReadImage(out m_hoImage, m_strPath);
                    m_Disp.HobjectToHimage(m_hoImage);
                    m_SvDetectionModuleToolMethod.InputImage = m_hoImage;
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
        #endregion

        #region 工具更新相关函数

        /// <summary>
        /// 工具更新
        /// </summary>
        /// <param name="iDgvSelectIndex">第几行选中</param>
        private void UpdateTool(int iDgvSelectIndex)
        {
            dgv_Tool.Rows.Clear();
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                for (int i = 0; i < m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count; i++)
                {
                    dgv_Tool.Rows.Add();
                    dgv_Tool.Rows[i].Selected = false;
                    dgv_Tool.Rows[i].Cells[0].Value = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].ToolName;
                    //dgv_Tool.Rows[i].Cells[1].Value = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].RowState;
                    //dgv_Tool.Rows[i].Cells[2].Value = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].ColumnState;
                    //dgv_Tool.Rows[i].Cells[3].Value = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].RadiusState;
                    //dgv_Tool.Rows[i].Cells[4].Value = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].AngleState;
                    //dgv_Tool.Rows[i].Cells[5].Value = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].AffinehomState;
                }
                dgv_Tool.Rows[iDgvSelectIndex].Selected = true;
                dgv_Tool.CurrentCell = dgv_Tool[0, iDgvSelectIndex];
            }
        }

        /// <summary>
        /// 首次添加工具时的各种设置状态
        /// </summary>
        /// <param name="strToolName">工具名称</param>
        /// <param name="bRow"></param>
        /// <param name="bColumn"></param>
        /// <param name="bRadius"></param>
        /// <param name="bAngle"></param>
        /// <param name="bAffineHom"></param>
        private void UpdateAddToolFirst(string strToolName, bool bRow, bool bColumn, bool bRadius, bool bAngle, bool bAffineHom)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd = new ToolParaAdd();
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.ToolName = strToolName;
            //m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.RowState = bRow;
            //m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.ColumnState = bColumn;
            //m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.RadiusState = bRadius;
            //m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.AngleState = bAngle;
            //m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.AffinehomState = bAffineHom;
        }

        /// <summary>
        /// 将参数更新到界面
        /// </summary>
        private void UpdateParaToCtrl()
        {
            chk_Blob.Checked = m_SvDetectionModuleToolMethod.ModualPara.BlobState;
            Chk_Circle.Checked = m_SvDetectionModuleToolMethod.ModualPara.CircleState;
            chk_找线.Checked = m_SvDetectionModuleToolMethod.ModualPara.LineState;
            chk_匹配.Checked = m_SvDetectionModuleToolMethod.ModualPara.PatMaxState;
            chk_AngleLX.Checked = m_SvDetectionModuleToolMethod.ModualPara.AngleLXState;
            chk_distancePl.Checked = m_SvDetectionModuleToolMethod.ModualPara.DistancePLState;
            chk_点点距离.Checked = m_SvDetectionModuleToolMethod.ModualPara.DistancePPState;
            chk_LineLineAngle.Checked = m_SvDetectionModuleToolMethod.ModualPara.AngleLLState;
        }
        #endregion

        #region 添加、删除、上移、下移、编辑工具

        // 添加匹配工具
        private void 匹配工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.PatMaxPara = new SvsPatMax.SvsPatMaxPara();
            UpdateAddToolFirst("匹配工具", false, false, false, false, false);
            m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Add(m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
        }

        // 添加找线工具
        private void 找线工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.FindLineParas = new SvsFindLineTool.FindLinePara();
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.FindLineParas.LineCalliperParas = new GVS.HalconDisp.ViewWindow.Config.LineCalliper();
            UpdateAddToolFirst("找线工具", false, false, false, false, false);
            m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Add(m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
        }

        // 添加找圆工具
        private void 找圆工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.FindCircleParas = new SvsFindCircleTool.FindCirclePara();
            UpdateAddToolFirst("找圆工具", false, false, false, false, false);
            m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Add(m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
        }

        // 添加blob工具
        private void blob工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.BlobParas = new SvBlobTool.BlobToolPara();
            UpdateAddToolFirst("Blob工具", false, false, false, false, false);
            m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Add(m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
        }

        // 工具编辑
        private void 编辑_Click(object sender, EventArgs e)
        {
            if (dgv_Tool.CurrentRow != null)
            {
                int iNow = dgv_Tool.CurrentRow.Index;
                dgv_Tool.Rows[iNow].Selected = true;
                switch (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].ToolName)
                {
                    case "匹配工具":
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = true;
                        m_SvDetectionModuleToolMethod.StateFindLine = true;
                        m_SvDetectionModuleToolMethod.StateFindCircle = true;
                        m_SvDetectionModuleToolMethod.StateBlob = true;
                        m_SvDetectionModuleToolMethod.Run();

                        SvsPatMax.PatMaxForm dlgPatMax = new SvsPatMax.PatMaxForm();
                        m_SvDetectionModuleToolMethod.m_PatMaxMethod.PatMaxName = m_SvDetectionModuleToolMethod.PatMaxName + iNow;
                        dlgPatMax.m_method = m_SvDetectionModuleToolMethod.m_PatMaxMethod;
                        dlgPatMax.m_method.Para = new SvsPatMax.SvsPatMaxPara();
                        dlgPatMax.m_method.Para = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].PatMaxPara;
                        dlgPatMax.m_method.Para.CreatePara = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].PatMaxPara.CreatePara;
                        dlgPatMax.m_method.ModelID = m_SvDetectionModuleToolMethod.m_ListModelID[iNow];
                        dlgPatMax.m_method.FindContourModel = null;
                        dlgPatMax.m_method.InputImage = m_hoImage;
                        dlgPatMax.ShowDialog();
                        m_SvDetectionModuleToolMethod.m_ListModelID[iNow] = dlgPatMax.m_method.ModelID;
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].PatMaxPara.CreatePara = dlgPatMax.m_method.Para.CreatePara;
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].PatMaxPara.FindPara = dlgPatMax.m_method.Para.FindPara;
                        break;
                    case "找线工具":
                        // 当前找线工具参数传递
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = true;
                        m_SvDetectionModuleToolMethod.StateFindLine = true;
                        m_SvDetectionModuleToolMethod.Run();

                        // 当前找线工具参数传递
                        SvsFindLineTool.FindLineForm dlgFindLine = new SvsFindLineTool.FindLineForm();
                        dlgFindLine.m_method = new SvsFindLineTool.FindLineMethod();
                        dlgFindLine.m_method.Para = new SvsFindLineTool.FindLinePara();
                        dlgFindLine.m_method.Para = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].FindLineParas;
                        dlgFindLine.m_method.Para.LineCalliperParas = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].FindLineParas.LineCalliperParas;
                        dlgFindLine.m_method.ListParaResultAll = m_SvDetectionModuleToolMethod.ListParaResultAll;
                        dlgFindLine.m_method.InputImage = m_hoImage;
                        dlgFindLine.ShowDialog();
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].FindLineParas = dlgFindLine.m_method.Para;
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].FindLineParas.LineCalliperParas = dlgFindLine.m_method.Para.LineCalliperParas;
                        m_SvDetectionModuleToolMethod.StateFindLine = true;
                        break;

                    // 当前找圆工具参数传递
                    case "找圆工具":

                        // 当前找线工具参数传递
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = true;
                        m_SvDetectionModuleToolMethod.StateFindLine = true;
                        m_SvDetectionModuleToolMethod.StateFindCircle = true;
                        m_SvDetectionModuleToolMethod.Run();

                        // 当前找圆工具参数传递
                        FindCircleForm dlgFindCircle = new FindCircleForm();
                        dlgFindCircle.m_method = new FindCircleMethod();
                        dlgFindCircle.m_method.Para = new FindCirclePara();
                        dlgFindCircle.m_method.Para = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].FindCircleParas;
                        dlgFindCircle.m_method.Para.CircleCalliper = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].FindCircleParas.CircleCalliper;
                        dlgFindCircle.m_method.ListParaResultAll = m_SvDetectionModuleToolMethod.ListParaResultAll;
                        dlgFindCircle.m_method.InputImage = m_hoImage;
                        dlgFindCircle.ShowDialog();
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].FindCircleParas = dlgFindCircle.m_method.Para;
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].FindCircleParas.CircleCalliper = dlgFindCircle.m_method.Para.CircleCalliper;
                        m_SvDetectionModuleToolMethod.StateFindCircle = true;
                        break;
                    case "Blob工具":
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = true;
                        m_SvDetectionModuleToolMethod.StateFindLine = true;
                        m_SvDetectionModuleToolMethod.StateFindCircle = true;
                        m_SvDetectionModuleToolMethod.StateBlob = true;
                        m_SvDetectionModuleToolMethod.StateAngleLL = true;
                        m_SvDetectionModuleToolMethod.Run();

                        // 当前找圆工具参数传递
                        BlobToolForm dlgBlob = new BlobToolForm();
                        dlgBlob.m_BlobMethod = new BlobToolMethod();
                        dlgBlob.m_BlobMethod.ParaBlob = new BlobToolPara();
                        dlgBlob.m_BlobMethod.ParaBlob = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].BlobParas;
                        dlgBlob.m_BlobMethod.ListClassParaResultAll = m_SvDetectionModuleToolMethod.ListParaResultAll;
                        dlgBlob.m_BlobMethod.InputImage = m_hoImage;
                        dlgBlob.ShowDialog();
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].BlobParas = dlgBlob.m_BlobMethod.ParaBlob;
                        m_SvDetectionModuleToolMethod.StateBlob = true;
                        break;
                    case "点线距离工具":
                        m_SvDetectionModuleToolMethod.StateDistancePL = false;
                        m_SvDetectionModuleToolMethod.StateAngleLX = false;
                        m_SvDetectionModuleToolMethod.StateDistancePP = false;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = false;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = false;
                        m_SvDetectionModuleToolMethod.StateAngleLL = true;
                        m_SvDetectionModuleToolMethod.Run();
                        SvDistancePointLineTool.SvDistancePointLineToolForm dlgDistancePL = new SvDistancePointLineTool.SvDistancePointLineToolForm();
                        dlgDistancePL.m_SvDistancePointLineToolMethod = m_SvDetectionModuleToolMethod.m_SvDistancePointLineToolMethod;
                        dlgDistancePL.m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas = new SvDistancePointLineTool.SvDistancePointLineToolPara();
                        dlgDistancePL.m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].DistancePointLineToolParas;
                        dlgDistancePL.m_SvDistancePointLineToolMethod.InputImage = m_hoImage;
                        dlgDistancePL.m_SvDistancePointLineToolMethod.ListParaResultAll = m_SvDetectionModuleToolMethod.ListParaResultAll;
                        dlgDistancePL.ShowDialog();
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].DistancePointLineToolParas = dlgDistancePL.m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        break;
                    case "点点距离工具":
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = false;
                        m_SvDetectionModuleToolMethod.StateAngleLX = false;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = false;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = false;
                        m_SvDetectionModuleToolMethod.StateAngleLL = true;
                        m_SvDetectionModuleToolMethod.Run();
                        SvDistancePointPointTool.SvDistancePointPointToolForm dlgDistancePP = new SvDistancePointPointToolForm();
                        dlgDistancePP.m_DistancePointPointToolMethod = new SvDistancePointPointToolMethod();
                        dlgDistancePP.m_DistancePointPointToolMethod.SvDistancePointPointToolParas = new SvDistancePointPointToolPara();
                        dlgDistancePP.m_DistancePointPointToolMethod.SvDistancePointPointToolParas = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].DistancePointPointToolParas;
                        dlgDistancePP.m_DistancePointPointToolMethod.InputImage = m_hoImage;
                        dlgDistancePP.m_DistancePointPointToolMethod.ListParaResultAll = m_SvDetectionModuleToolMethod.ListParaResultAll;
                        dlgDistancePP.ShowDialog();
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].DistancePointPointToolParas = dlgDistancePP.m_DistancePointPointToolMethod.SvDistancePointPointToolParas;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        break;
                    case "AngleLX工具":
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = false;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = false;
                        m_SvDetectionModuleToolMethod.StateFindLine = true;
                        m_SvDetectionModuleToolMethod.StateFindCircle = true;
                        m_SvDetectionModuleToolMethod.StateAngleLL = true;
                        m_SvDetectionModuleToolMethod.Run();
                        SvAngleLX.SvAngleLXForm dlgAngleLX = new SvAngleLXForm();
                        dlgAngleLX.m_AnglelxMethod = m_SvDetectionModuleToolMethod.m_AngleLXMethod;
                        dlgAngleLX.m_AnglelxMethod.SvAngleLXParas = new SvAngleLXToolPara();
                        dlgAngleLX.m_AnglelxMethod.SvAngleLXParas = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].AngleLxPara;
                        dlgAngleLX.m_AnglelxMethod.InputImage = m_hoImage;
                        dlgAngleLX.m_AnglelxMethod.ListParaResultAll = m_SvDetectionModuleToolMethod.ListParaResultAll;
                        dlgAngleLX.ShowDialog();
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].AngleLxPara = dlgAngleLX.m_AnglelxMethod.SvAngleLXParas;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        m_SvDetectionModuleToolMethod.StateFindLine = true;
                        m_SvDetectionModuleToolMethod.StateFindCircle = true;
                        break;

                    case "线线角度工具":
                        m_SvDetectionModuleToolMethod.StateDistancePP = false;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = false;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = false;
                        m_SvDetectionModuleToolMethod.StateFindLine = true;
                        m_SvDetectionModuleToolMethod.StateFindCircle = true;
                        m_SvDetectionModuleToolMethod.StateAngleLL = true;
                        m_SvDetectionModuleToolMethod.Run();
                        SvAngleLineLineTool.SvAngleLineLineToolForm dlgAngleLL = new SvAngleLineLineToolForm();
                        dlgAngleLL.m_AnglellMethod = m_SvDetectionModuleToolMethod.m_AngleLLMethod;
                        dlgAngleLL.m_AnglellMethod.Para = new SvAngleLineLineToolPara();
                        dlgAngleLL.m_AnglellMethod.Para = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].SvAngleLineLineToolParas;
                        dlgAngleLL.m_AnglellMethod.InputImage = m_hoImage;
                        dlgAngleLL.m_AnglellMethod.ListParaResultAll = m_SvDetectionModuleToolMethod.ListParaResultAll;
                        dlgAngleLL.ShowDialog();
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].SvAngleLineLineToolParas = dlgAngleLL.m_AnglellMethod.Para;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        m_SvDetectionModuleToolMethod.StateFindLine = true;
                        m_SvDetectionModuleToolMethod.StateFindCircle = true;
                        m_SvDetectionModuleToolMethod.StateAngleLL = true;
                        break;

                    case "公式示教工具":
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = false;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = false;
                        m_SvDetectionModuleToolMethod.Run();
                        SvVisualCorrectionToolForm dlgVisualCorrectTool = new SvVisualCorrectionToolForm();
                        dlgVisualCorrectTool.m_VisualCorrectionToolMethod = m_SvDetectionModuleToolMethod.m_SvVisualCorrectionToolMethod;
                        dlgVisualCorrectTool.m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara = new SvVisualCorrectionToolPara();
                        dlgVisualCorrectTool.m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].SvVisualCorrectionToolParas;
                        dlgVisualCorrectTool.m_VisualCorrectionToolMethod.InputImage = m_hoImage;
                        dlgVisualCorrectTool.m_VisualCorrectionToolMethod.ListParaResultAll = m_SvDetectionModuleToolMethod.ListParaResultAll;
                        dlgVisualCorrectTool.ShowDialog();
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].SvVisualCorrectionToolParas = dlgVisualCorrectTool.m_VisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        break;
                    case "结果判断工具":
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = false;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        m_SvDetectionModuleToolMethod.Run();
                        SvJudgeResultParaTool.SvJudgeResultParaToolForm dlgSvJudgeResultParaTool = new SvJudgeResultParaTool.SvJudgeResultParaToolForm();
                        dlgSvJudgeResultParaTool.m_JudgeMethod = m_SvDetectionModuleToolMethod.m_SvJudgeResultParaToolMethod;
                        dlgSvJudgeResultParaTool.m_JudgeMethod.ParasSvJudgeResultParaTool = new SvJudgeResultParaTool.SvJudgeResultParaToolPara();
                        dlgSvJudgeResultParaTool.m_JudgeMethod.ParasSvJudgeResultParaTool = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].SvJudgeResultParaToolParas;
                        dlgSvJudgeResultParaTool.m_JudgeMethod.InputImage = m_hoImage;
                        dlgSvJudgeResultParaTool.m_JudgeMethod.ListParaResultAll = m_SvDetectionModuleToolMethod.ListParaResultAll;
                        dlgSvJudgeResultParaTool.ShowDialog();
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[iNow].SvJudgeResultParaToolParas = dlgSvJudgeResultParaTool.m_JudgeMethod.ParasSvJudgeResultParaTool;
                        m_SvDetectionModuleToolMethod.StateDistancePL = true;
                        m_SvDetectionModuleToolMethod.StateAngleLX = true;
                        m_SvDetectionModuleToolMethod.StateDistancePP = true;
                        m_SvDetectionModuleToolMethod.StateJudgeResult = true;
                        m_SvDetectionModuleToolMethod.StateVisualCorrect = true;
                        break;
                }

            }
        }

        // delete
        private void tsp_delete_Click(object sender, EventArgs e)
        {
            if (dgv_Tool.CurrentRow != null && m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > -1)
            {
                if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
                {
                    m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.RemoveAt(dgv_Tool.CurrentRow.Index);
                    UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
                }
            }
        }

        // 上移
        private void tsp_MoveUp_Click(object sender, EventArgs e)
        {
            if (dgv_Tool.CurrentRow != null)
            {
                if (dgv_Tool.CurrentRow.Index > 0)
                {
                    if (dgv_Tool.CurrentRow.Index > 0)
                    {
                        ToolParaAdd INowPara = new ToolParaAdd();
                        ToolParaAdd IBeforePara = new ToolParaAdd();
                        INowPara = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[dgv_Tool.CurrentRow.Index];
                        IBeforePara = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[dgv_Tool.CurrentRow.Index - 1];
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[dgv_Tool.CurrentRow.Index] = IBeforePara;
                        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[dgv_Tool.CurrentRow.Index - 1] = INowPara;
                        UpdateTool(dgv_Tool.CurrentRow.Index - 1);
                    }
                }
            }
        }

        // 下移
        private void tsp_MoveDown_Click(object sender, EventArgs e)
        {
            if (null != dgv_Tool.CurrentRow && dgv_Tool.CurrentRow.Index + 1 <= m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1)
            {
                if (dgv_Tool.CurrentRow.Index < m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1)
                {
                    ToolParaAdd INowPara = new ToolParaAdd();
                    ToolParaAdd INextPara = new ToolParaAdd();
                    INowPara = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[dgv_Tool.CurrentRow.Index];
                    INextPara = m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[dgv_Tool.CurrentRow.Index + 1];
                    m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[dgv_Tool.CurrentRow.Index] = INextPara;
                    m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[dgv_Tool.CurrentRow.Index + 1] = INowPara;
                    UpdateTool(dgv_Tool.CurrentRow.Index + 1);
                }
            }

        }

        #endregion

        #region 单元格数据更改

        // 单元格状态数据更改
        private void dgv_Tool_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgv_Tool.CurrentRow != null)
            //{
            //    dgv_Tool.Rows[dgv_Tool.RowCount - 1].Selected = false;
            //    int i = dgv_Tool.CurrentRow.Index;

            //    // 当某行数据都有值时更改
            //    if (dgv_Tool.Rows.Count > 0 &&
            //        dgv_Tool.Rows[i].Cells[0].Value != null &&
            //        dgv_Tool.Rows[i].Cells[1].Value != null
            //        && dgv_Tool.Rows[i].Cells[2].Value != null &&
            //        dgv_Tool.Rows[i].Cells[3].Value != null &&
            //        dgv_Tool.Rows[i].Cells[4].Value != null
            //        && dgv_Tool.Rows[i].Cells[5].Value != null)
            //    {
            //        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].ToolName = (string)dgv_Tool.Rows[i].Cells[0].Value;
            //        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].RowState = (bool)dgv_Tool.Rows[i].Cells[1].Value;
            //        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].ColumnState = (bool)dgv_Tool.Rows[i].Cells[2].Value;
            //        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].RadiusState = (bool)dgv_Tool.Rows[i].Cells[3].Value;
            //        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].AngleState = (bool)dgv_Tool.Rows[i].Cells[4].Value;
            //        m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd[i].AffinehomState = (bool)dgv_Tool.Rows[i].Cells[5].Value;
            //    }
            //}
        }

        // 提交未提交控件的更改
        private void dgv_Tool_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_Tool.IsCurrentCellDirty)
            {
                dgv_Tool.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // 选中状态修改
        private void dgv_Tool_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Tool.CurrentRow != null)
            {
                int i = dgv_Tool.CurrentRow.Index;
                dgv_Tool.Rows[i].Selected = true;
            }
        }
        #endregion


        // 运行
        private void tsp_Run_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.StateAngleLX = true;
            m_SvDetectionModuleToolMethod.StateDistancePL = true;
            m_SvDetectionModuleToolMethod.Run();
            cmb_Disp.SelectedIndex = 1;
            cmb_Disp_SelectedIndexChanged(null, null);
            tsp_RunMsg.Text = m_SvDetectionModuleToolMethod.RunMsg;
            tsp_RunTime.Text = m_SvDetectionModuleToolMethod.RunTime.ToString("F2") + "ms";
            UpdateResultData();
        }

        /// <summary>
        /// 更新结果数据
        /// </summary>
        private void UpdateResultData()
        {
            dgv_匹配.Rows.Clear();
            dgv_找圆.Rows.Clear();
            dgv_找线.Rows.Clear();
            dgv_Blob.Rows.Clear();
            dgv_角度计算.Rows.Clear();
            dgv_点线距离.Rows.Clear();
            dgv_DistancePP.Rows.Clear();
            dgv_LinelineAngle.Rows.Clear();
            int i = 0;
            for (int item = 0; item < m_SvDetectionModuleToolMethod.ListParaResultAll.Count; item++)
            {
                if (m_SvDetectionModuleToolMethod.ListParaResultAll[item].ToolName.Contains("匹配工具" + item))
                {
                    i = dgv_匹配.Rows.Add();
                    dgv_匹配.Rows[i].Cells[0].Value = "匹配工具" + item;
                    dgv_匹配.Rows[i].Cells[1].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].PatMaxResultPara.Row.ToString("F0");
                    dgv_匹配.Rows[i].Cells[2].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].PatMaxResultPara.Col.ToString("F0");
                    dgv_匹配.Rows[i].Cells[3].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].PatMaxResultPara.Angle.ToString("F1");
                }
                if (m_SvDetectionModuleToolMethod.ListParaResultAll[item].ToolName.Contains("找线工具" + item))
                {
                    i = dgv_找线.Rows.Add();
                    dgv_找线.Rows[i].Cells[0].Value = "找线工具" + item;
                    dgv_找线.Rows[i].Cells[1].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].FindLineResultPara.RowStart.ToString("F0");
                    dgv_找线.Rows[i].Cells[2].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].FindLineResultPara.ColStart.ToString("F0");
                    dgv_找线.Rows[i].Cells[3].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].FindLineResultPara.RowEnd.ToString("F0");
                    dgv_找线.Rows[i].Cells[4].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].FindLineResultPara.ColEnd.ToString("F0");
                }
                if (m_SvDetectionModuleToolMethod.ListParaResultAll[item].ToolName.Contains("找圆工具" + item))
                {
                    i = dgv_找圆.Rows.Add();
                    dgv_找圆.Rows[i].Cells[0].Value = "找圆工具" + item;
                    dgv_找圆.Rows[i].Cells[1].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].FindCircleResultPara.Row.ToString("F0");
                    dgv_找圆.Rows[i].Cells[2].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].FindCircleResultPara.Col.ToString("F0");
                }
                if (m_SvDetectionModuleToolMethod.ListParaResultAll[item].ToolName.Contains("Blob工具" + item))
                {
                    i = dgv_Blob.Rows.Add();
                    foreach (ParaResult itemBlob in m_SvDetectionModuleToolMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara)
                    {
                        dgv_Blob.Rows[i].Cells[0].Value = "Blob工具" + item;
                        dgv_Blob.Rows[i].Cells[1].Value = itemBlob.Row.ToString("F0");
                        dgv_Blob.Rows[i].Cells[2].Value = itemBlob.Col.ToString("F0");
                        dgv_Blob.Rows[i].Cells[3].Value = itemBlob.Area.ToString("F1");
                    }
                }
                if (m_SvDetectionModuleToolMethod.ListParaResultAll[item].ToolName.Contains("AngleLX工具" + item))
                {
                    i = dgv_角度计算.Rows.Add();
                    dgv_角度计算.Rows[i].Cells[0].Value = "AngleLX工具" + item;
                    dgv_角度计算.Rows[i].Cells[1].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].ParasAngleLXResult.Angle.ToString("F3");
                }
                if (m_SvDetectionModuleToolMethod.ListParaResultAll[item].ToolName.Contains("点线距离工具" + item))
                {
                    i = dgv_点线距离.Rows.Add();
                    dgv_点线距离.Rows[i].Cells[0].Value = "点线距离工具" + item;
                    dgv_点线距离.Rows[i].Cells[1].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].ParaDistancePointLines.Row.ToString("F0");
                    dgv_点线距离.Rows[i].Cells[2].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].ParaDistancePointLines.Col.ToString("F0");
                    dgv_点线距离.Rows[i].Cells[3].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].ParaDistancePointLines.Distance.ToString("F2");
                }
                if (m_SvDetectionModuleToolMethod.ListParaResultAll[item].ToolName.Contains("点点距离工具" + item))
                {
                    i = dgv_DistancePP.Rows.Add();
                    dgv_DistancePP.Rows[i].Cells[0].Value = "点点距离工具" + item;
                    dgv_DistancePP.Rows[i].Cells[1].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].ParaDistancePointPoints.Distance.ToString("F2");
                    dgv_DistancePP.Rows[i].Cells[2].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].ParaDistancePointPoints.Row.ToString("F2");
                    dgv_DistancePP.Rows[i].Cells[3].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].ParaDistancePointPoints.Col.ToString("F2");
                }
                if (m_SvDetectionModuleToolMethod.ListParaResultAll[item].ToolName.Contains("线线角度工具" + item))
                {
                    i = dgv_DistancePP.Rows.Add();
                    dgv_DistancePP.Rows[i].Cells[0].Value = "线线角度工具" + item;
                    dgv_DistancePP.Rows[i].Cells[1].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].ParaParaAngleLL.Angle.ToString("F2");
                    dgv_DistancePP.Rows[i].Cells[2].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].ParaParaAngleLL.Row.ToString("F2");
                    dgv_DistancePP.Rows[i].Cells[3].Value = m_SvDetectionModuleToolMethod.ListParaResultAll[item].ParaParaAngleLL.Col.ToString("F2");
                }
            }
        }

        // 显示匹配
        private void chk_匹配_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.PatMaxState = chk_匹配.Checked;
            cmb_Disp_SelectedIndexChanged(null, null);
        }

        // 显示找线
        private void chk_找线_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.LineState = chk_找线.Checked;
            cmb_Disp_SelectedIndexChanged(null, null);
        }

        // 显示找圆
        private void Chk_Circle_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.CircleState = Chk_Circle.Checked;
            cmb_Disp_SelectedIndexChanged(null, null);
        }

        // 显示blob
        private void chk_Blob_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.BlobState = chk_Blob.Checked;
            cmb_Disp_SelectedIndexChanged(null, null);
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
                    m_Disp.HobjectToHimage(m_hoImage);
                    UpdateResultGraphic();
                    break;
            }
        }

        /// <summary>
        /// 更新结果区域
        /// </summary>
        private void UpdateResultGraphic()
        {
            m_Disp.HobjectToHimage(m_hoImage);
            m_Disp.DisplayMessage(m_SvDetectionModuleToolMethod.SendMsg, 100, 100, "blue", true);
            if (m_SvDetectionModuleToolMethod.ModualPara.BlobState)
            {
                foreach (HObjectWithColor item in m_SvDetectionModuleToolMethod.ListBlobReg)
                {
                    m_Disp.DispObj(item.HObject, item.Color, "fill");
                }
            }
            if (m_SvDetectionModuleToolMethod.ModualPara.CircleState)
            {
                foreach (HObjectWithColor item in m_SvDetectionModuleToolMethod.ListCircleReg)
                {
                    m_Disp.DispObj(item.HObject, item.Color, "margin");
                }
            }
            if (m_SvDetectionModuleToolMethod.ModualPara.LineState)
            {
                foreach (HObjectWithColor item in m_SvDetectionModuleToolMethod.ListLineReg)
                {
                    m_Disp.DispObj(item.HObject, item.Color, "margin");
                }
            }
            if (m_SvDetectionModuleToolMethod.ModualPara.PatMaxState)
            {
                foreach (HObjectWithColor item in m_SvDetectionModuleToolMethod.ListPatMaxReg)
                {
                    m_Disp.DispObj(item.HObject, item.Color, "margin");
                }
            }
            if (m_SvDetectionModuleToolMethod.ModualPara.AngleLXState)
            {
                foreach (HObjectWithColor item in m_SvDetectionModuleToolMethod.ListAngleLXReg)
                {
                    m_Disp.DispObj(item.HObject, item.Color, "margin");
                }
            }
            if (m_SvDetectionModuleToolMethod.ModualPara.DistancePLState)
            {
                foreach (HObjectWithColor item in m_SvDetectionModuleToolMethod.ListDistancePLReg)
                {
                    m_Disp.DispObj(item.HObject, item.Color, "margin");
                }
            }
            if (m_SvDetectionModuleToolMethod.ModualPara.DistancePPState)
            {
                foreach (HObjectWithColor item in m_SvDetectionModuleToolMethod.ListDistancePPReg)
                {
                    m_Disp.DispObj(item.HObject, item.Color, "margin");
                }
            }
            if (m_SvDetectionModuleToolMethod.ModualPara.AngleLLState)
            {
                foreach (HObjectWithColor item in m_SvDetectionModuleToolMethod.ListAngleLLReg)
                {
                    m_Disp.DispObj(item.HObject, item.Color, "margin");
                }
            }
        }

        // 运行按钮
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.Run();
            SvAngleLX.SvAngleLXForm dlg = new SvAngleLX.SvAngleLXForm();
            dlg.m_AnglelxMethod = m_SvDetectionModuleToolMethod.m_AngleLXMethod;
            dlg.m_AnglelxMethod.ListParaResultAll = m_SvDetectionModuleToolMethod.ListParaResultAll;
            dlg.ShowDialog();
        }

        // 添加AngleLX工具
        private void angleLX工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.AngleLxPara = new SvAngleLX.SvAngleLXToolPara();
            UpdateAddToolFirst("AngleLX工具", false, false, false, false, false);
            m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Add(m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
        }

        private void chk_AngleLX_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.AngleLXState = chk_AngleLX.Checked;
            cmb_Disp_SelectedIndexChanged(null, null);
        }

        private void distancePointLine工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.DistancePointLineToolParas = new SvDistancePointLineTool.SvDistancePointLineToolPara();
            UpdateAddToolFirst("点线距离工具", false, false, false, false, false);
            m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Add(m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
        }

        private void chk_distancePl_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.DistancePLState = chk_distancePl.Checked;
            cmb_Disp_SelectedIndexChanged(null, null);
        }

        private void 点点距离工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.DistancePointPointToolParas = new SvDistancePointPointTool.SvDistancePointPointToolPara();
            UpdateAddToolFirst("点点距离工具", false, false, false, false, false);
            m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Add(m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
        }

        private void chk_点点距离_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.DistancePPState = chk_点点距离.Checked;
            cmb_Disp_SelectedIndexChanged(null, null);
        }

        private void 结果判断工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.SvJudgeResultParaToolParas = new SvJudgeResultParaTool.SvJudgeResultParaToolPara();
            UpdateAddToolFirst("结果判断工具", false, false, false, false, false);
            m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Add(m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
        }

        private void 示教工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.SvVisualCorrectionToolParas = new SvVisualCorrectionToolPara();
            UpdateAddToolFirst("公式示教工具", false, false, false, false, false);
            m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Add(m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
        }

        private void 线线角度工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd.SvAngleLineLineToolParas = new SvAngleLineLineTool.SvAngleLineLineToolPara();
            UpdateAddToolFirst("线线角度工具", false, false, false, false, false);
            m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Add(m_SvDetectionModuleToolMethod.ModualPara.ToolParasAdd);
            if (m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count > 0)
            {
                UpdateTool(m_SvDetectionModuleToolMethod.ModualPara.ListToolParaAdd.Count - 1);
            }
        }

        private void chk_LineLineAngle_CheckedChanged(object sender, EventArgs e)
        {
            m_SvDetectionModuleToolMethod.ModualPara.AngleLLState = chk_LineLineAngle.Checked;
            cmb_Disp_SelectedIndexChanged(null, null);
        }
    }
}
