using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvsPLC
{
    /// <summary>
    /// 主要内容：PLC通讯，输入两个参数：工位+Halcon输出的结果数据（参数类）
    /// 时    间：2021/4/20
    /// 作    者：陈媛媛
    /// </summary>
    [Serializable]
    [XmlRoot("PLCPara")]
    public class PLCPara
    {
        private string ms_Brand = "基恩士";
        /// <summary>
        /// PLC品牌
        /// </summary>
        public string Brand
        {
            get
            {
                return this.ms_Brand;
            }
            set
            {
                this.ms_Brand = value;
            }
        }

        private string ms_Ip = "192.168.1.109";

        /// <summary>
        /// IP
        /// </summary>
        public string Ip
        {
            get
            {
                return this.ms_Ip;
            }
            set
            {
                this.ms_Ip = value;
            }
        }

        private int mi_Port = 8501;

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port
        {
            get
            {
                return this.mi_Port;
            }
            set
            {
                this.mi_Port = value;
            }
        }

        private int mi_PCIp = 10;

        /// <summary>
        /// PC-末位IP
        /// </summary>
        public int PCIp
        {
            get
            {
                return this.mi_PCIp;
            }
            set
            {
                this.mi_PCIp = value;
            }
        }

        private List<string> mlist_Register = new List<string>();

        /// <summary>
        /// 寄存器
        /// </summary>
        public List<string> listRegister
        {
            get
            {
                return this.mlist_Register;
            }
            set
            {
                this.mlist_Register = value;
            }
        }

        private List<int> mlist_Begin = new List<int>();

        /// <summary>
        /// 寄存器起始位置
        /// </summary>
        public List<int> listBegin
        {
            get
            {
                return this.mlist_Begin;
            }
            set
            {
                this.mlist_Begin = value;
            }
        }

        private List<int> mlist_Numbers = new List<int>();

        /// <summary>
        /// 数据个数
        /// </summary>
        public List<int> listNumbers
        {
            get
            {
                return this.mlist_Numbers;
            }
            set
            {
                this.mlist_Numbers = value;
            }
        }
    }
}
