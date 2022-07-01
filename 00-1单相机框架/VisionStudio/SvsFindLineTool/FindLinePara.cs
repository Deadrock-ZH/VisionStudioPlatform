using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvsFindLineTool
{
    /// <summary>
    /// 主要内容：本类是找线的参数类
    /// 时    间：2019/9/6
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    [XmlRoot("FindLinePara")]
    public class FindLinePara
    {
        private string m_strRow1SelectIndexName = "null";

        /// <summary>
        /// Row2选择索引对应名称
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

        private GVS.HalconDisp.ViewWindow.Config.LineCalliper m_LineCalliperPara = new GVS.HalconDisp.ViewWindow.Config.LineCalliper();

        /// <summary>
        /// 卡尺参数
        /// </summary>
        public GVS.HalconDisp.ViewWindow.Config.LineCalliper LineCalliperParas
        {
            get
            {
                return this.m_LineCalliperPara;
            }
            set
            {
                this.m_LineCalliperPara = value;
            }
        }

        private LineCtrlPara m_LineCtrlPara = new LineCtrlPara();

        /// <summary>
        /// 卡尺界面参数
        /// </summary>
        public LineCtrlPara LineCtrlParas
        {
            get
            {
                return this.m_LineCtrlPara;
            }
            set
            {
                this.m_LineCtrlPara = value;
            }
        }

        private double m_dPhi;

        /// <summary>
        /// 卡尺角度
        /// </summary>
        public double Phi
        {
            get
            {
                return this.m_dPhi;
            }
            set
            {
                this.m_dPhi = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public FindLinePara()
        {
            m_LineCalliperPara = new GVS.HalconDisp.ViewWindow.Config.LineCalliper();
            m_LineCtrlPara = new LineCtrlPara();
            HTuple hvAngle = null;
            HOperatorSet.AngleLx(m_LineCalliperPara.RowBegin, m_LineCalliperPara.ColumnBegin,
                         m_LineCalliperPara.RowEnd, m_LineCalliperPara.ColumnEnd, out hvAngle);
            m_dPhi = hvAngle[0].D;
        }
    }

    /// <summary>
    /// 主要内容：本类是找线的窗体控件参数类
    /// 时    间：2019/9/6
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    [XmlRoot("LineCtrlPara")]
    public class LineCtrlPara
    {
        private bool m_bLine = true;

        /// <summary>
        /// 直线区域
        /// </summary>
        public bool Line
        {
            get
            {
                return this.m_bLine;
            }
            set
            {
                this.m_bLine = value;
            }
        }

        private bool m_bCalliper = false;

        /// <summary>
        /// 卡尺区域
        /// </summary>
        public bool Calliper
        {
            get
            {
                return this.m_bCalliper;
            }
            set
            {
                this.m_bCalliper = value;
            }
        }
        public LineCtrlPara()
        {
            m_bLine = true;
            m_bCalliper = false;
        }
    }

    /// <summary>
    /// 主要内容：本类是线卡尺的参数类
    /// 时    间：2019/9/6
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    [XmlRoot("LineCalliperPara")]
    public class LineCalliperPara
    {
        private double m_dCalliperNum = 30;

        /// <summary>
        /// 卡尺个数
        /// </summary>
        public double CalliperNum
        {
            get
            {
                return this.m_dCalliperNum;
            }
            set
            {
                this.m_dCalliperNum = value;
            }
        }

        private double m_dCalliperWidth = 15;

        /// <summary>
        /// 卡尺宽度
        /// </summary>
        public double CalliperWidth
        {
            get
            {
                return this.m_dCalliperWidth;
            }
            set
            {
                this.m_dCalliperWidth = value;
            }
        }

        private double m_dCalliperHeight = 60;

        /// <summary>
        /// 卡尺长
        /// </summary>
        public double CalliperHeight
        {
            get
            {
                return this.m_dCalliperHeight;
            }
            set
            {
                m_dCalliperHeight = value;
            }
        }

        private double m_dSigma = 1;

        /// <summary>
        /// sigma
        /// </summary>
        public double Sigma
        {
            get
            {
                return this.m_dSigma;
            }
            set
            {
                this.m_dSigma = value;
            }
        }

        private double m_dThreshold = 20;

        /// <summary>
        /// 对比度
        /// </summary>
        public double Threshold
        {
            get
            {
                return this.m_dThreshold;
            }
            set
            {
                this.m_dThreshold = value;
            }
        }

        private string m_strTransition = "all";

        /// <summary>
        /// 边缘选择
        /// </summary>
        public string Transition
        {
            get
            {
                return this.m_strTransition;
            }
            set
            {
                this.m_strTransition = value;
            }
        }

        private string m_strSelect = "max";

        /// <summary>
        /// 极性
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

        private double m_dRowStart = 200;

        /// <summary>
        /// 卡尺起始行位置
        /// </summary>
        public double RowStart
        {
            get
            {
                return this.m_dRowStart;
            }
            set
            {
                this.m_dRowStart = value;
            }
        }

        private double m_dColStart = 200;

        /// <summary>
        /// 起始列位置
        /// </summary>
        public double ColStart
        {
            get
            {
                return this.m_dColStart;
            }
            set
            {
                this.m_dColStart = value;
            }
        }

        private double m_dRowEnd = 200;

        /// <summary>
        /// 卡尺终止行位置
        /// </summary>
        public double RowEnd
        {
            get
            {
                return this.m_dRowEnd;
            }
            set
            {
                this.m_dRowEnd = value;
            }
        }

        private double m_dColEnd = 500;

        /// <summary>
        /// 终止列位置
        /// </summary>
        public double ColEnd
        {
            get
            {
                return this.m_dColEnd;
            }
            set
            {
                this.m_dColEnd = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public LineCalliperPara()
        {
            m_dColEnd = 500;
            m_dRowEnd = 200;
            m_dColStart = 200;
            m_dRowStart = 200;
            m_strSelect = "max";
            m_strTransition = "all";
            m_dThreshold = 20;
            m_dSigma = 1;
            m_dCalliperHeight = 60;
            m_dCalliperWidth = 15;
            m_dCalliperNum = 30;
        }
    }
}
