using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvMask
{
    /// <summary>
    /// 主要内容：本类是掩模的方法类
    /// 时    间：2019/9/3
    /// 作    者：王雅鹏
    /// </summary>
    public class SvMaskMethod
    {
        /// <summary>
        /// 掩模参数类
        /// </summary>
        public SvMaskPara Para = new SvMaskPara();

        private HObject m_InputImage = null;

        /// <summary>
        /// 输入图像
        /// </summary>
        public HObject InputImage
        {
            get
            {
                return this.m_InputImage;
            }
            set
            {
                this.m_InputImage = value;
            }
        }

        private HTuple m_hvAffineHom = null;

        /// <summary>
        /// 彷射矩阵
        /// </summary>
        public HTuple AffineHom
        {
            get
            {
                return m_hvAffineHom;
            }
            set
            {
                m_hvAffineHom = value;
            }
        }

        private HObject m_modelReg = null;

        /// <summary>
        /// 模板区域
        /// </summary>
        public HObject ModelReg
        {
            get
            {
                return m_modelReg;
            }
            set
            {
                m_modelReg = value;
            }
        }

        private HObject m_reduceImage = null;

        /// <summary>
        /// 掩模后的图像
        /// </summary>
        public HObject ReducedImage
        {
            get
            {
                return this.m_reduceImage;
            }
            set
            {
                m_reduceImage = value;
            }
        }

        private HObject m_MaskReg = null;

        /// <summary>
        /// 掩模区域
        /// </summary>
        public HObject MaskReg
        {
            get
            {
                return this.m_MaskReg;
            }
            set
            {
                this.m_MaskReg = value;
            }
        }

        /// <summary>
        /// 运行方法
        /// </summary>
        public void Run()
        {
            HObject hoRegRectAll;
            HOperatorSet.GenEmptyObj(out hoRegRectAll);
            HObject hoRegPolygonAll;
            HOperatorSet.GenEmptyObj(out hoRegPolygonAll);
            HObject hoRegCircleAll;
            HOperatorSet.GenEmptyObj(out hoRegCircleAll);
            HObject hoMaskReg = null;
            HOperatorSet.GenEmptyObj(out hoMaskReg);
            m_MaskReg = null;
            HOperatorSet.GenEmptyObj(out m_MaskReg);
            foreach (GVS.HalconDisp.ViewWindow.Config.Rectangle2 item in Para.ListRectPara)
            {
                HObject hoRectReg=null;
                HOperatorSet.GenEmptyObj(out hoRectReg);
                HOperatorSet.GenContourPolygonXld(out hoRectReg, item.ArrayRow, item.ArrayCol);
                HOperatorSet.GenRegionContourXld(hoRectReg, out hoRectReg, "filled");
                HOperatorSet.ConcatObj(hoRegRectAll, hoRectReg, out hoRegRectAll);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Polygon item in Para.ListPolygonPara)
            {
                HObject hoPolygonReg=null;
                HOperatorSet.GenEmptyObj(out hoPolygonReg);
                HOperatorSet.GenContourPolygonXld(out hoPolygonReg, item.ListPointRowPeak.ToArray(),
                                                 item.ListPointColPeak.ToArray());
                HOperatorSet.GenRegionContourXld(hoPolygonReg, out hoPolygonReg, "filled");
                HOperatorSet.ConcatObj(hoRegPolygonAll, hoPolygonReg, out hoRegPolygonAll);
            }
            foreach (GVS.HalconDisp.ViewWindow.Config.Circle item in Para.ListCirclePara)
            {
                HObject hoCircleReg;
                HOperatorSet.GenEmptyObj(out hoCircleReg);
                HOperatorSet.GenCircle(out hoCircleReg, item.Row, item.Column, item.Radius);
                HOperatorSet.ConcatObj(hoRegCircleAll, hoCircleReg, out hoRegCircleAll);
            }
            HOperatorSet.ConcatObj(hoMaskReg, hoRegPolygonAll, out hoMaskReg);
            HOperatorSet.ConcatObj(hoMaskReg, hoRegCircleAll, out hoMaskReg);
            HOperatorSet.ConcatObj(hoMaskReg, hoRegRectAll, out hoMaskReg);
            if (null != m_hvAffineHom)
            {
                HObject MaskRegAffine = null;
                HOperatorSet.AffineTransRegion(hoMaskReg, out MaskRegAffine, m_hvAffineHom, "nearest_neighbor");
                m_MaskReg = MaskRegAffine;
            }
            else
            {
                m_MaskReg = hoMaskReg;
            }
        }
    }
}
