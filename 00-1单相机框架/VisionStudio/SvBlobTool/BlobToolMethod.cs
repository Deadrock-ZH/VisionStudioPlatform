using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GVS.HalconDisp.ViewROI.Config;
using HalconDotNet;
using ParaResultAll;

namespace SvBlobTool
{
    public class ParaResult
    {
        /// <summary>
        /// 面积
        /// </summary>
        public double Area
        {
            get;
            set;
        }

        /// <summary>
        /// 中心行坐标
        /// </summary>
        public double Row
        {
            get;
            set;
        }

        /// <summary>
        /// 中心列坐标
        /// </summary>
        public double Col
        {
            get;
            set;
        }

        /// <summary>
        /// 选中区域轮廓
        /// </summary>
        public HObject HoPointReg
        {
            get;
            set;
        }

        /// <summary>
        /// 质心
        /// </summary>
        public HObject HoCross
        {
            get;
            set;
        }

        public ParaResult()
        {
            Area = 0;
            Row = 0;
            Col = 0;
            HoCross = null;
            HoPointReg = null;
        }
    }

    public class BlobToolMethod : ToolInterFace.ToolInterface
    {
        private List<ParaResult> m_ListParaResultAll = new List<ParaResult>();

        /// <summary>
        /// 所有连通域结果数据
        /// </summary>
        public List<ParaResult> ListParaResultAll
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

        /// <summary>
        /// 检测结果类
        /// </summary>
        private List<ParasResultAll> m_ListClassParaResultAll = new List<ParasResultAll>();

        /// <summary>
        /// 检测结果集合
        /// </summary>
        public List<ParasResultAll> ListClassParaResultAll
        {
            get
            {
                return m_ListClassParaResultAll;
            }
            set
            {
                m_ListClassParaResultAll = value;
            }
        }

        private List<ParaResult> m_ListParaResultSelect = new List<ParaResult>();

        /// <summary>
        /// 所有选择域结果数据
        /// </summary>
        public List<ParaResult> ListParaResultSelect
        {
            get
            {
                return m_ListParaResultSelect;
            }
            set
            {
                m_ListParaResultSelect = value;
            }
        }

        private BlobToolPara m_blobPara = new BlobToolPara();

        /// <summary>
        /// blob参数类
        /// </summary>
        public BlobToolPara ParaBlob
        {
            get
            {
                return m_blobPara;
            }
            set
            {
                m_blobPara = value;
            }
        }

        private HTuple m_hvAffineHom = null;

        /// <summary>
        /// 仿射矩阵
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

        private HObject m_hoImage = null;

        /// <summary>
        /// 输入图片
        /// </summary>
        public HObject InputImage
        {
            get
            {
                return m_hoImage;
            }
            set
            {
                m_hoImage = value;
            }
        }

        private HObject m_hoRegGroup = null;

        /// <summary>
        /// 组合区域输入区域
        /// </summary>
        public HObject RegBlobReg
        {
            get
            {
                return m_hoRegGroup;
            }
            set
            {
                m_hoRegGroup = value;
            }
        }


        private List<HObjectWithColor> m_ListReg = new List<HObjectWithColor>();

        /// <summary>
        /// 结果区域集合
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

        // private List<>

        /// <summary>
        /// 模块名称设置
        /// </summary>
        public string ModualFormName
        {
            get;
            set;
        }

        private string m_strMsg = string.Empty;

        /// <summary>
        /// 运行消息
        /// </summary>
        public string RunMsg
        {
            get
            {
                return m_strMsg;
            }
            set
            {
                m_strMsg = value;
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
                return m_dTime;
            }
            set
            {
                m_dTime = value;
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 运行方法
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            bool bstate = true;
            m_ListReg = new List<HObjectWithColor>();
            m_dTime = 0;
            m_strMsg = string.Empty;
            HTuple hvTimeStart = null, hvTimeEnd = null;
            HOperatorSet.CountSeconds(out hvTimeStart);
            HObject hoRegCheckResultAffine = null;
            HTuple hvArea = null, hvRow = null, hvColumn = null;

            // 掩膜区域
            HObject m_MaskReg = null;
            HObject hoRegDifference = null;
            HObject hoReduceImage = null;
            HObject hoConnection = null;
            HObject hoConnectionMode = null;
            HObject hoMorphology = null;

            ParaResult paraResult = new ParaResult();
            m_ListParaResultAll = new List<ParaResult>();
            m_ListParaResultSelect = new List<ParaResult>();
            try
            {
                string strNameRow1 = string.Empty;
                string strNameCol1 = string.Empty;
                string strNameRadius = string.Empty;
                if (ParaBlob.AffineHomSelectIndexName == "null")
                {
                    m_hvAffineHom = null;
                }
                else
                {
                    int iIndex1 = 0;
                    foreach (ParasResultAll item in m_ListClassParaResultAll)
                    {
                        if (ParaBlob.AffineHomSelectIndexName.Contains("匹配工具" + iIndex1))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Affinehom_").Contains(ParaBlob.AffineHomSelectIndexName))
                            {
                                m_hvAffineHom = null;
                                m_hvAffineHom = item.PatMaxResultPara.Affinehom;
                                break;
                            }
                        }
                        iIndex1++;
                    }
                }
                int iIndex = 0;
                foreach (ParasResultAll item in m_ListClassParaResultAll)
                {
                    #region data1
                    if (ParaBlob.RowSelectIndexName != "null")
                    {
                        if (ParaBlob.RowSelectIndexName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (ParaBlob.RowSelectIndexName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (ParaBlob.RowSelectIndexName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (ParaBlob.RowSelectIndexName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(ParaBlob.RowSelectIndexName))
                                {
                                    strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(ParaBlob.RowSelectIndexName))
                                {
                                    strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(ParaBlob.RowSelectIndexName))
                                {
                                    strNameRow1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (ParaBlob.RowSelectIndexName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (ParaBlob.RowSelectIndexName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                        }
                        if (ParaBlob.RowSelectIndexName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                        if (ParaBlob.RowSelectIndexName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(ParaBlob.RowSelectIndexName))
                            {
                                strNameRow1 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                    }

                    #endregion

                    #region data2
                    if (ParaBlob.ColSelectIndexName != null)
                    {
                        if (ParaBlob.ColSelectIndexName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (ParaBlob.ColSelectIndexName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (ParaBlob.ColSelectIndexName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (ParaBlob.ColSelectIndexName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(ParaBlob.ColSelectIndexName))
                                {
                                    strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(ParaBlob.ColSelectIndexName))
                                {
                                    strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(ParaBlob.ColSelectIndexName))
                                {
                                    strNameCol1 = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (ParaBlob.ColSelectIndexName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (ParaBlob.ColSelectIndexName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                        }
                        if (ParaBlob.ColSelectIndexName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                        if (ParaBlob.ColSelectIndexName.Contains("公式示教工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParasVisualCorrection.X1_" + item.ParasVisualCorrection.X1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParasVisualCorrection.Y1_" + item.ParasVisualCorrection.Y1;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParasVisualCorrection.X2_" + item.ParasVisualCorrection.X2;
                            }
                            if ((item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2).Contains(ParaBlob.ColSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParasVisualCorrection.Y2_" + item.ParasVisualCorrection.Y2;
                            }
                        }
                    }

                    #endregion

                    #region data3
                    if (ParaBlob.RadiusSelectIndexName != null)
                    {
                        if (ParaBlob.RadiusSelectIndexName.Contains("匹配工具" + iIndex))
                        {
                            if ((item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.PatMaxResultPara.Row_" + item.PatMaxResultPara.Row;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.PatMaxResultPara.Col_" + item.PatMaxResultPara.Col;
                            }
                            if ((item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.PatMaxResultPara.Angle_" + item.PatMaxResultPara.Angle;
                            }
                        }
                        if (ParaBlob.RadiusSelectIndexName.Contains("找线工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.RowEnd_" + item.FindLineResultPara.RowEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.RowStart_" + item.FindLineResultPara.RowStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.ColEnd_" + item.FindLineResultPara.ColEnd;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.ColStart_" + item.FindLineResultPara.ColStart;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.ColMiddle_" + item.FindLineResultPara.ColMiddle;
                            }
                            if ((item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindLineResultPara.RowMiddle_" + item.FindLineResultPara.RowMiddle;
                            }
                        }
                        if (ParaBlob.RadiusSelectIndexName.Contains("找圆工具" + iIndex))
                        {
                            if ((item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindCircleResultPara.Row_" + item.FindCircleResultPara.Row;
                            }
                            string str = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            if ((item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindCircleResultPara.Col_" + item.FindCircleResultPara.Col;
                            }
                            if ((item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.FindCircleResultPara.Radius_" + item.FindCircleResultPara.Radius;
                            }
                        }
                        if (ParaBlob.RadiusSelectIndexName.Contains("Blob工具" + iIndex))
                        {
                            for (int k = 0; k < item.BlobResultPara.ListBlobResultPara.Count; k++)
                            {
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row).Contains(ParaBlob.RadiusSelectIndexName))
                                {
                                    strNameRadius = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Row_" + item.BlobResultPara.ListBlobResultPara[k].Row;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col).Contains(ParaBlob.RadiusSelectIndexName))
                                {
                                    strNameRadius = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Col_" + item.BlobResultPara.ListBlobResultPara[k].Col;
                                }
                                if ((item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area).Contains(ParaBlob.RadiusSelectIndexName))
                                {
                                    strNameRadius = item.ToolName + "item.BlobResultPara.ListBlobResultPara[" + k + "].Area_" + item.BlobResultPara.ListBlobResultPara[k].Area;
                                }
                            }
                        }

                        if (ParaBlob.RadiusSelectIndexName.Contains("点线距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.ParaDistancePointLines.Row_" + item.ParaDistancePointLines.Row;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.ParaDistancePointLines.Col_" + item.ParaDistancePointLines.Col;
                            }
                            if ((item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.ParaDistancePointLines.Distance_" + item.ParaDistancePointLines.Distance;
                            }
                        }

                        if (ParaBlob.RadiusSelectIndexName.Contains("点点距离工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameCol1 = item.ToolName + "item.ParaDistancePointPoints.Distance_" + item.ParaDistancePointPoints.Distance;
                            }
                        }
                        if (ParaBlob.RadiusSelectIndexName.Contains("AngleLX工具" + iIndex))
                        {
                            if ((item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle).Contains(ParaBlob.RadiusSelectIndexName))
                            {
                                strNameRadius = item.ToolName + "item.ParasAngleLXResult.Angle_" + item.ParasAngleLXResult.Angle;
                            }
                        }
                    }

                    #endregion

                    iIndex++;
                }
                if (strNameRow1.Length > 0)
                {
                    switch (ParaBlob.BlobRoiPara.RegType)
                    {
                        case GVS.HalconDisp.Control.RegionType.图片:
                            break;
                        case GVS.HalconDisp.Control.RegionType.矩形1:
                            // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyRect1.ArrayRow[0] = double.Parse(strNameRow1.Substring(strNameRow1.LastIndexOf('_') + 1,
                                                                 (strNameRow1.Length - (strNameRow1.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        case GVS.HalconDisp.Control.RegionType.矩形2: // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyRect2.ArrayRow[0] = double.Parse(strNameRow1.Substring(strNameRow1.LastIndexOf('_') + 1,
                                                                 (strNameRow1.Length - (strNameRow1.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        case GVS.HalconDisp.Control.RegionType.圆:
                            // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyCircle.Row = double.Parse(strNameRow1.Substring(strNameRow1.LastIndexOf('_') + 1,
                                                                 (strNameRow1.Length - (strNameRow1.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        case GVS.HalconDisp.Control.RegionType.多边形:

                            break;
                        case GVS.HalconDisp.Control.RegionType.椭圆:                       // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyEllipse.Row = double.Parse(strNameRow1.Substring(strNameRow1.LastIndexOf('_') + 1,
                                                                 (strNameRow1.Length - (strNameRow1.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        case GVS.HalconDisp.Control.RegionType.圆环:                       // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyCircularAnnulusSection.CenterRow = double.Parse(strNameRow1.Substring(strNameRow1.LastIndexOf('_') + 1,
                                                                 (strNameRow1.Length - (strNameRow1.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        default:
                            break;
                    }
                }
                if (strNameCol1.Length > 0)
                {
                    switch (ParaBlob.BlobRoiPara.RegType)
                    {
                        case GVS.HalconDisp.Control.RegionType.图片:
                            break;
                        case GVS.HalconDisp.Control.RegionType.矩形1:
                            // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyRect1.ArrayCol[0] = double.Parse(strNameCol1.Substring(strNameCol1.LastIndexOf('_') + 1,
                                                                 (strNameCol1.Length - (strNameCol1.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        case GVS.HalconDisp.Control.RegionType.矩形2: // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyRect2.ArrayCol[0] = double.Parse(strNameCol1.Substring(strNameCol1.LastIndexOf('_') + 1,
                                                                 (strNameCol1.Length - (strNameCol1.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        case GVS.HalconDisp.Control.RegionType.圆:
                            // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyCircle.Column = double.Parse(strNameCol1.Substring(strNameCol1.LastIndexOf('_') + 1,
                                                                 (strNameCol1.Length - (strNameCol1.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        case GVS.HalconDisp.Control.RegionType.多边形:

                            break;
                        case GVS.HalconDisp.Control.RegionType.椭圆:                       // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyEllipse.Column = double.Parse(strNameCol1.Substring(strNameCol1.LastIndexOf('_') + 1,
                                                                 (strNameCol1.Length - (strNameCol1.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        case GVS.HalconDisp.Control.RegionType.圆环:                       // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyCircularAnnulusSection.CenterCol = double.Parse(strNameCol1.Substring(strNameCol1.LastIndexOf('_') + 1,
                                                                 (strNameCol1.Length - (strNameCol1.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        default:
                            break;
                    }
                }
                if (strNameRadius.Length > 0)
                {
                    switch (ParaBlob.BlobRoiPara.RegType)
                    {
                        case GVS.HalconDisp.Control.RegionType.图片:
                            break;
                        case GVS.HalconDisp.Control.RegionType.矩形1:
                            break;
                        case GVS.HalconDisp.Control.RegionType.矩形2:
                            break;
                        case GVS.HalconDisp.Control.RegionType.圆:
                            // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyCircle.Radius = double.Parse(strNameRadius.Substring(strNameRadius.LastIndexOf('_') + 1,
                                                                 (strNameRadius.Length - (strNameRadius.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        case GVS.HalconDisp.Control.RegionType.多边形:

                            break;
                        case GVS.HalconDisp.Control.RegionType.椭圆:                       // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyEllipse.Radius1 = double.Parse(strNameRadius.Substring(strNameRadius.LastIndexOf('_') + 1,
                                                                 (strNameRadius.Length - (strNameRadius.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        case GVS.HalconDisp.Control.RegionType.圆环:                       // 将链接的数据赋予输入参数
                            ParaBlob.BlobRoiPara.MyCircularAnnulusSection.InnerRadius = double.Parse(strNameRadius.Substring(strNameRadius.LastIndexOf('_') + 1,
                                                                 (strNameRadius.Length - (strNameRadius.LastIndexOf('_') + 1))), NumberStyles.Any);
                            break;
                        default:
                            break;
                    }
                }


                m_hoRegGroup = m_blobPara.BlobRoiPara.Reg;
                if (null != m_hoRegGroup)
                {
                    HObject hoRegRectAll;
                    HOperatorSet.GenEmptyObj(out hoRegRectAll);
                    HObject hoRegPolygonAll;
                    HOperatorSet.GenEmptyObj(out hoRegPolygonAll);
                    HObject hoRegCircleAll;
                    HOperatorSet.GenEmptyObj(out hoRegCircleAll);
                    m_MaskReg = null;
                    HOperatorSet.GenEmptyObj(out m_MaskReg);
                    foreach (GVS.HalconDisp.ViewWindow.Config.Rectangle2 item in m_blobPara.MaskPara.ListRectPara)
                    {
                        HObject hoRectReg;
                        HOperatorSet.GenEmptyObj(out hoRectReg);
                        HOperatorSet.GenContourPolygonXld(out hoRectReg, item.ArrayRow, item.ArrayCol);
                        HOperatorSet.GenRegionContourXld(hoRectReg, out hoRectReg, "filled");
                        HOperatorSet.ConcatObj(hoRegRectAll, hoRectReg, out hoRegRectAll);
                    }
                    foreach (GVS.HalconDisp.ViewWindow.Config.Polygon item in m_blobPara.MaskPara.ListPolygonPara)
                    {
                        HObject hoPolygonReg;
                        HOperatorSet.GenEmptyObj(out hoPolygonReg);
                        HOperatorSet.GenContourPolygonXld(out hoPolygonReg, item.ListPointRowPeak.ToArray(),
                                                          item.ListPointColPeak.ToArray());
                        HOperatorSet.GenRegionContourXld(hoPolygonReg, out hoPolygonReg, "filled");
                        HOperatorSet.ConcatObj(hoRegPolygonAll, hoPolygonReg, out hoRegPolygonAll);
                    }
                    foreach (GVS.HalconDisp.ViewWindow.Config.Circle item in m_blobPara.MaskPara.ListCirclePara)
                    {
                        HObject hoCircleReg;
                        HOperatorSet.GenEmptyObj(out hoCircleReg);
                        HOperatorSet.GenCircle(out hoCircleReg, item.Row, item.Column, item.Radius);
                        HOperatorSet.ConcatObj(hoRegCircleAll, hoCircleReg, out hoRegCircleAll);
                    }
                    HOperatorSet.ConcatObj(m_MaskReg, hoRegPolygonAll, out m_MaskReg);
                    HOperatorSet.ConcatObj(m_MaskReg, hoRegCircleAll, out m_MaskReg);
                    HOperatorSet.ConcatObj(m_MaskReg, hoRegRectAll, out m_MaskReg);

                    HOperatorSet.Difference(m_hoRegGroup, m_MaskReg, out hoRegDifference);
                    if (null != m_hvAffineHom)
                    {
                        HOperatorSet.AffineTransRegion(hoRegDifference,
                                     out hoRegCheckResultAffine, m_hvAffineHom,
                                     "nearest_neighbor");
                    }
                    else
                    {
                        hoRegCheckResultAffine = hoRegDifference;
                    }

                    if (m_hoImage != null)
                    {
                        HObject hoThreshold = null;
                        HOperatorSet.ReduceDomain(m_hoImage, hoRegCheckResultAffine, out hoReduceImage);
                        switch (m_blobPara.EnumSegmentMode)
                        {
                            case EnumModeSegment.硬阈值固定:
                                switch (m_blobPara.EnumSegmentPolarity)
                                {
                                    case EnumPolarity.白底黑点:
                                        HOperatorSet.Threshold(hoReduceImage, out hoThreshold, 0, m_blobPara.Threshold);
                                        break;
                                    case EnumPolarity.黑底白点:
                                        HOperatorSet.Threshold(hoReduceImage, out hoThreshold, m_blobPara.Threshold, 255);
                                        break;
                                }
                                break;
                            case EnumModeSegment.硬阈值相对:
                                HOperatorSet.Threshold(hoReduceImage, out hoThreshold, m_blobPara.MinThreshold, m_blobPara.MaxThreshold);
                                break;
                            case EnumModeSegment.软阈值固定:
                                switch (m_blobPara.EnumSegmentPolarity)
                                {
                                    case EnumPolarity.白底黑点:
                                        HOperatorSet.Threshold(hoReduceImage, out hoThreshold, 0,m_blobPara.MaxThreshold);
                                        break;
                                    case EnumPolarity.黑底白点:
                                        HOperatorSet.Threshold(hoReduceImage, out hoThreshold, m_blobPara.MinThreshold, 255);
                                        break;
                                }
                                break;
                        }
                        HOperatorSet.Connection(hoThreshold, out hoConnection);
                        HOperatorSet.AreaCenter(hoConnection, out hvArea, out hvRow, out hvColumn);
                        if (hvArea != null && hvArea.Length > 0)
                        {
                            for (int iAllConnection = 0; iAllConnection < hvArea.Length; iAllConnection++)
                            {
                                paraResult = new ParaResult();
                                paraResult.Area = hvArea[iAllConnection].D;
                                paraResult.Row = hvRow[iAllConnection].D;
                                paraResult.Col = hvColumn[iAllConnection].D;
                                HObject hoOneConnection = null;
                                HOperatorSet.SelectObj(hoConnection, out hoOneConnection, (iAllConnection + 1));
                                HObject hoCross = null;
                                HOperatorSet.GenCrossContourXld(out hoCross, hvRow[iAllConnection].D, hvColumn[iAllConnection].D, 10, 6);
                                paraResult.HoPointReg = null;
                                paraResult.HoCross = hoCross;
                                paraResult.HoPointReg = hoOneConnection;
                                m_ListParaResultAll.Add(paraResult);
                            }
                        }
                        else
                        {
                            bstate = true;
                            // m_strMsg = "blob筛选面积不足，请确认参数设置！";
                            paraResult = new ParaResult();
                            paraResult.Area = 0;
                            paraResult.Row = 0;
                            paraResult.Col = 0;
                            m_ListParaResultAll.Add(paraResult);
                            HOperatorSet.CountSeconds(out hvTimeEnd);
                            m_dTime = (hvTimeEnd[0].D - hvTimeStart[0].D) * 1000;
                            return bstate;
                        }
                        hoConnectionMode = null;
                        switch (m_blobPara.EnumConnectionMode)
                        {
                            case EnumModeConnection.灰度:
                                switch (m_blobPara.EnumConnectionClear)
                                {
                                    case EnumClear.无:
                                        HOperatorSet.CopyObj(hoThreshold, out hoConnectionMode, 1, -1);
                                        break;
                                    case EnumClear.修剪:
                                        HOperatorSet.SelectShape(hoConnection, out hoConnectionMode, "area", "and", m_blobPara.MinArea, 999999999999999999);
                                        break;
                                    case EnumClear.填充:
                                        HOperatorSet.FillUpShape(hoConnection, out hoConnectionMode, "area", 1, m_blobPara.MinArea);
                                        break;
                                }
                                break;
                        }
                        HOperatorSet.CopyObj(hoConnectionMode, out hoMorphology, 1, -1);
                        foreach (EnumMorphology item in m_blobPara.ListMorPhology)
                        {
                            switch (item)
                            {
                                case EnumMorphology.侵蚀垂直面:
                                    HOperatorSet.ErosionRectangle1(hoMorphology, out hoMorphology, 1, 2);
                                    break;
                                case EnumMorphology.侵蚀水平面:
                                    HOperatorSet.ErosionRectangle1(hoMorphology, out hoMorphology, 2, 1);
                                    break;
                                case EnumMorphology.侵蚀正方形:
                                    HOperatorSet.ErosionRectangle1(hoMorphology, out hoMorphology, 2, 2);
                                    break;
                                case EnumMorphology.扩大垂直面:
                                    HOperatorSet.DilationRectangle1(hoMorphology, out hoMorphology, 1, 2);
                                    break;
                                case EnumMorphology.扩大水平面:
                                    HOperatorSet.DilationRectangle1(hoMorphology, out hoMorphology, 2, 1);
                                    break;
                                case EnumMorphology.扩大正方形:
                                    HOperatorSet.DilationRectangle1(hoMorphology, out hoMorphology, 2, 2);
                                    break;
                                case EnumMorphology.关闭垂直面:
                                    HOperatorSet.ClosingRectangle1(hoMorphology, out hoMorphology, 1, 2);
                                    break;
                                case EnumMorphology.关闭水平面:
                                    HOperatorSet.ClosingRectangle1(hoMorphology, out hoMorphology, 2, 1);
                                    break;
                                case EnumMorphology.关闭正方形:
                                    HOperatorSet.ClosingRectangle1(hoMorphology, out hoMorphology, 2, 2);
                                    break;
                                case EnumMorphology.打开垂直面:
                                    HOperatorSet.OpeningRectangle1(hoMorphology, out hoMorphology, 1, 2);
                                    break;
                                case EnumMorphology.打开水平面:
                                    HOperatorSet.OpeningRectangle1(hoMorphology, out hoMorphology, 2, 1);
                                    break;
                                case EnumMorphology.打开正方形:
                                    HOperatorSet.OpeningRectangle1(hoMorphology, out hoMorphology, 2, 2);
                                    break;
                            }
                        }
                        HObject hoMorphologyConnection = null;
                        if (m_blobPara.EnumConnectionClear != EnumClear.无)
                        {
                            HOperatorSet.Connection(hoMorphology, out hoMorphologyConnection);
                        }

                        else if (m_blobPara.EnumConnectionClear == EnumClear.无)
                        {
                            HOperatorSet.CopyObj(hoMorphology, out hoMorphologyConnection, 1, -1);
                        }
                        HTuple hvAreaSelect = null, hvRowSelect = null, hvColumnSelect = null;
                        HOperatorSet.AreaCenter(hoMorphologyConnection, out hvAreaSelect, out hvRowSelect, out hvColumnSelect);
                        if (hvAreaSelect != null && hvAreaSelect.Length > 0)
                        {
                            for (int iAllConnection = 0; iAllConnection < hvAreaSelect.Length; iAllConnection++)
                            {
                                paraResult = new ParaResult();
                                paraResult.Area = hvAreaSelect[iAllConnection].D;
                                paraResult.Row = hvRowSelect[iAllConnection].D;
                                paraResult.Col = hvColumnSelect[iAllConnection].D;
                                HObject hoOneConnection = null;
                                paraResult.HoPointReg = null;
                                HOperatorSet.SelectObj(hoMorphologyConnection, out hoOneConnection, (iAllConnection + 1));
                                paraResult.HoPointReg = hoOneConnection;
                                HObject hoCross = null;
                                paraResult.HoCross = null;
                                HOperatorSet.GenCrossContourXld(out hoCross, hvRowSelect[iAllConnection].D, hvColumnSelect[iAllConnection].D, 10, 6);
                                paraResult.HoCross = hoCross;
                                m_ListParaResultSelect.Add(paraResult);
                            }
                            m_ListReg.Add(new HObjectWithColor(hoMorphology, "green"));
                        }
                        else
                        {
                            bstate = true;
                            //m_strMsg = "blob筛选面积不足，请确认参数设置！";
                            paraResult = new ParaResult();
                            paraResult.Area = -100;
                            paraResult.Row =-100;
                            paraResult.Col = -100;
                            paraResult.HoCross = null;
                            paraResult.HoPointReg = null;
                            m_ListParaResultSelect.Add(paraResult);
                            HOperatorSet.CountSeconds(out hvTimeEnd);
                            m_dTime = (hvTimeEnd[0].D - hvTimeStart[0].D) * 1000;
                            return bstate;
                        }
                    }
                    else
                    {
                        bstate = false;
                        m_strMsg = "请读取图片！";
                        paraResult = new ParaResult();
                        paraResult.Area = -100;
                        paraResult.Row = -100;
                        paraResult.Col = -100;
                        m_ListParaResultSelect.Add(paraResult);
                        HOperatorSet.CountSeconds(out hvTimeEnd);
                        m_dTime = (hvTimeEnd[0].D - hvTimeStart[0].D) * 1000;
                        return bstate;
                    }
                }
                else
                {
                    bstate = false;
                    m_ListReg = new List<HObjectWithColor>();
                    m_strMsg = "请绘制检测区域！";
                    paraResult = new ParaResult();
                    paraResult.Area = -100;
                    paraResult.Row = -100;
                    paraResult.Col = -100;
                    m_ListParaResultSelect.Add(paraResult);
                    HOperatorSet.CountSeconds(out hvTimeEnd);
                    m_dTime = (hvTimeEnd[0].D - hvTimeStart[0].D) * 1000;
                    return bstate;
                }
            }
            catch (Exception ex)
            {
                bstate = false;
                m_ListReg = new List<HObjectWithColor>();
                m_strMsg = "catch到错误" + ex.ToString();
                paraResult = new ParaResult();
                paraResult.Area = -100;
                paraResult.Row = -100;
                paraResult.Col = -100;
                m_ListParaResultSelect.Add(paraResult);
                HOperatorSet.CountSeconds(out hvTimeEnd);
                m_dTime = (hvTimeEnd[0].D - hvTimeStart[0].D) * 1000;
                return bstate;
            }
            HOperatorSet.CountSeconds(out hvTimeEnd);
            m_dTime = (hvTimeEnd[0].D - hvTimeStart[0].D) * 1000;
            return bstate;
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Save(object obj, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
