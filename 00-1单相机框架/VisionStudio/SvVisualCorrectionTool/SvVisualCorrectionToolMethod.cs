using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using HalconDotNet;
using ParaResultAll;

namespace SvVisualCorrectionTool
{
    /// <summary>
    /// 内 容:本类是示教方法类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public class SvVisualCorrectionToolMethod : ToolInterFace.ToolInterface
    {
        private SvVisualCorrectionToolPara m_Para = new SvVisualCorrectionToolPara();

        /// <summary>
        /// 示教参数类
        /// </summary>
        public SvVisualCorrectionToolPara ParasSvVisualCorrectionToolPara
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

        private double m_dX1 = -100;

        /// <summary>
        /// 输出示教结果X1
        /// </summary>
        public double X1
        {
            get
            {
                return m_dX1;
            }
            set
            {
                m_dX1 = value;
            }
        }

        private double m_dY1 = -100;

        /// <summary>
        /// 输出示教结果Y1
        /// </summary>
        public double Y1
        {
            get
            {
                return m_dY1;
            }
            set
            {
                m_dY1 = value;
            }
        }

        private double m_dX2 = -100;

        /// <summary>
        /// 输出示教结果X2
        /// </summary>
        public double X2
        {
            get
            {
                return m_dX2;
            }
            set
            {
                m_dX2 = value;
            }
        }

        private double m_dY2 = -100;

        /// <summary>
        /// 输出示教结果Y2
        /// </summary>
        public double Y2
        {
            get
            {
                return m_dY2;
            }
            set
            {
                m_dY2 = value;
            }
        }

        /// <summary>
        /// 模块Form名
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

        /// <summary>
        /// 运行方法
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            bool bState = true;
            m_dTime = 0;
            m_strRunMsg = string.Empty;
            HTuple hvSecond1 = null, hvSecond2 = null;
            HOperatorSet.CountSeconds(out hvSecond1);
            try
            {
                int iIndex = 0;
                string strNameRow1 = string.Empty;
                string strNameRow2 = string.Empty;
                foreach (ParasResultAll item in m_ListParaResultAll)
                {
                    #region data1数据连接
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
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[0].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointPoints.Row" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[0].SelectName))
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
                    }
                    #endregion

                    #region data2数据连接
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
                            if ((item.ToolName + "item.ParaDistancePointPoints.Row" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaDistancePointPoints.Row_" + item.ParaDistancePointPoints.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Distance).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow2 = item.ToolName + "item.ParaDistancePointPoints.Col_" + item.ParaDistancePointPoints.Col;
                            }
                        }

                        if (m_Para.ListSelectRangePara[1].SelectName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(m_Para.ListSelectRangePara[1].SelectName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }
                    iIndex++;
                    #endregion
                }

                // 连接成功后传递至对应参数
                if (m_Para.ListSelectRangePara[0].SelectState)
                {
                    if (strNameRow1 != null && strNameRow1.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[0].Data = double.Parse(strNameRow1.Substring(strNameRow1.LastIndexOf('_') + 1,
                                                            (strNameRow1.Length - (strNameRow1.LastIndexOf('_') + 1))), NumberStyles.Any);
                        //break;
                    }
                }
                else
                {
                    m_Para.ListSelectRangePara[0].Data = -100;
                    bState = false;
                    HOperatorSet.CountSeconds(out hvSecond2);
                    m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                    m_strRunMsg = "示教数据" + 0 + "连接为空";
                    //return bState;
                }

                // 数据2连接成功后传递至对应参数
                if (m_Para.ListSelectRangePara[1].SelectState)
                {
                    if (strNameRow2 != null && strNameRow2.Length > 0)
                    {
                        // 将链接的数据赋予输入参数
                        m_Para.ListSelectRangePara[1].Data = double.Parse(strNameRow2.Substring(strNameRow2.LastIndexOf('_') + 1,
                                                             (strNameRow2.Length - (strNameRow2.LastIndexOf('_') + 1))), NumberStyles.Any);
                        //break;
                    }
                    else
                    {
                        m_Para.ListSelectRangePara[1].Data = -100;
                        bState = false;
                        HOperatorSet.CountSeconds(out hvSecond2);
                        m_dTime = (hvSecond2[0].D - hvSecond1[0].D) * 1000;
                        m_strRunMsg = "示教数据" + 1 + "连接为空";
                        // return bState;
                    }
                }

                // 传递成功后根据选择的示教类型计算输出的X\Y值
                if (m_Para.ListSelectRangePara[0].SelectState && m_Para.ListSelectRangePara[1].SelectState)
                {
                    // 来料上影像
                    if (m_Para.EnumTypeChecks == EnumTypeCheck.来料上影像)
                    {
                        m_dX1 = 1000 * m_Para.XM1 * ((m_Para.ColB2 * m_Para.ListSelectRangePara[0].Data - m_Para.RowK2 * m_Para.ListSelectRangePara[1].Data) /
                                (m_Para.RowK1 * m_Para.ColB2 - m_Para.RowK2 * m_Para.ColB1));
                        m_dY1 = 1000 * m_Para.YM2 * ((m_Para.RowK1 * m_Para.ListSelectRangePara[1].Data - m_Para.ColB1 * m_Para.ListSelectRangePara[0].Data) /
                               (m_Para.RowK1 * m_Para.ColB2 - m_Para.RowK2 * m_Para.ColB1));
                    }

                    // 组立上影像
                    if (m_Para.EnumTypeChecks == EnumTypeCheck.组立上影像)
                    {
                        m_dX1 = 1000 * m_Para.XM3 * ((m_Para.ColB4 * m_Para.ListSelectRangePara[0].Data - m_Para.RowK4 * m_Para.ListSelectRangePara[1].Data) /
                                (m_Para.RowK3 * m_Para.ColB4 - m_Para.RowK4 * m_Para.ColB3));
                        m_dY1 = 1000 * m_Para.YM4 * ((m_Para.RowK3 * m_Para.ListSelectRangePara[1].Data - m_Para.ColB3 * m_Para.ListSelectRangePara[0].Data) /
                                (m_Para.RowK3 * m_Para.ColB4 - m_Para.RowK4 * m_Para.ColB3));
                    }

                    // 吸嘴下影像
                    if (m_Para.EnumTypeChecks == EnumTypeCheck.吸嘴下影像)
                    {
                        m_dX1 = 1000 * m_Para.XM5 / (Math.Pow(m_Para.RowK5, 2) + Math.Pow(m_Para.ColB5, 2)) *
                                (m_Para.RowK5 * m_Para.ListSelectRangePara[0].Data + m_Para.ColB5 * m_Para.ListSelectRangePara[1].Data -
                                (m_Para.RowK1 * m_Para.RowK2 + m_Para.ColB1 * m_Para.ColB2) *
                                (m_Para.RowK5 * m_Para.ListSelectRangePara[1].Data - m_Para.ColB5 * m_Para.ListSelectRangePara[0].Data) /
                                (m_Para.RowK1 * m_Para.ColB2 - m_Para.RowK2 * m_Para.ColB1));
                        m_dY1 = 1000 * m_Para.XM5 / (Math.Pow(m_Para.RowK5, 2) + Math.Pow(m_Para.ColB5, 2)) *
                              (m_Para.RowK5 * m_Para.ListSelectRangePara[1].Data - m_Para.ColB5 * m_Para.ListSelectRangePara[0].Data) *
                             (m_Para.YM2 * (m_Para.ColB1 * m_Para.ColB1 + m_Para.ColB2 * m_Para.ColB2)) /
                              (m_Para.XM1 * (m_Para.RowK1 * m_Para.ColB2 - m_Para.RowK2 * m_Para.ColB1));

                        m_dX2 = 1000 * m_Para.XM5 / (Math.Pow(m_Para.RowK5, 2) + Math.Pow(m_Para.ColB5, 2)) *
                               (m_Para.RowK5 * m_Para.ListSelectRangePara[0].Data + m_Para.ColB5 * m_Para.ListSelectRangePara[1].Data -
                               (m_Para.RowK3 * m_Para.RowK4 + m_Para.ColB3 * m_Para.ColB4) *
                               (m_Para.RowK5 * m_Para.ListSelectRangePara[1].Data - m_Para.ColB5 * m_Para.ListSelectRangePara[0].Data) /
                               (m_Para.RowK3 * m_Para.ColB4 - m_Para.RowK4 * m_Para.ColB3));
                        m_dY2 = 1000 * m_Para.XM5 / (Math.Pow(m_Para.RowK5, 2) + Math.Pow(m_Para.ColB5, 2)) *
                              (m_Para.RowK5 * m_Para.ListSelectRangePara[1].Data - m_Para.ColB5 * m_Para.ListSelectRangePara[0].Data) *
                              (m_Para.YM4 * (m_Para.ColB3 * m_Para.ColB3 + m_Para.ColB4 * m_Para.ColB4)) /
                              (m_Para.XM3 * (m_Para.RowK3 * m_Para.ColB4 - m_Para.RowK4 * m_Para.ColB3));

                    }
                }
            }
            catch (HalconException ex)
            {
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
