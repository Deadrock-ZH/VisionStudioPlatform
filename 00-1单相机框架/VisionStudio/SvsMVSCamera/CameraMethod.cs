using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HalconDotNet;
using MvCamCtrl.NET;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace SvsMVSCamera
{
    public class CameraAPI
    {
        // 回调函数问题
        private HKCallBackDev hkCallBackDev = new HKCallBackDev();

        // 参数声明
        MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();

        // 相机
        private MyCamera m_pMyCamera = new MyCamera();

      /// <summary>
      /// 是否采集
      /// </summary>
       public bool m_bGrabbing = false;

        // 相机ID
        private String strUserID = null;

       /// <summary>
       /// 是否链接
       /// </summary>
        public bool ifccdConnected = false;

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool m_bOpenCamera = false;

        // 测试自动重连
        private MyCamera.MV_CC_DEVICE_INFO device_temp;

        // 包大小
        private uint g_nPayloadSize = 0;

        // 图像宽                               
        private uint nWidth = 0;

        // 图像高                                
        private uint nHeight = 0;

        private Boolean m_bDisConnect = false;
        private UInt32 m_nBufSizeForSaveImage;
        private byte[] m_pBufForSaveImage;

        // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        private UInt32 m_nBufSizeForDriver;
        private byte[] m_pBufForDriver;

        private Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// 图像处理自定义委托
        /// </summary>
        /// <param name="hImage"></param>
        public delegate void delegateProcessHImage(HObject hImage , int CcdId);

        /// <summary>
        /// 图像处理自定义事件
        /// </summary>
        public event delegateProcessHImage eventProcessHImage;

        private string m_strName = "";

        /// <summary>
        /// 根据相机UserID实例化相机
        /// </summary>
        /// <param name="UserID"></param>
        public CameraAPI(string UserID)
        {           
            // this.UserID = UserID;
            hkCallBackDev.ImageCallback = new MyCamera.cbOutputExdelegate(ImageCallbackFunc);
            hkCallBackDev.reconnectCallback = new MyCamera.cbExceptiondelegate(cbExceptiondelegate);
            try
            {
                int nRet;
                strUserID = UserID;

                // 可以掉线重连使用                    
                bool ifConnected = false;

                nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
                if (MyCamera.MV_OK != nRet)
                {
                    ifConnected = false;
                    MessageBox.Show("Enum Device Fail");
                    return;
                }
                for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
                {
                    MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    // 这里不判断，直接默认使用GIGE接口
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    // 测试一下通过UserID打开设备
                    if (gigeInfo.chUserDefinedName == UserID)
                    {
                        // 创建相机
                        nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
                        if (MyCamera.MV_OK != nRet)
                        {
                            ifccdConnected = true;
                            return;
                        }
                        // 保存目前相机信息
                        device_temp = device;
                        ifccdConnected = true;
                        ifConnected = true;
                    }
                }
                if (!ifConnected)
                {
                    ifConnected = false;
                    ifccdConnected = false;
                    MessageBox.Show("未查询到名称为 " + UserID + " 的相机");
                }
            }
            catch (Exception)
            {
                ifccdConnected = false;
                MessageBox.Show("相机实例化异常");
            }
        }

        /// <summary>
        /// 根据相机UserID实例化相机
        /// </summary>
        /// <param name="strName"></param>
        public CameraAPI(out string strName)
        {
            // this.UserID = UserID;
            hkCallBackDev.ImageCallback = new MyCamera.cbOutputExdelegate(ImageCallbackFunc);
            hkCallBackDev.reconnectCallback = new MyCamera.cbExceptiondelegate(cbExceptiondelegate);
            try
            {
                strName = "";
                int nRet;

                // 可以掉线重连使用                
                bool ifConnected = false;

                nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
                if (MyCamera.MV_OK != nRet)
                {
                    strName = "";
                    ifConnected = false;
                    MessageBox.Show("Enum Device Fail");
                    return;
                }
                for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
                {
                    MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                    {
                        MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        if (usbInfo.chUserDefinedName != "")
                        {
                            strName = ("USBU3V: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                        }
                        else
                        {
                            strName = ("USBU3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                        }
                    }
                    else
                    {
                        // 这里不判断，直接默认使用GIGE接口
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                        MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        if (gigeInfo.chUserDefinedName != "")
                        {
                            strName = ("GIEU3V: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                        }
                        else
                        {
                            strName = ("GIEU3V: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                        }
                    }
                  

                    // 创建相机
                    nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
                    if (MyCamera.MV_OK != nRet)
                    {
                        ifccdConnected = true;
                        strName = "";
                        m_strName = strName;
                        return;
                    }
                    // 保存目前相机信息
                    device_temp = device;
                    ifccdConnected = true;
                    ifConnected = true;
                    if (!ifConnected)
                    {
                        ifConnected = false;
                        ifccdConnected = false;
                    }
                }
            }
            catch (Exception)
            {
                strName = "";
                m_strName = strName;
                ifccdConnected = false;
                MessageBox.Show("相机实例化异常");
            }
            m_strName = strName;
        }
        /// <summary>
        /// 打开相机
        /// </summary>
        public void OpenCam()
        {
            try
            {
                int nRet = -1;

                // 打开相机
                nRet = m_pMyCamera.MV_CC_OpenDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    m_bOpenCamera = false;
                    MessageBox.Show("Open Device Fail");
                    return;
                }

                if (m_strName.Contains("GIE"))
                { 
                    // 探测网络最佳包大小----GIGE接口有，USB报错
                    int nPacketSize = m_pMyCamera.MV_CC_GetOptimalPacketSize_NET();
                    if (nPacketSize > 0)
                    {
                        nRet = m_pMyCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                        if (nRet != MyCamera.MV_OK)
                        {
                            MessageBox.Show("Warning: Set Packet Size failed");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Warning: Set Packet Size failed");
                    }
                }

                // 获取包大小
                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                nRet = m_pMyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                if (MyCamera.MV_OK != nRet)
                {
                    MessageBox.Show("Get PayloadSize Fail");
                    return;
                }
                g_nPayloadSize = stParam.nCurValue;

                // 获取高
                nRet = m_pMyCamera.MV_CC_GetIntValue_NET("Height", ref stParam);
                if (MyCamera.MV_OK != nRet)
                {
                    MessageBox.Show("Get Height Fail");
                    return;
                }
                nHeight = stParam.nCurValue;

                // 获取宽
                nRet = m_pMyCamera.MV_CC_GetIntValue_NET("Width", ref stParam);
                if (MyCamera.MV_OK != nRet)
                {
                    MessageBox.Show("Get Width Fail");
                    return;
                }
                nWidth = stParam.nCurValue;

                // 注册回调函数
                //  ImageCallback = new MyCamera.cbOutputExdelegate(ImageCallbackFunc);
                nRet = m_pMyCamera.MV_CC_RegisterImageCallBackEx_NET(hkCallBackDev.ImageCallback, IntPtr.Zero);
                nRet = m_pMyCamera.MV_CC_RegisterExceptionCallBack_NET(hkCallBackDev.reconnectCallback, IntPtr.Zero);
                if (MyCamera.MV_OK != nRet)
                {
                    ShowErrorMsg("Register expection callback failed", nRet);
                }
                GC.KeepAlive(hkCallBackDev.reconnectCallback);

                // 根据图片尺寸申请缓冲器
                m_nBufSizeForSaveImage = nWidth * nHeight * 3 * 3 + 2048;
                m_pBufForSaveImage = new byte[nWidth * nHeight * 3 * 3 + 2048];
                // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
                m_nBufSizeForDriver = nWidth * nHeight * 3;
                m_pBufForDriver = new byte[nWidth * nHeight * 3];
                m_bOpenCamera = true;
            }
            catch (Exception)
            {
                m_bOpenCamera = false;
                throw;
            }

        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        public void CloseCam()
        {
            try
            {
                int nRet = -1;
                if (m_bGrabbing)
                {
                    StopGrab();
                }
                nRet = m_pMyCamera.MV_CC_CloseDevice_NET();
                m_bOpenCamera = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("关闭相机异常 \n" + ex.ToString());
            }
        }

        /// <summary>
        ///  销毁相机
        /// </summary>
        public void DestoryCam()
        {
            try
            {
                int nRet = -1;
                nRet = m_pMyCamera.MV_CC_DestroyDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("关闭相机异常 \n" + ex.ToString());
            }
        }

        /// <summary>
        /// 开始采集
        /// </summary>
        public void StartGrab()
        {
            int nRet = -1;

            if (MyCamera.MV_OK != nRet)
            {
                Console.WriteLine("Register image callback failed!");
            }
            nRet = m_pMyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Start Grabbing Fail");
                return;
            }
            m_bGrabbing = true;
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        public void StopGrab()
        {
            int nRet = -1;

            nRet = m_pMyCamera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show("Stop Grabbing Fail! " + nRet.ToString());
            }
            if (m_bGrabbing)
            {
                m_bGrabbing = false;
            }
        }
        /**************************************************************/


        /***********************  相机参数获取 ************************/
        /// <summary>
        /// 或取心跳时间
        /// </summary>
        public void GetHeartBeatTime(out uint value)
        {
            int nRet = -1;
            MyCamera.MVCC_INTVALUE stIntValue = new MyCamera.MVCC_INTVALUE();
            nRet = m_pMyCamera.MV_CC_GetHeartBeatTimeout_NET(ref stIntValue);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("心跳时间或取失败");
                value = 1;
                return;
            }
            value = stIntValue.nCurValue;
        }

        /// <summary>
        /// 获取曝光时间
        /// </summary>
        /// <param name="fExposure"></param>
        public void GetExposureTime(out float fExposure)
        {
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = m_pMyCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("曝光时间获取失败");
                fExposure = 1;
                return;
            }
            fExposure = stParam.fCurValue;
            return;
        }

        /// <summary>
        /// 获取增益
        /// </summary>
        /// <param name="fGain"></param>
        public void GetGain(out float fGain)
        {
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = m_pMyCamera.MV_CC_GetFloatValue_NET("Gain", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("增益获取失败");
                fGain = 1;
                return;
            }
            fGain = stParam.fCurValue;
            return;
        }

        /**************************************************************/


        /***********************  相机参数设置 ************************/
        /// <summary>
        /// 设置相机心跳时间
        /// </summary>
        /// <param name="value"></param>
        public void SetHeartBeatTime(uint value)
        {
            int nRet = -1;
            nRet = m_pMyCamera.MV_CC_SetHeartBeatTimeout_NET(value);
            //  nRet = m_pMyCamera.MV_CC_SetEnumValue_NET("GevHeartbeatTimeout", value);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("心跳时间设置失败");
            }
        }

        /// <summary>
        /// 设置相机曝光时间
        /// </summary>
        /// <param name="value"></param>
        public void setExposureTime(long value)
        {
            int nRet;
            m_pMyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);

            nRet = m_pMyCamera.MV_CC_SetFloatValue_NET("ExposureTime", value);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show("Set Exposure Time Fail!");
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="value"></param>
        public void setGain(long value)
        {
            int nRet;
            m_pMyCamera.MV_CC_SetEnumValue_NET("GainAuto", 0);
            nRet = m_pMyCamera.MV_CC_SetFloatValue_NET("Gain", value);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show("Set Gain Fail!");
            }
        }

        /// <summary>
        /// 设置相机Freerun模式
        /// </summary>
        public void SetFreerun()
        {
            try
            {
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 0);
                // m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", 0);
            }
            catch (Exception)
            {
                MessageBox.Show("TriggerMode Set Fail!");
            }

        }

        /// <summary>
        /// 设置软触发模式
        /// </summary>
        public void SetSoftwareTrigger()
        {
            try
            {
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", 7);
            }
            catch (Exception)
            {
                MessageBox.Show("TriggerMode Set Fail!");
            }

        }

        /// <summary>
        /// 发送软触发信号
        /// </summary>
        public void SendSoftwareExecute()
        {
            int nRet;

            nRet = m_pMyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Trigger Fail! " + nRet.ToString());
            }
        }

        /// <summary>
        /// 设置相机外触发模式
        /// </summary>
        public void SetExternTrigger()
        {
            try
            {
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", 0);
            }
            catch (Exception)
            {
                MessageBox.Show("TriggerMode Set Fail!");
            }
        }
        /**************************************************************/


        /**********************  相机回调函数 ************************/
        /// <summary>
        /// 取相回调函数
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="pFrameInfo"></param>
        /// <param name="pUser"></param>
        void ImageCallbackFunc(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            Bitmap bmp = SaveBitMap(pFrameInfo, pData);

            HObject showImage = new HObject();
            Bitmap2HObjectBmp8(bmp, out showImage);
            string stringCcdId = strUserID.Remove(0,3);
            int CcdId = int.Parse(stringCcdId);
            eventProcessHImage(showImage,CcdId);
            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }
        }

        /// <summary>
        /// 异常回调函数
        /// </summary>
        /// <param name="nMsgType"></param>
        /// <param name="pUser"></param>
        private void cbExceptiondelegate(uint nMsgType, IntPtr pUser)
        {
            // 后期去除
            MessageBox.Show("相机: " + strUserID + " 连接断开，重新连接");
            if (nMsgType == MyCamera.MV_EXCEPTION_DEV_DISCONNECT)
            {
                m_bDisConnect = false;

                m_bGrabbing = false;
                int nRet = -1;

                DeInitCamera();

                if (m_pDeviceList.nDeviceNum == 0)
                {
                    ShowErrorMsg("No device, please Select", 0);
                    return;
                }
                // ch:打开设备 | en:Open Device
                while (!m_bDisConnect)
                {
                    // device_temp 是否能正常使用
                    nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device_temp);
                    if (MyCamera.MV_OK != nRet)
                    {
                        ShowErrorMsg("Create Camera failed", nRet);
                        m_pMyCamera.MV_CC_DestroyDevice_NET();
                        continue;
                    }
                    nRet = m_pMyCamera.MV_CC_OpenDevice_NET();
                    if (MyCamera.MV_OK != nRet)
                    {
                        m_pMyCamera.MV_CC_DestroyDevice_NET();
                        continue;
                    }
                    else
                    {
                        nRet = InitCamera();
                        if (MyCamera.MV_OK != nRet)
                        {
                            m_pMyCamera.MV_CC_DestroyDevice_NET();
                            continue;
                        }
                        m_bDisConnect = true;
                        MessageBox.Show("相机重连完成");
                    }
                }

            }
        }
        ///**************************************************************/


        /// <summary>
        /// 初始化相机
        /// </summary>
        /// <returns></returns>
        private int InitCamera()
        {
            int nRet = -1;
            nRet = m_pMyCamera.MV_CC_RegisterExceptionCallBack_NET(hkCallBackDev.reconnectCallback, IntPtr.Zero);
            GC.KeepAlive(hkCallBackDev.reconnectCallback);
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }
            nRet = m_pMyCamera.MV_CC_RegisterImageCallBackEx_NET(hkCallBackDev.ImageCallback, IntPtr.Zero);
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }
            // ch:控件操作 | en:Control operation
            //SetCtrlWhenOpen();

            // ch:开始采集 | en:Start Grabbing       -------       Freerun模式下自动开始采集
            // nRet = m_pMyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }

            // ch:控件操作 | en:Control Operation
            //SetCtrlWhenStartGrab();

            // ch:标志位置位true | en:Set flag bit true
            m_bGrabbing = true;

            // ch:显示 | en:Display
            // nRet = m_pMyCamera.MV_CC_Display_NET(pictureBox1.Handle);
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }

            return MyCamera.MV_OK;
        }
        /// <summary>
        /// 撤销初始化
        /// </summary>
        private void DeInitCamera()
        {
            // ch:取流标志位清零 | en:Zero setting grabbing flag bit
            m_bGrabbing = false;

            Thread.Sleep(1000);

            // ch:停止采集 | en:Stop Grabbing
            m_pMyCamera.MV_CC_StopGrabbing_NET();

            //// ch:控件操作 | en:Control Operation
            //SetCtrlWhenStopGrab();

            // ch:关闭设备 | en:Close Device
            int nRet = m_pMyCamera.MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }

            nRet = m_pMyCamera.MV_CC_DestroyDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }
            // ch:控件操作 | en:Control Operation
            //SetCtrlWhenClose();
        }
        public void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MyCamera.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case MyCamera.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                case MyCamera.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                case MyCamera.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case MyCamera.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case MyCamera.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case MyCamera.MV_E_NODATA: errorMsg += " No data "; break;
                case MyCamera.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case MyCamera.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                case MyCamera.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case MyCamera.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                case MyCamera.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                case MyCamera.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case MyCamera.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case MyCamera.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case MyCamera.MV_E_NETER: errorMsg += " Network error "; break;
            }

            MessageBox.Show(errorMsg, "PROMPT");
        }
        public Int32 ConvertToMono8(object obj, IntPtr pInData, IntPtr pOutData, ushort nHeight, ushort nWidth, MyCamera.MvGvspPixelType nPixelType)
        {
            if (IntPtr.Zero == pInData || IntPtr.Zero == pOutData)
            {
                return MyCamera.MV_E_PARAMETER;
            }

            int nRet = MyCamera.MV_OK;
            MyCamera device = obj as MyCamera;
            MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pInData;//源数据
            if (IntPtr.Zero == stPixelConvertParam.pSrcData)
            {
                return -1;
            }

            stPixelConvertParam.nWidth = nWidth;//图像宽度
            stPixelConvertParam.nHeight = nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = nPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = (uint)(nWidth * nHeight * ((((uint)nPixelType) >> 16) & 0x00ff) >> 3);

            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * ((((uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed) >> 16) & 0x00ff) >> 3);
            stPixelConvertParam.pDstBuffer = pOutData;//转换后的数据
            stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * 3);

            nRet = device.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            return nRet;
        }
        public Bitmap SaveBitMap(MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pData)
        {
            Bitmap bmp = null;
            int nRet;
            UInt32 nPayloadSize = 0;
            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            nRet = m_pMyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                return null;
            }
            nPayloadSize = stParam.nCurValue;
            if (nPayloadSize > m_nBufSizeForDriver)
            {
                m_nBufSizeForDriver = nPayloadSize;
                m_pBufForDriver = new byte[m_nBufSizeForDriver];

                // ch:同时对保存图像的缓存做大小判断处理 | en:Determine the buffer size to save image
                // ch:BMP图片大小：width * height * 3 + 2048(预留BMP头大小) | en:BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
                m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
                m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
            }

            //IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);
            //MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
            //// ch:超时获取一帧，超时时间为1秒 | en:Get one frame timeout, timeout is 1 sec
            //nRet = cam.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForDriver, ref stFrameInfo, 1000);
            //if (MyCamera.MV_OK != nRet)
            //{
            //    ShowErrorMsg("No Data!", nRet);
            //    return;
            //}

            MyCamera.MvGvspPixelType enDstPixelType;
            if (IsMonoData(pFrameInfo.enPixelType))
            {
                enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
            }
            else if (IsColorData(pFrameInfo.enPixelType))
            {
                enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            }
            else
            {
                return null;
            }

            IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
            MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
            MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();
            stConverPixelParam.nWidth = pFrameInfo.nWidth;
            stConverPixelParam.nHeight = pFrameInfo.nHeight;
            stConverPixelParam.pSrcData = pData;
            stConverPixelParam.nSrcDataLen = pFrameInfo.nFrameLen;
            stConverPixelParam.enSrcPixelType = pFrameInfo.enPixelType;
            stConverPixelParam.enDstPixelType = enDstPixelType;
            stConverPixelParam.pDstBuffer = pImage;
            stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
            nRet = m_pMyCamera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
            //if (MyCamera.MV_OK != nRet)
            //{
            //    return null;
            //}

            if (enDstPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
            {
                //************************Mono8 转 Bitmap*******************************
                bmp = new Bitmap(pFrameInfo.nWidth, pFrameInfo.nHeight, pFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pImage);

                ColorPalette cp = bmp.Palette;
                // init palette
                for (int i = 0; i < 256; i++)
                {
                    cp.Entries[i] = Color.FromArgb(i, i, i);
                }
                // set palette back
                bmp.Palette = cp;

                //bmp.Save("image.bmp", ImageFormat.Bmp);
            }
            else
            {
                //*********************RGB8 转 Bitmap**************************
                for (int i = 0; i < pFrameInfo.nHeight; i++)
                {
                    for (int j = 0; j < pFrameInfo.nWidth; j++)
                    {
                        byte chRed = m_pBufForSaveImage[i * pFrameInfo.nWidth * 3 + j * 3];
                        m_pBufForSaveImage[i * pFrameInfo.nWidth * 3 + j * 3] = m_pBufForSaveImage[i * pFrameInfo.nWidth * 3 + j * 3 + 2];
                        m_pBufForSaveImage[i * pFrameInfo.nWidth * 3 + j * 3 + 2] = chRed;
                    }
                }
                try
                {
                    bmp = new Bitmap(pFrameInfo.nWidth, pFrameInfo.nHeight, pFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, pImage);
                    //bmp.Save("image.bmp", ImageFormat.Bmp);
                }
                catch
                {
                }

            }
            return bmp;
        }
        public void Bitmap2HObjectBmp8(Bitmap bmp, out HObject image)
        {
            try
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);

                BitmapData srcBmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                HOperatorSet.GenImage1(out image, "byte", bmp.Width, bmp.Height, srcBmpData.Scan0);
                bmp.UnlockBits(srcBmpData);
            }
            catch (Exception ex)
            {
                image = null;
            }
        }
        private Boolean IsMonoData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;

                default:
                    return false;
            }
        }
        private Boolean IsColorData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YCBCR411_8_CBYYCRYY:
                    return true;

                default:
                    return false;
            }
        }

        public class HKCallBackDev
        {
            public MyCamera.cbOutputExdelegate ImageCallback;
            public MyCamera.cbExceptiondelegate reconnectCallback;
        }
    }
}
