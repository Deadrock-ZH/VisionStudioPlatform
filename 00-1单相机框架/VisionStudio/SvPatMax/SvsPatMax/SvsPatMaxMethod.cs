using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GVS.HalconDisp.ViewROI.Config;
using ToolInterFace;

namespace SvsPatMax
{
    /// <summary>
    /// 主要内容：本类是模板匹配的方法类
    /// 时    间：2019/8/31
    /// 作    者：王雅鹏
    /// </summary>
    public class SvsPatMaxMethod :  ToolInterface
    {
        /// <summary>
        /// 参数类
        /// </summary>
        public SvsPatMaxPara Para = new SvsPatMaxPara();

        /// <summary>
        /// 模块Form名
        /// </summary>
        public string ModualFormName
        {
            get;
            set;
        }

        private string m_PatMaxName = "Model";

        /// <summary>
        /// 模板名
        /// </summary>
        public string PatMaxName
        {
            get
            {
                return m_PatMaxName;
            }
            set
            {
                m_PatMaxName = value;
            }
        }

        private HObject m_hoImage = null;

        /// <summary>
        /// 输入图片
        /// </summary>
        public HObject InputImage
        {
            get
            {
                return this.m_hoImage;
            }
            set
            {
                this.m_hoImage = value;
            }
        }

        private HTuple m_hvaffineTans = null;

        /// <summary>
        /// 仿射变换
        /// </summary>
        public HTuple HvAffinetrans
        {
            get
            {
                return this.m_hvaffineTans;
            }
            set
            {
                this.m_hvaffineTans = value;
            }
        }

        private double m_dScore = 0;

        /// <summary>
        /// 匹配得分
        /// </summary>
        public double Score
        {
            get
            {
                return this.m_dScore;
            }
        }

        private double m_dScaleRow = 0;

        /// <summary>
        /// 行缩放比例
        /// </summary>
        public double ScaleRow
        {
            get
            {
                return this.m_dScaleRow;
            }
        }

        private double m_dScaleCol = 0;

        /// <summary>
        /// 列缩放比例
        /// </summary>
        public double ScaleCol
        {
            get
            {
                return this.m_dScaleCol;
            }
        }

        private double m_dRow = -1;

        /// <summary>
        /// 行坐标
        /// </summary>
        public double Row
        {
            get
            {
                return this.m_dRow;
            }
        }

        private double m_dCol = -1;

        /// <summary>
        /// 列坐标
        /// </summary>
        public double Col
        {
            get
            {
                return this.m_dCol;
            }
        }

        private double m_dAngle = 0;

        /// <summary>
        /// 角度
        /// </summary>
        public double Angle
        {
            get
            {
                return this.m_dAngle;
            }
        }

        private HObject m_modelReg = null;

        /// <summary>
        /// 模板区域
        /// </summary>
        public HObject RegModelReg
        {
            get
            {
                return this.m_modelReg;
            }
            set
            {
                this.m_modelReg = value;
            }
        }

        /// <summary>
        /// 模板Id
        /// </summary>
        private HTuple m_hvModelId = null;


        /// <summary>
        /// 模板ID
        /// </summary>
        public HTuple ModelID
        {
            get
            {
                return this.m_hvModelId;
            }
            set
            {
                this.m_hvModelId = value;
            }
        }

        private HObject m_hoCreateContourAffine = null;

        /// <summary>
        /// 创建时的模板轮廓
        /// </summary>
        public HObject CreateContourModel
        {
            get
            {
                return this.m_hoCreateContourAffine;
            }
            set
            {
                this.m_hoCreateContourAffine = value;
            }
        }

        private HObject m_hoFindCenterCross = null;

        /// <summary>
        /// 匹配到的中心坐标十字
        /// </summary>
        public HObject FindCenterCross
        {
            get
            {
                return this.m_hoFindCenterCross;
            }
            set
            {
                this.m_hoFindCenterCross = value;
            }
        }

        private HObject m_hoFindContourAffine = null;

        /// <summary>
        /// 匹配到的模板轮廓
        /// </summary>
        public HObject FindContourModel
        {
            get
            {
                return this.m_hoFindContourAffine;
            }
            set
            {
                this.m_hoFindContourAffine = value;
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
        /// 原图匹配结果参数
        /// </summary>
        HTuple m_hvRowCenter = null, m_hvColCenter = null,
            m_hvAngle = null, m_hvRowScale = null,
            m_hvColScale = null, m_hvScore = null;

        /// <summary>
        /// 当前图像匹配结果参数
        /// </summary>
        HTuple m_hvRowCenterNow = null, m_hvColCenterNow = null,
               m_hvAngleNow = null, m_hvRowScaleNow = null,
               m_hvColScaleNow = null, m_hvScoreNow = null;

        /// <summary>
        /// 阈值选择后的区域
        /// </summary>
        public HObject m_hoThresholdReg = null;

        /// <summary>
        /// 开运算后的区域
        /// </summary>
        public HObject m_hoThresholdRegOpening = null;

        /// <summary>
        /// 闭运算后的区域
        /// </summary>
        public HObject m_hoThresholdRegClosing = null;

        /// <summary>
        /// 连通后的区域
        /// </summary>
        public HObject m_hoRegConnection = null;

        /// <summary>
        /// 面积选择后的区域
        /// </summary>
        public HObject m_hoAeaSelectShape = null;

        /// <summary>
        /// 重绘后的图片
        /// </summary>
        public HObject m_hoPaint = null;

        /// <summary>
        /// areacenter算子后的信息
        /// </summary>
        public HTuple m_hvArea = null, m_hvRow = null, m_hvCol = null;

        /// <summary>
        /// 文件化XML序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public bool Save(object obj, string filename)
        {
            FileStream fs = null;
            try
            {
                string strarry = filename.Substring(0, filename.LastIndexOf("\\"));
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                if (null != m_hvModelId)
                {
                    HOperatorSet.WriteShapeModel(m_hvModelId, strarry + "\\" + m_PatMaxName + ".shm");
                }
                if (null != m_hoCreateContourAffine)
                {
                    // 将模板轮廓保存
                   // HOperatorSet.WriteObject(m_hoCreateContourAffine, strarry + "\\" + m_PatMaxName);
                }
                if (null != m_MaskReg)
                {
                    // 将模板轮廓保存
                    // HOperatorSet.WriteObject(m_MaskReg, strarry + "\\Mask");
                }
                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                m_StrMessage = ex.ToString();
                return false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return true;
        }

        /// <summary>
        /// 文件化XML反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        public object Load(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                string strarry = filename.Substring(0, filename.LastIndexOf("\\"));
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                if (File.Exists(strarry +"\\"+ m_PatMaxName+".shm"))
                {
                    HOperatorSet.ReadShapeModel(strarry + "\\" + m_PatMaxName + ".shm",
                                                out m_hvModelId);
                }
                if (File.Exists(strarry + "\\" + m_PatMaxName))
                {
                    HOperatorSet.ReadObject(out m_hoCreateContourAffine,
                                            strarry + "\\" + m_PatMaxName+".hobj");
                }
                if (File.Exists(strarry + "\\" + "\\Mask.hobj"))
                {
                    // HOperatorSet.ReadObject(out m_MaskReg,
                    //                        strarry + "\\Mask.hobj");
                }
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                m_StrMessage = ex.ToString();
                Para = new SvsPatMaxPara();
                return Para;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 创建模板
        /// </summary>
        /// <param name="bState">true：成功</param>
        /// <param name="strMessage">创建消息</param>
        public void CreateModel( out bool bState, out string strMessage)
        {
            bState = true;
            strMessage = "模板创建成功！";
            m_StrMessage = strMessage;
            m_hvModelId = null;
            HObject hoContour = null;
            HTuple hvAffine = null;
            HObject hoReducedImage = null;
            m_hoFindContourAffine = null;
            m_hoCreateContourAffine = null;
            #region 掩模相关
            // 实际区域
            HObject hoReg = null;
            if (null != m_modelReg)
            {
                HObject hoRegRectAll;
                HOperatorSet.GenEmptyObj(out hoRegRectAll);
                HObject hoRegPolygonAll;
                HOperatorSet.GenEmptyObj(out hoRegPolygonAll);
                HObject hoRegCircleAll;
                HOperatorSet.GenEmptyObj(out hoRegCircleAll);
                m_MaskReg = null;
                HOperatorSet.GenEmptyObj(out m_MaskReg);
                foreach (GVS.HalconDisp.ViewWindow.Config.Rectangle2 item in Para.MaskPara.ListRectPara)
                {
                    HObject hoRectReg;
                    HOperatorSet.GenEmptyObj(out hoRectReg);
                    HOperatorSet.GenContourPolygonXld(out hoRectReg, item.ArrayRow, item.ArrayCol);
                    HOperatorSet.GenRegionContourXld(hoRectReg, out hoRectReg, "filled");
                    HOperatorSet.ConcatObj(hoRegRectAll, hoRectReg, out hoRegRectAll);
                }
                foreach (GVS.HalconDisp.ViewWindow.Config.Polygon item in Para.MaskPara.ListPolygonPara)
                {
                    HObject hoPolygonReg;
                    HOperatorSet.GenEmptyObj(out hoPolygonReg);
                    HOperatorSet.GenContourPolygonXld(out hoPolygonReg, item.ListPointRowPeak.ToArray(),
                                                      item.ListPointColPeak.ToArray());
                    HOperatorSet.GenRegionContourXld(hoPolygonReg, out hoPolygonReg, "filled");
                    HOperatorSet.ConcatObj(hoRegPolygonAll, hoPolygonReg, out hoRegPolygonAll);
                }
                foreach (GVS.HalconDisp.ViewWindow.Config.Circle item in Para.MaskPara.ListCirclePara)
                {
                    HObject hoCircleReg;
                    HOperatorSet.GenEmptyObj(out hoCircleReg);
                    HOperatorSet.GenCircle(out hoCircleReg, item.Row, item.Column, item.Radius);
                    HOperatorSet.ConcatObj(hoRegCircleAll, hoCircleReg, out hoRegCircleAll);
                }
                HOperatorSet.ConcatObj(m_MaskReg, hoRegPolygonAll, out m_MaskReg);
                HOperatorSet.ConcatObj(m_MaskReg, hoRegCircleAll, out m_MaskReg);
                HOperatorSet.ConcatObj(m_MaskReg, hoRegRectAll, out m_MaskReg);
                if (null != m_MaskReg)
                {
                    HOperatorSet.Difference(m_modelReg, m_MaskReg, out hoReg);
                }
                else
                {
                    hoReg = m_modelReg;
                }
                #endregion
                if (null != m_hoImage)
                {
                    HOperatorSet.ReduceDomain(m_hoImage, hoReg, out hoReducedImage);
                    switch (Para.CreatePara.ModelTypes)
                    {
                        case ModelType.形状:
                            #region 自动创建参数
                            try
                            {
                                if (Para.CreatePara.NumLevelAto)
                                {
                                    if (Para.CreatePara.ContrastAuto)
                                    {
                                        if (Para.CreatePara.MinComponentAuto)
                                        {
                                            if (Para.CreatePara.PhiStepAuto)
                                            {
                                                if (Para.CreatePara.RowScaleStepAuto)
                                                {
                                                    if (Para.CreatePara.ColStepAuto)
                                                    {
                                                        if (Para.CreatePara.MinContrastAuto)
                                                        {
                                                            HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                              Para.CreatePara.StartPhi,
                                                                              Para.CreatePara.PhiExtend, "auto",
                                                                              Para.CreatePara.RowScaleMin,
                                                                              Para.CreatePara.RowScaleMax, "auto",
                                                                              Para.CreatePara.ColScaleMin,
                                                                              Para.CreatePara.ColScaleMax, "auto",
                                                                              Para.CreatePara.Optimize,
                                                                              Para.CreatePara.Polarity,
                                                                              (new HTuple("auto_contrast")).
                                                                              TupleConcat("auto_min_size"),
                                                                              "auto", out m_hvModelId);
                                                        }
                                                        else
                                                        {
                                                            HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                             Para.CreatePara.StartPhi,
                                                                             Para.CreatePara.PhiExtend, "auto",
                                                                             Para.CreatePara.RowScaleMin,
                                                                             Para.CreatePara.RowScaleMax, "auto",
                                                                             Para.CreatePara.ColScaleMin,
                                                                             Para.CreatePara.ColScaleMax, "auto",
                                                                             Para.CreatePara.Optimize,
                                                                             Para.CreatePara.Polarity,
                                                                             (new HTuple("auto_contrast")).
                                                                             TupleConcat("auto_min_size"),
                                                                             Para.CreatePara.MinContrast,
                                                                             out m_hvModelId);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                            Para.CreatePara.StartPhi,
                                                                            Para.CreatePara.PhiExtend, "auto",
                                                                            Para.CreatePara.RowScaleMin,
                                                                            Para.CreatePara.RowScaleMax, "auto",
                                                                            Para.CreatePara.ColScaleMin,
                                                                            Para.CreatePara.ColScaleMax,
                                                                            Para.CreatePara.ColScaleStep,
                                                                            Para.CreatePara.Optimize,
                                                                            Para.CreatePara.Polarity,
                                                                            (new HTuple("auto_contrast")).
                                                                            TupleConcat("auto_min_size"),
                                                                            Para.CreatePara.MinContrast,
                                                                            out m_hvModelId);
                                                    }
                                                }
                                                else
                                                {
                                                    HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                         Para.CreatePara.StartPhi,
                                                                         Para.CreatePara.PhiExtend, "auto",
                                                                         Para.CreatePara.RowScaleMin,
                                                                         Para.CreatePara.RowScaleMax,
                                                                         Para.CreatePara.RowScaleStep,
                                                                         Para.CreatePara.ColScaleMin,
                                                                         Para.CreatePara.ColScaleMax,
                                                                         Para.CreatePara.ColScaleStep,
                                                                         Para.CreatePara.Optimize,
                                                                         Para.CreatePara.Polarity,
                                                                         (new HTuple("auto_contrast")).
                                                                         TupleConcat("auto_min_size"),
                                                                         Para.CreatePara.MinContrast,
                                                                         out m_hvModelId);
                                                }
                                            }
                                            else
                                            {
                                                HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                        Para.CreatePara.StartPhi,
                                                                        Para.CreatePara.PhiExtend,
                                                                        Para.CreatePara.PhiStep,
                                                                        Para.CreatePara.RowScaleMin,
                                                                        Para.CreatePara.RowScaleMax,
                                                                        Para.CreatePara.RowScaleStep,
                                                                        Para.CreatePara.ColScaleMin,
                                                                        Para.CreatePara.ColScaleMax,
                                                                        Para.CreatePara.ColScaleStep,
                                                                        Para.CreatePara.Optimize,
                                                                        Para.CreatePara.Polarity,
                                                                        (new HTuple("auto_contrast")).
                                                                        TupleConcat("auto_min_size"),
                                                                        Para.CreatePara.MinContrast,
                                                                        out m_hvModelId);
                                            }
                                        }
                                        else
                                        {
                                            HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                     Para.CreatePara.StartPhi,
                                                                     Para.CreatePara.PhiExtend,
                                                                     Para.CreatePara.PhiStep,
                                                                     Para.CreatePara.RowScaleMin,
                                                                     Para.CreatePara.RowScaleMax,
                                                                     Para.CreatePara.RowScaleStep,
                                                                     Para.CreatePara.ColScaleMin,
                                                                     Para.CreatePara.ColScaleMax,
                                                                     Para.CreatePara.ColScaleStep,
                                                                     Para.CreatePara.Optimize,
                                                                     Para.CreatePara.Polarity,
                                                                    (new HTuple("auto_contrast")).
                                                                    TupleConcat(Para.CreatePara.
                                                                    MinSizeComponent),
                                                                    Para.CreatePara.MinContrast,
                                                                    out m_hvModelId);
                                        }
                                    }
                                    else
                                    {
                                        if (Para.CreatePara.MinComponentAuto)
                                        {
                                            if (Para.CreatePara.PhiStepAuto)
                                            {
                                                if (Para.CreatePara.RowScaleStepAuto)
                                                {
                                                    if (Para.CreatePara.ColStepAuto)
                                                    {
                                                        if (Para.CreatePara.MinContrastAuto)
                                                        {
                                                            HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                              Para.CreatePara.StartPhi,
                                                                              Para.CreatePara.PhiExtend, "auto",
                                                                              Para.CreatePara.RowScaleMin,
                                                                              Para.CreatePara.RowScaleMax, "auto",
                                                                              Para.CreatePara.ColScaleMin,
                                                                              Para.CreatePara.ColScaleMax, "auto",
                                                                              Para.CreatePara.Optimize,
                                                                              Para.CreatePara.Polarity,
                                                                               (new HTuple(Para.CreatePara.LowContrast)).
                                                                               TupleConcat(Para.CreatePara.HighContrast).
                                                                               TupleConcat("auto_min_size"),
                                                                              "auto", out m_hvModelId);
                                                        }
                                                        else
                                                        {
                                                            HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                             Para.CreatePara.StartPhi,
                                                                             Para.CreatePara.PhiExtend, "auto",
                                                                             Para.CreatePara.RowScaleMin,
                                                                             Para.CreatePara.RowScaleMax, "auto",
                                                                             Para.CreatePara.ColScaleMin,
                                                                             Para.CreatePara.ColScaleMax, "auto",
                                                                             Para.CreatePara.Optimize,
                                                                             Para.CreatePara.Polarity,
                                                                              (new HTuple(Para.CreatePara.LowContrast)).
                                                                              TupleConcat(Para.CreatePara.HighContrast).
                                                                              TupleConcat("auto_min_size"),
                                                                             Para.CreatePara.MinContrast,
                                                                             out m_hvModelId);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                            Para.CreatePara.StartPhi,
                                                                            Para.CreatePara.PhiExtend, "auto",
                                                                            Para.CreatePara.RowScaleMin,
                                                                            Para.CreatePara.RowScaleMax, "auto",
                                                                            Para.CreatePara.ColScaleMin,
                                                                            Para.CreatePara.ColScaleMax,
                                                                            Para.CreatePara.ColScaleStep,
                                                                            Para.CreatePara.Optimize,
                                                                            Para.CreatePara.Polarity,
                                                                            (new HTuple(Para.CreatePara.LowContrast)).
                                                                            TupleConcat(Para.CreatePara.HighContrast).
                                                                            TupleConcat("auto_min_size"),
                                                                            Para.CreatePara.MinContrast,
                                                                            out m_hvModelId);
                                                    }
                                                }
                                                else
                                                {
                                                    HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                         Para.CreatePara.StartPhi,
                                                                         Para.CreatePara.PhiExtend, "auto",
                                                                         Para.CreatePara.RowScaleMin,
                                                                         Para.CreatePara.RowScaleMax,
                                                                         Para.CreatePara.RowScaleStep,
                                                                         Para.CreatePara.ColScaleMin,
                                                                         Para.CreatePara.ColScaleMax,
                                                                         Para.CreatePara.ColScaleStep,
                                                                         Para.CreatePara.Optimize,
                                                                         Para.CreatePara.Polarity,
                                                                         (new HTuple("auto_contrast")).
                                                                         TupleConcat("auto_min_size"),
                                                                         Para.CreatePara.MinContrast,
                                                                         out m_hvModelId);
                                                }
                                            }
                                            else
                                            {
                                                HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                        Para.CreatePara.StartPhi,
                                                                        Para.CreatePara.PhiExtend,
                                                                        Para.CreatePara.PhiStep,
                                                                        Para.CreatePara.RowScaleMin,
                                                                        Para.CreatePara.RowScaleMax,
                                                                        Para.CreatePara.RowScaleStep,
                                                                        Para.CreatePara.ColScaleMin,
                                                                        Para.CreatePara.ColScaleMax,
                                                                        Para.CreatePara.ColScaleStep,
                                                                        Para.CreatePara.Optimize,
                                                                        Para.CreatePara.Polarity,
                                                                        (new HTuple("auto_contrast")).
                                                                        TupleConcat("auto_min_size"),
                                                                        Para.CreatePara.MinContrast,
                                                                        out m_hvModelId);
                                            }
                                        }
                                        else
                                        {
                                            HOperatorSet.CreateAnisoShapeModel(hoReducedImage, 0,
                                                                     Para.CreatePara.StartPhi,
                                                                     Para.CreatePara.PhiExtend,
                                                                     Para.CreatePara.PhiStep,
                                                                     Para.CreatePara.RowScaleMin,
                                                                     Para.CreatePara.RowScaleMax,
                                                                     Para.CreatePara.RowScaleStep,
                                                                     Para.CreatePara.ColScaleMin,
                                                                     Para.CreatePara.ColScaleMax,
                                                                     Para.CreatePara.ColScaleStep,
                                                                     Para.CreatePara.Optimize,
                                                                     Para.CreatePara.Polarity,
                                                                     (new HTuple("auto_contrast")).
                                                                     TupleConcat(Para.CreatePara.
                                                                     MinSizeComponent),
                                                                     Para.CreatePara.MinContrast,
                                                                     out m_hvModelId);
                                        }
                                    }
                                }
                                else
                                {
                                    if (Para.CreatePara.ContrastAuto)
                                    {
                                        if (Para.CreatePara.MinComponentAuto)
                                        {
                                            if (Para.CreatePara.PhiStepAuto)
                                            {
                                                if (Para.CreatePara.RowScaleStepAuto)
                                                {
                                                    if (Para.CreatePara.ColStepAuto)
                                                    {
                                                        if (Para.CreatePara.MinContrastAuto)
                                                        {
                                                            HOperatorSet.CreateAnisoShapeModel(hoReducedImage,
                                                                              Para.CreatePara.NumLevel,
                                                                              Para.CreatePara.StartPhi,
                                                                              Para.CreatePara.PhiExtend, "auto",
                                                                              Para.CreatePara.RowScaleMin,
                                                                              Para.CreatePara.RowScaleMax, "auto",
                                                                              Para.CreatePara.ColScaleMin,
                                                                              Para.CreatePara.ColScaleMax, "auto",
                                                                              Para.CreatePara.Optimize,
                                                                              Para.CreatePara.Polarity,
                                                                              (new HTuple("auto_contrast")).
                                                                              TupleConcat("auto_min_size"),
                                                                              "auto", out m_hvModelId);
                                                        }
                                                        else
                                                        {
                                                            HOperatorSet.CreateAnisoShapeModel(hoReducedImage,
                                                                             Para.CreatePara.NumLevel,
                                                                             Para.CreatePara.StartPhi,
                                                                             Para.CreatePara.PhiExtend, "auto",
                                                                             Para.CreatePara.RowScaleMin,
                                                                             Para.CreatePara.RowScaleMax, "auto",
                                                                             Para.CreatePara.ColScaleMin,
                                                                             Para.CreatePara.ColScaleMax, "auto",
                                                                             Para.CreatePara.Optimize,
                                                                             Para.CreatePara.Polarity,
                                                                            (new HTuple("auto_contrast")).
                                                                            TupleConcat("auto_min_size"),
                                                                             Para.CreatePara.MinContrast,
                                                                             out m_hvModelId);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        HOperatorSet.CreateAnisoShapeModel(hoReducedImage,
                                                                            Para.CreatePara.NumLevel,
                                                                            Para.CreatePara.StartPhi,
                                                                            Para.CreatePara.PhiExtend, "auto",
                                                                            Para.CreatePara.RowScaleMin,
                                                                            Para.CreatePara.RowScaleMax, "auto",
                                                                            Para.CreatePara.ColScaleMin,
                                                                            Para.CreatePara.ColScaleMax,
                                                                            Para.CreatePara.ColScaleStep,
                                                                            Para.CreatePara.Optimize,
                                                                            Para.CreatePara.Polarity,
                                                                            (new HTuple("auto_contrast")).
                                                                            TupleConcat("auto_min_size"),
                                                                            Para.CreatePara.MinContrast,
                                                                            out m_hvModelId);
                                                    }
                                                }
                                                else
                                                {
                                                    HOperatorSet.CreateAnisoShapeModel(hoReducedImage,
                                                                         Para.CreatePara.NumLevel,
                                                                         Para.CreatePara.StartPhi,
                                                                         Para.CreatePara.PhiExtend, "auto",
                                                                         Para.CreatePara.RowScaleMin,
                                                                         Para.CreatePara.RowScaleMax,
                                                                         Para.CreatePara.RowScaleStep,
                                                                         Para.CreatePara.ColScaleMin,
                                                                         Para.CreatePara.ColScaleMax,
                                                                         Para.CreatePara.ColScaleStep,
                                                                         Para.CreatePara.Optimize,
                                                                         Para.CreatePara.Polarity,
                                                                         (new HTuple("auto_contrast")).
                                                                         TupleConcat("auto_min_size"),
                                                                         Para.CreatePara.MinContrast,
                                                                         out m_hvModelId);
                                                }
                                            }
                                            else
                                            {
                                                HOperatorSet.CreateAnisoShapeModel(hoReducedImage,
                                                                        Para.CreatePara.NumLevel,
                                                                        Para.CreatePara.StartPhi,
                                                                        Para.CreatePara.PhiExtend,
                                                                        Para.CreatePara.PhiStep,
                                                                        Para.CreatePara.RowScaleMin,
                                                                        Para.CreatePara.RowScaleMax,
                                                                        Para.CreatePara.RowScaleStep,
                                                                        Para.CreatePara.ColScaleMin,
                                                                        Para.CreatePara.ColScaleMax,
                                                                        Para.CreatePara.ColScaleStep,
                                                                        Para.CreatePara.Optimize,
                                                                        Para.CreatePara.Polarity,
                                                                        (new HTuple("auto_contrast")).
                                                                        TupleConcat("auto_min_size"),
                                                                        Para.CreatePara.MinContrast,
                                                                        out m_hvModelId);
                                            }
                                        }
                                        else
                                        {
                                            HOperatorSet.CreateAnisoShapeModel(hoReducedImage,
                                                                     Para.CreatePara.NumLevel,
                                                                     Para.CreatePara.StartPhi,
                                                                     Para.CreatePara.PhiExtend,
                                                                     Para.CreatePara.PhiStep,
                                                                     Para.CreatePara.RowScaleMin,
                                                                     Para.CreatePara.RowScaleMax,
                                                                     Para.CreatePara.RowScaleStep,
                                                                     Para.CreatePara.ColScaleMin,
                                                                     Para.CreatePara.ColScaleMax,
                                                                     Para.CreatePara.ColScaleStep,
                                                                     Para.CreatePara.Optimize,
                                                                     Para.CreatePara.Polarity,
                                                                    (new HTuple("auto_contrast")).
                                                                     TupleConcat(Para.CreatePara.
                                                                     MinSizeComponent),
                                                                     Para.CreatePara.MinContrast,
                                                                     out m_hvModelId);
                                        }
                                    }
                                    else
                                    {
                                        HOperatorSet.CreateAnisoShapeModel(hoReducedImage,
                                            Para.CreatePara.NumLevel,
                                             Para.CreatePara.StartPhi,
                                             Para.CreatePara.PhiExtend,
                                             Para.CreatePara.PhiStep,
                                             Para.CreatePara.RowScaleMin,
                                             Para.CreatePara.RowScaleMax,
                                             Para.CreatePara.RowScaleStep,
                                             Para.CreatePara.ColScaleMin,
                                             Para.CreatePara.ColScaleMax,
                                             Para.CreatePara.ColScaleStep,
                                             Para.CreatePara.Optimize,
                                             Para.CreatePara.Polarity,
                                            (new HTuple(Para.CreatePara.LowContrast)).
                                            TupleConcat(Para.CreatePara.HighContrast).
                                             TupleConcat(Para.CreatePara.MinSizeComponent),
                                             Para.CreatePara.MinContrast, out m_hvModelId);
                                    }
                                }
                            }
                            catch (HalconException ex)
                            {
                                bState = false;
                                strMessage = "创建失败！" + ex.Message;
                                return;
                            }
                            #endregion
                            break;
                        case ModelType.轮廓:
                            break;
                    }
                    try
                    {
                        if (null != m_hvModelId && m_hvModelId.Length > 0)
                        {
                           HObject  hoReduced = null;
                            if (Para.FindPara.FindRoiPara.Reg != null)
                            {
                                HOperatorSet.ReduceDomain(m_hoImage, Para.FindPara.FindRoiPara.Reg, out hoReduced);
                            }
                            else
                            {
                                HOperatorSet.CopyImage(m_hoImage, out hoReduced);
                            }
                            HOperatorSet.FindAnisoShapeModel(hoReduced, m_hvModelId, Para.CreatePara.StartPhi,
                                                            Para.CreatePara.PhiExtend, Para.CreatePara.RowScaleMin,
                                                            Para.CreatePara.RowScaleMax, Para.CreatePara.ColScaleMin,
                                                            Para.CreatePara.ColScaleMax, Para.FindPara.MinScore,
                                                            1, Para.FindPara.MaxOver,
                                                            Para.FindPara.SubPix, Para.FindPara.FindNumLevel,
                                                            Para.FindPara.Greed, out m_hvRowCenter,
                                                            out m_hvColCenter, out m_hvAngle, out m_hvRowScale,
                                                            out m_hvColScale, out m_hvScore);
                        }
                        else
                        {
                            bState = false;
                            strMessage = "模板创建失败！";
                            m_StrMessage = strMessage;
                            return;
                        }
                        if (null != m_hvRowCenter && m_hvRowCenter.Length > 0)
                        {
                            HOperatorSet.GetShapeModelContours(out hoContour, m_hvModelId, 1);
                            HOperatorSet.VectorAngleToRigid(0, 0, 0, m_hvRowCenter[0].D,
                                         m_hvColCenter[0].D, m_hvAngle[0].D, out hvAffine);
                            Para.CreatePara.RowModel = m_hvRowCenter[0].D;
                            Para.CreatePara.ColModel = m_hvColCenter[0].D;
                            Para.CreatePara.AngleModel = m_hvAngle[0].D;
                            HOperatorSet.AffineTransContourXld(hoContour,
                                         out m_hoCreateContourAffine, hvAffine);
                        }
                        else
                        {
                            bState = false;
                            strMessage = "原图匹配失败！";
                            m_StrMessage = strMessage;
                            return;
                        }
                    }
                    catch (HalconException ex)
                    {
                        bState = false;
                        strMessage = "创建失败！" + ex.Message;
                        m_StrMessage = strMessage;
                        return;
                    }
                }
                else
                {
                    bState = false;
                    strMessage = "创建失败，图片不存在！";
                    m_StrMessage = strMessage;
                    return;
                }
            }
        }

        private double m_dTime = 0;

        /// <summary>
        /// 运行时间
        /// </summary>
        public double RunTime
        {
            get
            {
                return this.m_dTime;
            }
            set
            {
                this.m_dTime = value;
            }
        }

        private string m_StrMessage = "";

        /// <summary>
        /// 错误描述
        /// </summary>
        public string RunMsg
        {
            get
            {
                return m_StrMessage;
            }

            set
            {
                m_StrMessage = value;
            }
        }

        private List<HObjectWithColor> m_listReg = new List<HObjectWithColor>();

        /// <summary>
        /// 存储Reg的List
        /// </summary>
        public List<HObjectWithColor> ListReg
        {
            get
            {
                return m_listReg;
            }
            set
            {
                m_listReg =value;
            }
        }

        /// <summary>
        /// 运行
        /// </summary>
        public bool Run()
        {
            m_listReg = new List<HObjectWithColor>();
               m_dTime = 0;
            m_hoFindCenterCross = null;
            HTuple hvTime1 = null, hvTime2 = null;
            HOperatorSet.CountSeconds(out hvTime1);
            bool bState = false;
            string strMessage = string.Empty;
            m_StrMessage = strMessage;
            HTuple AffineHom = null;
            m_hvaffineTans = null;
            m_hoFindContourAffine = null;
            m_hvRowCenterNow = null;
            m_hvColCenterNow = null;
            m_hvAngleNow = null;
            m_hvRowScaleNow = null;
            m_hvColScaleNow = null;
            m_hvScoreNow = null;
            if (null != m_hoImage)
            {
                try
                {
                    if (null != m_hvModelId)
                    {
                        HObject hoReduced = null;
                        HObject hoContour = null;
                        HTuple hvAffine = null;
                        HOperatorSet.GetShapeModelContours(out hoContour, m_hvModelId, 1);
                        HOperatorSet.VectorAngleToRigid(0, 0, 0, Para.CreatePara.RowModel,
                        Para.CreatePara.ColModel, Para.CreatePara.AngleModel, out hvAffine);
                        HOperatorSet.AffineTransContourXld(hoContour, out m_hoCreateContourAffine, hvAffine);
                        if (Para.FindPara.FindRoiPara.Reg != null)
                        {
                            HOperatorSet.ReduceDomain(m_hoImage, Para.FindPara.FindRoiPara.Reg, out hoReduced);
                        }
                        else
                        {
                            HOperatorSet.CopyImage(m_hoImage, out hoReduced);
                        }
                        HOperatorSet.FindAnisoShapeModel(hoReduced, m_hvModelId,
                                                        Para.CreatePara.StartPhi, Para.CreatePara.PhiExtend,
                                                        Para.CreatePara.RowScaleMin, Para.CreatePara.RowScaleMax,
                                                        Para.CreatePara.ColScaleMin, Para.CreatePara.ColScaleMax,
                                                        Para.FindPara.MinScore, Para.FindPara.FindMaxNum,
                                                        Para.FindPara.MaxOver, new HTuple(Para.FindPara.SubPix).
                                                        TupleConcat("max_deformation " + Para.FindPara.MaxDeformation),
                                                        Para.FindPara.FindNumLevel, Para.FindPara.Greed,
                                                        out m_hvRowCenterNow, out m_hvColCenterNow, out m_hvAngleNow,
                                                        out m_hvRowScaleNow, out m_hvColScaleNow, out m_hvScoreNow);
                    }
                    if (null != m_hvRowScaleNow && m_hvRowScaleNow.Length > 0)
                    {
                        if (m_hvRowScaleNow.Length < 2)
                        {
                            if (Para.CreatePara.RowModel != -100)
                            {
                                HOperatorSet.VectorAngleToRigid(Para.CreatePara.RowModel,
                                           Para.CreatePara.ColModel, Para.CreatePara.AngleModel,
                                           m_hvRowCenterNow[0].D, m_hvColCenterNow[0].D,
                                           m_hvAngleNow[0].D, out AffineHom);
                                if (null != m_hoCreateContourAffine)
                                {
                                    HOperatorSet.GenCrossContourXld(out m_hoFindCenterCross,
                                                 m_hvRowCenterNow[0].D, m_hvColCenterNow[0].D, 10, 1);
                                    HOperatorSet.AffineTransContourXld(m_hoCreateContourAffine,
                                                 out m_hoFindContourAffine, AffineHom);
                                    m_listReg.Add(new HObjectWithColor(m_hoFindContourAffine, "green"));
                                    m_listReg.Add(new HObjectWithColor(m_hoFindCenterCross, "red"));
                                }
                                if (AffineHom != null && AffineHom.Length > 0)
                                {
                                    bState = true;
                                    m_hvaffineTans = AffineHom;
                                    m_dRow = m_hvRowCenterNow[0].D;
                                    m_dCol = m_hvColCenterNow[0].D;
                                    m_dAngle = m_hvAngleNow[0].D;
                                    m_dScaleRow = m_hvRowScaleNow[0].D;
                                    m_dScaleCol = m_hvColScaleNow[0].D;
                                    m_dScore = m_hvScoreNow[0].D;
                                }
                            }
                            else
                            {
                                bState = false;
                                m_dRow = -100;
                                m_dCol = -100;
                                m_dAngle = -100;
                                m_dScaleRow = -100;
                                m_dScaleCol = -100;
                                m_dScore = -10000000;
                                m_hoFindCenterCross = null;
                                strMessage = "请确定模板创建是否成功！！";
                                m_StrMessage = strMessage;
                                HOperatorSet.CountSeconds(out hvTime2);
                                m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                m_listReg = new List<HObjectWithColor>();
                                return bState;
                            }
                        }
                        else
                        {
                            bState = false;
                            m_hoFindCenterCross = null;
                            m_dRow = -100;
                            m_dCol = -100;
                            m_dAngle = -100;
                            m_dScaleRow = -100;
                            m_dScaleCol = -100;
                            m_dScore = -100;
                            strMessage = "匹配到两个以上数据！！";
                            m_StrMessage = strMessage;
                            HOperatorSet.CountSeconds(out hvTime2);
                            m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                            m_listReg = new List<HObjectWithColor>();
                            return bState;
                        }
                    }
                    else
                    {
                        m_dRow = -100;
                        m_dCol = -100;
                        m_dAngle = -100;
                        m_dScaleRow = -100;
                        m_dScaleCol = -100;
                        m_dScore = -100;
                        bState = false;
                        m_hoFindCenterCross = null;
                        strMessage = "匹配失败,请检查匹配参数设置！";
                        m_StrMessage = strMessage;
                        HOperatorSet.CountSeconds(out hvTime2);
                        m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                        m_listReg = new List<HObjectWithColor>();
                        return bState;
                    }
                }
                catch (HalconException ex)
                {
                    bState = false;
                    m_dRow = -100;
                    m_dCol = -100;
                    m_dAngle = -100;
                    m_dScaleRow = -100;
                    m_dScaleCol = -100;
                    m_dScore = -100;
                    m_hoFindCenterCross = null;
                    strMessage = "匹配失败！" + ex.Message;
                    m_StrMessage = strMessage;
                    HOperatorSet.CountSeconds(out hvTime2);
                    m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                    m_listReg = new List<HObjectWithColor>();
                    return bState;
                }
            }
            else
            {
                bState = false;
                strMessage = "匹配失败,图片不存在！";
                m_dRow = -100;
                m_dCol = -100;
                m_dAngle = -100;
                m_dScaleRow = -100;
                m_dScaleCol = -100;
                m_dScore = -100;
                m_hoFindCenterCross = null;
                m_StrMessage = strMessage;
                HOperatorSet.CountSeconds(out hvTime2);
                m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                m_listReg = new List<HObjectWithColor>();
                return bState;
            }
            HOperatorSet.CountSeconds(out hvTime2);
            m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
            return true;
        }
    }
}
