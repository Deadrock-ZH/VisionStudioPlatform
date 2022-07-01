using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvAngleLineLineTool
{
    /// <summary>
    /// 内 容:本类是角度计算参数类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    [Serializable]
    [XmlRoot("SvAngleLineLineToolPara")]
    public class SvAngleLineLineToolPara
    {
        private double m_dRow1 = 0;

        /// <summary>
        /// 起始行坐标
        /// </summary>
        public double Row1
        {
            get
            {
                return m_dRow1;
            }
            set
            {
                m_dRow1 = value;
            }
        }

        private double m_dCol1 = 0;

        /// <summary>
        /// 起始列坐标
        /// </summary>
        public double Col1
        {
            get
            {
                return m_dCol1;
            }
            set
            {
                m_dCol1 = value;
            }
        }

        private double m_dRow2 = 0;

        /// <summary>
        /// 终止行坐标
        /// </summary>
        public double Row2
        {
            get
            {
                return m_dRow2;
            }
            set
            {
                m_dRow2 = value;
            }
        }

        private double m_dCol2 = 0;

        /// <summary>
        /// 终止列坐标
        /// </summary>
        public double Col2
        {
            get
            {
                return m_dCol2;
            }
            set
            {
                m_dCol2 = value;
            }
        }
        private double m_dRow3= 0;

        /// <summary>
        /// 起始行坐标
        /// </summary>
        public double Row3
        {
            get
            {
                return m_dRow3;
            }
            set
            {
                m_dRow3= value;
            }
        }

        private double m_dCol3= 0;

        /// <summary>
        /// 起始列坐标
        /// </summary>
        public double Col3
        {
            get
            {
                return m_dCol3;
            }
            set
            {
                m_dCol3 = value;
            }
        }

        private double m_dRow4 = 0;

        /// <summary>
        /// 起始行坐标
        /// </summary>
        public double Row4
        {
            get
            {
                return m_dRow4;
            }
            set
            {
                m_dRow4 = value;
            }
        }

        private double m_dCol4 = 0;

        /// <summary>
        /// 起始列坐标
        /// </summary>
        public double Col4
        {
            get
            {
                return m_dCol4;
            }
            set
            {
                m_dCol4 = value;
            }
        }

        public bool m_bPoint1 = true;

        /// <summary>
        /// 点1显示状态
        /// </summary>
        public bool Point1State
        {
            get
            {
                return m_bPoint1;
            }
            set
            {
                m_bPoint1 = value;
            }
        }

        public bool m_bPoint2 = true;

        /// <summary>
        /// 点2显示状态
        /// </summary>
        public bool Point2State
        {
            get
            {
                return m_bPoint2;
            }
            set
            {
                m_bPoint2 = value;
            }
        }

        public bool m_bPoint3 = true;

        /// <summary>
        /// 点3显示状态
        /// </summary>
        public bool Point3State
        {
            get
            {
                return m_bPoint3;
            }
            set
            {
                m_bPoint3= value;
            }
        }

        public bool m_bPoint4= true;

        /// <summary>
        /// 点4显示状态
        /// </summary>
        public bool Point4State
        {
            get
            {
                return m_bPoint4;
            }
            set
            {
                m_bPoint4 = value;
            }
        }

        public bool m_bIntersectionPoint= true;

        /// <summary>
        /// 交点显示状态
        /// </summary>
        public bool IntersectionPoint
        {
            get
            {
                return m_bIntersectionPoint;
            }
            set
            {
                m_bIntersectionPoint = value;
            }
        }

        public bool m_bLine1 = true;

        /// <summary>
        /// 线显示状态
        /// </summary>
        public bool Line1State
        {
            get
            {
                return m_bLine1;
            }
            set
            {
                m_bLine1 = value;
            }
        }

        public bool m_bLine2 = true;

        /// <summary>
        /// 线2显示状态
        /// </summary>
        public bool Line2State
        {
            get
            {
                return m_bLine2;
            }
            set
            {
                m_bLine2 = value;
            }
        }

        private string m_strRow1SelectIndexName = string.Empty;

        /// <summary>
        /// Row1选择索引对应名称
        /// </summary>
        public string Row1SelectIndexName
        {
            get
            {
                return m_strRow1SelectIndexName;
            }
            set
            {
                m_strRow1SelectIndexName = value;
            }
        }

        private string m_strRow2SelectIndexName = string.Empty;

        /// <summary>
        /// Row2选择索引对应名称
        /// </summary>
        public string Row2SelectIndexName
        {
            get
            {
                return m_strRow2SelectIndexName;
            }
            set
            {
                m_strRow2SelectIndexName = value;
            }
        }

        private string m_strCol2SelectIndexName = string.Empty;

        /// <summary>
        /// Col2选择索引对应名称
        /// </summary>
        public string Col2SelectIndexName
        {
            get
            {
                return m_strCol2SelectIndexName;
            }
            set
            {
                m_strCol2SelectIndexName = value;
            }
        }

        private string m_strCol1SelectIndexName = string.Empty;

        /// <summary>
        /// Col1选择索引对应名称
        /// </summary>
        public string Col1SelectIndexName
        {
            get
            {
                return m_strCol1SelectIndexName;
            }
            set
            {
                m_strCol1SelectIndexName = value;
            }
        }

        private string m_strRow3SelectIndexName = string.Empty;

        /// <summary>
        /// Row3选择索引对应名称
        /// </summary>
        public string Row3SelectIndexName
        {
            get
            {
                return m_strRow3SelectIndexName;
            }
            set
            {
                m_strRow3SelectIndexName = value;
            }
        }

        private string m_strRow4SelectIndexName = string.Empty;

        /// <summary>
        /// Row4选择索引对应名称
        /// </summary>
        public string Row4SelectIndexName
        {
            get
            {
                return m_strRow4SelectIndexName;
            }
            set
            {
                m_strRow4SelectIndexName = value;
            }
        }

        private string m_strCol3SelectIndexName = string.Empty;

        /// <summary>
        /// Col3选择索引对应名称
        /// </summary>
        public string Col3SelectIndexName
        {
            get
            {
                return m_strCol3SelectIndexName;
            }
            set
            {
                m_strCol3SelectIndexName = value;
            }
        }

        private string m_strCol4SelectIndexName = string.Empty;

        /// <summary>
        /// Col4选择索引对应名称
        /// </summary>
        public string Col4SelectIndexName
        {
            get
            {
                return m_strCol4SelectIndexName;
            }
            set
            {
                m_strCol4SelectIndexName = value;
            }
        }
    }
}
