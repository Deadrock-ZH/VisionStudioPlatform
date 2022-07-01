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
using SvMask;
using GVS.HalconDisp.ViewROI.Config;

namespace SvsPatMax
{
    /// <summary>
    /// 本类是匹配界面
    /// </summary>
    public partial class PatMaxForm : Form
    {
        #region 参数
        /// <summary>
        /// 显示控件
        /// </summary>
        private GVS.HalconDisp.Control.HWindow_Final m_Disp = new GVS.HalconDisp.Control.HWindow_Final();

        /// <summary>
        /// 小的显示窗口
        /// </summary>
        private GVS.HalconDisp.Control.HWindow_Final m_dispSmall = new GVS.HalconDisp.Control.HWindow_Final();

        /// <summary>
        /// 模板方法类
        /// </summary>
        public SvsPatMaxMethod m_method;

        /// <summary>
        /// 是否模块使用
        /// </summary>
        public bool m_bModual = false;

        /// <summary>
        /// 创建区域组合的界面
        /// </summary>
        GVS.HalconDisp.Control.RegionTypeCtrl m_roiCreateType = new GVS.HalconDisp.Control.RegionTypeCtrl();

        /// <summary>
        /// 匹配区域组合的界面
        /// </summary>
        GVS.HalconDisp.Control.RegionTypeCtrl m_roiFindType = new GVS.HalconDisp.Control.RegionTypeCtrl();

        /// <summary>
        /// 存储ROI
        /// </summary>
        private List<GVS.HalconDisp.ViewWindow.Model.ROI> m_listRoi = new List<GVS.HalconDisp.ViewWindow.Model.ROI>();

        /// <summary>
        /// 输入图像
        /// </summary>
        private HObject m_hoImage = null;

        /// <summary>
        /// 是否训练
        /// </summary>
        private bool m_bTrain = false;

        /// <summary>
        /// 小显示窗口显示的图片
        /// </summary>
        private HObject m_hoReducedImage = null;

        /// <summary>
        /// 标准参数(false：向下)
        /// </summary>
        private bool m_bStandard = false;


        /// <summary>
        /// 高级参数（false:向下）
        /// </summary>
        private bool m_bGaoji = true;

        /// <summary>
        /// 用户标准参数(false：向下)
        /// </summary>
        private bool m_bStandardUser = false;

        /// <summary>
        /// 用户高级参数（false:向下）
        /// </summary>
        private bool m_bGaojiUser = true;

        /// <summary>
        /// 是否首次加载
        /// </summary>
        private bool m_bFirst = true;

        /// <summary>
        /// 创建状态
        /// </summary>
        private bool m_bCreateState = true;

        /// <summary>
        /// 创建信息
        /// </summary>
        private string m_strCreateMessage = "";

        /// <summary>
        /// 匹配状态
        /// </summary>
        private bool m_bFindState = true;

        ///// <summary>
        ///// 匹配信息
        ///// </summary>
        //private string m_strFindMessage = "";

        ///// <summary>
        ///// 彷射矩阵
        ///// </summary>
        //private HTuple m_affineHom = null;

        /// <summary>
        /// 运行时间
        /// </summary>
        private double m_dTime = 0;

        /// <summary>
        /// 掩模方法类
        /// </summary>
        private SvMaskMethod m_MaskMethod = new SvMaskMethod();
        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public PatMaxForm()
        {
            InitializeComponent();
        }

        // load
        private void PatMaxForm_Load(object sender, EventArgs e)
        {
            HOperatorSet.SetSystem("border_shape_models", "true");
            m_bFirst = true;
            txt_Create.Text = "";
            txt_Find.Text = "";
            if (m_method.Para != null)
            {
                m_method.Para.PretreatementPara.PretreatmentCheck = false;
            }

            // 小窗口
            pnl_SmallDisp.Controls.Add(m_dispSmall);
            m_dispSmall.Dock = DockStyle.Fill;
            m_dispSmall.ShowStatusBar(false);
            m_dispSmall.HobjectToHimage(m_hoReducedImage);

            // 大窗口
            pnl_Disp.Controls.Add(m_Disp);
            m_Disp.Dock = DockStyle.Fill;

            // 默认显示输入窗口
            cmb_Disp.SelectedIndex = 0;
            if (null != m_method.InputImage)
            {
                HOperatorSet.CopyImage(m_method.InputImage, out m_hoImage);
            }
            // m_hoimage = m_method.InputImage;
            m_Disp.HobjectToHimage(m_hoImage);

            // 创建模板类型
            switch (m_method.Para.CreatePara.ModelTypes)
            {
                case ModelType.形状:
                    cmb_CreateName.SelectedIndex = 0;
                    break;
                case ModelType.轮廓:
                    cmb_CreateName.SelectedIndex = 1;
                    break;
                case ModelType.灰度:
                    cmb_CreateName.SelectedIndex = 2;
                    break;
                default:
                    cmb_CreateName.SelectedIndex = 0;
                    break;
            }

            // 模板区域类型使用 
            m_roiCreateType.SetRegTypeGroup(new GVS.HalconDisp.Control.RegionType[]
                                    { GVS.HalconDisp.Control.RegionType.矩形1,
                                      GVS.HalconDisp.Control.RegionType.矩形2,
                                      GVS.HalconDisp.Control.RegionType.圆,
                                      GVS.HalconDisp.Control.RegionType.椭圆});
            if (null != m_method.Para.CreatePara.CreateRoiPara.Reg)
            {
                m_roiCreateType.RegPara = m_method.Para.CreatePara.CreateRoiPara;
            }
            else
            {
                m_method.Para.CreatePara.CreateRoiPara = new GVS.HalconDisp.Control.RegionTypeParas();
                m_roiCreateType.RegPara = m_method.Para.CreatePara.CreateRoiPara;
                m_roiCreateType.RegPara.RegType = GVS.HalconDisp.Control.RegionType.矩形2;
                m_roiCreateType.RegPara.MyRect2.ArrayRow = new double[] { 130, 130, 329, 329 };
                m_roiCreateType.RegPara.MyRect2.ArrayCol = new double[] { 185, 382, 382, 185 };
            }
            m_roiCreateType.HoImg = m_hoImage;
            m_roiCreateType.DispCtrl = this.m_Disp;
            pnl_CreateRoi.Controls.Add(m_roiCreateType);
            m_roiCreateType.Dock = DockStyle.Fill;
            m_roiCreateType.ListROI = m_listRoi;
            m_roiCreateType.DisplayEditReg = false;

            // 模板区域
            m_method.RegModelReg = m_method.Para.CreatePara.CreateRoiPara.Reg;

            // 搜索区域类型使用
            m_roiFindType.SetRegTypeGroup(new GVS.HalconDisp.Control.RegionType[]
                                         {GVS.HalconDisp.Control.RegionType.矩形1,
                                          GVS.HalconDisp.Control.RegionType.矩形2,
                                          GVS.HalconDisp.Control.RegionType.圆,
                                          GVS.HalconDisp.Control.RegionType.椭圆,
                                          GVS.HalconDisp.Control.RegionType.图片});

            if (null != m_method.Para.FindPara.FindRoiPara.Reg)
            {
                m_roiFindType.RegPara = m_method.Para.FindPara.FindRoiPara;
                //m_roiCreateType.RegPara = m_method.Para.CreatePara.CreateRoiPara;
            }
            else
            {
                m_method.Para.FindPara.FindRoiPara = new GVS.HalconDisp.Control.RegionTypeParas();
                m_roiFindType.RegPara = m_method.Para.FindPara.FindRoiPara;
                m_roiFindType.RegPara.RegType = GVS.HalconDisp.Control.RegionType.图片;
                //m_roiFindType.RegPara.MyRect2.ArrayRow = new double[] { 130, 130, 3290, 3290 };
                //m_roiFindType.RegPara.MyRect2.ArrayCol = new double[] { 185, 3820, 3820, 185 };
            }
            m_roiFindType.HoImg = m_hoImage;
            m_roiFindType.DispCtrl = this.m_Disp;
            pnl_Search.Controls.Add(m_roiFindType);
            m_roiFindType.Dock = DockStyle.Fill;
            m_roiFindType.ListROI = m_listRoi;
            m_roiFindType.DisplayEditReg = true;

            // 将参数类更新至参数界面
            UpdateCreateParaToCtrl();

            // 将参数更新至应用界面
            UpdateFindParaToCtrl();

            // 按钮图像更新
            UpdateCtrl();

            // 更新预处理参数
            // UpdatePretreatmentPara();

            // 鼠标抬起事件
            m_Disp.HWindowCtrl.MouseUp += HWindowCtrl_MouseUp;
            m_bFirst = false;
            lbl_Time.Text = "运行时间：" + m_method.RunTime + "ms";
            tsp_Save.Visible = m_bModual;
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

        #region 更新方法
        /// <summary>
        /// 自动选择、链接等图像更新
        /// </summary>
        private void UpdateCtrl()
        {
            // 标准参数
            if (!m_bStandard)
            {
                pnl_Standard.Visible = true;
                this.btn_standard.Image = global::SvsPatMax.Properties.Resources.向下;
                btn_Gaoji.Location = new System.Drawing.Point(4, 316);
                lbl_GAO.Location = new System.Drawing.Point(29, 316);
                lbl_Gao_.Location = new System.Drawing.Point(108, 316);
                pnl_Gao.Location = new System.Drawing.Point(4, 337);
            }
            else
            {
                pnl_Standard.Visible = false;
                this.btn_standard.Image = global::SvsPatMax.Properties.Resources.向上;
                btn_Gaoji.Location = new System.Drawing.Point(4, 28);
                lbl_GAO.Location = new System.Drawing.Point(29, 28);
                lbl_Gao_.Location = new System.Drawing.Point(108, 28);
                pnl_Gao.Location = new System.Drawing.Point(4, 45);
            }

            // 高级参数
            if (!m_bGaoji)
            {
                pnl_Gao.Visible = true;
                this.btn_Gaoji.Image = global::SvsPatMax.Properties.Resources.向下;
            }
            else
            {
                pnl_Gao.Visible = false;
                this.btn_Gaoji.Image = global::SvsPatMax.Properties.Resources.向上;
            }

            // 自动选择
            if (m_method.Para.CreatePara.OptimizeAuto)
            {
                this.btn_AutoOptimize.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_AutoOptimize.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            if (m_method.Para.CreatePara.ContrastAuto)
            {
                this.btn_AutoContrast.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_AutoContrast.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            if (m_method.Para.CreatePara.MinComponentAuto)
            {
                this.btn_MinSizeCompAuto.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_MinSizeCompAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            if (m_method.Para.CreatePara.NumLevelAto)
            {
                this.btn_NumLevelAuto.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_NumLevelAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            if (m_method.Para.CreatePara.MinContrastAuto)
            {
                this.btn_MinContrast.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_MinContrast.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            if (m_method.Para.CreatePara.ScaleConnect)
            {
                this.btn_Connect.Image = global::SvsPatMax.Properties.Resources.链接;
                trB_ColMinScale.Location = new System.Drawing.Point(169, 184);
                trB_ColMaxScale.Location = new System.Drawing.Point(169, 235);
                this.trB_ColMaxScale.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
                this.trB_ColMinScale.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            }
            else
            {
                this.btn_Connect.Image = global::SvsPatMax.Properties.Resources.断开;
                trB_ColMinScale.Location = new System.Drawing.Point(169, 186);
                trB_ColMaxScale.Location = new System.Drawing.Point(169, 239);
                this.trB_ColMaxScale.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
                this.trB_ColMinScale.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            }
            if (m_method.Para.CreatePara.PhiStepAuto)
            {
                this.btn_PhiStep.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_PhiStep.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            if (m_method.Para.CreatePara.RowScaleStepAuto)
            {
                this.btn_RowStep.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_RowStep.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            if (m_method.Para.CreatePara.ColStepAuto)
            {
                this.btn_ColStepAuto.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_ColStepAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }

            // chk
            chk_ModelROI.Checked = m_method.Para.CreatePara.ModelRoiChecked;
            chk_SearchRoi.Checked = m_method.Para.CreatePara.SearchRoiChecked;
            chk_model.Checked = m_method.Para.CreatePara.ModelContourChecked;
            chk_Find.Checked = m_method.Para.CreatePara.FindContourChecked;
        }

        /// <summary>
        /// 将参数类数据更新至控件
        /// </summary>
        private void UpdateCreateParaToCtrl()
        {
            // 标准
            numUD_Low.Value = (decimal)m_method.Para.CreatePara.LowContrast;
            numUD_High.Value = (decimal)m_method.Para.CreatePara.HighContrast;
            numUD_MinSizeComp.Value = (decimal)m_method.Para.CreatePara.MinSizeComponent;
            numUD_LevelNum.Value = (decimal)m_method.Para.CreatePara.NumLevel;
            HTuple StartPhi = null;
            HOperatorSet.TupleDeg(m_method.Para.CreatePara.StartPhi, out StartPhi);
            numUD_StartPhi.Value = (decimal)StartPhi[0].D;
            HTuple PhiExtend = null;
            HOperatorSet.TupleDeg(m_method.Para.CreatePara.PhiExtend, out PhiExtend);
            numUD_PhiExtend.Value = (decimal)PhiExtend[0].D;
            numUD_RowMinScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMin;
            numUD_RowMaxScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMax;
            numUD_ColMinScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMin;
            numUD_ColMaxScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMax;

            // 高级
            HTuple PhiStep = null;
            HOperatorSet.TupleDeg(m_method.Para.CreatePara.PhiStep, out PhiStep);
            numUD_PhiStep.Value = (decimal)PhiStep[0].D;
            numUD_RowScaleStep.Value = (decimal)m_method.Para.CreatePara.RowScaleStep;
            numUD_ColScaleStep.Value = (decimal)m_method.Para.CreatePara.ColScaleStep;

            // 度量
            switch (m_method.Para.CreatePara.Polarity)
            {
                case "use_polarity":
                    cmb_Polarity.SelectedIndex = 0;
                    break;
                case "ignore_global_polarity":
                    cmb_Polarity.SelectedIndex = 1;
                    break;
                case "ignore_local_polarity":
                    cmb_Polarity.SelectedIndex = 2;
                    break;
                case "ignore_color_polarity":
                    cmb_Polarity.SelectedIndex = 2;
                    break;
            }

            // 最优化
            switch (m_method.Para.CreatePara.Optimize)
            {
                case "none":
                    cmb_Optimize.SelectedIndex = 0;
                    break;
                case "point_reduction_low":
                    cmb_Optimize.SelectedIndex = 1;
                    break;
                case "point_reduction_medium":
                    cmb_Optimize.SelectedIndex = 2;
                    break;
                case "point_reduction_high":
                    cmb_Optimize.SelectedIndex = 2;
                    break;
            }
            chk_Pregenerate.Checked = m_method.Para.CreatePara.Pregenerate;
            numUD_MinContrast.Value = (decimal)m_method.Para.CreatePara.MinContrast;
        }

        /// <summary>
        /// 将参数更新到应用界面
        /// </summary>
        private void UpdateFindParaToCtrl()
        {
            numUD_MinScore.Value = (decimal)m_method.Para.FindPara.MinScore;
            numUD_MatchNum.Value = (decimal)m_method.Para.FindPara.FindMaxNum;
            numUD_Greed.Value = (decimal)m_method.Para.FindPara.Greed;
            numUD_MaxOver.Value = (decimal)m_method.Para.FindPara.MaxOver;
            numUD_Deformation.Value = (decimal)m_method.Para.FindPara.MaxDeformation;
            numUD_NumlevelF.Value = (decimal)m_method.Para.FindPara.FindNumLevel;
            numUD_time.Value = (decimal)m_method.Para.FindPara.Time;
            chk_Time.Checked = m_method.Para.FindPara.TimeCheck;
            switch (m_method.Para.FindPara.SubPix)
            {
                case "none":
                    cmb_SubPiex.SelectedIndex = 0;
                    break;
                case "interpolation":
                    cmb_SubPiex.SelectedIndex = 1;
                    break;
                case "least_squares":
                    cmb_SubPiex.SelectedIndex = 2;
                    break;
                case "least_squares_high":
                    cmb_SubPiex.SelectedIndex = 3;
                    break;
                case "least_squares_very_high":
                    cmb_SubPiex.SelectedIndex = 4;
                    break;
            }
        }

        /// <summary>
        /// 更新区域
        /// </summary>
        private void UpdateResultGraphic()
        {
            m_Disp.HobjectToHimage(m_hoImage);
            if (!m_bFirst)
            {
                if (m_bTrain)
                {
                    if (m_method.Para.CreatePara.ModelContourChecked)
                    {
                        cmb_Disp.SelectedIndex = 1;
                        m_Disp.DispObj(m_method.CreateContourModel, "green");
                    }
                    if (m_method.Para.CreatePara.ModelRoiChecked)
                    {
                        cmb_Disp.SelectedIndex = 1;
                        m_Disp.DispObj(m_method.Para.CreatePara.CreateRoiPara.Reg, "red");
                    }
                }
                else
                {
                    if (m_method.Para.CreatePara.SearchRoiChecked)
                    {
                        cmb_Disp.SelectedIndex = 2;
                        m_Disp.DispObj(m_method.Para.FindPara.FindRoiPara.Reg, "cyan");
                    }
                    if (m_method.Para.CreatePara.FindContourChecked)
                    {
                        cmb_Disp.SelectedIndex = 2;
                        foreach (HObjectWithColor item in m_method.ListReg)
                        {
                            m_Disp.DispObj(item.HObject, "cyan");
                        }

                    }
                    if (m_method.Para.PretreatementPara.PretreatmentCheck)
                    {
                        if (m_method.Para.PretreatementPara.ThresholdChecked)
                        {
                            cmb_Disp.SelectedIndex = 2;
                            m_Disp.DispObj(m_method.m_hoThresholdReg, "green");
                        }
                        if (m_method.Para.PretreatementPara.OpeningChecked)
                        {
                            cmb_Disp.SelectedIndex = 2;
                            m_Disp.DispObj(m_method.m_hoThresholdRegOpening, "green");
                        }
                        if (m_method.Para.PretreatementPara.ClosingChecked)
                        {
                            cmb_Disp.SelectedIndex = 2;
                            m_Disp.DispObj(m_method.m_hoThresholdRegClosing, "green");
                        }
                        if (m_method.Para.PretreatementPara.ConnectChecked)
                        {
                            cmb_Disp.SelectedIndex = 2;
                            m_Disp.DispObj(m_method.m_hoRegConnection, "green");
                        }
                        if (m_method.Para.PretreatementPara.AreaChecked)
                        {
                            cmb_Disp.SelectedIndex = 2;
                            m_Disp.DispObj(m_method.m_hoAeaSelectShape, "green");
                        }
                        if (m_method.Para.PretreatementPara.PaintChecked)
                        {
                            cmb_Disp.SelectedIndex = 2;
                            m_Disp.DispObj(m_method.m_hoPaint, "green");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将匹配结果更新到表格中
        /// </summary>
        private void UpdateResultPara()
        {
            if (m_method.Row != -1)
            {
                if (m_bFindState)
                {
                    dgv_Result.Rows[0].Cells[0].Value = 1;
                    dgv_Result.Rows[0].Cells[1].Value = m_method.Col.ToString("F3");
                    dgv_Result.Rows[0].Cells[2].Value = m_method.Row.ToString("F3");
                    dgv_Result.Rows[0].Cells[3].Value = m_method.Angle.ToString("F3");
                    dgv_Result.Rows[0].Cells[4].Value = m_method.ScaleRow.ToString("F3");
                    dgv_Result.Rows[0].Cells[5].Value = m_method.ScaleCol.ToString("F3");
                    dgv_Result.Rows[0].Cells[6].Value = m_method.Score.ToString("F3");
                    if (-100 != m_method.Row)
                    {
                        HObject hoCross = null;
                        HOperatorSet.GenCrossContourXld(out hoCross, m_method.Row, m_method.Col, 10, 10);
                        m_Disp.DispObj(hoCross, "red");
                    }
                }
                else
                {
                    dgv_Result.Rows.Clear();
                }
            }
        }
        #endregion

        #region 鼠标事件
        private void HWindowCtrl_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                txt_Create.Text = "";
                txt_Find.Text = string.Empty;
                m_method.RegModelReg = null;
                m_method.Para.CreatePara.CreateRoiPara = m_roiCreateType.RegPara;
                m_method.RegModelReg = m_method.Para.CreatePara.CreateRoiPara.Reg;
                if (null != m_method.Para.CreatePara.CreateRoiPara.Reg)
                {
                    if (null != m_hoImage)
                    {
                        HOperatorSet.ReduceDomain(m_hoImage, m_method.RegModelReg,
                                                   out m_hoReducedImage);
                        HOperatorSet.WriteImage(m_hoReducedImage, "bmp", 0, Application.StartupPath + "\\" + m_method.PatMaxName);
                        HOperatorSet.ReadImage(out m_hoReducedImage, Application.StartupPath + "\\" + m_method.PatMaxName + ".bmp");
                        m_dispSmall.HobjectToHimage(m_hoReducedImage);
                    }
                }
            }
            catch (Exception)
            {

                return;
            }
        }
        #endregion    

        #region 参数控件
        // 显示窗口
        private void cmb_Disp_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Disp.HobjectToHimage(m_hoImage);
            switch (cmb_Disp.SelectedIndex)
            {
                // 输入，显示搜索区域
                case 0:
                    m_roiFindType.DisplayEditReg = true;
                    m_roiCreateType.DisplayEditReg = false;
                    m_roiFindType.GenReg();
                    break;

                // 训练，显示模板区域
                case 1:
                    m_roiCreateType.DisplayEditReg = true;
                    m_roiFindType.DisplayEditReg = false;
                    m_roiCreateType.GenReg();
                    break;

                // 结果，显示结果信息
                case 2:
                    m_roiFindType.DisplayEditReg = false;
                    m_roiCreateType.DisplayEditReg = false;
                    break;
            }
        }

        // 标准参数
        private void btn_standard_Click(object sender, EventArgs e)
        {
            m_bStandard = !m_bStandard;
            if (!m_bStandard)
            {
                pnl_Standard.Visible = true;
                this.btn_standard.Image = global::SvsPatMax.Properties.Resources.向下;
                this.btn_Gaoji.Location = new System.Drawing.Point(4, 310);
                this.lbl_GAO.Location = new System.Drawing.Point(29, 312);
                this.lbl_Gao_.Location = new System.Drawing.Point(108, 312);
                this.pnl_Gao.Location = new System.Drawing.Point(4, 332);
            }
            else
            {
                pnl_Standard.Visible = false;
                this.btn_standard.Image = global::SvsPatMax.Properties.Resources.向上;
                this.btn_Gaoji.Location = new System.Drawing.Point(3, 30);
                this.lbl_GAO.Location = new System.Drawing.Point(28, 30);
                this.lbl_Gao_.Location = new System.Drawing.Point(107, 30);
                this.pnl_Gao.Location = new System.Drawing.Point(3, 50);
            }
        }

        // 高级参数
        private void btn_Gaoji_Click(object sender, EventArgs e)
        {
            m_bGaoji = !m_bGaoji;
            if (!m_bGaoji)
            {
                pnl_Gao.Visible = true;
                this.btn_Gaoji.Image = global::SvsPatMax.Properties.Resources.向下;
            }
            else
            {
                pnl_Gao.Visible = false;
                this.btn_Gaoji.Image = global::SvsPatMax.Properties.Resources.向上;
            }
        }

        // 对比度
        private void btn_AutoContrast_Click(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.ContrastAuto = !m_method.Para.CreatePara.ContrastAuto;
            if (m_method.Para.CreatePara.ContrastAuto)
            {
                this.btn_AutoContrast.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_AutoContrast.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
        }

        // 最小尺寸组件
        private void btn_MinSizeCompAuto_Click(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.MinComponentAuto = !m_method.Para.CreatePara.MinComponentAuto;
            if (m_method.Para.CreatePara.MinComponentAuto)
            {
                this.btn_MinSizeCompAuto.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_MinSizeCompAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
        }

        // 金字塔
        private void btn_NumLevelAuto_Click(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.NumLevelAto = !m_method.Para.CreatePara.MinComponentAuto;
            if (m_method.Para.CreatePara.NumLevelAto)
            {
                this.btn_NumLevelAuto.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_NumLevelAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
        }

        // 链接
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.ScaleConnect = !m_method.Para.CreatePara.ScaleConnect;
            if (m_method.Para.CreatePara.ScaleConnect)
            {
                this.btn_Connect.Image = global::SvsPatMax.Properties.Resources.链接;
                trB_ColMinScale.Location = new System.Drawing.Point(169, 184);
                trB_ColMaxScale.Location = new System.Drawing.Point(169, 235);
                this.trB_ColMaxScale.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
                this.trB_ColMinScale.TickStyle = System.Windows.Forms.TickStyle.BottomRight;

                // row、colMin
                trB_ColMinScale.Value = trB_RowMinScale.Value;
                m_method.Para.CreatePara.RowScaleMin = (trB_RowMinScale.Value) * 0.01;
                numUD_RowMinScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMin;
                m_method.Para.CreatePara.ColScaleMin = (trB_RowMinScale.Value) * 0.01;
                numUD_ColMinScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMin;

                // row、colMax
                trB_ColMaxScale.Value = trB_RowMaxScale.Value;
                m_method.Para.CreatePara.RowScaleMax = (trB_RowMaxScale.Value) * 0.01;
                numUD_RowMaxScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMax;
                m_method.Para.CreatePara.ColScaleMax = (trB_RowMaxScale.Value) * 0.01;
                numUD_ColMaxScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMax;
            }
            else
            {
                this.btn_Connect.Image = global::SvsPatMax.Properties.Resources.断开;
                trB_ColMinScale.Location = new System.Drawing.Point(169, 186);
                trB_ColMaxScale.Location = new System.Drawing.Point(169, 239);
                this.trB_ColMaxScale.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
                this.trB_ColMinScale.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            }
        }

        // 角度步长
        private void btn_PhiStep_Click(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.PhiStepAuto = !m_method.Para.CreatePara.PhiStepAuto;
            if (m_method.Para.CreatePara.PhiStepAuto)
            {
                this.btn_PhiStep.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_PhiStep.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
        }

        // 行方向步长
        private void btn_RowStep_Click(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.RowScaleStepAuto = !m_method.Para.CreatePara.RowScaleStepAuto;
            if (m_method.Para.CreatePara.RowScaleStepAuto)
            {
                this.btn_RowStep.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_RowStep.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
        }

        // 列方向步长
        private void btn_ColStepAuto_Click(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.ColStepAuto = !m_method.Para.CreatePara.ColStepAuto;
            if (m_method.Para.CreatePara.ColStepAuto)
            {
                this.btn_ColStepAuto.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_ColStepAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
        }

        // 最优化
        private void btn_AutoOptimize_Click(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.OptimizeAuto = !m_method.Para.CreatePara.OptimizeAuto;
            if (m_method.Para.CreatePara.OptimizeAuto)
            {
                this.btn_AutoOptimize.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_AutoOptimize.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
        }

        // columMax
        private void trB_ColMaxScale_Scroll(object sender, EventArgs e)
        {
            if (!m_method.Para.CreatePara.ScaleConnect)
            {
                m_method.Para.CreatePara.ColScaleMax = (trB_ColMaxScale.Value) * 0.01;
                numUD_ColMaxScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMax;
            }
            else
            {
                m_method.Para.CreatePara.RowScaleMax = (trB_ColMaxScale.Value) * 0.01;
                numUD_RowMaxScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMax;
                m_method.Para.CreatePara.ColScaleMax = (trB_ColMaxScale.Value) * 0.01;
                numUD_ColMaxScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMax;
                if (m_method.Para.CreatePara.ColScaleMax < m_method.Para.CreatePara.ColScaleMin)
                {
                    trB_ColMinScale.Value = (int)(m_method.Para.CreatePara.ColScaleMax * 100);
                    numUD_ColMinScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMax;
                    trB_RowMinScale.Value = (int)(m_method.Para.CreatePara.ColScaleMax * 100);
                    numUD_RowMinScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMax;
                }
            }
        }

        // trb_ColumMax
        private void numUD_ColMaxScale_ValueChanged(object sender, EventArgs e)
        {
            if (!m_method.Para.CreatePara.ScaleConnect)
            {
                m_method.Para.CreatePara.ColScaleMax = (double)numUD_ColMaxScale.Value;
                trB_ColMaxScale.Value = (int)(m_method.Para.CreatePara.ColScaleMax * 100);
                if (m_method.Para.CreatePara.ColScaleMin > m_method.Para.CreatePara.ColScaleMax)
                {
                    numUD_ColMinScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMax;
                }
            }
            else
            {
                m_method.Para.CreatePara.RowScaleMax = (double)numUD_ColMaxScale.Value;
                trB_RowMaxScale.Value = (int)(m_method.Para.CreatePara.RowScaleMax * 100);
                numUD_RowMaxScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMax;
                m_method.Para.CreatePara.ColScaleMax = (double)numUD_ColMaxScale.Value;
                trB_ColMaxScale.Value = (int)(m_method.Para.CreatePara.ColScaleMax * 100);
                if (m_method.Para.CreatePara.ColScaleMax < m_method.Para.CreatePara.ColScaleMin)
                {
                    trB_ColMinScale.Value = (int)(m_method.Para.CreatePara.ColScaleMax * 100);
                    numUD_ColMinScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMax;
                    trB_RowMinScale.Value = (int)(m_method.Para.CreatePara.ColScaleMax * 100);
                    numUD_RowMinScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMax;
                    trB_RowMaxScale.Value = (int)(m_method.Para.CreatePara.ColScaleMax * 100);
                    numUD_RowMaxScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMax;
                }
            }
        }

        // RowMax
        private void numUD_RowMaxScale_ValueChanged(object sender, EventArgs e)
        {
            if (!m_method.Para.CreatePara.ScaleConnect)
            {
                m_method.Para.CreatePara.RowScaleMax = (double)numUD_RowMaxScale.Value;
                trB_RowMaxScale.Value = (int)(m_method.Para.CreatePara.RowScaleMax * 100);
                if (m_method.Para.CreatePara.RowScaleMin > m_method.Para.CreatePara.RowScaleMax)
                {
                    numUD_RowMinScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMax;
                }
            }
            else
            {
                m_method.Para.CreatePara.RowScaleMax = (double)numUD_RowMaxScale.Value;
                trB_RowMaxScale.Value = (int)(m_method.Para.CreatePara.RowScaleMax * 100);
                m_method.Para.CreatePara.ColScaleMax = (double)numUD_RowMaxScale.Value;
                trB_ColMaxScale.Value = (int)(m_method.Para.CreatePara.ColScaleMax * 100);
                numUD_ColMaxScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMax;
            }
        }

        // trB_RowMax
        private void trB_RowMaxScale_Scroll(object sender, EventArgs e)
        {
            if (!m_method.Para.CreatePara.ScaleConnect)
            {
                m_method.Para.CreatePara.RowScaleMax = (trB_RowMaxScale.Value) * 0.01;
                numUD_RowMaxScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMax;
            }
            else
            {
                m_method.Para.CreatePara.RowScaleMax = (trB_RowMaxScale.Value) * 0.01;
                numUD_RowMaxScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMax;
                m_method.Para.CreatePara.ColScaleMax = (trB_RowMaxScale.Value) * 0.01;
                numUD_ColMaxScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMax;
            }

        }

        // RowMin
        private void numUD_RowMinScale_ValueChanged(object sender, EventArgs e)
        {
            if (!m_method.Para.CreatePara.ScaleConnect)
            {
                m_method.Para.CreatePara.RowScaleMin = (double)numUD_RowMinScale.Value;
                trB_RowMinScale.Value = (int)(m_method.Para.CreatePara.RowScaleMin * 100);
                if (m_method.Para.CreatePara.RowScaleMin > m_method.Para.CreatePara.RowScaleMax)
                {
                    numUD_RowMaxScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMin;
                }
            }
            else
            {
                m_method.Para.CreatePara.RowScaleMin = (double)numUD_RowMinScale.Value;
                trB_RowMinScale.Value = (int)(m_method.Para.CreatePara.RowScaleMin * 100);
                m_method.Para.CreatePara.ColScaleMin = (double)numUD_RowMinScale.Value;
                trB_ColMinScale.Value = (int)(m_method.Para.CreatePara.ColScaleMin * 100);
                numUD_ColMinScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMin;
            }
        }

        // RowMin
        private void trB_RowMinScale_Scroll(object sender, EventArgs e)
        {
            if (!m_method.Para.CreatePara.ScaleConnect)
            {
                m_method.Para.CreatePara.RowScaleMin = (trB_RowMinScale.Value) * 0.01;
                numUD_RowMinScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMin;
            }
            else
            {
                m_method.Para.CreatePara.RowScaleMin = (trB_RowMinScale.Value) * 0.01;
                numUD_RowMinScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMin;
                m_method.Para.CreatePara.ColScaleMin = (trB_RowMinScale.Value) * 0.01;
                numUD_ColMinScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMin;
            }
        }

        // ColumMin
        private void numUD_ColMinScale_ValueChanged(object sender, EventArgs e)
        {
            if (!m_method.Para.CreatePara.ScaleConnect)
            {
                m_method.Para.CreatePara.ColScaleMin = (double)numUD_ColMinScale.Value;
                trB_ColMinScale.Value = (int)(m_method.Para.CreatePara.ColScaleMin * 100);
                if (m_method.Para.CreatePara.ColScaleMin > m_method.Para.CreatePara.ColScaleMax)
                {
                    numUD_ColMaxScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMin;
                }
            }
            else
            {
                m_method.Para.CreatePara.RowScaleMin = (double)numUD_ColMinScale.Value;
                trB_RowMinScale.Value = (int)(m_method.Para.CreatePara.RowScaleMin * 100);
                m_method.Para.CreatePara.ColScaleMin = (double)numUD_ColMinScale.Value;
                trB_ColMinScale.Value = (int)(m_method.Para.CreatePara.ColScaleMin * 100);
                numUD_RowMinScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMin;
                if (m_method.Para.CreatePara.ColScaleMax < m_method.Para.CreatePara.ColScaleMin)
                {
                    trB_ColMaxScale.Value = (int)(m_method.Para.CreatePara.ColScaleMin * 100);
                    numUD_ColMaxScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMin;
                    trB_RowMaxScale.Value = (int)(m_method.Para.CreatePara.ColScaleMin * 100);
                    numUD_RowMaxScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMin;
                    trB_RowMinScale.Value = (int)(m_method.Para.CreatePara.ColScaleMin * 100);
                    numUD_RowMinScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMin;
                }
            }
        }

        // columMin
        private void trB_ColMinScale_Scroll(object sender, EventArgs e)
        {
            if (!m_method.Para.CreatePara.ScaleConnect)
            {
                m_method.Para.CreatePara.ColScaleMin = (trB_ColMinScale.Value) * 0.01;
                numUD_ColMinScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMin;
            }
            else
            {
                m_method.Para.CreatePara.RowScaleMin = (trB_ColMinScale.Value) * 0.01;
                numUD_RowMinScale.Value = (decimal)m_method.Para.CreatePara.RowScaleMin;
                m_method.Para.CreatePara.ColScaleMin = (trB_ColMinScale.Value) * 0.01;
                numUD_ColMinScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMin;
                if (m_method.Para.CreatePara.ColScaleMax < m_method.Para.CreatePara.ColScaleMin)
                {
                    trB_ColMaxScale.Value = (int)(m_method.Para.CreatePara.ColScaleMin * 100);
                    numUD_ColMaxScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMin;
                    trB_RowMaxScale.Value = (int)(m_method.Para.CreatePara.ColScaleMin * 100);
                    numUD_RowMaxScale.Value = (decimal)m_method.Para.CreatePara.ColScaleMin;
                }
            }
        }

        // 对比度低
        private void numUD_Low_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.ContrastAuto = false;
                this.btn_AutoContrast.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.LowContrast = (double)numUD_Low.Value;
            trb_Low.Value = (int)m_method.Para.CreatePara.LowContrast;
            if (m_method.Para.CreatePara.LowContrast > m_method.Para.CreatePara.HighContrast)
            {
                numUD_High.Value = (decimal)m_method.Para.CreatePara.LowContrast;
                trB_High.Value = (int)m_method.Para.CreatePara.LowContrast;
            }
        }

        // 对比度低
        private void trb_Low_Scroll(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.ContrastAuto = false;
                this.btn_AutoContrast.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.LowContrast = (double)trb_Low.Value;
            numUD_Low.Value = (decimal)m_method.Para.CreatePara.LowContrast;
        }

        // 对比度高
        private void numUD_High_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.ContrastAuto = false;
                this.btn_AutoContrast.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.HighContrast = (double)numUD_High.Value;
            trB_High.Value = (int)m_method.Para.CreatePara.HighContrast;
            if (m_method.Para.CreatePara.LowContrast > m_method.Para.CreatePara.HighContrast)
            {
                numUD_Low.Value = (decimal)m_method.Para.CreatePara.HighContrast;
                trb_Low.Value = (int)m_method.Para.CreatePara.HighContrast;
            }
        }

        // 对比度高
        private void trB_High_Scroll(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.ContrastAuto = false;
                this.btn_AutoContrast.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.HighContrast = trB_High.Value;
            numUD_High.Value = (int)m_method.Para.CreatePara.HighContrast;
        }

        // 最小组件尺寸
        private void numUD_MinSizeComp_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.MinComponentAuto = false;
                btn_MinSizeCompAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.MinSizeComponent = (double)numUD_MinSizeComp.Value;
            trB_MinSizeComp.Value = (int)m_method.Para.CreatePara.MinSizeComponent;
        }

        // 最小组件尺寸
        private void trB_MinSizeComp_Scroll(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.MinComponentAuto = false;
                btn_MinSizeCompAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.MinSizeComponent = trB_MinSizeComp.Value;
            numUD_MinSizeComp.Value = (decimal)m_method.Para.CreatePara.MinSizeComponent;
        }

        // 金字塔层数
        private void numUD_LevelNum_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.NumLevelAto = false;
                btn_NumLevelAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.NumLevel = (double)numUD_LevelNum.Value;
            trB_NUmLevel.Value = (int)m_method.Para.CreatePara.NumLevel;
        }

        // 金字塔层数
        private void trB_NUmLevel_Scroll(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.NumLevelAto = false;
                btn_NumLevelAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.NumLevel = trB_NUmLevel.Value;
            numUD_LevelNum.Value = (decimal)m_method.Para.CreatePara.NumLevel;
        }

        // 最小对比度
        private void numUD_MinContrast_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.MinContrast = (double)numUD_MinContrast.Value;
            trB_MinContrast.Value = (int)m_method.Para.CreatePara.MinContrast;
        }

        // 最小对比度
        private void trB_MinContrast_Scroll(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.MinContrastAuto = false;
                btn_MinContrast.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.MinContrast = (double)trB_MinContrast.Value;
            numUD_MinContrast.Value = (decimal)m_method.Para.CreatePara.MinContrast;
        }

        // 最小对比度
        private void btn_MinContrast_Click(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.MinContrastAuto = !m_method.Para.CreatePara.MinContrastAuto;
            if (m_method.Para.CreatePara.MinContrastAuto)
            {
                this.btn_MinContrast.Image = global::SvsPatMax.Properties.Resources.自动选择;
            }
            else
            {
                this.btn_MinContrast.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
        }

        // 起始角度
        private void numUD_StartPhi_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.MinContrastAuto = false;
                btn_MinContrast.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            HTuple StartPhi = null;
            HOperatorSet.TupleRad((double)numUD_StartPhi.Value, out StartPhi);
            m_method.Para.CreatePara.StartPhi = StartPhi;
            trB_StartPhi.Value = (int)(numUD_StartPhi.Value * 100);
        }

        // 起始角度
        private void trB_StartPhi_Scroll(object sender, EventArgs e)
        {
            HTuple StartPhi = null;
            HOperatorSet.TupleRad((double)(trB_StartPhi.Value * 0.01), out StartPhi);
            m_method.Para.CreatePara.StartPhi = StartPhi[0].D;
            numUD_StartPhi.Value = (decimal)(trB_StartPhi.Value * 0.01);
        }

        // 角度范围
        private void numUD_PhiExtend_ValueChanged(object sender, EventArgs e)
        {
            HTuple Phiextend = null;
            HOperatorSet.TupleRad((double)numUD_PhiExtend.Value, out Phiextend);
            m_method.Para.CreatePara.PhiExtend = Phiextend[0].D;
            trB_PhiExtend.Value = (int)(numUD_PhiExtend.Value * 100);
        }

        // 角度范围
        private void trB_PhiExtend_Scroll(object sender, EventArgs e)
        {
            HTuple Phiextend = null;
            HOperatorSet.TupleRad((double)(trB_PhiExtend.Value * 0.01), out Phiextend);
            m_method.Para.CreatePara.PhiExtend = Phiextend[0].D;
            numUD_PhiExtend.Value = (decimal)(trB_PhiExtend.Value * 0.01);
        }

        // 角度步长
        private void numUD_PhiStep_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.PhiStepAuto = false;
                btn_PhiStep.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            HTuple PhiStep = null;
            HOperatorSet.TupleRad((double)numUD_PhiStep.Value, out PhiStep);
            m_method.Para.CreatePara.PhiStep = PhiStep;
            trB_PhiStep.Value = (int)(numUD_PhiStep.Value * 10000);
        }

        // 角度步长
        private void trB_PhiStep_Scroll(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.PhiStepAuto = false;
                btn_PhiStep.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            HTuple PhiStep = null;
            HOperatorSet.TupleRad((double)trB_PhiStep.Value * 0.0001, out PhiStep);
            m_method.Para.CreatePara.PhiStep = PhiStep[0].D;
            numUD_PhiStep.Value = (decimal)((double)trB_PhiStep.Value * 0.0001);
        }

        // 行缩放步长
        private void numUD_RowScaleStep_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.RowScaleStepAuto = false;
                btn_RowStep.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.RowScaleStep = (double)numUD_RowScaleStep.Value;
            trB_RowScaleStep.Value = (int)(m_method.Para.CreatePara.RowScaleStep * 10000);
        }

        // 行缩放步长
        private void trB_RowScaleStep_Scroll(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.RowScaleStepAuto = false;
                btn_RowStep.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.RowScaleStep = (double)trB_RowScaleStep.Value * 0.0001;
            numUD_RowScaleStep.Value = (decimal)m_method.Para.CreatePara.RowScaleStep;
        }

        // 列缩放步长
        private void numUD_ColScaleStep_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.ColStepAuto = false;
                btn_ColStepAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.ColScaleStep = (double)numUD_ColScaleStep.Value;
            trB_ColScaleStep.Value = (int)(m_method.Para.CreatePara.ColScaleStep * 10000);
        }

        // 列缩放步长
        private void trB_ColScaleStep_Scroll(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.ColStepAuto = false;
                btn_ColStepAuto.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.ColScaleStep = (double)trB_ColScaleStep.Value * 0.0001;
            numUD_ColScaleStep.Value = (decimal)m_method.Para.CreatePara.ColScaleStep;
        }

        // 极性
        private void cmb_Polarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.Polarity = cmb_Polarity.Text.ToString();
        }

        // 最优化
        private void cmb_Optimize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_bFirst)
            {
                m_method.Para.CreatePara.OptimizeAuto = false;
                this.btn_AutoOptimize.Image = global::SvsPatMax.Properties.Resources.不自动选择;
            }
            m_method.Para.CreatePara.Optimize = cmb_Optimize.Text.ToString();
        }

        // 预生成
        private void chk_Pregenerate_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.Pregenerate = chk_Pregenerate.Checked;
        }
        #endregion

        #region 应用参数界面

        // 最小分数
        private void numUD_MinScore_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.FindPara.MinScore = (double)numUD_MinScore.Value;
            trB_MinScore.Value = (int)(m_method.Para.FindPara.MinScore * 100);
        }

        // 最小分数
        private void trB_MinScore_Scroll(object sender, EventArgs e)
        {
            m_method.Para.FindPara.MinScore = trB_MinScore.Value * 0.01;
            numUD_MinScore.Value = (decimal)m_method.Para.FindPara.MinScore;
        }

        // 最大匹配个数
        private void numUD_MatchNum_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.FindPara.FindMaxNum = (double)numUD_MatchNum.Value;
            trB_MatchNum.Value = (int)m_method.Para.FindPara.FindMaxNum;
        }

        // 最大匹配个数
        private void trB_MatchNum_Scroll(object sender, EventArgs e)
        {
            m_method.Para.FindPara.FindMaxNum = trB_MatchNum.Value;
            numUD_MatchNum.Value = (decimal)m_method.Para.FindPara.FindMaxNum;
        }

        // 贪心算法
        private void numUD_Greed_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.FindPara.Greed = (double)numUD_Greed.Value;
            trB_Greed.Value = (int)(m_method.Para.FindPara.Greed * 100);
        }

        // 贪心算法
        private void trB_Greed_Scroll(object sender, EventArgs e)
        {
            m_method.Para.FindPara.Greed = trB_Greed.Value * 0.01;
            numUD_Greed.Value = (decimal)m_method.Para.FindPara.Greed;
        }

        // 最大重叠
        private void numUD_MaxOver_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.FindPara.MaxOver = (double)numUD_MaxOver.Value;
            trB_MaxOver.Value = (int)(m_method.Para.FindPara.MaxOver * 100);
        }

        // 最大重叠
        private void trB_MaxOver_Scroll(object sender, EventArgs e)
        {
            m_method.Para.FindPara.MaxOver = trB_MaxOver.Value * 0.01;
            numUD_MaxOver.Value = (decimal)m_method.Para.FindPara.MaxOver;
        }

        // 亚像素
        private void cmb_SubPiex_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_method.Para.FindPara.SubPix = cmb_SubPiex.Text.ToString();
        }

        // 最大变形
        private void numUD_Deformation_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.FindPara.MaxDeformation = (int)numUD_Deformation.Value;
            trB_Deformation.Value = (int)(m_method.Para.FindPara.MaxDeformation);
        }

        // 最大变形
        private void trB_Deformation_Scroll(object sender, EventArgs e)
        {
            m_method.Para.FindPara.MaxDeformation = trB_Deformation.Value;
            numUD_Deformation.Value = m_method.Para.FindPara.MaxDeformation;
        }

        // 匹配金字塔层数
        private void numUD_NumlevelF_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.FindPara.FindNumLevel = (int)numUD_NumlevelF.Value;
            trB_NumlevelF.Value = (int)(m_method.Para.FindPara.FindNumLevel);
        }

        // 匹配金字塔层数
        private void trB_NumlevelF_Scroll(object sender, EventArgs e)
        {
            m_method.Para.FindPara.FindNumLevel = trB_NumlevelF.Value;
            numUD_NumlevelF.Value = (decimal)m_method.Para.FindPara.FindNumLevel;
        }

        // 超时
        private void numUD_time_ValueChanged(object sender, EventArgs e)
        {
            m_method.Para.FindPara.Time = (int)numUD_time.Value;
            trB_Time.Value = (int)(m_method.Para.FindPara.Time);
        }

        // 超时
        private void trB_Time_Scroll(object sender, EventArgs e)
        {
            m_method.Para.FindPara.Time = trB_Time.Value;
            numUD_time.Value = (decimal)m_method.Para.FindPara.Time;
        }

        // 超时启用
        private void chk_Time_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.FindPara.TimeCheck = chk_Time.Checked;
        }

        // 用户标准参数
        private void btn_UserPara_Click(object sender, EventArgs e)
        {
            m_bStandardUser = !m_bStandardUser;
            if (!m_bStandardUser)
            {
                pnl__UserPara.Visible = true;
                this.btn_UserPara.Image = global::SvsPatMax.Properties.Resources.向下;
                this.btn__UserParaGao.Location = new System.Drawing.Point(3, 94);
                this.lbl_UserParaGao.Location = new System.Drawing.Point(28, 94);
                this.lbl_UserParaGao_.Location = new System.Drawing.Point(107, 94);
                this.pnl_UserParaGao.Location = new System.Drawing.Point(3, 112);
            }
            else
            {
                pnl__UserPara.Visible = false;
                this.btn_UserPara.Image = global::SvsPatMax.Properties.Resources.向上;
                this.btn__UserParaGao.Location = new System.Drawing.Point(3, 30);
                this.lbl_UserParaGao.Location = new System.Drawing.Point(28, 30);
                this.lbl_UserParaGao_.Location = new System.Drawing.Point(107, 30);
                this.pnl_UserParaGao.Location = new System.Drawing.Point(3, 50);
            }
        }

        // 用户高级
        private void btn__UserParaGao_Click(object sender, EventArgs e)
        {
            m_bGaojiUser = !m_bGaojiUser;
            if (m_bGaojiUser)
            {
                pnl_UserParaGao.Visible = true;
                this.btn__UserParaGao.Image = global::SvsPatMax.Properties.Resources.向下;
            }
            else
            {
                pnl_UserParaGao.Visible = false;
                this.btn__UserParaGao.Image = global::SvsPatMax.Properties.Resources.向上;
            }
        }
        #endregion

        // 抓取
        private void btn_Get_Click(object sender, EventArgs e)
        {
            txt_Create.Text = "";
            cmb_Disp.SelectedIndex = 1;
            cmb_Disp_SelectedIndexChanged(null, null);
            m_method.RegModelReg = null;
            m_method.ModelID = null;
            m_method.Para.CreatePara.CreateRoiPara = m_roiCreateType.RegPara;
            m_method.RegModelReg = m_method.Para.CreatePara.CreateRoiPara.Reg;
            if (null != m_method.Para.CreatePara.CreateRoiPara.Reg)
            {
                if (null != m_hoImage)
                {
                    HOperatorSet.ReduceDomain(m_hoImage, m_method.RegModelReg,
                                               out m_hoReducedImage);
                    HOperatorSet.WriteImage(m_hoReducedImage, "bmp", 0, Application.StartupPath + "\\" + m_method.PatMaxName);
                    HOperatorSet.ReadImage(out m_hoReducedImage, Application.StartupPath + "\\" + m_method.PatMaxName + ".bmp");
                    m_dispSmall.HobjectToHimage(m_hoReducedImage);
                }
            }
        }

        // 训练
        private void btn_Train_Click(object sender, EventArgs e)
        {
            m_bTrain = true;
            m_method.Para.CreatePara.CreateRoiPara = m_roiCreateType.RegPara;
            m_method.RegModelReg = m_method.Para.CreatePara.CreateRoiPara.Reg;
            //m_method.MaskReg = m_MaskMethod.MaskReg;
            cmb_Disp.SelectedIndex = 1;
            txt_Create.Text = "";
            m_method.CreateModel(out m_bCreateState, out m_strCreateMessage);
            if (m_method.HvAffinetrans != null)
            {

            }
            txt_Create.Text = m_strCreateMessage.ToString();
            UpdateResultGraphic();
            bool bState = m_method.Save(m_method.Para, Application.StartupPath + "\\" + m_method.PatMaxName);
            if (!bState)
            {
                MessageBox.Show("参数保存失败！");
            }
            m_bTrain = false;
        }

        // 运行
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            HTuple hvSecond1 = null, hvSecond2 = null;
            HOperatorSet.CountSeconds(out hvSecond1);
            bool bState = m_method.Run();
            txt_Find.Text = m_method.RunMsg;
            if (bState)
            {
                UpdateResultGraphic();
                UpdateResultPara();
            }
            else
            {
                MessageBox.Show(m_method.RunMsg);
            }
            HOperatorSet.CountSeconds(out hvSecond2);
            m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
            lbl_Time.Text = "运行时间：" + m_dTime + "ms";
        }

        // 模板区域
        private void chk_ModelROI_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.ModelRoiChecked = chk_ModelROI.Checked;
            UpdateResultGraphic();
        }

        // 搜索轮廓
        private void chk_SearchRoi_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.SearchRoiChecked = chk_SearchRoi.Checked;
            UpdateResultGraphic();
        }

        // 模板
        private void chk_model_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.ModelContourChecked = chk_model.Checked;
            UpdateResultGraphic();
        }

        // 创建类型
        private void cmb_CreateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_CreateName.SelectedIndex)
            {
                case 0:
                    m_method.Para.CreatePara.ModelTypes = ModelType.形状;
                    break;
                case 1:
                    m_method.Para.CreatePara.ModelTypes = ModelType.轮廓;
                    break;
            }

        }

        // 匹配轮廓
        private void chk_Find_CheckedChanged(object sender, EventArgs e)
        {
            m_method.Para.CreatePara.FindContourChecked = chk_Find.Checked;
            UpdateResultGraphic();
        }

        private void tsp_Mask_Click(object sender, EventArgs e)
        {
            //m_affineHom = null;
            m_method.Para.CreatePara.CreateRoiPara = m_roiCreateType.RegPara;
            m_method.RegModelReg = m_method.Para.CreatePara.CreateRoiPara.Reg;
            bool bState = m_method.Run();
            SvMask.MaskForm dlg = new SvMask.MaskForm();
            m_MaskMethod.Para = m_method.Para.MaskPara;
            m_MaskMethod.InputImage = m_hoImage;
            m_MaskMethod.ModelReg = m_method.Para.CreatePara.CreateRoiPara.Reg;
            dlg.m_method = this.m_MaskMethod;
            dlg.m_method.InputImage = m_hoImage;
            dlg.m_method.AffineHom = m_method.HvAffinetrans;
            dlg.ShowDialog();
        }

        private void tsp_Save_Click(object sender, EventArgs e)
        {
            if (m_bModual)
            {
                bool bState = m_method.Save(m_method.Para, "D:\\Model.Xml");
                if (!bState)
                {
                    MessageBox.Show("参数保存失败！");
                }
                else
                {
                    MessageBox.Show("参数保存成功！");
                }
            }
        }
    }
}
