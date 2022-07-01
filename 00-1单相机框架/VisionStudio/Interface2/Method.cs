using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Xml.Serialization;
using GVS.HalconDisp.ViewROI.Config;
using HalconDotNet;
using SvDetectionModuleTool;
using SvsPLC;

namespace VisionStudio
{
    /// <summary>
    /// 内 容:本类是总方法类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public class Method : ToolInterFace.ToolInterface
    {
        /// <summary>
        /// 检测方法类数据
        /// </summary>
        public SvDetectionModuleToolMethod[] m_ListModuleMethod = new SvDetectionModuleToolMethod[8];

        /// <summary>
        /// PLC运行方法
        /// </summary>
        public SvsPLC.PLCMethod[] m_listPlcMethod = new SvsPLC.PLCMethod[8];

        /// <summary>
        /// 相机集合
        /// </summary>
        public SvsMVSCamera.CameraAPI[] m_ArrayCameraMethod = new SvsMVSCamera.CameraAPI[8];

        /// <summary>
        /// 构造方法
        /// </summary>
        public Method()
        {
            for (int i = 0; i < m_ListModuleMethod.Length; i++)
            {
                m_ListModuleMethod[i] = new SvDetectionModuleToolMethod();
                m_listPlcMethod[i] = new PLCMethod();
            }
        }

        private Para m_Para = new Para();

        /// <summary>
        /// 参数类
        /// </summary>
        public Para Paras
        {
            get
            {
                for (int i = 0; i < m_Para.ArrayModulePara[i].ListToolParaAdd.Count; i++)
                {
                    m_ListModuleMethod[i].m_AngleLXMethod.SvAngleLXParas = m_Para.ArrayModulePara[i].ListToolParaAdd[i].AngleLxPara;
                    m_listPlcMethod[i].Para = m_Para.ArrayModulePara[i].ParasPlc;            
                }
                return m_Para;
            }
            set
            {
                m_Para = value;
                for (int i = 0; i < m_Para.ArrayModulePara.Length; i++)
                {
                    //for (int j = 0; j < m_Para.ArrayModulePara[i].ListToolParaAdd.Count; j++)
                    {
                        m_ListModuleMethod[i].ModualPara = m_Para.ArrayModulePara[i];
                        m_listPlcMethod[i].Para = m_Para.ArrayModulePara[i].ParasPlc;
                    }
                }
            }
        }

        private HObject m_InputImage = null;

        /// <summary>
        /// 输入图像
        /// </summary>
        public HObject InputImage
        {
            get
            {
                return m_InputImage;
            }
            set
            {
                m_InputImage = value;
                for (int i = 0; i < m_ListModuleMethod.Length; i++)
                {
                    m_ListModuleMethod[i].InputImage = m_InputImage;
                }
            }
        }

        private List<HObjectWithColor> m_listReg = new List<HObjectWithColor>();

        /// <summary>
        /// 结果总区域
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

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public object Load(Type type, string filename)
        {
            m_strRunMsg = string.Empty;
               HTuple hvModelId = null;
            FileStream fs = null;
            try
            {
                string strarry = filename.Substring(0, filename.LastIndexOf("\\"));
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                for (int i = 0; i < m_ListModuleMethod.Length; i++)
                {
                    string strFile = strarry + "\\" + i;
                    for (int j = 0; j < m_ListModuleMethod[i].m_ListModelID.Length; j++)
                    {
                        if (Directory.Exists(strFile))
                        {
                            if (File.Exists(strFile + "\\" + j + "SingleCameraModualModelID.shm"))
                            {
                                hvModelId = null;
                                HOperatorSet.ReadShapeModel(strFile + "\\" + j + "SingleCameraModualModelID.shm",
                                                            out hvModelId);
                                m_ListModuleMethod[i].m_ListModelID[j] = hvModelId;
                            }
                            //if (File.Exists(strFile + "\\" + i + "SingleModualCreateContour.hobj"))
                            //{
                            //    hoCreateContourAffine = null;
                            //    HOperatorSet.ReadObject(out hoCreateContourAffine,
                            //                            strFile + "\\" + i + "SingleModualCreateContour.hobj");
                            //    m_ListModuleMethod[i].m_PatMaxMethod.CreateContourModel = hoCreateContourAffine;
                            //}
                        }
                    }
                }
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                m_strRunMsg = ex.ToString();
                m_Para = new Para();
                return m_Para;
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
            bool bState = false;
            return bState;
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Save(object obj, string filename)
        {
            m_strRunMsg = string.Empty;
            FileStream fs = null;
            try
            {
                string strarry = filename.Substring(0, filename.LastIndexOf("\\"));
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                for (int i = 0; i < m_ListModuleMethod.Length; i++)
                {
                    // 启用保存
                    string strFile = strarry + "\\" + i;
                    for (int j = 0; j < m_ListModuleMethod[i].m_ListModelID.Length; j++)
                    {
                        if (null != m_ListModuleMethod[i].m_ListModelID[j])
                        {
                            if (Directory.Exists(strFile))
                            {
                                HOperatorSet.WriteShapeModel(m_ListModuleMethod[i].m_ListModelID[j], strFile + "\\" + j + "SingleCameraModualModelID.shm");
                            }
                            else
                            {
                                Directory.CreateDirectory(strFile);
                                if (Directory.Exists(strFile))
                                {
                                    HOperatorSet.WriteShapeModel(m_ListModuleMethod[i].m_ListModelID[j], strFile + "\\" + j + "SingleCameraModualModelID.shm");
                                }

                            }
                        }
                    }

                }
                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                m_strRunMsg = ex.ToString();
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
