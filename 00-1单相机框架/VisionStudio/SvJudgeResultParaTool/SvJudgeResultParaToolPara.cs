using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SvJudgeResultParaTool
{
    /// <summary>
    /// 结果判断参数类
    /// </summary>
    [Serializable]
    [XmlRoot("SelectRangePara")]
    public class SelectRangePara
    {
        private double m_dMinRange = 0;

        /// <summary>
        /// 最小值
        /// </summary>
        public double MinRange
        {
            get
            {
                return m_dMinRange;
            }
            set
            {
                m_dMinRange = value;
            }
        }

        private double m_dMaxRange = 1000000;

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxRange
        {
            get
            {
                return m_dMaxRange;
            }
            set
            {
                m_dMaxRange = value;
            }
        }

        private bool m_bSelectState = false;

        /// <summary>
        /// 输入数据启用状态
        /// </summary>
        public bool SelectState
        {
            get
            {
                return m_bSelectState;
            }
            set
            {
                m_bSelectState = value;
            }
        }

        private double m_dData = 0;

        /// <summary>
        /// 输入数据
        /// </summary>
        public double Data
        {
            get
            {
                return m_dData;
            }
            set
            {
                m_dData = value;
            }
        }

        private string m_strSelectName = string.Empty;

        /// <summary>
        /// 选择Item的名
        /// </summary>
        public string SelectName
        {
            get
            {
                return m_strSelectName;
            }
            set
            {
                m_strSelectName = value;
            }
        }

        private double m_dK = 1;

        /// <summary>
        /// K值
        /// </summary>
        public double K
        {
            get
            {
                return m_dK;
            }
            set
            {
                m_dK = value;
            }
        }

        private double m_dOffset = 0;

        /// <summary>
        /// 差值
        /// </summary>
        public double Offset
        {
            get
            {
                return m_dOffset;
            }
            set
            {
                m_dOffset = value;
            }
        }

        private bool m_bSend = false;

        /// <summary>
        /// 数据发送状态
        /// </summary>
        public bool SendState
        {
            get
            {
                return m_bSend;
            }
            set
            {
                m_bSend = value;
            }
        }

        private bool m_bHaveOrNot = false;

        /// <summary>
        /// 有无状态
        /// </summary>
        public bool HaveOrNotState
        {
            get
            {
                return m_bHaveOrNot;
            }
            set
            {
                m_bHaveOrNot = value;
            }
        }

        private int m_iDecimalPlaces = 2;

        /// <summary>
        /// 小数点个数
        /// </summary>
        public int DecimalPlaces
        {
            get
            {
                return m_iDecimalPlaces;
            }
            set
            {
                m_iDecimalPlaces = value;
            }
        }
    }

    /// <summary>
    /// 内 容:本类是数据判断参数类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    [Serializable]
    [XmlRoot("SvJudgeResultParaToolPara")]
    public class SvJudgeResultParaToolPara
    {
        SelectRangePara[]m_ListSelectRangePara =new  SelectRangePara[16];

        /// <summary>
        /// 选择范围
        /// </summary>
        public SelectRangePara[] ListSelectRangePara
        {
            get
            {
                return m_ListSelectRangePara;
            }
            set
            {
                m_ListSelectRangePara = value;
            }
        }

        private string m_strSplit = "/";

        /// <summary>
        /// 发送分隔符
        /// </summary>
        public string SplitChar
        {
            get
            {
                return m_strSplit;
            }
            set
            {
                m_strSplit = value;
            }
        }

        private int m_dOk = 1;

        /// <summary>
        /// 检测OK前缀
        /// </summary>
        public int OK
        {
            get
            {
                return m_dOk;
            }
            set
            {
                m_dOk = value;
            }
        }

        private int m_dHaveOrNot = 0;

        /// <summary>
        /// 有无前缀
        /// </summary>
        public int HaveOrNotForward
        {
            get
            {
                return m_dHaveOrNot;
            }
            set
            {
                m_dHaveOrNot = value;
            }
        }

        private int m_dHaveOrNotBack = 0;

        /// <summary>
        /// 有无后缀
        /// </summary>
        public int HaveOrNotBack
        {
            get
            {
                return m_dHaveOrNotBack;
            }
            set
            {
                m_dHaveOrNotBack = value;
            }
        }


        private bool m_bHaveOrNotState = true;

        /// <summary>
        /// 有无前缀启用标志
        /// </summary>
        public bool HaveOrNotForwardState
        {
            get
            {
                return m_bHaveOrNotState;
            }
            set
            {
                m_bHaveOrNotState = value;
            }
        }

        private bool m_bHaveOrNotBackState = false;

        /// <summary>
        /// 有无后缀启用标志
        /// </summary>
        public bool HaveOrNotBackState
        {
            get
            {
                return m_bHaveOrNotBackState;
            }
            set
            {
                m_bHaveOrNotBackState = value;
            }
        }

        private bool m_bOK = true;

        /// <summary>
        /// OK前缀是否发送标志
        /// </summary>
        public bool OKForwardState
        {
            get
            {
                return m_bOK;
            }
            set
            {
                m_bOK = value;
            }
        }

        private bool m_bNG = true;

        /// <summary>
        /// NG前缀是否发送标志
        /// </summary>
        public bool NGForwardState
        {
            get
            {
                return m_bNG;
            }
            set
            {
                m_bNG = value;
            }
        }

        private int m_dNG = 2;

        /// <summary>
        /// 检测NG前缀
        /// </summary>
        public int NG
        {
            get
            {
                return m_dNG;
            }
            set
            {
                m_dNG = value;
            }
        }

        private int m_dOkBack = 1;

        /// <summary>
        /// 检测OK后缀
        /// </summary>
        public int OKBack
        {
            get
            {
                return m_dOkBack;
            }
            set
            {
                m_dOkBack = value;
            }
        }

        private bool m_bOKBack = false;

        /// <summary>
        /// OK后缀是否发送标志
        /// </summary>
        public bool OKBackState
        {
            get
            {
                return m_bOKBack;
            }
            set
            {
                m_bOKBack = value;
            }
        }

        private bool m_bNGBack = false;

        /// <summary>
        /// NG后缀是否发送标志
        /// </summary>
        public bool NGBackState
        {
            get
            {
                return m_bNGBack;
            }
            set
            {
                m_bNGBack = value;
            }
        }

        private int m_dNGBack = 2;

        /// <summary>
        /// 检测NG后缀
        /// </summary>
        public int NGBack
        {
            get
            {
                return m_dNGBack;
            }
            set
            {
                m_dNGBack = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public SvJudgeResultParaToolPara()
        {
            for (int i = 0; i < m_ListSelectRangePara.Length; i++)
            {
                m_ListSelectRangePara[i] = new SelectRangePara();
            }
        }

    }
}
