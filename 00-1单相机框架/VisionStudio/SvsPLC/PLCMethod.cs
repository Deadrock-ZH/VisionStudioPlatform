using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HalconDotNet;
using System.Threading;
using System.Xml.Serialization;
using System.Windows.Forms;


namespace SvsPLC
{
    /// <summary>
    /// 主要内容：PLC通讯，输入两个参数：工位+Halcon输出的结果数据（方法类）
    /// 时    间：2021/4/20
    /// 作    者：陈媛媛
    /// </summary>    
    public class PLCMethod
    {
        /// <summary>
        /// 方法类
        /// </summary>
        private NetworkStream readstream;
        private NetworkStream stream;
        private StreamWriter sWriter;
        private TcpClient client;

        private bool m_bConnect = false;

        /// <summary>
        /// PLC连接状态
        /// </summary>
        public bool ConnectState
        {
            get
            {
                return m_bConnect;
            }
            set
            {
                m_bConnect = value;
            }
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

        /// <summary>
        /// PLC参数类
        /// </summary>
        public PLCPara Para = new PLCPara();

        /// <summary>
        /// 文件化XML序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public void Save(object obj, string filename)
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
                m_strRunMsg = "PLC" + ex.ToString();
                return;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
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

        #region 欧姆龙发送数据

        /// <summary>
        /// 欧姆龙数据发送
        /// </summary>
        /// <param name="Camera">发送对应相机索引</param>
        /// <param name="SendData">发送的数据</param>
        public void SendData_OMRON(int Camera, HTuple SendData)
        {
            if (Camera > Para.listRegister.Count)
            {
                m_strRunMsg = "工位设置不匹配！";
                //MessageBox.Show("工位设置不匹配！");
                return;
            }
            if (Para.listNumbers[Camera - 1] > SendData.Length)
            {
                m_strRunMsg = "发送数据长度大于输出结果！";
                // MessageBox.Show("发送数据长度大于输出结果！");
                return;
            }
            int Register;
            if (Para.listRegister[0] == "D")
            {
                Register = 130;
            }
            else
            {
                //CIO写入80即128
                Register = 128;
            }
            int NoWrite = Para.listBegin[Camera - 1];
            int quantityWrite = (Para.listNumbers[Camera - 1]) * 2;
            HTuple SendData2 = null;
            int index = Para.listNumbers[Camera - 1] - 1;
            HOperatorSet.TupleSelectRange(SendData, 0, index, out SendData2);
            string message = SendData2.ToString();
            char[] a = { '[', ']' };
            message = message.TrimStart(a).TrimEnd(a);

            //PLCIP,PLC端口，电脑IP末位，写入寄存器区（D需要写入82及130，CIO写入80即128），寄存器地址(起始地址1000写入1000)，写入个数，数据
            WriteISPLCConnection11(Para.Ip, Para.Port, Para.PCIp, Register, NoWrite, quantityWrite, message);
        }


        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="PLC_IP_adress">PLCIP</param>
        /// <param name="FINS_UDP_PORT">PLC端口</param>
        /// <param name="ComIPLastPlace">电脑IP末位</param>
        /// <param name="memory">写入寄存器区</param>
        /// <param name="StartP">寄存器地址</param>
        /// <param name="Readquantity">写入个数</param>
        /// <param name="str">数据</param>
        public void WriteISPLCConnection11(string PLC_IP_adress, int FINS_UDP_PORT,
                                           int ComIPLastPlace, int memory, int StartP,
                                           int Readquantity, string str)
        {

            int Readquantity1 = Readquantity;

            byte[] send_data11 = new byte[18 + Readquantity1 * 2];

            string serv_IP_adress = PLC_IP_adress;//测试用ip地址，跟据实际更改
                                                  //  const int FINS_UDP_PORT = 9600;//端口号
            IPEndPoint remote_ip = new IPEndPoint(IPAddress.Parse(serv_IP_adress), FINS_UDP_PORT);//远程节点对象
            UdpClient newclient = new UdpClient();
            byte[] send_data = new byte[]
            {
                0x80, //0.(ICF) Display frame information: 1000 0001
                0x00, //1.(RSV) Reserved by system: (hex)00
                0x02, //2.(GCT) Permissible number of gateways: (hex)02
                0x00, //3.(DNA) Destination network address: (hex)00, local network
                0x01, //4.(DA1)** Destination node address: (hex)00, local PLC unit，PLC IP地址最后一位，例106=16#6A
                0x00, //5.(DA2) Destination unit address: (hex)00, PLC
                0x00, //6.(SNA) Source network address: (hex)00, local network
                0x64, //7.(SA1)** Source node address   ，本地电脑的IP最后一位，例156=16#9C
                0x00, //8.(SA2) Source unit address: (hex)00, PC only has one ethernet
                0x00, //9.(SID) Service ID: just give a random number 19

                    // Command
                0x01, //10.(MRC) Main request code: 01, memory area read
                0x02, //11.(SRC) Sub-request code: 01, memory area read
                0x82, //12Memory area code     访问D区
                0x00, //13beginning address
                0x01,//14
                0x00, //15访问的起始地址
                0x00, //16number of items     访问个数（以字为单位访问）
                0x01,//17
            };

            #region IP设置
            char[] separator = new char[] { '.' };
            string[] strxx4 = PLC_IP_adress.Split(separator);
            int intxx4 = 0;
            int.TryParse(strxx4[3], out intxx4);

            //int转byte
            byte[] xx4 = System.BitConverter.GetBytes(intxx4);
            byte[] xx7 = System.BitConverter.GetBytes(ComIPLastPlace);
            send_data[4] = xx4[0];
            send_data[7] = xx7[0];
            #endregion

            #region 寄存器设置
            byte[] jcq = System.BitConverter.GetBytes(memory);
            send_data[12] = jcq[0];
            #endregion

            #region 起始位置设置
            send_data[13] = (byte)(StartP >> 8);
            send_data[14] = (byte)(StartP);
            #endregion

            #region 读取字的个数设置
            send_data[16] = (byte)(Readquantity >> 8);
            send_data[17] = (byte)(Readquantity);
            #endregion

            #region 数据写入
            string[] wwwr;
            if (Readquantity == 2)
            {
                wwwr = new string[1] { str };

            }
            else
            {
                wwwr = str.Split(',');

            }
            int[] intwwwr = new int[wwwr.Length];

            int lllength = wwwr.Length;


            if (lllength * 2 == Readquantity1)
            {
                //字符串转int
                for (int i = 0; i < wwwr.Length; i++)
                {
                    int fdsa;
                    int.TryParse(wwwr[i], out fdsa);
                    intwwwr[i] = fdsa;
                }

                //  18  19
                int jj = 18;//int 转byte
                for (int i = 0; i < lllength; i++)
                {

                    int fdasfdas = intwwwr[i];
                    string dsa2 = System.Convert.ToString(fdasfdas, 2).PadLeft(32, '0');

                    //高低位获取   从左到右(高到低)
                    String z0;//获取高16位
                    String z1;//获取低16位

                    z0 = dsa2.Substring(0, 16);
                    z1 = dsa2.Substring(16, 16);

                    //2进制转16进制 结果为string
                    string g16 =
                     string.Format("{0:X}", System.Convert.ToInt32(z0, 2)).PadLeft(4, '0');

                    string d16 =
                    string.Format("{0:X}", System.Convert.ToInt32(z1, 2)).PadLeft(4, '0');

                    //16进制转10进制  结果为int
                    int g16_10 = Convert.ToInt32(g16, 16);
                    int d16_10 = Convert.ToInt32(d16, 16);

                    byte g0 = (byte)(g16_10 >> 8);
                    byte g1 = (byte)(g16_10);

                    byte g2 = (byte)(d16_10 >> 8);
                    byte g3 = (byte)(d16_10);

                    send_data11[jj] = g2;
                    send_data11[jj + 1] = g3;
                    send_data11[jj + 2] = g0;
                    send_data11[jj + 3] = g1;
                    //send_data11[jj] = g0;
                    //send_data11[jj + 1] = g1;
                    //send_data11[jj + 2] = g2;
                    //send_data11[jj + 3] = g3;
                    jj = jj + 4;
                }
                for (int i = 0; i < 18; i++)
                {
                    send_data11[i] = send_data[i];
                }
            }
            else
            {
                m_strRunMsg = "写入数据和数量不符合";
                //MessageBox.Show("写入数据和数量不符合");
                return;
            }
            #endregion
            try
            {
                newclient.Send(send_data11, send_data11.Length, remote_ip);
                newclient.Client.ReceiveTimeout = 3000;
                //byte[] dm_data = newclient.Receive(ref remote_ip);
                //if (dm_data[12] != 0 || dm_data[13] != 0)
                //{
                //    MessageBox.Show("if出错");
                //    // textBox1.Text = "出错";
                //    //  return true;
                //}
                //else
                //{
                //MessageBox.Show("写入完成");
                //}
            }
            catch (Exception ex)
            {
                //MessageBox.Show("catch");
                // textBox1.Text = "出错";
                m_strRunMsg = ex.ToString();
                return;
            }

        }
        #endregion

        #region 基恩士发送数据
        public void SendData_Keyence(int Camera, HTuple SendData)
        {
            if (Camera > Para.listRegister.Count)
            {
                m_strRunMsg = "工位设置不匹配！";
                //MessageBox.Show("工位设置不匹配！");
                return;
            }
            if (Para.listNumbers[Camera - 1] > SendData.Length)
            {
                m_strRunMsg = "发送数据长度大于输出结果！";
                //MessageBox.Show("发送数据长度大于输出结果！");
                return;
            }

            //eg.  message = "WRS DM4000.L 4 " + SendData[0] + " " + SendData[1] + " " + SendData[2] + " " + SendData[3] + "\r";
            string message;
            message = "WRS " + Para.listRegister[Camera - 1] + Para.listBegin[Camera - 1] + ".L " + Para.listNumbers[Camera - 1];
            for (int i = 0; i < Para.listNumbers[Camera - 1]; i++)
            {
                message = message + " " + SendData[i];
            }
            message = message + "\r";
            sWriter.Write(message);
            sWriter.Flush();
        }
        #endregion

        #region 三菱发送数据
        public void SendData_Mitsubishi(int Camera, HTuple SendData)
        {
            if (Camera > Para.listRegister.Count)
            {
                m_strRunMsg = "工位设置不匹配！";
                //  MessageBox.Show("工位设置不匹配！");
                return;
            }
            if (Para.listNumbers[Camera - 1] > SendData.Length)
            {
                m_strRunMsg = "发送数据长度大于输出结果！";
                // MessageBox.Show("发送数据长度大于输出结果！");
                return;
            }

            //eg.  string S = "500000FF03FF000048001014010000D*000000000C" + s + s1 + s2 + s3 + s4 + s5; 
            string message;
            int data3 = (Para.listNumbers[Camera - 1]) * 2;
            string Data1 = (24 + 4 * data3).ToString("X4");
            string Data2 = (Para.listBegin[Camera - 1]).ToString("D6");
            string Data3 = ((Para.listNumbers[Camera - 1]) * 2).ToString("X4");
            message = "500000FF03FF00" + Data1 + "001014010000" + Para.listRegister[Camera - 1] + "*" + Data2 + Data3;
            for (int i = 0; i < Para.listNumbers[Camera - 1]; i++)
            {
                string reslutValue;
                reslutValue = ""+ SendData[i];
                int ii = int.Parse(reslutValue);

                string data = RepairZero(Convert.ToString(ii, 16).ToUpper(), 8);
                message = message + data;
            }
            message = message.Replace(" ", "");
            sWriter.Write(message);
            sWriter.Flush();
        }


        public string RepairZero(string text, int limitedLength)
        {
            //补足0的字符串
            string temp = "";
            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }
            //连接text
            temp += text;
            //高低位对调
            if (temp.Length == 8)
            {
                string str1 = temp.Substring(0, 4);
                string str2 = temp.Substring(4, 4);
                temp = " " + str2 + " " + str1;
            }
            else
            {
                // MessageBox.Show("PLC参数发送异常");
            }
            //返回补足0的字符串
            return temp;
        }
        #endregion

        /// <summary>
        /// PLC连接方法
        /// </summary>
        public string Connect()
        {

            try
            {
                if (Para.Brand == "欧姆龙")
                {
                    // 远程节点对象
                    IPEndPoint remote_ip = new IPEndPoint(IPAddress.Parse(Para.Ip), Para.Port);
                    UdpClient newclient = new UdpClient();
                }
                else
                {
                    client = new TcpClient(Para.Ip, Para.Port);
                    stream = client.GetStream();

                    // 得到了一个网络流  从这个网络流可以取得客户端发送过来的数据
                    readstream = client.GetStream();
                    sWriter = new StreamWriter(stream, Encoding.ASCII);
                }
                m_bConnect = true;
                return "PLC连接：>>[已连接]";
            }
            catch (Exception ex)
            {
                m_bConnect = false;
                m_strRunMsg = ex.ToString();
                return "PLC连接：>>[异常未连接!]";
            }
        }

        /// <summary>
        /// PLC发送数据方法
        /// </summary>
        public string SendData(int Camera, HTuple SendData)
        {
            try
            {
                if (Para.Brand == "欧姆龙")
                {
                    SendData_OMRON(Camera, SendData);
                }
                else if (Para.Brand == "基恩士")
                {
                    SendData_Keyence(Camera, SendData);
                }
                else if (Para.Brand == "三菱")
                {
                    SendData_Mitsubishi(Camera, SendData);
                }

                return "发送成功！";
            }
            catch (Exception ex)
            {
                m_strRunMsg = ex.ToString();
                return "发送异常！";
            }
        }

        /// <summary>
        /// PLC接收数据方法
        /// </summary>
        public string ReceiveData(string getData)
        {
            while (true)
            {
                if (!(Para.Brand == "欧姆龙"))
                {
                    try
                    {
                        byte[] data = new byte[255];//创建一个数据的容器，用来承接数据
                        int length = stream.Read(data, 0, data.Length);//读取数据
                        m_strReceiveMsg = Encoding.UTF8.GetString(data, 0, length);
                    }
                    catch
                    {

                    }
                }
            }
            ////基恩士读取报文(暂存)
            //messg = "RD DM306.L" + "\r";
            //sWriter.Write(messg);
            //sWriter.Flush();
            //Receive_Data();
        }

        private string m_strReceiveMsg = string.Empty;

        /// <summary>
        /// 接收消息
        /// </summary>
        public string ReceiveMsg
        {
            get
            {
                return m_strReceiveMsg;
            }
            set
            {
                m_strReceiveMsg = value;
            }
        }

        /// <summary>
        /// PLC接收数据方法
        /// </summary>
        public void ReceiveData()
        {
            while (true)
            {
                if (!(Para.Brand == "欧姆龙"))
                {
                    try
                    {
                        byte[] data = new byte[255];//创建一个数据的容器，用来承接数据
                        int length = stream.Read(data, 0, data.Length);//读取数据
                        m_strReceiveMsg = Encoding.UTF8.GetString(data, 0, length);
                    }
                    catch 
                    { 

                    }
                }
            }
            ////基恩士读取报文(暂存)
            //messg = "RD DM306.L" + "\r";
            //sWriter.Write(messg);
            //sWriter.Flush();
            //Receive_Data();
        }
    }
}
