using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Interface2
{

    [Serializable]
    [XmlRoot("MainPara")]
    public class MainPara : EventArgs
    {
        private int m_NumDisp=5;

        /// <summary>
        /// 相机个数确认——工位数量
        /// </summary>
        public int NumDisp { get { return m_NumDisp; } set { m_NumDisp = value; } }

        #region 图像保存设置
        
        private string m_ImageRunSave1 = "不保存";
        /// <summary>
        /// 工位1 图像保存
        /// </summary>
        public string ImageRunSave1 { get { return m_ImageRunSave1; } set { m_ImageRunSave1 = value; } }

        private string m_ImageRunSave2 = "不保存";
        /// <summary>
        /// 工位2 图像保存
        /// </summary>
        public string ImageRunSave2 { get { return m_ImageRunSave2; } set { m_ImageRunSave2 = value; } }

        private string m_ImageRunSave3 = "不保存";
        /// <summary>
        /// 工位3-1 图像保存
        /// </summary>
        public string ImageRunSave3 { get { return m_ImageRunSave3; } set { m_ImageRunSave3 = value; } }

        private string m_ImageRunSave4 = "不保存";
        /// <summary>
        /// 工位3-2 图像保存
        /// </summary>
        public string ImageRunSave4 { get { return m_ImageRunSave4; } set { m_ImageRunSave4 = value; } }

        private string m_ImageRunSave5 = "不保存";
        /// <summary>
        /// 工位4 图像保存
        /// </summary>
        public string ImageRunSave5 { get { return m_ImageRunSave5; } set { m_ImageRunSave5 = value; } }


        #endregion

        #region 显示图像保存设置

        private string m_ImageShowSave1 = "不保存";
        /// <summary>
        /// 工位1 图像保存
        /// </summary>
        public string ImageShowSave1 { get { return m_ImageShowSave1; } set { m_ImageShowSave1 = value; } }

        private string m_ImageShowSave2 = "不保存";
        /// <summary>
        /// 工位2 图像保存
        /// </summary>
        public string ImageShowSave2 { get { return m_ImageShowSave2; } set { m_ImageShowSave2 = value; } }

        private string m_ImageShowSave3 = "不保存";
        /// <summary>
        /// 工位3-1 图像保存
        /// </summary>
        public string ImageShowSave3 { get { return m_ImageShowSave3; } set { m_ImageShowSave3 = value; } }

        private string m_ImageShowSave4 = "不保存";
        /// <summary>
        /// 工位3-2 图像保存
        /// </summary>
        public string ImageShowSave4 { get { return m_ImageShowSave4; } set { m_ImageShowSave4 = value; } }

        private string m_ImageShowSave5 = "不保存";
        /// <summary>
        /// 工位4 图像保存
        /// </summary>
        public string ImageShowSave5 { get { return m_ImageShowSave5; } set { m_ImageShowSave5 = value; } }


        #endregion

        #region 工位参数名

        private string m_gw1ParaName;

        /// <summary>
        /// 工位1 参数文件名
        /// </summary>
        public string gw1ParaName { get { return m_gw1ParaName; } set { m_gw1ParaName = value; } }

        private string m_gw2ParaName;

        /// <summary>
        /// 工位2 参数文件名
        /// </summary>
        public string gw2ParaName { get { return m_gw2ParaName; } set { m_gw2ParaName = value; } }

        private string m_gw3ParaName;

        /// <summary>
        /// 工位3参数文件名
        /// </summary>
        public string gw3ParaName { get { return m_gw3ParaName; } set { m_gw3ParaName = value; } }

        private string m_gw4ParaName;

        /// <summary>
        /// 工位4参数文件名 对应第3-2个相机
        /// </summary>
        public string gw4ParaName { get { return m_gw4ParaName; } set { m_gw4ParaName = value; } }

        private string m_gw5ParaName;

        /// <summary>
        /// 工位5参数文件名  
        /// </summary>
        public string gw5ParaName { get { return m_gw5ParaName; } set { m_gw5ParaName = value; } }



        #endregion

        #region 相机参数   运行

        private int m_expose1 = 5000;
        /// <summary>
        /// 相机1  
        /// </summary>
        public int expose1 { get { return m_expose1; } set { m_expose1 = value; } }

        private int m_gain1 = 0;
        /// <summary>
        /// 相机1  
        /// </summary>
        public int gain1 { get { return m_gain1; } set { m_gain1 = value; } }

        private int m_expose2 = 5000;
        /// <summary>
        /// 相机2  
        /// </summary>
        public int expose2 { get { return m_expose2; } set { m_expose2 = value; } }

        private int m_gain2 = 0;
        /// <summary>
        /// 相机2  
        /// </summary>
        public int gain2 { get { return m_gain2; } set { m_gain2 = value; } }

        private int m_expose3 = 5000;
        /// <summary>
        /// 相机3 
        /// </summary>
        public int expose3 { get { return m_expose3; } set { m_expose3 = value; } }

        private int m_gain3 = 0;
        /// <summary>
        /// 相机3
        /// </summary>
        public int gain3 { get { return m_gain3; } set { m_gain3 = value; } }

        private int m_expose4 = 5000;
        /// <summary>
        /// 相机4  
        /// </summary>
        public int expose4 { get { return m_expose4; } set { m_expose4 = value; } }

        private int m_gain4 = 0;
        /// <summary>
        /// 相机4
        /// </summary>
        public int gain4 { get { return m_gain4; } set { m_gain4 = value; } }

        private int m_expose5 = 5000;
        /// <summary>
        /// 相机5  当5个相机时，第三工位的第二个相机
        /// </summary> 
        public int expose5 { get { return m_expose5; } set { m_expose5 = value; } }

        private int m_gain5 = 0;
        /// <summary>
        /// 相机5  当5个相机时，第三工位的第二个相机
        /// </summary>
        public int gain5 { get { return m_gain5; } set { m_gain5 = value; } }



        #endregion

        #region PLC通信连接参数
        private string m_PLCSelect = "欧姆龙";

        /// <summary>
        /// PLC选择
        /// </summary>
        public string PLCSelect { get { return m_PLCSelect; } set { m_PLCSelect = value; } }

        private int m_PC_finalIP_omr = 2;
        ///电脑IP末位_欧姆龙
        /// </summary>
        public int PC_finalIP_omr { get { return m_PC_finalIP_omr; } set { m_PC_finalIP_omr = value; } }


        private int m_PLC_Port_omr = 9600;
        ///PLC 端口_欧姆龙
        /// </summary>
        public int PLC_Port_omr { get { return m_PLC_Port_omr; } set { m_PLC_Port_omr = value; } }

        private string m_PLC_IP_omr = "192.168.250.1";
        ///PLC 端口_欧姆龙
        /// </summary>
        public string PLC_IP_omr { get { return m_PLC_IP_omr; } set { m_PLC_IP_omr = value; } }


        //*********************************************
        private int m_PLC_Port_kv = 8501;
        ///PLC 端口_基恩士
        /// </summary>
        public int PLC_Port_kv { get { return m_PLC_Port_kv; } set { m_PLC_Port_kv = value; } }

        private string m_PLC_IP_kv = "192.168.1.109";
        ///PLC 端口_基恩士
        /// </summary>
        public string PLC_IP_kv { get { return m_PLC_IP_kv; } set { m_PLC_IP_kv = value; } }

        #endregion

        #region 显示数据记录

        private double[] m_gw1_LVDataDisplay = new double[8] { 0, 0, 0, 0, 0, 0, 0 ,0};
        ///工位1 显示数据记录
        /// </summary>
        public double[] gw1_LVDataDisplay { get { return m_gw1_LVDataDisplay; } set { m_gw1_LVDataDisplay = value; } }

        private double[] m_gw2_LVDataDisplay = new double[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        ///工位2 显示数据记录
        /// </summary>
        public double[] gw2_LVDataDisplay { get { return m_gw2_LVDataDisplay; } set { m_gw2_LVDataDisplay = value; } }

        private double[] m_gw3_LVDataDisplay = new double[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        ///工位3 显示数据记录
        /// </summary>
        public double[] gw3_LVDataDisplay { get { return m_gw3_LVDataDisplay; } set { m_gw3_LVDataDisplay = value; } }

        private double[] m_gw4_LVDataDisplay = new double[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        ///工位3-2 显示数据记录
        /// </summary>
        public double[] gw4_LVDataDisplay { get { return m_gw4_LVDataDisplay; } set { m_gw4_LVDataDisplay = value; } }


        private double[] m_gw5_LVDataDisplay = new double[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        ///工位4 显示数据记录
        /// </summary>
        public double[] gw5_LVDataDisplay { get { return m_gw5_LVDataDisplay; } set { m_gw5_LVDataDisplay = value; } }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainPara()
        { 
        
        
        
        
        
        }


    }
}
