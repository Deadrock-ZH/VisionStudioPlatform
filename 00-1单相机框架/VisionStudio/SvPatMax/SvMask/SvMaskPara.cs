using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvMask
{
    /// <summary>
    ///主要内容：本类掩模参数类
    /// 时    间：2019/9/3
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    [XmlRoot("SvMaskPara")]
    public class SvMaskPara
    {

        SvMaskParaSingle m_ParaROI = new SvMaskParaSingle();

        /// <summary>
        /// ROI参数类
        /// </summary>
        public SvMaskParaSingle ParaROI
        {
            get
            {
                return this.m_ParaROI;
            }
            set
            {
                m_ParaROI = value;
            }
        }

        private List<GVS.HalconDisp.ViewWindow.Config.Rectangle2> m_listRectPara = new List<GVS.HalconDisp.ViewWindow.Config.Rectangle2>();

        /// <summary>
        /// 存储矩形ROI
        /// </summary>
        public List<GVS.HalconDisp.ViewWindow.Config.Rectangle2> ListRectPara
        {
            get
            {
                return this.m_listRectPara;
            }
            set
            {
                this.m_listRectPara = value;
            }
        }

        private List<GVS.HalconDisp.ViewWindow.Config.Circle> m_listCirclePara = new List<GVS.HalconDisp.ViewWindow.Config.Circle>();

        /// <summary>
        /// 存储圆形ROI
        /// </summary>
        public List<GVS.HalconDisp.ViewWindow.Config.Circle> ListCirclePara
        {
            get
            {
                return this.m_listCirclePara;
            }
            set
            {
                this.m_listCirclePara = value;
            }
        }

        private List<GVS.HalconDisp.ViewWindow.Config.Polygon> m_listPolygonPara = new List<GVS.HalconDisp.ViewWindow.Config.Polygon>();

        /// <summary>
        /// 存储多边形ROI
        /// </summary>
        public List<GVS.HalconDisp.ViewWindow.Config.Polygon> ListPolygonPara
        {
            get
            {
                return this.m_listPolygonPara;
            }
            set
            {
                this.m_listPolygonPara = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public SvMaskPara()
        {
            m_listRectPara = new List<GVS.HalconDisp.ViewWindow.Config.Rectangle2>();
            m_listPolygonPara = new List<GVS.HalconDisp.ViewWindow.Config.Polygon>();
            m_listCirclePara = new List<GVS.HalconDisp.ViewWindow.Config.Circle>();
        }
    }

    /// <summary>
    /// 主要内容：本类是单个ROI的参数类
    /// 时    间：2019/9/3
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    [XmlRoot("SvMaskParaSingle")]
    public class SvMaskParaSingle
    {
        GVS.HalconDisp.ViewWindow.Config.Rectangle2 m_rect2 = new GVS.HalconDisp.ViewWindow.Config.Rectangle2();

        /// <summary>
        /// 矩形2
        /// </summary>
        public GVS.HalconDisp.ViewWindow.Config.Rectangle2 Rect2
        {
            get
            {
                return this.m_rect2;
            }
            set
            {
                this.m_rect2 = value;
            }
        }

        GVS.HalconDisp.ViewWindow.Config.Circle m_Circle = new GVS.HalconDisp.ViewWindow.Config.Circle();

        /// <summary>
        /// 圆
        /// </summary>
        public GVS.HalconDisp.ViewWindow.Config.Circle Circle
        {
            get
            {
                return this.m_Circle;
            }
            set
            {
                this.m_Circle = value;
            }
        }

        GVS.HalconDisp.ViewWindow.Config.Polygon m_polygon = new GVS.HalconDisp.ViewWindow.Config.Polygon();

        /// <summary>
        /// 多边形
        /// </summary>
        public GVS.HalconDisp.ViewWindow.Config.Polygon Polygon
        {
            get
            {
                return this.m_polygon;
            }
            set
            {
                this.m_polygon = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public SvMaskParaSingle()
        {
            m_polygon = new GVS.HalconDisp.ViewWindow.Config.Polygon();
            m_Circle = new GVS.HalconDisp.ViewWindow.Config.Circle();
            m_rect2 = new GVS.HalconDisp.ViewWindow.Config.Rectangle2();
        }
    }
}
