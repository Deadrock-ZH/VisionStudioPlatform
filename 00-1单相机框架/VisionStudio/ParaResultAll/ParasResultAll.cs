using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace ParaResultAll
{
    #region 结果参数类
    /// <summary>
    /// PatMax结果类
    /// </summary>
    public class ParaPatMaxResult
    {
        private double m_dRow = -100;

        /// <summary>
        /// 行坐标
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
        /// 列坐标
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

        private double m_dAngle = -100;

        /// <summary>
        /// 角度
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

        private HTuple m_hvAffinehom = null;

        /// <summary>
        /// 仿射矩阵
        /// </summary>
        public HTuple Affinehom
        {
            get
            {
                return m_hvAffinehom;
            }
            set
            {
                m_hvAffinehom = value;
            }
        }
    }

    /// <summary>
    /// FindLine结果类
    /// </summary>
    public class ParaFindLineResult
    {
        private double m_dRowStart = -100;

        /// <summary>
        /// 起始行坐标
        /// </summary>
        public double RowStart
        {
            get
            {
                return m_dRowStart;
            }
            set
            {
                m_dRowStart = value;
            }
        }

        private double m_dColStart = -100;

        /// <summary>
        /// 起始列坐标
        /// </summary>
        public double ColStart
        {
            get
            {
                return m_dColStart;
            }
            set
            {
                m_dColStart = value;
            }
        }

        private double m_dRowEnd = -100;

        /// <summary>
        /// 终点行坐标
        /// </summary>
        public double RowEnd
        {
            get
            {
                return m_dRowEnd;
            }
            set
            {
                m_dRowEnd = value;
            }
        }

        private double m_dColEnd = -100;

        /// <summary>
        /// 终点列坐标
        /// </summary>
        public double ColEnd
        {
            get
            {
                return m_dColEnd;
            }
            set
            {
                m_dColEnd = value;
            }
        }


        private double m_dRowMiddle = -100;

        /// <summary>
        /// 中点行坐标
        /// </summary>
        public double RowMiddle
        {
            get
            {
                return m_dRowMiddle;
            }
            set
            {
                m_dRowMiddle = value;
            }
        }

        private double m_dColMiddle = -100;

        /// <summary>
        /// 中点列坐标
        /// </summary>
        public double ColMiddle
        {
            get
            {
                return m_dColMiddle;
            }
            set
            {
                m_dColMiddle = value;
            }
        }
    }

    /// <summary>
    /// FindCircle结果类
    /// </summary>
    public class ParaFindCircleResult
    {
        private double m_dRow = -100;

        /// <summary>
        /// 行坐标
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
        /// 列坐标
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

        private double m_dRadius = -100;

        /// <summary>
        /// 终点行坐标
        /// </summary>
        public double Radius
        {
            get
            {
                return m_dRadius;
            }
            set
            {
                m_dRadius = value;
            }
        }
    }

    /// <summary>
    /// 角度计算结果类
    /// </summary>
    public class ParaAngleLXResult
    {
        private double m_Angle = -100;

        /// <summary>
        /// 角度输出
        /// </summary>
        public double Angle
        {
            get
            {
                return m_Angle;
            }
            set
            {
                m_Angle = value;
            }
        }
    }

    /// <summary>
    /// 线线交点，角度计算
    /// </summary>
    public class ParaAngleLLResult
    {
        private double m_Angle = -100;

        /// <summary>
        /// 角度输出
        /// </summary>
        public double Angle
        {
            get
            {
                return m_Angle;
            }
            set
            {
                m_Angle = value;
            }
        }

        private double m_Row = -100;

        /// <summary>
        /// 交点行坐标输出
        /// </summary>
        public double Row
        {
            get
            {
                return m_Row;
            }
            set
            {
                m_Row = value;
            }
        }

        private double m_Col = -100;

        /// <summary>
        /// 交点列坐标输出
        /// </summary>
        public double Col
        {
            get
            {
                return m_Col;
            }
            set
            {
                m_Col = value;
            }
        }
    }

    /// <summary>
    /// blob结果参数类
    /// </summary>
    public class ParaBlobResult
    {
        List<SvBlobTool.ParaResult> m_ListBlobResultPara = new List<SvBlobTool.ParaResult>();

        /// <summary>
        /// Blob结果类
        /// </summary>
        public List<SvBlobTool.ParaResult> ListBlobResultPara
        {
            get
            {
                return m_ListBlobResultPara;
            }
            set
            {
                m_ListBlobResultPara = value;
            }
        }

    }

    /// <summary>
    /// 点点距离结果参数类
    /// </summary>
    public class ParaDistancePointPointResult
    {
        private double m_dDistance = -100;

        /// <summary>
        /// 点点距离
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

        private double m_dRow= -100;

        /// <summary>
        /// 点点中心行坐标
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

        private double m_dCol= -100;

        /// <summary>
        /// 点点中心列坐标
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
    }

    /// <summary>
    /// 点线距离结果类
    /// </summary>
    public class ParaDistancePointLine
    {
        private double m_dRow = -100;

        /// <summary>
        /// 点行坐标
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
        /// 点列坐标
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

        private double m_dDistance = -100;

        /// <summary>
        /// 点到直线的距离
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
    }

    /// <summary>
    /// 示教结果类
    /// </summary>
    public class ParaVisualCorrection
    {
        private double m_dRX1 = -100;

        /// <summary>
        /// X1
        /// </summary>
        public double X1
        {
            get
            {
                return m_dRX1;
            }
            set
            {
                m_dRX1 = value;
            }
        }

        private double m_dY1 = -100;

        /// <summary>
        /// Y1
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
        /// X2
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
        /// Y2
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
    }


    /// <summary>
    /// 内 容:本类是总结果参数类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public class ParasResultAll
    {
        private string m_strToolName = string.Empty;

        /// <summary>
        /// 工具名称
        /// </summary>
        public string ToolName
        {
            get
            {
                return m_strToolName;
            }
            set
            {
                m_strToolName = value;
            }
        }


        ParaBlobResult m_BlobResultPara = new ParaBlobResult();

        /// <summary>
        /// Blob结果类
        /// </summary>
        public ParaBlobResult BlobResultPara
        {
            get
            {
                return m_BlobResultPara;
            }
            set
            {
                m_BlobResultPara = value;
            }
        }

        ParaFindCircleResult m_FindCircleResultPara = new ParaFindCircleResult();

        /// <summary>
        /// FindCircle结果类
        /// </summary>
        public ParaFindCircleResult FindCircleResultPara
        {
            get
            {
                return m_FindCircleResultPara;
            }
            set
            {
                m_FindCircleResultPara = value;
            }
        }

        ParaFindLineResult m_FindLineResultPara = new ParaFindLineResult();

        /// <summary>
        /// FindLine结果类
        /// </summary>
        public ParaFindLineResult FindLineResultPara
        {
            get
            {
                return m_FindLineResultPara;
            }
            set
            {
                m_FindLineResultPara = value;
            }
        }

        ParaAngleLXResult m_ParaAngleLXResult = new ParaAngleLXResult();

        /// <summary>
        /// 角度计算结果类
        /// </summary>
        public ParaAngleLXResult ParasAngleLXResult
        {
            get
            {
                return m_ParaAngleLXResult;
            }
            set
            {
                m_ParaAngleLXResult = value;
            }
        }

        private ParaVisualCorrection m_ParaVisualCorrection = new ParaVisualCorrection();

        /// <summary>
        /// 示教工具结果类
        /// </summary>
        public ParaVisualCorrection ParasVisualCorrection
        {
            get
            {
                return m_ParaVisualCorrection;
            }
            set
            {
                m_ParaVisualCorrection = value;
            }
        }

        ParaPatMaxResult m_PatMaxResultPara = new ParaPatMaxResult();

        /// <summary>
        /// ParaPatMaxResult结果类
        /// </summary>
        public ParaPatMaxResult PatMaxResultPara
        {
            get
            {
                return m_PatMaxResultPara;
            }
            set
            {
                m_PatMaxResultPara = value;
            }
        }

        ParaDistancePointLine m_ParaDistancePointLine = new ParaDistancePointLine();

        /// <summary>
        /// ParaDistancePointLine结果类
        /// </summary>
        public ParaDistancePointLine ParaDistancePointLines
        {
            get
            {
                return m_ParaDistancePointLine;
            }
            set
            {
                m_ParaDistancePointLine = value;
            }
        }

        private ParaDistancePointPointResult m_ParaDistancePointPointResult = new ParaDistancePointPointResult();

        /// <summary>
        /// 点点距离计算结果类
        /// </summary>
        public ParaDistancePointPointResult ParaDistancePointPoints
        {
            get
            {
                return m_ParaDistancePointPointResult;
            }
            set
            {
                m_ParaDistancePointPointResult = value;
            }
        }

        private ParaAngleLLResult m_ParaAngleLLResult = new ParaAngleLLResult();

        /// <summary>
        /// 线线角度计算，交点计算
        /// </summary>
        public ParaAngleLLResult ParaParaAngleLL
        {
            get
            {
                return m_ParaAngleLLResult;
            }
            set
            {
                m_ParaAngleLLResult = value;
            }
        }
    }
    #endregion
}
