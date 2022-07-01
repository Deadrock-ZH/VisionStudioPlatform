using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using HalconDotNet;
using ParaResultAll;

namespace SvJudgeResultParaTool
{
    /// <summary>
    /// 内 容:本类是数据判断方法类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public class SvJudgeResultParaToolMethod : ToolInterFace.ToolInterface
    {
        private SvJudgeResultParaToolPara m_Para = new SvJudgeResultParaToolPara();

        /// <summary>
        /// 结果判断参数类
        /// </summary>
        public SvJudgeResultParaToolPara ParasSvJudgeResultParaTool
        {
            get
            {
                return m_Para;
            }
            set
            {
                m_Para = value;
            }
        }

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
        /// 输入图像
        /// </summary>
        public HObject InputImage
        {
            get;
            set;
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

        private string m_strRunMsg = string.Empty;

        /// <summary>
        /// 运行消息
        /// </summary>
        public string RunMsg
        {
            get
            {
                return m_strRunMsg;
            }
            set
            {
                m_strRunMsg = value;
            }
        }

        /// <summary>
        /// 模块名
        /// </summary>
        public string ModualFormName
        {
            get;
            set;
        }

        /// <summary>
        /// 输出区域
        /// </summary>
        public List<global::GVS.HalconDisp.ViewROI.Config.HObjectWithColor> ListReg
        {
            get;
            set;
        }

        private List<double> m_ListSendDataMsg = new List<double>();

        /// <summary>
        /// 发送给PLC的数据
        /// </summary>
        public List<double> ListSendDataMsg
        {
            get
            {
                return m_ListSendDataMsg;
            }
            set
            {
                m_ListSendDataMsg = value;
            }
        }

        private List<bool> m_ListSendDataMsgState = new List<bool>();

        /// <summary>
        /// 发送数据比较状态，在最小及最大范围内为true
        /// </summary>
        public List<bool> ListSendDataMsgState
        {
            get
            {
                return m_ListSendDataMsgState;
            }
            set
            {
                m_ListSendDataMsgState = value;
            }
        }

        private List<bool> m_ListHaveOrNotState = new List<bool>();

        /// <summary>
        /// 有无产品标志，在最小及最大范围内为true
        /// </summary>
        public List<bool> ListHaveOrNotState
        {
            get
            {
                return m_ListHaveOrNotState;
            }
            set
            {
                m_ListHaveOrNotState = value;
            }
        }

        private double[] m_JudgeArrayDispData = new double[16];

        /// <summary>
        /// 结果判断工具中界面显示结果数据
        /// </summary>
        public double[] JudgeArrayDispResultData
        {
            get
            {
                return m_JudgeArrayDispData;
            }
            set
            {
                m_JudgeArrayDispData = value;
            }
        }

        private bool[] m_JudgeArraybResultDispState = new bool[16];

        /// <summary>
        /// 结果判断工具中界面显示结果数据比较状态
        /// </summary>
        public bool[] DispArrayBoolJudgeState
        {
            get
            {
                return m_JudgeArraybResultDispState;
            }
            set
            {
                m_JudgeArraybResultDispState = value;
            }
        }

        /// <summary>
        /// 运行方法
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            bool bState = true;
            bool bResultJudgeState = false;
            m_dTime = 0;
            m_strRunMsg = string.Empty;
            m_ListSendDataMsg = new List<double>();
            m_ListSendDataMsgState = new List<bool>();
            m_ListHaveOrNotState = new List<bool>();
            double dSenData = 0;
            HTuple hvSecond1 = null, hvSecond2 = null;
            HOperatorSet.CountSeconds(out hvSecond1);
            try
            {
                int iIndex = 0;
                string strNameRow1 = string.Empty;
                string strNameRow2 = string.Empty;
                string strNameRow3 = string.Empty;
                string strNameRow4 = string.Empty;
                string strNameRow5 = string.Empty;
                string strNameRow6 = string.Empty;
                string strNameRow7 = string.Empty;
                string strNameRow8 = string.Empty;
                string strNameRow9 = string.Empty;
                string strNameRow10 = string.Empty;
                string strNameRow11 = string.Empty;
                string strNameRow12 = string.Empty;
                string strNameRow13 = string.Empty;
                string strNameRow14 = string.Empty;
                string strNameRow15 = string.Empty;
                string strNameRow16 = string.Empty;
                foreach (ParasResultAll item in m_ListParaResultAll)
                {
                    #region data1
                    if (m_Para.ListSelectRangePara[0].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[0].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[0].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[0].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[0].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[0].SelectName))
                                {
                                    strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[0].SelectName))
                                {
                                    strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[0].SelectName))
                                {
                                    strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[0].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[0].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[0].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[0].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[0].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                    }

                    #endregion
                    #region data2
                    if (m_Para.ListSelectRangePara[1].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[1].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[1].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[1].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[1].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[1].SelectName))
                                {
                                    strNameRow2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[1].SelectName))
                                {
                                    strNameRow2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[1].SelectName))
                                {
                                    strNameRow2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[1].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[1].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[1].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[1].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[1].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                    }

                    #endregion
                    #region data3
                    if (m_Para.ListSelectRangePara[2].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[2].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[2].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[2].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[2].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[2].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[2].SelectName))
                                {
                                    strNameRow3 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[2].SelectName))
                                {
                                    strNameRow3 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[2].SelectName))
                                {
                                    strNameRow3 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[2].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[2].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[2].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[2].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[2].SelectName))
                            {
                                strNameRow3 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                    }


                    #endregion
                    #region data4
                    if (m_Para.ListSelectRangePara[3].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[3].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[3].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[3].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[3].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[3].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[3].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[3].SelectName))
                                {
                                    strNameRow4 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[3].SelectName))
                                {
                                    strNameRow4 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[3].SelectName))
                                {
                                    strNameRow4 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[3].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[3].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[3].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[3].SelectName))
                            {
                                strNameRow4 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }


                    #endregion
                    #region data5
                    if (m_Para.ListSelectRangePara[4].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[4].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[4].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[4].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[4].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[4].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[4].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[4].SelectName))
                                {
                                    strNameRow5 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[4].SelectName))
                                {
                                    strNameRow5 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[4].SelectName))
                                {
                                    strNameRow5 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[4].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[4].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[4].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[4].SelectName))
                            {
                                strNameRow5 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }

                    #endregion
                    #region data6
                    if (m_Para.ListSelectRangePara[5].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[5].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[5].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[5].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[5].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[5].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[5].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[5].SelectName))
                                {
                                    strNameRow6 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[5].SelectName))
                                {
                                    strNameRow6 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[5].SelectName))
                                {
                                    strNameRow6 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[5].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[5].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[5].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[5].SelectName))
                            {
                                strNameRow6 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }

                    #endregion
                    #region data7
                    if (m_Para.ListSelectRangePara[6].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[6].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[6].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[6].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[6].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[6].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[6].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[6].SelectName))
                                {
                                    strNameRow7 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[6].SelectName))
                                {
                                    strNameRow7 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[6].SelectName))
                                {
                                    strNameRow7 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[6].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[6].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[6].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[6].SelectName))
                            {
                                strNameRow7 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }

                    #endregion
                    #region data8
                    if (m_Para.ListSelectRangePara[7].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[7].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[7].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[7].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[7].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[7].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[7].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[7].SelectName))
                                {
                                    strNameRow8 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[7].SelectName))
                                {
                                    strNameRow8 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[7].SelectName))
                                {
                                    strNameRow8 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[7].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[7].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[7].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[7].SelectName))
                            {
                                strNameRow8 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }

                    #endregion
                    #region data9
                    if (m_Para.ListSelectRangePara[8].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[8].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[8].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[8].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[8].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[8].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[8].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[8].SelectName))
                                {
                                    strNameRow9 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[8].SelectName))
                                {
                                    strNameRow9 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[8].SelectName))
                                {
                                    strNameRow9 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[8].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[8].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[8].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[8].SelectName))
                            {
                                strNameRow9 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }

                    #endregion
                    #region data10
                    if (m_Para.ListSelectRangePara[9].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[9].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[10].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[9].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[9].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[9].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[9].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[9].SelectName))
                                {
                                    strNameRow10 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[9].SelectName))
                                {
                                    strNameRow10 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[9].SelectName))
                                {
                                    strNameRow10 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[9].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[9].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[9].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[9].SelectName))
                            {
                                strNameRow10 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }


                    #endregion

                    #region data11
                    if (m_Para.ListSelectRangePara[10].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[10].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[10].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[10].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[10].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[10].SelectName))
                                {
                                    strNameRow11 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[10].SelectName))
                                {
                                    strNameRow11 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[10].SelectName))
                                {
                                    strNameRow11 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[10].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[10].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[10].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[10].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[10].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[10].SelectName))
                            {
                                strNameRow11 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                    }

                    #endregion
                    #region data12
                    if (m_Para.ListSelectRangePara[11].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[11].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[11].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[11].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[11].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[11].SelectName))
                                {
                                    strNameRow12 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[11].SelectName))
                                {
                                    strNameRow12 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[11].SelectName))
                                {
                                    strNameRow12 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[11].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[11].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[11].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[11].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[11].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[11].SelectName))
                            {
                                strNameRow12 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                    }

                    #endregion
                    #region data13
                    if (m_Para.ListSelectRangePara[12].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[12].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[12].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[12].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[12].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[12].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[12].SelectName))
                                {
                                    strNameRow13 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[12].SelectName))
                                {
                                    strNameRow13 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[12].SelectName))
                                {
                                    strNameRow13 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[12].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[12].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[12].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[12].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[12].SelectName))
                            {
                                strNameRow13 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                    }
                    #endregion
                    #region data14
                    if (m_Para.ListSelectRangePara[13].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[13].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[13].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[13].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[13].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[13].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[13].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[13].SelectName))
                                {
                                    strNameRow14 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[13].SelectName))
                                {
                                    strNameRow14 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[13].SelectName))
                                {
                                    strNameRow14 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[13].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[13].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[13].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[13].SelectName))
                            {
                                strNameRow14 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }


                    #endregion
                    #region data15
                    if (m_Para.ListSelectRangePara[14].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[14].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[14].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[14].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[14].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[14].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[14].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[14].SelectName))
                                {
                                    strNameRow15 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[14].SelectName))
                                {
                                    strNameRow15 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[14].SelectName))
                                {
                                    strNameRow15 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[14].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[14].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[14].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[14].SelectName))
                            {
                                strNameRow15 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }

                    #endregion
                    #region data16
                    if (m_Para.ListSelectRangePara[15].SelectState)
                    {
                        if (m_Para.ListSelectRangePara[15].SelectName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                        if (m_Para.ListSelectRangePara[15].SelectName.Contains("线线角度工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                            }
                            if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[15].SelectName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[15].SelectName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (m_Para.ListSelectRangePara[15].SelectName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (m_Para.ListSelectRangePara[15].SelectName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(m_Para.ListSelectRangePara[15].SelectName))
                                {
                                    strNameRow16 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(m_Para.ListSelectRangePara[15].SelectName))
                                {
                                    strNameRow16 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(m_Para.ListSelectRangePara[15].SelectName))
                                {
                                    strNameRow16 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (m_Para.ListSelectRangePara[15].SelectName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (m_Para.ListSelectRangePara[15].SelectName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }
                        if (m_Para.ListSelectRangePara[15].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[15].SelectName))
                            {
                                strNameRow16 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }
                    #endregion
                    iIndex++;
                }

                #region  数据连接成功后传递给对应参数
                if (m_Para.ListSelectRangePara[0].SelectState)
                {
                    if (strNameRow1 != null && strNameRow1.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[0].Data = Math.Round(double.Parse(strNameRow1.Substring(strNameRow1.LastIndexOf('_') + 1,
                                                            (strNameRow1.Length - (strNameRow1.LastIndexOf('_') + 1))), NumberStyles.Any), 
                                                             m_Para.ListSelectRangePara[0].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[0].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 0 + "连接为空";
                        // return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[1].SelectState)
                {
                    if (strNameRow2 != null && strNameRow2.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[1].Data = Math.Round(double.Parse(strNameRow2.Substring(strNameRow2.LastIndexOf('_') + 1, 
                                                             (strNameRow2.Length - (strNameRow2.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                              m_Para.ListSelectRangePara[1].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[1].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 1 + "连接为空";
                        // return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[2].SelectState)
                {
                    if (strNameRow3 != null && strNameRow3.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[2].Data = Math.Round(double.Parse(strNameRow3.Substring(strNameRow3.LastIndexOf('_') + 1,
                                                             (strNameRow3.Length - (strNameRow3.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                              m_Para.ListSelectRangePara[2].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[2].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 2 + "连接为空";
                        //return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[3].SelectState)
                {
                    if (strNameRow4 != null && strNameRow4.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[3].Data = Math.Round(double.Parse(strNameRow4.Substring(strNameRow4.LastIndexOf('_') + 1, 
                                                             (strNameRow4.Length - (strNameRow4.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                             m_Para.ListSelectRangePara[3].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[3].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 3 + "连接为空";
                        //return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[4].SelectState)
                {
                    if (strNameRow5 != null && strNameRow5.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[4].Data = Math.Round(double.Parse(strNameRow5.Substring(strNameRow5.LastIndexOf('_') + 1, 
                                                            (strNameRow5.Length - (strNameRow5.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                             m_Para.ListSelectRangePara[4].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[4].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 4 + "连接为空";
                        // return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[5].SelectState)
                {
                    if (strNameRow6 != null && strNameRow6.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[5].Data = Math.Round(double.Parse(strNameRow6.Substring(strNameRow6.LastIndexOf('_') + 1, 
                                                             (strNameRow6.Length - (strNameRow6.LastIndexOf('_') + 1))), NumberStyles.Any), 
                                                             m_Para.ListSelectRangePara[5].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[5].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 5 + "连接为空";
                        // return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[6].SelectState)
                {
                    if (strNameRow7 != null && strNameRow7.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[6].Data = Math.Round(double.Parse(strNameRow7.Substring(strNameRow7.LastIndexOf('_') + 1, 
                                                             (strNameRow7.Length - (strNameRow7.LastIndexOf('_') + 1))), NumberStyles.Any), 
                                                              m_Para.ListSelectRangePara[6].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[6].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 6 + "连接为空";
                        //return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[7].SelectState)
                {
                    if (strNameRow8 != null && strNameRow8.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[7].Data = Math.Round(double.Parse(strNameRow8.Substring(strNameRow8.LastIndexOf('_') + 1, 
                                                             (strNameRow8.Length - (strNameRow8.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                             m_Para.ListSelectRangePara[7].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[7].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 7 + "连接为空";
                        //return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[8].SelectState)
                {
                    if (strNameRow9 != null && strNameRow9.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[8].Data = Math.Round(double.Parse(strNameRow9.Substring(strNameRow9.LastIndexOf('_') + 1,
                                                             (strNameRow9.Length - (strNameRow9.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                             m_Para.ListSelectRangePara[8].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[8].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 8 + "连接为空";
                        //return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[9].SelectState)
                {
                    if (strNameRow10 != null && strNameRow10.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[9].Data = Math.Round(double.Parse(strNameRow10.Substring(strNameRow10.LastIndexOf('_') + 1,
                                                             (strNameRow10.Length - (strNameRow10.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                              m_Para.ListSelectRangePara[9].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[9].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 9 + "连接为空";
                        //return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[10].SelectState)
                {
                    if (strNameRow11 != null && strNameRow11.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[10].Data = Math.Round(double.Parse(strNameRow11.Substring(strNameRow11.LastIndexOf('_') + 1, 
                                                              (strNameRow11.Length - (strNameRow11.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                               m_Para.ListSelectRangePara[10].DecimalPlaces);
                        //break;
                    }
                else
                {
                    m_Para.ListSelectRangePara[10].Data = -100;
                    bState = false;
                    HOperatorSet.CountSeconds(out hvSecond2);
                    m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                    m_strRunMsg = "结果判断数据" + 10 + "连接为空";
                        // return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[11].SelectState)
                {
                    if (strNameRow12 != null && strNameRow12.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[11].Data = Math.Round(double.Parse(strNameRow12.Substring(strNameRow12.LastIndexOf('_') + 1,
                                                              (strNameRow12.Length - (strNameRow12.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                               m_Para.ListSelectRangePara[11].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[11].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 11 + "连接为空";
                        // return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[12].SelectState)
                {
                    if (strNameRow13 != null && strNameRow13.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[12].Data = Math.Round(double.Parse(strNameRow13.Substring(strNameRow13.LastIndexOf('_') + 1, 
                                                              (strNameRow13.Length - (strNameRow13.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                              m_Para.ListSelectRangePara[12].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[12].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 12 + "连接为空";
                        //return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[13].SelectState)
                {
                    if (strNameRow14 != null && strNameRow14.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[13].Data = Math.Round(double.Parse(strNameRow14.Substring(strNameRow14.LastIndexOf('_') + 1, 
                                                              (strNameRow14.Length - (strNameRow14.LastIndexOf('_') + 1))), NumberStyles.Any), 
                                                              m_Para.ListSelectRangePara[13].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[13].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 13 + "连接为空";
                        //return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[14].SelectState)
                {
                    if (strNameRow15 != null && strNameRow15.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[14].Data = Math.Round(double.Parse(strNameRow15.Substring(strNameRow15.LastIndexOf('_') + 1,
                                                              (strNameRow15.Length - (strNameRow15.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                              m_Para.ListSelectRangePara[14].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[14].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 14 + "连接为空";
                        // return bState;
                    }
                }
                if (m_Para.ListSelectRangePara[15].SelectState)
                {
                    if (strNameRow16 != null && strNameRow16.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[15].Data = Math.Round(double.Parse(strNameRow16.Substring(strNameRow16.LastIndexOf('_') + 1,
                                                              (strNameRow16.Length - (strNameRow16.LastIndexOf('_') + 1))), NumberStyles.Any),
                                                              m_Para.ListSelectRangePara[15].DecimalPlaces);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[15].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "结果判断数据" + 15 + "连接为空";
                        // return bState;
                    }
                }
                #endregion
                #region 数据传递成功后判断范围并将发送的数据连接
                for (int i = 0; i < m_Para.ListSelectRangePara.Length; i++)
                {
                    m_JudgeArrayDispData[i] = 0;
                    m_JudgeArraybResultDispState[i] = false;
                    if (m_Para.ListSelectRangePara[i].SelectState)
                    {
                        // 发送的数据
                        dSenData = -100;
                        // if (m_Para.ListSelectRangePara[i].Data != -100)
                        {
                            dSenData = Math.Round((m_Para.ListSelectRangePara[i].Data - m_Para.ListSelectRangePara[i].Offset) * m_Para.ListSelectRangePara[i].K, 0);
                            if (dSenData <= m_Para.ListSelectRangePara[i].MaxRange &&
                               dSenData >= m_Para.ListSelectRangePara[i].MinRange)
                            {
                                bResultJudgeState = true;
                            }
                            else
                            {
                                bResultJudgeState = false;
                            }
                            if (m_Para.ListSelectRangePara[i].SelectState)
                            {
                                m_ListSendDataMsgState.Add(bResultJudgeState);
                            }
                            if (m_Para.ListSelectRangePara[i].SendState)
                            {
                                m_ListSendDataMsg.Add(dSenData);
                            }
                            if (m_Para.ListSelectRangePara[i].HaveOrNotState)
                            {
                                m_ListHaveOrNotState.Add(bResultJudgeState);
                            }                            
                        }
                        m_JudgeArrayDispData[i] = dSenData;
                        m_JudgeArraybResultDispState[i] = bResultJudgeState;
                    }
                }
                #endregion
            }
            catch (HalconException ex)
            {
                m_ListSendDataMsgState.Add(false);
                m_ListSendDataMsg.Add(-100);
                bState = false;
                m_strRunMsg = "Catch到错误" + ex.ToString();
            }
            HOperatorSet.CountSeconds(out hvSecond2);
            m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
            return bState;
        }

        public bool Save(object obj, string filename)
        {
            throw new NotImplementedException();
        }

        public object Load(Type type, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
