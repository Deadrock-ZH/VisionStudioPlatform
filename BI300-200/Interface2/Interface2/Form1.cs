using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using GVS;
using System.Threading;
using Station;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Interface2
{

    public partial class Form1 : Form
    {

        static public bool IsRegister = true;//登录标识判断1234

        MainPara Para_MainPara = new MainPara();

        StationMethod m1_Method = new StationMethod();
        StationMethod m2_Method = new StationMethod();
        StationMethod m3_Method = new StationMethod();
        StationMethod m4_Method = new StationMethod();
        StationMethod m5_Method = new StationMethod();

        //窗口设置 目前只支持4个或5个窗口
        int NumDisp = 5;

        public Form1()
        {
            InitializeComponent();

            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

            FoldCreate0();

            DataLoad();


            //文件夹生成
            FoldCreate();

            //界面整体布局
            InitControlShape2();

            //左侧快捷键
            tabLayoutSet(panel6, tableLayoutPanel1, 9, 1);

            //显示窗口
            m_Disp_List.Add(m1_Disp);
            m_Disp_List.Add(m2_Disp);
            m_Disp_List.Add(m3_Disp);
            if (NumDisp <= 4)
            {
                m_Disp_List.Add(m5_Disp);
                m_Disp_List.Add(m4_Disp);

            }
            else
            {

                m_Disp_List.Add(m4_Disp);
                m_Disp_List.Add(m5_Disp);
            }


            m_Disp_List.Add(m6_Disp);
            m_Disp_List.Add(m7_Disp);
            m_Disp_List.Add(m8_Disp);

            //显示窗口
            dispSet(NumDisp);

            //MenuStrip
            MenuStrip1();

            //委托注册
            DelegateDeclaration();

            //线程开启
            OpenRunThread();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            HOperatorSet.SetSystem("parallelize_operators", "true");
            HOperatorSet.SetSystem("border_shape_models", "true");

            //相机连接
            Cameraconfigure();

            //硬件连接状态
            HardWareConnection();

            UpdateLogList("软件开启", p_D_Log);

            if (IsRegister)
            {
                toolStripStatusLabel3.Text = "Adminstrator";
            }
            else
            {
                toolStripStatusLabel3.Text = "user";
            }

            sleepTime = 3000;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PassData.cameraSetAbout == "相机个数更改" || MessageBox.Show("确定关闭软件?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                SaveDataAll();
                //UpdateLogList("关闭软件", p_D_Log);

                if (ccd1 != null && ccd1.ifccdConnected)
                {
                    ccd1.CloseCam();
                }
                if (ccd2 != null && ccd2.ifccdConnected)
                {
                    ccd2.CloseCam();
                }
                if (ccd3_1 != null && ccd3_1.ifccdConnected)
                {
                    ccd3_1.CloseCam();
                }
                if (ccd3_2 != null && ccd3_2.ifccdConnected)
                {
                    ccd3_2.CloseCam();
                }
                if (ccd4 != null && ccd4.ifccdConnected)
                {
                    ccd4.CloseCam();
                }

                UpdateLogList("软件关闭", p_D_Log);

            }
            else
            {
                e.Cancel = true;
            }


        }

        void test()
        {




            return;

            //SaveShowImage(m1_Disp, p_gw1_SaveWDNGImage, "jpg");
            Random rd33 = new Random();
            Random rd44 = new Random();

            int q33 = rd33.Next(1, 3);

            m3_Method.resultPara = new StationMethod.ResultPara();
            m3_Method.resultPara.ClassSerial = q33.ToString();
            double x = rd44.Next(1, 100) * 1.0 / 1000;
            Random rd55 = new Random();
            double y = rd55.Next(50, 100) * 1.0 / 1000;

            m3_Method.resultPara.DeltaX = x;
            m3_Method.resultPara.DeltaY = y;


            datagridViewRun_DataCal(m3_Method.resultPara, 3);


            //  datagridViewPlay(index_gw3, Para_MainPara.gw1_LVDataDisplay);


            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Random rd = new Random();
                    int q1 = rd.Next(1, 34);
                    m1_Method.resultPara = new StationMethod.ResultPara();
                    m1_Method.resultPara.ClassSerial = q1.ToString();
                    datagridViewRun_DataCal(m1_Method.resultPara, 1);


                    Random rd2 = new Random();
                    int q2 = rd.Next(1, 54);
                    m2_Method.resultPara = new StationMethod.ResultPara();
                    m2_Method.resultPara.ClassSerial = q2.ToString();
                    datagridViewRun_DataCal(m2_Method.resultPara, 2);

                    Random rd3 = new Random();
                    int q3 = rd.Next(1, 3);
                    m3_Method.resultPara = new StationMethod.ResultPara();
                    m3_Method.resultPara.ClassSerial = q3.ToString();
                    datagridViewRun_DataCal(m3_Method.resultPara, 3);

                    if (NumDisp >= 5)
                    {
                        Random rd4 = new Random();
                        int q4 = rd.Next(1, 7);
                        m4_Method.resultPara = new StationMethod.ResultPara();
                        m4_Method.resultPara.ClassSerial = q4.ToString();
                        datagridViewRun_DataCal(m4_Method.resultPara, 4);
                    }
                    Random rd5 = new Random();
                    int q5 = rd.Next(1, 5);
                    m5_Method.resultPara = new StationMethod.ResultPara();
                    m5_Method.resultPara.ClassSerial = q5.ToString();
                    datagridViewRun_DataCal(m5_Method.resultPara, 5);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

                Thread.Sleep(10);
            }


        }

        //跨界面执行函数***************************
        void delegate_CameraSet()
        {
            try
            {
                switch (PassData.cameraSetAbout)
                {

                    //********************
                    case "实时模式显示切换":
                        FreeChangeShow();
                        break;


                    case "示教页面发送数据":
                        PT_FormSendMsgToPLC();
                        break;
                    //***********************
                    case "相机个数更改":
                        UpdateLogList("相机个数更改为:" + Para_MainPara.NumDisp.ToString(),
                            p_D_Log);

                        NumDispChange();
                        break;

                    //************************
                    case "ccd1_SoftTriggerPT"://软触发试教
                        RM1 = RunMode.Tech;
                        CameraUse2(true, ccd1, RM1, RM1, "软触发模式",
                            PassData.Is_Open_ccd1,
                            PassData.Is_Grabing_ccd1,
                            PassData.Is_SetSoftTrigger_ccd1,
                            PassData.Is_SetExternal_ccd1,
                            out PassData.Is_Open_ccd1,
                            out PassData.Is_Grabing_ccd1,
                            out PassData.Is_SetSoftTrigger_ccd1,
                            out PassData.Is_SetExternal_ccd1,
                            out RM1);
                        break;

                    case "ccd1_HardTrigger_Run"://运行硬触发
                        RM1 = RunMode.Auto;
                        CameraUse2(true, ccd1, RM1, RM1, "硬触发模式",
                            PassData.Is_Open_ccd1,
                            PassData.Is_Grabing_ccd1,
                            PassData.Is_SetSoftTrigger_ccd1,
                            PassData.Is_SetExternal_ccd1,
                            out PassData.Is_Open_ccd1,
                            out PassData.Is_Grabing_ccd1,
                            out PassData.Is_SetSoftTrigger_ccd1,
                            out PassData.Is_SetExternal_ccd1,
                            out RM1);
                        break;

                    case "ccd1_HardTrigger_PT"://示教硬触发
                        RM1 = RunMode.Tech;
                        CameraUse2(true, ccd1, RM1, RM1, "硬触发模式",
                            PassData.Is_Open_ccd1,
                            PassData.Is_Grabing_ccd1,
                            PassData.Is_SetSoftTrigger_ccd1,
                            PassData.Is_SetExternal_ccd1,
                            out PassData.Is_Open_ccd1,
                            out PassData.Is_Grabing_ccd1,
                            out PassData.Is_SetSoftTrigger_ccd1,
                            out PassData.Is_SetExternal_ccd1,
                            out RM1);
                        break;

                    case "ccd1_SoftTriggerDo":
                        RM1 = RunMode.Save;
                        CameraUse2(true, ccd1, RM1, RM1, "软触发模式",
                            PassData.Is_Open_ccd1,
                            PassData.Is_Grabing_ccd1,
                            PassData.Is_SetSoftTrigger_ccd1,
                            PassData.Is_SetExternal_ccd1,
                            out PassData.Is_Open_ccd1,
                            out PassData.Is_Grabing_ccd1,
                            out PassData.Is_SetSoftTrigger_ccd1,
                            out PassData.Is_SetExternal_ccd1,
                            out RM1);
                        break;

                    case "ccd1_Stop":

                        RM1 = RunMode.Free;
                        CameraUse2(false, ccd1, RM1, RM1, "软触发模式",
                            PassData.Is_Open_ccd1,
                            PassData.Is_Grabing_ccd1,
                            PassData.Is_SetSoftTrigger_ccd1,
                            PassData.Is_SetExternal_ccd1,
                            out PassData.Is_Open_ccd1,
                            out PassData.Is_Grabing_ccd1,
                            out PassData.Is_SetSoftTrigger_ccd1,
                            out PassData.Is_SetExternal_ccd1,
                            out RM1);

                        break;

                    case "ccd1_Free":
                        RM1 = RunMode.Free;
                        CameraUse2(true, ccd1, RM1, RM1, "实时模式",
                            PassData.Is_Open_ccd1,
                            PassData.Is_Grabing_ccd1,
                            PassData.Is_SetSoftTrigger_ccd1,
                            PassData.Is_SetExternal_ccd1,
                            out PassData.Is_Open_ccd1,
                            out PassData.Is_Grabing_ccd1,
                            out PassData.Is_SetSoftTrigger_ccd1,
                            out PassData.Is_SetExternal_ccd1,
                            out RM1);
                        break;
                    //******************************
                    case "ccd2_SoftTriggerPT"://软触发试教
                        RM2 = RunMode.Tech;
                        CameraUse2(true, ccd2, RM2, RM2, "软触发模式",
                            PassData.Is_Open_ccd2,
                            PassData.Is_Grabing_ccd2,
                            PassData.Is_SetSoftTrigger_ccd2,
                            PassData.Is_SetExternal_ccd2,
                            out PassData.Is_Open_ccd2,
                            out PassData.Is_Grabing_ccd2,
                            out PassData.Is_SetSoftTrigger_ccd2,
                            out PassData.Is_SetExternal_ccd2,
                            out RM2);
                        break;

                    case "ccd2_HardTrigger_Run"://运行硬触发
                        RM2 = RunMode.Auto;
                        CameraUse2(true, ccd2, RM2, RM2, "硬触发模式",
                            PassData.Is_Open_ccd2,
                            PassData.Is_Grabing_ccd2,
                            PassData.Is_SetSoftTrigger_ccd2,
                            PassData.Is_SetExternal_ccd2,
                            out PassData.Is_Open_ccd2,
                            out PassData.Is_Grabing_ccd2,
                            out PassData.Is_SetSoftTrigger_ccd2,
                            out PassData.Is_SetExternal_ccd2,
                            out RM2);
                        break;

                    case "ccd2_HardTrigger_PT"://示教硬触发
                        RM2 = RunMode.Tech;
                        CameraUse2(true, ccd2, RM2, RM2, "硬触发模式",
                            PassData.Is_Open_ccd2,
                            PassData.Is_Grabing_ccd2,
                            PassData.Is_SetSoftTrigger_ccd2,
                            PassData.Is_SetExternal_ccd2,
                            out PassData.Is_Open_ccd2,
                            out PassData.Is_Grabing_ccd2,
                            out PassData.Is_SetSoftTrigger_ccd2,
                            out PassData.Is_SetExternal_ccd2,
                            out RM2);
                        break;

                    case "ccd2_SoftTriggerDo":
                        RM2 = RunMode.Save;
                        CameraUse2(true, ccd2, RM2, RM2, "软触发模式",
                           PassData.Is_Open_ccd2,
                            PassData.Is_Grabing_ccd2,
                            PassData.Is_SetSoftTrigger_ccd2,
                            PassData.Is_SetExternal_ccd2,
                            out PassData.Is_Open_ccd2,
                            out PassData.Is_Grabing_ccd2,
                            out PassData.Is_SetSoftTrigger_ccd2,
                            out PassData.Is_SetExternal_ccd2,
                            out RM2);
                        break;

                    case "ccd2_Stop":

                        RM2 = RunMode.Free;
                        CameraUse2(false, ccd2, RM2, RM2, "软触发模式",
                            PassData.Is_Open_ccd2,
                            PassData.Is_Grabing_ccd2,
                            PassData.Is_SetSoftTrigger_ccd2,
                            PassData.Is_SetExternal_ccd2,
                            out PassData.Is_Open_ccd2,
                            out PassData.Is_Grabing_ccd2,
                            out PassData.Is_SetSoftTrigger_ccd2,
                            out PassData.Is_SetExternal_ccd2,
                            out RM2);

                        break;


                    case "ccd2_Free":
                        RM2 = RunMode.Free;
                        CameraUse2(true, ccd2, RM2, RM2, "实时模式",
                            PassData.Is_Open_ccd2,
                            PassData.Is_Grabing_ccd2,
                            PassData.Is_SetSoftTrigger_ccd2,
                            PassData.Is_SetExternal_ccd2,
                            out PassData.Is_Open_ccd2,
                            out PassData.Is_Grabing_ccd2,
                            out PassData.Is_SetSoftTrigger_ccd2,
                            out PassData.Is_SetExternal_ccd2,
                            out RM2);
                        break;
                    //*************************************

                    case "ccd3_1_SoftTriggerPT"://软触发试教
                        RM3_1 = RunMode.Tech;
                        CameraUse2(true, ccd3_1, RM3_1, RM3_1, "软触发模式",
                            PassData.Is_Open_ccd3_1,
                            PassData.Is_Grabing_ccd3_1,
                            PassData.Is_SetSoftTrigger_ccd3_1,
                            PassData.Is_SetExternal_ccd3_1,
                            out PassData.Is_Open_ccd3_1,
                            out PassData.Is_Grabing_ccd3_1,
                            out PassData.Is_SetSoftTrigger_ccd3_1,
                            out PassData.Is_SetExternal_ccd3_1,
                            out RM3_1);
                        break;

                    case "ccd3_1_HardTrigger_Run"://运行硬触发
                        RM3_1 = RunMode.Auto;
                        CameraUse2(true, ccd3_1, RM3_1, RM3_1, "硬触发模式",
                            PassData.Is_Open_ccd3_1,
                            PassData.Is_Grabing_ccd3_1,
                            PassData.Is_SetSoftTrigger_ccd3_1,
                            PassData.Is_SetExternal_ccd3_1,
                            out PassData.Is_Open_ccd3_1,
                            out PassData.Is_Grabing_ccd3_1,
                            out PassData.Is_SetSoftTrigger_ccd3_1,
                            out PassData.Is_SetExternal_ccd3_1,
                            out RM3_1);
                        break;

                    case "ccd3_1_HardTrigger_PT"://示教硬触发
                        RM3_1 = RunMode.Tech;
                        CameraUse2(true, ccd3_1, RM3_1, RM3_1, "硬触发模式",
                           PassData.Is_Open_ccd3_1,
                            PassData.Is_Grabing_ccd3_1,
                            PassData.Is_SetSoftTrigger_ccd3_1,
                            PassData.Is_SetExternal_ccd3_1,
                            out PassData.Is_Open_ccd3_1,
                            out PassData.Is_Grabing_ccd3_1,
                            out PassData.Is_SetSoftTrigger_ccd3_1,
                            out PassData.Is_SetExternal_ccd3_1,
                            out RM3_1);
                        break;

                    case "ccd3_1_SoftTriggerDo":
                        RM3_1 = RunMode.Save;
                        CameraUse2(true, ccd3_1, RM3_1, RM3_1, "软触发模式",
                           PassData.Is_Open_ccd3_1,
                            PassData.Is_Grabing_ccd3_1,
                            PassData.Is_SetSoftTrigger_ccd3_1,
                            PassData.Is_SetExternal_ccd3_1,
                            out PassData.Is_Open_ccd3_1,
                            out PassData.Is_Grabing_ccd3_1,
                            out PassData.Is_SetSoftTrigger_ccd3_1,
                            out PassData.Is_SetExternal_ccd3_1,
                            out RM3_1);
                        break;

                    case "ccd3_1_Stop":

                        RM3_1 = RunMode.Free;
                        CameraUse2(false, ccd3_1, RM3_1, RM3_1, "软触发模式",
                            PassData.Is_Open_ccd3_1,
                            PassData.Is_Grabing_ccd3_1,
                            PassData.Is_SetSoftTrigger_ccd3_1,
                            PassData.Is_SetExternal_ccd3_1,
                            out PassData.Is_Open_ccd3_1,
                            out PassData.Is_Grabing_ccd3_1,
                            out PassData.Is_SetSoftTrigger_ccd3_1,
                            out PassData.Is_SetExternal_ccd3_1,
                            out RM3_1);

                        break;


                    case "ccd3_1_Free":
                        RM3_1 = RunMode.Free;
                        CameraUse2(true, ccd3_1, RM3_1, RM3_1, "实时模式",
                            PassData.Is_Open_ccd3_1,
                            PassData.Is_Grabing_ccd3_1,
                            PassData.Is_SetSoftTrigger_ccd3_1,
                            PassData.Is_SetExternal_ccd3_1,
                            out PassData.Is_Open_ccd3_1,
                            out PassData.Is_Grabing_ccd3_1,
                            out PassData.Is_SetSoftTrigger_ccd3_1,
                            out PassData.Is_SetExternal_ccd3_1,
                            out RM3_1);
                        break;
                    //*************************************
                    case "ccd3_2_SoftTriggerPT"://软触发试教
                        RM3_2 = RunMode.Tech;
                        CameraUse2(true, ccd3_2, RM3_2, RM3_2, "软触发模式",
                             PassData.Is_Open_ccd3_2,
                            PassData.Is_Grabing_ccd3_2,
                            PassData.Is_SetSoftTrigger_ccd3_2,
                            PassData.Is_SetExternal_ccd3_2,
                            out PassData.Is_Open_ccd3_2,
                            out PassData.Is_Grabing_ccd3_2,
                            out PassData.Is_SetSoftTrigger_ccd3_2,
                            out PassData.Is_SetExternal_ccd3_2,
                            out RM3_2);
                        break;


                    case "ccd3_2_HardTrigger_Run"://运行硬触发
                        RM3_2 = RunMode.Auto;
                        CameraUse2(true, ccd3_2, RM3_2, RM3_2, "硬触发模式",
                            PassData.Is_Open_ccd3_2,
                            PassData.Is_Grabing_ccd3_2,
                            PassData.Is_SetSoftTrigger_ccd3_2,
                            PassData.Is_SetExternal_ccd3_2,
                            out PassData.Is_Open_ccd3_2,
                            out PassData.Is_Grabing_ccd3_2,
                            out PassData.Is_SetSoftTrigger_ccd3_2,
                            out PassData.Is_SetExternal_ccd3_2,
                            out RM3_2);
                        break;

                    case "ccd3_2_HardTrigger_PT"://示教硬触发
                        RM3_2 = RunMode.Tech;
                        CameraUse2(true, ccd3_2, RM3_2, RM3_2, "硬触发模式",
                           PassData.Is_Open_ccd3_2,
                            PassData.Is_Grabing_ccd3_2,
                            PassData.Is_SetSoftTrigger_ccd3_2,
                            PassData.Is_SetExternal_ccd3_2,
                            out PassData.Is_Open_ccd3_2,
                            out PassData.Is_Grabing_ccd3_2,
                            out PassData.Is_SetSoftTrigger_ccd3_2,
                            out PassData.Is_SetExternal_ccd3_2,
                            out RM3_2);
                        break;

                    case "ccd3_2_SoftTriggerDo":
                        RM3_2 = RunMode.Save;
                        CameraUse2(true, ccd3_2, RM3_2, RM3_2, "软触发模式",
                           PassData.Is_Open_ccd3_2,
                            PassData.Is_Grabing_ccd3_2,
                            PassData.Is_SetSoftTrigger_ccd3_2,
                            PassData.Is_SetExternal_ccd3_2,
                            out PassData.Is_Open_ccd3_2,
                            out PassData.Is_Grabing_ccd3_2,
                            out PassData.Is_SetSoftTrigger_ccd3_2,
                            out PassData.Is_SetExternal_ccd3_2,
                            out RM3_2);
                        break;

                    case "ccd3_2_Stop":

                        RM3_2 = RunMode.Free;
                        CameraUse2(false, ccd3_2, RM3_2, RM3_2, "软触发模式",
                            PassData.Is_Open_ccd3_2,
                            PassData.Is_Grabing_ccd3_2,
                            PassData.Is_SetSoftTrigger_ccd3_2,
                            PassData.Is_SetExternal_ccd3_2,
                            out PassData.Is_Open_ccd3_2,
                            out PassData.Is_Grabing_ccd3_2,
                            out PassData.Is_SetSoftTrigger_ccd3_2,
                            out PassData.Is_SetExternal_ccd3_2,
                            out RM3_2);

                        break;


                    case "ccd3_2_Free":
                        RM3_2 = RunMode.Free;
                        CameraUse2(true, ccd3_2, RM3_2, RM3_2, "实时模式",
                            PassData.Is_Open_ccd3_2,
                            PassData.Is_Grabing_ccd3_2,
                            PassData.Is_SetSoftTrigger_ccd3_2,
                            PassData.Is_SetExternal_ccd3_2,
                            out PassData.Is_Open_ccd3_2,
                            out PassData.Is_Grabing_ccd3_2,
                            out PassData.Is_SetSoftTrigger_ccd3_2,
                            out PassData.Is_SetExternal_ccd3_2,
                            out RM3_2);
                        break;
                    //*************************************

                    case "ccd4_SoftTriggerPT"://软触发试教
                        RM4 = RunMode.Tech;
                        CameraUse2(true, ccd4, RM4, RM4, "软触发模式",
                            PassData.Is_Open_ccd4,
                            PassData.Is_Grabing_ccd4,
                            PassData.Is_SetSoftTrigger_ccd4,
                            PassData.Is_SetExternal_ccd4,
                            out PassData.Is_Open_ccd4,
                            out PassData.Is_Grabing_ccd4,
                            out PassData.Is_SetSoftTrigger_ccd4,
                            out PassData.Is_SetExternal_ccd4,
                            out RM4);
                        break;


                    case "ccd4_HardTrigger_Run"://运行硬触发
                        RM4 = RunMode.Auto;
                        CameraUse2(true, ccd4, RM4, RM4, "硬触发模式",
                            PassData.Is_Open_ccd4,
                            PassData.Is_Grabing_ccd4,
                            PassData.Is_SetSoftTrigger_ccd4,
                            PassData.Is_SetExternal_ccd4,
                            out PassData.Is_Open_ccd4,
                            out PassData.Is_Grabing_ccd4,
                            out PassData.Is_SetSoftTrigger_ccd4,
                            out PassData.Is_SetExternal_ccd4,
                            out RM4);
                        break;

                    case "ccd4_HardTrigger_PT"://示教硬触发
                        RM4 = RunMode.Tech;
                        CameraUse2(true, ccd4, RM4, RM4, "硬触发模式",
                            PassData.Is_Open_ccd4,
                            PassData.Is_Grabing_ccd4,
                            PassData.Is_SetSoftTrigger_ccd4,
                            PassData.Is_SetExternal_ccd4,
                            out PassData.Is_Open_ccd4,
                            out PassData.Is_Grabing_ccd4,
                            out PassData.Is_SetSoftTrigger_ccd4,
                            out PassData.Is_SetExternal_ccd4,
                            out RM4);
                        break;

                    case "ccd4_SoftTriggerDo":
                        RM4 = RunMode.Save;
                        CameraUse2(true, ccd4, RM4, RM4, "软触发模式",
                           PassData.Is_Open_ccd4,
                            PassData.Is_Grabing_ccd4,
                            PassData.Is_SetSoftTrigger_ccd4,
                            PassData.Is_SetExternal_ccd4,
                            out PassData.Is_Open_ccd4,
                            out PassData.Is_Grabing_ccd4,
                            out PassData.Is_SetSoftTrigger_ccd4,
                            out PassData.Is_SetExternal_ccd4,
                            out RM4);
                        break;

                    case "ccd4_Stop":

                        RM4 = RunMode.Free;
                        CameraUse2(false, ccd4, RM4, RM4, "软触发模式",
                            PassData.Is_Open_ccd4,
                            PassData.Is_Grabing_ccd4,
                            PassData.Is_SetSoftTrigger_ccd4,
                            PassData.Is_SetExternal_ccd4,
                            out PassData.Is_Open_ccd4,
                            out PassData.Is_Grabing_ccd4,
                            out PassData.Is_SetSoftTrigger_ccd4,
                            out PassData.Is_SetExternal_ccd4,
                            out RM4);

                        break;

                    case "ccd4_Free":
                        RM4 = RunMode.Free;
                        CameraUse2(true, ccd4, RM4, RM4, "实时模式",
                            PassData.Is_Open_ccd4,
                            PassData.Is_Grabing_ccd4,
                            PassData.Is_SetSoftTrigger_ccd4,
                            PassData.Is_SetExternal_ccd4,
                            out PassData.Is_Open_ccd4,
                            out PassData.Is_Grabing_ccd4,
                            out PassData.Is_SetSoftTrigger_ccd4,
                            out PassData.Is_SetExternal_ccd4,
                            out RM4);
                        break;

                    //***************PLC通信************************
                    case "欧姆龙写入":
                        PLC_Read_Write("omr_Write");
                        break;

                    case "欧姆龙读取":
                        PLC_Read_Write("omr_Read");

                        break;

                    case "基恩士写入":
                        PLC_Read_Write("kv_Write");

                        break;

                    case "基恩士读取":
                        PLC_Read_Write("kv_Read");

                        break;

                    //删除
                    case "删除":
                        DeletetFile();
                        break;

                    //************另存为*********************
                    case "另存为":

                        SaveAs();
                        break;


                    //************保存*********************
                    case "保存文件":
                        SaveDataAll();
                        UpdateLogList("保存文件", p_D_Log);

                        break;


                    //************打开*********************
                    case "打开文件":
                        ReadFile();
                        break;


                    case "获取当前工位参数文件":
                        getCurrentFile();
                        break;

                    case "获取参数文件":
                        UpdateUsedPara();

                        break;

                        string strn1;
                    case "工位1新建":
                        if (PassData.strContent != "" && PassData.strContent != string.Empty && PassData.strContent != null)
                        {
                            string stt = PassData.strContent;
                            strn1 = AboutTime("ms") + "___" + stt;
                            string strPath = pathFile1 + "\\" + strn1;
                            if (Directory.Exists(strPath) == false)
                            {
                                Directory.CreateDirectory(strPath);
                                UpdateLogList("ccd1新建:" + strn1, p_D_Log);
                            }

                        }
                        else
                        {
                            MessageBox.Show("请先命名");

                        }
                        break;

                    case "工位2新建":
                        if (PassData.strContent != "" && PassData.strContent != string.Empty && PassData.strContent != null)
                        {
                            string stt = PassData.strContent;
                            strn1 = AboutTime("ms") + "___" + stt;
                            string strPath = pathFile2 + "\\" + strn1;
                            if (Directory.Exists(strPath) == false)
                            {
                                Directory.CreateDirectory(strPath);
                                UpdateLogList("ccd1新建:" + strn1, p_D_Log);
                            }

                        }
                        else
                        {
                            MessageBox.Show("请先命名");

                        }
                        break;

                    case "工位3_1新建":
                        if (PassData.strContent != "" && PassData.strContent != string.Empty && PassData.strContent != null)
                        {
                            string stt = PassData.strContent;
                            strn1 = AboutTime("ms") + "___" + stt;
                            string strPath = pathFile3 + "\\" + strn1;
                            if (Directory.Exists(strPath) == false)
                            {
                                Directory.CreateDirectory(strPath);
                                UpdateLogList("ccd1新建:" + strn1, p_D_Log);
                            }

                        }
                        else
                        {
                            MessageBox.Show("请先命名");

                        }
                        break;

                    case "工位3_2新建":
                        if (PassData.strContent != "" && PassData.strContent != string.Empty && PassData.strContent != null)
                        {
                            string stt = PassData.strContent;
                            strn1 = AboutTime("ms") + "___" + stt;
                            string strPath = pathFile4 + "\\" + strn1;
                            if (Directory.Exists(strPath) == false)
                            {
                                Directory.CreateDirectory(strPath);
                                UpdateLogList("ccd1新建:" + strn1, p_D_Log);
                            }

                        }
                        else
                        {
                            MessageBox.Show("请先命名");

                        }
                        break;


                    case "工位4新建":
                        if (PassData.strContent != "" && PassData.strContent != string.Empty && PassData.strContent != null)
                        {
                            string stt = PassData.strContent;
                            strn1 = AboutTime("ms") + "___" + stt;
                            string strPath = pathFile5 + "\\" + strn1;
                            if (Directory.Exists(strPath) == false)
                            {
                                Directory.CreateDirectory(strPath);
                                UpdateLogList("ccd1新建:" + strn1, p_D_Log);
                            }

                        }
                        else
                        {
                            MessageBox.Show("请先命名");

                        }
                        break;




                    //******************1111111111111111
                    case "运行相机1曝光":
                        if (ccd1.ifccdConnected)
                        {
                            ccd1.setExposureTime(Para_MainPara.expose1);
                            UpdateLogList("ccd1曝光:" + Para_MainPara.expose1.ToString(), p_D_Log);

                        }

                        break;



                    case "运行相机1增益":
                        if (ccd1.ifccdConnected)
                        {
                            ccd1.setGain(Para_MainPara.gain1);
                            UpdateLogList("ccd1增益:" + Para_MainPara.gain1.ToString(), p_D_Log);

                        }

                        break;

                    //****************222222222222

                    case "运行相机2曝光":
                        if (ccd2.ifccdConnected)
                        {
                            ccd2.setExposureTime(Para_MainPara.expose2);
                            UpdateLogList("ccd2曝光:" + Para_MainPara.expose2.ToString(), p_D_Log);

                        }

                        break;



                    case "运行相机2增益":
                        if (ccd2.ifccdConnected)
                        {
                            ccd2.setGain(Para_MainPara.gain2);
                            UpdateLogList("ccd2增益:" + Para_MainPara.gain2.ToString(), p_D_Log);

                        }

                        break;

                    //***************运行相机3 
                    case "运行相机3曝光":
                        if (ccd3_1.ifccdConnected)
                        {
                            ccd3_1.setExposureTime(Para_MainPara.expose3);
                            UpdateLogList("ccd3_1曝光:" + Para_MainPara.expose3.ToString(), p_D_Log);

                        }

                        break;


                    case "运行相机3测增益":
                        if (ccd3_1.ifccdConnected)
                        {
                            ccd3_1.setGain(Para_MainPara.gain3);
                            UpdateLogList("ccd3_1增益:" + Para_MainPara.gain3.ToString(), p_D_Log);
                        }

                        break;


                    //***************运行相机4 
                    case "运行相机4曝光":
                        if (ccd3_2.ifccdConnected)
                        {
                            ccd3_2.setExposureTime(Para_MainPara.expose4);
                            UpdateLogList("ccd3_2曝光:" + Para_MainPara.expose4.ToString(), p_D_Log);

                        }

                        break;


                    case "运行相机4测增益":
                        if (ccd3_2.ifccdConnected)
                        {
                            ccd3_2.setGain(Para_MainPara.gain4);
                            UpdateLogList("ccd3_2增益:" + Para_MainPara.gain4.ToString(), p_D_Log);

                        }

                        break;

                    //***************运行相机5 
                    case "运行相机5曝光":
                        if (ccd4.ifccdConnected)
                        {
                            ccd4.setExposureTime(Para_MainPara.expose5);
                            UpdateLogList("ccd4曝光:" + Para_MainPara.expose5.ToString(), p_D_Log);

                        }

                        break;


                    case "运行相机5测增益":
                        if (ccd4.ifccdConnected)
                        {
                            ccd4.setGain(Para_MainPara.gain5);
                            UpdateLogList("ccd4增益:" + Para_MainPara.gain5.ToString(), p_D_Log);

                        }

                        break;



                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("问题：" + ex.ToString());
            }


        }

        #region 其他函数


        //委托事件函数
        void DelegateDeclaration()
        {

            PassData.delegate_CameraSet = delegate_CameraSet;

        }

        //参数加载
        void DataLoad()
        {
            try
            {

                Para_MainPara = (MainPara)LoadNoDeletePara(Para_MainPara.GetType(), p_pathParaMainName);
                NumDisp = Para_MainPara.NumDisp;
                PassData.NumDisp = NumDisp;
                dataGridViewInitial();

                pathFileName1 = pathFile1 + "\\" + Para_MainPara.gw1ParaName;
                pathFileName2 = pathFile2 + "\\" + Para_MainPara.gw2ParaName;
                pathFileName3 = pathFile3 + "\\" + Para_MainPara.gw3ParaName;
                pathFileName5 = pathFile5 + "\\" + Para_MainPara.gw5ParaName;

                m1_Method.Para_stationpara = (StationPara)m1_Method.LoadTest(
                m1_Method.Para_stationpara.GetType(), pathFileName1);
                ShowModuleName(1);

                m2_Method.Para_stationpara = (StationPara)m2_Method.LoadTest(
                m2_Method.Para_stationpara.GetType(), pathFileName2);
                ShowModuleName(2);

                m3_Method.Para_stationpara = (StationPara)m3_Method.LoadTest(
                m3_Method.Para_stationpara.GetType(), pathFileName3);
                ShowModuleName(3);

                m5_Method.Para_stationpara = (StationPara)m5_Method.LoadTest(
                m5_Method.Para_stationpara.GetType(), pathFileName5);
                ShowModuleName(5);

                if (NumDisp >= 5)
                {
                    pathFileName4 = pathFile4 + "\\" + Para_MainPara.gw4ParaName;

                    m4_Method.Para_stationpara = (StationPara)m4_Method.LoadTest(
                   m4_Method.Para_stationpara.GetType(), pathFileName4);
                    ShowModuleName(4);

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        //模块名字显示
        void ShowModuleName(int gw)
        {
            try
            {

                switch (gw)
                {
                    case 1:
                        m1_Method.ModualFormName = "第1工位";
                        m1_Method.m_P1PatPreprocessMethod.ModualFormName = "第1工位定位1模板预处理";
                        m1_Method.m_PatMaxMethod1.ModualFormName = "第1工位定位1模板";
                        m1_Method.m_P1CalPreprocessMethod.ModualFormName = "第1工位定位1卡尺预处理";
                        m1_Method.m_FindCircleMethod1.ModualFormName = "第1工位定位1圆卡尺";
                        m1_Method.m_ComputeCenterMethod1.ModualFormName = "第1工位定位1方形卡尺";

                        m1_Method.m_P2PatPreprocessMethod.ModualFormName = "第1工位定位2模板预处理";
                        m1_Method.m_PatMaxMethod2.ModualFormName = "第1工位定位2模板";
                        m1_Method.m_P2CalPreprocessMethod.ModualFormName = "第1工位定位2卡尺预处理";
                        m1_Method.m_FindCircleMethod2.ModualFormName = "第1工位定位2圆卡尺";
                        m1_Method.m_ComputeCenterMethod2.ModualFormName = "第1工位定位2方形卡尺";

                        m1_Method.m_ValueConvertMethod.ModualFormName = "第1工位传值设置";

                        m1_Method.m_blobMethod.ModualFormName = "第1工位Blob";
                        m1_Method.m_svmMethod.ModualFormName = "第1工位SVM";

                        m1_Method.m_InkDetectMethod.ModualFormName = "第1工位涂墨检测";
                        m1_Method.m_InkDetectMethodND.ModualFormName = "第1工位空盘检测";

                        break;

                    case 2:
                        m2_Method.ModualFormName = "第2工位";
                        m2_Method.m_P1PatPreprocessMethod.ModualFormName = "第2工位定位1模板预处理";
                        m2_Method.m_PatMaxMethod1.ModualFormName = "第2工位定位1模板";
                        m2_Method.m_P1CalPreprocessMethod.ModualFormName = "第2工位定位1卡尺预处理";
                        m2_Method.m_FindCircleMethod1.ModualFormName = "第2工位定位1圆卡尺";
                        m2_Method.m_ComputeCenterMethod1.ModualFormName = "第2工位定位1方形卡尺";
                        m2_Method.m_P2PatPreprocessMethod.ModualFormName = "第2工位定位2模板预处理";
                        m2_Method.m_PatMaxMethod2.ModualFormName = "第2工位定位2模板";
                        m2_Method.m_P2CalPreprocessMethod.ModualFormName = "第2工位定位2卡尺预处理";
                        m2_Method.m_FindCircleMethod2.ModualFormName = "第2工位定位2圆卡尺";
                        m2_Method.m_ComputeCenterMethod2.ModualFormName = "第2工位定位2方形卡尺";
                        m2_Method.m_ValueConvertMethod.ModualFormName = "第2工位传值设置";
                        m2_Method.m_blobMethod.ModualFormName = "第2工位Blob";
                        m2_Method.m_svmMethod.ModualFormName = "第2工位SVM";
                        m2_Method.m_InkDetectMethod.ModualFormName = "第2工位涂墨检测";
                        m2_Method.m_InkDetectMethodND.ModualFormName = "第2工位空盘检测";

                        break;


                    case 3:
                        m3_Method.ModualFormName = "第3_1工位";
                        m3_Method.m_P1PatPreprocessMethod.ModualFormName = "第3_1工位定位1模板预处理";
                        m3_Method.m_PatMaxMethod1.ModualFormName = "第3_1工位定位1模板";
                        m3_Method.m_P1CalPreprocessMethod.ModualFormName = "第3_1工位定位1卡尺预处理";
                        m3_Method.m_FindCircleMethod1.ModualFormName = "第3_1工位定位1圆卡尺";
                        m3_Method.m_ComputeCenterMethod1.ModualFormName = "第3_1工位定位1方形卡尺";
                        m3_Method.m_P2PatPreprocessMethod.ModualFormName = "第3_1工位定位2模板预处理";
                        m3_Method.m_PatMaxMethod2.ModualFormName = "第3_1工位定位2模板";
                        m3_Method.m_P2CalPreprocessMethod.ModualFormName = "第3_1工位定位2卡尺预处理";
                        m3_Method.m_FindCircleMethod2.ModualFormName = "第3_1工位定位2圆卡尺";
                        m3_Method.m_ComputeCenterMethod2.ModualFormName = "第3_1工位定位2方形卡尺";
                        m3_Method.m_ValueConvertMethod.ModualFormName = "第3_1工位传值设置";
                        m3_Method.m_blobMethod.ModualFormName = "第3_1工位Blob";
                        m3_Method.m_svmMethod.ModualFormName = "第3_1工位SVM";
                        m3_Method.m_InkDetectMethod.ModualFormName = "第3_1工位涂墨检测";
                        m3_Method.m_InkDetectMethodND.ModualFormName = "第3_1工位空盘检测";

                        break;

                    case 4:
                        m4_Method.ModualFormName = "第3_2工位";
                        m4_Method.m_P1PatPreprocessMethod.ModualFormName = "第3_2工位定位1模板预处理";
                        m4_Method.m_PatMaxMethod1.ModualFormName = "第3_2工位定位1模板";
                        m4_Method.m_P1CalPreprocessMethod.ModualFormName = "第3_2工位定位1卡尺预处理";
                        m4_Method.m_FindCircleMethod1.ModualFormName = "第3_2工位定位1圆卡尺";
                        m4_Method.m_ComputeCenterMethod1.ModualFormName = "第3_2工位定位1方形卡尺";
                        m4_Method.m_P2PatPreprocessMethod.ModualFormName = "第3_2工位定位2模板预处理";
                        m4_Method.m_PatMaxMethod2.ModualFormName = "第3_2工位定位2模板";
                        m4_Method.m_P2CalPreprocessMethod.ModualFormName = "第3_2工位定位2卡尺预处理";
                        m4_Method.m_FindCircleMethod2.ModualFormName = "第3_2工位定位2圆卡尺";
                        m4_Method.m_ComputeCenterMethod2.ModualFormName = "第3_2工位定位2方形卡尺";
                        m4_Method.m_ValueConvertMethod.ModualFormName = "第3_2工位传值设置";
                        m4_Method.m_blobMethod.ModualFormName = "第3_2工位Blob";
                        m4_Method.m_svmMethod.ModualFormName = "第3_2工位SVM";
                        m4_Method.m_InkDetectMethod.ModualFormName = "第3_2工位涂墨检测";
                        m4_Method.m_InkDetectMethodND.ModualFormName = "第3_2工位空盘检测";

                        break;


                    case 5:
                        m5_Method.ModualFormName = "第4工位";
                        m5_Method.m_P1PatPreprocessMethod.ModualFormName = "第4工位定位1模板预处理";
                        m5_Method.m_PatMaxMethod1.ModualFormName = "第4工位定位1模板";
                        m5_Method.m_P1CalPreprocessMethod.ModualFormName = "第4工位定位1卡尺预处理";
                        m5_Method.m_FindCircleMethod1.ModualFormName = "第4工位定位1圆卡尺";
                        m5_Method.m_ComputeCenterMethod1.ModualFormName = "第4工位定位1方形卡尺";
                        m5_Method.m_P2PatPreprocessMethod.ModualFormName = "第4工位定位2模板预处理";
                        m5_Method.m_PatMaxMethod2.ModualFormName = "第4工位定位2模板";
                        m5_Method.m_P2CalPreprocessMethod.ModualFormName = "第4工位定位2卡尺预处理";
                        m5_Method.m_FindCircleMethod2.ModualFormName = "第4工位定位2圆卡尺";
                        m5_Method.m_ComputeCenterMethod2.ModualFormName = "第4工位定位2方形卡尺";
                        m5_Method.m_ValueConvertMethod.ModualFormName = "第4工位传值设置";
                        m5_Method.m_blobMethod.ModualFormName = "第4工位Blob";
                        m5_Method.m_svmMethod.ModualFormName = "第4工位SVM";
                        m5_Method.m_InkDetectMethod.ModualFormName = "第4工位涂墨检测";
                        m5_Method.m_InkDetectMethodND.ModualFormName = "第4工位空盘检测";

                        break;

                    default:
                        break;
                }



            }
            catch (Exception)
            {


            }


        }

        //
        void SaveDataAll()
        {
            try
            {

                bool d1 = m1_Method.SaveTest(m1_Method.Para_stationpara,
                  pathFileName1);

                bool d2 = m2_Method.SaveTest(m2_Method.Para_stationpara,
                pathFileName2);

                bool d3 = m3_Method.SaveTest(m3_Method.Para_stationpara,
               pathFileName3);

                bool d5 = m5_Method.SaveTest(m5_Method.Para_stationpara,
               pathFileName5);

                if (NumDisp >= 5)
                {
                    bool d4 = m4_Method.SaveTest(m4_Method.Para_stationpara,
                    pathFileName4);
                }

                SaveNoDeletePara(Para_MainPara, p_pathParaMainName);

            }
            catch (Exception)
            {

                // throw;
            }


        }

        public object LoadNoDeletePara(Type type, string filename)
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
                return Para_MainPara;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        public void SaveNoDeletePara(object obj, string filename)
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
                return;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        private string AboutTime(string s_or_ms)
        {
            string qwe = null;
            try
            {
                HTuple ms = new HTuple();
                HTuple s = new HTuple();
                HTuple minute = new HTuple();
                HTuple hour = new HTuple();
                HTuple day = new HTuple();
                HTuple yDay = new HTuple();
                HTuple month = new HTuple();
                HTuple year = new HTuple();

                HOperatorSet.GetSystemTime(out ms, out s, out minute, out hour, out day, out yDay,
                    out month, out year);

                switch (s_or_ms)
                {
                    case "s":
                        qwe = year.ToString() + "__" +
                    month.ToString() + "__" +
                    day.ToString() + "__" +
                    hour.ToString() + "_" +
                      minute.ToString() + "_" +
                      s.ToString();
                        break;


                    case "ms":
                        qwe = year.ToString() + "__" +
                      month.ToString() + "__" +
                      day.ToString() + "__" +
                      hour.ToString() + "_" +
                      minute.ToString() + "_" +
                      s.ToString() + "_" +
                      ms.ToString();
                        break;


                    default:
                        break;
                }



            }
            catch (Exception)
            {
                return qwe;

            }

            return qwe;
        }

        void LoadAllPara(string path, out List<string> m_AllparaName)
        {
            m_AllparaName = null;
            try
            {

                m_AllparaName = new List<string>();

                foreach (string f in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
                {

                    m_AllparaName.Add(
                        Path.GetFileNameWithoutExtension(f.ToString()));

                }

                //cmb_ParaName.Items.Clear();
                //foreach (var item in m_AllparaName)
                //{
                //    cmb_ParaName.Items.Add(item.ToString());
                //}

                //cmb_ParaName.Text = m_AllparaName[0];

            }
            catch (Exception)
            {

                // throw;
            }

        }

        //列举参数文件
        void UpdateUsedPara()
        {
            //属性
            try
            {
                switch (PassData.strContent)
                {

                    case "工位1列举参数文件":
                        LoadAllPara(pathFile1, out PassData.m_AllparaName);

                        break;


                    case "工位2列举参数文件":
                        LoadAllPara(pathFile2, out PassData.m_AllparaName);


                        break;

                    case "工位3_1列举参数文件":

                        LoadAllPara(pathFile3, out PassData.m_AllparaName);

                        break;

                    case "工位3_2列举参数文件":
                        LoadAllPara(pathFile4, out PassData.m_AllparaName);


                        break;

                    case "工位4列举参数文件":
                        LoadAllPara(pathFile5, out PassData.m_AllparaName);


                        break;

                    default:
                        break;
                }



            }
            catch (Exception)
            {


            }


        }

        //获取当前工位所用参数文件
        void getCurrentFile()
        {
            try
            {
                switch (PassData.strContent)
                {
                    case "获取工位1参数文件":
                        PassData.strContent = Para_MainPara.gw1ParaName;
                        break;

                    case "获取工位2参数文件":
                        PassData.strContent = Para_MainPara.gw2ParaName;
                        break;

                    case "获取工位3_1参数文件":
                        PassData.strContent = Para_MainPara.gw3ParaName;
                        break;

                    case "获取工位3_2参数文件":
                        PassData.strContent = Para_MainPara.gw4ParaName;
                        break;

                    case "获取工位4参数文件":
                        PassData.strContent = Para_MainPara.gw5ParaName;
                        break;

                    default:
                        break;
                }

            }
            catch (Exception)
            {


            }


        }

        //打开/读取文件
        void ReadFile()
        {
            try
            {
                switch (PassData.strContent)
                {

                    case "工位1打开":
                        Para_MainPara.gw1ParaName = PassData.strContent2;
                        UpdateLogList("ccd1读取:" + PassData.strContent2
                           , p_D_Log);
                        break;

                    case "工位2打开":
                        Para_MainPara.gw2ParaName = PassData.strContent2;
                        UpdateLogList("ccd2读取:" + PassData.strContent2
                           , p_D_Log);

                        break;

                    case "工位3_1打开":
                        Para_MainPara.gw3ParaName = PassData.strContent2;

                        UpdateLogList("ccd3_1读取:" + PassData.strContent2
                           , p_D_Log);
                        break;

                    case "工位3_2打开":
                        Para_MainPara.gw4ParaName = PassData.strContent2;
                        UpdateLogList("ccd3_2读取:" + PassData.strContent2
                           , p_D_Log);

                        break;

                    case "工位4打开":
                        Para_MainPara.gw5ParaName = PassData.strContent2;
                        UpdateLogList("ccd4读取:" + PassData.strContent2
                           , p_D_Log);

                        break;


                    default:
                        break;

                }

                SaveDataAll();
                DataLoad();

            }
            catch (Exception)
            {


            }



        }

        //复制文件夹
        public int CopyFolder(string sourceFolder, string destFolder)
        {
            try
            {
                //如果目标路径不存在,则创建目标路径
                if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }
                //得到原文件根目录下的所有文件
                string[] files = System.IO.Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = System.IO.Path.GetFileName(file);
                    string dest = System.IO.Path.Combine(destFolder, name);
                    System.IO.File.Copy(file, dest);//复制文件
                }
                //得到原文件根目录下的所有文件夹
                string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = System.IO.Path.GetFileName(folder);
                    string dest = System.IO.Path.Combine(destFolder, name);
                    CopyFolder(folder, dest);//构建目标路径,递归复制文件
                }
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }

        }

        //另存为
        void SaveAs()
        {
            // string strPath = pathFile1 + "\\" + AboutTime("ms") + "___" + stt;

            try
            {
                string str3;
                string str1;
                string str2;
                switch (PassData.strContent)
                {

                    case "工位1另存为":
                        //目标
                        str1 = pathFile1 + "\\" + PassData.strContent3;
                        //结果
                        str3 = AboutTime("ms") + "__" + PassData.strContent2;
                        str2 = pathFile1 + "\\" + str3;
                        CopyFolder(str1, str2);
                        UpdateLogList("ccd1另存操作:" + PassData.strContent3
                            + "->" + str3, p_D_Log);
                        break;

                    case "工位2另存为":
                        //目标
                        str1 = pathFile2 + "\\" + PassData.strContent3;
                        //结果
                        str3 = AboutTime("ms") + "__" + PassData.strContent2;
                        str2 = pathFile2 + "\\" + str3;
                        CopyFolder(str1, str2);
                        UpdateLogList("ccd2另存操作:" + PassData.strContent3
                            + "->" + str3, p_D_Log);

                        break;

                    case "工位3_1另存为":
                        //目标
                        str1 = pathFile3 + "\\" + PassData.strContent3;
                        //结果
                        str3 = AboutTime("ms") + "__" + PassData.strContent2;
                        str2 = pathFile3 + "\\" + str3;
                        CopyFolder(str1, str2);
                        UpdateLogList("ccd3_1另存操作:" + PassData.strContent3
                            + "->" + str3, p_D_Log);
                        break;

                    case "工位3_2另存为":
                        //目标
                        str1 = pathFile4 + "\\" + PassData.strContent3;
                        //结果
                        str3 = AboutTime("ms") + "__" + PassData.strContent2;
                        str2 = pathFile4 + "\\" + str3;
                        CopyFolder(str1, str2);
                        UpdateLogList("ccd3_2另存操作:" + PassData.strContent3
                            + "->" + str3, p_D_Log);
                        break;

                    case "工位4另存为":
                        //目标
                        str1 = pathFile5 + "\\" + PassData.strContent3;
                        //结果
                        str3 = AboutTime("ms") + "__" + PassData.strContent2;
                        str2 = pathFile5 + "\\" + str3;
                        CopyFolder(str1, str2);
                        UpdateLogList("ccd4另存操作:" + PassData.strContent3
                            + "->" + str3, p_D_Log);
                        break;

                    default:
                        break;

                }





            }
            catch (Exception)
            {

            }


        }

        //删除
        void DeletetFile()
        {

            string strPath = "";
            try
            {
                switch (PassData.strContent)
                {

                    case "工位1":
                        strPath = pathFile1 + "\\" + PassData.strContent2;
                        break;

                    case "工位2":
                        strPath = pathFile2 + "\\" + PassData.strContent2;

                        break;

                    case "工位3_1":
                        strPath = pathFile3 + "\\" + PassData.strContent2;

                        break;

                    case "工位3_2":
                        strPath = pathFile4 + "\\" + PassData.strContent2;

                        break;

                    case "工位4":
                        strPath = pathFile5 + "\\" + PassData.strContent2;

                        break;

                    default:
                        break;

                }

                if (Directory.Exists(strPath))
                {
                    Directory.Delete(strPath, true);

                    UpdateLogList(
                        PassData.strContent + "删除:" + PassData.strContent2
                        , p_D_Log);


                }

            }
            catch (Exception)
            {


            }


        }

        //plc读取写入
        void PLC_Read_Write(string action)
        {
            try
            {
                switch (action)
                {

                    case "omr_Read":

                        string sdaf;
                        ReadISPLCConnection11(
                            Para_MainPara.PLC_IP_omr,
                            Para_MainPara.PLC_Port_omr,
                            Para_MainPara.PC_finalIP_omr,
                            int.Parse(PassData.strContent),
                            int.Parse(PassData.strContent2),
                            int.Parse(PassData.strContent3),
                            out sdaf);
                        PassData.strContent4 = sdaf;

                        PassData.plc_about = "omr_Read";
                        PassData.plcDone++;
                        break;

                    case "omr_Write":

                        WriteISPLCConnection11(
                            Para_MainPara.PLC_IP_omr,
                            Para_MainPara.PLC_Port_omr,
                            Para_MainPara.PC_finalIP_omr,
                            int.Parse(PassData.strContent),
                            int.Parse(PassData.strContent2),
                            int.Parse(PassData.strContent3),
                            PassData.strContent4);

                        break;

                    case "kv_Write":

                        SendMsg(
                            Para_MainPara.PLC_IP_kv,
                            Para_MainPara.PLC_Port_kv,
                            int.Parse(PassData.strContent),
                            int.Parse(PassData.strContent2),
                            PassData.strContent3);


                        break;

                    case "kv_Read":

                        string dsad = null;
                        ReadMsg(
                        Para_MainPara.PLC_IP_kv,
                        Para_MainPara.PLC_Port_kv,
                        int.Parse(PassData.strContent),
                        int.Parse(PassData.strContent2),
                        out dsad);
                        PassData.strContent3 = dsad;

                        PassData.plc_about = "kv_Read";
                        PassData.plcDone++;
                        break;

                    default:
                        break;
                }




            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }



        }

        //图像保存函数
        void SaveImage1(HObject Image, string savePath, string strType, string strName,
            out string readPath)
        {
            readPath = "";
            if (Image != null)
            {
                try
                {

                    HTuple ms = new HTuple();
                    HTuple s = new HTuple();
                    HTuple minute = new HTuple();
                    HTuple hour = new HTuple();
                    HTuple day = new HTuple();
                    HTuple yDay = new HTuple();
                    HTuple month = new HTuple();
                    HTuple year = new HTuple();

                    HOperatorSet.GetSystemTime(out ms, out s, out minute, out hour, out day, out yDay,
                        out month, out year);

                    string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_" + ms.I.ToString();
                    string q1 = savePath + "\\" + strName + "_" + time + "." + strType;
                    HOperatorSet.WriteImage(Image, strType, 0, q1);
                    readPath = q1;
                }
                catch
                {

                    MessageBox.Show("图像保存失败");
                }



            }

        }

        //保存窗口图像
        void SaveShowImage(GVS.HalconDisp.Control.HWindow_Final m_Disp, string strPath, string strType)
        {
            HTuple year = new HTuple();
            HTuple month = new HTuple();
            HTuple Yday = new HTuple();
            HTuple day = new HTuple();

            HTuple hour = new HTuple();
            HTuple minute = new HTuple();
            HTuple second = new HTuple();
            HTuple msecond = new HTuple();

            HOperatorSet.GetSystemTime(
                out msecond,
                out second,
                out minute,
                out hour,

                out day,
                out Yday,
                out month,
                out year
                );

            double qww2313 = msecond.D;

            string time24 = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_" + qww2313.ToString();

            string qePath = strPath + "\\" + time24;

            m_Disp.SaveWindowDump(qePath, strType);

        }

        //相机个数更改，重启程序
        void NumDispChange()
        {
            try
            {

                this.Close();
                //using System.Diagnostics;
                // Process m_Process = new Process();
                // m_Process.Close();
                //  Thread.Sleep(3000);
                Application.Restart();


            }
            catch (Exception)
            {

            }

        }

        void OMR_PT_Send(string X, string Y, int startAddr)
        {

            string dfdas = X + "," + Y;
            WriteISPLCConnection11(
              Para_MainPara.PLC_IP_omr,
              Para_MainPara.PLC_Port_omr,
              Para_MainPara.PC_finalIP_omr,
              130,//D区
              startAddr,//从100开始
              2,
             dfdas);

        }

        void OMR_Run_Send(string X, string Y, string Flag, int startAddr)
        {

            string dfdas = X + "," + Y + "," + Flag;
            WriteISPLCConnection11(
              Para_MainPara.PLC_IP_omr,
              Para_MainPara.PLC_Port_omr,
              Para_MainPara.PC_finalIP_omr,
              130,//D区
              startAddr,//从100开始
              3,
             dfdas);

        }

        void KV_PT_Send(string x, string y, string angle,string Flag, int MemoryP)
        {
            string daa = x + "," + y + "," + angle + "," + Flag;
            SendMsg(Para_MainPara.PLC_IP_kv,
                 Para_MainPara.PLC_Port_kv,
                 MemoryP,//60000
                  4,
                 daa
                );

        }

        void KV_Run_Send(string x, string y, string angle, string Flag, int MemoryP)
        {
            string daa = x + "," + y + "," + angle +","+ Flag;
            SendMsg(Para_MainPara.PLC_IP_kv,
                 Para_MainPara.PLC_Port_kv,
                 MemoryP,//60000
                  4,
                 daa
                );

        }

        //示教页面发送数据给PLC
        void PT_FormSendMsgToPLC()
        {

            try
            {
                switch (PassData.strContent3)
                {
                    case "ccd1":
                        if (Para_MainPara.PLCSelect == "欧姆龙")
                        {
                            OMR_PT_Send(
                                PassData.strContent,
                                PassData.strContent2,
                                P_CCD1_TrialTeach_omr);
                        }
                        else
                        {
                            KV_PT_Send(
                                PassData.strContent,
                                PassData.strContent2,"0","1",
                                P_CCD1_TrialTeach_kv
                                );
                        }
                        break;
                    case "ccd2":
                        if (Para_MainPara.PLCSelect == "欧姆龙")
                        {
                            OMR_PT_Send(
                                PassData.strContent,
                                PassData.strContent2,
                                P_CCD2_TrialTeach_omr);
                        }
                        else
                        {
                            KV_PT_Send(
                             PassData.strContent,
                             PassData.strContent2, "0", "1",
                             P_CCD2_TrialTeach_kv
                             );
                        }
                        break;
                    case "ccd3_1":
                        if (Para_MainPara.PLCSelect == "欧姆龙")
                        {
                            OMR_PT_Send(
                                PassData.strContent,
                                PassData.strContent2,
                                P_CCD3_TrialTeach_omr);
                        }
                        else
                        {
                            KV_PT_Send(
                             PassData.strContent,
                             PassData.strContent2, "0", "1",
                             P_CCD3_TrialTeach_kv
                             );
                        }
                        break;
                    case "ccd3_2":
                        if (Para_MainPara.PLCSelect == "欧姆龙")
                        {
                            OMR_PT_Send(
                                PassData.strContent,
                                PassData.strContent2,
                                P_CCD4_TrialTeach_omr);
                        }
                        else
                        {
                            KV_PT_Send(
                             PassData.strContent,
                             PassData.strContent2, "0", "1",
                             P_CCD4_TrialTeach_kv
                             );
                        }
                        break;

                    case "ccd4":

                        if (Para_MainPara.PLCSelect == "欧姆龙")
                        {
                            OMR_PT_Send(
                            PassData.strContent,
                            PassData.strContent2,
                            P_CCD5_TrialTeach_omr);
                        }
                        else
                        {
                            KV_PT_Send(
                             PassData.strContent,
                             PassData.strContent2, "0", "1",
                             P_CCD5_TrialTeach_kv
                             );
                        }
                        break;
                    default:
                        break;

                }

            }
            catch (Exception)
            {


            }

        }

        //Form界面相机回调函数使用
        void PT_CameraSendMsgToPLC(string PLCSelect, string str1, string str2, int start_omr, int start_kv)
        {

            try
            {
                //   PLCSelect= Para_MainPara.PLCSelect 
                if (PLCSelect == "欧姆龙")
                {
                    OMR_PT_Send(
                        str1,
                        str2,
                        start_omr);
                }
                else
                  {
                    KV_PT_Send(
                        str1,
                        str2, "0", "1",
                        start_kv
                        );
                }



            }
            catch (Exception)
            {


            }

        }

        //显示切换
        void FreeChangeShow()
        {
            try
            {
                switch (PassData.OnlyUse1_FreeShow)
                {
                    case "ccd1":
                        FreeChange = "ccd1";
                        break;

                    case "ccd2":
                        FreeChange = "ccd2";
                        break;

                    case "ccd3_1":
                        FreeChange = "ccd3_1";
                        break;

                    case "ccd3_2":
                        FreeChange = "ccd3_2";
                        break;

                    case "ccd4":
                        FreeChange = "ccd4";
                        break;

                    default:
                        break;
                }


            }
            catch (Exception)
            {


            }




        }

        //运行数据清除
        void DataClear()
        {
            if (MessageBox.Show("确定数据清零?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                try
                {

                    string g1 = "gw1-" + "OK:" + Para_MainPara.gw1_LVDataDisplay[4]
                                   + ",NG:" + Para_MainPara.gw1_LVDataDisplay[5]
                                   + ",All:" + Para_MainPara.gw1_LVDataDisplay[6]
                                   + ",良率:" + Para_MainPara.gw1_LVDataDisplay[7];
                    UpdateLogList(g1, p_D_Log);
                    string g2 = "gw2-" + "OK:" + Para_MainPara.gw2_LVDataDisplay[4]
                                + ",NG:" + Para_MainPara.gw2_LVDataDisplay[5]
                                + ",All:" + Para_MainPara.gw2_LVDataDisplay[6]
                                + ",良率:" + Para_MainPara.gw2_LVDataDisplay[7];
                    UpdateLogList(g2, p_D_Log);
                    if (NumDisp >= 5)
                    {
                        string g3 = "gw3_1-" + "OK:" + Para_MainPara.gw3_LVDataDisplay[4]
                                                      + ",NG:" + Para_MainPara.gw3_LVDataDisplay[5]
                                                      + ",All:" + Para_MainPara.gw3_LVDataDisplay[6]
                                                      + ",良率:" + Para_MainPara.gw3_LVDataDisplay[7];
                        UpdateLogList(g3, p_D_Log);
                        string g4 = "gw3_2-" + "OK:" + Para_MainPara.gw4_LVDataDisplay[4]
                                                     + ",NG:" + Para_MainPara.gw4_LVDataDisplay[5]
                                                     + ",All:" + Para_MainPara.gw4_LVDataDisplay[6]
                                                     + ",良率:" + Para_MainPara.gw4_LVDataDisplay[7];
                        UpdateLogList(g4, p_D_Log);
                    }
                    else
                    {
                        string g3 = "gw3-" + "OK:" + Para_MainPara.gw3_LVDataDisplay[4]
                                                        + ",NG:" + Para_MainPara.gw3_LVDataDisplay[5]
                                                        + ",All:" + Para_MainPara.gw3_LVDataDisplay[6]
                                                        + ",良率:" + Para_MainPara.gw3_LVDataDisplay[7];
                        UpdateLogList(g3, p_D_Log);
                    }

                    string g5 = "gw4-" + "OK:" + Para_MainPara.gw5_LVDataDisplay[4]
                                                      + ",NG:" + Para_MainPara.gw5_LVDataDisplay[5]
                                                      + ",All:" + Para_MainPara.gw5_LVDataDisplay[6]
                                                      + ",良率:" + Para_MainPara.gw5_LVDataDisplay[7];
                    UpdateLogList(g5, p_D_Log);

                    //数据清零
                    Para_MainPara.gw1_LVDataDisplay[0] = 1;
                    Para_MainPara.gw1_LVDataDisplay[1] = 0;
                    Para_MainPara.gw1_LVDataDisplay[2] = 0;
                    Para_MainPara.gw1_LVDataDisplay[3] = 0;
                    Para_MainPara.gw1_LVDataDisplay[4] = 0;
                    Para_MainPara.gw1_LVDataDisplay[5] = 0;
                    Para_MainPara.gw1_LVDataDisplay[6] = 0;
                    Para_MainPara.gw1_LVDataDisplay[7] = 0;
                    datagridViewPlay(index_gw1, Para_MainPara.gw1_LVDataDisplay);

                    Para_MainPara.gw2_LVDataDisplay[0] = 2;
                    Para_MainPara.gw2_LVDataDisplay[1] = 0;
                    Para_MainPara.gw2_LVDataDisplay[2] = 0;
                    Para_MainPara.gw2_LVDataDisplay[3] = 0;
                    Para_MainPara.gw2_LVDataDisplay[4] = 0;
                    Para_MainPara.gw2_LVDataDisplay[5] = 0;
                    Para_MainPara.gw2_LVDataDisplay[6] = 0;
                    Para_MainPara.gw2_LVDataDisplay[7] = 0;
                    datagridViewPlay(index_gw2, Para_MainPara.gw2_LVDataDisplay);

                    Para_MainPara.gw3_LVDataDisplay[0] = 3;
                    Para_MainPara.gw3_LVDataDisplay[1] = 0;
                    Para_MainPara.gw3_LVDataDisplay[2] = 0;
                    Para_MainPara.gw3_LVDataDisplay[3] = 0;
                    Para_MainPara.gw3_LVDataDisplay[4] = 0;
                    Para_MainPara.gw3_LVDataDisplay[5] = 0;
                    Para_MainPara.gw3_LVDataDisplay[6] = 0;
                    Para_MainPara.gw3_LVDataDisplay[7] = 0;
                    datagridViewPlay(index_gw3, Para_MainPara.gw3_LVDataDisplay);

                    if (NumDisp >= 5)
                    {
                        Para_MainPara.gw4_LVDataDisplay[0] = 4;
                        Para_MainPara.gw4_LVDataDisplay[1] = 0;
                        Para_MainPara.gw4_LVDataDisplay[2] = 0;
                        Para_MainPara.gw4_LVDataDisplay[3] = 0;
                        Para_MainPara.gw4_LVDataDisplay[4] = 0;
                        Para_MainPara.gw4_LVDataDisplay[5] = 0;
                        Para_MainPara.gw4_LVDataDisplay[6] = 0;
                        Para_MainPara.gw4_LVDataDisplay[7] = 0;
                        datagridViewPlay(index_gw4, Para_MainPara.gw4_LVDataDisplay);
                    }
                    else
                    {
                        Para_MainPara.gw4_LVDataDisplay[0] = 4;
                        Para_MainPara.gw4_LVDataDisplay[1] = 0;
                        Para_MainPara.gw4_LVDataDisplay[2] = 0;
                        Para_MainPara.gw4_LVDataDisplay[3] = 0;
                        Para_MainPara.gw4_LVDataDisplay[4] = 0;
                        Para_MainPara.gw4_LVDataDisplay[5] = 0;
                        Para_MainPara.gw4_LVDataDisplay[6] = 0;
                        Para_MainPara.gw4_LVDataDisplay[7] = 0;

                    }


                    Para_MainPara.gw5_LVDataDisplay[0] = 4;
                    Para_MainPara.gw5_LVDataDisplay[1] = 0;
                    Para_MainPara.gw5_LVDataDisplay[2] = 0;
                    Para_MainPara.gw5_LVDataDisplay[3] = 0;
                    Para_MainPara.gw5_LVDataDisplay[4] = 0;
                    Para_MainPara.gw5_LVDataDisplay[5] = 0;
                    Para_MainPara.gw5_LVDataDisplay[6] = 0;
                    Para_MainPara.gw5_LVDataDisplay[7] = 0;
                    datagridViewPlay(index_gw5, Para_MainPara.gw5_LVDataDisplay);

                    UpdateLogList("数据清零", p_D_Log);

                    SaveDataAll();

                    //bool d1=  m1_Method.SaveTest(m1_Method.Para_stationpara,
                    //  pathFileName1);




                }
                catch (Exception ex)
                {

                    UpdateLogList(ex.Message, p_D_Log);
                    // UpdateLogList(ex.ToString(), p_D_Log);

                }
            }


        }

        //运行图像保存
        void RunImageSave(int gw, bool resultOk, HObject Image, GVS.HalconDisp.Control.HWindow_Final m_Disp)
        {
            try
            {

                switch (gw)
                {
                    case 1:

                        if (Para_MainPara.ImageRunSave1 == "仅保存NG" && (resultOk == false))
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw1_SaveNGImage, "bmp", "gw1_J_NG", out readPath);
                        }
                        else if (Para_MainPara.ImageRunSave1 == "仅保存OK" && (resultOk == true))
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw1_SaveOKImage, "bmp", "gw1_J_OK", out readPath);
                        }
                        else if (Para_MainPara.ImageRunSave1 == "保存所有")
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw1_SaveAllImage, "bmp", "gw1_All", out readPath);
                        }


                        if (Para_MainPara.ImageShowSave1 == "仅保存NG" && (resultOk == false))
                        {
                            SaveShowImage(m_Disp, p_gw1_SaveWDNGImage, "jpg");

                        }
                        else if (Para_MainPara.ImageShowSave1 == "仅保存OK" && (resultOk == true))
                        {
                            SaveShowImage(m_Disp, p_gw1_SaveWDOKImage, "jpg");

                        }
                        else if (Para_MainPara.ImageShowSave1 == "保存所有")
                        {
                            SaveShowImage(m_Disp, p_gw1_SaveWDAllImage, "jpg");

                        }


                        break;


                    case 2:

                        if (Para_MainPara.ImageRunSave2 == "仅保存NG" && (resultOk == false))
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw2_SaveNGImage, "bmp", "gw1_J_NG", out readPath);
                        }
                        else if (Para_MainPara.ImageRunSave2 == "仅保存OK" && (resultOk == true))
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw2_SaveOKImage, "bmp", "gw1_J_OK", out readPath);
                        }
                        else if (Para_MainPara.ImageRunSave2 == "保存所有")
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw2_SaveAllImage, "bmp", "gw1_All", out readPath);
                        }


                        if (Para_MainPara.ImageShowSave2 == "仅保存NG" && (resultOk == false))
                        {
                            SaveShowImage(m_Disp, p_gw2_SaveWDNGImage, "jpg");

                        }
                        else if (Para_MainPara.ImageShowSave2 == "仅保存OK" && (resultOk == true))
                        {
                            SaveShowImage(m_Disp, p_gw2_SaveWDOKImage, "jpg");

                        }
                        else if (Para_MainPara.ImageShowSave2 == "保存所有")
                        {
                            SaveShowImage(m_Disp, p_gw2_SaveWDAllImage, "jpg");

                        }

                        break;


                    case 3://3-1

                        if (Para_MainPara.ImageRunSave3 == "仅保存NG" && (resultOk == false))
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw3_SaveNGImage, "bmp", "gw1_J_NG", out readPath);
                        }
                        else if (Para_MainPara.ImageRunSave3 == "仅保存OK" && (resultOk == true))
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw3_SaveOKImage, "bmp", "gw1_J_OK", out readPath);
                        }
                        else if (Para_MainPara.ImageRunSave3 == "保存所有")
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw3_SaveAllImage, "bmp", "gw1_All", out readPath);
                        }


                        if (Para_MainPara.ImageShowSave3 == "仅保存NG" && (resultOk == false))
                        {
                            SaveShowImage(m_Disp, p_gw3_SaveWDNGImage, "jpg");

                        }
                        else if (Para_MainPara.ImageShowSave3 == "仅保存OK" && (resultOk == true))
                        {
                            SaveShowImage(m_Disp, p_gw3_SaveWDOKImage, "jpg");

                        }

                        else if (Para_MainPara.ImageShowSave3 == "保存所有")
                        {
                            SaveShowImage(m_Disp, p_gw3_SaveWDAllImage, "jpg");

                        }

                        break;

                    case 4://3-2

                        if (Para_MainPara.ImageRunSave4 == "仅保存NG" && (resultOk == false))
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw4_SaveNGImage, "bmp", "gw1_J_NG", out readPath);
                        }
                        else if (Para_MainPara.ImageRunSave4 == "仅保存OK" && (resultOk == true))
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw4_SaveOKImage, "bmp", "gw1_J_OK", out readPath);
                        }
                        else if (Para_MainPara.ImageRunSave4 == "保存所有")
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw4_SaveAllImage, "bmp", "gw1_All", out readPath);
                        }


                        if (Para_MainPara.ImageShowSave4 == "仅保存NG" && (resultOk == false))
                        {
                            SaveShowImage(m_Disp, p_gw4_SaveWDNGImage, "jpg");

                        }
                        else if (Para_MainPara.ImageShowSave4 == "仅保存OK" && (resultOk == true))
                        {
                            SaveShowImage(m_Disp, p_gw4_SaveWDOKImage, "jpg");

                        }

                        else if (Para_MainPara.ImageShowSave4 == "保存所有")
                        {
                            SaveShowImage(m_Disp, p_gw4_SaveWDAllImage, "jpg");

                        }

                        break;

                    case 5://4

                        if (Para_MainPara.ImageRunSave5 == "仅保存NG" && (resultOk == false))
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw5_SaveNGImage, "bmp", "gw1_J_NG", out readPath);
                        }
                        else if (Para_MainPara.ImageRunSave5 == "仅保存OK" && (resultOk == true))
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw5_SaveOKImage, "bmp", "gw1_J_OK", out readPath);
                        }
                        else if (Para_MainPara.ImageRunSave5 == "保存所有")
                        {
                            string readPath = "00";
                            SaveImage1(Image, p_gw5_SaveAllImage, "bmp", "gw1_All", out readPath);
                        }


                        if (Para_MainPara.ImageShowSave5 == "仅保存NG" && (resultOk == false))
                        {
                            SaveShowImage(m_Disp, p_gw5_SaveWDNGImage, "jpg");
                        }
                        else if (Para_MainPara.ImageShowSave5 == "仅保存OK" && (resultOk == true))
                        {
                            SaveShowImage(m_Disp, p_gw5_SaveWDOKImage, "jpg");
                        }
                        else if (Para_MainPara.ImageShowSave5 == "保存所有")
                        {
                            SaveShowImage(m_Disp, p_gw5_SaveWDAllImage, "jpg");
                        }

                        break;


                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                UpdateLogList(ex.Message, p_D_Log);
            }


        }


        string strHelpPath = "";
        //说明文档打开
        void OpenHelpPDFfile()
        {
            try
            {
                Process.Start(strHelpPath);

            }
            catch
            {


            }


        }

        #endregion

        void Start()
        {
            try
            {

                //再次判断PLC连接状态
                if (Para_MainPara.PLCSelect == "欧姆龙")
                {
                    if (IsPLC_Connected_omr())
                    {
                        HardWareStateChange("plc", 1);
                    }
                    else
                    {
                        HardWareStateChange("plc", 0);

                        MessageBox.Show("plc连接失败，无法开启");
                        return;
                    }
                }
                else
                {
                    if (IsPLC_Connected_kv())
                    {
                        HardWareStateChange("plc", 1);
                    }
                    else
                    {
                        HardWareStateChange("plc", 0);
                        MessageBox.Show("plc连接失败，无法开启");
                        return;
                    }
                }


                //再次判断相机连接状态
                if (ccd1 != null && ccd1.ifccdConnected)
                {
                    HardWareStateChange("ccd1", 1);
                }
                else
                {
                    HardWareStateChange("ccd1", 0);
                    MessageBox.Show("ccd1连接失败，无法开启");
                    return;
                }

                if (ccd2 != null && ccd2.ifccdConnected)
                {
                    HardWareStateChange("ccd2", 1);
                }
                else
                {
                    HardWareStateChange("ccd2", 0);
                    MessageBox.Show("ccd2连接失败，无法开启");
                    return;
                }

                if (ccd3_1 != null && ccd3_1.ifccdConnected)
                {
                    HardWareStateChange("ccd3_1", 1);
                }
                else
                {
                    HardWareStateChange("ccd3_1", 0);
                    MessageBox.Show("ccd3_1连接失败，无法开启");
                    return;
                }

                if (NumDisp >= 5)
                {
                    if (ccd3_2 != null && ccd3_2.ifccdConnected)
                    {
                        HardWareStateChange("ccd3_2", 1);
                    }
                    else
                    {
                        HardWareStateChange("ccd3_2", 0);
                        MessageBox.Show("ccd3_2连接失败，无法开启");
                        return;
                    }
                }

                if (ccd4 != null && ccd4.ifccdConnected)
                {
                    HardWareStateChange("ccd4", 1);
                }
                else
                {
                    HardWareStateChange("ccd4", 0);
                    MessageBox.Show("ccd4连接失败，无法开启");
                    return;
                }

                //enable 
                menuStrip1.Enabled = false;
                pb1.Enabled = false;
                pb3.Enabled = false;
                pb4.Enabled = false;
                pb5.Enabled = false;
                pb6.Enabled = false;
                pb7.Enabled = false;
                pb8.Enabled = false;
                pb9.Enabled = false;

                //关闭窗口
                if (Form_PLCSet != null)
                {
                    Form_PLCSet.Close();
                }
                if (Form_FileSet != null)
                {
                    Form_FileSet.Close();
                }
                if (Form_PointTech_Camera != null)
                {
                    Form_PointTech_Camera.Close();
                }

                sleepTime = 20;

                //打开硬触发
                PassData.cameraSetAbout = "ccd1_HardTrigger_Run";
                PassData.cameraSet++;

                PassData.cameraSetAbout = "ccd2_HardTrigger_Run";
                PassData.cameraSet++;

                PassData.cameraSetAbout = "ccd3_1_HardTrigger_Run";
                PassData.cameraSet++;

                if (NumDisp >= 5)
                {
                    PassData.cameraSetAbout = "ccd3_2_HardTrigger_Run";
                    PassData.cameraSet++;
                }
                PassData.cameraSetAbout = "ccd4_HardTrigger_Run";
                PassData.cameraSet++;

                toolStripStatusLabel6.Text = "运行";
                HardWareStateChange("主程序状态", 1);
                UpdateLogList("运行打开", p_D_Log);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()); ;
            }

        }

        void Finish()
        {

            try
            {
                sleepTime = 3000;

                //enable 恢复
                menuStrip1.Enabled = true;
                pb1.Enabled = true;
                pb3.Enabled = true;
                pb4.Enabled = true;
                pb5.Enabled = true;
                pb6.Enabled = true;
                pb7.Enabled = true;
                pb8.Enabled = true;
                pb9.Enabled = true;

                PassData.cameraSetAbout = "ccd1_Stop";
                PassData.cameraSet++;
                PassData.cameraSetAbout = "ccd2_Stop";
                PassData.cameraSet++;
                PassData.cameraSetAbout = "ccd3_1_Stop";
                PassData.cameraSet++;
                if (NumDisp >= 5)
                {
                    PassData.cameraSetAbout = "ccd3_2_Stop";
                    PassData.cameraSet++;
                }

                PassData.cameraSetAbout = "ccd4_Stop";
                PassData.cameraSet++;

                toolStripStatusLabel6.Text = "暂停";
                HardWareStateChange("主程序状态", 0);

                UpdateLogList("运行关闭", p_D_Log);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }

        #region 路径相关地址函数 声明 文件夹创建

        //string p_pathParaBase = "..\\VisionFiles";
        string p_pathParaBase;
        string p_pathParaMain;
        string pathFile1;
        string pathFile2;
        string pathFile3;
        string pathFile4;
        string pathFile5;

        string p_pathParaMainName;

        //当前正在使用的文件名
        string pathFileName1;
        string pathFileName2;
        string pathFileName3;
        string pathFileName4;//3-2 相机
        string pathFileName5;//4

        void FoldCreate0()
        {
            try
            {
                DirectoryInfo Path_Zong = new DirectoryInfo(string.Format(@"{0}..\..\", Application.StartupPath));
                p_pathParaBase = Path_Zong.FullName + "VisionFiles";
                pathFile1 = p_pathParaBase + "\\1";
                pathFile2 = p_pathParaBase + "\\2";
                pathFile3 = p_pathParaBase + "\\3_1";
                pathFile4 = p_pathParaBase + "\\3_2";
                pathFile5 = p_pathParaBase + "\\4";

                p_pathParaMain = p_pathParaBase + "\\Main";
                p_pathParaMainName = p_pathParaMain + "\\MainPara";

                strHelpPath = p_pathParaBase + "\\帮助文档";

                if (!Directory.Exists(p_pathParaBase))
                {
                    Directory.CreateDirectory(p_pathParaBase);
                }

                if (!Directory.Exists(p_pathParaMain))
                {
                    Directory.CreateDirectory(p_pathParaMain);
                }

                if (!Directory.Exists(pathFile1))
                {
                    Directory.CreateDirectory(pathFile1);
                }

                if (!Directory.Exists(pathFile2))
                {
                    Directory.CreateDirectory(pathFile2);
                }

                if (!Directory.Exists(pathFile3))
                {
                    Directory.CreateDirectory(pathFile3);
                }

                if (!Directory.Exists(pathFile5))
                {
                    Directory.CreateDirectory(pathFile5);
                }


            }
            catch (Exception)
            {


            }


        }

        // string p_D_Image = "D:\\TM2\\图片";//存放图像总
        string p_D_Log = "D:\\TM2\\日志\\";//存放日志

        string p_gw1_SaveImage = "D:\\TM2\\工位1\\SaveImage_手动";//手动保存(原图)
        string p_gw1_SaveAllImage = "D:\\TM2\\工位1\\SaveAllImage_运行";//运行保存所有图片(原图)
        string p_gw1_SaveNGImage = "D:\\TM2\\工位1\\SaveNGImage_运行";//运行保存NG图片(原图)
        string p_gw1_SaveOKImage = "D:\\TM2\\工位1\\SaveOKImage_运行";//运行保存NG图片(原图)
        string p_gw1_SaveWDAllImage = "D:\\TM2\\工位1\\SaveWDAllImage_运行";//窗口图像(缩略图)
        string p_gw1_SaveWDNGImage = "D:\\TM2\\工位1\\SaveWDNGImage_运行";//窗口图像(缩略图)
        string p_gw1_SaveWDOKImage = "D:\\TM2\\工位1\\SaveWDOKImage_运行";//窗口图像(缩略图)

        string p_gw2_SaveImage = "D:\\TM2\\工位2\\SaveImage_手动";//手动保存(原图)
        string p_gw2_SaveAllImage = "D:\\TM2\\工位2\\SaveAllImage_运行";//运行保存所有图片(原图)
        string p_gw2_SaveNGImage = "D:\\TM2\\工位2\\SaveNGImage_运行";//运行保存NG图片(原图)
        string p_gw2_SaveOKImage = "D:\\TM2\\工位2\\SaveOKImage_运行";//运行保存NG图片(原图)
        string p_gw2_SaveWDAllImage = "D:\\TM2\\工位2\\SaveWDAllImage_运行";//窗口图像(缩略图)
        string p_gw2_SaveWDNGImage = "D:\\TM2\\工位2\\SaveWDNGImage_运行";//窗口图像(缩略图)
        string p_gw2_SaveWDOKImage = "D:\\TM2\\工位2\\SaveWDOKImage_运行";//窗口图像(缩略图)

        string p_gw3_SaveImage = "D:\\TM2\\工位3_1\\SaveImage_手动";//手动保存(原图)
        string p_gw3_SaveAllImage = "D:\\TM2\\工位3_1\\SaveAllImage_运行";//运行保存所有图片(原图)
        string p_gw3_SaveNGImage = "D:\\TM2\\工位3_1\\SaveNGImage_运行";//运行保存NG图片(原图)
        string p_gw3_SaveOKImage = "D:\\TM2\\工位3_1\\SaveOKImage_运行";//运行保存NG图片(原图)
        string p_gw3_SaveWDAllImage = "D:\\TM2\\工位3_1\\SaveWDAllImage_运行";//窗口图像(缩略图)
        string p_gw3_SaveWDNGImage = "D:\\TM2\\工位3_1\\SaveWDNGImage_运行";//窗口图像(缩略图)
        string p_gw3_SaveWDOKImage = "D:\\TM2\\工位3_1\\SaveWDOKImage_运行";//窗口图像(缩略图)

        string p_gw4_SaveImage = "D:\\TM2\\工位3_2\\SaveImage_手动";//手动保存(原图)
        string p_gw4_SaveAllImage = "D:\\TM2\\工位3_2\\SaveAllImage_运行";//运行保存所有图片(原图)
        string p_gw4_SaveNGImage = "D:\\TM2\\工位3_2\\SaveNGImage_运行";//运行保存NG图片(原图)
        string p_gw4_SaveOKImage = "D:\\TM2\\工位3_2\\SaveOKImage_运行";//运行保存NG图片(原图)
        string p_gw4_SaveWDAllImage = "D:\\TM2\\工位3_2\\SaveWDAllImage_运行";//窗口图像(缩略图)
        string p_gw4_SaveWDNGImage = "D:\\TM2\\工位3_2\\SaveWDNGImage_运行";//窗口图像(缩略图)
        string p_gw4_SaveWDOKImage = "D:\\TM2\\工位3_2\\SaveWDOKImage_运行";//窗口图像(缩略图)

        string p_gw5_SaveImage = "D:\\TM2\\工位4\\SaveImage_手动";//手动保存(原图)
        string p_gw5_SaveAllImage = "D:\\TM2\\工位4\\SaveAllImage_运行";//运行保存所有图片(原图)
        string p_gw5_SaveNGImage = "D:\\TM2\\工位4\\SaveNGImage_运行";//运行保存NG图片(原图)
        string p_gw5_SaveOKImage = "D:\\TM2\\工位4\\SaveOKImage_运行";//运行保存NG图片(原图)
        string p_gw5_SaveWDAllImage = "D:\\TM2\\工位4\\SaveWDAllImage_运行";//窗口图像(缩略图)
        string p_gw5_SaveWDNGImage = "D:\\TM2\\工位4\\SaveWDNGImage_运行";//窗口图像(缩略图)
        string p_gw5_SaveWDOKImage = "D:\\TM2\\工位4\\SaveWDOKImage_运行";//窗口图像(缩略图)

        void FoldCreate()
        {

            if (!Directory.Exists(p_D_Log))
            {
                Directory.CreateDirectory(p_D_Log);
            }

            //111
            if (!Directory.Exists(p_gw1_SaveImage))
            {
                Directory.CreateDirectory(p_gw1_SaveImage);
            }

            if (!Directory.Exists(p_gw1_SaveAllImage))
            {
                Directory.CreateDirectory(p_gw1_SaveAllImage);
            }
            if (!Directory.Exists(p_gw1_SaveNGImage))
            {
                Directory.CreateDirectory(p_gw1_SaveNGImage);
            }
            if (!Directory.Exists(p_gw1_SaveOKImage))
            {
                Directory.CreateDirectory(p_gw1_SaveOKImage);
            }


            if (!Directory.Exists(p_gw1_SaveWDAllImage))
            {
                Directory.CreateDirectory(p_gw1_SaveWDAllImage);
            }
            if (!Directory.Exists(p_gw1_SaveWDOKImage))
            {
                Directory.CreateDirectory(p_gw1_SaveWDOKImage);
            }
            if (!Directory.Exists(p_gw1_SaveWDNGImage))
            {
                Directory.CreateDirectory(p_gw1_SaveWDNGImage);
            }
            //222
            if (!Directory.Exists(p_gw2_SaveImage))
            {
                Directory.CreateDirectory(p_gw2_SaveImage);
            }

            if (!Directory.Exists(p_gw2_SaveAllImage))
            {
                Directory.CreateDirectory(p_gw2_SaveAllImage);
            }
            if (!Directory.Exists(p_gw2_SaveNGImage))
            {
                Directory.CreateDirectory(p_gw2_SaveNGImage);
            }
            if (!Directory.Exists(p_gw2_SaveOKImage))
            {
                Directory.CreateDirectory(p_gw2_SaveOKImage);
            }


            if (!Directory.Exists(p_gw2_SaveWDAllImage))
            {
                Directory.CreateDirectory(p_gw2_SaveWDAllImage);
            }
            if (!Directory.Exists(p_gw2_SaveWDOKImage))
            {
                Directory.CreateDirectory(p_gw2_SaveWDOKImage);
            }
            if (!Directory.Exists(p_gw2_SaveWDNGImage))
            {
                Directory.CreateDirectory(p_gw2_SaveWDNGImage);
            }

            //3_1
            if (!Directory.Exists(p_gw3_SaveImage))
            {
                Directory.CreateDirectory(p_gw3_SaveImage);
            }

            if (!Directory.Exists(p_gw3_SaveAllImage))
            {
                Directory.CreateDirectory(p_gw3_SaveAllImage);
            }
            if (!Directory.Exists(p_gw3_SaveNGImage))
            {
                Directory.CreateDirectory(p_gw3_SaveNGImage);
            }
            if (!Directory.Exists(p_gw3_SaveOKImage))
            {
                Directory.CreateDirectory(p_gw3_SaveOKImage);
            }

            if (!Directory.Exists(p_gw3_SaveWDAllImage))
            {
                Directory.CreateDirectory(p_gw3_SaveWDAllImage);
            }
            if (!Directory.Exists(p_gw3_SaveWDOKImage))
            {
                Directory.CreateDirectory(p_gw3_SaveWDOKImage);
            }
            if (!Directory.Exists(p_gw3_SaveWDNGImage))
            {
                Directory.CreateDirectory(p_gw3_SaveWDNGImage);
            }

            //4
            if (!Directory.Exists(p_gw5_SaveImage))
            {
                Directory.CreateDirectory(p_gw5_SaveImage);
            }

            if (!Directory.Exists(p_gw5_SaveAllImage))
            {
                Directory.CreateDirectory(p_gw5_SaveAllImage);
            }

            if (!Directory.Exists(p_gw5_SaveNGImage))
            {
                Directory.CreateDirectory(p_gw5_SaveNGImage);
            }

            if (!Directory.Exists(p_gw5_SaveOKImage))
            {
                Directory.CreateDirectory(p_gw5_SaveOKImage);
            }


            if (!Directory.Exists(p_gw5_SaveWDAllImage))
            {
                Directory.CreateDirectory(p_gw5_SaveWDAllImage);
            }
            if (!Directory.Exists(p_gw5_SaveWDOKImage))
            {
                Directory.CreateDirectory(p_gw5_SaveWDOKImage);
            }
            if (!Directory.Exists(p_gw5_SaveWDNGImage))
            {
                Directory.CreateDirectory(p_gw5_SaveWDNGImage);
            }

            //
            if (NumDisp >= 5)
            {
                if (!Directory.Exists(pathFile4))
                {
                    Directory.CreateDirectory(pathFile4);
                }

                //3-2
                if (!Directory.Exists(p_gw4_SaveImage))
                {
                    Directory.CreateDirectory(p_gw4_SaveImage);
                }

                if (!Directory.Exists(p_gw4_SaveAllImage))
                {
                    Directory.CreateDirectory(p_gw4_SaveAllImage);
                }
                if (!Directory.Exists(p_gw4_SaveNGImage))
                {
                    Directory.CreateDirectory(p_gw4_SaveNGImage);
                }
                if (!Directory.Exists(p_gw4_SaveOKImage))
                {
                    Directory.CreateDirectory(p_gw4_SaveOKImage);
                }


                if (!Directory.Exists(p_gw4_SaveWDAllImage))
                {
                    Directory.CreateDirectory(p_gw4_SaveWDAllImage);
                }

                if (!Directory.Exists(p_gw4_SaveWDOKImage))
                {
                    Directory.CreateDirectory(p_gw4_SaveWDOKImage);
                }

                if (!Directory.Exists(p_gw4_SaveWDNGImage))
                {
                    Directory.CreateDirectory(p_gw4_SaveWDNGImage);
                }

            }


        }

        #endregion

        #region Menustrip

        void MenuStrip1()
        {
            try
            {

                MenuStrip MS = menuStrip1;
                //*****************文件***********************
                ToolStripMenuItem tsm1_1 = new ToolStripMenuItem("新建");
                ToolStripMenuItem tsm1_2 = new ToolStripMenuItem("打开");
                ToolStripMenuItem tsm1_3 = new ToolStripMenuItem("保存");
                ToolStripMenuItem tsm1_4 = new ToolStripMenuItem("另存为");
                tsm1_1.Click += DemoClick;
                tsm1_2.Click += DemoClick;
                tsm1_3.Click += DemoClick;
                tsm1_4.Click += DemoClick;
                文件ToolStripMenuItem.DropDownItems.Add(tsm1_1);
                文件ToolStripMenuItem.DropDownItems.Add(tsm1_2);
                文件ToolStripMenuItem.DropDownItems.Add(tsm1_3);
                文件ToolStripMenuItem.DropDownItems.Add(tsm1_4);

                //******************设置***********************
                ToolStripMenuItem tsm2_1 = new ToolStripMenuItem("工位");
                ToolStripMenuItem tsm2_2 = new ToolStripMenuItem("相机");
                ToolStripMenuItem tsm2_3 = new ToolStripMenuItem("登录");
                ToolStripMenuItem tsm2_4 = new ToolStripMenuItem("PLC");
                //tsm2_1.Click += DemoClick;
                tsm2_2.Click += DemoClick;
                tsm2_3.Click += DemoClick;
                tsm2_4.Click += DemoClick;
                设置ToolStripMenuItem.DropDownItems.Add(tsm2_1);
                设置ToolStripMenuItem.DropDownItems.Add(tsm2_2);
                设置ToolStripMenuItem.DropDownItems.Add(tsm2_3);
                设置ToolStripMenuItem.DropDownItems.Add(tsm2_4);

                ToolStripMenuItem tsm2_1_1 = new ToolStripMenuItem("工位1");
                ToolStripMenuItem tsm2_1_2 = new ToolStripMenuItem("工位2");

                ToolStripMenuItem tsm2_1_3 = new ToolStripMenuItem("工位3_1");
                ToolStripMenuItem tsm2_1_31 = new ToolStripMenuItem("工位3_1");
                ToolStripMenuItem tsm2_1_32 = new ToolStripMenuItem("工位3_2");
                if (NumDisp == 4)
                {
                    tsm2_1_3 = new ToolStripMenuItem("工位3_1");

                }
                else//NumDisp=5
                {
                    tsm2_1_31 = new ToolStripMenuItem("工位3_1");
                    tsm2_1_32 = new ToolStripMenuItem("工位3_2");

                }
                ToolStripMenuItem tsm2_1_4 = new ToolStripMenuItem("工位4");
                tsm2_1_1.Click += DemoClick;
                tsm2_1_2.Click += DemoClick;
                if (NumDisp == 4)
                {
                    tsm2_1_3.Click += DemoClick;

                }
                else
                {
                    tsm2_1_31.Click += DemoClick;
                    tsm2_1_32.Click += DemoClick;
                }
                tsm2_1_4.Click += DemoClick;


                tsm2_1.DropDownItems.Add(tsm2_1_1);
                tsm2_1.DropDownItems.Add(tsm2_1_2);
                if (NumDisp == 4)
                {
                    tsm2_1.DropDownItems.Add(tsm2_1_3);
                }
                else
                {
                    tsm2_1.DropDownItems.Add(tsm2_1_31);
                    tsm2_1.DropDownItems.Add(tsm2_1_32);
                }
                tsm2_1.DropDownItems.Add(tsm2_1_4);

                //******************图像***********************
                ToolStripMenuItem tsm3_1 = new ToolStripMenuItem("工位1打开图像");
                ToolStripMenuItem tsm3_2 = new ToolStripMenuItem("工位2打开图像");
                ToolStripMenuItem tsm3_3_1 = new ToolStripMenuItem("工位3_1打开图像");
                ToolStripMenuItem tsm3_4 = new ToolStripMenuItem("工位4打开图像");
                ToolStripMenuItem tsm3_3_2 = null;
                tsm3_1.Click += DemoClick;
                tsm3_2.Click += DemoClick;
                tsm3_3_1.Click += DemoClick;
                tsm3_4.Click += DemoClick;

                if (NumDisp >= 5)
                {
                    tsm3_3_2 = new ToolStripMenuItem("工位3_2打开图像");
                    tsm3_3_2.Click += DemoClick;
                    //  图像ToolStripMenuItem.DropDownItems.Add(tsm3_3_2);

                }
                图像ToolStripMenuItem.DropDownItems.Add(tsm3_1);
                图像ToolStripMenuItem.DropDownItems.Add(tsm3_2);
                图像ToolStripMenuItem.DropDownItems.Add(tsm3_3_1);
                if (NumDisp >= 5)
                {
                    图像ToolStripMenuItem.DropDownItems.Add(tsm3_3_2);
                }

                图像ToolStripMenuItem.DropDownItems.Add(tsm3_4);


                //******************检测***********************
                ToolStripMenuItem tsm4_1 = new ToolStripMenuItem("工位1检测");
                ToolStripMenuItem tsm4_2 = new ToolStripMenuItem("工位2检测");
                ToolStripMenuItem tsm4_3_1 = new ToolStripMenuItem("工位3_1检测");
                ToolStripMenuItem tsm4_4 = new ToolStripMenuItem("工位4检测");
                ToolStripMenuItem tsm4_3_2 = null;
                tsm4_1.Click += DemoClick;
                tsm4_2.Click += DemoClick;
                tsm4_3_1.Click += DemoClick;
                tsm4_4.Click += DemoClick;
                if (NumDisp >= 5)
                {
                    tsm4_3_2 = new ToolStripMenuItem("工位3_2检测");
                    tsm4_3_2.Click += DemoClick;
                }
                检测ToolStripMenuItem.DropDownItems.Add(tsm4_1);
                检测ToolStripMenuItem.DropDownItems.Add(tsm4_2);
                检测ToolStripMenuItem.DropDownItems.Add(tsm4_3_1);
                if (NumDisp >= 5)
                {
                    检测ToolStripMenuItem.DropDownItems.Add(tsm4_3_2);
                }
                检测ToolStripMenuItem.DropDownItems.Add(tsm4_4);


                //******************帮助***********************
                ToolStripMenuItem tsm5_1 = new ToolStripMenuItem("帮助文档");
                ToolStripMenuItem tsm5_2 = new ToolStripMenuItem("其他项设置");
                tsm5_1.Click += DemoClick;
                tsm5_2.Click += DemoClick;
                帮助ToolStripMenuItem.DropDownItems.Add(tsm5_1);
                帮助ToolStripMenuItem.DropDownItems.Add(tsm5_2);






                ////创建MenuStrip对象
                //  MenuStrip MS = menuStrip1;
                ////创建一个ToolStripMenuItem菜单，可以文本和图片一并添加
                //ToolStripMenuItem tsmi = new ToolStripMenuItem("测试按钮");
                ////绑定菜单的点击事件
                //tsmi.Click += DemoClick;
                ////创建子菜单 可以文本和图片一并添加
                //ToolStripMenuItem tsmi2 = new ToolStripMenuItem("测试子按钮");
                ////绑定子菜单点击事件
                //tsmi2.Click += DemoClick;
                ////添加子菜单
                //tsmi.DropDownItems.Add(tsmi2);
                ////添加主菜单
                //MS.Items.Add(tsmi);
                ////界面显示
                //this.Controls.Add(MS);




            }
            catch (Exception)
            {

                // throw;
            }


        }

        private void DemoClick(object sender, EventArgs e)
        {
            ToolStripMenuItem but = sender as ToolStripMenuItem;
            // MessageBox.Show(but.Text);

            try
            {
                switch (but.Text)
                {
                    case "帮助文档":
                        OpenHelpPDFfile();
                        UpdateLogList("帮助文档打开", p_D_Log);
                        break;

                    case "PLC":
                        if (IsRegister)
                        {
                            PLCsetForm();
                        }
                        else
                        {
                            MessageBox.Show("无权限，请登录");
                        }

                        break;

                    case "登录":
                        Adminstrator1();
                        break;

                    case "相机":
                        if (IsRegister)
                        {
                            PointTech_Camera11();
                        }
                        else
                        {
                            MessageBox.Show("无权限，请登录");
                        }

                        break;

                    case "另存为":
                        if (IsRegister)
                        {
                            FileSetForm();
                        }
                        else
                        {
                            MessageBox.Show("无权限，请登录");
                        }

                        break;

                    case "保存":

                        if (IsRegister)
                        {
                            FileSetForm();
                        }
                        else
                        {
                            MessageBox.Show("无权限，请登录");
                        }

                        break;

                    case "打开":
                        if (IsRegister)
                        {
                            FileSetForm();
                        }
                        else
                        {
                            MessageBox.Show("无权限，请登录");
                        }

                        break;

                    case "新建":
                        if (IsRegister)
                        {
                            FileSetForm();
                        }
                        else
                        {
                            MessageBox.Show("无权限，请登录");
                        }

                        break;

                    //*****************检测——工位*****************
                    case "工位1检测":
                        UpdateLogList("工位1检测", p_D_Log);

                        Detect(m1_Method, m1_Disp);
                        break;

                    case "工位2检测":
                        UpdateLogList("工位2检测", p_D_Log);

                        Detect(m2_Method, m2_Disp);


                        break;

                    case "工位3_1检测":
                        UpdateLogList("工位3_1检测", p_D_Log);

                        Detect(m3_Method, m3_Disp);

                        break;

                    case "工位3_2检测":
                        UpdateLogList("工位3_2检测", p_D_Log);

                        Detect(m4_Method, m4_Disp);


                        break;

                    case "工位4检测":
                        UpdateLogList("工位4检测", p_D_Log);
                        Detect(m5_Method, m5_Disp);

                        break;
                    //*****************设置——工位*****************
                    case "工位1":
                        try
                        {

                            if (!IsRegister)
                            {
                                MessageBox.Show("无权限，请登录");
                                return;
                            }

                            UpdateLogList("工位1设置打开", p_D_Log);
                            Station.StationForm m1_StationForm = new Station.StationForm();
                            m1_StationForm.m_StationMethod = m1_Method;
                            m1_StationForm.m_StationMethod.Para_stationpara = m1_Method.Para_stationpara;
                            m1_StationForm.m_StationMethod.InputImage = m1_Method.InputImage;
                            m1_StationForm.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            UpdateLogList("工位1:" + ex.Message, p_D_Log);


                        }


                        break;

                    case "工位2":
                        try
                        {
                            if (!IsRegister)
                            {
                                MessageBox.Show("无权限，请登录");
                                return;
                            }
                            UpdateLogList("工位2设置打开", p_D_Log);

                            Station.StationForm m2_StationForm = new Station.StationForm();
                            m2_StationForm.m_StationMethod = m2_Method;
                            m2_StationForm.m_StationMethod.Para_stationpara = m2_Method.Para_stationpara;
                            m2_StationForm.m_StationMethod.InputImage = m2_Method.InputImage;

                            m2_StationForm.ShowDialog();

                        }
                        catch (Exception ex)
                        {
                            UpdateLogList("工位2:" + ex.Message, p_D_Log);


                        }

                        break;


                    case "工位3_1":
                        try
                        {
                            if (!IsRegister)
                            {
                                MessageBox.Show("无权限，请登录");
                                return;
                            }
                            UpdateLogList("工位3_1设置打开", p_D_Log);
                            Station.StationForm m3_StationForm = new Station.StationForm();
                            m3_StationForm.m_StationMethod = m3_Method;
                            m3_StationForm.m_StationMethod.Para_stationpara = m3_Method.Para_stationpara;
                            m3_StationForm.m_StationMethod.InputImage = m3_Method.InputImage;
                            m3_StationForm.ShowDialog();

                        }
                        catch (Exception ex)
                        {
                            UpdateLogList("工位3_1:" + ex.Message, p_D_Log);


                        }

                        break;

                    case "工位3_2":
                        try
                        {
                            if (!IsRegister)
                            {
                                MessageBox.Show("无权限，请登录");
                                return;
                            }
                            UpdateLogList("工位3_2设置打开", p_D_Log);

                            Station.StationForm m4_StationForm = new Station.StationForm();
                            m4_StationForm.m_StationMethod = m4_Method;
                            m4_StationForm.m_StationMethod.Para_stationpara = m4_Method.Para_stationpara;
                            m4_StationForm.m_StationMethod.InputImage = m4_Method.InputImage;

                            m4_StationForm.ShowDialog();

                        }
                        catch (Exception ex)
                        {
                            UpdateLogList("工位3_2:" + ex.Message, p_D_Log);
                        }

                        break;

                    case "工位4":
                        try
                        {
                            if (!IsRegister)
                            {
                                MessageBox.Show("无权限，请登录");
                                return;
                            }
                            UpdateLogList("工位4设置打开", p_D_Log);

                            Station.StationForm m5_StationForm = new Station.StationForm();
                            m5_StationForm.m_StationMethod = m5_Method;
                            m5_StationForm.m_StationMethod.Para_stationpara = m5_Method.Para_stationpara;
                            m5_StationForm.m_StationMethod.InputImage = m5_Method.InputImage;

                            m5_StationForm.ShowDialog();

                        }
                        catch (Exception ex)
                        {
                            UpdateLogList("工位4:" + ex.Message, p_D_Log);
                        }

                        break;

                    //*****************图像打开*****************
                    case "工位1打开图像":
                        m1_Method.InputImage = ReadPicture(m1_Disp);
                        break;

                    case "工位2打开图像":
                        m2_Method.InputImage = ReadPicture(m2_Disp);
                        break;
                    case "工位3_1打开图像":
                        m3_Method.InputImage = ReadPicture(m3_Disp);
                        break;
                    case "工位3_2打开图像":
                        m4_Method.InputImage = ReadPicture(m4_Disp);
                        break;
                    case "工位4打开图像":
                        m5_Method.InputImage = ReadPicture(m5_Disp);
                        break;

                    default:
                        break;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("问题：" + ex.ToString());

            }



        }

        #endregion

        #region Menustrip调用函数

        HObject ReadPicture(GVS.HalconDisp.Control.HWindow_Final m_Disp)
        {
            HObject Image = null;
            try
            {
                OpenFileDialog opnDlg = new OpenFileDialog();
                opnDlg.Filter = "所有图像文件 | *.bmp; *.pcx; *.png; *.jpg; *.gif;" +
                    "*.tif; *.ico; *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf";
                opnDlg.Title = "打开图像文件";
                opnDlg.ShowHelp = true;
                opnDlg.Multiselect = false;
                if (opnDlg.ShowDialog() == DialogResult.OK)
                {
                    // 文件夹位置
                    string srFileName = opnDlg.FileName;

                    // 读取、显示图片
                    HOperatorSet.ReadImage(out Image, srFileName);
                    m_Disp.HobjectToHimage(Image);
                    // m1_Method.InputImage = m1_hoImage;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


            return Image;

        }

        void Detect(StationMethod m_StationMethod,
            GVS.HalconDisp.Control.HWindow_Final m_Disp)
        {

            try
            {

                bool bState = m_StationMethod.Run();

                //显示图像
                if (m_StationMethod.Para_stationpara.PositionSelect == "使用定位1")
                {
                    m_Disp.ClearWindow();

                    try
                    {
                        if (m_StationMethod.m_P1CalPreprocessMethod.resultPara.ResultImage != null)
                        {
                            m_Disp.HobjectToHimage(m_StationMethod.m_P1CalPreprocessMethod.resultPara.ResultImage);
                            m_StationMethod.m_P1CalPreprocessMethod.resultPara.ResultImage = null;

                        }
                        else
                        {
                            m_Disp.HobjectToHimage(m_StationMethod.InputImage);

                        }

                    }
                    catch
                    {
                        m_Disp.ClearWindow();
                        m_Disp.HobjectToHimage(m_StationMethod.InputImage);
                    }

                }
                else if (m_StationMethod.Para_stationpara.PositionSelect == "使用定位2")
                {
                    m_Disp.ClearWindow();

                    try
                    {
                        if (m_StationMethod.m_P2CalPreprocessMethod.resultPara.ResultImage != null)
                        {
                            m_Disp.HobjectToHimage(m_StationMethod.m_P2CalPreprocessMethod.resultPara.ResultImage);
                            m_StationMethod.m_P2CalPreprocessMethod.resultPara.ResultImage = null;
                        }
                        else
                        {
                            m_Disp.HobjectToHimage(m_StationMethod.InputImage);
                        }
                    }
                    catch
                    {
                        m_Disp.ClearWindow();
                        m_Disp.HobjectToHimage(m_StationMethod.InputImage);

                    }
                }

                if (m_StationMethod.Para_stationpara.JudgeTCdistance == "需要圆心距检测")
                {

                }

                if (m_StationMethod.Para_stationpara.JudgeState == "需要状态判断")
                {
                    m_Disp.ClearWindow();

                    m_Disp.HobjectToHimage(m_StationMethod.InputImage);
                }

                if (m_StationMethod.Para_stationpara.NeedInkDetect == "需要涂墨检测")
                {

                    m_Disp.ClearWindow();
                    m_Disp.HobjectToHimage(m_StationMethod.m_InkDetectMethod.InputImage);

                }

                if (m_StationMethod.Para_stationpara.NoLensDetect == "需要空盘检测")
                {

                }

                //显示region

                if (m_StationMethod.resultPara.P1IsOK && m_StationMethod.Para_stationpara.PositionSelect == "使用定位1")
                {

                    if (m_StationMethod.Para_stationpara.Position1Select == "圆卡尺")
                    {
                        foreach (var item in m_StationMethod.m_FindCircleMethod1.ListReg)
                        {
                            m_Disp.DispObj(item.HObject);
                        }
                    }
                    else
                    {
                        //显示卡尺region
                        foreach (var item in m_StationMethod.m_ComputeCenterMethod1.ListReg)
                        {
                            m_Disp.DispObj(item.HObject);
                        }
                    }

                    ////显示卡尺region
                    //foreach (var item in m_StationMethod.m_ComputeCenterMethod1.ListReg)
                    //{
                    //    m_Disp.DispObj(item.HObject);
                    //}


                }
                else if (m_StationMethod.resultPara.P2IsOK && m_StationMethod.Para_stationpara.PositionSelect == "使用定位2")
                {

                    if (m_StationMethod.Para_stationpara.Position2Select == "圆卡尺")
                    {
                        foreach (var item in m_StationMethod.m_FindCircleMethod2.ListReg)
                        {
                            m_Disp.DispObj(item.HObject);
                        }
                    }
                    else
                    {

                        //显示卡尺region
                        foreach (var item in m_StationMethod.m_ComputeCenterMethod2.ListReg)
                        {
                            m_Disp.DispObj(item.HObject);
                        }

                    }
                }

                if (m_StationMethod.Para_stationpara.JudgeTCdistance == "需要圆心距检测")
                {

                }

                if (m_StationMethod.Para_stationpara.JudgeState == "需要状态判断")
                {

                    if ((m_StationMethod.Para_stationpara.JudgeStatePositionSelect == "使用定位1" && m_StationMethod.resultPara.P1IsOK)
                        || (m_StationMethod.Para_stationpara.JudgeStatePositionSelect == "使用定位2" && m_StationMethod.resultPara.P2IsOK))
                    {

                        if (m_StationMethod.Para_stationpara.JudgeStateBy == "使用SVM")
                        {

                            m_Disp.DispObj(m_StationMethod.m_svmMethod.resultPara.RegionThr,
                       m_StationMethod.m_svmMethod.resultPara.ClassColor);
                            m_Disp.DispObj(m_StationMethod.m_svmMethod.resultPara.RegionXLD);
                            //m_Disp.DisplayMessage("结果：" + m_StationMethod.m_svmMethod.resultPara.ClassName, 1, 1, "red", false);
                            //m_Disp.DisplayMessage("序号：" + m_StationMethod.m_svmMethod.resultPara.ClassSerial, 60, 1, "red", false);


                        }
                        else
                        {

                            //使用Blob
                            m_Disp.DispObj(m_StationMethod.m_blobMethod.resultPara.RegionThr,
                             "red", "fill");
                            m_Disp.DispObj(m_StationMethod.m_blobMethod.resultPara.RegionXLD);

                            //m_Disp.DisplayMessage("结果：" + m_StationMethod.m_blobMethod.resultPara.ClassName, 1, 1, "red", false);
                            //m_Disp.DisplayMessage("序号：" + m_StationMethod.m_blobMethod.resultPara.ClassSerial, 60, 1, "red", false);
                            //m_Disp.DisplayMessage("面积：" + m_StationMethod.m_blobMethod.resultPara.Area, 110, 1, "red", false);



                        }

                    }




                }

                if (m_StationMethod.Para_stationpara.NeedInkDetect == "需要涂墨检测")
                {

                    if ((m_StationMethod.Para_stationpara.InkDetectStatePositionSelect == "使用定位1" && m_StationMethod.resultPara.P1IsOK)
                       || (m_StationMethod.Para_stationpara.InkDetectStatePositionSelect == "使用定位2" && m_StationMethod.resultPara.P2IsOK))
                    {
                        int qww = 0;
                        foreach (var item in m_StationMethod.m_InkDetectMethod.resultPara.Area)
                        {
                            if (m_StationMethod.m_InkDetectMethod.Para_InkDetectPara.FillSelect == "填充")
                            {
                                if (m_StationMethod.m_InkDetectMethod.Para_InkDetectPara.AreaSelect[qww] == "最大面积")
                                {
                                    if (m_StationMethod.m_InkDetectMethod.resultPara.RegionThr[qww] != null)
                                    {
                                        m_Disp.DispObj(m_StationMethod.m_InkDetectMethod.resultPara.RegionThr[qww], "yellow", "fill");
                                    }
                                    if (m_StationMethod.m_InkDetectMethod.resultPara.RegionMax[qww] != null)
                                    {
                                        m_Disp.DispObj(m_StationMethod.m_InkDetectMethod.resultPara.RegionMax[qww], "red", "fill");

                                    }

                                }
                                else
                                {
                                    if (m_StationMethod.m_InkDetectMethod.resultPara.RegionThr[qww] != null)
                                    {
                                        m_Disp.DispObj(m_StationMethod.m_InkDetectMethod.resultPara.RegionThr[qww], "red", "fill");
                                    }

                                }
                            }
                            else
                            {


                                if (m_StationMethod.m_InkDetectMethod.Para_InkDetectPara.AreaSelect[qww] == "最大面积")
                                {
                                    if (m_StationMethod.m_InkDetectMethod.resultPara.RegionThr[qww] != null)
                                    {
                                        m_Disp.DispObj(m_StationMethod.m_InkDetectMethod.resultPara.RegionThr[qww], "yellow");
                                    }
                                    if (m_StationMethod.m_InkDetectMethod.resultPara.RegionMax[qww] != null)
                                    {
                                        m_Disp.DispObj(m_StationMethod.m_InkDetectMethod.resultPara.RegionMax[qww], "red");

                                    }



                                }
                                else
                                {
                                    if (m_StationMethod.m_InkDetectMethod.resultPara.RegionThr[qww] != null)
                                    {
                                        m_Disp.DispObj(m_StationMethod.m_InkDetectMethod.resultPara.RegionThr[qww], "red");
                                    }

                                }





                            }

                            if (m_StationMethod.m_InkDetectMethod.resultPara.RegionXLD[qww] != null)
                            {
                                m_Disp.DispObj(m_StationMethod.m_InkDetectMethod.resultPara.RegionXLD[qww], "green");
                            }

                            qww++;
                        }

                    }


                }

                if (m_StationMethod.Para_stationpara.NoLensDetect == "需要空盘检测")
                {
                    //if ((m_StationMethod.Para_stationpara.NullDetectPositionSelect == "使用定位1" && m_StationMethod.resultPara.P1IsOK)
                    //  ||(m_StationMethod.Para_stationpara.NullDetectPositionSelect == "使用定位2" && m_StationMethod.resultPara.P2IsOK))
                    //{ 

                    //}

                }

                //显示文字表达
                HTuple Height = new HTuple();
                HTuple Width = new HTuple();
                HOperatorSet.GetImageSize(m_StationMethod.InputImage, out Width, out Height);
                int hIndex = (int)(Height[0].D / 24.0);
                int num = 1;

                m_Disp.DisplayMessage("总结果:" + (m_StationMethod.resultPara.AllIsOK ? "OK" : "NG")
                    +
                    "，发送序号:" + m_StationMethod.resultPara.PLC_Serial, 1, 1, "red", true);

                if (m_StationMethod.resultPara.P1IsOK && m_StationMethod.Para_stationpara.PositionSelect == "使用定位1")
                {
                    m_Disp.DisplayMessage("实际偏差ΔX,ΔY:" + m_StationMethod.resultPara.DeltaX.ToString("f3") + ","
                  + m_StationMethod.resultPara.DeltaY.ToString("f3"), hIndex * num, 1, "red", true);
                    num++;
                }
                else if (m_StationMethod.resultPara.P2IsOK && m_StationMethod.Para_stationpara.PositionSelect == "使用定位2")
                {

                    m_Disp.DisplayMessage("实际偏差ΔX,ΔY:" + m_StationMethod.resultPara.DeltaX.ToString("f3") + ","
                     + m_StationMethod.resultPara.DeltaY.ToString("f3"), hIndex * num, 1, "red", true);
                    num++;

                }

                if (m_StationMethod.Para_stationpara.JudgeTCdistance == "需要圆心距检测")
                {
                    if (m_StationMethod.Para_stationpara.JudgeTCby == "像素判断")
                    {
                        m_Disp.DisplayMessage("像素判断:" + m_StationMethod.resultPara.TCIsOK + ",圆心距离:"
                            + m_StationMethod.resultPara.TCPixelsdistance.ToString("f3") + "Pixels", hIndex * num, 1, "red", true);
                        num++;

                    }
                    else
                    {

                        m_Disp.DisplayMessage("实际距离判断:" + m_StationMethod.resultPara.TCIsOK + ",圆心距离:"
                           + m_StationMethod.resultPara.TCTruedistance.ToString("f3") + "Pixels", hIndex * num, 1, "red", true);
                        num++;
                    }

                }

                if (m_StationMethod.Para_stationpara.JudgeState == "需要状态判断")
                {

                    if ((m_StationMethod.Para_stationpara.JudgeStatePositionSelect == "使用定位1" && m_StationMethod.resultPara.P1IsOK)
                     || (m_StationMethod.Para_stationpara.JudgeStatePositionSelect == "使用定位2" && m_StationMethod.resultPara.P2IsOK))
                    {

                        if (m_StationMethod.Para_stationpara.JudgeStateBy == "使用SVM")
                        {

                            m_Disp.DisplayMessage("状态结果：" + m_StationMethod.m_svmMethod.resultPara.ClassName, num * hIndex, 1, "red", true);
                            num++;
                            m_Disp.DisplayMessage("输出序号：" + m_StationMethod.m_svmMethod.resultPara.ClassSerial, num * hIndex, 1, "red", true);
                            num++;

                        }
                        else
                        {
                            //使用Blob
                            m_Disp.DisplayMessage("状态结果：" + m_StationMethod.m_blobMethod.resultPara.ClassName, num * hIndex, 1, "red", true);
                            num++;
                            m_Disp.DisplayMessage("输出序号：" + m_StationMethod.m_blobMethod.resultPara.ClassSerial, num * hIndex, 1, "red", true);
                            num++;
                            m_Disp.DisplayMessage("状态判断面积：" + m_StationMethod.m_blobMethod.resultPara.Area, num * hIndex, 1, "red", true);
                            num++;

                        }

                    }

                }


                if (m_StationMethod.Para_stationpara.NeedInkDetect == "需要涂墨检测")
                {

                    if ((m_StationMethod.Para_stationpara.InkDetectStatePositionSelect == "使用定位1" && m_StationMethod.resultPara.P1IsOK)
                       || (m_StationMethod.Para_stationpara.InkDetectStatePositionSelect == "使用定位2" && m_StationMethod.resultPara.P2IsOK))
                    {

                        m_Disp.DisplayMessage("涂墨结果：" + (m_StationMethod.resultPara.IsInkDetectOK ? "OK" : "NG") + "。True为OK,False为NG",
                            num * hIndex, 1, "red", true);
                        num++;

                        int qw1 = 0;
                        foreach (var item in m_StationMethod.m_InkDetectMethod.resultPara.Area)
                        {
                            m_Disp.DisplayMessage("环形圈序号：" + (qw1 + 1).ToString() + "，检测面积：" + item.ToString() + "，比较面积：" + m_StationMethod.m_InkDetectMethod.Para_InkDetectPara.Area[qw1].ToString()
                              + ",该层：" + m_StationMethod.m_InkDetectMethod.resultPara.ResultEachRing[qw1].ToString(), num * hIndex, 1, "red", true);
                            qw1++; num++;
                        }


                    }


                }

                if (m_StationMethod.Para_stationpara.NoLensDetect == "需要空盘检测")
                {
                    if ((m_StationMethod.Para_stationpara.NullDetectPositionSelect == "使用定位1" && m_StationMethod.resultPara.P1IsOK)
                     || (m_StationMethod.Para_stationpara.NullDetectPositionSelect == "使用定位2" && m_StationMethod.resultPara.P2IsOK))
                    {
                        m_Disp.DisplayMessage("空盘：" + m_StationMethod.resultPara.IsNull.ToString(), num * hIndex, 1, "red", true);
                        num++;
                    }

                }

            }
            catch (Exception ex)
            {

                string dasad = ex.ToString();
                MessageBox.Show(dasad);
            }




        }


        #endregion

        #region 主界面生成

        void InitControlShape2()
        {
            try
            {

                //**************************************
                this.WindowState = FormWindowState.Maximized;

                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;



                //int width = this.panel1.Width;
                //int height = this.panel1.Height;

                int secHeight = (int)height / 10;
                int secWidth = (int)width / 10;

                this.panel6.Size = new Size(Convert.ToInt32(secWidth * 0.38), Convert.ToInt32(secHeight * 9.14));
                this.panel6.Location = new Point(Convert.ToInt32(secWidth * 0.01), Convert.ToInt32(secHeight * 0.02));


                this.panel2.Size = new Size(Convert.ToInt32(secWidth * 9.58), Convert.ToInt32(secHeight * 7.71));
                this.panel2.Location = new Point(Convert.ToInt32(secWidth * 0.395), Convert.ToInt32(secHeight * 0.02));

                this.panel3.Size = new Size(Convert.ToInt32(secWidth * 1.9416), Convert.ToInt32(secHeight * 1.2));
                this.panel3.Location = new Point(Convert.ToInt32(secWidth * 0.395), Convert.ToInt32(secHeight * 7.72));


                this.panel4.Size = new Size(Convert.ToInt32(secWidth * 1.9416), Convert.ToInt32(secHeight * 1.2));
                this.panel4.Location = new Point(Convert.ToInt32(secWidth * 2.35), Convert.ToInt32(secHeight * 7.72));


                this.panel5.Size = new Size(Convert.ToInt32(secWidth * 5.68), Convert.ToInt32(secHeight * 1.42));
                this.panel5.Location = new Point(Convert.ToInt32(secWidth * 4.305), Convert.ToInt32(secHeight * 7.72));

                this.statusStrip1.Location = new Point(Convert.ToInt32(secWidth * 0.395), Convert.ToInt32(secHeight * 8.93));

                //**************************************
                if (NumDisp <= 4)
                {
                    this.Text = "BI200";
                }
                else
                {
                    this.Text = "BI300";
                }

                this.Icon = new Icon(@"../../icon2/圆环.ico");


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.PadRight(30, ' '),
                //    "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        void InitControlShape2R()
        {
            try
            {
                //this.WindowState = FormWindowState.Maximized;

                //int width = Screen.PrimaryScreen.Bounds.Width;
                //int height = Screen.PrimaryScreen.Bounds.Height;

                return;

                int width = this.Width + 25;
                int height = this.Height + 25;

                int secHeight = (int)height / 6;
                int secWidth = (int)width / 6;



                this.panel6.Size = new Size(Convert.ToInt32(secWidth * 0.17), Convert.ToInt32(secHeight * 5.32));
                this.panel6.Location = new Point(Convert.ToInt32(secWidth * 0.01), Convert.ToInt32(secHeight * 0.02));


                this.panel2.Size = new Size(Convert.ToInt32(secWidth * 5.775), Convert.ToInt32(secHeight * 4.3));
                this.panel2.Location = new Point(Convert.ToInt32(secWidth * 0.2), Convert.ToInt32(secHeight * 0.02));


                this.panel3.Size = new Size(Convert.ToInt32(secWidth * 1), Convert.ToInt32(secHeight * 0.8));
                this.panel3.Location = new Point(Convert.ToInt32(secWidth * 0.20), Convert.ToInt32(secHeight * 4.35));


                this.panel4.Size = new Size(Convert.ToInt32(secWidth * 1.5), Convert.ToInt32(secHeight * 0.8));
                this.panel4.Location = new Point(Convert.ToInt32(secWidth * 1.215), Convert.ToInt32(secHeight * 4.35));


                this.panel5.Size = new Size(Convert.ToInt32(secWidth * 3.25), Convert.ToInt32(secHeight * 0.8));
                this.panel5.Location = new Point(Convert.ToInt32(secWidth * 2.725), Convert.ToInt32(secHeight * 4.35));


                this.statusStrip1.Location = new Point(Convert.ToInt32(secWidth * 0.20), Convert.ToInt32(secHeight * 5.17));

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.PadRight(30, ' '),
                //    "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            InitControlShape2R();
        }

        #endregion

        #region 显示窗口

        private GVS.HalconDisp.Control.HWindow_Final m1_Disp = new GVS.HalconDisp.Control.HWindow_Final();
        private GVS.HalconDisp.Control.HWindow_Final m2_Disp = new GVS.HalconDisp.Control.HWindow_Final();
        private GVS.HalconDisp.Control.HWindow_Final m3_Disp = new GVS.HalconDisp.Control.HWindow_Final();
        private GVS.HalconDisp.Control.HWindow_Final m4_Disp = new GVS.HalconDisp.Control.HWindow_Final();//3_2
        private GVS.HalconDisp.Control.HWindow_Final m5_Disp = new GVS.HalconDisp.Control.HWindow_Final();
        private GVS.HalconDisp.Control.HWindow_Final m6_Disp = new GVS.HalconDisp.Control.HWindow_Final();
        private GVS.HalconDisp.Control.HWindow_Final m7_Disp = new GVS.HalconDisp.Control.HWindow_Final();
        private GVS.HalconDisp.Control.HWindow_Final m8_Disp = new GVS.HalconDisp.Control.HWindow_Final();

        List<GVS.HalconDisp.Control.HWindow_Final> m_Disp_List = new List<GVS.HalconDisp.Control.HWindow_Final>();



        void dispSet(int num)
        {
            if (num <= 4)
            {
                num = 4;
            }
            if (num >= 5)
            {
                num = 5;
            }
            try
            {
                switch (num)
                {
                    case 4:
                        tabLayoutSet2(panel2, 2, 2);

                        break;

                    case 5:
                        tabLayoutSet2(panel2, 2, 3);

                        break;


                    default:
                        break;
                }


            }
            catch (Exception)
            {


            }

        }

        void tabLayoutSet2(Panel panel1, int rows, int cols)
        {

            try
            {

                TableLayoutPanel table = new TableLayoutPanel();
                table.Dock = DockStyle.Fill;     //顶部填充
                panel1.Controls.Add(table);
                table.ColumnCount = cols;
                table.RowCount = rows;

                //计算每个小框的宽
                double sda = 1 * 1.0 / cols * 1.0;
                float dsadsadsada = (float)sda;
                float tableWidth = dsadsadsada * table.Width - 2;

                float h1 = (float)(1.0 / rows);
                float tableHeight = h1 * table.Height - 22;

                for (int i = 0; i < cols; i++)
                {

                    //table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, tableWidth));    //利用百分比计算，0.2f表示占用本行长度的20%
                    table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, tableWidth));    //利用百分比计算，0.2f表示占用本行长度的20%
                    table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, (float)0.5));    //利用百分比计算，0.2f表示占用本行长度的20%

                }

                int f1 = 0;
                for (int j1 = 0; j1 < rows; j1++)//行
                {
                    for (int i1 = 0; i1 < cols; i1++)//列
                    {
                        m_Disp_List[f1].Dock = DockStyle.Fill;
                        table.Controls.Add(m_Disp_List[f1], i1, j1);
                        f1++;
                    }

                }






            }
            catch (Exception)
            {


            }

        }

        #endregion

        #region 左侧快捷键界面

        PictureBox pb1 = null;
        PictureBox pb2 = null;
        PictureBox pb3 = null;
        PictureBox pb4 = null;
        PictureBox pb5 = null;
        PictureBox pb6 = null;
        PictureBox pb7 = null;
        PictureBox pb8 = null;
        PictureBox pb9 = null;
        void tabLayoutSet(Panel panel1, TableLayoutPanel table, int rows, int cols)
        {
            try
            {

                table.Dock = DockStyle.Top;     //顶部填充
                panel1.Controls.Add(table);
                table.ColumnCount = cols;
                table.RowCount = rows;

                //计算每个小框的宽
                double sda = 1 * 1.0 / cols * 1.0;
                float dsadsadsada = (float)sda;
                float tableWidth = dsadsadsada * table.Width - 2;

                for (int i = 0; i < cols; i++)
                {

                    table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, tableWidth));    //利用百分比计算，0.2f表示占用本行长度的20%

                }

                // 动态添加一行

                //设置高度,边框线也算高度，所以将40修改大一点
                int singleBoxHeight = 42;
                table.Height = table.RowCount * singleBoxHeight;
                // 行高
                table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, singleBoxHeight - 4));
                // 设置cell样式，增加线条
                table.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetPartial;

                int dsq = 0;

                int slbW = 33;
                int slbH = 32;
                //************************************
                //  PictureBox pb1 = new PictureBox();
                pb1 = new PictureBox();
                pb1.Width = slbW;
                pb1.Height = slbH;
                pb1.Click += pb1_Click;
                pb1.Tag = "开始";
                Image img1 = Image.FromFile(@"../../icon2/开始.png");
                pb1.BackgroundImage = img1;
                table.Controls.Add(pb1, 0, dsq);
                dsq++;
                //************************************
                pb2 = new PictureBox();
                pb2.Width = slbW;
                pb2.Height = slbH;
                pb2.Click += pb1_Click;
                pb2.Tag = "暂停";
                Image img2 = Image.FromFile(@"../../icon2/暂停.png");
                pb2.BackgroundImage = img2;
                table.Controls.Add(pb2, 0, dsq);
                dsq++;
                //************************************
                pb3 = new PictureBox();
                pb3.Width = slbW;
                pb3.Height = slbH;
                pb3.Click += pb1_Click;
                pb3.Tag = "新建";
                Image img3 = Image.FromFile(@"../../icon2/新建.png");
                pb3.BackgroundImage = img3;
                table.Controls.Add(pb3, 0, dsq);
                dsq++;
                //**********************************
                pb4 = new PictureBox();

                pb4.Width = slbW;
                pb4.Height = slbH;
                //   pb4.Dock = DockStyle.Fill;
                pb4.Click += pb1_Click;
                pb4.Tag = "保存";
                Image img4 = Image.FromFile(@"../../icon2/保存.png");
                pb4.BackgroundImage = img4;
                table.Controls.Add(pb4, 0, dsq);
                dsq++;
                //**********************************
                pb5 = new PictureBox();
                pb5.Width = slbW;
                pb5.Height = slbH;
                pb5.Click += pb1_Click;
                pb5.Tag = "用户";
                Image img5 = Image.FromFile(@"../../icon2/用户.png");
                pb5.BackgroundImage = img5;
                table.Controls.Add(pb5, 0, dsq);
                dsq++;
                //**********************************
                pb6 = new PictureBox();
                pb6.Width = slbW;
                pb6.Height = slbH;
                pb6.Click += pb1_Click;
                pb6.Tag = "刷新";
                Image img6 = Image.FromFile(@"../../icon2/刷新.png");
                pb6.BackgroundImage = img6;
                table.Controls.Add(pb6, 0, dsq);
                dsq++;
                //***********************************************
                pb7 = new PictureBox();
                pb7.Width = slbW;
                pb7.Height = slbH;
                pb7.Click += pb1_Click;
                pb7.Tag = "相机";
                Image img7 = Image.FromFile(@"../../icon2/相机.png");
                pb7.BackgroundImage = img7;
                table.Controls.Add(pb7, 0, dsq);
                dsq++;
                //*************************************
                pb8 = new PictureBox();
                pb8.Width = slbW;
                pb8.Height = slbH;
                pb8.Click += pb1_Click;
                pb8.Tag = "调试";
                Image img8 = Image.FromFile(@"../../icon2/调试.png");
                pb8.BackgroundImage = img8;
                table.Controls.Add(pb8, 0, dsq);
                dsq++;
                //*************************************
                pb9 = new PictureBox();
                pb9.Width = slbW;
                pb9.Height = slbH;
                // pb9.Dock = DockStyle.Fill;
                pb9.Click += pb1_Click;
                pb9.Tag = "通信";
                Image img9 = Image.FromFile(@"../../icon2/通信-8.png");
                pb9.BackgroundImage = img9;
                table.Controls.Add(pb9, 0, dsq);
                dsq++;
            }
            catch (Exception)
            {

                // throw;
            }




        }

        private void pb1_Click(object sender, EventArgs e)
        {

            PictureBox pb = (PictureBox)sender;

            string ttaa = (string)pb.Tag;
            switch (ttaa)
            {
                case "开始":
                    //MessageBox.Show(ttaa);
                    Start();
                    break;

                case "暂停":
                    // MessageBox.Show(ttaa);
                    Finish();
                    break;

                case "新建":
                    if (IsRegister)
                    {
                        FileSetForm();
                    }
                    else
                    {
                        MessageBox.Show("无权限，请登录");
                    }

                    // MessageBox.Show(ttaa);
                    break;

                case "保存":
                    //  MessageBox.Show(ttaa);
                    SaveDataAll();
                    UpdateLogList("保存文件", p_D_Log);
                    break;


                case "相机":
                    if (IsRegister)
                    {
                        PointTech_Camera11();
                    }
                    else
                    {
                        MessageBox.Show("无权限，请登录");
                    }


                    break;


                case "刷新":

                    if (IsRegister)
                    {
                        DataClear();
                    }
                    else
                    {
                        MessageBox.Show("无权限，请登录");
                    }
                    //  MessageBox.Show(ttaa);
                    break;

                case "用户":
                    //MessageBox.Show(ttaa);
                    Adminstrator1();
                    break;


                case "调试":
                    test();
                    break;


                case "通信":
                    // test();
                    // MessageBox.Show(ttaa);
                    if (IsRegister)
                    {
                        PLCsetForm();
                    }
                    else
                    {
                        MessageBox.Show("无权限，请登录");
                    }

                    break;


                default:
                    break;
            }



        }



        #endregion

        #region 函数  左侧快捷键界面

        void Adminstrator1()
        {
            try
            {

                if (IsRegister)//已登录
                {

                    //判断是否需要登出 
                    if (MessageBox.Show("已登录，是否退出登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.OK)
                    {
                        IsRegister = false;
                        toolStripStatusLabel3.Text = "user";
                        UpdateLogList("权限登出", p_D_Log);

                    }


                }
                else//未登录
                {

                    Administrator Form_Administrator = new Administrator();
                    Form_Administrator.ShowDialog(this);
                    if (IsRegister)
                    {
                        toolStripStatusLabel3.Text = "Adminstrator";
                        MessageBox.Show("权限登录成功");
                        UpdateLogList("权限登录成功", p_D_Log);

                    }
                    else
                    {
                        toolStripStatusLabel3.Text = "user";
                        MessageBox.Show("权限登录失败");
                        UpdateLogList("权限登录失败", p_D_Log);

                    }
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }




        }


        #endregion

        #region 运行线程相关

        Thread CCD1_Run_thread;
        Thread CCD2_Run_thread;
        Thread CCD3_Run_thread;
        Thread CCD4_Run_thread;
        Thread CCD5_Run_thread;

        void OpenRunThread()
        {
            try
            {

                CCD1_Run_thread = new Thread(Image_Process_ccd1);
                CCD2_Run_thread = new Thread(Image_Process_ccd2);
                CCD3_Run_thread = new Thread(Image_Process_ccd3);
                CCD5_Run_thread = new Thread(Image_Process_ccd5);

                CCD1_Run_thread.IsBackground = true;
                CCD2_Run_thread.IsBackground = true;
                CCD3_Run_thread.IsBackground = true;
                CCD5_Run_thread.IsBackground = true;

                //启动线程
                CCD1_Run_thread.Start();
                CCD2_Run_thread.Start();
                CCD3_Run_thread.Start();
                CCD5_Run_thread.Start();

                if (NumDisp >= 5)
                {
                    CCD4_Run_thread = new Thread(Image_Process_ccd4);
                    CCD4_Run_thread.IsBackground = true;
                    CCD4_Run_thread.Start();

                }

            }
            catch
            {
                MessageBox.Show("启动失败");
            }


        }

        #endregion

        #region *****************运行函数加进线程*****************

        int sleepTime = 20;
        string FreeChange = string.Empty;

        void Image_Process_ccd1()
        {

            while (true)
            {
                if (Image_ccd1_Run != null)
                {

                    try
                    {
                        m1_Method.InputImage = Image_ccd1_Run;
                        Detect(m1_Method, m1_Disp);

                        //图像保存
                        RunImageSave(1, m1_Method.resultPara.AllIsOK, Image_ccd1_Run, m1_Disp);

                        //dagridView 显示
                        datagridViewRun_DataCal(m1_Method.resultPara, 1);


                        int x = (int)(m1_Method.resultPara.DeltaX * 1000);
                        int y = (int)(m1_Method.resultPara.DeltaY * 1000);
                        string flag = m1_Method.resultPara.PLC_Serial;

                        string[] rst = new string[3];
                        rst[0] = x.ToString();
                        rst[1] = y.ToString();
                        rst[2] = flag;
                        //发送数据
                        if (Para_MainPara.PLCSelect == "欧姆龙")
                        {
                            OMR_Run_Send(rst[0], rst[1], rst[2], P_CCD1_Run_omr);
                        }
                        else
                        {
                            KV_Run_Send(rst[0], rst[1],"0", rst[2], P_CCD1_Run_kv);
                        }

                        Image_ccd1_Run.Dispose();
                        Image_ccd1_Run = null;
                    }
                    catch (Exception ex)
                    {
                        UpdateLogList("工位1运行出错:" + ex.Message, p_D_Log);

                    }


                }



                Thread.Sleep(sleepTime);

            }
        }

        void Image_Process_ccd2()
        {
            while (true)
            {
                if (Image_ccd2_Run != null)
                {
                    try
                    {
                        m2_Method.InputImage = Image_ccd2_Run;
                        Detect(m2_Method, m2_Disp);
                        //图像保存
                        RunImageSave(2, m2_Method.resultPara.AllIsOK, Image_ccd2_Run, m2_Disp);


                        //dagridView 显示
                        datagridViewRun_DataCal(m2_Method.resultPara, 2);

                        int x = (int)(m2_Method.resultPara.DeltaX * 1000);
                        int y = (int)(m2_Method.resultPara.DeltaY * 1000);
                        string flag = m2_Method.resultPara.PLC_Serial;

                        string[] rst = new string[3];
                        rst[0] = x.ToString();
                        rst[1] = y.ToString();
                        rst[2] = flag;
                        //发送数据
                        if (Para_MainPara.PLCSelect == "欧姆龙")
                        {
                            OMR_Run_Send(rst[0], rst[1], rst[2], P_CCD2_Run_omr);
                        }
                        else
                        {
                            KV_Run_Send(rst[0], rst[1], "0", rst[2], P_CCD2_Run_kv);
                        }

                        Image_ccd2_Run.Dispose();
                        Image_ccd2_Run = null;
                    }
                    catch (Exception ex)
                    {
                        UpdateLogList("工位2运行出错:" + ex.Message, p_D_Log);

                    }

                }

                Thread.Sleep(sleepTime);

            }
        }

        //3-1  四个相机时就用这个
        void Image_Process_ccd3()
        {
            while (true)
            {
                if (Image_ccd3_Run != null)
                {
                    try
                    {

                        m3_Method.InputImage = Image_ccd3_Run;
                        Detect(m3_Method, m3_Disp);
                        //图像保存
                        RunImageSave(3, m3_Method.resultPara.AllIsOK, Image_ccd3_Run, m3_Disp);

                        //dagridView 显示
                        datagridViewRun_DataCal(m3_Method.resultPara, 3);
                        int x = (int)(m3_Method.resultPara.DeltaX * 1000);
                        int y = (int)(m3_Method.resultPara.DeltaY * 1000);
                        string flag = m3_Method.resultPara.PLC_Serial;

                        string[] rst = new string[3];
                        rst[0] = x.ToString();
                        rst[1] = y.ToString();
                        rst[2] = flag;
                        //发送数据
                        if (Para_MainPara.PLCSelect == "欧姆龙")
                        {
                            OMR_Run_Send(rst[0], rst[1], rst[2], P_CCD3_Run_omr);
                        }
                        else
                        {
                            KV_Run_Send(rst[0], rst[1], "0", rst[2], P_CCD3_Run_kv);
                        }

                        Image_ccd3_Run.Dispose();
                        Image_ccd3_Run = null;

                    }
                    catch (Exception ex)
                    {
                        UpdateLogList("工位3_1运行出错:" + ex.Message, p_D_Log);

                    }


                }

                Thread.Sleep(sleepTime);

            }
        }

        //3-2 第三工位第二个相机用这个
        void Image_Process_ccd4()
        {
            while (true)
            {
                if (Image_ccd4_Run != null)
                {
                    try
                    {

                        m4_Method.InputImage = Image_ccd4_Run;
                        Detect(m4_Method, m4_Disp);
                        //图像保存
                        RunImageSave(4, m4_Method.resultPara.AllIsOK, Image_ccd4_Run, m4_Disp);

                        
                        //dagridView 显示
                        datagridViewRun_DataCal(m4_Method.resultPara, 4);

                        int x = (int)(m4_Method.resultPara.DeltaX * 1000);
                        int y = (int)(m4_Method.resultPara.DeltaY * 1000);
                        string flag = m4_Method.resultPara.PLC_Serial;

                        string[] rst = new string[3];
                        rst[0] = x.ToString();
                        rst[1] = y.ToString();
                        rst[2] = flag;
                        //发送数据
                        if (Para_MainPara.PLCSelect == "欧姆龙")
                        {
                            OMR_Run_Send(rst[0], rst[1], rst[2], P_CCD4_Run_omr);
                        }
                        else
                        {
                            KV_Run_Send(rst[0], rst[1], "0", rst[2], P_CCD4_Run_kv);
                        }

                        Image_ccd4_Run.Dispose();
                        Image_ccd4_Run = null;

                    }
                    catch (Exception ex)
                    {
                       // UpdateLogList("工位3_2运行出错:" + ex.Message, p_D_Log);
                        UpdateLogList("工位3_2运行出错:" + ex.ToString(), p_D_Log);

                    }



                }

                Thread.Sleep(sleepTime);

            }
        }

        //第四工位
        void Image_Process_ccd5()
        {
            while (true)
            {
                if (Image_ccd5_Run != null)
                {

                    try
                    {

                        m5_Method.InputImage = Image_ccd5_Run;
                        Detect(m5_Method, m5_Disp);
                        //图像保存
                        RunImageSave(5, m5_Method.resultPara.AllIsOK, Image_ccd5_Run, m5_Disp);

                        //dagridView 显示
                        datagridViewRun_DataCal(m5_Method.resultPara, 5);
                        int x = (int)(m5_Method.resultPara.DeltaX * 1000);
                        int y = (int)(m5_Method.resultPara.DeltaY * 1000);
                        string flag = m5_Method.resultPara.PLC_Serial;

                        string[] rst = new string[3];
                        rst[0] = x.ToString();
                        rst[1] = y.ToString();
                        //rst[2] = flag;

                        //0611  PLC 对于标志位的判断：1 吸取， 2 吸取，抛料， 3 跳过， 4和5 停止
                        if (flag != "1")
                        {
                            rst[2] = "2";
                        }
                        else
                        {
                            rst[2] = "1";
                        }

                        //发送数据
                        if (Para_MainPara.PLCSelect == "欧姆龙")
                        {
                            OMR_Run_Send(rst[0], rst[1], rst[2], P_CCD5_Run_omr);
                        }
                        else
                        {
                            KV_Run_Send(rst[0], rst[1], "0", rst[2], P_CCD5_Run_kv);
                        }

                        Image_ccd5_Run.Dispose();
                        Image_ccd5_Run = null;

                    }
                    catch (Exception ex)
                    {
                        UpdateLogList("工位4运行出错:" + ex.Message, p_D_Log);

                    }


                }

                Thread.Sleep(sleepTime);

            }
        }

        #endregion

        #region 相机设置示教子界面

        //注册事件
        //发送给子界面事件
        public event EventHandler SendMsgToSetting_Event;

        private void Form_PointTech_Camera_SendMsgToForm1_Event(object sender, EventArgs e)
        {

            //取得子界面参数
            Para_MainPara = e as MainPara;

            // int dasdsa = Para_M_DispenserPara.expose1;
        }

        PointTech_Camera Form_PointTech_Camera;

        void PointTech_Camera11()
        {


            if (Form_PointTech_Camera == null)
            {

                Form_PointTech_Camera = new PointTech_Camera();


                //注册事件  子界面向主界面传值
                Form_PointTech_Camera.SendMsgToForm1_Event += Form_PointTech_Camera_SendMsgToForm1_Event;

                //注册事件  主界面向子界面传值
                SendMsgToSetting_Event += Form_PointTech_Camera.EventFromForm1Chaned;
                SendMsgToSetting_Event(null, Para_MainPara);

                Form_PointTech_Camera.StartPosition = FormStartPosition.CenterScreen;
                // Form_PointTech_Camera.ShowDialog(this);
                Form_PointTech_Camera.Show(this);
                // UpdateLogList("打开试教及设置界面", p_D_Log);

            }
            else
            {
                if (Form_PointTech_Camera.IsDisposed)
                {
                    Form_PointTech_Camera = new PointTech_Camera();

                    Form_PointTech_Camera.SendMsgToForm1_Event += Form_PointTech_Camera_SendMsgToForm1_Event;

                    SendMsgToSetting_Event += Form_PointTech_Camera.EventFromForm1Chaned;
                    SendMsgToSetting_Event(null, Para_MainPara);


                    Form_PointTech_Camera.StartPosition = FormStartPosition.CenterScreen;
                    // Form_PointTech_Camera.ShowDialog(this);
                    Form_PointTech_Camera.Show(this);
                    //  UpdateLogList("打开试教及设置界面", p_D_Log);

                }
                else
                {


                    Form_PointTech_Camera.SendMsgToForm1_Event += Form_PointTech_Camera_SendMsgToForm1_Event;

                    SendMsgToSetting_Event += Form_PointTech_Camera.EventFromForm1Chaned;

                    SendMsgToSetting_Event(null, Para_MainPara);

                    Form_PointTech_Camera.WindowState = FormWindowState.Normal;

                    Form_PointTech_Camera.Activate();

                    // Form_PointTech_Camera.ShowDialog(this);
                    // Form_PointTech_Camera.Show(this);


                }
            }


        }


        #endregion

        #region 相机回调函数

        //调用相机
        CameraAPI ccd1 = null;
        CameraAPI ccd2 = null;
        CameraAPI ccd3_1 = null;
        CameraAPI ccd3_2 = null;
        CameraAPI ccd4 = null;

        //相机图像尺寸
        int ccd1_Height = 0; int ccd1_Width = 0;
        int ccd2_Height = 0; int ccd2_Width = 0;
        int ccd3_1_Height = 0; int ccd3_1_Width = 0;
        int ccd3_2_Height = 0; int ccd3_2_Width = 0;
        int ccd4_Height = 0; int ccd4_Width = 0;


        public enum RunMode
        {
            Free,                                                           // 实时模式
            Auto,                                                           // 自动运行模式
            Tech,                                                            // 试教模式
            Save
        }

        public enum CameraMode
        {
            ccd_Free,
            ccd_Stop,
            ccd_SoftTriggerDo,
            ccd_SoftTrigger,
            ccd_HardTrigger

        }

        RunMode RM1 = new RunMode();
        RunMode RM2 = new RunMode();
        RunMode RM3_1 = new RunMode();
        RunMode RM3_2 = new RunMode();
        RunMode RM4 = new RunMode();

        // HObject Image_ccd1_Free = null;


        HObject Image_ccd1_Run = null;
        HObject Image_ccd2_Run = null;
        HObject Image_ccd3_Run = null;
        HObject Image_ccd4_Run = null;
        HObject Image_ccd5_Run = null;

        HObject Image_ccd1_FreeCross = null;
        HObject Image_ccd2_FreeCross = null;
        HObject Image_ccd3_FreeCross = null;
        HObject Image_ccd4_FreeCross = null;
        HObject Image_ccd5_FreeCross = null;

        //相机回调函数
        private void processHImage1(HObject hImg)
        {
            if (PassData.OnlyUse1_TestCameraHardTrigger)
            {
                MessageBox.Show("ccd1成功硬触发，正常运行请关闭该功能");
            }
            HObject ccd_Img = null;
            HOperatorSet.GenEmptyObj(out ccd_Img);
            ccd_Img.Dispose();
            HOperatorSet.CopyImage(hImg, out ccd_Img);

            if (RM1 == RunMode.Free)//实时模式***************
            {

                if (FreeChange == "ccd1")
                {
                    PassData.m1_disp.HobjectToHimage(ccd_Img);
                    PassData.m1_disp.DispObj(Image_ccd1_FreeCross);
                }

                m1_Disp.HobjectToHimage(ccd_Img);
                m1_Disp.DispObj(Image_ccd1_FreeCross);

                //BeginInvoke(new Action(() =>
                //{
                //    PassData.m1_disp.HobjectToHimage(ccd_Img);
                //    if (Image_ccd1_FreeCross!=null)
                //    {  
                //        PassData.m1_disp.DispObj(Image_ccd1_FreeCross);
                //    }  
                //}));


            }
            else if (RM1 == RunMode.Auto)//正常检测***************
            {
                HOperatorSet.CopyImage(ccd_Img, out Image_ccd1_Run);
            }
            else if (RM1 == RunMode.Tech)//试教***************
            {

                #region 测试用，正常使用需要注视掉

                //ccd_Img=null;
                //HOperatorSet.ReadImage(out ccd_Img,
                //    @"C:\Users\1\Desktop\t1\g1_g3\norm\Image_20191231094915977.bmp");

                #endregion

                string JudgeTCdistance_Ori = m1_Method.Para_stationpara.JudgeTCdistance;
                string JudgeState_Ori = m1_Method.Para_stationpara.JudgeState;
                string NeedInkDetect_Ori = m1_Method.Para_stationpara.NeedInkDetect;
                string NoLensDetect_Ori = m1_Method.Para_stationpara.NoLensDetect;

                m1_Method.Para_stationpara.JudgeTCdistance = "不需要圆心距检测";
                m1_Method.Para_stationpara.JudgeState = "不需要状态判断";
                m1_Method.Para_stationpara.NeedInkDetect = "不需要涂墨检测";
                m1_Method.Para_stationpara.NoLensDetect = "不需要空盘检测";

                m1_Method.InputImage = ccd_Img;

                Detect(m1_Method, m1_Disp);

                if (PassData.OnlyUse1_HardTrigger == "ccd1")
                {
                    Detect(m1_Method, PassData.m1_disp);
                }



                m1_Method.Para_stationpara.JudgeTCdistance = JudgeTCdistance_Ori;
                m1_Method.Para_stationpara.JudgeState = JudgeState_Ori;
                m1_Method.Para_stationpara.NeedInkDetect = NeedInkDetect_Ori;
                m1_Method.Para_stationpara.NoLensDetect = NoLensDetect_Ori;

                PassData.strContent = m1_Method.resultPara.DeltaX.ToString();
                PassData.strContent2 = m1_Method.resultPara.DeltaY.ToString();
                PassData.CameraPT_about = "ccd1PT";
                PassData.CameraPT++;

                //发送PLC
                double x = m1_Method.resultPara.DeltaX * 1000;
                double y = m1_Method.resultPara.DeltaY * 1000;

                int str1 = Convert.ToInt32(x);
                int str2 = Convert.ToInt32(y);
                PT_CameraSendMsgToPLC(Para_MainPara.PLCSelect, str1.ToString(), str2.ToString(), P_CCD1_TrialTeach_omr, P_CCD1_TrialTeach_kv);


            }
            else if (RM1 == RunMode.Save)//保存图像***********
            {

                string readPath = "00";
                SaveImage1(ccd_Img, p_gw1_SaveImage, "bmp", "gw1Save", out readPath);
                PassData.m1_disp.HobjectToHimage(ccd_Img);
                m1_Disp.HobjectToHimage(ccd_Img);
                if (readPath != "00")
                {
                    HObject Image = null;
                    HOperatorSet.ReadImage(out Image, readPath);
                    m1_Method.InputImage = Image;
                }

            }

            ccd_Img.Dispose();
            hImg.Dispose();
        }

        private void processHImage2(HObject hImg)
        {
            if (PassData.OnlyUse1_TestCameraHardTrigger)
            {
                MessageBox.Show("ccd2成功硬触发，正常运行请关闭该功能");
            }
            HObject ccd_Img = null;
            HOperatorSet.GenEmptyObj(out ccd_Img);
            ccd_Img.Dispose();
            HOperatorSet.CopyImage(hImg, out ccd_Img);
            if (RM2 == RunMode.Free)//实时模式***************
            {


                if (FreeChange == "ccd2")
                {
                    PassData.m2_disp.HobjectToHimage(ccd_Img);
                    PassData.m2_disp.DispObj(Image_ccd2_FreeCross);
                }

                m2_Disp.HobjectToHimage(ccd_Img);
                m2_Disp.DispObj(Image_ccd2_FreeCross);
            }
            else if (RM2 == RunMode.Auto)//正常检测***************
            {
                HOperatorSet.CopyImage(ccd_Img, out Image_ccd2_Run);
            }
            else if (RM2 == RunMode.Tech)//试教***************
            {
                string JudgeTCdistance_Ori = m2_Method.Para_stationpara.JudgeTCdistance;
                string JudgeState_Ori = m2_Method.Para_stationpara.JudgeState;
                string NeedInkDetect_Ori = m2_Method.Para_stationpara.NeedInkDetect;
                string NoLensDetect_Ori = m2_Method.Para_stationpara.NoLensDetect;

                m2_Method.Para_stationpara.JudgeTCdistance = "不需要圆心距检测";
                m2_Method.Para_stationpara.JudgeState = "不需要状态判断";
                m2_Method.Para_stationpara.NeedInkDetect = "不需要涂墨检测";
                m2_Method.Para_stationpara.NoLensDetect = "不需要空盘检测";

                m2_Method.InputImage = ccd_Img;
                Detect(m2_Method, m2_Disp);

                if (PassData.OnlyUse1_HardTrigger == "ccd2")
                {
                    Detect(m2_Method, PassData.m2_disp);


                }
                m2_Method.Para_stationpara.JudgeTCdistance = JudgeTCdistance_Ori;
                m2_Method.Para_stationpara.JudgeState = JudgeState_Ori;
                m2_Method.Para_stationpara.NeedInkDetect = NeedInkDetect_Ori;
                m2_Method.Para_stationpara.NoLensDetect = NoLensDetect_Ori;

                PassData.strContent = m2_Method.resultPara.DeltaX.ToString();
                PassData.strContent2 = m2_Method.resultPara.DeltaY.ToString();
                PassData.CameraPT_about = "ccd2PT";
                PassData.CameraPT++;

                //发送PLC
                double x = m2_Method.resultPara.DeltaX * 1000;
                double y = m2_Method.resultPara.DeltaY * 1000;
                int str1 = Convert.ToInt32(x);
                int str2 = Convert.ToInt32(y);
                PT_CameraSendMsgToPLC(Para_MainPara.PLCSelect, str1.ToString(), str2.ToString(),
                    P_CCD2_TrialTeach_omr,
                    P_CCD2_TrialTeach_kv);

            }
            else if (RM2 == RunMode.Save)//保存图像***********
            {
                string readPath = "00";
                SaveImage1(ccd_Img, p_gw2_SaveImage, "bmp", "gw2Save", out readPath);

                PassData.m2_disp.HobjectToHimage(ccd_Img);
                m2_Disp.HobjectToHimage(ccd_Img);

                if (readPath != "00")
                {
                    HObject Image = null;
                    HOperatorSet.ReadImage(out Image, readPath);
                    m2_Method.InputImage = Image;
                }

            }

            ccd_Img.Dispose();
            hImg.Dispose();
        }

        private void processHImage3(HObject hImg)
        {
            if (PassData.OnlyUse1_TestCameraHardTrigger)
            {
                MessageBox.Show("ccd3_1成功硬触发，正常运行请关闭该功能");
            }
            HObject ccd_Img = null;
            HOperatorSet.GenEmptyObj(out ccd_Img);
            ccd_Img.Dispose();
            HOperatorSet.CopyImage(hImg, out ccd_Img);
            if (RM3_1 == RunMode.Free)//实时模式***************
            {


                if (FreeChange == "ccd3_1")
                {

                    PassData.m3_disp.HobjectToHimage(ccd_Img);
                    PassData.m3_disp.DispObj(Image_ccd3_FreeCross);

                }

                m3_Disp.HobjectToHimage(ccd_Img);
                m3_Disp.DispObj(Image_ccd3_FreeCross);



            }
            else if (RM3_1 == RunMode.Auto)//正常检测***************
            {
                HOperatorSet.CopyImage(ccd_Img, out Image_ccd3_Run);
            }
            else if (RM3_1 == RunMode.Tech)//试教***************
            {
                string JudgeTCdistance_Ori = m3_Method.Para_stationpara.JudgeTCdistance;
                string JudgeState_Ori = m3_Method.Para_stationpara.JudgeState;
                string NeedInkDetect_Ori = m3_Method.Para_stationpara.NeedInkDetect;
                string NoLensDetect_Ori = m3_Method.Para_stationpara.NoLensDetect;

                m3_Method.Para_stationpara.JudgeTCdistance = "不需要圆心距检测";
                m3_Method.Para_stationpara.JudgeState = "不需要状态判断";
                m3_Method.Para_stationpara.NeedInkDetect = "不需要涂墨检测";
                m3_Method.Para_stationpara.NoLensDetect = "不需要空盘检测";

                m3_Method.InputImage = ccd_Img;
                Detect(m3_Method, m3_Disp);

                if (PassData.OnlyUse1_HardTrigger == "ccd3_1")
                {
                    Detect(m3_Method, PassData.m3_disp);

                }

                m3_Method.Para_stationpara.JudgeTCdistance = JudgeTCdistance_Ori;
                m3_Method.Para_stationpara.JudgeState = JudgeState_Ori;
                m3_Method.Para_stationpara.NeedInkDetect = NeedInkDetect_Ori;
                m3_Method.Para_stationpara.NoLensDetect = NoLensDetect_Ori;

                PassData.strContent = m3_Method.resultPara.DeltaX.ToString();
                PassData.strContent2 = m3_Method.resultPara.DeltaY.ToString();
                PassData.CameraPT_about = "ccd3_1PT";
                PassData.CameraPT++;

                //发送PLC
                double x = m3_Method.resultPara.DeltaX * 1000;
                double y = m3_Method.resultPara.DeltaY * 1000;
                int str1 = Convert.ToInt32(x);
                int str2 = Convert.ToInt32(y);
                PT_CameraSendMsgToPLC(Para_MainPara.PLCSelect, str1.ToString(), str2.ToString(),
                    P_CCD3_TrialTeach_omr,
                    P_CCD3_TrialTeach_kv);
            }
            else if (RM3_1 == RunMode.Save)//保存图像***********
            {
                string readPath = "00";
                SaveImage1(ccd_Img, p_gw3_SaveImage, "bmp", "gw3_1Save", out readPath);

                PassData.m3_disp.HobjectToHimage(ccd_Img);
                m3_Disp.HobjectToHimage(ccd_Img);

                if (readPath != "00")
                {
                    HObject Image = null;
                    HOperatorSet.ReadImage(out Image, readPath);
                    m3_Method.InputImage = Image;
                }

            }

            ccd_Img.Dispose();
            hImg.Dispose();
        }

        private void processHImage4(HObject hImg)
        {
            if (PassData.OnlyUse1_TestCameraHardTrigger)
            {
                MessageBox.Show("ccd3_2成功硬触发，正常运行请关闭该功能");
            }
            HObject ccd_Img = null;
            HOperatorSet.GenEmptyObj(out ccd_Img);
            ccd_Img.Dispose();
            HOperatorSet.CopyImage(hImg, out ccd_Img);
            if (RM3_2 == RunMode.Free)//实时模式***************
            {


                if (FreeChange == "ccd3_2")
                {
                    PassData.m4_disp.HobjectToHimage(ccd_Img);
                    PassData.m4_disp.DispObj(Image_ccd4_FreeCross);
                }

                m4_Disp.HobjectToHimage(ccd_Img);
                m4_Disp.DispObj(Image_ccd4_FreeCross);




            }
            else if (RM3_2 == RunMode.Auto)//正常检测***************
            {
                HOperatorSet.CopyImage(ccd_Img, out Image_ccd4_Run);
            }
            else if (RM3_2 == RunMode.Tech)//试教***************
            {

                string JudgeTCdistance_Ori = m4_Method.Para_stationpara.JudgeTCdistance;
                string JudgeState_Ori = m4_Method.Para_stationpara.JudgeState;
                string NeedInkDetect_Ori = m4_Method.Para_stationpara.NeedInkDetect;
                string NoLensDetect_Ori = m4_Method.Para_stationpara.NoLensDetect;

                m4_Method.Para_stationpara.JudgeTCdistance = "不需要圆心距检测";
                m4_Method.Para_stationpara.JudgeState = "不需要状态判断";
                m4_Method.Para_stationpara.NeedInkDetect = "不需要涂墨检测";
                m4_Method.Para_stationpara.NoLensDetect = "不需要空盘检测";

                m4_Method.InputImage = ccd_Img;
                Detect(m4_Method, m4_Disp);

                if (PassData.OnlyUse1_HardTrigger == "ccd3_2")
                {
                    Detect(m4_Method, PassData.m4_disp);
                }

                m4_Method.Para_stationpara.JudgeTCdistance = JudgeTCdistance_Ori;
                m4_Method.Para_stationpara.JudgeState = JudgeState_Ori;
                m4_Method.Para_stationpara.NeedInkDetect = NeedInkDetect_Ori;
                m4_Method.Para_stationpara.NoLensDetect = NoLensDetect_Ori;

                PassData.strContent = m4_Method.resultPara.DeltaX.ToString();
                PassData.strContent2 = m4_Method.resultPara.DeltaY.ToString();

                PassData.CameraPT_about = "ccd3_2PT";
                PassData.CameraPT++;

                //发送PLC
                double x = m4_Method.resultPara.DeltaX * 1000;
                double y = m4_Method.resultPara.DeltaY * 1000;
                int str1 = Convert.ToInt32(x);
                int str2 = Convert.ToInt32(y);
                PT_CameraSendMsgToPLC(Para_MainPara.PLCSelect, str1.ToString(), str2.ToString(),
                    P_CCD4_TrialTeach_omr,
                    P_CCD4_TrialTeach_kv);

            }
            else if (RM3_2 == RunMode.Save)//保存图像***********
            {
                string readPath = "00";
                SaveImage1(ccd_Img, p_gw4_SaveImage, "bmp", "gw3_2Save", out readPath);

                PassData.m4_disp.HobjectToHimage(ccd_Img);
                m4_Disp.HobjectToHimage(ccd_Img);

                if (readPath != "00")
                {
                    HObject Image = null;
                    HOperatorSet.ReadImage(out Image, readPath);
                    m4_Method.InputImage = Image;
                }

            }

            ccd_Img.Dispose();
            hImg.Dispose();
        }

        private void processHImage5(HObject hImg)
        {
            if (PassData.OnlyUse1_TestCameraHardTrigger)
            {
                MessageBox.Show("ccd4成功硬触发，正常运行请关闭该功能");
            }
            HObject ccd_Img = null;
            HOperatorSet.GenEmptyObj(out ccd_Img);
            ccd_Img.Dispose();
            HOperatorSet.CopyImage(hImg, out ccd_Img);
            if (RM4 == RunMode.Free)//实时模式***************
            {

                if (FreeChange == "ccd4")
                {
                    PassData.m5_disp.HobjectToHimage(ccd_Img);
                    PassData.m5_disp.DispObj(Image_ccd5_FreeCross);
                }

                m5_Disp.HobjectToHimage(ccd_Img);
                m5_Disp.DispObj(Image_ccd5_FreeCross);

            }
            else if (RM4 == RunMode.Auto)//正常检测***************
            {
                HOperatorSet.CopyImage(ccd_Img, out Image_ccd5_Run);
            }
            else if (RM4 == RunMode.Tech)//试教***************
            {
                string JudgeTCdistance_Ori = m5_Method.Para_stationpara.JudgeTCdistance;
                string JudgeState_Ori = m5_Method.Para_stationpara.JudgeState;
                string NeedInkDetect_Ori = m5_Method.Para_stationpara.NeedInkDetect;
                string NoLensDetect_Ori = m5_Method.Para_stationpara.NoLensDetect;

                m5_Method.Para_stationpara.JudgeTCdistance = "不需要圆心距检测";
                m5_Method.Para_stationpara.JudgeState = "不需要状态判断";
                m5_Method.Para_stationpara.NeedInkDetect = "不需要涂墨检测";
                m5_Method.Para_stationpara.NoLensDetect = "不需要空盘检测";

                m5_Method.InputImage = ccd_Img;
                Detect(m5_Method, m5_Disp);

                if (PassData.OnlyUse1_HardTrigger == "ccd4")
                {
                    Detect(m5_Method, PassData.m5_disp);

                }
                m5_Method.Para_stationpara.JudgeTCdistance = JudgeTCdistance_Ori;
                m5_Method.Para_stationpara.JudgeState = JudgeState_Ori;
                m5_Method.Para_stationpara.NeedInkDetect = NeedInkDetect_Ori;
                m5_Method.Para_stationpara.NoLensDetect = NoLensDetect_Ori;
                PassData.strContent = m5_Method.resultPara.DeltaX.ToString();
                PassData.strContent2 = m5_Method.resultPara.DeltaY.ToString();
                PassData.CameraPT_about = "ccd4PT";
                PassData.CameraPT++;

                //发送PLC
                double x = m5_Method.resultPara.DeltaX * 1000;
                double y = m5_Method.resultPara.DeltaY * 1000;
                int str1 = Convert.ToInt32(x);
                int str2 = Convert.ToInt32(y);
                PT_CameraSendMsgToPLC(Para_MainPara.PLCSelect, str1.ToString(), str2.ToString(),
                    P_CCD5_TrialTeach_omr,
                    P_CCD5_TrialTeach_kv);
            }
            else if (RM4 == RunMode.Save)//保存图像***********
            {
                string readPath = "00";
                SaveImage1(ccd_Img, p_gw5_SaveImage, "bmp", "gw4Save", out readPath);

                PassData.m5_disp.HobjectToHimage(ccd_Img);
                m5_Disp.HobjectToHimage(ccd_Img);

                if (readPath != "00")
                {
                    HObject Image = null;
                    HOperatorSet.ReadImage(out Image, readPath);
                    m5_Method.InputImage = Image;
                }

            }

            ccd_Img.Dispose();
            hImg.Dispose();
        }


        //相机连接配置
        void Cameraconfigure()
        {
            try
            {

                ccd1 = new CameraAPI("ccd1");
                ccd2 = new CameraAPI("ccd2");
                ccd3_1 = new CameraAPI("ccd3");
                if (NumDisp >= 5)
                {
                    ccd3_2 = new CameraAPI("ccd3_2");
                }
                ccd4 = new CameraAPI("ccd4");




                if (ccd1 != null && ccd1.ifccdConnected)
                {
                    ccd1.OpenCam(); PassData.Is_Open_ccd1 = true;
                    ccd1.SetHeartBeatTime(2000);
                    ccd1.setExposureTime(Para_MainPara.expose1);
                    ccd1.setGain(Para_MainPara.gain1);
                    ccd1.eventProcessHImage += processHImage1;
                    ccd1_Height = (int)ccd1.nHeight;
                    ccd1_Width = (int)ccd1.nWidth;
                    HOperatorSet.GenCrossContourXld(out Image_ccd1_FreeCross,
                        ccd1_Height * 1.0 / 2, ccd1_Width * 1.0 / 2, 100, 0);
                    // hardWareConnectionList.Items.Add("相机一:>>" + "已连接");

                }
                else
                {
                    //  hardWareConnectionList.Items.Add("相机一:>>" + "未连接");


                }

                if (ccd2 != null && ccd2.ifccdConnected)
                {
                    ccd2.OpenCam(); PassData.Is_Open_ccd2 = true;
                    ccd2.SetHeartBeatTime(2000);
                    ccd2.setExposureTime(Para_MainPara.expose2);
                    ccd2.setGain(Para_MainPara.gain1);
                    ccd2.eventProcessHImage += processHImage2;
                    ccd2_Height = (int)ccd2.nHeight;
                    ccd2_Width = (int)ccd2.nWidth;
                    HOperatorSet.GenCrossContourXld(out Image_ccd2_FreeCross,
                        ccd2_Height * 1.0 / 2, ccd2_Width * 1.0 / 2, 100, 0);
                    // hardWareConnectionList.Items.Add("相机一:>>" + "已连接");

                }
                else
                {
                    //  hardWareConnectionList.Items.Add("相机一:>>" + "未连接");


                }

                if (ccd3_1 != null && ccd3_1.ifccdConnected)
                {
                    ccd3_1.OpenCam(); PassData.Is_Open_ccd3_1 = true;
                    ccd3_1.SetHeartBeatTime(2000);
                    ccd3_1.setExposureTime(Para_MainPara.expose3);
                    ccd3_1.setGain(Para_MainPara.gain3);
                    ccd3_1.eventProcessHImage += processHImage3;
                    ccd3_1_Height = (int)ccd3_1.nHeight;
                    ccd3_1_Width = (int)ccd3_1.nWidth;
                    HOperatorSet.GenCrossContourXld(out Image_ccd3_FreeCross,
                        ccd3_1_Height * 1.0 / 2, ccd3_1_Width * 1.0 / 2, 100, 0);
                    // hardWareConnectionList.Items.Add("相机一:>>" + "已连接");

                }
                else
                {
                    //  hardWareConnectionList.Items.Add("相机一:>>" + "未连接");


                }


                if (NumDisp >= 5 && ccd3_2 != null && ccd3_2.ifccdConnected)
                {
                    ccd3_2.OpenCam(); PassData.Is_Open_ccd3_2 = true;
                    ccd3_2.SetHeartBeatTime(2000);
                    ccd3_2.setExposureTime(Para_MainPara.expose4);
                    ccd3_2.setGain(Para_MainPara.gain4);
                    ccd3_2.eventProcessHImage += processHImage4;
                    ccd3_2_Height = (int)ccd3_2.nHeight;
                    ccd3_2_Width = (int)ccd3_2.nWidth;
                    HOperatorSet.GenCrossContourXld(out Image_ccd4_FreeCross,
                        ccd3_2_Height * 1.0 / 2, ccd3_2_Width * 1.0 / 2, 100, 0);
                    // hardWareConnectionList.Items.Add("相机一:>>" + "已连接");

                }
                else
                {
                    //  hardWareConnectionList.Items.Add("相机一:>>" + "未连接");


                }


                if (ccd4 != null && ccd4.ifccdConnected)
                {
                    ccd4.OpenCam(); PassData.Is_Open_ccd4 = true;
                    ccd4.SetHeartBeatTime(2000);
                    ccd4.setExposureTime(Para_MainPara.expose5);
                    ccd4.setGain(Para_MainPara.gain5);
                    ccd4.eventProcessHImage += processHImage5;
                    ccd4_Height = (int)ccd4.nHeight;
                    ccd4_Width = (int)ccd4.nWidth;
                    HOperatorSet.GenCrossContourXld(out Image_ccd5_FreeCross,
                        ccd4_Height * 1.0 / 2, ccd4_Width * 1.0 / 2, 100, 0);
                    // hardWareConnectionList.Items.Add("相机一:>>" + "已连接");

                }
                else
                {
                    //  hardWareConnectionList.Items.Add("相机一:>>" + "未连接");


                }




            }
            catch (Exception)
            {


            }


        }


        /// <summary>
        /// 使用相机前，状态确定及参数设定
        /// </summary>
        /// <param name="ccd"></param>
        /// <param name="Mode"></param> 软触发模式、硬触发模式、实时模式
        /// <param name="RM"></param>
        /// <param name="Is_Grabing"></param> 采图是否打开
        /// <param name="Is_SoftTrigger"></param>软触发模式
        /// <param name="Is_ExternalTrigger"></param>//硬触发模式
        void CameraUse(CameraAPI ccd, RunMode rm, RunMode rmBefore, string Mode,
            bool Is_Open,
            bool Is_Grabing, bool Is_SoftTrigger, bool Is_ExternalTrigger,
            out bool isopen,
            out bool isgrabing, out bool issofttrigger, out bool isexternal,
            out RunMode rm1)
        {
            rm1 = rmBefore;
            isopen = Is_Open;
            isgrabing = Is_Grabing;
            issofttrigger = Is_SoftTrigger;
            isexternal = Is_ExternalTrigger;
            if (ccd.ifccdConnected)//相机是否连接
            {
                if (Is_Open == false)
                {
                    ccd.OpenCam();
                    isopen = true;
                }
                else
                {
                    isopen = true;
                }
                switch (Mode)
                {
                    case "实时模式":
                        if (Is_Grabing == false)
                        {
                            ccd.StartGrab();
                            Is_Grabing = true;
                        }
                        else
                        {
                            Is_Grabing = true;
                        }
                        Is_SoftTrigger = false;
                        Is_ExternalTrigger = false;
                        break;

                    case "软触发模式":


                        if (Is_Grabing == true)
                        {
                            ccd.StopGrab();
                            Is_Grabing = false;
                        }
                        else
                        {
                            Is_Grabing = false;

                        }

                        if (Is_SoftTrigger == false)//未设置软触发模式
                        {
                            ccd.SetSoftwareTrigger();
                            Is_SoftTrigger = true;
                        }
                        else
                        {
                            Is_SoftTrigger = true;
                        }

                        if (Is_Grabing == false)
                        {
                            ccd.StartGrab();
                            Is_Grabing = true;
                        }
                        else
                        {
                            Is_Grabing = true;
                        }

                        Is_ExternalTrigger = false;

                        break;

                    case "硬触发模式":
                        if (Is_Grabing == true)
                        {
                            ccd.StopGrab();
                            Is_Grabing = false;
                        }
                        else
                        {
                            Is_Grabing = false;

                        }

                        if (Is_ExternalTrigger == false)
                        {
                            ccd.SetExternTrigger();
                            Is_ExternalTrigger = true;
                        }
                        else
                        {
                            Is_ExternalTrigger = true;
                        }

                        if (Is_Grabing == false)
                        {
                            ccd.StartGrab();
                            Is_Grabing = true;
                        }
                        else
                        {
                            Is_Grabing = true;
                        }

                        Is_SoftTrigger = false;

                        break;

                    default:
                        break;
                }
                isgrabing = Is_Grabing;
                issofttrigger = Is_SoftTrigger;
                isexternal = Is_ExternalTrigger;
                rm1 = rm;
            }
            else
            {
                MessageBox.Show("相机未连接");
            }


        }

        //较之前，把实时触发的执行和软触发的执行放置在里面，即直接调用函数即可
        void CameraUse2(bool IsNeedDo, CameraAPI ccd, RunMode rm, RunMode rmBefore, string Mode,
          bool Is_Open,
          bool Is_Grabing, bool Is_SoftTrigger, bool Is_ExternalTrigger,
          out bool isopen,
          out bool isgrabing, out bool issofttrigger, out bool isexternal,
          out RunMode rm1)
        {
            rm1 = rmBefore;
            isopen = Is_Open;
            isgrabing = Is_Grabing;
            issofttrigger = Is_SoftTrigger;
            isexternal = Is_ExternalTrigger;
            if (ccd != null)
            {
                if (ccd.ifccdConnected)//相机是否连接
                {
                    if (Is_Open == false)
                    {
                        ccd.OpenCam();
                        isopen = true;
                    }
                    else
                    {
                        isopen = true;
                    }
                    switch (Mode)
                    {
                        case "实时模式":
                            if (Is_Grabing == false)
                            {
                                ccd.StartGrab();
                                Is_Grabing = true;
                            }
                            else
                            {
                                Is_Grabing = true;
                            }
                            Is_SoftTrigger = false;
                            Is_ExternalTrigger = false;

                            if (IsNeedDo)
                            {
                                ccd.SetFreerun();
                            }

                            break;

                        case "软触发模式":


                            if (Is_Grabing == true)
                            {
                                ccd.StopGrab();
                                Is_Grabing = false;
                            }
                            else
                            {
                                Is_Grabing = false;

                            }

                            if (Is_SoftTrigger == false)//未设置软触发模式
                            {
                                ccd.SetSoftwareTrigger();
                                Is_SoftTrigger = true;
                            }
                            else
                            {
                                Is_SoftTrigger = true;
                            }

                            if (Is_Grabing == false)
                            {
                                ccd.StartGrab();
                                Is_Grabing = true;
                            }
                            else
                            {
                                Is_Grabing = true;
                            }

                            Is_ExternalTrigger = false;

                            if (IsNeedDo)
                            {
                                ccd.SendSoftwareExecute();
                            }


                            break;

                        case "硬触发模式":
                            if (Is_Grabing == true)
                            {
                                ccd.StopGrab();
                                Is_Grabing = false;
                            }
                            else
                            {
                                Is_Grabing = false;

                            }

                            if (Is_ExternalTrigger == false)
                            {
                                ccd.SetExternTrigger();
                                Is_ExternalTrigger = true;
                            }
                            else
                            {
                                Is_ExternalTrigger = true;
                            }

                            if (Is_Grabing == false)
                            {
                                ccd.StartGrab();
                                Is_Grabing = true;
                            }
                            else
                            {
                                Is_Grabing = true;
                            }

                            Is_SoftTrigger = false;

                            break;

                        default:
                            break;
                    }
                    isgrabing = Is_Grabing;
                    issofttrigger = Is_SoftTrigger;
                    isexternal = Is_ExternalTrigger;
                    rm1 = rm;
                }
                else
                {
                    //  MessageBox.Show("相机未连接");
                }
            }

        }

        #endregion

        #region PLC通信_欧姆龙

        //PLC 试教时 传入D区的具体位置
        int P_CCD1_TrialTeach_omr = 100;
        int P_CCD2_TrialTeach_omr = 200;
        int P_CCD3_TrialTeach_omr = 300;
        int P_CCD4_TrialTeach_omr = 500;
        int P_CCD5_TrialTeach_omr = 400;

        //PLC 工作时 传入D区的具体位置
        int P_CCD1_Run_omr = 100;
        int P_CCD2_Run_omr = 200;
        int P_CCD3_Run_omr = 300;
        int P_CCD4_Run_omr = 500;
        int P_CCD5_Run_omr = 400;

        //32位  Write
        public void WriteISPLCConnection11(string PLC_IP_adress, int FINS_UDP_PORT,
           int ComIPLastPlace,
           int memory,
           int StartP,
           int Readquantity,
           string str)
        {

            Readquantity = Readquantity * 2;
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
            //if (str!=null|| str != "")
            //{
            if (Readquantity == 2)
            {
                wwwr = new string[1] { str };

            }
            else
            {
                wwwr = str.Split(',');

            }
            // }

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



                    string dsa2 =
                    System.Convert.ToString(fdasfdas, 2).PadLeft(32, '0');

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

                    jj = jj + 4;
                }



                for (int i = 0; i < 18; i++)
                {
                    send_data11[i] = send_data[i];
                }

            }
            else
            {
                MessageBox.Show("写入数据和数量不符合");
                return;
            }



            #endregion

            try
            {
                newclient.Send(send_data11, send_data11.Length, remote_ip);
                newclient.Client.ReceiveTimeout = 2000;
                //byte[] dm_data = newclient.Receive(ref remote_ip);
                //if (dm_data[12] != 0 || dm_data[13] != 0)
                //{
                //    MessageBox.Show("PLC写入后的返回值dm_data[12]:" + dm_data[12] +
                //        " dm_data[13]:" + dm_data[13]);
                //    // textBox1.Text = "出错";
                //    //  return true;
                //}
                //else
                //{
                //    MessageBox.Show("写入完成");
                //}
            }
            catch
            {
                // MessageBox.Show("PLC连接失败");
                // textBox1.Text = "出错";
                //  return false;
            }

        }

        //32位 Read
        public void ReadISPLCConnection11(string PLC_IP_adress, int FINS_UDP_PORT,
           int ComIPLastPlace,
           int memory,
           int StartP,
           int Readquantity,
           out string str)
        {
            str = null;
            Readquantity = Readquantity * 2;
            int Readquantity1 = Readquantity;

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
                0x01, //11.(SRC) Sub-request code: 01, memory area read
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

            #region 读取个数设置
            send_data[16] = (byte)(Readquantity >> 8);
            send_data[17] = (byte)(Readquantity);
            #endregion

            try
            {
                newclient.Send(send_data, send_data.Length, remote_ip);
                newclient.Client.ReceiveTimeout = 1000;
                byte[] dm_data = newclient.Receive(ref remote_ip);
                if (dm_data[12] != 0 || dm_data[13] != 0)
                {
                    // MessageBox.Show("if出错");

                }
                else
                {
                    string fdsa = "";
                    int qqwi = 14;
                    //textBox1.Text=(dm_data[14]*256+dm_data[15]).ToString();
                    for (int i = 0; i < Readquantity1 / 2; i++)
                    {

                        int dd1 = dm_data[qqwi] * 256 + dm_data[qqwi + 1];

                        int gg0 = dm_data[qqwi + 2] * 256 + dm_data[qqwi + +3];

                        string g_10To2 = Convert.ToString(gg0, 2).PadLeft(16, '0');

                        string d_10To2 = Convert.ToString(dd1, 2).PadLeft(16, '0');


                        string fdsaasa = g_10To2 + d_10To2;

                        int fdsf11 = Convert.ToInt32(fdsaasa, 2);

                        str = fdsf11 + "," + str;

                        qqwi = qqwi + 4;

                    }
                }
            }
            catch
            {
                // MessageBox.Show("PLC出错");
            }

        }

        bool IsPLC_Connected_omr()
        {
            bool Is_Connect = false;
            try
            {
                string str00 = "";
                ReadISPLCConnection11(
                    Para_MainPara.PLC_IP_omr,
                    Para_MainPara.PLC_Port_omr,
                    Para_MainPara.PC_finalIP_omr,
                     130,//D区
                     100,//D100
                       1,//读取1位
                       out str00
                    );

                if (str00 != null)
                {
                    Is_Connect = true;
                }
                else
                {
                    Is_Connect = false;
                }



            }
            catch (Exception ex)
            {
                WriteLog("OMR_PLC连接问题:" + ex.Message, p_D_Log);
                Is_Connect = false;
            }
            return Is_Connect;

        }

        //备用 
        public void WriteISPLCConnection113(
            string PLC_IP_adress,
            int FINS_UDP_PORT,
            int ComIPLastPlace,
            int memory,
            int StartP,
            int Readquantity,
            string str)
        {
            //  str = null;
            Readquantity = Readquantity * 2;
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
            //if (str!=null|| str != "")
            //{
            if (Readquantity == 2)
            {
                wwwr = new string[1] { str };

            }
            else
            {
                wwwr = str.Split(',');

            }
            // }

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

                    string dsa2 =
                    System.Convert.ToString(fdasfdas, 2).PadLeft(32, '0');

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
                MessageBox.Show("写入数据个数和数量不符合");
                return;
            }

            #endregion

            try
            {
                newclient.Send(send_data11, send_data11.Length, remote_ip);
                newclient.Client.ReceiveTimeout = 3000;
                byte[] dm_data = newclient.Receive(ref remote_ip);
                if (dm_data[12] != 0 || dm_data[13] != 0)
                {
                    //  MessageBox.Show("返回标志位出错");

                }
                else
                {
                    // MessageBox.Show("写入完成");
                }
            }
            catch
            {
                MessageBox.Show("PLC连接失败");
            }
        }

        #endregion

        #region PLC通信_基恩士

        //寄存器  需要确认
        int P_CCD1_TrialTeach_kv = 18000;//18000-X,18002-Y,18004-Ang,18006-bzw
        int P_CCD2_TrialTeach_kv = 18008;
        int P_CCD3_TrialTeach_kv = 18016;
        int P_CCD4_TrialTeach_kv = 18024;
        int P_CCD5_TrialTeach_kv = 18032;


        int P_CCD1_Run_kv = 18000;//ccd1
        int P_CCD2_Run_kv = 18008;//ccd2
        int P_CCD3_Run_kv = 18016;//ccd3
        int P_CCD4_Run_kv = 18024;//ccd3_2
        int P_CCD5_Run_kv = 18032;//ccd4


        void SendMsg(string PLC_IP, int Port, int MemoryP, int str_num, string message)
        {

            string Msg = null;

            try
            {
                string[] arr = message.Split(',');

                if (str_num != arr.Length)
                {

                    MessageBox.Show("数据个数与写入个数不相等");

                    return;

                }

                string justMsg = null;
                //仅数据发送格式
                for (int i = 0; i < str_num; i++)
                {
                    if (i == 0)
                    {
                        justMsg = arr[i];
                    }
                    else
                    {
                        justMsg = justMsg + " " + arr[i];
                    }
                }

                string Msg11 = "WRS DM" + MemoryP.ToString() + ".L" + " " + str_num.ToString() + " " + justMsg + "\r";

                Msg = Msg11;

            }
            catch (Exception)
            {

            }

            if (Msg == null)
            {
                MessageBox.Show("发送数据格式有错误");
                return;
            }

            // UdpClient SendClient = new UdpClient(0);
            UdpClient SendClient = new UdpClient();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Msg);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(PLC_IP), Port);

            try
            {

                SendClient.Send(bytes, bytes.Length, iep);
                SendClient.Client.ReceiveTimeout = 1000;
                // byte[] dm_data = SendClient.Receive(ref iep);

                //if (dm_data[12] != 0 || dm_data[13] != 0)
                //{
                //    MessageBox.Show("进入返回值");
                //}
                //else
                //{
                //    MessageBox.Show("写入完成");
                //}

            }
            catch (Exception)
            {
                // AddItem(listBoxstatus, "发送出错" + ex.Message);
                //MessageBox.Show("写入出错");

            }
        }

        void ReadMsg(string PLC_IP, int Port, int MemoryP, int str_ReadNum, out string message)
        {
            message = null;
            string Msg = "RDS DM" + MemoryP.ToString() + ".L" + " " + str_ReadNum.ToString() + "\r";
            UdpClient SendClient = new UdpClient();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Msg);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(PLC_IP), Port);
            try
            {
                SendClient.Send(bytes, bytes.Length, iep);
                SendClient.Client.ReceiveTimeout = 1000;
                byte[] dm_data = SendClient.Receive(ref iep);
                message = System.Text.Encoding.Default.GetString(dm_data);
            }
            catch (Exception)
            {
                message = null;

            }
        }

        bool IsPLC_Connected_kv()
        {
            bool Is_connect = false;

            try
            {

                string dsa = null;
                ReadMsg(
                    Para_MainPara.PLC_IP_kv,
                    Para_MainPara.PLC_Port_kv,
                    6006,
                    1,
                    out dsa);
                if (dsa == null)
                {
                    Is_connect = false;
                }
                else
                {
                    Is_connect = true;
                }



            }
            catch (Exception ex)
            {
                WriteLog("KV_PLC连接问题:" + ex.Message, p_D_Log);
                Is_connect = false;
            }

            return Is_connect;
        }

        #endregion

        #region 文件存储子界面

        FileSet Form_FileSet;
        void FileSetForm()
        {

            if (Form_FileSet == null)
            {

                Form_FileSet = new FileSet();


                //注册事件  子界面向主界面传值
                // Form_Settings.SendMsgToForm1_Event += EventFromSettingChaned;

                //注册事件  主界面向子界面传值
                //    SendMsgToSetting_Event += Form_Settings.EventFromForm1Chaned;
                //   SendMsgToSetting_Event(null, Para_M_DispenserPara);

                Form_FileSet.StartPosition = FormStartPosition.CenterScreen;
                Form_FileSet.ShowDialog(this);
                // UpdateLogList("打开试教及设置界面", p_D_Log);

            }
            else
            {
                if (Form_FileSet.IsDisposed)
                {
                    Form_FileSet = new FileSet();

                    // Form_Settings.SendMsgToForm1_Event += EventFromSettingChaned;

                    // SendMsgToSetting_Event += Form_Settings.EventFromForm1Chaned;
                    // SendMsgToSetting_Event(null, Para_M_DispenserPara);


                    Form_FileSet.StartPosition = FormStartPosition.CenterScreen;
                    Form_FileSet.ShowDialog();
                    //  UpdateLogList("打开试教及设置界面", p_D_Log);

                }
                else
                {
                    Form_FileSet = new FileSet();

                    //  Form_Settings.SendMsgToForm1_Event += EventFromSettingChaned;

                    // SendMsgToSetting_Event += Form_Settings.EventFromForm1Chaned;

                    //  SendMsgToSetting_Event(null, Para_M_DispenserPara);

                    Form_FileSet.WindowState = FormWindowState.Normal;

                    //Form_FileSet.Activate();

                    Form_FileSet.ShowDialog();


                }
            }


        }

        #endregion

        #region PLC设置子界面

        //注册事件
        //发送给子界面事件
        public event EventHandler SendMsgToPLCSet_Event;

        private void Form_PLCSet_SendMsgToForm1_Event(object sender, EventArgs e)
        {
            //取得子界面参数
            Para_MainPara = e as MainPara;

            // int dasdsa = Para_M_DispenserPara.expose1;
        }

        PLCSet Form_PLCSet;
        void PLCsetForm()
        {
            try
            {
                if (Form_PLCSet == null)
                {

                    Form_PLCSet = new PLCSet();


                    //注册事件  子界面向主界面传值
                    Form_PLCSet.SendMsgToForm1_Event += Form_PLCSet_SendMsgToForm1_Event; ;

                    //注册事件  主界面向子界面传值
                    SendMsgToPLCSet_Event += Form_PLCSet.EventFromForm1Chaned;
                    SendMsgToPLCSet_Event(null, Para_MainPara);

                    Form_PLCSet.StartPosition = FormStartPosition.CenterScreen;
                    // Form_PLCSet.ShowDialog(this);
                    Form_PLCSet.Show(this);
                    // UpdateLogList("打开试教及设置界面", p_D_Log);

                }
                else
                {
                    if (Form_PLCSet.IsDisposed)
                    {
                        Form_PLCSet = new PLCSet();

                        Form_PLCSet.SendMsgToForm1_Event += Form_PLCSet_SendMsgToForm1_Event;

                        SendMsgToPLCSet_Event += Form_PLCSet.EventFromForm1Chaned;
                        SendMsgToPLCSet_Event(null, Para_MainPara);


                        Form_PLCSet.StartPosition = FormStartPosition.CenterScreen;
                        // Form_PLCSet.ShowDialog(this);
                        Form_PLCSet.Show(this);
                        //  UpdateLogList("打开试教及设置界面", p_D_Log);

                    }
                    else
                    {
                        Form_PLCSet = new PLCSet();

                        Form_PLCSet.SendMsgToForm1_Event += Form_PLCSet_SendMsgToForm1_Event;

                        SendMsgToPLCSet_Event += Form_PLCSet.EventFromForm1Chaned;

                        SendMsgToPLCSet_Event(null, Para_MainPara);

                        Form_PLCSet.WindowState = FormWindowState.Normal;

                        Form_PLCSet.Activate();

                        // Form_PLCSet.ShowDialog(this);
                        // Form_PLCSet.Show(this);


                    }
                }



            }
            catch (Exception)
            {


            }



        }



        #endregion

        #region 操作指令记录

        void UpdateLogList(string updatelogmsg, string msgSavepath)
        {// 
            UpdateLogList0(updatelogmsg);
            WriteLog(updatelogmsg, msgSavepath);

        }

        string timeLogList = DateTime.Now.ToString();

        /// <summary>
        /// 操作记录输出
        /// </summary>
        /// <param name="str"></param>
        private delegate void Myinvoke1(string str);

        private void UpdateLogList0(string str)
        {

            if (this.LogList.InvokeRequired)     //(this.ClientList.InvokeRequired)
            {
                Myinvoke1 _myInvoke = new Myinvoke1(UpdateLogList0);
                this.Invoke(_myInvoke, new object[] { str });
            }
            else
            {
                this.LogList.Items.Add(timeLogList + " " + af + " >>-- " + str);
            }

            this.LogList.SelectedIndex = this.LogList.Items.Count - 1;
            this.LogList.TopIndex = this.LogList.Items.Count - 1;

            af++;//统计已经加载的操作记录
            if (af > 1000)
            {
                this.LogList.Items.Clear(); //没有问题；
                af = 1;
            }


        }
        int af = 1;

        //写日志  和  动作指令一起输入
        void WriteLog(string msg, string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string logPath = filePath + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + msg);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (IOException e)
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {

                    sw.WriteLine(e.Message + " " + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();

                }
            }
        }

        //删除日志  在启动程序时判断
        void LogDelete(string logPath, double saveDays)
        {
            try
            {
                if (!Directory.Exists(logPath))
                {
                    return;
                }
                DirectoryInfo folder = new DirectoryInfo(logPath);
                foreach (FileInfo file in folder.GetFiles("*.txt"))
                {
                    //DateTime dt = file.CreationTime;
                    DateTime dt = file.LastWriteTime;
                    DateTime dt2 = dt.Date;
                    DateTime dtnow = DateTime.Now.Date;

                    DateTime comdt = dt2.AddDays(saveDays);


                    if (comdt < dtnow)
                    {
                        try
                        {
                            File.Delete(file.FullName);
                        }
                        catch
                        {

                        }
                    }
                }

                return;
            }
            catch
            {

                return;
            }

        }

        //判断文件夹大小，然后删除  在启动程序时判断
        void GetDirectoryLength(string dirPath, double numG)
        {
            //判断给定的路径是否存在,如果不存在则退出
            long len = 0;
            if (!Directory.Exists(dirPath))
            {
                return;
            }

            //定义一个DirectoryInfo对象
            DirectoryInfo di = new DirectoryInfo(dirPath);
            //通过GetFiles方法,获取di目录中的所有文件的大小
            int Numpic = 0;
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
                Numpic = Numpic + 1;
            }

            long YI_G = 1024 * 1024 * 1024;
            long Thr_G = Convert.ToInt64(numG * YI_G);

            if (len > Thr_G && Numpic > 20)//超过一定比例就会删除
            {
                int tempNum = Convert.ToInt32(Numpic * 0.9);

                DirectoryInfo folder = new DirectoryInfo(dirPath);

                int Inum = 0;

                foreach (FileInfo file in folder.GetFiles("*.bmp"))
                {
                    if (Inum < tempNum)
                    {
                        try
                        {
                            File.Delete(file.FullName);
                            Inum = Inum + 1;
                        }
                        catch
                        {
                            return;
                        }
                    }
                }
            }

            return;
        }

        #endregion

        #region 硬件连接状态


        //硬件连接
        void HardWareConnection()
        {
            this.HardWareList.Items.Clear();
            this.HardWareList.Items.Add("当前状态:>>" + "未运行");

            //PLC连接判断
            if (Para_MainPara.PLCSelect == "欧姆龙")
            {
                if (IsPLC_Connected_omr())
                {
                    this.HardWareList.Items.Add("PLC连接:>>" + "已连接");
                }
                else
                {
                    this.HardWareList.Items.Add("PLC连接:>>" + "未连接");
                }
            }
            else
            {
                if (IsPLC_Connected_kv())
                {
                    this.HardWareList.Items.Add("PLC连接:>>" + "已连接");
                }
                else
                {
                    this.HardWareList.Items.Add("PLC连接:>>" + "未连接");
                }
            }


            //相机连接
            if (ccd1 != null && ccd1.ifccdConnected)
            {
                this.HardWareList.Items.Add("ccd1:>>" + "已连接");
            }
            else
            {
                this.HardWareList.Items.Add("ccd1:>>" + "未连接");
            }

            if (ccd2 != null && ccd2.ifccdConnected)
            {
                this.HardWareList.Items.Add("ccd2:>>" + "已连接");
            }
            else
            {
                this.HardWareList.Items.Add("ccd2:>>" + "未连接");
            }

            if (ccd3_1 != null && ccd3_1.ifccdConnected)
            {
                this.HardWareList.Items.Add("ccd3_1:>>" + "已连接");
            }
            else
            {
                this.HardWareList.Items.Add("ccd3_1:>>" + "未连接");
            }

            if (NumDisp >= 5)
            {
                if (ccd3_2 != null && ccd3_2.ifccdConnected)
                {
                    this.HardWareList.Items.Add("ccd3_2:>>" + "已连接");
                }
                else
                {
                    this.HardWareList.Items.Add("ccd3_2:>>" + "未连接");
                }
            }

            if (ccd4 != null && ccd4.ifccdConnected)
            {
                this.HardWareList.Items.Add("ccd4:>>" + "已连接");
            }
            else
            {
                this.HardWareList.Items.Add("ccd4:>>" + "未连接");
            }


        }

        /// <summary>
        /// 修改硬件状态
        /// </summary>
        /// <param name="strState">1为On，0为Off</param>
        void HardWareStateChange(string strName, int strState)
        {
            try
            {
                switch (strName)
                {
                    case "主程序状态":

                        if (strState >= 1)
                        {
                            this.HardWareList.Items[0] = "当前状态:>>" + "运行中";
                        }
                        else
                        {
                            this.HardWareList.Items[0] = "当前状态:>>" + "未运行";
                        }

                        break;

                    case "plc":

                        if (strState >= 1)
                        {
                            this.HardWareList.Items[1] = "PLC连接:>>" + "已连接";
                        }
                        else
                        {
                            this.HardWareList.Items[1] = "PLC连接:>>" + "未连接";
                        }

                        break;

                    case "ccd1":

                        if (strState >= 1)
                        {
                            this.HardWareList.Items[2] = "ccd1:>>" + "已连接";
                        }
                        else
                        {
                            this.HardWareList.Items[2] = "ccd1:>>" + "未连接";
                        }
                        break;

                    case "ccd2":

                        if (strState >= 1)
                        {
                            this.HardWareList.Items[3] = "ccd2:>>" + "已连接";
                        }
                        else
                        {
                            this.HardWareList.Items[3] = "ccd2:>>" + "未连接";
                        }

                        break;


                    case "ccd3_1":

                        if (strState >= 1)
                        {
                            this.HardWareList.Items[4] = "ccd3_1:>>" + "已连接";
                        }
                        else
                        {
                            this.HardWareList.Items[4] = "ccd3_1:>>" + "未连接";
                        }
                        break;

                    case "ccd3_2":

                        if (strState >= 1)
                        {
                            this.HardWareList.Items[5] = "ccd3_2:>>" + "已连接";
                        }
                        else
                        {
                            this.HardWareList.Items[5] = "ccd3_2:>>" + "未连接";
                        }
                        break;

                    case "ccd4":

                        if (strState >= 1)
                        {
                            if (NumDisp >= 5)
                            {
                                this.HardWareList.Items[6] = "ccd4:>>" + "已连接";

                            }
                            else
                            {
                                this.HardWareList.Items[5] = "ccd4:>>" + "已连接";

                            }
                        }
                        else
                        {
                            if (NumDisp >= 5)
                            {
                                this.HardWareList.Items[6] = "ccd4:>>" + "未连接";

                            }
                            else
                            {
                                this.HardWareList.Items[5] = "ccd4:>>" + "未连接";

                            }
                        }
                        break;

                    default:
                        break;
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }

        #endregion

        #region 数据显示 datagridView

        int index_gw1;
        int index_gw2;
        int index_gw3;
        int index_gw4;//3-2
        int index_gw5;
        void dataGridViewInitial()
        {
            //for (int i = 0; i < dataGridView1.ColumnCount; i++)
            //{
            //    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;//自适应列宽
            //}

            dataGridView1.Columns[0].Width = 85;//工位
            dataGridView1.Columns[1].Width = 85;//x
            dataGridView1.Columns[2].Width = 85;//y
            dataGridView1.Columns[3].Width = 85;//结果 
            dataGridView1.Columns[4].Width = 85;//ok
            dataGridView1.Columns[5].Width = 85;//ng
            dataGridView1.Columns[6].Width = 85;//总量
            dataGridView1.Columns[7].Width = 85;//良率


            index_gw1 = this.dataGridView1.Rows.Add();
            datagridViewPlay(index_gw1, Para_MainPara.gw1_LVDataDisplay);
            index_gw2 = this.dataGridView1.Rows.Add();
            datagridViewPlay(index_gw2, Para_MainPara.gw2_LVDataDisplay);

            index_gw3 = this.dataGridView1.Rows.Add();
            datagridViewPlay(index_gw3, Para_MainPara.gw3_LVDataDisplay);

            if (NumDisp >= 5)
            {
                index_gw4 = this.dataGridView1.Rows.Add();
                datagridViewPlay(index_gw4, Para_MainPara.gw4_LVDataDisplay);
            }

            index_gw5 = this.dataGridView1.Rows.Add();
            datagridViewPlay(index_gw5, Para_MainPara.gw5_LVDataDisplay);

        }

        void datagridViewPlay(int n_row, double[] data)
        {
            try
            {
                for (int i = 0; i < data.Length; i++)
                {

                    switch (i)
                    {

                        case 0://gw


                            if (NumDisp >= 5)
                            {
                                if (n_row == 2 || n_row == 3)
                                {

                                    if (n_row == 2)
                                    {
                                        dataGridView1.Rows[n_row].Cells[i].Value = "3_1";
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[n_row].Cells[i].Value = "3_2";
                                    }

                                }
                                else
                                {
                                    dataGridView1.Rows[n_row].Cells[i].Value = data[i].ToString();
                                }

                            }
                            else
                            {
                                dataGridView1.Rows[n_row].Cells[i].Value = data[i].ToString();
                            }

                            break;

                        case 1://X
                            dataGridView1.Rows[n_row].Cells[i].Value = data[i].ToString();

                            break;

                        case 2://Y
                            dataGridView1.Rows[n_row].Cells[i].Value = data[i].ToString();

                            break;


                        case 3://jieguo 
                            if (data[i] == 1)//ok
                            {
                                dataGridView1.Rows[n_row].Cells[i].Value = "OK";
                            }
                            else//ng
                            {
                                dataGridView1.Rows[n_row].Cells[i].Value = "NG";
                            }
                            break;

                        case 4://OK
                            dataGridView1.Rows[n_row].Cells[i].Value = data[i].ToString();

                            break;

                        case 5://NG
                            dataGridView1.Rows[n_row].Cells[i].Value = data[i].ToString();

                            break;

                        case 6://all
                            dataGridView1.Rows[n_row].Cells[i].Value = data[i].ToString();

                            break;

                        case 7://%
                            dataGridView1.Rows[n_row].Cells[i].Value = data[i].ToString() + "%";

                            break;

                        default:
                            break;
                    }



                }

            }
            catch { }


        }

        void datagridViewRun_DataCal(StationMethod.ResultPara rsdata, int gw)
        {
            try
            {
                switch (gw)
                {
                    case 1://工位1

                        if (rsdata.PLC_Serial == "1")//检测成功
                        {

                            //  0-gw 1-x 2-y  3-jieguo   4-ok  5-ng  6-all 7-%  
                            Para_MainPara.gw1_LVDataDisplay[0] = 1;
                            Para_MainPara.gw1_LVDataDisplay[1] = rsdata.DeltaX;
                            Para_MainPara.gw1_LVDataDisplay[2] = rsdata.DeltaY;
                            Para_MainPara.gw1_LVDataDisplay[3] = 1;
                            Para_MainPara.gw1_LVDataDisplay[4]++;
                            //Para_MainPara.gw1_LVDataDisplay[5]++;
                            Para_MainPara.gw1_LVDataDisplay[6]++;
                            Para_MainPara.gw1_LVDataDisplay[7] =
                                Math.Round(
                            Para_MainPara.gw1_LVDataDisplay[4] /
                            Para_MainPara.gw1_LVDataDisplay[6]
                                , 3) * 100;


                        }
                        else//检测失败
                        {

                            //  0-gw 1-x 2-y  3-jieguo   4-ok  5-ng  6-all 7-%  
                            Para_MainPara.gw1_LVDataDisplay[0] = 1;
                            Para_MainPara.gw1_LVDataDisplay[1] = rsdata.DeltaX;
                            Para_MainPara.gw1_LVDataDisplay[2] = rsdata.DeltaY;
                            Para_MainPara.gw1_LVDataDisplay[3] = 2;
                            //Para_MainPara.gw1_LVDataDisplay[4]++;
                            Para_MainPara.gw1_LVDataDisplay[5]++;
                            Para_MainPara.gw1_LVDataDisplay[6]++;
                            Para_MainPara.gw1_LVDataDisplay[7] =
                                Math.Round(
                            Para_MainPara.gw1_LVDataDisplay[4] /
                            Para_MainPara.gw1_LVDataDisplay[6]
                                , 3) * 100;

                        }

                        datagridViewPlay(index_gw1, Para_MainPara.gw1_LVDataDisplay);
                        break;

                    case 2://工位2

                        if (rsdata.PLC_Serial == "1")//检测成功
                        {

                            //  0-gw 1-x 2-y  3-jieguo   4-ok  5-ng  6-all 7-%  
                            Para_MainPara.gw2_LVDataDisplay[0] = 2;
                            Para_MainPara.gw2_LVDataDisplay[1] = rsdata.DeltaX;
                            Para_MainPara.gw2_LVDataDisplay[2] = rsdata.DeltaY;
                            Para_MainPara.gw2_LVDataDisplay[3] = 1;
                            Para_MainPara.gw2_LVDataDisplay[4]++;
                            //Para_MainPara.gw1_LVDataDisplay[5]++;
                            Para_MainPara.gw2_LVDataDisplay[6]++;
                            Para_MainPara.gw2_LVDataDisplay[7] =
                                Math.Round(
                            Para_MainPara.gw2_LVDataDisplay[4] /
                            Para_MainPara.gw2_LVDataDisplay[6]
                                , 3) * 100;


                        }
                        else//检测失败
                        {

                            //  0-gw 1-x 2-y  3-jieguo   4-ok  5-ng  6-all 7-%  
                            Para_MainPara.gw2_LVDataDisplay[0] = 2;
                            Para_MainPara.gw2_LVDataDisplay[1] = rsdata.DeltaX;
                            Para_MainPara.gw2_LVDataDisplay[2] = rsdata.DeltaY;
                            Para_MainPara.gw2_LVDataDisplay[3] = 2;
                            //Para_MainPara.gw1_LVDataDisplay[4]++;
                            Para_MainPara.gw2_LVDataDisplay[5]++;
                            Para_MainPara.gw2_LVDataDisplay[6]++;
                            Para_MainPara.gw2_LVDataDisplay[7] =
                                Math.Round(
                            Para_MainPara.gw2_LVDataDisplay[4] /
                            Para_MainPara.gw2_LVDataDisplay[6]
                                , 3) * 100;

                        }

                        datagridViewPlay(index_gw2, Para_MainPara.gw2_LVDataDisplay);

                        break;


                    case 3://工位3-1

                        if (rsdata.PLC_Serial == "1")//检测成功
                        {

                            //0-gw 1-x 2-y  3-jieguo   4-ok  5-ng  6-all 7-%  
                            Para_MainPara.gw3_LVDataDisplay[0] = 3;
                            Para_MainPara.gw3_LVDataDisplay[1] = rsdata.DeltaX;
                            Para_MainPara.gw3_LVDataDisplay[2] = rsdata.DeltaY;
                            Para_MainPara.gw3_LVDataDisplay[3] = 1;
                            Para_MainPara.gw3_LVDataDisplay[4]++;
                            //Para_MainPara.gw1_LVDataDisplay[5]++;
                            Para_MainPara.gw3_LVDataDisplay[6]++;
                            Para_MainPara.gw3_LVDataDisplay[7] =
                                Math.Round(
                            Para_MainPara.gw3_LVDataDisplay[4] /
                            Para_MainPara.gw3_LVDataDisplay[6]
                                , 3) * 100;


                        }
                        else//检测失败
                        {

                            //  0-gw 1-x 2-y  3-jieguo   4-ok  5-ng  6-all 7-%  
                            Para_MainPara.gw3_LVDataDisplay[0] = 3;
                            Para_MainPara.gw3_LVDataDisplay[1] = rsdata.DeltaX;
                            Para_MainPara.gw3_LVDataDisplay[2] = rsdata.DeltaY;
                            Para_MainPara.gw3_LVDataDisplay[3] = 2;
                            //Para_MainPara.gw1_LVDataDisplay[4]++;
                            Para_MainPara.gw3_LVDataDisplay[5]++;
                            Para_MainPara.gw3_LVDataDisplay[6]++;
                            Para_MainPara.gw3_LVDataDisplay[7] =
                                Math.Round(
                            Para_MainPara.gw3_LVDataDisplay[4] /
                            Para_MainPara.gw3_LVDataDisplay[6]
                                , 3) * 100;

                        }

                        datagridViewPlay(index_gw3, Para_MainPara.gw3_LVDataDisplay);

                        break;

                    case 4://工位3-2

                        if (rsdata.PLC_Serial == "1")//检测成功
                        {

                            //0-gw 1-x 2-y  3-jieguo   4-ok  5-ng  6-all 7-%  
                            Para_MainPara.gw4_LVDataDisplay[0] = 32;
                            Para_MainPara.gw4_LVDataDisplay[1] = rsdata.DeltaX;
                            Para_MainPara.gw4_LVDataDisplay[2] = rsdata.DeltaY;
                            Para_MainPara.gw4_LVDataDisplay[3] = 1;
                            Para_MainPara.gw4_LVDataDisplay[4]++;
                            //Para_MainPara.gw1_LVDataDisplay[5]++;
                            Para_MainPara.gw4_LVDataDisplay[6]++;
                            Para_MainPara.gw4_LVDataDisplay[7] =
                                Math.Round(
                            Para_MainPara.gw4_LVDataDisplay[4] /
                            Para_MainPara.gw4_LVDataDisplay[6]
                                , 3) * 100;


                        }
                        else//检测失败
                        {

                            //  0-gw 1-x 2-y  3-jieguo   4-ok  5-ng  6-all 7-%  
                            Para_MainPara.gw4_LVDataDisplay[0] = 32;
                            Para_MainPara.gw4_LVDataDisplay[1] = rsdata.DeltaX;
                            Para_MainPara.gw4_LVDataDisplay[2] = rsdata.DeltaY;
                            Para_MainPara.gw4_LVDataDisplay[3] = 2;
                            //Para_MainPara.gw1_LVDataDisplay[4]++;
                            Para_MainPara.gw4_LVDataDisplay[5]++;
                            Para_MainPara.gw4_LVDataDisplay[6]++;
                            Para_MainPara.gw4_LVDataDisplay[7] =
                                Math.Round(
                            Para_MainPara.gw4_LVDataDisplay[4] /
                            Para_MainPara.gw4_LVDataDisplay[6]
                                , 3) * 100;

                        }
                        datagridViewPlay(index_gw4, Para_MainPara.gw4_LVDataDisplay);

                        break;


                    case 5://工位4

                        if (rsdata.PLC_Serial == "1")//检测成功
                        {

                            //0-gw 1-x 2-y  3-jieguo   4-ok  5-ng  6-all 7-%  
                            Para_MainPara.gw5_LVDataDisplay[0] = 4;
                            Para_MainPara.gw5_LVDataDisplay[1] = rsdata.DeltaX;
                            Para_MainPara.gw5_LVDataDisplay[2] = rsdata.DeltaY;
                            Para_MainPara.gw5_LVDataDisplay[3] = 1;
                            Para_MainPara.gw5_LVDataDisplay[4]++;
                            //Para_MainPara.gw1_LVDataDisplay[5]++;
                            Para_MainPara.gw5_LVDataDisplay[6]++;
                            Para_MainPara.gw5_LVDataDisplay[7] =
                                Math.Round(
                            Para_MainPara.gw5_LVDataDisplay[4] /
                            Para_MainPara.gw5_LVDataDisplay[6]
                                , 3) * 100;


                        }
                        else//检测失败
                        {

                            //  0-gw 1-x 2-y  3-jieguo   4-ok  5-ng  6-all 7-%  
                            Para_MainPara.gw5_LVDataDisplay[0] = 4;
                            Para_MainPara.gw5_LVDataDisplay[1] = rsdata.DeltaX;
                            Para_MainPara.gw5_LVDataDisplay[2] = rsdata.DeltaY;
                            Para_MainPara.gw5_LVDataDisplay[3] = 2;
                            //Para_MainPara.gw1_LVDataDisplay[4]++;
                            Para_MainPara.gw5_LVDataDisplay[5]++;
                            Para_MainPara.gw5_LVDataDisplay[6]++;
                            Para_MainPara.gw5_LVDataDisplay[7] =
                                Math.Round(
                            Para_MainPara.gw5_LVDataDisplay[4] /
                            Para_MainPara.gw5_LVDataDisplay[6]
                                , 3) * 100;

                        }
                        datagridViewPlay(index_gw5, Para_MainPara.gw5_LVDataDisplay);

                        break;



                    default:
                        break;
                }





            }
            catch (Exception)
            {


            }




        }


        #endregion


    }
}
