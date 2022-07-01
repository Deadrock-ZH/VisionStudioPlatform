using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SvMask;

namespace SvBlobTool
{
    [Serializable]
    /// <summary>
    /// 分段模式
    /// </summary>
    public enum EnumModeSegment
    {
        硬阈值固定,
        硬阈值相对,
        软阈值固定,
    }

    [Serializable]
    /// <summary>
    /// 分段极性
    /// </summary>
    public enum EnumPolarity
    {
        黑底白点,
        白底黑点,
    }

    [Serializable]
    /// <summary>
    /// 联通模式
    /// </summary>
    public enum EnumModeConnection
    {
        灰度 = 0,
    }

    [Serializable]
    /// <summary>
    /// 清除
    /// </summary>
    public enum EnumClear
    {
        无,
        修剪,
        填充,
    }

    [Serializable]
    /// <summary>
    /// 形态学处理
    /// </summary>
    public enum EnumMorphology
    {
        侵蚀水平面,
        侵蚀垂直面,
        侵蚀正方形,
        扩大水平面,
        扩大垂直面,
        扩大正方形,
        关闭水平面,
        关闭垂直面,
        关闭正方形,
        打开水平面,
        打开垂直面,
        打开正方形,
    }

    /// <summary>
    /// blob参数类
    /// </summary>
    [Serializable]
    [XmlRoot("BlobToolPara")]
    public class BlobToolPara
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
        public string RadiusSelectIndexName
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

        private SvMaskPara m_maskPara = new SvMask.SvMaskPara();

        /// <summary>
        /// 掩模模块参数
        /// </summary>
        public SvMaskPara MaskPara
        {
            get
            {
                return this.m_maskPara;
            }
            set
            {
                this.m_maskPara = value;
            }
        }

        private EnumModeSegment m_EnumModeSegment = EnumModeSegment.硬阈值固定;

        /// <summary>
        /// 分段模式
        /// </summary>
        public EnumModeSegment EnumSegmentMode
        {
            get
            {
                return m_EnumModeSegment;
            }
            set
            {
                m_EnumModeSegment = value;
            }
        }

        private EnumPolarity m_EnumPolarity = EnumPolarity.白底黑点;

        /// <summary>
        /// 分段极性
        /// </summary>
        public EnumPolarity EnumSegmentPolarity
        {
            get
            {
                return m_EnumPolarity;
            }
            set
            {
                m_EnumPolarity = value;
            }
        }

        private EnumModeConnection m_EnumModeConnection = EnumModeConnection.灰度;

        /// <summary>
        /// 联通模式
        /// </summary>
        public EnumModeConnection EnumConnectionMode
        {
            get
            {
                return m_EnumModeConnection;
            }
            set
            {
                m_EnumModeConnection = value;
            }
        }

        private EnumClear m_EnumEnumClear = EnumClear.修剪;

        /// <summary>
        /// 联通清除
        /// </summary>
        public EnumClear EnumConnectionClear
        {
            get
            {
                return m_EnumEnumClear;
            }
            set
            {
                m_EnumEnumClear = value;
            }
        }

        private double m_dMinArea = 10;

        /// <summary>
        /// 最小面积
        /// </summary>
        public double MinArea
        {
            get
            {
                return m_dMinArea;
            }
            set
            {
                m_dMinArea = value;
            }
        }

        private int m_iThreshold = 128;

        /// <summary>
        /// 硬阈值固定
        /// </summary>
        public int Threshold
        {
            get
            {
                return m_iThreshold;
            }
            set
            {
                m_iThreshold = value;
            }
        }

        private int m_iMinThreshold = 100;

        /// <summary>
        /// 软阈值固定最小灰度
        /// </summary>
        public int MinThreshold
        {
            get
            {
                return m_iMinThreshold;
            }
            set
            {
                m_iMinThreshold = value;
            }
        }


        private int m_iMaxThreshold = 128;

        /// <summary>
        /// 软阈值固定最大灰度
        /// </summary>
        public int MaxThreshold
        {
            get
            {
                return m_iMaxThreshold;
            }
            set
            {
                m_iMaxThreshold = value;
            }
        }

        private bool m_bBorder = true;

        /// <summary>
        /// 显示边界
        /// </summary>
        public bool Border
        {
            get
            {
                return m_bBorder;
            }
            set
            {
                m_bBorder = value;
            }
        }

        private bool m_bAreacenter = false;

        /// <summary>
        /// 显示质心
        /// </summary>
        public bool AreaCenter
        {
            get
            {
                return m_bAreacenter;
            }
            set
            {
                m_bAreacenter = value;
            }
        }

        private bool m_bAllPoint = false;

        /// <summary>
        /// 显示所有斑点
        /// </summary>
        public bool AllPoint
        {
            get
            {
                return m_bAllPoint;
            }
            set
            {
                m_bAllPoint = value;
            }
        }

        private bool m_bSelectPoint = false;

        /// <summary>
        /// 显示未过滤斑点
        /// </summary>
        public bool SelectPoint
        {
            get
            {
                return m_bSelectPoint;
            }
            set
            {
                m_bSelectPoint = value;
            }
        }

        private bool m_bPointConver = false;

        /// <summary>
        /// 显示斑点覆盖图
        /// </summary>
        public bool PointConver
        {
            get
            {
                return m_bPointConver;
            }
            set
            {
                m_bPointConver = value;
            }
        }

        private EnumMorphology m_EnumMorphology = EnumMorphology.侵蚀垂直面;

        /// <summary>
        /// 形态学处理
        /// </summary>
        public EnumMorphology EnumMorphologys
        {
            get
            {
                return m_EnumMorphology;
            }
            set
            {
                m_EnumMorphology = value;
            }
        }

        private List<EnumMorphology> m_ListMorPhology = new List<EnumMorphology>();

        /// <summary>
        /// 形态学处理
        /// </summary>
        public List<EnumMorphology> ListMorPhology
        {
            get
            {
                return m_ListMorPhology;
            }
            set
            {
                m_ListMorPhology = value;
            }
        }

        private GVS.HalconDisp.Control.RegionTypeParas m_BlobRoiParas = new GVS.HalconDisp.Control.RegionTypeParas();

        /// <summary>
        /// 模板区域类型参数
        /// </summary>
        public GVS.HalconDisp.Control.RegionTypeParas BlobRoiPara
        {
            get
            {
                return this.m_BlobRoiParas;
            }
            set
            {
                m_BlobRoiParas = value;
            }
        }

        private bool m_bDispSelectData = true;

        /// <summary>
        /// 显示未过滤斑点数据
        /// </summary>
        public bool DispSelectData
        {
            get
            {
                return this.m_bDispSelectData;
            }
            set
            {
                m_bDispSelectData = value;
            }
        }
    }
}
