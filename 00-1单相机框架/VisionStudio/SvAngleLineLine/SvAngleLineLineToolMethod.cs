using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GVS.HalconDisp.ViewROI.Config;
using HalconDotNet;
using ParaResultAll;

namespace SvAngleLineLineTool
{
    /// <summary>
    /// 内 容:本类是角度计算方法类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public class SvAngleLineLineToolMethod : ToolInterFace.ToolInterface
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

        private SvAngleLineLineToolPara m_SvAngleLlPara = new SvAngleLineLineToolPara();

        /// <summary>
        /// 参数类
        /// </summary>
        public SvAngleLineLineToolPara Para
        {
            get
            {
                return m_SvAngleLlPara;
            }
            set
            {
                m_SvAngleLlPara = value;
            }
        }

        private HObject m_hoImage = null;

        /// <summary>
        ///  输入图像
        /// </summary>
        public HObject InputImage
        {
            get
            {
                return m_hoImage;
            }
            set
            {
                m_hoImage = value;
            }
        }

        private List<HObjectWithColor> m_listReg = new List<HObjectWithColor>();

        /// <summary>
        /// 输出区域信息
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
        /// 模块名
        /// </summary>
        public string ModualFormName
        {
            get;
            set;
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

        private double m_dRunTime = 0;

        /// <summary>
        /// 运行时间
        /// </summary>
        public double RunTime
        {
            get
            {
                return m_dRunTime;
            }
            set
            {
                m_dRunTime = value;
            }
        }

        private double m_dAngle = -100;

        /// <summary>
        /// 输出角度
        /// </summary>
        public double Angle
        {
            get
            {
                return m_dAngle;
            }
            set
            {
                m_dAngle = value;
            }
        }

        private double m_dRow = -100;

        /// <summary>
        /// 输出交点行坐标
        /// </summary>
        public double Row
        {
            get
            {
                return m_dRow;
            }
            set
            {
                m_dRow = value;
            }
        }

        private double m_dCol = -100;

        /// <summary>
        /// 输出交点列坐标
        /// </summary>
        public double Col
        {
            get
            {
                return m_dCol;
            }
            set
            {
                m_dCol = value;
            }
        }

        private HObject m_hoLine1 = null;

        /// <summary>
        /// 生成直线1
        /// </summary>
        public HObject Line1
        {
            get
            {
                return m_hoLine1;
            }
            set
            {
                m_hoLine1 = value;
            }
        }

        private HObject m_hoCrossIntersection = null;

        /// <summary>
        /// 线线交点
        /// </summary>
        public HObject Crossintersection
        {
            get
            {
                return m_hoCrossIntersection;
            }
            set
            {
                m_hoCrossIntersection = value;
            }
        }

        private HObject m_hoCross1 = null;

        /// <summary>
        /// point1
        /// </summary>
        public HObject Cross1
        {
            get
            {
                return m_hoCross1;
            }
            set
            {
                m_hoCross1 = value;
            }
        }

        private HObject m_hoCross2 = null;

        /// <summary>
        /// point2
        /// </summary>
        public HObject Cross2
        {
            get
            {
                return m_hoCross2;
            }
            set
            {
                m_hoCross2 = value;
            }
        }

        private HObject m_hoCross3 = null;

        /// <summary>
        /// point3
        /// </summary>
        public HObject Cross3
        {
            get
            {
                return m_hoCross3;
            }
            set
            {
                m_hoCross3 = value;
            }
        }

        private HObject m_hoCross4 = null;

        /// <summary>
        /// point4
        /// </summary>
        public HObject Cross4
        {
            get
            {
                return m_hoCross4;
            }
            set
            {
                m_hoCross4 = value;
            }
        }


        private HObject m_hoLine2 = null;

        /// <summary>
        /// Line2
        /// </summary>
        public HObject Line2
        {
            get
            {
                return m_hoLine2;
            }
            set
            {
                m_hoLine2 = value;
            }
        }

        public object Load(Type type, string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 运行方法
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            bool bState = true;
            m_hoLine1 = null;
            m_dAngle = -100;
            m_listReg = new List<HObjectWithColor>();
            m_strRunMsg = string.Empty;
            m_dRunTime = 0;
            m_hoCross1 = null;
            m_hoCross2 = null;
            HTuple hvAngle = null, hvDeg = null;
            HTuple hvRow = null, hvCol = null, hvIsOverlap = null;
            HTuple hvSeconds1 = null, hvSeconds2 = null;
            m_dRow = -100;
            m_dCol = -100;
            HOperatorSet.CountSeconds(out hvSeconds1);
            try
            {
                int i = 0;
                string strNameRow1 = string.Empty;
                string strNameRow2 = string.Empty;
                string strNameCol1 = string.Empty;
                string strNameCol2 = string.Empty;

                string strNameRow3 = string.Empty;
                string strNameRow4 = string.Empty;
                string strNameCol3 = string.Empty;
                string strNameCol4 = string.Empty;
                foreach (ParasResultAll item in m_ListParaResultAll)
                {
                    #region Row1
                    if (m_SvAngleLlPara.Row1SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Row1SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvAngleLlPara.Row1SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvAngleLlPara.Row1SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }

                    if (m_SvAngleLlPara.Row1SelectIndexName.Contains("点线距离工具" + i))
                    {
                        if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                        }
                    }
                    if (m_SvAngleLlPara.Row1SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvAngleLlPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    #endregion

                    #region row2
                    if (m_SvAngleLlPara.Row2SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Row2SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Row2SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvAngleLlPara.Row2SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvAngleLlPara.Row2SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                            {
                                strNameRow2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + " item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                            {
                                strNameRow2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                            {
                                strNameRow2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }

                    if (m_SvAngleLlPara.Row2SelectIndexName.Contains("点线距离工具" + i))
                    {
                        if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_SvAngleLlPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                        }
                    }
                    #endregion

                    #region Col1
                    if (m_SvAngleLlPara.Col1SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Col1SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Col1SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvAngleLlPara.Col1SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvAngleLlPara.Col1SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }

                    if (m_SvAngleLlPara.Col1SelectIndexName.Contains("点线距离工具" + i))
                    {
                        if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_SvAngleLlPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                        }
                    }
                    #endregion

                    #region Col2
                    if (m_SvAngleLlPara.Col2SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Col2SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Col2SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvAngleLlPara.Col2SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvAngleLlPara.Col2SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                            {
                                strNameCol2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                            {
                                strNameCol2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                            {
                                strNameCol2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }

                    if (m_SvAngleLlPara.Col2SelectIndexName.Contains("点线距离工具" + i))
                    {
                        if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_SvAngleLlPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                        }
                    }
                    #endregion

                    #region Row3
                    if (m_SvAngleLlPara.Row3SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Row3SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Row3SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvAngleLlPara.Row3SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvAngleLlPara.Row3SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                            {
                                strNameRow3 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                            {
                                strNameRow3 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                            {
                                strNameRow3 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }

                    if (m_SvAngleLlPara.Row3SelectIndexName.Contains("点线距离工具" + i))
                    {
                        if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_SvAngleLlPara.Row3SelectIndexName))
                        {
                            strNameRow3 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                        }
                    }
                    #endregion

                    #region Row4
                    if (m_SvAngleLlPara.Row4SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Row4SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Row4SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvAngleLlPara.Row4SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvAngleLlPara.Row4SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                            {
                                strNameRow4 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                            {
                                strNameRow4 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                            {
                                strNameRow4 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }

                    if (m_SvAngleLlPara.Row4SelectIndexName.Contains("点线距离工具" + i))
                    {
                        if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_SvAngleLlPara.Row4SelectIndexName))
                        {
                            strNameRow4 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                        }
                    }
                    #endregion

                    #region Col3
                    if (m_SvAngleLlPara.Col3SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Col3SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Col3SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvAngleLlPara.Col3SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvAngleLlPara.Col3SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                            {
                                strNameCol3 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                            {
                                strNameCol3 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                            {
                                strNameCol3 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }

                    if (m_SvAngleLlPara.Col3SelectIndexName.Contains("点线距离工具" + i))
                    {
                        if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_SvAngleLlPara.Col3SelectIndexName))
                        {
                            strNameCol3 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                        }
                    }
                    #endregion

                    #region Col4
                    if (m_SvAngleLlPara.Col4SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Col4SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    if (m_SvAngleLlPara.Col4SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvAngleLlPara.Col4SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvAngleLlPara.Col4SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                            {
                                strNameCol4 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                            {
                                strNameCol4 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                            {
                                strNameCol4 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }

                    if (m_SvAngleLlPara.Col4SelectIndexName.Contains("点线距离工具" + i))
                    {
                        if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                        }
                        if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(m_SvAngleLlPara.Col4SelectIndexName))
                        {
                            strNameCol4 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                        }
                    }
                    #endregion

                    i++;
                }

                if (strNameRow1.Length > 0 && strNameRow2.Length > 0 && strNameCol1.Length > 0 && strNameCol2.Length > 0
                    && strNameRow3.Length > 0 && strNameRow4.Length > 0 && strNameCol3.Length > 0 && strNameCol4.Length > 0)
                {
                    // 将链接的数据赋予输入参数
                    m_SvAngleLlPara.Row1 = double.Parse(strNameRow1.Substring(strNameRow1.LastIndexOf('_') + 1, (strNameRow1.Length - (strNameRow1.LastIndexOf('_') + 1))), NumberStyles.Any);
                    m_SvAngleLlPara.Row2 = double.Parse(strNameRow2.Substring(strNameRow2.LastIndexOf('_') + 1, (strNameRow2.Length - (strNameRow2.LastIndexOf('_') + 1))), NumberStyles.Any);

                    m_SvAngleLlPara.Col1 = double.Parse(strNameCol1.Substring(strNameCol1.LastIndexOf('_') + 1, (strNameCol1.Length - (strNameCol1.LastIndexOf('_') + 1))), NumberStyles.Any);
                    m_SvAngleLlPara.Col2 = double.Parse(strNameCol2.Substring(strNameCol2.LastIndexOf('_') + 1, (strNameCol2.Length - (strNameCol2.LastIndexOf('_') + 1))), NumberStyles.Any);

                    // 将链接的数据赋予输入参数
                    m_SvAngleLlPara.Row3 = double.Parse(strNameRow3.Substring(strNameRow3.LastIndexOf('_') + 1, (strNameRow3.Length - (strNameRow3.LastIndexOf('_') + 1))), NumberStyles.Any);
                    m_SvAngleLlPara.Row4 = double.Parse(strNameRow4.Substring(strNameRow4.LastIndexOf('_') + 1, (strNameRow4.Length - (strNameRow4.LastIndexOf('_') + 1))), NumberStyles.Any);

                    m_SvAngleLlPara.Col3 = double.Parse(strNameCol3.Substring(strNameCol3.LastIndexOf('_') + 1, (strNameCol3.Length - (strNameCol3.LastIndexOf('_') + 1))), NumberStyles.Any);
                    m_SvAngleLlPara.Col4 = double.Parse(strNameCol4.Substring(strNameCol4.LastIndexOf('_') + 1, (strNameCol4.Length - (strNameCol4.LastIndexOf('_') + 1))), NumberStyles.Any);
                }
                else
                {
                    m_hoCross1 = null;
                    m_hoCross2 = null;
                    m_hoLine1 = null;
                    m_dAngle = 0;
                    m_listReg = new List<HObjectWithColor>();
                    bState = false;
                    HOperatorSet.CountSeconds(out hvSeconds2);
                    m_dRunTime = (hvSeconds2[0].D - hvSeconds1[0].D) * 1000;
                    m_strRunMsg = "线线角度计算模块数据连接为空";
                    return bState;
                }
                HOperatorSet.GenCrossContourXld(out m_hoCross1, m_SvAngleLlPara.Row1, m_SvAngleLlPara.Col1, 11, 1);
                HOperatorSet.GenCrossContourXld(out m_hoCross2, m_SvAngleLlPara.Row2, m_SvAngleLlPara.Col2, 11, 1);
                HOperatorSet.GenCrossContourXld(out m_hoCross3, m_SvAngleLlPara.Row3, m_SvAngleLlPara.Col3, 11, 1);
                HOperatorSet.GenCrossContourXld(out m_hoCross4, m_SvAngleLlPara.Row4, m_SvAngleLlPara.Col4, 11, 1);
                HOperatorSet.GenContourPolygonXld(out m_hoLine1, new HTuple(m_SvAngleLlPara.Row1).TupleConcat(m_SvAngleLlPara.Row2),
                                                  new HTuple(m_SvAngleLlPara.Col1).TupleConcat(m_SvAngleLlPara.Col2));
                HOperatorSet.IntersectionLines(m_SvAngleLlPara.Row1, m_SvAngleLlPara.Col1, m_SvAngleLlPara.Row2, m_SvAngleLlPara.Col2,
                                               m_SvAngleLlPara.Row3, m_SvAngleLlPara.Col3, m_SvAngleLlPara.Row4, m_SvAngleLlPara.Col4,
                                               out hvRow, out hvCol, out hvIsOverlap);
                HOperatorSet.AngleLl(m_SvAngleLlPara.Row1, m_SvAngleLlPara.Col1, m_SvAngleLlPara.Row2, m_SvAngleLlPara.Col2,
                                     m_SvAngleLlPara.Row3, m_SvAngleLlPara.Col3, m_SvAngleLlPara.Row4, m_SvAngleLlPara.Col4, out hvAngle);
                HOperatorSet.GenContourPolygonXld(out m_hoLine2, new HTuple(m_SvAngleLlPara.Row3).TupleConcat(m_SvAngleLlPara.Row4),
                                                 new HTuple(m_SvAngleLlPara.Col3).TupleConcat(m_SvAngleLlPara.Col4));
                if (hvAngle != null && hvAngle.Length > 0)
                {
                    HOperatorSet.TupleDeg(hvAngle, out hvDeg);
                    m_dAngle = hvDeg[0].D;
                    m_listReg.Add(new HObjectWithColor(m_hoLine1, "green"));
                    m_listReg.Add(new HObjectWithColor(m_hoCross1, "green"));
                    m_listReg.Add(new HObjectWithColor(m_hoCross2, "green"));
                    m_listReg.Add(new HObjectWithColor(m_hoLine2, "green"));
                    m_listReg.Add(new HObjectWithColor(m_hoCross3, "green"));
                    m_listReg.Add(new HObjectWithColor(m_hoCross4, "green"));
                }
                if (hvRow != null && hvRow.Length > 0)
                {
                    HOperatorSet.GenCrossContourXld(out m_hoCrossIntersection, hvRow[0].D, hvCol[0].D, 11, 1);
                    m_dRow = hvRow[0].D;
                    m_dCol = hvCol[0].D;
                    m_listReg.Add(new HObjectWithColor(m_hoCrossIntersection, "green"));
                }
            }
            catch (Exception ex)
            {
                m_hoCrossIntersection = null;
                m_hoCross1 = null;
                m_hoCross2 = null;
                m_hoCross3 = null;
                m_hoCross4 = null;
                m_hoLine1 = null;
                m_hoLine2 = null;
                m_dRow = -100;
                m_dAngle = -100;
                m_dCol = -100;
                m_listReg = new List<HObjectWithColor>();
                bState = false;
                HOperatorSet.CountSeconds(out hvSeconds2);
                m_dRunTime = (hvSeconds2[0].D - hvSeconds1[0].D) * 1000;
                m_strRunMsg = "AngleLX模块Catch到" + ex.ToString();
                return bState;
            }
            HOperatorSet.CountSeconds(out hvSeconds2);
            m_dRunTime = (hvSeconds2[0].D - hvSeconds1[0].D) * 1000;
            return bState;
        }

        public bool Save(object obj, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
