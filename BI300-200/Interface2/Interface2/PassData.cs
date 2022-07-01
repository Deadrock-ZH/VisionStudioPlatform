using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;


namespace Interface2
{


    public static class PassData
    {

        public static GVS.HalconDisp.Control.HWindow_Final m1_disp;
        public static GVS.HalconDisp.Control.HWindow_Final m2_disp;
        public static GVS.HalconDisp.Control.HWindow_Final m3_disp;
        public static GVS.HalconDisp.Control.HWindow_Final m4_disp;
        public static GVS.HalconDisp.Control.HWindow_Final m5_disp;

        public delegate void delegateTest();

        //专传递相机使用个数值
        public static int NumDisp = 4;

        //相机设置去确定
        public static string cameraSetAbout = "";

        //传值
        public static string strContent =String.Empty;
        public static string strContent2 =String.Empty;
        public static string strContent3 =String.Empty;
        public static string strContent4 =String.Empty;

        //仅用单个地方
        public static string OnlyUse1_HardTrigger = String.Empty;
        public static string OnlyUse1_FreeShow = String.Empty;
        public static bool OnlyUse1_TestCameraHardTrigger = false;
        //
        // public static string listParaPath;

        public static  List<string> m_AllparaName;
 


        //相机设置按钮
        public static delegateTest delegate_CameraSet;
        private static int _cameraSet;
        public static int cameraSet
        {
            get
            {
                return _cameraSet;
            }
            set
            {
                _cameraSet = value;
                if (delegate_CameraSet != null)
                {
                    delegate_CameraSet();
                }
            }

        }


        public static string plc_about;
        //plc界面动作处理
        public static delegateTest delegate_PLC_Form;
        private static int _plcDone;
        public static int plcDone
        {
            get
            {
                return _plcDone;
            }
            set
            {
                _plcDone = value;
                if (delegate_PLC_Form != null)
                {
                    delegate_PLC_Form();
                }
            }

        }

        public static string CameraPT_about;
        //相机设置界面动作处理
        public static delegateTest delegate_CameraPT_Form;
        private static int _CameraPT;
        public static int CameraPT
        {
            get
            {
                return _CameraPT;
            }
            set
            {
                _CameraPT = value;
                if (delegate_CameraPT_Form != null)
                {
                    delegate_CameraPT_Form();
                }
            }

        }


        //相机状态
        //相机是否打开(注意打开和连接不是同一概念，打开的前提是连接)
        public static bool Is_Open_ccd1 = false;
        public static bool Is_Open_ccd2 = false;
        public static bool Is_Open_ccd3_1 = false;
        public static bool Is_Open_ccd3_2 = false;
        public static bool Is_Open_ccd4   = false;


        //是否为抓图模式
        public static bool Is_Grabing_ccd1 = false;
        public static bool Is_Grabing_ccd2 = false;
        public static bool Is_Grabing_ccd3_1 = false;
        public static bool Is_Grabing_ccd3_2 = false;
        public static bool Is_Grabing_ccd4 = false;

        //是否设置为软触发模式
        public static bool Is_SetSoftTrigger_ccd1 = false;
        public static bool Is_SetSoftTrigger_ccd2 = false;
        public static bool Is_SetSoftTrigger_ccd3_1 = false;
        public static bool Is_SetSoftTrigger_ccd3_2 = false;
        public static bool Is_SetSoftTrigger_ccd4 = false;

        //是否设置为硬触发模式
        public static bool Is_SetExternal_ccd1 = false;
        public static bool Is_SetExternal_ccd2 = false;
        public static bool Is_SetExternal_ccd3_1 = false;
        public static bool Is_SetExternal_ccd3_2 = false;
        public static bool Is_SetExternal_ccd4 = false;








    }




}
