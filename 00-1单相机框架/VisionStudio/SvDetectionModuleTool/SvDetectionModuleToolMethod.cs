using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GVS.HalconDisp.ViewROI.Config;
using HalconDotNet;
using ParaResultAll;
using SvAngleLineLineTool;
using SvAngleLX;
using SvBlobTool;
using SvsFindLineTool;
using SvVisualCorrectionTool;

namespace SvDetectionModuleTool
{
    /// <summary>
    /// 内 容:本类是检测模块方法
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public class SvDetectionModuleToolMethod : ToolInterFace.ToolInterface
    {
        /// <summary>
        /// 角度计算方法
        /// </summary>
        public SvAngleLX.SvAngleLXToolMethod m_AngleLXMethod = new SvAngleLX.SvAngleLXToolMethod();

        /// <summary>
        /// 线线角度工具
        /// </summary>
        public SvAngleLineLineTool.SvAngleLineLineToolMethod m_AngleLLMethod = new SvAngleLineLineTool.SvAngleLineLineToolMethod();

        /// <summary>
        /// 点到直线距离方法
        /// </summary>
        public SvDistancePointLineTool.SvDistancePointLineToolMethod m_SvDistancePointLineToolMethod = new SvDistancePointLineTool.SvDistancePointLineToolMethod();

        /// <summary>
        /// 点点距离方法
        /// </summary>
        public SvDistancePointPointTool.SvDistancePointPointToolMethod m_SvDistancePointPToolMethod = new SvDistancePointPointTool.SvDistancePointPointToolMethod();

        /// <summary>
        /// 找圆方法类
        /// </summary>
        public SvsFindCircleTool.FindCircleMethod m_FindCircleMethod = new SvsFindCircleTool.FindCircleMethod();

        /// <summary>
        /// 找线方法类
        /// </summary>
        public SvsFindLineTool.FindLineMethod m_FindLineMethod = new SvsFindLineTool.FindLineMethod();

        /// <summary>
        /// 匹配方法类
        /// </summary>
        public SvsPatMax.SvsPatMaxMethod m_PatMaxMethod = new SvsPatMax.SvsPatMaxMethod();

        /// <summary>
        /// 匹配方法类集合
        /// </summary>
        public HTuple[] m_ListModelID = new HTuple[100];

        /// <summary>
        /// blob方法类
        /// </summary>
        public SvBlobTool.BlobToolMethod m_BlobMethod = new SvBlobTool.BlobToolMethod();

        /// <summary>
        /// 结果判断方法类
        /// </summary>
        public SvJudgeResultParaTool.SvJudgeResultParaToolMethod m_SvJudgeResultParaToolMethod = new SvJudgeResultParaTool.SvJudgeResultParaToolMethod();

        /// <summary>
        /// 示教工具方法类
        /// </summary>
        public SvVisualCorrectionTool.SvVisualCorrectionToolMethod m_SvVisualCorrectionToolMethod = new SvVisualCorrectionToolMethod();

        private string m_strSendMdg = string.Empty;

        /// <summary>
        /// 发送信息
        /// </summary>
        public string SendMsg
        {
            get
            {
                return m_strSendMdg;
            }
            set
            {
                m_strSendMdg = value;
            }
        }

        /// <summary>
        /// 检测结果类
        /// </summary>
        private List<ParasResultAll> m_ListParaResultAll = new List<ParasResultAll>();

        /// <summary>
        /// 检测结果集合
        /// </summary>
        public List<ParasResultAll> ListParaResultAll
        {
            get
            {
                return m_ListParaResultAll;
            }
            set
            {
                m_ListParaResultAll = value;
            }
        }

        private string m_PatMaxName = "CCD1";

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

        private SvDetectionModuleToolPara m_ModualPara = new SvDetectionModuleToolPara();

        /// <summary>
        /// 模块参数类
        /// </summary>
        public SvDetectionModuleToolPara ModualPara
        {
            get
            {
                return m_ModualPara;
            }
            set
            {
                m_ModualPara = value;
            }
        }

        private HObject m_hoInputImage = null;

        /// <summary>
        /// 输入图像
        /// </summary>
        public HObject InputImage
        {
            get
            {
                m_AngleLXMethod.InputImage = m_hoInputImage;
                m_PatMaxMethod.InputImage = m_hoInputImage;
                m_FindCircleMethod.InputImage = m_hoInputImage;
                m_FindLineMethod.InputImage = m_hoInputImage;
                m_BlobMethod.InputImage = m_hoInputImage;
                m_SvDistancePointLineToolMethod.InputImage = m_hoInputImage;
                m_SvDistancePointPToolMethod.InputImage = m_hoInputImage;
                return m_hoInputImage;
            }

            set
            {
                m_hoInputImage = value;
                m_AngleLXMethod.InputImage = m_hoInputImage;
                m_PatMaxMethod.InputImage = m_hoInputImage;
                m_FindCircleMethod.InputImage = m_hoInputImage;
                m_FindLineMethod.InputImage = m_hoInputImage;
                m_BlobMethod.InputImage = m_hoInputImage;
                m_SvDistancePointLineToolMethod.InputImage = m_hoInputImage;
                m_SvDistancePointPToolMethod.InputImage = m_hoInputImage;
            }
        }

        private List<HObjectWithColor> m_ListReg = new List<HObjectWithColor>();

        /// <summary>
        /// 结果区域
        /// </summary>
        public List<HObjectWithColor> ListReg
        {
            get
            {
                return m_ListReg;
            }

            set
            {
                m_ListReg = value;
            }
        }

        private List<HObjectWithColor> m_listBlobReg = new List<HObjectWithColor>();

        /// <summary>
        /// Blob区域
        /// </summary>
        public List<HObjectWithColor> ListBlobReg
        {
            get
            {
                return m_listBlobReg;
            }
            set
            {
                m_listBlobReg = value;
            }
        }

        private List<HObjectWithColor> m_listDistancePLReg = new List<HObjectWithColor>();

        /// <summary>
        /// 点线距离区域
        /// </summary>
        public List<HObjectWithColor> ListDistancePLReg
        {
            get
            {
                return m_listDistancePLReg;
            }
            set
            {
                m_listDistancePLReg = value;
            }
        }

        private List<HObjectWithColor> m_listAngleLXReg = new List<HObjectWithColor>();

        /// <summary>
        /// 角度计算区域
        /// </summary>
        public List<HObjectWithColor> ListAngleLXReg
        {
            get
            {
                return m_listAngleLXReg;
            }
            set
            {
                m_listAngleLXReg = value;
            }
        }

        private List<HObjectWithColor> m_listAngleLLReg = new List<HObjectWithColor>();

        /// <summary>
        /// 线线角度计算区域
        /// </summary>
        public List<HObjectWithColor> ListAngleLLReg
        {
            get
            {
                return m_listAngleLLReg;
            }
            set
            {
                m_listAngleLLReg = value;
            }
        }


        private List<HObjectWithColor> m_listDistancePPReg = new List<HObjectWithColor>();

        /// <summary>
        /// 点点距离计算区域
        /// </summary>
        public List<HObjectWithColor> ListDistancePPReg
        {
            get
            {
                return m_listDistancePPReg;
            }
            set
            {
                m_listDistancePPReg = value;
            }
        }

        private List<HObjectWithColor> m_listPatMaxReg = new List<HObjectWithColor>();

        /// <summary>
        /// PatMax区域
        /// </summary>
        public List<HObjectWithColor> ListPatMaxReg
        {
            get
            {
                return m_listPatMaxReg;
            }
            set
            {
                m_listPatMaxReg = value;
            }
        }

        private List<HObjectWithColor> m_listCircleReg = new List<HObjectWithColor>();

        /// <summary>
        /// Circle区域
        /// </summary>
        public List<HObjectWithColor> ListCircleReg
        {
            get
            {
                return m_listCircleReg;
            }
            set
            {
                m_listCircleReg = value;
            }
        }

        private List<HObjectWithColor> m_listLineReg = new List<HObjectWithColor>();

        /// <summary>
        /// Line区域
        /// </summary>
        public List<HObjectWithColor> ListLineReg
        {
            get
            {
                return m_listLineReg;
            }
            set
            {
                m_listLineReg = value;
            }
        }

        /// <summary>
        /// 模块名
        /// </summary>
        public string ModualFormName
        {
            get;
            set;
        }

        private string m_strRunMsg = string.Empty;

        /// <summary>
        /// 运行消息
        /// </summary>
        public string RunMsg
        {
            get
            {
                return m_strRunMsg;
            }
            set
            {
                m_strRunMsg = value;
            }
        }

        private double m_dRunTime = 0;

        /// <summary>
        /// 运行时间
        /// </summary>
        public double RunTime
        {
            get
            {
                return m_dRunTime;
            }

            set
            {
                m_dRunTime = value;
            }
        }

        private bool m_bStateAnglelx = true;

        /// <summary>
        /// 角度计算是否启用
        /// </summary>
        public bool StateAngleLX
        {
            get
            {
                return m_bStateAnglelx;
            }

            set
            {
                m_bStateAnglelx = value;
            }
        }

        private bool m_bStateAngleLL = true;

        /// <summary>
        /// 线线角度计算是否启用
        /// </summary>
        public bool StateAngleLL
        {
            get
            {
                return m_bStateAngleLL;
            }

            set
            {
                m_bStateAngleLL = value;
            }
        }

        private bool m_bStateFindLine = true;

        /// <summary>
        /// 找线是否启用
        /// </summary>
        public bool StateFindLine
        {
            get
            {
                return m_bStateFindLine;
            }

            set
            {
                m_bStateFindLine = value;
            }
        }

        private bool m_bStateFindCircle = true;

        /// <summary>
        /// 找圆是否启用
        /// </summary>
        public bool StateFindCircle
        {
            get
            {
                return m_bStateFindCircle;
            }

            set
            {
                m_bStateFindCircle = value;
            }
        }

        private bool m_bStateBlob = true;

        /// <summary>
        /// Blob是否启用
        /// </summary>
        public bool StateBlob
        {
            get
            {
                return m_bStateBlob;
            }

            set
            {
                m_bStateBlob = value;
            }
        }

        private bool m_bDistancePL = true;

        /// <summary>
        /// 距离计算是否启用
        /// </summary>
        public bool StateDistancePL
        {
            get
            {
                return m_bDistancePL;
            }

            set
            {
                m_bDistancePL = value;
            }
        }

        private bool m_bDistancePP = true;

        /// <summary>
        /// 点点距离计算是否启用
        /// </summary>
        public bool StateDistancePP
        {
            get
            {
                return m_bDistancePP;
            }

            set
            {
                m_bDistancePP = value;
            }
        }

        private bool m_bJudgeResult = true;

        /// <summary>
        /// 结果判断是否启用
        /// </summary>
        public bool StateJudgeResult
        {
            get
            {
                return m_bJudgeResult;
            }

            set
            {
                m_bJudgeResult = value;
            }
        }

        private bool m_bVisualCorrect = true;

        /// <summary>
        /// 示教是否启用
        /// </summary>
        public bool StateVisualCorrect
        {
            get
            {
                return m_bVisualCorrect;
            }

            set
            {
                m_bVisualCorrect = value;
            }
        }

        private HTuple m_hvSendTuple = null;

        /// <summary>
        /// 数据发送
        /// </summary>
        public HTuple SendHTuple
        {
            get
            {
                return m_hvSendTuple;
            }

            set
            {
                m_hvSendTuple = value;
            }
        }

        /// <summary>
        /// 加载方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public object Load(Type type, string filename)
        {
            HTuple hvModelId = null;
            HObject hoCreateContourAffine = null;
            FileStream fs = null;
            try
            {
                string strarry = filename.Substring(0, filename.LastIndexOf("\\"));
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                if (File.Exists(strarry + "\\SingleCameraModualModelID.shm"))
                {
                    hvModelId = null;
                    HOperatorSet.ReadShapeModel(strarry + "\\SingleCameraModualModelID.shm",
                                                out hvModelId);
                    m_PatMaxMethod.ModelID = hvModelId;
                }
                if (File.Exists(strarry + "\\SingleModualCreateContour.hobj"))
                {
                    hoCreateContourAffine = null;
                    HOperatorSet.ReadObject(out hoCreateContourAffine,
                                            strarry + "\\SingleModualCreateContour.hobj");
                    m_PatMaxMethod.CreateContourModel = hoCreateContourAffine;
                }
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                m_strRunMsg = ex.ToString();
                m_ModualPara = new SvDetectionModuleToolPara();
                return m_ModualPara;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// 运行方法
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            bool bState = true;
            HOperatorSet.SetSystem("parallelize_operators", "true");
            HOperatorSet.SetSystem("border_shape_models", "true");
            m_hvSendTuple = new HTuple();
            m_strSendMdg = string.Empty;
            m_ListParaResultAll = new List<ParasResultAll>();
            ParasResultAll paraResult = new ParasResultAll();
            m_strRunMsg = string.Empty;
            m_dRunTime = 0;
            m_listDistancePLReg = new List<HObjectWithColor>();
            m_listAngleLXReg = new List<HObjectWithColor>();
            m_ListReg = new List<HObjectWithColor>();
            m_listBlobReg = new List<HObjectWithColor>();
            m_listCircleReg = new List<HObjectWithColor>();
            m_listLineReg = new List<HObjectWithColor>();
            m_listPatMaxReg = new List<HObjectWithColor>();
            m_listDistancePPReg = new List<HObjectWithColor>();
            m_listAngleLLReg = new List<HObjectWithColor>();
            HTuple hvTime1 = null, hvTime2 = null;
            HOperatorSet.CountSeconds(out hvTime1);
            if (m_hoInputImage != null)
            {
                for (int iNow = 0; iNow < m_ModualPara.ListToolParaAdd.Count; iNow++)
                {
                    int iBefore = iNow - 1;
                    switch (m_ModualPara.ListToolParaAdd[iNow].ToolName)
                    {
                        case "匹配工具":

                            #region  只有一个模块，且该模块为匹配工具时
                            //   if (m_ModualPara.ListToolParaAdd.Count == 1)
                            {
                                m_PatMaxMethod.InputImage = m_hoInputImage;
                                m_PatMaxMethod.Para = new SvsPatMax.SvsPatMaxPara();
                                m_PatMaxMethod.Para = m_ModualPara.ListToolParaAdd[iNow].PatMaxPara;
                                m_PatMaxMethod.Para.CreatePara = m_ModualPara.ListToolParaAdd[iNow].PatMaxPara.CreatePara;
                                m_PatMaxMethod.ModelID = m_ListModelID[iNow];
                                bState = m_PatMaxMethod.Run();
                                if (!bState)
                                {
                                    m_listAngleLXReg = new List<HObjectWithColor>();
                                    m_ListReg = new List<HObjectWithColor>();
                                    m_listBlobReg = new List<HObjectWithColor>();
                                    m_listCircleReg = new List<HObjectWithColor>();
                                    m_listLineReg = new List<HObjectWithColor>();
                                    m_listPatMaxReg = new List<HObjectWithColor>();
                                    HOperatorSet.CountSeconds(out hvTime2);
                                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                    m_strRunMsg = "匹配模块" + iNow + m_PatMaxMethod.RunMsg;
                                    //return bState;
                                }
                                if (bState)
                                {
                                    m_ListReg.Add(new HObjectWithColor(m_PatMaxMethod.FindContourModel, "green"));
                                    m_ListReg.Add(new HObjectWithColor(m_PatMaxMethod.FindCenterCross, "green"));
                                    m_listPatMaxReg.Add(new HObjectWithColor(m_PatMaxMethod.FindContourModel, "green"));
                                    m_listPatMaxReg.Add(new HObjectWithColor(m_PatMaxMethod.FindCenterCross, "green"));
                                }

                                // 模板结果赋值
                                paraResult = new ParasResultAll();
                                paraResult.ToolName = m_ModualPara.ListToolParaAdd[iNow].ToolName + iNow;
                                paraResult.PatMaxResultPara.Row = m_PatMaxMethod.Row;
                                paraResult.PatMaxResultPara.Col = m_PatMaxMethod.Col;
                                paraResult.PatMaxResultPara.Angle = m_PatMaxMethod.Angle;
                                paraResult.PatMaxResultPara.Affinehom = m_PatMaxMethod.HvAffinetrans;
                                m_ListParaResultAll.Add(paraResult);
                            }
                            #endregion

                            break;
                        case "找线工具":

                            #region 只有一个工具，且工具为找线工具时
                            if (m_bStateFindLine)
                            {
                                m_FindLineMethod.InputImage = m_hoInputImage;
                                m_FindLineMethod.Para = new SvsFindLineTool.FindLinePara();
                                m_FindLineMethod.Para = m_ModualPara.ListToolParaAdd[iNow].FindLineParas;
                                m_FindLineMethod.Para.LineCalliperParas = m_ModualPara.ListToolParaAdd[iNow].FindLineParas.LineCalliperParas;
                                m_FindLineMethod.ListParaResultAll = m_ListParaResultAll;
                                bState = m_FindLineMethod.Run();
                                m_ModualPara.ListToolParaAdd[iNow].FindLineParas = new FindLinePara();
                                m_ModualPara.ListToolParaAdd[iNow].FindLineParas = m_FindLineMethod.Para;
                                m_ModualPara.ListToolParaAdd[iNow].FindLineParas.LineCalliperParas = m_FindLineMethod.Para.LineCalliperParas;
                                if (!bState)
                                {
                                    m_listAngleLXReg = new List<HObjectWithColor>();
                                    m_ListReg = new List<HObjectWithColor>();
                                    m_listBlobReg = new List<HObjectWithColor>();
                                    m_listCircleReg = new List<HObjectWithColor>();
                                    m_listLineReg = new List<HObjectWithColor>();
                                    m_listPatMaxReg = new List<HObjectWithColor>();
                                    HOperatorSet.CountSeconds(out hvTime2);
                                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                    m_strRunMsg = "找线模块" + iNow + m_FindLineMethod.RunMsg;
                                    //return bState;
                                }
                                if (bState)
                                {
                                    m_ListReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                                    m_listLineReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                                }

                                // 最后将当前找线工具的结果信息添加到结果类中
                                paraResult = new ParasResultAll();
                                paraResult.ToolName = m_ModualPara.ListToolParaAdd[iNow].ToolName + iNow;
                                paraResult.FindLineResultPara.RowStart = m_FindLineMethod.RowStart;
                                paraResult.FindLineResultPara.ColStart = m_FindLineMethod.ColStart;
                                paraResult.FindLineResultPara.RowEnd = m_FindLineMethod.RowEnd;
                                paraResult.FindLineResultPara.ColEnd = m_FindLineMethod.ColEnd;
                                paraResult.FindLineResultPara.RowMiddle = m_FindLineMethod.RowMiddle;
                                paraResult.FindLineResultPara.ColMiddle = m_FindLineMethod.ColMiddle;
                                m_ListParaResultAll.Add(paraResult);
                            }
                            #endregion

                            #region 有多个工具，当前索引工具为找线工具时
                            //else if (iNow > 0)
                            //{
                            //    #region 当前找线工具之前有匹配工具时
                            //    for (int j = 0; j < iNow; j++)
                            //    {
                            //        switch (m_ModualPara.ListToolParaAdd[j].ToolName)
                            //        {
                            //            case "匹配工具":
                            //                m_PatMaxMethod.InputImage = m_hoInputImage;
                            //                m_PatMaxMethod.Para = new SvsPatMax.SvsPatMaxPara();
                            //                m_PatMaxMethod.Para = m_ModualPara.ListToolParaAdd[j].PatMaxPara;
                            //                m_PatMaxMethod.Para.CreatePara = m_ModualPara.ListToolParaAdd[j].PatMaxPara.CreatePara;
                            //                m_PatMaxMethod.ModelID = m_ListModelID[j];
                            //                bState = m_PatMaxMethod.Run();
                            //                if (!bState)
                            //                {
                            //                    m_listAngleLXReg = new List<HObjectWithColor>();
                            //                    m_ListReg = new List<HObjectWithColor>();
                            //                    m_listBlobReg = new List<HObjectWithColor>();
                            //                    m_listCircleReg = new List<HObjectWithColor>();
                            //                    m_listLineReg = new List<HObjectWithColor>();
                            //                    m_listPatMaxReg = new List<HObjectWithColor>();
                            //                    HOperatorSet.CountSeconds(out hvTime2);
                            //                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                            //                    m_strRunMsg = "匹配模块" + j + m_PatMaxMethod.RunMsg;
                            //                    // return bState;
                            //                }
                            //                if (bState)
                            //                {
                            //                    m_ListReg.Add(new HObjectWithColor(m_PatMaxMethod.FindContourModel, "green"));
                            //                    m_ListReg.Add(new HObjectWithColor(m_PatMaxMethod.FindCenterCross, "green"));
                            //                    m_listPatMaxReg.Add(new HObjectWithColor(m_PatMaxMethod.FindContourModel, "green"));
                            //                    m_listPatMaxReg.Add(new HObjectWithColor(m_PatMaxMethod.FindCenterCross, "green"));
                            //                }

                            //                m_FindLineMethod.AffineTans = null;
                            //               // m_FindLineMethod.ListParaResultAll = m_ListParaResultAll;
                            //               // m_FindLineMethod.AffineTans = m_PatMaxMethod.HvAffinetrans;
                            //                m_FindLineMethod.InputImage = m_hoInputImage;
                            //                m_FindLineMethod.Para = new SvsFindLineTool.FindLinePara();
                            //                m_FindLineMethod.Para = m_ModualPara.ListToolParaAdd[iNow].FindLineParas;
                            //                if (m_ModualPara.ListToolParaAdd[iNow].RowState)
                            //                {
                            //                    m_FindLineMethod.Para.LineCalliperParas.RowBegin = m_PatMaxMethod.Row;
                            //                }
                            //                if (m_ModualPara.ListToolParaAdd[iNow].ColumnState)
                            //                {
                            //                    m_FindLineMethod.Para.LineCalliperParas.ColumnBegin = m_PatMaxMethod.Col;
                            //                }
                            //                if (m_ModualPara.ListToolParaAdd[iNow].AngleState)
                            //                {
                            //                    m_FindLineMethod.Para.LineCalliperParas.Phi = m_PatMaxMethod.Angle;
                            //                }
                            //                bState = m_FindLineMethod.Run();
                            //                if (!bState)
                            //                {
                            //                    m_listAngleLXReg = new List<HObjectWithColor>();
                            //                    m_ListReg = new List<HObjectWithColor>();
                            //                    m_listBlobReg = new List<HObjectWithColor>();
                            //                    m_listCircleReg = new List<HObjectWithColor>();
                            //                    m_listLineReg = new List<HObjectWithColor>();
                            //                    m_listPatMaxReg = new List<HObjectWithColor>();
                            //                    HOperatorSet.CountSeconds(out hvTime2);
                            //                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                            //                    m_strRunMsg = "找线模块" + iNow + m_FindLineMethod.RunMsg;
                            //                    // return bState;
                            //                }
                            //                if (bState)
                            //                {
                            //                    m_ListReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                            //                    m_listLineReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                            //                }
                            //                break;
                            //        }
                            //    }
                            //    #endregion

                            //    #region 找线工具的前一个工具信息判断
                            //    switch (m_ModualPara.ListToolParaAdd[iBefore].ToolName)
                            //    {
                            //        #region 找线工具的前一个为找线工具时
                            //        case "找线工具":
                            //            m_FindLineMethod.InputImage = m_hoInputImage;
                            //            m_FindLineMethod.Para = new SvsFindLineTool.FindLinePara();
                            //            m_FindLineMethod.Para = m_ModualPara.ListToolParaAdd[iBefore].FindLineParas;
                            //            //m_FindLineMethod.ListParaResultAll = m_ListParaResultAll;
                            //            bState = m_FindLineMethod.Run();
                            //            if (!bState)
                            //            {
                            //                m_listAngleLXReg = new List<HObjectWithColor>();
                            //                m_ListReg = new List<HObjectWithColor>();
                            //                m_listBlobReg = new List<HObjectWithColor>();
                            //                m_listCircleReg = new List<HObjectWithColor>();
                            //                m_listLineReg = new List<HObjectWithColor>();
                            //                m_listPatMaxReg = new List<HObjectWithColor>();
                            //                HOperatorSet.CountSeconds(out hvTime2);
                            //                m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                            //                m_strRunMsg = "找线模块" + iBefore + m_FindLineMethod.RunMsg;
                            //                // return bState;
                            //            }
                            //            if (bState)
                            //            {
                            //                m_ListReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                            //                m_listLineReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                            //            }

                            //            m_FindLineMethod.InputImage = m_hoInputImage;
                            //            m_FindLineMethod.Para = new SvsFindLineTool.FindLinePara();
                            //            m_FindLineMethod.Para = m_ModualPara.ListToolParaAdd[iNow].FindLineParas;
                            //            if (m_ModualPara.ListToolParaAdd[iNow].RowState)
                            //            {
                            //                m_FindLineMethod.Para.LineCalliperParas.RowBegin = m_FindLineMethod.RowStart;
                            //            }
                            //            if (m_ModualPara.ListToolParaAdd[iNow].ColumnState)
                            //            {
                            //                m_FindLineMethod.Para.LineCalliperParas.ColumnBegin = m_FindLineMethod.ColStart;
                            //            }
                            //           // m_FindLineMethod.ListParaResultAll = m_ListParaResultAll;
                            //            bState = m_FindLineMethod.Run();
                            //            if (!bState)
                            //            {
                            //                m_listAngleLXReg = new List<HObjectWithColor>();
                            //                m_ListReg = new List<HObjectWithColor>();
                            //                m_listBlobReg = new List<HObjectWithColor>();
                            //                m_listCircleReg = new List<HObjectWithColor>();
                            //                m_listLineReg = new List<HObjectWithColor>();
                            //                m_listPatMaxReg = new List<HObjectWithColor>();
                            //                HOperatorSet.CountSeconds(out hvTime2);
                            //                m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                            //                m_strRunMsg = "找线模块" + iNow + m_FindLineMethod.RunMsg;
                            //                // return bState;
                            //            }
                            //            if (bState)
                            //            {
                            //                m_ListReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                            //                m_listLineReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                            //            }
                            //            break;
                            //        #endregion

                            //        #region 找线工具的前一个为找圆工具时
                            //        case "找圆工具":
                            //            m_FindCircleMethod.InputImage = m_hoInputImage;
                            //            m_FindCircleMethod.Para = new SvsFindCircleTool.FindCirclePara();
                            //            m_FindCircleMethod.Para = m_ModualPara.ListToolParaAdd[iBefore].FindCircleParas;
                            //            bState = m_FindCircleMethod.Run();
                            //            if (!bState)
                            //            {
                            //                m_listAngleLXReg = new List<HObjectWithColor>();
                            //                m_ListReg = new List<HObjectWithColor>();
                            //                m_listBlobReg = new List<HObjectWithColor>();
                            //                m_listCircleReg = new List<HObjectWithColor>();
                            //                m_listLineReg = new List<HObjectWithColor>();
                            //                m_listPatMaxReg = new List<HObjectWithColor>();
                            //                HOperatorSet.CountSeconds(out hvTime2);
                            //                m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                            //                m_strRunMsg = "找圆模块" + iBefore + m_FindCircleMethod.RunMsg;
                            //                // return bState;
                            //            }
                            //            if (bState)
                            //            {
                            //                m_ListReg.Add(new HObjectWithColor(m_FindCircleMethod.RegCircle, "red"));
                            //                m_ListReg.Add(new HObjectWithColor(m_FindCircleMethod.RegPoint, "red"));
                            //                m_ListReg.Add(new HObjectWithColor(m_FindCircleMethod.RegCenterCircle, "red"));
                            //                m_listCircleReg.Add(new HObjectWithColor(m_FindCircleMethod.RegCircle, "red"));
                            //                m_listCircleReg.Add(new HObjectWithColor(m_FindCircleMethod.RegPoint, "red"));
                            //                m_listCircleReg.Add(new HObjectWithColor(m_FindCircleMethod.RegCenterCircle, "red"));
                            //            }

                            //            m_FindLineMethod.InputImage = m_hoInputImage;
                            //            m_FindLineMethod.Para = new SvsFindLineTool.FindLinePara();
                            //            m_FindLineMethod.Para = m_ModualPara.ListToolParaAdd[iNow].FindLineParas;
                            //            if (m_ModualPara.ListToolParaAdd[iNow].RowState)
                            //            {
                            //                m_FindLineMethod.Para.LineCalliperParas.RowBegin = m_FindCircleMethod.RowCircle;
                            //            }
                            //            if (m_ModualPara.ListToolParaAdd[iNow].ColumnState)
                            //            {
                            //                m_FindLineMethod.Para.LineCalliperParas.ColumnBegin = m_FindCircleMethod.ColCircle;
                            //            }
                            //            //m_FindLineMethod.ListParaResultAll = m_ListParaResultAll;
                            //            bState = m_FindLineMethod.Run();
                            //            if (!bState)
                            //            {
                            //                m_listAngleLXReg = new List<HObjectWithColor>();
                            //                m_ListReg = new List<HObjectWithColor>();
                            //                m_listBlobReg = new List<HObjectWithColor>();
                            //                m_listCircleReg = new List<HObjectWithColor>();
                            //                m_listLineReg = new List<HObjectWithColor>();
                            //                m_listPatMaxReg = new List<HObjectWithColor>();
                            //                HOperatorSet.CountSeconds(out hvTime2);
                            //                m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                            //                m_strRunMsg = "找线模块" + iNow + m_FindLineMethod.RunMsg;
                            //                // return bState;
                            //            }
                            //            if (bState)
                            //            {
                            //                m_ListReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                            //                m_listLineReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                            //            }
                            //            break;
                            //        #endregion

                            //        #region 找线工具的前一个为blob工具时
                            //        case "Blob工具":
                            //            m_BlobMethod.InputImage = m_hoInputImage;
                            //            m_BlobMethod.ParaBlob = new SvBlobTool.BlobToolPara();
                            //            m_BlobMethod.ParaBlob = m_ModualPara.ListToolParaAdd[iBefore].BlobParas;
                            //            bState = m_BlobMethod.Run();
                            //            if (!bState)
                            //            {
                            //                m_listAngleLXReg = new List<HObjectWithColor>();
                            //                m_ListReg = new List<HObjectWithColor>();
                            //                m_listBlobReg = new List<HObjectWithColor>();
                            //                m_listCircleReg = new List<HObjectWithColor>();
                            //                m_listLineReg = new List<HObjectWithColor>();
                            //                m_listPatMaxReg = new List<HObjectWithColor>();
                            //                HOperatorSet.CountSeconds(out hvTime2);
                            //                m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                            //                m_strRunMsg = "Blob模块" + iBefore + m_BlobMethod.RunMsg;
                            //                // return bState;
                            //            }
                            //            if (bState)
                            //            {
                            //                foreach (ParaResult item in m_BlobMethod.ListParaResultSelect)
                            //                {
                            //                    m_ListReg.Add(new HObjectWithColor(item.HoPointReg, "orange"));
                            //                    m_listBlobReg.Add(new HObjectWithColor(item.HoPointReg, "orange"));
                            //                }
                            //            }

                            //            m_FindLineMethod.InputImage = m_hoInputImage;
                            //            m_FindLineMethod.Para = new SvsFindLineTool.FindLinePara();
                            //            m_FindLineMethod.Para = m_ModualPara.ListToolParaAdd[iNow].FindLineParas;
                            //            if (m_ModualPara.ListToolParaAdd[iNow].RowState)
                            //            {
                            //                if (m_BlobMethod.ListParaResultSelect.Count > 0)
                            //                {
                            //                    m_FindLineMethod.Para.LineCalliperParas.RowBegin = m_BlobMethod.ListParaResultSelect[0].Row;
                            //                }
                            //            }
                            //            if (m_ModualPara.ListToolParaAdd[iNow].ColumnState)
                            //            {
                            //                if (m_BlobMethod.ListParaResultSelect.Count > 0)
                            //                {
                            //                    m_FindLineMethod.Para.LineCalliperParas.ColumnBegin = m_BlobMethod.ListParaResultSelect[0].Col;
                            //                }
                            //            }
                            //           // m_FindLineMethod.ListParaResultAll = m_ListParaResultAll;
                            //            bState = m_FindLineMethod.Run();
                            //            if (!bState)
                            //            {
                            //                m_listAngleLXReg = new List<HObjectWithColor>();
                            //                m_ListReg = new List<HObjectWithColor>();
                            //                m_listBlobReg = new List<HObjectWithColor>();
                            //                m_listCircleReg = new List<HObjectWithColor>();
                            //                m_listLineReg = new List<HObjectWithColor>();
                            //                m_listPatMaxReg = new List<HObjectWithColor>();
                            //                HOperatorSet.CountSeconds(out hvTime2);
                            //                m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                            //                m_strRunMsg = "找线模块" + iNow + m_FindLineMethod.RunMsg;
                            //                // return bState;
                            //            }
                            //            if (bState)
                            //            {
                            //                m_ListReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                            //                m_listLineReg.Add(new HObjectWithColor(m_FindLineMethod.HoLine, "blue"));
                            //            }
                            //            break;
                            //            #endregion
                            //    }
                            //    #endregion

                            //    // 最后将当前找线工具的结果信息添加到结果类中
                            //    paraResult = new ParasResultAll();
                            //    paraResult.ToolName = m_ModualPara.ListToolParaAdd[iNow].ToolName + iNow;
                            //    paraResult.FindLineResultPara.RowStart = m_FindLineMethod.RowStart;
                            //    paraResult.FindLineResultPara.ColStart = m_FindLineMethod.ColStart;
                            //    paraResult.FindLineResultPara.RowEnd = m_FindLineMethod.RowEnd;
                            //    paraResult.FindLineResultPara.ColEnd = m_FindLineMethod.ColEnd;
                            //    paraResult.FindLineResultPara.RowMiddle = m_FindLineMethod.RowMiddle;
                            //    paraResult.FindLineResultPara.ColMiddle = m_FindLineMethod.ColMiddle;
                            //    m_ListParaResultAll.Add(paraResult);
                            //}
                            #endregion

                            break;
                        case "找圆工具":

                            if (m_bStateFindCircle)
                            {
                                m_FindCircleMethod.InputImage = m_hoInputImage;
                                m_FindCircleMethod.Para = new SvsFindCircleTool.FindCirclePara();
                                m_FindCircleMethod.Para = m_ModualPara.ListToolParaAdd[iNow].FindCircleParas;
                                m_FindCircleMethod.Para.CircleCalliper = m_ModualPara.ListToolParaAdd[iNow].FindCircleParas.CircleCalliper;
                                m_FindCircleMethod.ListParaResultAll = m_ListParaResultAll;
                                bState = m_FindCircleMethod.Run();
                                m_ModualPara.ListToolParaAdd[iNow].FindCircleParas = new SvsFindCircleTool.FindCirclePara();
                                m_ModualPara.ListToolParaAdd[iNow].FindCircleParas = m_FindCircleMethod.Para;
                                m_ModualPara.ListToolParaAdd[iNow].FindCircleParas.CircleCalliper = m_FindCircleMethod.Para.CircleCalliper;
                                if (!bState)
                                {
                                    m_listAngleLXReg = new List<HObjectWithColor>();
                                    m_ListReg = new List<HObjectWithColor>();
                                    m_listBlobReg = new List<HObjectWithColor>();
                                    m_listCircleReg = new List<HObjectWithColor>();
                                    m_listLineReg = new List<HObjectWithColor>();
                                    m_listPatMaxReg = new List<HObjectWithColor>();
                                    HOperatorSet.CountSeconds(out hvTime2);
                                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                    m_strRunMsg = "找圆模块" + iNow + m_FindCircleMethod.RunMsg;
                                    //return bState;
                                }



                                if (bState)
                                {
                                    m_ListReg.Add(new HObjectWithColor(m_FindCircleMethod.RegCircle, "red"));
                                    m_ListReg.Add(new HObjectWithColor(m_FindCircleMethod.RegPoint, "red"));
                                    m_ListReg.Add(new HObjectWithColor(m_FindCircleMethod.RegCenterCircle, "red"));
                                    m_listCircleReg.Add(new HObjectWithColor(m_FindCircleMethod.RegCircle, "red"));
                                    m_listCircleReg.Add(new HObjectWithColor(m_FindCircleMethod.RegPoint, "red"));
                                    m_listCircleReg.Add(new HObjectWithColor(m_FindCircleMethod.RegCenterCircle, "red"));
                                }

                                // 最后将当前找圆工具的结果信息添加到结果类中
                                paraResult = new ParasResultAll();
                                paraResult.ToolName = m_ModualPara.ListToolParaAdd[iNow].ToolName + iNow;
                                paraResult.FindCircleResultPara.Row = m_FindCircleMethod.RowCircle;
                                paraResult.FindCircleResultPara.Col = m_FindCircleMethod.ColCircle;
                                paraResult.FindCircleResultPara.Radius = m_FindCircleMethod.RadiusCircle;
                                m_ListParaResultAll.Add(paraResult);
                            }
                            break;

                        case "线线角度工具":

                            if (m_bStateAngleLL)
                            {
                                m_AngleLLMethod.InputImage = m_hoInputImage;
                                m_AngleLLMethod.Para = new SvAngleLineLineTool.SvAngleLineLineToolPara();
                                m_AngleLLMethod.Para = m_ModualPara.ListToolParaAdd[iNow].SvAngleLineLineToolParas;
                                m_AngleLLMethod.ListParaResultAll = m_ListParaResultAll;
                                bState = m_AngleLLMethod.Run();
                                m_ModualPara.ListToolParaAdd[iNow].SvAngleLineLineToolParas = new SvAngleLineLineToolPara();
                                m_ModualPara.ListToolParaAdd[iNow].SvAngleLineLineToolParas = m_AngleLLMethod.Para;
                                if (!bState)
                                {
                                    m_listAngleLLReg = new List<HObjectWithColor>();
                                    m_listAngleLLReg = new List<HObjectWithColor>();
                                    m_listAngleLXReg = new List<HObjectWithColor>();
                                    m_ListReg = new List<HObjectWithColor>();
                                    m_listBlobReg = new List<HObjectWithColor>();
                                    m_listCircleReg = new List<HObjectWithColor>();
                                    m_listLineReg = new List<HObjectWithColor>();
                                    m_listPatMaxReg = new List<HObjectWithColor>();
                                    HOperatorSet.CountSeconds(out hvTime2);
                                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                    m_strRunMsg = "线线角度模块" + iNow + m_AngleLLMethod.RunMsg;
                                    //return bState;
                                }
                                if (bState)
                                {
                                    m_listAngleLLReg.Add(new HObjectWithColor(m_AngleLLMethod.Cross1, "cyan"));
                                    m_listAngleLLReg.Add(new HObjectWithColor(m_AngleLLMethod.Cross2, "cyan"));
                                    m_listAngleLLReg.Add(new HObjectWithColor(m_AngleLLMethod.Cross3, "cyan"));
                                    m_listAngleLLReg.Add(new HObjectWithColor(m_AngleLLMethod.Cross4, "cyan"));
                                    m_listAngleLLReg.Add(new HObjectWithColor(m_AngleLLMethod.Line1, "cyan"));
                                    m_listAngleLLReg.Add(new HObjectWithColor(m_AngleLLMethod.Line2, "cyan"));
                                    m_listAngleLLReg.Add(new HObjectWithColor(m_AngleLLMethod.Crossintersection, "cyan"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLLMethod.Cross1, "cyan"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLLMethod.Cross2, "cyan"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLLMethod.Cross3, "cyan"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLLMethod.Cross4, "cyan"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLLMethod.Line1, "cyan"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLLMethod.Line2, "cyan"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLLMethod.Crossintersection, "cyan"));
                                }

                                // 最后将当前找圆工具的结果信息添加到结果类中
                                paraResult = new ParasResultAll();
                                paraResult.ToolName = m_ModualPara.ListToolParaAdd[iNow].ToolName + iNow;
                                paraResult.ParaParaAngleLL.Row = m_AngleLLMethod.Row;
                                paraResult.ParaParaAngleLL.Col = m_AngleLLMethod.Col;
                                paraResult.ParaParaAngleLL.Angle = m_AngleLLMethod.Angle;
                                m_ListParaResultAll.Add(paraResult);
                            }
                            break;

                        case "Blob工具":
                            if (m_bStateBlob)
                            {
                                m_BlobMethod.InputImage = m_hoInputImage;
                                m_BlobMethod.ParaBlob = new BlobToolPara();
                                m_BlobMethod.ParaBlob = m_ModualPara.ListToolParaAdd[iNow].BlobParas;
                                m_BlobMethod.ListClassParaResultAll = m_ListParaResultAll;
                                bState = m_BlobMethod.Run();
                                m_ModualPara.ListToolParaAdd[iNow].BlobParas = new BlobToolPara();
                                m_ModualPara.ListToolParaAdd[iNow].BlobParas = m_BlobMethod.ParaBlob;
                                if (!bState)
                                {
                                    m_listAngleLXReg = new List<HObjectWithColor>();
                                    m_ListReg = new List<HObjectWithColor>();
                                    m_listBlobReg = new List<HObjectWithColor>();
                                    m_listCircleReg = new List<HObjectWithColor>();
                                    m_listLineReg = new List<HObjectWithColor>();
                                    m_listPatMaxReg = new List<HObjectWithColor>();
                                    HOperatorSet.CountSeconds(out hvTime2);
                                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                    m_strRunMsg = "Blob模块" + iNow + m_BlobMethod.RunMsg;
                                    // return bState;
                                }
                                paraResult = new ParasResultAll();
                                paraResult.ToolName = m_ModualPara.ListToolParaAdd[iNow].ToolName + iNow;
                                paraResult.BlobResultPara.ListBlobResultPara = new List<ParaResult>();
                                if (m_BlobMethod.ListParaResultSelect.Count > 0)
                                {
                                    foreach (ParaResult item in m_BlobMethod.ListParaResultSelect)
                                    {
                                        if (bState)
                                        {
                                            m_ListReg.Add(new HObjectWithColor(item.HoPointReg, "orange"));
                                            m_listBlobReg.Add(new HObjectWithColor(item.HoPointReg, "orange"));
                                        }
                                        ParaResult blobResultPara = new ParaResult();
                                        blobResultPara.Row = item.Row;
                                        blobResultPara.Col = item.Col;
                                        blobResultPara.Area = item.Area;
                                        paraResult.BlobResultPara.ListBlobResultPara.Add(blobResultPara);
                                    }
                                }
                                else
                                {
                                    ParaResult blobResultPara = new ParaResult();
                                    blobResultPara.Row = -100;
                                    blobResultPara.Col = -100;
                                    blobResultPara.Area = -100;
                                    paraResult.BlobResultPara.ListBlobResultPara.Add(blobResultPara);
                                }
                                m_ListParaResultAll.Add(paraResult);
                            }
                            break;
                        case "AngleLX工具":
                            if (m_bStateAnglelx)
                            {
                                #region 角度计算工具
                                m_AngleLXMethod.InputImage = m_hoInputImage;
                                m_AngleLXMethod.ListParaResultAll = m_ListParaResultAll;
                                m_AngleLXMethod.SvAngleLXParas = m_ModualPara.ListToolParaAdd[iNow].AngleLxPara;
                                bState = m_AngleLXMethod.Run();
                                m_ModualPara.ListToolParaAdd[iNow].AngleLxPara = new SvAngleLXToolPara();
                                m_ModualPara.ListToolParaAdd[iNow].AngleLxPara = m_AngleLXMethod.SvAngleLXParas;
                                if (!bState)
                                {
                                    m_listAngleLXReg = new List<HObjectWithColor>();
                                    m_ListReg = new List<HObjectWithColor>();
                                    m_listBlobReg = new List<HObjectWithColor>();
                                    m_listCircleReg = new List<HObjectWithColor>();
                                    m_listLineReg = new List<HObjectWithColor>();
                                    m_listPatMaxReg = new List<HObjectWithColor>();
                                    HOperatorSet.CountSeconds(out hvTime2);
                                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                    m_strRunMsg = "AngleLX模块" + iNow + m_AngleLXMethod.RunMsg;
                                }
                                if (bState)
                                {
                                    m_listAngleLXReg.Add(new HObjectWithColor(m_AngleLXMethod.Line, "coral"));
                                    m_listAngleLXReg.Add(new HObjectWithColor(m_AngleLXMethod.Cross1, "coral"));
                                    m_listAngleLXReg.Add(new HObjectWithColor(m_AngleLXMethod.Cross2, "coral"));
                                    m_listAngleLXReg.Add(new HObjectWithColor(m_AngleLXMethod.CenterLine, "coral"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLXMethod.Line, "coral"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLXMethod.Cross1, "coral"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLXMethod.Cross2, "coral"));
                                    m_ListReg.Add(new HObjectWithColor(m_AngleLXMethod.CenterLine, "coral"));
                                }

                                paraResult = new ParasResultAll();
                                paraResult.ToolName = m_ModualPara.ListToolParaAdd[iNow].ToolName + iNow;
                                paraResult.ParasAngleLXResult.Angle = m_AngleLXMethod.Angle;
                                m_ListParaResultAll.Add(paraResult);
                                #endregion
                            }
                            break;

                        case "点点距离工具":
                            if (m_bDistancePP)
                            {
                                #region 点点距离工具
                                m_SvDistancePointPToolMethod.InputImage = m_hoInputImage;
                                m_SvDistancePointPToolMethod.ListParaResultAll = m_ListParaResultAll;
                                m_SvDistancePointPToolMethod.SvDistancePointPointToolParas = m_ModualPara.ListToolParaAdd[iNow].DistancePointPointToolParas;
                                bState = m_SvDistancePointPToolMethod.Run();
                                if (!bState)
                                {
                                    m_listDistancePPReg = new List<HObjectWithColor>();
                                    m_listAngleLXReg = new List<HObjectWithColor>();
                                    m_ListReg = new List<HObjectWithColor>();
                                    m_listBlobReg = new List<HObjectWithColor>();
                                    m_listCircleReg = new List<HObjectWithColor>();
                                    m_listLineReg = new List<HObjectWithColor>();
                                    m_listPatMaxReg = new List<HObjectWithColor>();
                                    HOperatorSet.CountSeconds(out hvTime2);
                                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                    m_strRunMsg = "点点距离工具模块" + iNow + m_SvDistancePointPToolMethod.RunMsg;
                                    // return bState;
                                }
                                if (bState)
                                {
                                    m_listDistancePPReg.Add(new HObjectWithColor(m_SvDistancePointPToolMethod.CrossCenter, "yellow"));
                                    m_listDistancePPReg.Add(new HObjectWithColor(m_SvDistancePointPToolMethod.Line, "yellow"));
                                    m_listDistancePPReg.Add(new HObjectWithColor(m_SvDistancePointPToolMethod.Cross1, "yellow"));
                                    m_listDistancePPReg.Add(new HObjectWithColor(m_SvDistancePointPToolMethod.Cross2, "yellow"));
                                    m_listDistancePPReg.Add(new HObjectWithColor(m_SvDistancePointPToolMethod.CenterLine, "yellow"));
                                    m_ListReg.Add(new HObjectWithColor(m_SvDistancePointPToolMethod.Line, "yellow"));
                                    m_ListReg.Add(new HObjectWithColor(m_SvDistancePointPToolMethod.Cross1, "yellow"));
                                    m_ListReg.Add(new HObjectWithColor(m_SvDistancePointPToolMethod.Cross2, "yellow"));
                                    m_ListReg.Add(new HObjectWithColor(m_SvDistancePointPToolMethod.CenterLine, "yellow"));
                                    m_ListReg.Add(new HObjectWithColor(m_SvDistancePointPToolMethod.CrossCenter, "yellow"));
                                }

                                paraResult = new ParasResultAll();
                                paraResult.ToolName = m_ModualPara.ListToolParaAdd[iNow].ToolName + iNow;
                                paraResult.ParaDistancePointPoints.Distance = m_SvDistancePointPToolMethod.Distance;
                                paraResult.ParaDistancePointPoints.Row = m_SvDistancePointPToolMethod.Row;
                                paraResult.ParaDistancePointPoints.Col = m_SvDistancePointPToolMethod.Col;
                                m_ListParaResultAll.Add(paraResult);
                                #endregion
                            }
                            break;

                        case "点线距离工具":
                            if (m_bDistancePL)
                            {
                                m_SvDistancePointLineToolMethod.InputImage = m_hoInputImage;
                                m_SvDistancePointLineToolMethod.ListParaResultAll = m_ListParaResultAll;
                                m_SvDistancePointLineToolMethod.SvDistancePointLineToolParas = m_ModualPara.ListToolParaAdd[iNow].DistancePointLineToolParas;
                                bState = m_SvDistancePointLineToolMethod.Run();
                                if (!bState)
                                {
                                    m_listDistancePLReg = new List<HObjectWithColor>();
                                    m_listAngleLXReg = new List<HObjectWithColor>();
                                    m_ListReg = new List<HObjectWithColor>();
                                    m_listBlobReg = new List<HObjectWithColor>();
                                    m_listCircleReg = new List<HObjectWithColor>();
                                    m_listLineReg = new List<HObjectWithColor>();
                                    m_listPatMaxReg = new List<HObjectWithColor>();
                                    HOperatorSet.CountSeconds(out hvTime2);
                                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                    m_strRunMsg = "点线距离工具" + iNow + m_SvDistancePointLineToolMethod.RunMsg;
                                    // return bState;
                                }
                                if (bState)
                                {
                                    m_listDistancePLReg.Add(new HObjectWithColor(m_SvDistancePointLineToolMethod.Line, "slate blue"));
                                    m_listDistancePLReg.Add(new HObjectWithColor(m_SvDistancePointLineToolMethod.Cross1, "slate blue"));
                                    m_listDistancePLReg.Add(new HObjectWithColor(m_SvDistancePointLineToolMethod.Cross2, "slate blue"));
                                    m_listDistancePLReg.Add(new HObjectWithColor(m_SvDistancePointLineToolMethod.Cross, "slate blue"));
                                    m_listDistancePLReg.Add(new HObjectWithColor(m_SvDistancePointLineToolMethod.ProjCross, "slate blue"));
                                    m_ListReg.Add(new HObjectWithColor(m_SvDistancePointLineToolMethod.Line, "slate blue"));
                                    m_ListReg.Add(new HObjectWithColor(m_SvDistancePointLineToolMethod.Cross1, "slate blue"));
                                    m_ListReg.Add(new HObjectWithColor(m_SvDistancePointLineToolMethod.Cross2, "slate blue"));
                                    m_ListReg.Add(new HObjectWithColor(m_SvDistancePointLineToolMethod.Cross, "slate blue"));
                                    m_ListReg.Add(new HObjectWithColor(m_SvDistancePointLineToolMethod.ProjCross, "slate blue"));
                                }

                                paraResult = new ParasResultAll();
                                paraResult.ToolName = m_ModualPara.ListToolParaAdd[iNow].ToolName + iNow;
                                paraResult.ParaDistancePointLines.Row = m_SvDistancePointLineToolMethod.Row;
                                paraResult.ParaDistancePointLines.Col = m_SvDistancePointLineToolMethod.Col;
                                paraResult.ParaDistancePointLines.Distance = m_SvDistancePointLineToolMethod.Distance;
                                m_ListParaResultAll.Add(paraResult);
                            }
                            break;
                        case "公式示教工具":
                            if (m_bVisualCorrect)
                            {
                                m_SvVisualCorrectionToolMethod.InputImage = m_hoInputImage;
                                m_SvVisualCorrectionToolMethod.ListParaResultAll = m_ListParaResultAll;
                                m_SvVisualCorrectionToolMethod.ParasSvVisualCorrectionToolPara = m_ModualPara.ListToolParaAdd[iNow].SvVisualCorrectionToolParas;
                                bState = m_SvVisualCorrectionToolMethod.Run();
                                if (!bState)
                                {
                                    m_listDistancePLReg = new List<HObjectWithColor>();
                                    m_listAngleLXReg = new List<HObjectWithColor>();
                                    m_ListReg = new List<HObjectWithColor>();
                                    m_listBlobReg = new List<HObjectWithColor>();
                                    m_listCircleReg = new List<HObjectWithColor>();
                                    m_listLineReg = new List<HObjectWithColor>();
                                    m_listPatMaxReg = new List<HObjectWithColor>();
                                    HOperatorSet.CountSeconds(out hvTime2);
                                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                    m_strRunMsg = "公式示教工具" + iNow + m_SvVisualCorrectionToolMethod.RunMsg;
                                    // return bState;
                                }
                                paraResult = new ParasResultAll();
                                paraResult.ToolName = m_ModualPara.ListToolParaAdd[iNow].ToolName + iNow;
                                paraResult.ParasVisualCorrection.X1 = m_SvVisualCorrectionToolMethod.X1;
                                paraResult.ParasVisualCorrection.Y1 = m_SvVisualCorrectionToolMethod.Y1;
                                paraResult.ParasVisualCorrection.X2 = m_SvVisualCorrectionToolMethod.X2;
                                paraResult.ParasVisualCorrection.Y2 = m_SvVisualCorrectionToolMethod.Y2;
                                m_ListParaResultAll.Add(paraResult);
                            }
                            break;
                        case "结果判断工具":
                            if (m_bJudgeResult)
                            {
                                m_SvJudgeResultParaToolMethod.InputImage = m_hoInputImage;
                                m_SvJudgeResultParaToolMethod.ListParaResultAll = m_ListParaResultAll;
                                m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool = m_ModualPara.ListToolParaAdd[iNow].SvJudgeResultParaToolParas;
                                bState = m_SvJudgeResultParaToolMethod.Run();
                                if (!bState)
                                {
                                    m_listDistancePLReg = new List<HObjectWithColor>();
                                    m_listAngleLXReg = new List<HObjectWithColor>();
                                    m_ListReg = new List<HObjectWithColor>();
                                    m_listBlobReg = new List<HObjectWithColor>();
                                    m_listCircleReg = new List<HObjectWithColor>();
                                    m_listLineReg = new List<HObjectWithColor>();
                                    m_listPatMaxReg = new List<HObjectWithColor>();
                                    HOperatorSet.CountSeconds(out hvTime2);
                                    m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                                    m_strRunMsg = "结果判断工具" + iNow + m_SvJudgeResultParaToolMethod.RunMsg;
                                    // return bState;
                                }

                                int isendState = 2;


                                // 判断选择发送的检测状态，有一个为false,整个结果为false
                                for (int i = 0; i < m_SvJudgeResultParaToolMethod.ListSendDataMsgState.Count; i++)
                                {
                                    bState = m_SvJudgeResultParaToolMethod.ListSendDataMsgState[i];
                                    if (m_SvJudgeResultParaToolMethod.ListSendDataMsgState[i])
                                    {
                                        if (m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.OKForwardState)
                                        {
                                            isendState = m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.OK;
                                        }
                                    }
                                    else
                                    {
                                        if (m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.NGForwardState)
                                        {
                                            isendState = m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.NG;
                                        }
                                        break;
                                    }
                                }

                                if (m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.HaveOrNotForwardState)
                                {
                                    for (int j = 0; j < m_SvJudgeResultParaToolMethod.ListHaveOrNotState.Count; j++)
                                    {
                                        bool haveOrNotState = true;
                                        bState = m_SvJudgeResultParaToolMethod.ListHaveOrNotState[j];
                                        haveOrNotState = haveOrNotState && m_SvJudgeResultParaToolMethod.ListHaveOrNotState[j];
                                        if (!haveOrNotState)
                                        {
                                            isendState = m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.HaveOrNotForward;
                                            break;
                                        }
                                    }
                                }

                                if (m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.OKForwardState || m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.NGForwardState)
                                {
                                    m_hvSendTuple[0] = isendState;
                                    int jTuple = 0;
                                    for (int i = 0; i < m_SvJudgeResultParaToolMethod.ListSendDataMsg.Count; i++)
                                    {
                                        jTuple = i + 1;
                                        m_hvSendTuple[jTuple] = m_SvJudgeResultParaToolMethod.ListSendDataMsg[i];
                                    }
                                }
                                else
                                {
                                    int jTuple = 0;
                                    for (int i = 0; i < m_SvJudgeResultParaToolMethod.ListSendDataMsg.Count; i++)
                                    {
                                        jTuple = i;
                                        m_hvSendTuple[jTuple] = m_SvJudgeResultParaToolMethod.ListSendDataMsg[i];
                                    }
                                }

                                int isendStateBack = 2;

                                // 判断选择发送的检测状态，有一个为false,整个结果为false
                                for (int i = 0; i < m_SvJudgeResultParaToolMethod.ListSendDataMsgState.Count; i++)
                                {
                                    if (m_SvJudgeResultParaToolMethod.ListSendDataMsgState[i])
                                    {
                                        if (m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.OKBackState)
                                        {
                                            isendStateBack = m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.OKBack;
                                        }
                                    }
                                    else
                                    {
                                        if (m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.NGBackState)
                                        {
                                            isendStateBack = m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.NGBack;
                                        }
                                        break;
                                    }
                                }

                                if (m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.HaveOrNotBackState)
                                {
                                    bool haveOrNotState = true;
                                    for (int j = 0; j < m_SvJudgeResultParaToolMethod.ListHaveOrNotState.Count; j++)
                                    {
                                        bState = m_SvJudgeResultParaToolMethod.ListHaveOrNotState[j];
                                        haveOrNotState = haveOrNotState && m_SvJudgeResultParaToolMethod.ListHaveOrNotState[j];
                                        if(!haveOrNotState) 
                                        {
                                            isendStateBack = m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.HaveOrNotBack;
                                            break;
                                        }
                                    }
                                }

                                if (m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.OKBackState || m_SvJudgeResultParaToolMethod.ParasSvJudgeResultParaTool.NGBackState)
                                {
                                    int jTuple = 0;
                                    jTuple = m_hvSendTuple.Length;
                                    m_hvSendTuple[jTuple] = isendStateBack;
                                }

                                HTuple hvsend = null;
                                int ilength = m_hvSendTuple.TupleLength();
                                HOperatorSet.TupleSelectRange(m_hvSendTuple, 0, ilength - 1, out hvsend);
                                m_strSendMdg = hvsend.ToString();
                            }
                            break;
                    }
                }
            }
            else
            {
                bState = false;
                m_strRunMsg = "请输入图像！";
            }
            HOperatorSet.CountSeconds(out hvTime2);
            m_dRunTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
            return bState;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Save(object obj, string filename)
        {
            FileStream fs = null;
            try
            {
                string strarry = filename.Substring(0, filename.LastIndexOf("\\"));
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                if (null != m_PatMaxMethod.ModelID)
                {
                    HOperatorSet.WriteShapeModel(m_PatMaxMethod.ModelID, strarry + "\\SingleCameraModualModelID.shm");
                }
                if (null != m_PatMaxMethod.CreateContourModel)
                {
                    HOperatorSet.WriteObject(m_PatMaxMethod.CreateContourModel, strarry + "\\SingleModualCreateContour.hobj");
                }

                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return true;
        }
    }
}
