using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvDistancePointPointTool
{
    /// <summary>
    /// 内 容:本类是点点距离计算参数类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    [Serializable]
    [XmlRoot("SvDistancePointPointToolPara")]
    public class SvDistancePointPointToolPara
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

        public bool m_bLine = true;

        /// <summary>
        /// 线显示状态
        /// </summary>
        public bool LineState
        {
            get
            {
                return m_bLine;
            }
            set
            {
                m_bLine = value;
            }
        }

        public bool m_bStandardLine = true;

        /// <summary>
        /// 基准线显示状态
        /// </summary>
        public bool StandardLineState
        {
            get
            {
                return m_bStandardLine;
            }
            set
            {
                m_bStandardLine = value;
            }
        }


        public bool m_bMiddlePointState = true;

        /// <summary>
        /// 中点的显示状态
        /// </summary>
        public bool MiddlePointState
        {
            get
            {
                return m_bMiddlePointState;
            }
            set
            {
                m_bMiddlePointState = value;
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
    }
}
