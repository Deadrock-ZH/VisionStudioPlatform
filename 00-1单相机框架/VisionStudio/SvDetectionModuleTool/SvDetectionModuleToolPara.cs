using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SvDistancePointLineTool;
using SvDistancePointPointTool;
using SvJudgeResultParaTool;
using SvVisualCorrectionTool;

namespace SvDetectionModuleTool
{
    /// <summary>
    /// 模块参数类
    /// </summary>
    [Serializable]
    [XmlRoot("SvDetectionModuleToolPara")]
    public class SvDetectionModuleToolPara
    {
        private ToolParaAdd m_ToolParaAdd = new ToolParaAdd();

        /// <summary>
        /// 工具添加类
        /// </summary>
        public ToolParaAdd ToolParasAdd
        {
            get
            {
                return m_ToolParaAdd;
            }
            set
            {
                m_ToolParaAdd = value;
            }
        }

        private List<ToolParaAdd> m_ListToolParaAdd = new List<ToolParaAdd>();

        /// <summary>
        /// 工具添加类
        /// </summary>
        public List<ToolParaAdd> ListToolParaAdd
        {
            get
            {
                return m_ListToolParaAdd;
            }
            set
            {
                m_ListToolParaAdd = value;
            }
        }


        private bool m_bLine = true;

        /// <summary>
        /// 是否显示线轮廓状态
        /// </summary>
        public bool LineState
        {
            get
            {
                return m_bLine;
            }
            set
            {
                m_bLine = value;
            }
        }

        private bool m_bAngleLXState = true;

        /// <summary>
        /// 是否显示Anglelx
        /// </summary>
        public bool AngleLXState
        {
            get
            {
                return m_bAngleLXState;
            }
            set
            {
                m_bAngleLXState = value;
            }
        }

        private bool m_bDistancePLState = true;

        /// <summary>
        /// 是否显示点线距离
        /// </summary>
        public bool DistancePLState
        {
            get
            {
                return m_bDistancePLState;
            }
            set
            {
                m_bDistancePLState = value;
            }
        }

        private bool m_bDistancePPState = true;

        /// <summary>
        /// 是否显示点点距离
        /// </summary>
        public bool DistancePPState
        {
            get
            {
                return m_bDistancePPState;
            }
            set
            {
                m_bDistancePPState = value;
            }
        }

        private bool m_bAngleLLState = true;

        /// <summary>
        /// 是否显示线线角度
        /// </summary>
        public bool AngleLLState
        {
            get
            {
                return m_bAngleLLState;
            }
            set
            {
                m_bAngleLLState = value;
            }
        }

        private bool m_bCircle = true;

        /// <summary>
        /// 是否显示圆轮廓状态
        /// </summary>
        public bool CircleState
        {
            get
            {
                return m_bCircle;
            }
            set
            {
                m_bCircle = value;
            }
        }

        private bool m_bPatMax = true;

        /// <summary>
        /// 是否显示匹配轮廓状态
        /// </summary>
        public bool PatMaxState
        {
            get
            {
                return m_bPatMax;
            }
            set
            {
                m_bPatMax = value;
            }
        }

        private bool m_bBlob = true;

        /// <summary>
        /// 是否显示Blob状态
        /// </summary>
        public bool BlobState
        {
            get
            {
                return m_bBlob;
            }
            set
            {
                m_bBlob = value;
            }
        }

        private SvsPLC.PLCPara m_PlcPara = new SvsPLC.PLCPara();

        /// <summary>
        /// PLC参数类
        /// </summary>
        public SvsPLC.PLCPara ParasPlc
        {
            get
            {
                return m_PlcPara;
            }
            set
            {
                m_PlcPara = value;
            }
        }

        //相机参数
        private float m_cameraGain;
        public float cameraGain 
        {
            get { return m_cameraGain; }
            set { m_cameraGain = value; }
        }

        private float m_cameraExposureTime;
        public float cameraExposureTime
        {
            get { return m_cameraExposureTime; }
            set { m_cameraExposureTime = value; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public SvDetectionModuleToolPara()
        {
            m_ListToolParaAdd = new List<ToolParaAdd>();
            m_bBlob = true;
            m_bCircle = true;
            m_bLine = true;
            m_bPatMax = true;
            cameraGain = 0;
            cameraExposureTime = 5000;
        }
    }

    /// <summary>
    /// 模块添加参数类
    /// </summary>
    [Serializable]
    [XmlRoot("ToolParaAdd")]
    public class ToolParaAdd
    {      

        private SvDistancePointPointToolPara m_DistancePointPointToolPara = new SvDistancePointPointToolPara();

        /// <summary>
        /// 点到点的参数类
        /// </summary>
        public SvDistancePointPointTool.SvDistancePointPointToolPara DistancePointPointToolParas
        {
            get
            {
                return m_DistancePointPointToolPara;
            }
            set
            {
                m_DistancePointPointToolPara = value;
            }
        }

        private SvBlobTool.BlobToolPara m_BlobPara = new SvBlobTool.BlobToolPara();

        /// <summary>
        /// Blob参数类
        /// </summary>
        public SvBlobTool.BlobToolPara BlobParas
        {
            get
            {
                return m_BlobPara;
            }
            set
            {
                m_BlobPara = value;
            }
        }

        private SvAngleLX.SvAngleLXToolPara m_AngleLxPara = new SvAngleLX.SvAngleLXToolPara();

        /// <summary>
        /// 角度计算参数类
        /// </summary>
        public SvAngleLX.SvAngleLXToolPara AngleLxPara
        {
            get
            {
                return m_AngleLxPara;
            }
            set
            {
                m_AngleLxPara = value;
            }
        } /// <summary>
          /// 工具名称
          /// </summary>
        public string ToolName
        {
            get;
            set;
        }

        /// <summary>
        /// 行坐标启用状态
        /// </summary>
        public bool RowState
        {
            get;
            set;
        }

        /// <summary>
        /// 列坐标启用状态
        /// </summary>
        public bool ColumnState
        {
            get;
            set;
        }

        /// <summary>
        /// 角度启用状态
        /// </summary>
        public bool AngleState
        {
            get;
            set;
        }

        /// <summary>
        /// 半径启用状态
        /// </summary>
        public bool RadiusState
        {
            get;
            set;
        }

        /// <summary>
        /// 仿射矩阵启用状态
        /// </summary>
        public bool AffinehomState
        {
            get;
            set;
        }

        private SvsPatMax.SvsPatMaxPara m_PatMaxPara = new SvsPatMax.SvsPatMaxPara();

        /// <summary>
        /// 匹配参数类
        /// </summary>
        public SvsPatMax.SvsPatMaxPara PatMaxPara
        {
            get
            {
                return m_PatMaxPara;
            }
            set
            {
                m_PatMaxPara = value;
            }
        }

        private SvJudgeResultParaTool.SvJudgeResultParaToolPara m_SvJudgeResultParaToolPara = new SvJudgeResultParaToolPara();

        /// <summary>
        /// 结果判断参数类
        /// </summary>
        public SvJudgeResultParaToolPara SvJudgeResultParaToolParas
        {
            get
            {
                return m_SvJudgeResultParaToolPara;
            }
            set
            {
                m_SvJudgeResultParaToolPara = value;
            }
        }

        private SvVisualCorrectionToolPara m_SvVisualCorrectionToolPara = new SvVisualCorrectionToolPara();

        /// <summary>
        /// 示教参数类
        /// </summary>
        public SvVisualCorrectionToolPara SvVisualCorrectionToolParas
        {
            get
            {
                return m_SvVisualCorrectionToolPara;
            }
            set
            {
                m_SvVisualCorrectionToolPara = value;
            }
        }

        private SvsFindCircleTool.FindCirclePara m_FindCirclePara = new SvsFindCircleTool.FindCirclePara();

        /// <summary>
        /// 找圆参数类
        /// </summary>
        public SvsFindCircleTool.FindCirclePara FindCircleParas
        {
            get
            {
                return m_FindCirclePara;
            }
            set
            {
                m_FindCirclePara = value;
            }
        }

        private SvAngleLineLineTool.SvAngleLineLineToolPara m_SvAngleLineLineToolPara = new SvAngleLineLineTool.SvAngleLineLineToolPara();

        /// <summary>
        /// 线线角度参数类
        /// </summary>
        public SvAngleLineLineTool.SvAngleLineLineToolPara SvAngleLineLineToolParas
        {
            get
            {
                return m_SvAngleLineLineToolPara;
            }
            set
            {
                m_SvAngleLineLineToolPara = value;
            }
        }

        private SvsFindLineTool.FindLinePara m_FindLinePara = new SvsFindLineTool.FindLinePara();

        /// <summary>
        /// 找线参数类
        /// </summary>
        public SvsFindLineTool.FindLinePara FindLineParas
        {
            get
            {
                return m_FindLinePara;
            }
            set
            {
                m_FindLinePara = value;
            }
        }

        private SvDistancePointLineTool.SvDistancePointLineToolPara m_DistancePointLineToolPara = new SvDistancePointLineToolPara();

        /// <summary>
        /// 点到直线的参数类
        /// </summary>
        public SvDistancePointLineToolPara DistancePointLineToolParas
        {
            get
            {
                return m_DistancePointLineToolPara;
            }
            set
            {
                m_DistancePointLineToolPara = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ToolParaAdd()
        {
            ToolName = string.Empty;
            RowState = false;
            ColumnState = false;
            AngleState = false;
            RadiusState = false;
            AffinehomState = true;
            m_PatMaxPara = new SvsPatMax.SvsPatMaxPara();
            m_FindCirclePara = new SvsFindCircleTool.FindCirclePara();
            m_FindLinePara = new SvsFindLineTool.FindLinePara();
            m_BlobPara = new SvBlobTool.BlobToolPara();
            m_SvVisualCorrectionToolPara = new SvVisualCorrectionToolPara();
        }
    }
}
