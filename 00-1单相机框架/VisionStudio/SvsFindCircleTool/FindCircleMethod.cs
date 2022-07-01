using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GVS.HalconDisp.ViewROI.Config;
using ToolInterFace;
using ParaResultAll;
using System.Globalization;

namespace SvsFindCircleTool
{
    /// <summary>
    /// 主要内容：本类是找圆的方法类
    /// 时    间：2019/9/29
    /// 作    者：王雅鹏
    /// </summary>
    public class FindCircleMethod : ToolInterface
    {
        /// <summary>
        /// 检测结果类
        /// </summary>
        private List<ParasResultAll> m_ListParaResultAll = new List<ParasResultAll>();

        /// <summary>
        /// 检测结果集合
        /// </summary>
        public List<ParasResultAll> ListParaResultAll
        {
            get
            {
                return m_ListParaResultAll;
            }
            set
            {
                m_ListParaResultAll = value;
            }
        }

        /// <summary>
        /// 参数类
        /// </summary>
        public FindCirclePara Para = new FindCirclePara();

        /// <summary>
        /// 模块Form名
        /// </summary>
        public string ModualFormName
        {
            get;
            set;
        }

        private HTuple m_affineHom = null;

        /// <summary>
        /// 仿射矩阵
        /// </summary>
        public HTuple AffineHom
        {
            get
            {
                return this.m_affineHom;
            }
            set
            {
                this.m_affineHom = value;
            }
        }

        private HObject m_hoInputImg = null;

        /// <summary>
        /// 输入图像
        /// </summary>
        public HObject InputImage
        {
            get
            {
                return this.m_hoInputImg;
            }
            set
            {
                this.m_hoInputImg = value;
            }
        }

        private HObject m_hoRegCircle = null;

        /// <summary>
        /// 拟合的圆
        /// </summary>
        public HObject RegCircle
        {
            get
            {
                return this.m_hoRegCircle;
            }
            set
            {
                this.m_hoRegCircle = value;
            }
        }

        private HObject m_hoRegPoint = null;

        /// <summary>
        /// 各个卡尺拟合点
        /// </summary>
        public HObject RegPoint
        {
            get
            {
                return this.m_hoRegPoint;
            }
            set
            {
                this.m_hoRegPoint = value;
            }
        }

        private HObject m_hoRegCircleCenter = null;

        /// <summary>
        /// 拟合圆的中心
        /// </summary>
        public HObject RegCenterCircle
        {
            get
            {
                return this.m_hoRegCircleCenter;
            }
            set
            {
                this.m_hoRegCircleCenter = value;
            }
        }
        private double m_dRowCircle = 0;

        /// <summary>
        /// 圆的中心行坐标
        /// </summary>
        public double RowCircle
        {
            get
            {
                return this.m_dRowCircle;
            }
            set
            {
                m_dRowCircle = value;
            }
        }

        private double m_dColCircle = 0;

        /// <summary>
        /// 圆的中心列坐标
        /// </summary>
        public double ColCircle
        {
            get
            {
                return this.m_dColCircle;
            }
            set
            {
                m_dColCircle = value;
            }
        }

        private double m_dRadiusCircle = 1.0;

        /// <summary>
        /// 圆的半径
        /// </summary>
        public double RadiusCircle
        {
            get
            {
                return this.m_dRadiusCircle;
            }
            set
            {
                m_dRadiusCircle = value;
            }
        }

        private double m_dContrast = 0;

        /// <summary>
        /// 对比度
        /// </summary>
        public double Contrast
        {
            get
            {
                return this.m_dContrast;
            }
            set
            {
                m_dContrast = value;
            }
        }

        private List<double> m_listRows = new List<double>();

        /// <summary>
        /// 卡尺点的行坐标
        /// </summary>
        public List<double> ListRows
        {
            get
            {
                return this.m_listRows;
            }
            set
            {
                this.m_listRows = value;
            }
        }

        private List<double> m_listCols = new List<double>();

        /// <summary>
        /// 卡尺点的列坐标
        /// </summary>
        public List<double> ListCols
        {
            get
            {
                return this.m_listCols;
            }
            set
            {
                this.m_listCols = value;
            }
        }

        private List<double> m_listContrasts = new List<double>();

        /// <summary>
        /// 卡尺点的幅度
        /// </summary>
        public List<double> ListContrasts
        {
            get
            {
                return this.m_listContrasts;
            }
            set
            {
                this.m_listContrasts = value;
            }
        }

        private double m_dTime = 0;

        /// <summary>
        /// 运行时间
        /// </summary>
        public double RunTime
        {
            get
            {
                return m_dTime;
            }
            set
            {
                m_dTime = value;
            }
        }

        private string m_strMsg = "";

        /// <summary>
        /// 运行消息
        /// </summary>
        public string RunMsg
        {
            get
            {
                return m_strMsg;
            }

            set
            {
                m_strMsg = value;
            }
        }

        private List<HObjectWithColor> m_listReg = new List<HObjectWithColor>();

        /// <summary>
        /// 存储指定颜色Hobject的集合
        /// </summary>
        public List<HObjectWithColor> ListReg
        {
            get
            {
                return m_listReg;
            }
            set
            {
                m_listReg = value;
            }
        }

        /// <summary>
        /// 仿射变换后的中心坐标
        /// </summary>
        HTuple m_hvRowCenter = null, m_hvColCenter = null;

        /// <summary>
        /// 运行方法
        /// </summary>
        public bool Run()
        {
            m_hvRowCenter = null;
            m_hvColCenter = null;
            m_listReg = new List<HObjectWithColor>();
            bool bState = true;
            m_dColCircle = -100;
            m_dRowCircle = -100;
            m_dRadiusCircle = -100;
            m_listRows = new List<double>();
            m_listCols = new List<double>();
            m_listContrasts = new List<double>();
            m_hoRegCircle = null;
            m_hoRegPoint = null;
            m_hoRegCircleCenter = null;
            HOperatorSet.GenEmptyObj(out m_hoRegCircle);
            HOperatorSet.GenEmptyObj(out m_hoRegPoint);
            HOperatorSet.GenEmptyObj(out m_hoRegCircleCenter);
            HTuple hvSecond1 = null, hvSecond2 = null;
            m_dTime = 0;
            m_strMsg = string.Empty;
            HTuple hv_ResultRow = null;
            HTuple hv_ResultColumn = null;
            HTuple hv_ResultRowCircle = null, hvResultPoint = null;
            HTuple hv_ResultColumnCircle = null, hvResultRadius = null;
            HTuple hv_ResultStartPhi = null, hvResultEndPhi = null, hv_Contrast = null;
            HOperatorSet.CountSeconds(out hvSecond1);
            string strNameRow1 = string.Empty;
            string strNameCol1 = string.Empty;
            string strNameRadius = string.Empty;
            if (Para.AffineHomSelectIndexName == "null")
            {
                m_affineHom = null;
            }
            else
            {
                int iIndex = 0;
                foreach (ParasResultAll item in m_ListParaResultAll)
                {
                    if (Para.AffineHomSelectIndexName.Contains("匹配工具" + iIndex))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Affinehom_").Contains(Para.AffineHomSelectIndexName))
                        {
                            m_affineHom = null;
                            m_affineHom = item.PatMaxResultPara.Affinehom;
                            break;
                        }
                    }

                    #region data1
                    if (Para.RowSelectIndexName != "null")
                    {
                        if (Para.RowSelectIndexName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (Para.RowSelectIndexName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (Para.RowSelectIndexName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (Para.RowSelectIndexName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(Para.RowSelectIndexName))
                                {
                                    strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(Para.RowSelectIndexName))
                                {
                                    strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(Para.RowSelectIndexName))
                                {
                                    strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (Para.RowSelectIndexName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (Para.RowSelectIndexName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                        }
                        if (Para.RowSelectIndexName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                        if (Para.RowSelectIndexName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(Para.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                    }

                    #endregion

                    #region data2
                    if (Para.ColSelectIndexName != null)
                    {
                        if (Para.ColSelectIndexName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (Para.ColSelectIndexName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (Para.ColSelectIndexName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (Para.ColSelectIndexName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(Para.ColSelectIndexName))
                                {
                                    strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(Para.ColSelectIndexName))
                                {
                                    strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(Para.ColSelectIndexName))
                                {
                                    strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (Para.ColSelectIndexName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (Para.ColSelectIndexName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                        }
                        if (Para.ColSelectIndexName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                        if (Para.ColSelectIndexName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(Para.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                    }

                    #endregion

                    #region data3
                    if (Para.RadiusSelectIndexName != null)
                    {
                        if (Para.RadiusSelectIndexName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (Para.RadiusSelectIndexName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (Para.RadiusSelectIndexName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (Para.RadiusSelectIndexName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(Para.RadiusSelectIndexName))
                                {
                                    strNameRadius = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(Para.RadiusSelectIndexName))
                                {
                                    strNameRadius = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(Para.RadiusSelectIndexName))
                                {
                                    strNameRadius = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (Para.RadiusSelectIndexName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (Para.RadiusSelectIndexName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                        }
                        if (Para.RadiusSelectIndexName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(Para.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }

                    #endregion

                    iIndex++;
                }
                if (strNameRow1.Length > 0)
                {
                    // 将链接的数据赋予输入参数
                    Para.CircleCalliper.RowCenter = double.Parse(strNameRow1.Substring(strNameRow1.LastIndexOf('_') + 1,
                                                         (strNameRow1.Length - (strNameRow1.LastIndexOf('_') + 1))), NumberStyles.Any);
                }
                if (strNameCol1.Length > 0)
                {
                    Para.CircleCalliper.ColCenter = double.Parse(strNameCol1.Substring(strNameCol1.LastIndexOf('_') + 1,
                                                          (strNameCol1.Length - (strNameCol1.LastIndexOf('_') + 1))), NumberStyles.Any);
                }
                if (strNameRadius.Length > 0)
                {
                    Para.CircleCalliper.Radius = double.Parse(strNameRadius.Substring(strNameRadius.LastIndexOf('_') + 1,
                                                          (strNameRadius.Length - (strNameRadius.LastIndexOf('_') + 1))), NumberStyles.Any);
                }
            }

            if (null != m_hoInputImg)
            {
                HTuple hvRow = null, hvCol = null;
                HOperatorSet.GetImageSize(m_hoInputImg, out hvCol, out hvRow);
                if (null != m_affineHom)
                {
                    HOperatorSet.AffineTransPoint2d(m_affineHom,
                                 Para.CircleCalliper.RowCenter,
                                 Para.CircleCalliper.ColCenter,
                                 out m_hvRowCenter, out m_hvColCenter);
                }
                else
                {
                    m_hvRowCenter = Para.CircleCalliper.RowCenter;
                    m_hvColCenter = Para.CircleCalliper.ColCenter;
                }
                spoke(m_hoInputImg, out m_hoRegPoint,
                      Para.CircleCalliper.CalliperCircleNum,
                      Para.CircleCalliper.HeightCircle,
                      Para.CircleCalliper.WidthCircle,
                      1, Para.Contrast, Para.Polarity,
                      Para.Select, out hv_ResultRow,
                      out hv_ResultColumn, out hv_Contrast);
                if (hv_ResultRow != null && hv_ResultRow.Length > 0)
                {
                    for (int i = 0; i < hv_ResultRow.Length; i++)
                    {
                        m_listRows.Add(hv_ResultRow[i].D);
                        m_listCols.Add(hv_ResultColumn[i].D);
                        m_listContrasts.Add(hv_Contrast[i].D);
                    }
                }
                else
                {
                    bState = false;
                    m_listRows = new List<double>();
                    m_listCols = new List<double>();
                    m_dColCircle = hvCol/2;
                    m_dRowCircle = hvRow/2;
                    m_dRadiusCircle = -100;
                    m_strMsg = "找圆失败！！";
                    return bState;
                }
                if (m_listRows.Count <Para.FitCircleNum)
                {
                    bState = false;
                    m_listRows = new List<double>();
                    m_listCols = new List<double>();
                    m_dColCircle = hvCol/2;
                    m_dRowCircle = hvRow/2;
                    m_dRadiusCircle = -100;
                    m_strMsg = "卡尺卡到的点不足设定的"+ Para.FitCircleNum+"个数！！";
                    return bState;
                }

                pts_to_best_circle(out m_hoRegCircle, hv_ResultRow, hv_ResultColumn, 3,
                                   out hv_ResultRowCircle, out hv_ResultColumnCircle,
                                   out hvResultRadius, out hv_ResultStartPhi,
                                   out hvResultEndPhi, out hvResultPoint);
                if (null != hvResultRadius && hvResultRadius.Length > 0)
                {
                    m_dColCircle = hv_ResultColumnCircle[0].D;
                    m_dRowCircle = hv_ResultRowCircle[0].D;
                    m_dRadiusCircle = hvResultRadius[0].D;
                    HOperatorSet.GenCrossContourXld(out m_hoRegCircleCenter,
                                                    m_dRowCircle, m_dColCircle, 10, 1);
                    m_listReg.Add(new HObjectWithColor(m_hoRegCircleCenter, "blue"));
                    m_listReg.Add(new HObjectWithColor(m_hoRegCircle, "green"));
                }
                else
                {
                    HOperatorSet.GenEmptyObj(out m_hoRegCircle);
                    HOperatorSet.GenEmptyObj(out m_hoRegPoint);
                    HOperatorSet.GenEmptyObj(out m_hoRegCircleCenter);
                    m_dColCircle = hvCol/2;
                    m_dRowCircle = hvRow/2;
                    m_dRadiusCircle = -100;
                    bState = false;
                    m_strMsg = "拟合失败！！";
                    m_listReg = new List<HObjectWithColor>();
                }
                HOperatorSet.CountSeconds(out hvSecond2);
                m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                return bState;
            }
            else
            {
                bState = false;
                m_strMsg = "请读取图像！！";
                m_dColCircle = -100;
                m_dRowCircle = -100;
                m_dRadiusCircle = -100;
                HOperatorSet.CountSeconds(out hvSecond2);
                m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                return bState;
            }
        }

        public void pts_to_best_circle(out HObject ho_Circle, HTuple hv_Rows, HTuple hv_Cols,
                                       HTuple hv_ActiveNum, out HTuple hv_RowCenter,
                                       out HTuple hv_ColCenter, out HTuple hv_Radius,
                                       out HTuple hv_StartPhi, out HTuple hv_EndPhi,
                                       out HTuple hv_PointOrder)
        {

            m_strMsg = string.Empty;

            // Local iconic variables 

            HObject ho_Contour = null;

            // Local control variables 

            HTuple hv_Length = null, hv_Length1 = new HTuple();
            HTuple hv_CircleLength = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            hv_StartPhi = new HTuple();
            hv_EndPhi = new HTuple();
            hv_PointOrder = new HTuple();
            //hv_ArcAngle = new HTuple();
            try
            {
                //初始化
                hv_RowCenter = null;
                hv_ColCenter = null;
                hv_Radius = null;
                //产生一个空的直线对象，用于保存拟合后的圆
                ho_Circle.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Circle);
                //计算边缘数量
                if (hv_Cols != null)
                {
                    HOperatorSet.TupleLength(hv_Cols, out hv_Length);
                    //当边缘数量不小于有效点数时进行拟合
                    if ((int)((new HTuple(hv_Length.TupleGreaterEqual(hv_ActiveNum))).TupleAnd(
                        new HTuple(hv_ActiveNum.TupleGreater(2)))) != 0)
                    {
                        //{
                        //如果是闭合的圆，轮廓需要首尾相连
                        ho_Contour.Dispose();
                        HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows.TupleConcat(hv_Rows.TupleSelect(
                            0)), hv_Cols.TupleConcat(hv_Cols.TupleSelect(0)));
                        //HOperatorSet.WriteObject(ho_Contour,"D:\\1");
                        // }
                        //拟合圆。使用的算法是''geotukey''，其他算法请参考fit_circle_contour_xld的描述部分。
                        HOperatorSet.FitCircleContourXld(ho_Contour, "geohuber", -1, 0, 0, 3, 2,
                            out hv_RowCenter, out hv_ColCenter, out hv_Radius, out hv_StartPhi, out hv_EndPhi,
                            out hv_PointOrder);
                        if (null != hv_StartPhi)
                        //判断拟合结果是否有效：如果拟合成功，数组中元素的数量大于0
                        {
                            HOperatorSet.TupleLength(hv_StartPhi, out hv_Length1);
                            if ((int)(new HTuple(hv_Length1.TupleLess(1))) != 0)
                            {
                                ho_Contour.Dispose();

                                return;
                            }
                        }
                        if (hv_RowCenter != null && hv_RowCenter.Length > 0)
                        {
                            hv_StartPhi = 0;
                            hv_EndPhi = (new HTuple(360)).TupleRad();
                            // hv_ArcAngle = (new HTuple(360)).TupleRad();
                            ho_Circle.Dispose();
                            HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter,
                                hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 1);
                        }
                        else
                        {

                            hv_RowCenter = null;
                            hv_ColCenter = null;
                            hv_Radius = null;
                        }
                    }

                    ho_Contour.Dispose();

                    return;
                }
            }
            catch (HalconException ex)
            {
                hv_RowCenter = null;
                hv_ColCenter = null;
                hv_Radius = null;
                ho_Contour.Dispose();
                m_strMsg = "拟合失败！" + ex.Message;
            }
        }

        public void spoke(HObject ho_Image, out HObject ho_Regions, HTuple hv_Elements,
            HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Sigma, HTuple hv_Threshold,
            HTuple hv_Transition, HTuple hv_Select, out HTuple hv_ResultRow,
            out HTuple hv_ResultColumn, out HTuple hv_Contrast)
        {

            m_strMsg = "拟合成功！";
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Contour, ho_ContCircle, ho_Rectangle1 = null;
            HObject ho_Arrow1 = null;

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null, hv_RowC = null;
            HTuple hv_ColumnC = null, hv_Radius = null, hv_StartPhi = null;
            HTuple hv_EndPhi = null, hv_PointOrder = null, hv_RowXLD = null;
            HTuple hv_ColXLD = null, hv_Length2 = null, hv_i = null;
            HTuple hv_j = new HTuple(), hv_RowE = new HTuple(), hv_ColE = new HTuple();
            HTuple hv_ATan = new HTuple(), hv_RowL2 = new HTuple();
            HTuple hv_RowL1 = new HTuple(), hv_ColL2 = new HTuple();
            HTuple hv_ColL1 = new HTuple(), hv_MsrHandle_Measure = new HTuple();
            HTuple hv_RowEdge = new HTuple(), hv_ColEdge = new HTuple();
            HTuple hv_Amplitude = new HTuple(), hv_Distance = new HTuple();
            HTuple hv_tRow = new HTuple(), hv_tCol = new HTuple();
            HTuple hv_t = new HTuple(), hv_Number = new HTuple(), hv_k = new HTuple();
            HTuple hv_Select_COPY_INP_TMP = hv_Select.Clone();
            HTuple hv_Transition_COPY_INP_TMP = hv_Transition.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            HOperatorSet.GenEmptyObj(out ho_ContCircle);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Arrow1);
            hv_Contrast = new HTuple();
            try
            {
                // 获取图像尺寸
                if (null != ho_Image)
                {
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                }
                //产生一个空显示对象，用于显示
                ho_Regions.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Regions);
                //初始化边缘坐标数组
                hv_ResultRow = new HTuple();
                hv_ResultColumn = new HTuple();

                ////产生xld
                ////ho_Contour.Dispose();
                ////HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_ROIRows, hv_ROICols);
                ////用回归线法（不抛出异常点，所有点权重一样）拟合圆
                //HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_RowC,
                //    out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                //根据拟合结果产生xld，并保持到显示对象
                ho_ContCircle.Dispose();
                HOperatorSet.GenCircleContourXld(out ho_ContCircle, m_hvRowCenter,
                    m_hvColCenter, Para.CircleCalliper.Radius,
                    0, 6.28318, "positive", 3);
                {
                    HObject ExpTmpOutVar_0;
                    //HOperatorSet.ConcatObj(ho_Regions, ho_ContCircle, out ExpTmpOutVar_0);
                    //ho_Regions.Dispose();
                    // ho_Regions = ExpTmpOutVar_0;
                }
                HOperatorSet.FitCircleContourXld(ho_ContCircle, "algebraic", -1, 0, 0, 3, 2, out hv_RowC,
                    out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                //获取圆或圆弧xld上的点坐标
                HOperatorSet.GetContourXld(ho_ContCircle, out hv_RowXLD, out hv_ColXLD);

                //求圆或圆弧xld上的点的数量
                HOperatorSet.TupleLength(hv_ColXLD, out hv_Length2);
                if ((int)(new HTuple(hv_Elements.TupleLess(3))) != 0)
                {
                    //disp_message(hv_ExpDefaultWinHandle, "检测的边缘数量太少，请重新设置!", "window",
                    //    52, 12, "red", "false");
                    ho_Contour.Dispose();
                    ho_ContCircle.Dispose();
                    ho_Rectangle1.Dispose();
                    ho_Arrow1.Dispose();

                    m_strMsg = "检测边缘数过少！！";
                    return;
                }
                //如果xld是圆弧，有Length2个点，从起点开始，等间距（间距为Length2/(Elements-1)）取Elements个点，作为卡尺工具的中点
                //如果xld是圆，有Length2个点，以0°为起点，从起点开始，等间距（间距为Length2/(Elements)）取Elements个点，作为卡尺工具的中点
                HTuple end_val27 = hv_Elements - 1;
                HTuple step_val27 = 1;
                for (hv_i = 0; hv_i.Continue(end_val27, step_val27); hv_i = hv_i.TupleAdd(step_val27))
                {

                    //if ((int)(new HTuple(((hv_RowXLD.TupleSelect(0))).TupleEqual(hv_RowXLD.TupleSelect(
                    //    hv_Length2 - 1)))) != 0)
                    {
                        //xld的起点和终点坐标相对，为圆
                        HOperatorSet.TupleInt(((1.0 * hv_Length2) / hv_Elements) * hv_i, out hv_j);
                        // hv_ArcType = "circle";
                    }
                    //else
                    //{
                    //    //否则为圆弧
                    //    HOperatorSet.TupleInt(((1.0 * hv_Length2) / (hv_Elements - 1)) * hv_i, out hv_j);
                    //    hv_ArcType = "arc";
                    //}
                    //索引越界，强制赋值为最后一个索引
                    if ((int)(new HTuple(hv_j.TupleGreaterEqual(hv_Length2))) != 0)
                    {
                        hv_j = hv_Length2 - 1;
                        //continue
                    }
                    //获取卡尺工具中心
                    hv_RowE = hv_RowXLD.TupleSelect(hv_j);
                    hv_ColE = hv_ColXLD.TupleSelect(hv_j);

                    //超出图像区域，不检测，否则容易报异常
                    if ((int)((new HTuple((new HTuple((new HTuple(hv_RowE.TupleGreater(hv_Height - 1))).TupleOr(
                        new HTuple(hv_RowE.TupleLess(0))))).TupleOr(new HTuple(hv_ColE.TupleGreater(
                        hv_Width - 1))))).TupleOr(new HTuple(hv_ColE.TupleLess(0)))) != 0)
                    {
                        continue;
                    }
                    //边缘搜索方向类型：'inner'搜索方向由圆外指向圆心；'outer'搜索方向由圆心指向圆外
                    // if ((int)(new HTuple(hv_Direct.TupleEqual("inner"))) != 0)
                    {
                        //求卡尺工具的边缘搜索方向
                        //求圆心指向边缘的矢量的角度
                        HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                        hv_ATan = hv_ATan + ((new HTuple(Para.CircleCalliper.PhiCircle)).TupleRad());
                        //角度反向
                        // hv_ATan = ((new HTuple(180)).TupleRad()) + hv_ATan;
                    }
                    //   else
                    //{
                    //    //求卡尺工具的边缘搜索方向
                    //    //求圆心指向边缘的矢量的角度
                    //    HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                    //}


                    //产生卡尺xld，并保持到显示对象
                    ho_Rectangle1.Dispose();
                    HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle1, hv_RowE, hv_ColE,
                        hv_ATan, hv_DetectHeight / 2, hv_DetectWidth / 2);
                    {
                        //HObject ExpTmpOutVar_0;
                        //HOperatorSet.ConcatObj(ho_Regions, ho_Rectangle1, out ExpTmpOutVar_0);
                        //ho_Regions.Dispose();
                        //ho_Regions = ExpTmpOutVar_0;
                    }
                    //用箭头xld指示边缘搜索方向，并保持到显示对象
                    if ((int)(new HTuple(hv_i.TupleEqual(0))) != 0)
                    {
                        hv_RowL2 = hv_RowE + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                        hv_RowL1 = hv_RowE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                        hv_ColL2 = hv_ColE + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                        hv_ColL1 = hv_ColE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                        ho_Arrow1.Dispose();
                        //gen_arrow_contour_xld(out ho_Arrow1, hv_RowL1, hv_ColL1, hv_RowL2, hv_ColL2,
                        //   25, 25);
                        {
                            // HObject ExpTmpOutVar_0;
                            //// HOperatorSet.ConcatObj(ho_Regions, ho_Arrow1, out ExpTmpOutVar_0);
                            // ho_Regions.Dispose();
                            //ho_Regions = ExpTmpOutVar_0;
                        }
                    }


                    //产生测量对象句柄
                    HOperatorSet.GenMeasureRectangle2(hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2,
                        hv_DetectWidth / 2, hv_Width, hv_Height, "nearest_neighbor", out hv_MsrHandle_Measure);

                    //设置极性
                    if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("negative"))) != 0)
                    {
                        hv_Transition_COPY_INP_TMP = "negative";
                    }
                    else
                    {
                        if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("positive"))) != 0)
                        {

                            hv_Transition_COPY_INP_TMP = "positive";
                        }
                        else
                        {
                            hv_Transition_COPY_INP_TMP = "all";
                        }
                    }
                    //设置边缘位置。最强点是从所有边缘中选择幅度绝对值最大点，需要设置为'all'
                    if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("first"))) != 0)
                    {
                        hv_Select_COPY_INP_TMP = "first";
                    }
                    else
                    {
                        if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("last"))) != 0)
                        {

                            hv_Select_COPY_INP_TMP = "last";
                        }
                        else
                        {
                            hv_Select_COPY_INP_TMP = "all";
                        }
                    }
                    //检测边缘
                    HOperatorSet.MeasurePos(ho_Image, hv_MsrHandle_Measure, hv_Sigma, hv_Threshold,
                        hv_Transition_COPY_INP_TMP, hv_Select_COPY_INP_TMP, out hv_RowEdge, out hv_ColEdge,
                        out hv_Amplitude, out hv_Distance);
                    //清除测量对象句柄
                    HOperatorSet.CloseMeasure(hv_MsrHandle_Measure);
                    //临时变量初始化
                    //tRow，tCol保存找到指定边缘的坐标
                    hv_tRow = 0;
                    hv_tCol = 0;
                    //t保存边缘的幅度绝对值
                    hv_t = 0;
                    HOperatorSet.TupleLength(hv_RowEdge, out hv_Number);
                    //找到的边缘必须至少为1个
                    if ((int)(new HTuple(hv_Number.TupleLess(1))) != 0)
                    {
                        continue;
                    }
                    //有多个边缘时，选择幅度绝对值最大的边缘
                    HTuple end_val120 = hv_Number - 1;
                    HTuple step_val120 = 1;
                    for (hv_k = 0; hv_k.Continue(end_val120, step_val120); hv_k = hv_k.TupleAdd(step_val120))
                    {
                        if ((int)(new HTuple(((((hv_Amplitude.TupleSelect(hv_k))).TupleAbs())).TupleGreater(
                            hv_t))) != 0)
                        {

                            hv_tRow = hv_RowEdge.TupleSelect(hv_k);
                            hv_tCol = hv_ColEdge.TupleSelect(hv_k);
                            hv_t = ((hv_Amplitude.TupleSelect(hv_k))).TupleAbs();
                        }
                    }
                    //把找到的边缘保存在输出数组
                    if ((int)(new HTuple(hv_t.TupleGreater(0))) != 0)
                    {
                        hv_Contrast = hv_Contrast.TupleConcat(hv_t);
                        hv_ResultRow = hv_ResultRow.TupleConcat(hv_tRow);
                        hv_ResultColumn = hv_ResultColumn.TupleConcat(hv_tCol);
                    }
                    if (hv_ResultRow.Length > 0)
                    {
                        HOperatorSet.GenCrossContourXld(out ho_Regions, hv_ResultRow, hv_ResultColumn, 10, 1);
                    }
                }

                ho_Contour.Dispose();
                ho_ContCircle.Dispose();
                ho_Rectangle1.Dispose();
                ho_Arrow1.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Contour.Dispose();
                ho_ContCircle.Dispose();
                ho_Rectangle1.Dispose();
                ho_Arrow1.Dispose();
                m_strMsg = "找圆失败！！" + HDevExpDefaultException.Message;
                hv_ResultColumn = null;
                hv_ResultRow = null;
            }
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="obj">保存对象</param>
        /// <param name="filename">保存地址</param>
        /// <returns>true:保存成功</returns>
        public bool Save(object obj, string filename)
        {
            FileStream fs = null;
            try
            {
                string strarry = filename.Substring(0, filename.IndexOf("\\"));
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write,
                                    FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return true;
        }

        /// <summary>
        /// 加载方法
        /// </summary>
        /// <param name="type">加载数据类型</param>
        /// <param name="filename">加载地址</param>
        /// <returns>true:加载成功</returns>
        public object Load(Type type, string filename)
        {

            FileStream fs = null;
            try
            {
                string strarry = filename.Substring(0, filename.IndexOf("\\"));
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                return Para;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
    }
}
