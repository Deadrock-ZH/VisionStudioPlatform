using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SvVisualCorrectionTool
{
    /// <summary>
    /// CCD工位影像位置
    /// </summary>
    [Serializable]
    public enum EnumTypeCheck
    {
        组立上影像 = 0,
        来料上影像,
        吸嘴下影像,
    }

    /// <summary>
    /// 结果判断参数类
    /// </summary>
    [Serializable]
    [XmlRoot("SelectRangeParas")]
    public class SelectRangeParas
    {
        private bool m_bSelectState = true;

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
    }

    /// <summary>
    /// 内 容:本类是示教参数类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    [Serializable]
    [XmlRoot("SvVisualCorrectionToolPara")]
    public class SvVisualCorrectionToolPara
    {
        private EnumTypeCheck m_EnumTypeCheck = EnumTypeCheck.来料上影像;

        /// <summary>
        /// 工位位置
        /// </summary>
        public EnumTypeCheck EnumTypeChecks
        {
            get
            {
                return m_EnumTypeCheck;
            }
            set
            {
                m_EnumTypeCheck = value;
            }
        }

        SelectRangeParas[] m_ListSelectRangePara = new SelectRangeParas[2];

        /// <summary>
        /// 选择范围
        /// </summary>
        public SelectRangeParas[] ListSelectRangePara
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

        private double m_dRowK1 = 1;

        /// <summary>
        /// K1
        /// </summary>
        public double RowK1
        {
            get
            {
                return m_dRowK1;
            }
            set
            {
                m_dRowK1 = value;
            }
        }

        private double m_dColB1 = 1;

        /// <summary>
        ///B1
        /// </summary>
        public double ColB1
        {
            get
            {
                return m_dColB1;
            }
            set
            {
                m_dColB1 = value;
            }
        }

        private double m_dRowK2 = 1;

        /// <summary>
        /// K2
        /// </summary>
        public double RowK2
        {
            get
            {
                return m_dRowK2;
            }
            set
            {
                m_dRowK2 = value;
            }
        }

        private double m_dColB2 = 1;

        /// <summary>
        ///B2
        /// </summary>
        public double ColB2
        {
            get
            {
                return m_dColB2;
            }
            set
            {
                m_dColB2 = value;
            }
        }

        private double m_dRowK3 = 1;

        /// <summary>
        /// K3
        /// </summary>
        public double RowK3
        {
            get
            {
                return m_dRowK3;
            }
            set
            {
                m_dRowK3 = value;
            }
        }

        private double m_dColB3 = 1;

        /// <summary>
        ///B3
        /// </summary>
        public double ColB3
        {
            get
            {
                return m_dColB3;
            }
            set
            {
                m_dColB3 = value;
            }
        }

        private double m_dRowK4 = 1;

        /// <summary>
        /// K4
        /// </summary>
        public double RowK4
        {
            get
            {
                return m_dRowK4;
            }
            set
            {
                m_dRowK4 = value;
            }
        }

        private double m_dColB4 = 1;

        /// <summary>
        ///B4
        /// </summary>
        public double ColB4
        {
            get
            {
                return m_dColB4;
            }
            set
            {
                m_dColB4 = value;
            }
        }

        private double m_dRowK5 = 1;

        /// <summary>
        /// K5
        /// </summary>
        public double RowK5
        {
            get
            {
                return m_dRowK5;
            }
            set
            {
                m_dRowK5 = value;
            }
        }

        private double m_dColB5 = 1;

        /// <summary>
        ///B5
        /// </summary>
        public double ColB5
        {
            get
            {
                return m_dColB5;
            }
            set
            {
                m_dColB5 = value;
            }
        }

        private double m_dXM1 = 1;

        /// <summary>
        /// XM1
        /// </summary>
        public double XM1
        {
            get
            {
                return m_dXM1;
            }
            set
            {
                m_dXM1 = value;
            }
        }

        private double m_dYM2 = 1;

        /// <summary>
        ///YM2
        /// </summary>
        public double YM2
        {
            get
            {
                return m_dYM2;
            }
            set
            {
                m_dYM2 = value;
            }
        }

        private double m_dXM3 = 1;

        /// <summary>
        /// XM3
        /// </summary>
        public double XM3
        {
            get
            {
                return m_dXM3;
            }
            set
            {
                m_dXM3 = value;
            }
        }

        private double m_dYM4 = 1;

        /// <summary>
        ///YM4
        /// </summary>
        public double YM4
        {
            get
            {
                return m_dYM4;
            }
            set
            {
                m_dYM4 = value;
            }
        }

        private double m_dXM5 = 1;

        /// <summary>
        ///XM5
        /// </summary>
        public double XM5
        {
            get
            {
                return m_dXM5;
            }
            set
            {
                m_dXM5 = value;
            }
        }

        private bool m_bUseZuLiUp = false;

        /// <summary>
        /// 组立上影像
        /// </summary>
        public bool UseZuLiUp
        {
            get
            {
                return m_bUseZuLiUp;
            }
            set
            {
                m_bUseZuLiUp = value;
            }
        }

        private bool m_bUseLaiLiaoUp = true;

        /// <summary>
        /// 来料上影像
        /// </summary>
        public bool UseLaiLiaoUp
        {
            get
            {
                return m_bUseLaiLiaoUp;
            }
            set
            {
                m_bUseLaiLiaoUp = value;
            }
        }

        private bool m_bUseXiZuiDown = false;

        /// <summary>
        /// 吸嘴下影像
        /// </summary>
        public bool UseXiZuiDown
        {
            get
            {
                return m_bUseXiZuiDown;
            }
            set
            {
                m_bUseXiZuiDown = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public SvVisualCorrectionToolPara()
        {
            for (int i = 0; i < m_ListSelectRangePara.Length; i++)
            {
                m_ListSelectRangePara[i] = new SelectRangeParas();
            }
        }

    }
}
