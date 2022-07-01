using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SvDetectionModuleTool;

namespace VisionStudio
{
    /// <summary>
    /// 触发模式
    /// </summary>
    [Serializable]
    [XmlRoot("EnumTrigger")]
    public enum EnumTrigger
    {
        /// <summary>
        /// 硬触发
        /// </summary>
        硬触发,

        /// <summary>
        /// 软触发
        /// </summary>
        软触发,
    }

    /// <summary>
    /// 保存图像类型
    /// </summary>
    [Serializable]
    [XmlRoot("ParaIamgeSave")]
    public class ParaImageSave
    {
        /// <summary>
        /// 图像保存类型
        /// </summary>
        public enum ImageSaveType
        {/// <summary>
         /// OK图像
         /// </summary>
            OK,
            /// <summary>
            /// NG
            /// </summary>
            NG,
            /// <summary>
            /// All
            /// </summary>
            ALL,
            /// <summary>
            /// No
            /// </summary>
            NO,
        }

        private ImageSaveType m_ImageSave = ImageSaveType.NO;

        /// <summary>
        /// 图像保存类型
        /// </summary>
        public ImageSaveType ImageSaveTypes
        {
            get
            {
                return m_ImageSave;
            }
            set
            {
                m_ImageSave = value;
            }
        }

        private int m_iSaveDay = 3;

        /// <summary>
        /// 保存最近几天的图片
        /// </summary>
        public int SaveDay
        {
            get
            {
                return m_iSaveDay;
            }
            set
            {
                m_iSaveDay = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ParaImageSave()
        {
            m_ImageSave = ImageSaveType.NO;
        }
    }

    /// <summary>
    /// 内 容:本类是总参数类
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    [Serializable]
    [XmlRoot("Para")]
    public class Para
    {

        private bool m_bPLCOne = true;

        /// <summary>
        /// 启用一个或多个相机
        /// </summary>
        public bool PLCOneState
        {
            get
            {
                return m_bPLCOne;
            }
            set
            {
                m_bPLCOne = value;
            }
        }

        private string m_strSaveFileName = string.Empty;

        /// <summary>
        /// 保存路径
        /// </summary>
        public string SaveFileName
        {
            get
            {
                return m_strSaveFileName;
            }
            set
            {
                m_strSaveFileName = value;
            }
        }

        private SvDetectionModuleTool.SvDetectionModuleToolPara m_ModulePara = new SvDetectionModuleToolPara();

        /// <summary>
        /// 通用参数类
        /// </summary>
        public SvDetectionModuleToolPara ModulePara
        {
            get
            {
                return m_ModulePara;
            }
            set
            {
                m_ModulePara = value;
            }
        }

        private ParaImageSave m_ParaImageSave = new ParaImageSave();

        /// <summary>
        /// 相机保存图像类型设置
        /// </summary>
        public ParaImageSave ParasImageSave
        {
            get
            {
                return m_ParaImageSave;
            }
            set
            {
                m_ParaImageSave = value;
            }
        }

        private SvDetectionModuleToolPara[] m_ArrayModulePara = new SvDetectionModuleToolPara[8];

        /// <summary>
        /// 多相机对应参数
        /// </summary>
        public SvDetectionModuleToolPara[] ArrayModulePara
        {
            get
            {
                return m_ArrayModulePara;
            }
            set
            {
                m_ArrayModulePara = value;
            }
        }


        private EnumTrigger m_EnumTriggerMode = EnumTrigger.硬触发;

        /// <summary>
        /// 触发模式设置
        /// </summary>
        public EnumTrigger EnumTriggerMode
        {
            get
            {
                return m_EnumTriggerMode;
            }
            set
            {
                m_EnumTriggerMode = value;
            }
        }

        private int m_iCameraNum = 3;

        /// <summary>
        /// 相机个数
        /// </summary>
        public int CameraNum
        {
            get
            {
                return m_iCameraNum;
            }
            set
            {
                m_iCameraNum = value;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public Para()
        {
            m_iCameraNum = 3;
            for (int i = 0; i < 8; i++)
            {
                m_ArrayModulePara[i] = new SvDetectionModuleToolPara();
            }
        }
    }
}
