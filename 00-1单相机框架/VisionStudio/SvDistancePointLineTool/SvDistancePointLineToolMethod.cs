using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GVS.HalconDisp.ViewROI.Config;
using HalconDotNet;
using ParaResultAll;

namespace SvDistancePointLineTool
{
    /// <summary>
    /// 内 容:本类是点线距离计算方法类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public class SvDistancePointLineToolMethod : ToolInterFace.ToolInterface
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

        private SvDistancePointLineToolPara m_SvDistancePointLineToolPara = new SvDistancePointLineToolPara();

        /// <summary>
        /// 参数类
        /// </summary>
        public SvDistancePointLineToolPara SvDistancePointLineToolParas
        {
            get
            {
                return m_SvDistancePointLineToolPara;
            }
            set
            {
                m_SvDistancePointLineToolPara = value;
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

        private double m_dRow = 0;

        /// <summary>
        /// 输出垂点行坐标
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

        private double m_dCol = 0;

        /// <summary>
        /// 输出垂点列坐标
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

        private double m_dDistance = 0;

        /// <summary>
        /// 点到直线距离
        /// </summary>
        public double Distance
        {
            get
            {
                return m_dDistance;
            }
            set
            {
                m_dDistance = value;
            }
        }

        private HObject m_hoLine = null;

        /// <summary>
        /// 生成直线
        /// </summary>
        public HObject Line
        {
            get
            {
                return m_hoLine;
            }
            set
            {
                m_hoLine = value;
            }
        }

        private HObject m_hoProjCross = null;

        /// <summary>
        /// 生成垂足轮廓
        /// </summary>
        public HObject ProjCross
        {
            get
            {
                return m_hoProjCross;
            }
            set
            {
                m_hoProjCross = value;
            }
        }

        private HObject m_hvCross1 = null;

        /// <summary>
        /// point1
        /// </summary>
        public HObject Cross1
        {
            get
            {
                return m_hvCross1;
            }
            set
            {
                m_hvCross1 = value;
            }
        }

        private HObject m_hvCross = null;

        /// <summary>
        /// 垂足
        /// </summary>
        public HObject Cross
        {
            get
            {
                return m_hvCross;
            }
            set
            {
                m_hvCross = value;
            }
        }

        private HObject m_hvCross2 = null;

        /// <summary>
        /// point2
        /// </summary>
        public HObject Cross2
        {
            get
            {
                return m_hvCross2;
            }
            set
            {
                m_hvCross2 = value;
            }
        }

        private HObject m_hvCenterLine = null;

        /// <summary>
        /// X轴
        /// </summary>
        public HObject CenterLine
        {
            get
            {
                return m_hvCenterLine;
            }
            set
            {
                m_hvCenterLine = value;
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
            m_hoLine = null;
            m_listReg = new List<HObjectWithColor>();
            m_strRunMsg = string.Empty;
            m_dRunTime = 0;
            m_hvCross1 = null;
            m_hvCross2 = null;
            HTuple hvDistance = null, hvRow = null, hvCol = null;
            HTuple hvSeconds1 = null, hvSeconds2 = null;
            m_dRow = -100;
            m_dCol = -100;
            m_dDistance = -100;
            HOperatorSet.CountSeconds(out hvSeconds1);
            try
            {
                int i = 0;
                string strNameRow = string.Empty;
                string strNameRow1 = string.Empty;
                string strNameRow2 = string.Empty;
                string strNameCol1 = string.Empty;
                string strNameCol2 = string.Empty;
                string strNameCol = string.Empty;
                foreach (ParasResultAll item in m_ListParaResultAll)
                {
                    #region Row1
                    if (m_SvDistancePointLineToolPara.Row1SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Row1SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Row1SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Row1SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }

                    if (m_SvDistancePointLineToolPara.Row1SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvDistancePointLineToolPara.Row1SelectIndexName))
                        {
                            strNameRow1 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    #endregion

                    #region row2
                    if (m_SvDistancePointLineToolPara.Row2SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Row2SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Row2SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Row2SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                            {
                                strNameRow2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + " item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                            {
                                strNameRow2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                            {
                                strNameRow2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Row2SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvDistancePointLineToolPara.Row2SelectIndexName))
                        {
                            strNameRow2 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    #endregion

                    #region Col1
                    if (m_SvDistancePointLineToolPara.Col1SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Col1SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Col1SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Col1SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Col1SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvDistancePointLineToolPara.Col1SelectIndexName))
                        {
                            strNameCol1 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    #endregion

                    #region Col2
                    if (m_SvDistancePointLineToolPara.Col2SelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Col2SelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Col2SelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Col2SelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                            {
                                strNameCol2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                            {
                                strNameCol2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                            {
                                strNameCol2 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }
                    if (m_SvDistancePointLineToolPara.Col2SelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvDistancePointLineToolPara.Col2SelectIndexName))
                        {
                            strNameCol2 = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    #endregion

                    #region Col
                    if (m_SvDistancePointLineToolPara.ColSelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.ColSelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.ColSelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.ColSelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                            {
                                strNameCol = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                            {
                                strNameCol = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                            {
                                strNameCol = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }
                    if (m_SvDistancePointLineToolPara.ColSelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvDistancePointLineToolPara.ColSelectIndexName))
                        {
                            strNameCol = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    #endregion

                    #region Row
                    if (m_SvDistancePointLineToolPara.RowSelectIndexName.Contains("匹配工具" + i))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                        }
                        if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.RowSelectIndexName.Contains("找线工具" + i))
                    {
                        if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                        }
                        if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.RowSelectIndexName.Contains("找圆工具" + i))
                    {
                        if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                        }
                        if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                        }
                    }
                    if (m_SvDistancePointLineToolPara.RowSelectIndexName.Contains("Blob工具" + i))
                    {
                        for (int j = 0; j < item.BlobResultPara.ListBlobResultPara.Count; j++)
                        {
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                            {
                                strNameRow = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Row_" + item.BlobResultPara.ListBlobResultPara[j].Row;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                            {
                                strNameRow = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Col_" + item.BlobResultPara.ListBlobResultPara[j].Col;
                            }
                            if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                            {
                                strNameRow = item.ToolName + "item.BlobResultPara.ListBlobResultPara[i].Area_" + item.BlobResultPara.ListBlobResultPara[j].Area;
                            }
                        }
                    }
                    if (m_SvDistancePointLineToolPara.RowSelectIndexName.Contains("线线角度工具" + i))
                    {
                        if ((item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.ParaParaAngleLL.Row_" + item.ParaParaAngleLL.Row;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.ParaParaAngleLL.Col_" + item.ParaParaAngleLL.Col;
                        }
                        if ((item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle).Contains(m_SvDistancePointLineToolPara.RowSelectIndexName))
                        {
                            strNameRow = item.ToolName + "item.ParaParaAngleLL.Angle_" + item.ParaParaAngleLL.Angle;
                        }
                    }
                    #endregion

                    i++;
                }
                if (strNameRow1.Length > 0 && strNameRow2.Length > 0 && strNameCol1.Length > 0 && strNameCol2.Length > 0 && strNameRow.Length > 0 && strNameCol.Length > 0)
                {
                    // 将链接的数据赋予输入参数
                    m_SvDistancePointLineToolPara.Row1 = double.Parse(strNameRow1.Substring(strNameRow1.LastIndexOf('_') + 1,
                                                         (strNameRow1.Length - (strNameRow1.LastIndexOf('_') + 1))), NumberStyles.Any);
                    m_SvDistancePointLineToolPara.Row2 = double.Parse(strNameRow2.Substring(strNameRow2.LastIndexOf('_') + 1,
                                                         (strNameRow2.Length - (strNameRow2.LastIndexOf('_') + 1))), NumberStyles.Any);
                    m_SvDistancePointLineToolPara.Col1 = double.Parse(strNameCol1.Substring(strNameCol1.LastIndexOf('_') + 1,
                                                         (strNameCol1.Length - (strNameCol1.LastIndexOf('_') + 1))), NumberStyles.Any);
                    m_SvDistancePointLineToolPara.Col2 = double.Parse(strNameCol2.Substring(strNameCol2.LastIndexOf('_') + 1,
                                                         (strNameCol2.Length - (strNameCol2.LastIndexOf('_') + 1))), NumberStyles.Any);
                    m_SvDistancePointLineToolPara.Row = double.Parse(strNameRow.Substring(strNameRow.LastIndexOf('_') + 1,
                                                        (strNameRow.Length - (strNameRow.LastIndexOf('_') + 1))), NumberStyles.Any);
                    m_SvDistancePointLineToolPara.Col = double.Parse(strNameCol.Substring(strNameCol.LastIndexOf('_') + 1,
                                                        (strNameCol.Length - (strNameCol.LastIndexOf('_') + 1))), NumberStyles.Any);
                }
                else
                {
                    m_dRow = -100;
                    m_dCol = -100;
                    m_dDistance = -100;
                    m_hvCross = null;
                    m_hvCross1 = null;
                    m_hvCross2 = null;
                    m_hoLine = null;
                    m_listReg = new List<HObjectWithColor>();
                    bState = false;
                    HOperatorSet.CountSeconds(out hvSeconds2);
                    m_dRunTime = (hvSeconds2[0].D - hvSeconds1[0].D) * 1000;
                    m_strRunMsg = "点线距离模块数据链接错误";
                    return bState;
                }

                HOperatorSet.GenCrossContourXld(out m_hvCross, m_SvDistancePointLineToolPara.Row, m_SvDistancePointLineToolPara.Col, 18, 1);
                HOperatorSet.GenCrossContourXld(out m_hvCross1, m_SvDistancePointLineToolPara.Row1, m_SvDistancePointLineToolPara.Col1, 18, 1);
                HOperatorSet.GenCrossContourXld(out m_hvCross2, m_SvDistancePointLineToolPara.Row2, m_SvDistancePointLineToolPara.Col2, 18, 1);
                HOperatorSet.GenContourPolygonXld(out m_hoLine, new HTuple(m_SvDistancePointLineToolPara.Row1).TupleConcat(m_SvDistancePointLineToolPara.Row2),
                                                  new HTuple(m_SvDistancePointLineToolPara.Col1).TupleConcat(m_SvDistancePointLineToolPara.Col2));
                HOperatorSet.ProjectionPl(m_SvDistancePointLineToolPara.Row, m_SvDistancePointLineToolPara.Col,
                                        m_SvDistancePointLineToolPara.Row1, m_SvDistancePointLineToolPara.Col1,
                                        m_SvDistancePointLineToolPara.Row2, m_SvDistancePointLineToolPara.Col2,
                                        out hvRow, out hvCol);
                HOperatorSet.DistancePl(m_SvDistancePointLineToolPara.Row, m_SvDistancePointLineToolPara.Col,
                                        m_SvDistancePointLineToolPara.Row1, m_SvDistancePointLineToolPara.Col1,
                                        m_SvDistancePointLineToolPara.Row2, m_SvDistancePointLineToolPara.Col2,
                                        out hvDistance);
                HOperatorSet.GenContourPolygonXld(out m_hvCenterLine,
                                                 new HTuple((m_SvDistancePointLineToolPara.Row1 + m_SvDistancePointLineToolPara.Row2) / 2).
                                                 TupleConcat((m_SvDistancePointLineToolPara.Row1 + m_SvDistancePointLineToolPara.Row2) / 2),
                                                 new HTuple((m_SvDistancePointLineToolPara.Col1 + m_SvDistancePointLineToolPara.Row2) / 2 - 1000).
                                                 TupleConcat((m_SvDistancePointLineToolPara.Row1 + m_SvDistancePointLineToolPara.Col2) / 2 + 1000));
                if (hvRow != null && hvDistance != null && hvDistance.Length > 0)
                {
                    m_dRow = hvRow[0].D;
                    m_dCol = hvCol[0].D;
                    m_dDistance = hvDistance[0].D;
                    HOperatorSet.GenCrossContourXld(out m_hoProjCross, m_dRow, m_dCol, 18, 1);
                    m_listReg.Add(new HObjectWithColor(m_hoLine, "red"));
                    m_listReg.Add(new HObjectWithColor(m_hvCross1, "red"));
                    m_listReg.Add(new HObjectWithColor(m_hvCross2, "red"));
                    m_listReg.Add(new HObjectWithColor(m_hvCenterLine, "green"));
                    m_listReg.Add(new HObjectWithColor(m_hvCross, "red"));
                    m_listReg.Add(new HObjectWithColor(m_hoProjCross, "green"));
                }
            }
            catch (HalconException ex)
            {
                m_dRow = -100;
                m_dCol = -100;
                m_dDistance = -100;
                m_hvCross = null;
                m_hvCross1 = null;
                m_hvCross2 = null;
                m_hoLine = null;
                m_listReg = new List<HObjectWithColor>();
                bState = false;
                HOperatorSet.CountSeconds(out hvSeconds2);
                m_dRunTime = (hvSeconds2[0].D - hvSeconds1[0].D) * 1000;
                m_strRunMsg = "点线距离模块Catch到" + ex.ToString();
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
