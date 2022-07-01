using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SvMask;

namespace SvsPatMax
{
    /// <summary>
    /// 主要内容：本类是模板匹配的参数类
    /// 时    间：2019/8/26
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    [XmlRoot("SvsPatMaxPara")]
    public class SvsPatMaxPara
    {
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

        /// <summary>
        /// 创建模板参数类
        /// </summary>
        public SvsPatMaxCreatePara CreatePara
        {
            get;
            set;
        }

        /// <summary>
        /// 匹配参数
        /// </summary>
        public SvsPatMaxFindPara FindPara
        {
            get;
            set;
        }

        /// <summary>
        /// 预处理参数
        /// </summary>
        public SvsPretreatmentPara PretreatementPara
        {
            get;
            set;
        }

        /// <summary>
        /// 存储为byte类型的模板
        /// </summary>
        public byte ModelID
        {
            get;
            set;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public SvsPatMaxPara()
        {
            CreatePara = new SvsPatMaxCreatePara();
            FindPara = new SvsPatMaxFindPara();
            PretreatementPara = new SvsPretreatmentPara();
            m_maskPara = new SvMask.SvMaskPara();
        }
    }

    /// <summary>
    /// 模板类型
    /// </summary>
    public enum ModelType
    {
        /// <summary>
        /// 形状
        /// </summary>
        形状 = 0,
        /// <summary>
        /// 轮廓
        /// </summary>
        轮廓 = 1,
        /// <summary>
        /// 灰度
        /// </summary>
        灰度 = 2,
    }

    /// <summary>
    /// 主要内容：本类是预处理参数
    /// 时    间：2019/8/31
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    [XmlRoot("SvsPretreatmentPara")]
    public class SvsPretreatmentPara
    {
        private double m_dLowThreshold = 0;

        /// <summary>
        /// 低阈值
        /// </summary>
        public double LowThreshold
        {
            get
            {
                return this.m_dLowThreshold;
            }
            set
            {
                this.m_dLowThreshold = value;
            }
        }

        private double m_dHighThreshold = 0;

        /// <summary>
        /// 高阈值
        /// </summary>
        public double HighThreshold
        {
            get
            {
                return this.m_dHighThreshold;
            }
            set
            {
                this.m_dHighThreshold = value;
            }
        }

        private double m_dOpening = 0.5;

        /// <summary>
        /// 开运算
        /// </summary>
        public double Opening
        {
            get
            {
                return this.m_dOpening;
            }
            set
            {
                this.m_dOpening = value;
            }
        }

        private double m_dClosing = 0.5;

        /// <summary>
        /// 闭运算
        /// </summary>
        public double Closing
        {
            get
            {
                return this.m_dClosing;
            }
            set
            {
                this.m_dClosing = value;
            }
        }

        private double m_dMinSize = 0;

        /// <summary>
        /// 小面积
        /// </summary>
        public double LowSize
        {
            get
            {
                return this.m_dMinSize;
            }
            set
            {
                this.m_dMinSize = value;
            }
        }

        private double m_dMaxSize = 0;

        /// <summary>
        /// 大面积
        /// </summary>
        public double HighSize
        {
            get
            {
                return this.m_dMaxSize;
            }
            set
            {
                this.m_dMaxSize = value;
            }
        }

        private double m_dPaint = 0;

        /// <summary>
        /// 重绘灰度
        /// </summary>
        public double PaintGray
        {
            get
            {
                return this.m_dPaint;
            }
            set
            {
                this.m_dPaint = value;
            }
        }

        private bool m_bPretreatment;

        /// <summary>
        /// 是否启用预处理
        /// </summary>
        public bool PretreatmentCheck
        {
            get
            {
                return this.m_bPretreatment;
            }
            set
            {
                this.m_bPretreatment = value;
            }
        }

        private bool m_bThresholdCheck;

        /// <summary>
        /// 阈值选择
        /// </summary>
        public bool ThresholdChecked
        {
            get
            {
                return this.m_bThresholdCheck;
            }
            set
            {
                this.m_bThresholdCheck = value;
            }
        }

        private bool m_bOpeningCheck;

        /// <summary>
        /// 开运算选择
        /// </summary>
        public bool OpeningChecked
        {
            get
            {
                return this.m_bOpeningCheck;
            }
            set
            {
                this.m_bOpeningCheck = value;
            }
        }

        private bool m_bClosingCheck;

        /// <summary>
        /// 闭运算选择
        /// </summary>
        public bool ClosingChecked
        {
            get
            {
                return this.m_bClosingCheck;
            }
            set
            {
                this.m_bClosingCheck = value;
            }
        }

        private bool m_bConnectionCheck;

        /// <summary>
        /// 连通选择
        /// </summary>
        public bool ConnectChecked
        {
            get
            {
                return this.m_bConnectionCheck;
            }
            set
            {
                this.m_bConnectionCheck = value;
            }
        }

        private bool m_bCAreaCheck;

        /// <summary>
        /// 面积选择
        /// </summary>
        public bool AreaChecked
        {
            get
            {
                return this.m_bCAreaCheck;
            }
            set
            {
                this.m_bCAreaCheck = value;
            }
        }

        private bool m_bPaintCheck;

        /// <summary>
        /// 重绘选择
        /// </summary>
        public bool PaintChecked
        {
            get
            {
                return this.m_bPaintCheck;
            }
            set
            {
                this.m_bPaintCheck = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public SvsPretreatmentPara()
        {
            m_dLowThreshold = 0;
            m_dHighThreshold = 128;
            m_dOpening = 0.5;
            m_dClosing = 2;
            m_dMinSize = 200;
            m_dMaxSize = 99999;
            m_dPaint = 255;
            m_bPretreatment = true;
            m_bThresholdCheck = false;
            m_bClosingCheck = false;
            m_bCAreaCheck = false;
            m_bConnectionCheck = true;
            m_bOpeningCheck = false;
            m_bPaintCheck = false;
        }
    }

    /// <summary>
    /// 主要内容：本类是模板匹配的创建参数类
    /// 时    间：2019/8/26
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    [XmlRoot("SvsPatMaxCreatePara")]
    public class SvsPatMaxCreatePara
    {
        private double m_dLowContrast;

        /// <summary>
        /// 低对比度
        /// </summary>
        public double LowContrast
        {
            get
            {
                return this.m_dLowContrast;
            }
            set
            {
                this.m_dLowContrast = value;
            }
        }

        private double m_dHighContrast;

        /// <summary>
        /// 高对比度
        /// </summary>
        public double HighContrast
        {
            get
            {
                return this.m_dHighContrast;
            }
            set
            {
                this.m_dHighContrast = value;
            }
        }

        private bool m_bContrastAuto = true;

        /// <summary>
        /// 高低对比度是否自动选择
        /// </summary>
        public bool ContrastAuto
        {
            get
            {
                return this.m_bContrastAuto;
            }
            set
            {
                m_bContrastAuto = value;
            }
        }

        private double m_dMinSizeComponent;

        /// <summary>
        /// 最小组件尺寸
        /// </summary>
        public double MinSizeComponent
        {
            get
            {
                return this.m_dMinSizeComponent;
            }
            set
            {
                this.m_dMinSizeComponent = value;
            }
        }
        private bool m_bMinComponentAuto = true;

        /// <summary>
        /// 最小组件自动选择
        /// </summary>
        public bool MinComponentAuto
        {
            get
            {
                return this.m_bMinComponentAuto;
            }
            set
            {
                this.m_bMinComponentAuto = value;
            }
        }

        private double m_dNumLevel;

        /// <summary>
        /// 金字塔层数
        /// </summary>
        public double NumLevel
        {
            get
            {
                return this.m_dNumLevel;
            }
            set
            {
                this.m_dNumLevel = value;
            }
        }

        private bool m_bNumLevelAuto = true;

        /// <summary>
        /// 金字塔级别自动选择
        /// </summary>
        public bool NumLevelAto
        {
            get
            {
                return this.m_bNumLevelAuto;
            }
            set
            {
                this.m_bNumLevelAuto = value;
            }
        }

        private double m_dStartPhi;

        /// <summary>
        /// 起始角度
        /// </summary>
        public double StartPhi
        {
            get
            {
                return this.m_dStartPhi;
            }
            set
            {
                this.m_dStartPhi = value;
            }
        }

        private double m_dPhiExtend;

        /// <summary>
        /// 角度范围
        /// </summary>
        public double PhiExtend
        {
            get
            {
                return this.m_dPhiExtend;
            }
            set
            {
                this.m_dPhiExtend = value;
            }
        }

        private double m_dRowScaleMin;

        /// <summary>
        /// 行方向最小缩放
        /// </summary>
        public double RowScaleMin
        {
            get
            {
                return this.m_dRowScaleMin;
            }
            set
            {
                this.m_dRowScaleMin = value;
            }
        }

        private double m_dRowScaleMax;

        /// <summary>
        /// 行方向最大缩放
        /// </summary>
        public double RowScaleMax
        {
            get
            {
                return this.m_dRowScaleMax;
            }
            set
            {
                this.m_dRowScaleMax = value;
            }
        }

        private double m_dColScaleMin;

        /// <summary>
        /// 列方向最小缩放
        /// </summary>
        public double ColScaleMin
        {
            get
            {
                return this.m_dColScaleMin;
            }
            set
            {
                this.m_dColScaleMin = value;
            }
        }

        private double m_dColScaleMax;

        /// <summary>
        /// 列方向最大缩放
        /// </summary>
        public double ColScaleMax
        {
            get
            {
                return this.m_dColScaleMax;
            }
            set
            {
                this.m_dColScaleMax = value;
            }
        }

        private bool m_bScaleConnect = false;

        /// <summary>
        /// 比例是否关联
        /// </summary>
        public bool ScaleConnect
        {
            get
            {
                return this.m_bScaleConnect;
            }
            set
            {
                this.m_bScaleConnect = value;
            }
        }

        private double m_dPhiStep;

        /// <summary>
        /// 角度步长
        /// </summary>
        public double PhiStep
        {
            get
            {
                return this.m_dPhiStep;
            }
            set
            {
                this.m_dPhiStep = value;
            }
        }

        private bool m_bPhiStepAuto = true;

        /// <summary>
        /// 角度步长自动设置
        /// </summary>
        public bool PhiStepAuto
        {
            get
            {
                return m_bPhiStepAuto;
            }
            set
            {
                this.m_bPhiStepAuto = value;
            }
        }

        private double m_dRowScaleStep;

        /// <summary>
        /// 行方向缩放步长
        /// </summary>
        public double RowScaleStep
        {
            get
            {
                return this.m_dRowScaleStep;
            }
            set
            {
                this.m_dRowScaleStep = value;
            }
        }

        private bool m_bRowScaleStepAuto = true;

        /// <summary>
        /// 行方向缩放步长
        /// </summary>
        public bool RowScaleStepAuto
        {
            get
            {
                return this.m_bRowScaleStepAuto;
            }
            set
            {
                this.m_bRowScaleStepAuto = value;
            }
        }

        private double m_dColScaleStep;

        /// <summary>
        /// 列方向缩放步长
        /// </summary>
        public double ColScaleStep
        {
            get
            {
                return this.m_dColScaleStep;
            }
            set
            {
                this.m_dColScaleStep = value;
            }
        }

        private bool m_bColScaleStepAuto = true;

        /// <summary>
        /// 列方向缩放步长
        /// </summary>
        public bool ColStepAuto
        {
            get
            {
                return this.m_bColScaleStepAuto;
            }
            set
            {
                this.m_bColScaleStepAuto = value;
            }
        }

        private string m_strPolarity;

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
                this.m_strPolarity = value;
            }
        }

        private string m_strOptimize;

        /// <summary>
        /// 最优化
        /// </summary>
        public string Optimize
        {
            get
            {
                return this.m_strOptimize;
            }
            set
            {
                this.m_strOptimize = value;
            }
        }

        private bool m_bOptimizeAuto = true;

        /// <summary>
        /// 最优化自动
        /// </summary>
        public bool OptimizeAuto
        {
            get
            {
                return this.m_bOptimizeAuto;
            }
            set
            {
                this.m_bOptimizeAuto = value;
            }
        }

        private bool m_bPregenerateAuto = false;

        /// <summary>
        /// 是否预生成                                                           
        /// </summary>
        public bool Pregenerate
        {
            get
            {
                return this.m_bPregenerateAuto;
            }
            set
            {
                this.m_bPregenerateAuto = value;
            }
        }

        private double m_dMinContrast;

        /// <summary>
        /// 最小对比度
        /// </summary>
        public double MinContrast
        {
            get
            {
                return this.m_dMinContrast;
            }
            set
            {
                this.m_dMinContrast = value;
            }
        }

        private bool m_bMinContrastAuto = true;

        /// <summary>
        /// 最小对比度自动
        /// </summary>
        public bool MinContrastAuto
        {
            get
            {
                return this.m_bMinContrastAuto;
            }
            set
            {
                m_bMinContrastAuto = value;
            }
        }

        private ModelType m_tModelType;

        /// <summary>
        /// 模板类型
        /// </summary>
        public ModelType ModelTypes
        {
            get
            {
                return m_tModelType;
            }
            set
            {
                m_tModelType = value;
            }
        }

        private GVS.HalconDisp.Control.RegionTypeParas m_CreateRoiParas = new GVS.HalconDisp.Control.RegionTypeParas();

        /// <summary>
        /// 模板区域类型参数
        /// </summary>
        public GVS.HalconDisp.Control.RegionTypeParas CreateRoiPara
        {
            get
            {
                return this.m_CreateRoiParas;
            }
            set
            {
                m_CreateRoiParas = value;
            }
        }

        private bool m_bModel;

        /// <summary>
        /// 是否显示模板轮廓
        /// </summary>
        public bool ModelContourChecked
        {
            get
            {
                return this.m_bModel;
            }
            set
            {
                this.m_bModel = value;
            }
        }

        private bool m_bFindContour = true;

        /// <summary>
        /// 是否显示模板轮廓
        /// </summary>
        public bool FindContourChecked
        {
            get
            {
                return this.m_bFindContour;
            }
            set
            {
                this.m_bFindContour = value;
            }
        }

        private bool m_bModelRoi;

        /// <summary>
        /// 模板区域
        /// </summary>
        public bool ModelRoiChecked
        {
            get
            {
                return this.m_bModelRoi;
            }
            set
            {
                this.m_bModelRoi = value;
            }
        }

        private bool m_bSearchRoi;

        /// <summary>
        /// 搜索区域
        /// </summary>
        public bool SearchRoiChecked
        {
            get
            {
                return this.m_bSearchRoi;
            }
            set
            {
                this.m_bSearchRoi = value;
            }
        }
        private double m_dRowModel = -100;

        /// <summary>
        /// 模板图像的行坐标
        /// </summary>
        public double RowModel
        {
            get
            {
                return this.m_dRowModel;
            }
            set
            {
                this.m_dRowModel = value;
            }
        }

        private double m_dColModel = -100;

        /// <summary>
        /// 模板图像的列坐标
        /// </summary>
        public double ColModel
        {
            get
            {
                return this.m_dColModel;
            }
            set
            {
                this.m_dColModel = value;
            }
        }

        private double m_dAngleModel = -100;

        /// <summary>
        /// 模板图像的角度
        /// </summary>
        public double AngleModel
        {
            get
            {
                return this.m_dAngleModel;
            }
            set
            {
                this.m_dAngleModel = value;
            }
        }


        /// <summary>
        /// 构造方法
        /// </summary>
        public SvsPatMaxCreatePara()
        {
            m_dRowModel = -100;
            m_dColModel = -100;
            m_dAngleModel = -100;
            m_dLowContrast = 30;
            m_dHighContrast = 30;
            m_bContrastAuto = true;
            m_dMinSizeComponent = 0;
            m_bMinComponentAuto = true;
            m_dNumLevel = 5;
            m_bNumLevelAuto = true;
            m_dStartPhi = -0.39;
            m_dPhiExtend = 0.78;
            m_dColScaleMax = 1.0;
            m_dColScaleMin = 1.0;
            m_dRowScaleMin = 1.0;
            m_dRowScaleMax = 1.0;
            m_bScaleConnect = false;
            m_dPhiStep = 0.0155;
            m_bPhiStepAuto = true;
            m_dRowScaleStep = 0.0262;
            m_bRowScaleStepAuto = true;
            m_dColScaleStep = 0.0200;
            m_bColScaleStepAuto = true;
            m_strPolarity = "use_polarity";
            m_strOptimize = "none";
            m_bOptimizeAuto = true;
            m_bPregenerateAuto = false;
            m_dMinContrast = 10;
            m_bMinContrastAuto = true;
            m_tModelType = ModelType.形状;
            m_CreateRoiParas = new GVS.HalconDisp.Control.RegionTypeParas();
            m_bModelRoi = false;
            m_bModel = false;
            m_bSearchRoi = false;
        }
    }

    /// <summary>
    /// 主要内容：本类是模板匹配的匹配参数类
    /// 时    间：2019/8/27
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    [XmlRoot("SvsPatMaxFindPara")]
    public class SvsPatMaxFindPara
    {
        private double m_dMinScore;

        /// <summary>
        /// 最小分数
        /// </summary>
        public double MinScore
        {
            get
            {
                return this.m_dMinScore;
            }
            set
            {
                this.m_dMinScore = value;
            }
        }

        private double m_dFindMaxNum;

        /// <summary>
        /// 匹配的最大数
        /// </summary>
        public double FindMaxNum
        {
            get
            {
                return this.m_dFindMaxNum;
            }
            set
            {
                this.m_dFindMaxNum = value;
            }
        }

        private double m_dGreed;

        /// <summary>
        /// 贪心算法
        /// </summary>
        public double Greed
        {
            get
            {
                return this.m_dGreed;
            }
            set
            {
                this.m_dGreed = value;
            }
        }

        private double m_dMaxOver;

        /// <summary>
        /// 最大重叠
        /// </summary>
        public double MaxOver
        {
            get
            {
                return this.m_dMaxOver;
            }
            set
            {
                this.m_dMaxOver = value;
            }
        }

        private string m_strSubPixs;

        /// <summary>
        /// 亚像素
        /// </summary>
        public string SubPix
        {
            get
            {
                return this.m_strSubPixs;
            }
            set
            {
                this.m_strSubPixs = value;
            }
        }

        private Int32 m_iMaxDeformation;

        /// <summary>
        /// 最大形变
        /// </summary>
        public Int32 MaxDeformation
        {
            get
            {
                return this.m_iMaxDeformation;
            }
            set
            {
                this.m_iMaxDeformation = value;
            }
        }

        private double m_dFindNumLevel;

        /// <summary>
        /// 匹配的最大金字塔层数
        /// </summary>
        public double FindNumLevel
        {
            get
            {
                return this.m_dFindNumLevel;
            }
            set
            {
                this.m_dFindNumLevel = value;
            }
        }

        private double m_dTime;

        /// <summary>
        /// 超时时间
        /// </summary>
        public double Time
        {
            get
            {
                return m_dTime;
            }
            set
            {
                this.m_dTime = value;
            }
        }

        private bool m_bTime = false;

        /// <summary>
        /// 是否启用超时
        /// </summary>
        public bool TimeCheck
        {
            get
            {
                return this.m_bTime;
            }
            set
            {
                this.m_bTime = value;
            }
        }

        private bool m_bIncreased = false;

        /// <summary>
        /// 增加公差模式
        /// </summary>
        public bool Increased
        {
            get
            {
                return this.m_bIncreased;
            }
            set
            {
                this.m_bIncreased = value;
            }
        }

        private bool m_bIntersection = false;

        /// <summary>
        /// 形状模板可能与图像边缘相交
        /// </summary>
        public bool Intersection
        {
            get
            {
                return this.m_bIntersection;
            }
            set
            {
                this.m_bIntersection = value;
            }
        }

        private GVS.HalconDisp.Control.RegionTypeParas m_FindRoiParas = new GVS.HalconDisp.Control.RegionTypeParas();

        /// <summary>
        /// 搜索区域类型参数
        /// </summary>
        public GVS.HalconDisp.Control.RegionTypeParas FindRoiPara
        {
            get
            {
                return this.m_FindRoiParas;
            }
            set
            {
                m_FindRoiParas = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public SvsPatMaxFindPara()
        {
            m_dMinScore = 0.5;
            m_dFindMaxNum = 1;
            m_dGreed = 0.75;
            m_dMaxOver = 0.5;
            m_strSubPixs = "least_squares";
            m_iMaxDeformation = 0;
            m_dFindNumLevel = 0;
            m_dTime = 0;
            m_bIncreased = false;
            m_bIntersection = false;
            m_bTime = false;
            m_FindRoiParas = new GVS.HalconDisp.Control.RegionTypeParas();
        }
    }
}
