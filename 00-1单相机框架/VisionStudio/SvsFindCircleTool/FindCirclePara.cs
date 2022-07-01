using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvsFindCircleTool
{
    /// <summary>
    /// 主要内容：本类是找圆的参数类
    /// 时    间：2019/9/29
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    [XmlRoot("FindCirclePara")]
    public class FindCirclePara
    {
        private string m_strAffinehomSelectIndexName = "null";

        /// <summary>
        /// 仿射矩阵选择索引对应名称
        /// </summary>
        public string AffineHomSelectIndexName
        {
            get
            {
                return m_strAffinehomSelectIndexName;
            }
            set
            {
                m_strAffinehomSelectIndexName = value;
            }
        }

        private string m_strRowSelectIndexName = "null";

        /// <summary>
        /// Row选择索引对应名称
        /// </summary>
        public string RowSelectIndexName
        {
            get
            {
                return m_strRowSelectIndexName;
            }
            set
            {
                m_strRowSelectIndexName = value;
            }
        }

        private string m_strColSelectIndexName = "null";

        /// <summary>
        /// Col选择索引对应名称
        /// </summary>
        public string ColSelectIndexName
        {
            get
            {
                return m_strColSelectIndexName;
            }
            set
            {
                m_strColSelectIndexName = value;
            }
        }

        private string m_strRadiusSelectIndexName = "null";

        /// <summary>
        /// 直径选择索引对应名称
        /// </summary>
        public string  RadiusSelectIndexName
        {
            get
            {
                return m_strRadiusSelectIndexName;
            }
            set
            {
                m_strRadiusSelectIndexName = value;
            }
        }

        private GVS.HalconDisp.ViewWindow.Config.CircleCalliper m_circleCalliper = new GVS.HalconDisp.ViewWindow.Config.CircleCalliper();

        /// <summary>
        /// 圆卡尺
        /// </summary>
        public GVS.HalconDisp.ViewWindow.Config.CircleCalliper CircleCalliper
        {
            get
            {
                return this.m_circleCalliper;
            }
            set
            {
                m_circleCalliper = value;
            }
        }

        private int m_iContrast = 20;

        /// <summary>
        /// 灰度值
        /// </summary>
        public int Contrast
        {
            get
            {
                return this.m_iContrast;
            }
            set
            {
                m_iContrast = value;
            }
        }

        private string m_strPolarity = "all";

        /// <summary>
        /// 极性
        /// </summary>
        public string Polarity
        {
            get
            {
                return this.m_strPolarity;
            }
            set
            {
                m_strPolarity = value;
            }
        }

        private string m_strSelect = "max";

        /// <summary>
        /// 选择方向
        /// </summary>
        public string Select
        {
            get
            {
                return this.m_strSelect;
            }
            set
            {
                this.m_strSelect = value;
            }
        }

        private bool m_bCircle = true;

        /// <summary>
        /// 圆check
        /// </summary>
        public bool Circle
        {
            get
            {
                return this.m_bCircle;
            }
            set
            {
                this.m_bCircle = value;
            }
        }

        private bool m_bCirclePoint = true;

        /// <summary>
        /// 点check
        /// </summary>
        public bool CirclePoint
        {
            get
            {
                return this.m_bCirclePoint;
            }
            set
            {
                this.m_bCirclePoint = value;
            }
        }

        private double m_VisionRadius = 2.0000;

        /// <summary>
        /// 标准块半径mm
        /// </summary>
        public double VisionRadius
        {
            get
            {
                return this.m_VisionRadius;
            }
            set
            {
                m_VisionRadius = value;
            }
        }
       
        private double m_dKValue = 1;

        /// <summary>
        /// K值
        /// </summary>
        public double KValue
        {
            get
            {
                return this.m_dKValue;
            }
            set
            {
                m_dKValue = value;
            }
        }

        private double m_dFitCircleNum = 3;

        /// <summary>
        /// 拟合圆的最少个数
        /// </summary>
        public double FitCircleNum
        {
            get
            {
                return this.m_dFitCircleNum;
            }
            set
            {
                m_dFitCircleNum = value;
            }

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public FindCirclePara()
        {
            m_dFitCircleNum = 3;
            m_bCircle = true;
            m_bCirclePoint = true;
            m_circleCalliper = new GVS.HalconDisp.ViewWindow.Config.CircleCalliper();
            m_strPolarity = "all";
            m_strSelect = "max";
            m_VisionRadius = 2.000;
            m_dKValue = 1;
        }
    }
}
