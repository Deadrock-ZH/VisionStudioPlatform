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
using ParaResultAll;

namespace SvsFindLineTool
{
    /// <summary>
    /// 主要内容：本类是找线的方法类
    /// 时    间：2019/9/6
    /// 作    者：王雅鹏
    /// </summary>
    [Serializable]
    public class FindLineMethod : ToolInterface
    {
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

        private FindLinePara m_para = new FindLinePara();

        /// <summary>
        /// 参数类
        /// </summary>
        public FindLinePara Para
        {
            get
            {
                return m_para;
            }
            set
            {
                m_para = value;
            }
        }

        /// <summary>
        /// 模块Form名
        /// </summary>
        public string ModualFormName
        {
            get;
            set;
        }

        private HObject m_hoImage = null;

        /// <summary>
        /// 输入图像
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

        private List<double> m_ListRows = new List<double>();

        /// <summary>
        /// 卡尺卡到的点的行坐标集合
        /// </summary>
        public List<double> ListRows
        {
            get
            {
                return this.m_ListRows;
            }
            set
            {
                this.m_ListRows = value;
            }
        }

        private List<double> m_ListCols;

        /// <summary>
        /// 卡尺卡到的点的列坐标集合
        /// </summary>
        public List<double> ListCols
        {
            get
            {
                return this.m_ListCols;
            }
            set
            {
                this.m_ListCols = value;
            }
        }

        private List<double> m_ListApptims;

        /// <summary>
        /// 卡尺卡到的点的幅度集合
        /// </summary>
        public List<double> ListApptims
        {
            get
            {
                return this.m_ListApptims;
            }
            set
            {
                this.m_ListApptims = value;
            }
        }

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
                string strarry = filename.Substring(0, filename.IndexOf("\\"));
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write,
                                    FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
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
                string strarry = filename.Substring(0, filename.IndexOf("\\"));
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                return Para;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        private HObject m_hoLine = null;

        /// <summary>
        /// 拟合的直线
        /// </summary>
        public HObject HoLine
        {
            get
            {
                return this.m_hoLine;
            }
            set
            {
                this.m_hoLine = value;
            }
        }

        private double m_dRowStart = -1;

        /// <summary>
        /// 拟合直线的起始行坐标
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

        private double m_dRowMiddle = -1;

        /// <summary>
        /// 拟合直线的中心行坐标
        /// </summary>
        public double RowMiddle
        {
            get
            {
                return this.m_dRowMiddle;
            }
            set
            {
                this.m_dRowMiddle = value;
            }
        }

        private double m_dRowEnd = -1;

        /// <summary>
        /// 拟合直线的终止行坐标
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

        private double m_dColStart = -1;

        /// <summary>
        /// 拟合直线的起始列坐标
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

        private double m_dColMiddle = -1;

        /// <summary>
        /// 拟合直线的中心列坐标
        /// </summary>
        public double ColMiddle
        {
            get
            {
                return this.m_dColMiddle;
            }
            set
            {
                this.m_dColMiddle = value;
            }
        }

        private double m_dColEnd = -1;

        /// <summary>
        /// 拟合直线的终止列坐标
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

        private HTuple m_hvAffineTrans = null;

        /// <summary>
        /// 仿射矩阵
        /// </summary>
        public HTuple AffineTans
        {
            get
            {
                return this.m_hvAffineTrans;
            }
            set
            {
                this.m_hvAffineTrans = value;
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

        private string m_strMsg = "";

        /// <summary>
        /// 消息运行
        /// </summary>
        public string RunMsg
        {
            get
            {
                return this.m_strMsg;
            }
            set
            {
                m_strMsg = value;
            }
        }

        private List<HObjectWithColor> m_listReg = new List<HObjectWithColor>();

        /// <summary>
        /// 存储指定颜色Hobject的List
        /// </summary>
        public List<HObjectWithColor> ListReg
        {
            get
            {
                return m_listReg;
            }
            set
            {
                m_listReg = value;
            }
        }

        /// <summary>
        /// 运行 
        /// </summary>
        public bool Run()
        {
            m_listReg = new List<HObjectWithColor>();
            HObject hoRegion = null;
            m_dTime = 0;
            HTuple hvTime1 = null, hvTime2 = null;
            HOperatorSet.CountSeconds(out hvTime1);
            m_ListRows = new List<double>();
            m_ListCols = new List<double>();
            m_strMsg = string.Empty;
            HTuple hvRowsCalliper = null;
            HTuple hvColumnsCalliper = null;
            HTuple hvRow1 = null, hvCol1 = null, hvRow2 = null, hvCol2 = null;
            m_dRowMiddle = -100;
            m_dColMiddle = -100;
            m_dRowStart = -100;
            m_dColStart = -100;
            m_dRowEnd = -100;
            m_dColEnd = -100;
            m_hoLine = null;
            int iIndex = 0;
           // m_hvAffineTrans = null;
            if (m_para.Row1SelectIndexName == "null")
            {
                m_hvAffineTrans = null;
            }
            else
            {
                foreach (ParasResultAll item in m_ListParaResultAll)
                {
                    if (m_para.Row1SelectIndexName.Contains("匹配工具" + iIndex))
                    {
                        if ((item.ToolName + "item.PatMaxResultPara.Affinehom_").Contains(m_para.Row1SelectIndexName))
                        {
                            m_hvAffineTrans = null;
                            m_hvAffineTrans = item.PatMaxResultPara.Affinehom;
                            break;
                        }
                    }
                    iIndex++;
                }
            }
            HOperatorSet.GenEmptyObj(out m_hoLine);
            try
            {
                HTuple RowBegin = null, ColumnBegin = null; 
                HTuple RowEnd = null, ColumnEnd = null;
                if (null != m_hvAffineTrans && m_hvAffineTrans.Length > 0)
                {
                    HOperatorSet.AffineTransPoint2d(m_hvAffineTrans,
                                 Para.LineCalliperParas.RowBegin,
                                 Para.LineCalliperParas.ColumnBegin,
                                 out RowBegin, out ColumnBegin);
                    HOperatorSet.AffineTransPoint2d(m_hvAffineTrans,
                                 Para.LineCalliperParas.RowEnd,
                                 Para.LineCalliperParas.ColumnEnd,
                                 out RowEnd, out ColumnEnd);
                }
                else
                {
                    RowBegin = Para.LineCalliperParas.RowBegin;
                    ColumnBegin = Para.LineCalliperParas.ColumnBegin;
                    RowEnd = Para.LineCalliperParas.RowEnd;
                    ColumnEnd = Para.LineCalliperParas.ColumnEnd;
                }
                rake(m_hoImage, out hoRegion,
                        Para.LineCalliperParas.CalliperCount,
                        Para.LineCalliperParas.RecHeight,
                        Para.LineCalliperParas.RecWidth,
                        Para.LineCalliperParas.
                        Sigma, Para.LineCalliperParas.GrayContrast,
                        Para.LineCalliperParas.Direction,
                        Para.LineCalliperParas.SelectType,
                        RowBegin, ColumnBegin, RowEnd, ColumnEnd,
                        out hvRowsCalliper, out hvColumnsCalliper);
                if (hvColumnsCalliper.Length < 1)
                {
                    m_strMsg = "卡尺卡点失败,请检查参数设置是否合适！";
                    m_dRowMiddle = -100;
                    m_dColMiddle = -100;
                    m_dRowStart = -100;
                    m_dColStart = -100;
                    m_dRowEnd = -100;
                    m_dColEnd = -100;
                    m_listReg = new List<HObjectWithColor>();
                    HOperatorSet.CountSeconds(out hvTime2);
                    m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                    return false;
                }

            }
            catch (HalconException ex)
            {
                m_strMsg = "卡尺卡点失败！" + ex.ToString();
                m_dRowMiddle = -100;
                m_dColMiddle = -100;
                m_dRowStart = -100;
                m_dColStart = -100;
                m_dRowEnd = -100;
                m_dColEnd = -100;
                HOperatorSet.CountSeconds(out hvTime2);
                m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                m_listReg = new List<HObjectWithColor>();
                return false;
            }
            try
            {
                if (null != hvColumnsCalliper && hvColumnsCalliper.Length > 0)
                {
                    pts_to_best_line(out m_hoLine, hvRowsCalliper, hvColumnsCalliper, 2,
                                     out hvRow1, out hvCol1, out hvRow2, out hvCol2);
                    m_listReg.Add(new HObjectWithColor(m_hoLine, "red"));
                    if (hvCol2.Length < 1)
                    {
                        m_dRowMiddle = -100;
                        m_dColMiddle = -100;
                        m_dRowStart = -100;
                        m_dColStart = -100;
                        m_dRowEnd = -100;
                        m_dColEnd = -100;
                        m_strMsg = "直线拟合失败,请检查参数设置是否合适！";
                        HOperatorSet.CountSeconds(out hvTime2);
                        m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                        m_listReg = new List<HObjectWithColor>();
                        return false;
                    }
                    m_dRowStart = hvRow1[0].D;
                    m_dColStart = hvCol1[0].D;
                    m_dRowEnd = hvRow2[0].D;
                    m_dColEnd = hvCol2[0].D;
                    m_dRowMiddle = (m_dRowStart + m_dRowEnd) / 2;
                    m_dColMiddle = (m_dColStart + m_dColEnd) / 2;
                }
                else
                {
                    m_dRowMiddle = -100;
                    m_dColMiddle = -100;
                    m_dRowStart = -100;
                    m_dColStart = -100;
                    m_dRowEnd = -100;
                    m_dColEnd = -100;
                    m_strMsg = "卡尺卡点小于1！！";
                    HOperatorSet.CountSeconds(out hvTime2);
                    m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                    m_listReg = new List<HObjectWithColor>();
                    return false;
                }
            }
            catch (HalconException ex)
            {
                m_strMsg = "直线拟合失败！！" + ex.ToString();
                m_dRowMiddle = -100;
                m_dColMiddle = -100;
                m_dRowStart = -100;
                m_dColStart = -100;
                m_dRowEnd = -100;
                m_dColEnd = -100;
                HOperatorSet.CountSeconds(out hvTime2);
                m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                m_listReg = new List<HObjectWithColor>();
                return false;
            }
            HOperatorSet.CountSeconds(out hvTime2);
            m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
            return true;
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <returns></returns>
        public bool LastRun()
        {
            m_listReg = new List<HObjectWithColor>();
            HObject hoRegion = null;
            m_dTime = 0;
            HTuple hvTime1 = null, hvTime2 = null;
            HOperatorSet.CountSeconds(out hvTime1);
            m_ListRows = new List<double>();
            m_ListCols = new List<double>();
            m_strMsg = "直线拟合成功！";
            HTuple hvRowsCalliper = null;
            HTuple hvColumnsCalliper = null;
            HTuple hvRow1 = null, hvCol1 = null, hvRow2 = null, hvCol2 = null;
            m_dRowStart = -1;
            m_dColStart = -1;
            m_dRowEnd = -1;
            m_dColEnd = -1;
            m_hoLine = null;
            HOperatorSet.GenEmptyObj(out m_hoLine);
            try
            {
                HTuple RowBegin = null, ColumnBegin = null;
                HTuple RowEnd = null, ColumnEnd = null;
                if (null != m_hvAffineTrans && m_hvAffineTrans.Length > 0)
                {
                    HOperatorSet.AffineTransPoint2d(m_hvAffineTrans,
                                 Para.LineCalliperParas.RowBegin,
                                 Para.LineCalliperParas.ColumnBegin,
                                 out RowBegin, out ColumnBegin);
                    HOperatorSet.AffineTransPoint2d(m_hvAffineTrans,
                                 Para.LineCalliperParas.RowEnd,
                                 Para.LineCalliperParas.ColumnEnd,
                                 out RowEnd, out ColumnEnd);
                }
                else
                {
                    RowBegin = Para.LineCalliperParas.RowBegin;
                    ColumnBegin = Para.LineCalliperParas.ColumnBegin;
                    RowEnd = Para.LineCalliperParas.RowEnd;
                    ColumnEnd = Para.LineCalliperParas.ColumnEnd;
                }
                rake(m_hoImage, out hoRegion,
                        Para.LineCalliperParas.CalliperCount,
                        Para.LineCalliperParas.RecHeight,
                        Para.LineCalliperParas.RecWidth,
                        Para.LineCalliperParas.
                        Sigma, Para.LineCalliperParas.GrayContrast,
                        Para.LineCalliperParas.Direction,
                        Para.LineCalliperParas.SelectType,
                        RowBegin, ColumnBegin, RowEnd, ColumnEnd,
                        out hvRowsCalliper, out hvColumnsCalliper);
                if (hvColumnsCalliper.Length < 1)
                {
                    m_strMsg = "卡尺卡点失败,请检查参数设置是否合适！";
                    m_listReg = new List<HObjectWithColor>();
                    HOperatorSet.CountSeconds(out hvTime2);
                    m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                    return false;
                }

            }
            catch (HalconException ex)
            {
                m_strMsg = "卡尺卡点失败！" + ex.ToString();
                HOperatorSet.CountSeconds(out hvTime2);
                m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                m_listReg = new List<HObjectWithColor>();
                return false;
            }
            try
            {
                if (null != hvColumnsCalliper && hvColumnsCalliper.Length > 0)
                {
                    pts_to_best_line(out m_hoLine, hvRowsCalliper, hvColumnsCalliper, 2,
                                     out hvRow1, out hvCol1, out hvRow2, out hvCol2);
                    m_listReg.Add(new HObjectWithColor(m_hoLine, "red"));
                    if (hvCol2.Length < 1)
                    {
                        m_dRowStart = -1;
                        m_dColStart = -1;
                        m_dRowEnd = -1;
                        m_dColEnd = -1;
                        m_strMsg = "直线拟合失败,请检查参数设置是否合适！";
                        HOperatorSet.CountSeconds(out hvTime2);
                        m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                        m_listReg = new List<HObjectWithColor>();
                        return false;
                    }
                    m_dRowStart = hvRow1[0].D;
                    m_dColStart = hvCol1[0].D;
                    m_dRowEnd = hvRow2[0].D;
                    m_dColEnd = hvCol2[0].D;
                }
                else
                {
                    m_strMsg = "卡尺卡点小于1！！";
                    HOperatorSet.CountSeconds(out hvTime2);
                    m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                    m_listReg = new List<HObjectWithColor>();
                    return false;
                }
            }
            catch (HalconException ex)
            {
                m_strMsg = "直线拟合失败！！" + ex.ToString();
                HOperatorSet.CountSeconds(out hvTime2);
                m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
                m_listReg = new List<HObjectWithColor>();
                return false;
            }
            HOperatorSet.CountSeconds(out hvTime2);
            m_dTime = (hvTime2[0].D - hvTime1[0].D) * 1000;
            return true;
        }

        /// <summary>
        /// 拟合直线
        /// </summary>
        /// <param name="ho_Line">输出直线轮廓</param>
        /// <param name="hv_Rows">行坐标</param>
        /// <param name="hv_Cols">列坐标</param>
        /// <param name="hv_ActiveNum">最小有效点</param>
        /// <param name="hv_Row1">输出直线的起点行坐标</param>
        /// <param name="hv_Column1">直线的起点列坐标</param>
        /// <param name="hv_Row2">直线的终点行坐标</param>
        /// <param name="hv_Column2">直线的终点列坐标</param>
        private void pts_to_best_line(out HObject ho_Line,
                                     HTuple hv_Rows, HTuple hv_Cols,
                                     HTuple hv_ActiveNum, out HTuple hv_Row1,
                                     out HTuple hv_Column1, out HTuple hv_Row2,
                                     out HTuple hv_Column2)
        {



            // Local iconic variables 

            HObject ho_Contour = null;

            // Local control variables 

            HTuple hv_Length = null, hv_Nr = new HTuple();
            HTuple hv_Nc = new HTuple(), hv_Dist = new HTuple(),
                   hv_Length1 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Line);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            try
            {
                //初始化
                hv_Row1 = 0;
                hv_Column1 = 0;
                hv_Row2 = 0;
                hv_Column2 = 0;
                //产生一个空的直线对象，用于保存拟合后的直线
                ho_Line.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Line);
                //计算边缘数量
                HOperatorSet.TupleLength(hv_Cols, out hv_Length);
                //当边缘数量不小于有效点数时进行拟合
                if ((int)((new HTuple(hv_Length.TupleGreaterEqual(hv_ActiveNum))).TupleAnd(
                    new HTuple(hv_ActiveNum.TupleGreater(1)))) != 0)
                {
                    //halcon的拟合是基于xld的，需要把边缘连接成xld
                    ho_Contour.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows, hv_Cols);
                    //拟合直线。使用的算法是'tukey'，其他算法请参考fit_line_contour_xld的描述部分。
                    HOperatorSet.FitLineContourXld(ho_Contour, "tukey", -1, 0, 5, 2, out hv_Row1,
                        out hv_Column1, out hv_Row2, out hv_Column2, out hv_Nr, out hv_Nc, out hv_Dist);
                    //判断拟合结果是否有效：如果拟合成功，数组中元素的数量大于0
                    HOperatorSet.TupleLength(hv_Dist, out hv_Length1);
                    if ((int)(new HTuple(hv_Length1.TupleLess(1))) != 0)
                    {
                        ho_Contour.Dispose();
                        m_strMsg = "直线拟合失败！";
                        return;
                    }
                    //根据拟合结果，产生直线xld
                    ho_Line.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_Line, hv_Row1.TupleConcat(hv_Row2),
                        hv_Column1.TupleConcat(hv_Column2));
                }
                ho_Contour.Dispose();
                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Contour.Dispose();

                throw HDevExpDefaultException;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ho_Image">输入图像</param>
        /// <param name="ho_Regions">输出区域</param>
        /// <param name="hv_Elements">卡尺个数</param>
        /// <param name="hv_DetectHeight">卡尺的高度</param>
        /// <param name="hv_DetectWidth">卡尺的宽度</param>
        /// <param name="hv_Sigma">滤波参数</param>
        /// <param name="hv_Threshold">对比度</param>
        /// <param name="hv_Transition">极性</param>
        /// <param name="hv_Select">选择类型</param>
        /// <param name="hv_Row1">直线的起点行坐标</param>
        /// <param name="hv_Column1">直线的起点列坐标</param>
        /// <param name="hv_Row2">直线的终点行坐标</param>
        /// <param name="hv_Column2">直线的终点列坐标</param>
        /// <param name="hv_ResultRow">输出的卡尺卡到的行坐标集合</param>
        /// <param name="hv_ResultColumn">输出的卡尺卡到的列坐标集合</param>
        private void rake(HObject ho_Image, out HObject ho_Regions, HTuple hv_Elements,
            HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Sigma, HTuple hv_Threshold,
            HTuple hv_Transition, HTuple hv_Select, HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2,
            HTuple hv_Column2, out HTuple hv_ResultRow, out HTuple hv_ResultColumn)
        {


            //初始化边缘坐标数组
            hv_ResultRow = new HTuple();
            hv_ResultColumn = new HTuple();
            m_ListRows = new List<double>();
            m_ListCols = new List<double>();
            m_ListApptims = new List<double>();
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_RegionLines, ho_Rectangle = null;
            HObject ho_Arrow1 = null;

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null, hv_ATan = null;
            HTuple hv_i = null, hv_RowC = new HTuple(), hv_ColC = new HTuple();
            HTuple hv_Distance = new HTuple(), hv_RowL2 = new HTuple();
            HTuple hv_RowL1 = new HTuple(), hv_ColL2 = new HTuple();
            HTuple hv_ColL1 = new HTuple(), hv_MsrHandle_Measure = new HTuple();
            HTuple hv_RowEdge = new HTuple(), hv_ColEdge = new HTuple();
            HTuple hv_Amplitude = new HTuple(), hv_tRow = new HTuple();
            HTuple hv_tCol = new HTuple(), hv_t = new HTuple(), hv_Number = new HTuple();
            HTuple hv_j = new HTuple();
            HTuple hv_DetectWidth_COPY_INP_TMP = hv_DetectWidth.Clone();
            HTuple hv_Select_COPY_INP_TMP = hv_Select.Clone();
            HTuple hv_Transition_COPY_INP_TMP = hv_Transition.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Arrow1);
            try
            {
                try
                {
                    // 获取图像尺寸
                    if (null != ho_Image)
                    {
                        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    }
                    //产生一个空显示对象，用于显示
                    ho_Regions.Dispose();
                    HOperatorSet.GenEmptyObj(out ho_Regions);
                    //产生直线xld
                    ho_RegionLines.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_RegionLines, hv_Row1.TupleConcat(hv_Row2),
                        hv_Column1.TupleConcat(hv_Column2));
                    //存储到显示对象
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConcatObj(ho_Regions, ho_RegionLines, out ExpTmpOutVar_0);
                        ho_Regions.Dispose();
                        ho_Regions = ExpTmpOutVar_0;
                    }
                    //计算直线与x轴的夹角，逆时针方向为正向。
                    HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_ATan);

                    //边缘检测方向垂直于检测直线：直线方向正向旋转90°为边缘检测方向
                    hv_ATan = hv_ATan + ((new HTuple(Para.LineCalliperParas.Phi)).TupleRad());

                    //根据检测直线按顺序产生测量区域矩形，并存储到显示对象
                    HTuple end_val18 = hv_Elements;
                    HTuple step_val18 = 1;
                    for (hv_i = 1; hv_i.Continue(end_val18, step_val18); hv_i = hv_i.TupleAdd(step_val18))
                    {
                        //如果只有一个测量矩形，作为卡尺工具，宽度为检测直线的长度
                        if ((int)(new HTuple(hv_Elements.TupleEqual(1))) != 0)
                        {
                            hv_RowC = (hv_Row1 + hv_Row2) * 0.5;
                            hv_ColC = (hv_Column1 + hv_Column2) * 0.5;
                            //判断是否超出图像,超出不检测边缘
                            if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(
                                new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                                hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                            {
                                continue;
                            }
                            HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Distance);
                            hv_DetectWidth_COPY_INP_TMP = hv_Distance.Clone();
                            ho_Rectangle.Dispose();
                            HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC,
                                hv_ATan, hv_DetectHeight / 2, hv_Distance / 2);
                        }
                        else
                        {
                            //如果有多个测量矩形，产生该测量矩形xld
                            hv_RowC = hv_Row1 + (((hv_Row2 - hv_Row1) * (hv_i - 1)) / (hv_Elements - 1));
                            hv_ColC = hv_Column1 + (((hv_Column2 - hv_Column1) * (hv_i - 1)) / (hv_Elements - 1));
                            //判断是否超出图像,超出不检测边缘
                            if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(
                                new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                                hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                            {
                                continue;
                            }
                            ho_Rectangle.Dispose();
                            HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC,
                                hv_ATan, hv_DetectHeight / 2, hv_DetectWidth_COPY_INP_TMP / 2);
                        }
                        if ((int)(new HTuple(hv_i.TupleEqual(1))) != 0)
                        {
                            //在第一个测量矩形绘制一个箭头xld，用于只是边缘检测方向
                            hv_RowL2 = hv_RowC + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                            hv_RowL1 = hv_RowC - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                            hv_ColL2 = hv_ColC + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                            hv_ColL1 = hv_ColC - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                            ho_Arrow1.Dispose();
                        }
                        //产生测量对象句柄
                        HOperatorSet.GenMeasureRectangle2(hv_RowC, hv_ColC, hv_ATan, hv_DetectHeight / 2,
                            hv_DetectWidth_COPY_INP_TMP / 2, hv_Width, hv_Height, "nearest_neighbor",
                            out hv_MsrHandle_Measure);

                        //设置极性
                        if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("negative"))) != 0)
                        {
                            hv_Transition_COPY_INP_TMP = "negative";
                        }
                        else
                        {
                            if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("positive"))) != 0)
                            {

                                hv_Transition_COPY_INP_TMP = "positive";
                            }
                            else
                            {
                                hv_Transition_COPY_INP_TMP = "all";
                            }
                        }
                        //设置边缘位置。最强点是从所有边缘中选择幅度绝对值最大点，需要设置为'all'
                        if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("first"))) != 0)
                        {
                            hv_Select_COPY_INP_TMP = "first";
                        }
                        else
                        {
                            if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("last"))) != 0)
                            {

                                hv_Select_COPY_INP_TMP = "last";
                            }
                            else
                            {
                                hv_Select_COPY_INP_TMP = "all";
                            }
                        }
                        //检测边缘
                        HOperatorSet.MeasurePos(ho_Image, hv_MsrHandle_Measure, hv_Sigma, hv_Threshold,
                                                hv_Transition_COPY_INP_TMP, hv_Select_COPY_INP_TMP,
                                                out hv_RowEdge, out hv_ColEdge,
                            out hv_Amplitude, out hv_Distance);
                        //清除测量对象句柄
                        HOperatorSet.CloseMeasure(hv_MsrHandle_Measure);

                        //临时变量初始化
                        //tRow，tCol保存找到指定边缘的坐标
                        hv_tRow = 0;
                        hv_tCol = 0;
                        //t保存边缘的幅度绝对值
                        hv_t = 0;
                        //找到的边缘必须至少为1个
                        HOperatorSet.TupleLength(hv_RowEdge, out hv_Number);
                        if ((int)(new HTuple(hv_Number.TupleLess(1))) != 0)
                        {
                            continue;
                        }

                        //有多个边缘时，选择幅度绝对值最大的边缘
                        HTuple end_val100 = hv_Number - 1;
                        HTuple step_val100 = 1;
                        for (hv_j = 0; hv_j.Continue(end_val100, step_val100); hv_j = hv_j.TupleAdd(step_val100))
                        {
                            if ((int)(new HTuple(((((hv_Amplitude.TupleSelect(hv_j))).TupleAbs())).TupleGreater(
                                hv_t))) != 0)
                            {

                                hv_tRow = hv_RowEdge.TupleSelect(hv_j);
                                hv_tCol = hv_ColEdge.TupleSelect(hv_j);
                                hv_t = ((hv_Amplitude.TupleSelect(hv_j))).TupleAbs();
                            }
                        }
                        //把找到的边缘保存在输出数组
                        if ((int)(new HTuple(hv_t.TupleGreater(0))) != 0)
                        {
                            hv_ResultRow = hv_ResultRow.TupleConcat(hv_tRow);
                            hv_ResultColumn = hv_ResultColumn.TupleConcat(hv_tCol);
                            m_ListRows.Add(hv_tRow[0].D);
                            m_ListCols.Add(hv_tCol[0].D);
                            m_ListApptims.Add(hv_t[0].D);
                        }
                    }

                    ho_RegionLines.Dispose();
                    ho_Rectangle.Dispose();
                    return;
                }
                catch (HalconException HDevExpDefaultException)
                {
                    ho_RegionLines.Dispose();
                    ho_Rectangle.Dispose();
                    ho_Arrow1.Dispose();
                }
            }

            catch (Exception)
            {
                ho_RegionLines.Dispose();
                ho_Rectangle.Dispose();
                ho_Arrow1.Dispose();
                return;
            }
        }

    }
}
