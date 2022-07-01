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
using GVS.HalconDisp.Control;
using System.Threading;
using System.IO;
using SvDetectionModuleTool;
using GVS.HalconDisp.ViewROI.Config;
using System.Net.Sockets;
using System.Net;

namespace VisionStudio
{
    /// <summary>
    /// 采集模式
    /// </summary>
    public enum EnumAcq
    {
        自动运行,
        持续采集,
        停止采集,
        无,
    }

    /// <summary>
    /// 内 容:本类是软件主界面
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public partial class Form1 : Form
    {

        /// <summary>
        /// 相机采集模式
        /// </summary>
        EnumAcq m_EnumAcq = new EnumAcq();

        /// <summary>
        /// 方法类
        /// </summary>
        private Method m_method = new Method();

        /// <summary>
        /// 登录状态
        /// </summary>
        private bool m_bRegiste = false;

        private string m_serviceIP = "127.0.0.1";
        private string m_servicePort = "5567";

        #region 多窗口

        /// <summary>
        /// 多窗口控件
        /// </summary>
        private GVS.HalconDisp.Control.ListPicCtrl m_MoreDispCtrl = new GVS.HalconDisp.Control.ListPicCtrl();

        /// <summary>
        /// 窗口集合
        /// </summary>
        private List<HWindow_Final> m_ListPic = new List<HWindow_Final>();

        #endregion

        #region 多相机图片及采集状态标志

        /// <summary>
        /// 输入图像
        /// </summary>
        private HObject[] m_ArrayhoImage = new HObject[8];

        /// <summary>
        /// 自动运行触发成功标志
        /// </summary>
        private bool[] m_bArrayCameraTriggerState = new bool[8];

        /// <summary>
        /// All图片分类保存
        /// </summary>
        private bool[] m_bAllImage = new bool[8];

        /// <summary>
        /// 删除保存图片标志
        /// </summary>
        private bool[] m_bArraySaveDeleteImage = new bool[8];

        #endregion

        #region 线程定义

        //List<Thread> m_thread = new List<Thread>(8);
        Thread[] m_thread = new Thread[8];
        /// <summary>
        /// 保存删除图片
        /// </summary>
        Thread m_ThreadSaveAndDelete = null;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        #region 界面控件    

        // 窗体加载
        private void Form1_Load(object sender, EventArgs e)
        {
            HOperatorSet.SetSystem("flush_graphic", "true");
            HOperatorSet.SetSystem("parallelize_operators", "true");
            HOperatorSet.SetSystem("border_shape_models", "true");
            grp_Disp.Controls.Clear();
            m_method.Paras = (Para)m_method.Load(m_method.Paras.GetType(), Application.StartupPath + "\\半自动线");
            UpdateDoLog("加载参数", m_method.RunMsg);

            // 窗口更新
            UpdateDispCtrl();

            // 窗口对应检测按钮个数更新,PLc设置
            UpdateFuncNum();

            if (m_method.Paras.PLCOneState)
            {
                // 通讯设置
                PLCSet();
            }

            for (int i = 0; i < m_ArrayhoImage.Length; i++)
            {
                m_ArrayhoImage[i] = null;
                m_bArrayCameraTriggerState[i] = false;
                m_bArraySaveDeleteImage[i] = false;
                m_bAllImage[i] = false;
            }

            // 相机连接更新
            UpdateCamera();
            //设置相机参数
            setCCDParas();

            // 等待相机连接
            // Thread.Sleep(10);

            // 切换至自动运行窗口
            m_EnumAcq = EnumAcq.自动运行;

            // 更新上方按钮状态
            UpdateCtrlState(false);

            // 更新相机按钮状态
            tsp_Auto_Click(null, null);

            // 图像保存删除线程开启
            m_ThreadSaveAndDelete = new Thread(SaveAndDeleteImage);
            m_ThreadSaveAndDelete.Start();
            m_ThreadSaveAndDelete.IsBackground = true;

            // 检测线程开启
            ThreadStart();

            if(m_method.Paras.EnumTriggerMode == EnumTrigger.软触发) {
                TCPConnectService(m_serviceIP,m_servicePort);
            }
        }

        #region 拖拽图片，form属性AllowDrag设为true,再添加以下
        // 接受拖动到该界面的数据
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string m_strPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (File.Exists(m_strPath))
            {
                try
                {
                    HObject hoImage = null;
                    HOperatorSet.ReadImage(out hoImage, m_strPath);
                    for (int i = 0; i < m_method.Paras.CameraNum; i++)
                    {
                        m_ListPic[i].HobjectToHimage(hoImage);
                        m_ArrayhoImage[i] = hoImage;
                        m_method.InputImage = m_ArrayhoImage[i];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请检查是否为图片类型！" + "\r" + ex.ToString());
                }
            }
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        #endregion

        /// <summary>
        /// 单个连接PLC设置
        /// </summary>
        private void PLCSet()
        {
            通讯ToolStripMenuItem.DropDownItems.Clear();
            ToolStripMenuItem tspPLC = new ToolStripMenuItem("CCD" + "通讯");
            tspPLC.Click += TspPLC_Click;
            通讯ToolStripMenuItem.DropDownItems.Add(tspPLC);
            string strPLCMsg = m_method.m_listPlcMethod[0].Connect();
            UpdateHardAndSendLog("PLC", strPLCMsg);
            if (m_method.m_listPlcMethod[0].ConnectState)
            {
                Thread PLCReceiveThread = new Thread(m_method.m_listPlcMethod[0].ReceiveData);
                PLCReceiveThread.Start();
                PLCReceiveThread.IsBackground = true;
            }
        }

        /// <summary>
        /// 相机连接
        /// </summary>
        private void UpdateCamera()
        {
            for (int i = 1; i <= m_method.Paras.CameraNum; i++)
            {
                m_bArrayCameraTriggerState[i - 1] = false;
                if (m_method.m_ArrayCameraMethod[i - 1] == null)
                {
                    m_method.m_ArrayCameraMethod[i - 1] = new SvsMVSCamera.CameraAPI("CCD" + i);
                }
                var currentCCD = m_method.m_ArrayCameraMethod[i - 1];
                // 相机连接
                if (currentCCD != null)
                {
                    if (currentCCD.ifccdConnected)
                    {
                        if (currentCCD.m_bGrabbing)
                        {
                            currentCCD.StopGrab();
                        }
                        if (!currentCCD.m_bOpenCamera)
                        {
                            currentCCD.OpenCam();
                        }
                        if (currentCCD != null)
                        {
                            if (currentCCD.ifccdConnected)
                            {
                                UpdateHardAndSendLog("相机", "CCD" + i + "连接成功");
                            }
                            else
                            {
                                UpdateHardAndSendLog("相机", "CCD" + i + "连接失败");
                            }

                            // 设置相机心跳时间
                            currentCCD.SetHeartBeatTime(5000);

                            // 注册halcon显示回调函数
                            currentCCD.eventProcessHImage += eventGetImage;
                        }
                        else
                        {
                            currentCCD = null;
                            UpdateHardAndSendLog("相机", "CCD" + i + "连接失败");
                        }
                    }
                    else
                    {
                        currentCCD = null;
                        UpdateHardAndSendLog("相机", "CCD" + i + "连接失败");
                    }
                }
            }
        }

        /// <summary>
        /// 更新相机按钮状态
        /// </summary>
        /// <param name="bAuto"></param>
        /// <param name="bContinous"></param>
        /// <param name="bStop"></param>
        private void UpadteCameraState(bool bAuto, bool bContinous, bool bStop)
        {
            tsp_Auto.Enabled = bAuto;
            tsp_Continous.Enabled = bContinous;
            tsp_stop.Enabled = bStop;
        }

        /// <summary>
        /// 设置相机参数
        /// </summary>
        private void setCCDParas() 
        {
            for(int i = 1; i <= m_method.Paras.CameraNum; i++) {
                SvsMVSCamera.CameraAPI currentCCD = m_method.m_ArrayCameraMethod[i - 1];
                if(currentCCD != null) {
                    if(currentCCD.ifccdConnected) {
                        currentCCD.setExposureTime((long)m_method.Paras.ArrayModulePara[i - 1].cameraExposureTime);
                        currentCCD.setGain((long)m_method.Paras.ArrayModulePara[i - 1].cameraGain);
                    }
                }
            }
        }


        /// <summary>
        /// 显示窗口更新
        /// 图像、检测更新
        /// </summary>
        private void UpdateDispCtrl()
        {
            m_MoreDispCtrl.Describe = "CCD";
            grp_Disp.Controls.Add(m_MoreDispCtrl);
            m_MoreDispCtrl.Dock = DockStyle.Fill;
            m_MoreDispCtrl.UpdataImageDisp(m_method.Paras.CameraNum, ref m_ListPic);
        }

        /// <summary>
        /// 检测按钮个数更新
        /// </summary>
        private void UpdateFuncNum()
        {
            this.图像ToolStripMenuItem.DropDownItems.Clear();
            检测ToolStripMenuItem.DropDownItems.Clear();
            手动ToolStripMenuItem.DropDownItems.Clear();
            for (int i = 0; i < m_method.Paras.CameraNum; i++)
            {
                int j = i + 1;
                ToolStripMenuItem tspImage = new ToolStripMenuItem("CCD" + j + "打开图像");
                ToolStripMenuItem tspTest = new ToolStripMenuItem("CCD" + j + "检测");
                ToolStripMenuItem tspManual = new ToolStripMenuItem("CCD" + j + "测试");
                if (!m_method.Paras.PLCOneState)
                {
                    通讯ToolStripMenuItem.DropDownItems.Clear();
                    ToolStripMenuItem tspPLC = new ToolStripMenuItem("CCD" + j + "通讯");
                    tspPLC.Click += TspPLCMore_Click;
                    string strPLCMsg = m_method.m_listPlcMethod[i].Connect();
                    UpdateHardAndSendLog("PLC", "CCD" + j + strPLCMsg);
                    if (m_method.m_listPlcMethod[i].ConnectState)
                    {
                        Thread PLCReceiveThread = new Thread(m_method.m_listPlcMethod[i].ReceiveData);
                        PLCReceiveThread.Start();
                        PLCReceiveThread.IsBackground = true;
                    }
                    通讯ToolStripMenuItem.DropDownItems.Add(tspPLC);
                }
                tspImage.Click += tspImage_Click;
                tspTest.Click += TspTest_Click;
                tspManual.Click += TspManual_Click;
                图像ToolStripMenuItem.DropDownItems.Add(tspImage);
                检测ToolStripMenuItem.DropDownItems.Add(tspTest);
                手动ToolStripMenuItem.DropDownItems.Add(tspManual);
            }
        }

        // 手动测试按钮
        private void TspManual_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tspManual = sender as ToolStripMenuItem;
            string strIndex = tspManual.Text.Substring(3, 1);
            m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].InputImage = m_ArrayhoImage[int.Parse(strIndex) - 1];
            m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].ModualPara = m_method.Paras.ArrayModulePara[int.Parse(strIndex) - 1];
            m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].StateAngleLX = true;
            m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].StateDistancePL = true;
            m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].StateDistancePP = true;
            m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].StateJudgeResult = true;
            bool bstate = m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].Run();
            UpdateDoLog("手动", "手动测试" + (int.Parse(strIndex) - 1));
            if (!m_method.Paras.PLCOneState)
            {
                if (m_method.m_listPlcMethod[int.Parse(strIndex) - 1].ConnectState)
                {
                    SendPLCData(int.Parse(strIndex) - 1, 1);
                    //m_method.m_listPlcMethod[int.Parse(strIndex) - 1].SendData(int.Parse(strIndex) - 1, 1);
                    UpdateDoLog("PLC", "数据发送" + m_method.m_listPlcMethod[int.Parse(strIndex) - 1].RunMsg);
                }
            }
            else
            {
                if (m_method.m_listPlcMethod[0].ConnectState)
                {
                    SendPLCData(1, int.Parse(strIndex) - 1);
                    // m_method.m_listPlcMethod[0].SendData(int.Parse(strIndex) - 1, m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].SendHTuple);
                    UpdateDoLog("PLC", "数据发送" + m_method.m_listPlcMethod[0].RunMsg);
                }
            }
            UpdateResultGraphic(m_ListPic[int.Parse(strIndex) - 1], m_ArrayhoImage[int.Parse(strIndex) - 1], int.Parse(strIndex) - 1);
            UpdateResultData();
        }

        /// <summary>
        /// PLC设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspPLC_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tspPLC = sender as ToolStripMenuItem;
            string strIndex = tspPLC.Text.Substring(3, 1);
            SvsPLC.PLCForm dlgPlc = new SvsPLC.PLCForm(Application.StartupPath);
            dlgPlc.m_Method = m_method.m_listPlcMethod[0];
            dlgPlc.m_Method.Para = m_method.Paras.ArrayModulePara[0].ParasPlc;
            dlgPlc.ShowDialog();
            UpdateDoLog("通讯", "PLC设置" + (0));
        }

        /// <summary>
        /// PLC设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspPLCMore_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tspPLC = sender as ToolStripMenuItem;
            string strIndex = tspPLC.Text.Substring(3, 1);
            SvsPLC.PLCForm dlgPlc = new SvsPLC.PLCForm(Application.StartupPath);
            dlgPlc.m_Method = m_method.m_listPlcMethod[int.Parse(strIndex) - 1];
            dlgPlc.m_Method.Para = m_method.Paras.ArrayModulePara[int.Parse(strIndex) - 1].ParasPlc;
            dlgPlc.m_Method.Para.listRegister = m_method.Paras.ArrayModulePara[int.Parse(strIndex) - 1].ParasPlc.listRegister;
            dlgPlc.ShowDialog();
            UpdateDoLog("通讯", "PLC设置" + (int.Parse(strIndex) - 1));
        }

        /// <summary>
        /// 结果数据更新至界面
        /// </summary>
        private void UpdateResultData()
        {
            dgv_result.Rows.Clear();
            int iIndex = dgv_result.Rows.Add(m_method.Paras.CameraNum);
            for (int i = 0; i < m_method.Paras.CameraNum; i++)
            {
                dgv_result.Rows[i].Cells[0].Value = i;
                dgv_result.Rows[i].Cells[1].Value = m_method.m_ListModuleMethod[i].SendMsg;
            }
        }

        /// <summary>
        /// 图像读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspImage_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem tspImage = sender as ToolStripMenuItem;
                OpenFileDialog opl = new OpenFileDialog();
                if (opl.ShowDialog() == DialogResult.OK)
                {
                    HObject hoImage = null;
                    HOperatorSet.ReadImage(out hoImage, opl.FileName);
                    string strIndex = tspImage.Text.Substring(3, 1);
                    m_ArrayhoImage[int.Parse(strIndex) - 1] = null;
                    m_ArrayhoImage[int.Parse(strIndex) - 1] = hoImage;
                    m_ListPic[int.Parse(strIndex) - 1].HobjectToHimage(m_ArrayhoImage[int.Parse(strIndex) - 1]);
                    UpdateDoLog("图像", "图像读取设置" + (int.Parse(strIndex) - 1));
                    m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].InputImage = null;
                    m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].InputImage = m_ArrayhoImage[int.Parse(strIndex) - 1];
                    HTuple hvRow = null, hvCol = null;
                    HOperatorSet.GetImageSize(hoImage, out hvCol, out hvRow);
                    HObject hoCross = null;
                    HOperatorSet.GenCrossContourXld(out hoCross, hvRow / 2, hvCol / 2, hvRow * 2, 0);
                    m_ListPic[int.Parse(strIndex) - 1].DispObj(hoCross, "red");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("图片读取" + ex.ToString());
                return;
            }
        }

        /// <summary>
        /// 工位检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspTest_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tspTest = sender as ToolStripMenuItem;
            string strIndex = tspTest.Text.Substring(3, 1);
            SvDetectionModuleToolForm dlg = new SvDetectionModuleToolForm();
            m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].PatMaxName = tspTest.Text;
            m_method.m_ListModuleMethod[int.Parse(strIndex) - 1].InputImage = m_ArrayhoImage[int.Parse(strIndex) - 1];
            dlg.m_SvDetectionModuleToolMethod = m_method.m_ListModuleMethod[int.Parse(strIndex) - 1];
            dlg.m_SvDetectionModuleToolMethod.InputImage = m_ArrayhoImage[int.Parse(strIndex) - 1];
            dlg.m_SvDetectionModuleToolMethod.ModualPara = m_method.Paras.ArrayModulePara[int.Parse(strIndex) - 1];
            dlg.ShowDialog();
            UpdateDoLog("检测", "检测设置" + (int.Parse(strIndex) - 1));
            UpdateDoLog("文件", "保存文件");
            if (MessageBox.Show("是否保存", "保存", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                bool bstate = m_method.Save(m_method.Paras, Application.StartupPath + "\\半自动线");
                if (bstate)
                {
                    //MessageBox.Show("保存成功！");
                    return;
                }
                else
                {
                    MessageBox.Show("保存失败！" + m_method.RunMsg);
                }
            }
        }

        /// <summary>
        /// 结果区域更新
        /// </summary>
        private void UpdateResultGraphic(GVS.HalconDisp.Control.HWindow_Final m_Disp, HObject hoImage, int iMethodIndex)
        {
            m_Disp.HobjectToHimage(hoImage);
            HTuple hvRow = null, hvCol = null;
            if (hoImage != null)
            {
                HOperatorSet.GetImageSize(hoImage, out hvCol, out hvRow);
                HObject hoCross = null;
                HOperatorSet.GenCrossContourXld(out hoCross, hvRow / 2, hvCol / 2, hvRow * 2, 0);
                m_Disp.DispObj(hoCross, "red");
                if (m_method.m_ListModuleMethod[iMethodIndex].ModualPara.BlobState)
                {
                    foreach (HObjectWithColor item in m_method.m_ListModuleMethod[iMethodIndex].ListBlobReg)
                    {
                        m_Disp.DispObj(item.HObject, item.Color, "fill");
                    }
                }
                if (m_method.m_ListModuleMethod[iMethodIndex].ModualPara.CircleState)
                {
                    foreach (HObjectWithColor item in m_method.m_ListModuleMethod[iMethodIndex].ListCircleReg)
                    {
                        m_Disp.DispObj(item.HObject, item.Color, "margin");
                    }
                }
                if (m_method.m_ListModuleMethod[iMethodIndex].ModualPara.LineState)
                {
                    foreach (HObjectWithColor item in m_method.m_ListModuleMethod[iMethodIndex].ListLineReg)
                    {
                        m_Disp.DispObj(item.HObject, item.Color, "margin");
                    }
                }
                if (m_method.m_ListModuleMethod[iMethodIndex].ModualPara.PatMaxState)
                {
                    foreach (HObjectWithColor item in m_method.m_ListModuleMethod[iMethodIndex].ListPatMaxReg)
                    {
                        m_Disp.DispObj(item.HObject, item.Color, "margin");
                    }
                }
                if (m_method.m_ListModuleMethod[iMethodIndex].ModualPara.AngleLXState)
                {
                    foreach (HObjectWithColor item in m_method.m_ListModuleMethod[iMethodIndex].ListAngleLXReg)
                    {
                        m_Disp.DispObj(item.HObject, item.Color, "margin");
                    }
                }
                if (m_method.m_ListModuleMethod[iMethodIndex].ModualPara.DistancePLState)
                {
                    foreach (HObjectWithColor item in m_method.m_ListModuleMethod[iMethodIndex].ListDistancePLReg)
                    {
                        m_Disp.DispObj(item.HObject, item.Color, "margin");
                    }
                }
                if (m_method.m_ListModuleMethod[iMethodIndex].ModualPara.DistancePPState)
                {
                    foreach (HObjectWithColor item in m_method.m_ListModuleMethod[iMethodIndex].ListDistancePPReg)
                    {
                        m_Disp.DispObj(item.HObject, item.Color, "margin");
                    }
                }
                m_Disp.DisplayMessage(m_method.m_ListModuleMethod[iMethodIndex].SendMsg, 20, 20, "blue", true, "宋体", "Normal", 7);
                m_Disp.DisplayMessage(m_method.m_ListModuleMethod[iMethodIndex].RunMsg, 150, 20, "blue", true, "宋体", "Normal", 7);
                UpdateDoLog("CCD" + (iMethodIndex + 1) + "运行消息", m_method.m_ListModuleMethod[iMethodIndex].RunMsg);
            }
        }

        // 相机重连
        private void 相机重连ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDoLog("文件", "相机重连");
            UpdateCamera();
            ThreadStart();
        }

        // 图像保存设置
        private void 图像保存设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageSaveForm dlg = new ImageSaveForm();
            dlg.m_Method = m_method;
            dlg.m_Method.Paras.ParasImageSave = m_method.Paras.ParasImageSave;
            dlg.ShowDialog();
            UpdateDoLog("文件", "图像保存设置");
        }

        #region 相机回调函数

        /// <summary>
        /// 硬件日志更新
        /// </summary>
        /// <param name="Type">日志类型</param>
        /// <param name="Describe">日志描述</param>
        private void UpdateHardAndSendLog(string Type, string Describe)
        {
            CommonMethod.CommonMethods.WriteLogs("Log", Type, Describe);
            lsb_Log.Items.Add(DateTime.Now.ToString("MM-dd HH:mm:ss:") + Describe);
            lsb_Log.SelectedIndex = lsb_Log.Items.Count - 1;
        }

        /// <summary>
        /// 操作日志更新
        /// </summary>
        /// <param name="Type">日志类型</param>
        /// <param name="Describe">日志描述</param>
        private void UpdateDoLog(string Type, string Describe)
        {
            CommonMethod.CommonMethods.WriteLogs("LogDo", Type, Describe);
            lsb_Do.Items.Add(DateTime.Now.ToString("MM-dd HH:mm:ss:") + Describe);
            lsb_Do.SelectedIndex = lsb_Do.Items.Count - 1;
        }

        // 相机1获取图像
        private void eventGetImage(HObject hoImage , int CcdId)
        {
            int indexOfCcd = CcdId - 1;
            switch (m_EnumAcq)
            {
                case EnumAcq.持续采集:
                    m_ArrayhoImage[indexOfCcd] = hoImage;
                    BeginInvoke(new Action(() =>
                    {
                        m_ListPic[indexOfCcd].HobjectToHimage(hoImage);
                        HTuple hvRow = null, hvCol = null;
                        HOperatorSet.GetImageSize(hoImage,out hvCol,out hvRow);
                        HObject hoCross = null;
                        HOperatorSet.GenCrossContourXld(out hoCross,hvRow / 2,hvCol / 2,hvRow * 2,0);
                        m_ListPic[indexOfCcd].DispObj(hoCross,"red");
                    }));
                    break;
                case EnumAcq.自动运行:
                    m_ArrayhoImage[indexOfCcd] = hoImage;
                    // 触发成功，开启检测线程
                    m_bArrayCameraTriggerState[indexOfCcd] = true;
                    break;
            }
        }
        #endregion

        #region 线程开启及线程方法

        /// <summary>
        /// 线程开启
        /// </summary>
        private void ThreadStart()
        {
            int cameraNum = m_method.Paras.CameraNum;
            for(int i = 0;i < cameraNum; i++) 
            {
                if(m_thread[i] == null) {
                    m_thread[i] = new Thread(new ParameterizedThreadStart(RunCCD));
                    m_thread[i].Start(i);
                    m_thread[i].IsBackground = true;
                }
            }
            for(int i = cameraNum;i < 8; i++) 
            {
                if (m_thread[i] != null && m_thread[i].IsAlive)
                {
                    m_thread[i].Abort();
                    m_thread[i] = null;
                }
            }
        }

        /// <summary>
        /// 线程关闭
        /// </summary>
        private void ThreaAbort()
        {
            for (int i = 0; i < 8; i++)
            {
                if (m_thread[i] != null && m_thread[i].IsAlive)
                {
                    m_thread[i].Abort();
                }
            }
        }

        /// <summary>
        /// 相机方法
        /// </summary>
        private void RunCCD(object obj)
        {
            int runNum = (int)obj;
            while (true)
            {
                Thread.Sleep(1);
                lock (this)
                {
                    switch (m_EnumAcq)
                    {
                        case EnumAcq.自动运行:
                            if (m_method.Paras.EnumTriggerMode == EnumTrigger.软触发 && m_bArrayCameraTriggerState[runNum])
                            {
                                m_bArrayCameraTriggerState[runNum] = false;
                                m_method.m_ListModuleMethod[runNum].InputImage = m_ArrayhoImage[runNum];
                                m_method.m_ListModuleMethod[runNum].ModualPara = m_method.Paras.ArrayModulePara[runNum];
                                bool bState = m_method.m_ListModuleMethod[runNum].Run();
                                SendMsg(sendHtupleByClient(runNum));
                                BeginInvoke(new Action(() =>
                                {
                                    UpdateResult(runNum);
                                    UpdateResultData();
                                }));
                                break;
                            }

                            if (m_bArrayCameraTriggerState[runNum])
                            {
                                m_bArrayCameraTriggerState[runNum] = false;
                                m_method.m_ListModuleMethod[runNum].InputImage = m_ArrayhoImage[runNum];
                                m_method.m_ListModuleMethod[runNum].ModualPara = m_method.Paras.ArrayModulePara[runNum];
                                bool bState = m_method.m_ListModuleMethod[runNum].Run();
                                if (!m_method.Paras.PLCOneState)
                                {
                                    SendPLCData(runNum,1);
                                }
                                else
                                {
                                    SendPLCData(0,runNum + 1);
                                }
                                BeginInvoke(new Action(() =>
                                {
                                    UpdateResult(runNum);
                                    UpdateResultData();
                                }));
                                switch (m_method.Paras.ParasImageSave.ImageSaveTypes)
                                {
                                    case ParaImageSave.ImageSaveType.OK:
                                        if (bState)
                                        {
                                            m_bArraySaveDeleteImage[runNum] = true;
                                        }
                                        break;
                                    case ParaImageSave.ImageSaveType.NG:
                                        if (!bState)
                                        {
                                            m_bArraySaveDeleteImage[runNum] = true;
                                        }
                                        break;
                                    case ParaImageSave.ImageSaveType.ALL:
                                        m_bAllImage[runNum] = bState;
                                        m_bArraySaveDeleteImage[runNum] = true;
                                        break;
                                    case ParaImageSave.ImageSaveType.NO:
                                        m_bArraySaveDeleteImage[runNum] = false;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;

                    }
                }
            }
        }

        /// <summary>
        /// 更新对应数据
        /// </summary>
        /// <param name="iCCD">相机索引</param>
        private void UpdateResult(int iCCD)
        {
            int j = iCCD + 1;
            UpdateResultGraphic(m_ListPic[iCCD], m_ArrayhoImage[iCCD], iCCD);
            if (!m_method.Paras.PLCOneState)
            {
                UpdateDoLog("PLC", "CCD" + j + " 发送数据" + m_method.m_listPlcMethod[iCCD].RunMsg);
            }
            else
            {
                UpdateDoLog("PLC", "CCD" + j + " 发送数据" + m_method.m_listPlcMethod[0].RunMsg);
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="iCCD">共用一个服务器填入0，否则填入每个相机的索引，索引从0开始</param>
        /// <param name="iPLC">每个相机对应的索引，如CCD1 则填入1，索引从1开始</param>
        private void SendPLCData(int iCCD, int iPLC)
        {
            if (m_method.m_listPlcMethod[iCCD].ConnectState)
            {
                m_method.m_listPlcMethod[iCCD].SendData(iPLC, m_method.m_ListModuleMethod[iPLC - 1].SendHTuple);
            }
        }

        #endregion

        #region 相机保存删除图片线程及相关方法

        /// <summary>
        /// 保存删除图片
        /// </summary>
        private void SaveAndDeleteImage()
        {
            while (true)
            {
                Thread.Sleep(1);
                switch (m_EnumAcq)
                {
                    case EnumAcq.自动运行:
                        for(int i = 0; i < 8;i++) {
                            int j = i + 1;
                            SaveImage(i,"CCD" + j.ToString());
                        }
                        break;
                    default:                        
                        break;
                }

                for (int i = 0; i < 8; i++)
                {
                    int j = i + 1;
                    DeleteImage("CCD" + j.ToString());
                }
            }
        }

        /// <summary>
        /// 保存图片方法
        /// </summary>
        private void SaveImage(int i, string strCCD)
        {
            if (m_bArraySaveDeleteImage[i])
            {
                string strFile = string.Empty;
                if (m_method.Paras.ParasImageSave.ImageSaveTypes == ParaImageSave.ImageSaveType.ALL)
                {
                    if (m_bAllImage[i])
                    {
                        // 启用保存
                        strFile = Directory.GetCurrentDirectory() + "\\" +
                                  m_method.Paras.ParasImageSave.ImageSaveTypes.ToString() + "\\" + strCCD + "\\" +
                                  DateTime.Now.ToString("yyyyMMdd")+"\\OK";
                    }
                    else
                    {
                        // 启用保存
                        strFile = Directory.GetCurrentDirectory() + "\\" +
                                  m_method.Paras.ParasImageSave.ImageSaveTypes.ToString() + "\\" + strCCD + "\\" +
                                  DateTime.Now.ToString("yyyyMMdd")+ "\\NG";
                    }
                }
                else
                {
                    strFile = Directory.GetCurrentDirectory() + "\\" +
                              m_method.Paras.ParasImageSave.ImageSaveTypes.ToString() + "\\" + strCCD + "\\" +
                              DateTime.Now.ToString("yyyyMMdd");
                }
                string strImageName = DateTime.Now.ToString("HHmmssfff");
                if (m_ArrayhoImage[i] != null)
                {
                    if (m_bArraySaveDeleteImage[i])
                    {
                        if (Directory.Exists(strFile))
                        {
                            HOperatorSet.WriteImage(m_ArrayhoImage[i], "bmp", 0,
                            strFile + "\\" + strImageName);
                        }
                        else
                        {
                            Directory.CreateDirectory(strFile);
                            if (Directory.Exists(strFile))
                            {
                                HOperatorSet.WriteImage(m_ArrayhoImage[i], "bmp", 0,
                                strFile + "\\" + strImageName);
                            }
                        }
                    }
                }
                m_bArraySaveDeleteImage[i] = false;
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        private void DeleteImage(string strCCD)
        {
            string strSaveFile = Directory.GetCurrentDirectory();
            if (Directory.Exists(strSaveFile + "\\" + "NG" + "\\" + strCCD + "\\"))
            {
                DirectoryInfo root = new DirectoryInfo(strSaveFile + "\\" + "NG" + "\\" + strCCD + "\\");
                foreach (DirectoryInfo d in root.GetDirectories())
                {
                    if (int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(d.ToString()) >= m_method.Paras.ParasImageSave.SaveDay)
                    {
                        Directory.Delete(strSaveFile + "\\" + "NG" + "\\" + strCCD + "\\" + d.ToString(), true);
                    }
                }
            }
            if (Directory.Exists(strSaveFile + "\\" + "OK" + "\\CCD1\\"))
            {
                DirectoryInfo root = new DirectoryInfo(strSaveFile + "\\" + "OK" + "\\" + strCCD + "\\");
                foreach (DirectoryInfo d in root.GetDirectories())
                {
                    if (int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(d.ToString()) >= m_method.Paras.ParasImageSave.SaveDay)
                    {
                        Directory.Delete(strSaveFile + "\\" + "OK" + "\\" + strCCD + "\\" + d.ToString(), true);
                    }
                }
            }
            if (Directory.Exists(strSaveFile + "\\" + "ALL" + "\\" + strCCD + "\\"))
            {
                DirectoryInfo root = new DirectoryInfo(strSaveFile + "\\" + "ALL" + "\\" + strCCD + "\\");
                foreach (DirectoryInfo d in root.GetDirectories())
                {
                    if (int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(d.ToString()) >= m_method.Paras.ParasImageSave.SaveDay)
                    {
                        Directory.Delete(strSaveFile + "\\" + "ALL" + "\\" + strCCD + "\\" + d.ToString(), true);
                    }
                }
            }


            if (Directory.Exists(strSaveFile + "\\" + "NO" + "\\" + strCCD + "\\"))
            {
                DirectoryInfo root = new DirectoryInfo(strSaveFile + "\\" + "NO" + "\\" + strCCD + "\\");
                foreach (DirectoryInfo d in root.GetDirectories())
                {
                    if (int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(d.ToString()) >= m_method.Paras.ParasImageSave.SaveDay)
                    {
                        Directory.Delete(strSaveFile + "\\" + "NO" + "\\" + strCCD + "\\" + d.ToString(), true);
                    }
                }
            }
        }

        #endregion

        #region 相机个数切换

        // 1个相机
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            m_method.Paras.CameraNum = 1;
            UpdateDoLog("相机个数", "相机个数" + m_method.Paras.CameraNum.ToString());
            UpdateDispCtrl();
            UpdateFuncNum();
            UpdateCamera();
            ThreadStart();
        }

        // 2个相机
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            m_method.Paras.CameraNum = 2;
            UpdateDoLog("相机个数", "相机个数" + m_method.Paras.CameraNum.ToString());
            UpdateDispCtrl();
            UpdateFuncNum();
            UpdateCamera();
            ThreadStart();
        }

        // 3个相机
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            m_method.Paras.CameraNum = 3;
            UpdateDoLog("相机个数", "相机个数" + m_method.Paras.CameraNum.ToString());
            UpdateDispCtrl();
            UpdateFuncNum();
            UpdateCamera();
            ThreadStart();
        }

        // 4个相机
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            m_method.Paras.CameraNum = 4;
            UpdateDoLog("相机个数", "相机个数" + m_method.Paras.CameraNum.ToString());
            UpdateDispCtrl();
            UpdateFuncNum();
            UpdateCamera();
            ThreadStart();
        }

        // 5个相机
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            m_method.Paras.CameraNum = 5;
            UpdateDoLog("相机个数", "相机个数" + m_method.Paras.CameraNum.ToString());
            UpdateDispCtrl();
            UpdateFuncNum();
            UpdateCamera();
            ThreadStart();
        }

        // 6个相机
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            m_method.Paras.CameraNum = 6;
            UpdateDoLog("相机个数", "相机个数" + m_method.Paras.CameraNum.ToString());
            UpdateDispCtrl();
            UpdateFuncNum();
            UpdateCamera();
            ThreadStart();
        }

        // 7个相机
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            m_method.Paras.CameraNum = 7;
            UpdateDoLog("相机个数", "相机个数" + m_method.Paras.CameraNum.ToString());
            UpdateDispCtrl();
            UpdateFuncNum();
            UpdateCamera();
            ThreadStart();
        }

        // 8个相机
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            m_method.Paras.CameraNum = 8;
            UpdateDoLog("相机个数", "相机个数" + m_method.Paras.CameraNum.ToString());
            UpdateDispCtrl();
            UpdateFuncNum();
            UpdateCamera();
            ThreadStart();
        }

        #endregion

        #region 相机状态切换
        // 持续采集
        private void tsp_Continous_Click(object sender, EventArgs e)
        {
            UpdateDoLog("相机", "设置持续采集");
            //UpdateCtrlState(false);
            // 更新相机按钮状态
            UpadteCameraState(false, false, true);
            m_EnumAcq = EnumAcq.持续采集;
            UpdateCtrlState(false);
            for (int i = 0; i < m_method.Paras.CameraNum; i++)
            {
                if (null != m_method.m_ArrayCameraMethod[i])
                {
                    if (m_method.m_ArrayCameraMethod[i].ifccdConnected)
                    {
                        m_method.m_ArrayCameraMethod[i].SetFreerun();
                        if (!m_method.m_ArrayCameraMethod[i].m_bGrabbing)
                        {
                            if (m_method.m_ArrayCameraMethod[i].ifccdConnected)
                            {
                                m_method.m_ArrayCameraMethod[i].StartGrab();
                            }
                        }
                    }
                }
            }
        }

        // 自动运行
        private void tsp_Auto_Click(object sender, EventArgs e)
        {
            UpdateDoLog("相机", "设置自动运行");
            //UpdateCtrlState(false);
            // 更新相机按钮状态
            UpadteCameraState(false, false, true);
            try
            {
                m_EnumAcq = EnumAcq.自动运行;
                UpdateCtrlState(false);
                for (int i = 0; i < m_method.Paras.CameraNum; i++)
                {
                    if (m_method.m_ArrayCameraMethod[i] != null)
                    {
                        if (m_method.m_ArrayCameraMethod[i].ifccdConnected)
                        {
                            if (m_method.m_ArrayCameraMethod[i].m_bGrabbing)
                            {
                                m_method.m_ArrayCameraMethod[i].StopGrab();
                            }
                            switch (m_method.Paras.EnumTriggerMode)
                            {
                                case EnumTrigger.硬触发:
                                    m_method.m_ArrayCameraMethod[i].SetExternTrigger();
                                    break;
                                case EnumTrigger.软触发:
                                    m_method.m_ArrayCameraMethod[i].SetSoftwareTrigger();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if (m_method.m_ArrayCameraMethod[i] != null)
                    {
                        if (m_method.m_ArrayCameraMethod[i].ifccdConnected)
                        {
                            if (!m_method.m_ArrayCameraMethod[i].m_bGrabbing)
                            {
                                m_method.m_ArrayCameraMethod[i].StartGrab();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        // 停止采集
        private void tsp_stop_Click(object sender, EventArgs e)
        {
            UpdateDoLog("相机", "设置停止采集");
            //if (m_bRegiste)
            //{
            //    UpdateCtrlState(true);
            //}
            // 更新相机按钮状态
            UpadteCameraState(true, true, true);
            m_EnumAcq = EnumAcq.停止采集;

            if (m_bRegiste)
            {
                UpdateCtrlState(true);
            }

            for (int i = 0; i < m_method.Paras.CameraNum; i++)
            {
                if (null != m_method.m_ArrayCameraMethod[i])
                {
                    if (m_method.m_ArrayCameraMethod[i].ifccdConnected)
                    {
                        if (m_method.m_ArrayCameraMethod[i].m_bGrabbing)
                        {
                            m_method.m_ArrayCameraMethod[i].StopGrab();
                            m_method.m_ArrayCameraMethod[i].SetFreerun();
                        }
                    }
                }
            }
        }
        #endregion

        #region 用户状态切换及相关按钮状态切换

        // 用户登录
        private void tsp_user_Click(object sender, EventArgs e)
        {
            UserForm dlg = new UserForm();
            dlg.eventParaUserArgs += UpdateLoginState;
            dlg.ShowDialog();
            UpdateDoLog("登录界面", "用户切换");
        }

        /// <summary>
        /// 更新登录状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UpdateLoginState(object sender, ParaUserArgs e)
        {
            m_bRegiste = e.Login;
            UpdateCtrlState(e.Login);
        }

        /// <summary>
        /// 更新上方按钮状态
        /// </summary>
        /// <param name="bstate">true：可用，false：不可用</param>
        void UpdateCtrlState(bool bstate)
        {
            检测ToolStripMenuItem.Enabled = bstate;
            设置ToolStripMenuItem.Enabled = bstate;
            文件ToolStripMenuItem.Enabled = bstate;
            通讯ToolStripMenuItem.Enabled = bstate;
            相机参数设置ToolStripMenuItem.Enabled = (m_EnumAcq != EnumAcq.自动运行) && m_bRegiste;
        }

        #endregion

        #endregion

        #region 辅助按钮
        private void 读取文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDoLog("文件", "读取文件");
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDoLog("文件", "保存文件");
            if (MessageBox.Show("是否保存", "保存", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                bool bstate = m_method.Save(m_method.Paras, Application.StartupPath + "\\半自动线");
                if (bstate)
                {
                    //MessageBox.Show("保存成功！");
                    return;
                }
                else
                {
                    MessageBox.Show("保存失败！" + m_method.RunMsg);
                }
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog opl = new SaveFileDialog();
            if (opl.ShowDialog() == DialogResult.OK)
            {
                bool bstate = m_method.Save(m_method.Paras, opl.FileName);
                if (bstate)
                {
                    // MessageBox.Show("保存成功！");
                    return;
                }
                else
                {
                    MessageBox.Show("保存失败！" + m_method.RunMsg);
                }
            }
            UpdateDoLog("文件", "另存为" + opl.FileName + m_method.RunMsg);
        }

        private void 加载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opl = new OpenFileDialog();
            if (opl.ShowDialog() == DialogResult.OK)
            {
                m_method.Paras = (Para)m_method.Load(m_method.Paras.GetType(), opl.FileName);
                if (m_method.RunMsg.Length > 0)
                {
                    MessageBox.Show("加载失败！" + m_method.RunMsg);
                }
                else
                {
                    MessageBox.Show("加载成功!");
                    return;
                }
            }
            UpdateDoLog("文件", "加载" + opl.FileName + m_method.RunMsg);
        }
        #endregion

        #region PLC切换
        private void 一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_method.Paras.PLCOneState = true;
            PLCSet();
        }

        private void 多个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_method.Paras.PLCOneState = false;
            通讯ToolStripMenuItem.DropDownItems.Clear();
            for (int i = 0; i < m_method.Paras.CameraNum; i++)
            {
                int j = i + 1;
                if (!m_method.Paras.PLCOneState)
                {
                    ToolStripMenuItem tspPLC = new ToolStripMenuItem("CCD" + j + "通讯");
                    通讯ToolStripMenuItem.DropDownItems.Add(tspPLC);
                    tspPLC.Click += TspPLCMore_Click;
                    if (m_method.m_listPlcMethod[i].ConnectState)
                    {
                        string strPLCMsg = m_method.m_listPlcMethod[i].Connect();
                        UpdateHardAndSendLog("PLC", "CCD" + j + strPLCMsg);
                        if (m_method.m_listPlcMethod[i].ConnectState)
                        {
                            Thread PLCReceiveThread = new Thread(m_method.m_listPlcMethod[i].ReceiveData);
                            PLCReceiveThread.Start();
                            PLCReceiveThread.IsBackground = true;
                        }
                    }
                    else
                    {
                        UpdateHardAndSendLog("PLC", "CCD" + j + "[PLC未连接]");
                    }
                }
            }
        }
        #endregion

        /// <summary>
        ///  窗体关闭
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_EnumAcq = EnumAcq.停止采集;
            for (int i = 1; i <= m_method.Paras.CameraNum; i++)
            {
                m_bArrayCameraTriggerState[i - 1] = false;
                // 相机连接
                if (m_method.m_ArrayCameraMethod[i - 1] != null)
                {
                    if (m_method.m_ArrayCameraMethod[i - 1].ifccdConnected)
                    {
                        if (m_method.m_ArrayCameraMethod[i - 1].m_bGrabbing)
                        {
                            m_method.m_ArrayCameraMethod[i - 1].StopGrab();
                        }
                        if (m_method.m_ArrayCameraMethod[i - 1].m_bOpenCamera)
                        {
                            m_method.m_ArrayCameraMethod[i - 1].CloseCam();
                        }
                    }
                }
            }
            ThreadClose();
            if (m_ThreadSaveAndDelete != null)
            {
                if (m_ThreadSaveAndDelete.IsAlive)
                {
                    m_ThreadSaveAndDelete.Abort();
                    m_ThreadSaveAndDelete = null;
                }
            }
            UpdateDoLog("文件", "保存文件");
            if (MessageBox.Show("是否保存", "保存", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                bool bstate = m_method.Save(m_method.Paras, Application.StartupPath + "\\半自动线");
                if (bstate)
                {
                    //MessageBox.Show("保存成功！");
                    return;
                }
                else
                {
                    MessageBox.Show("保存失败！" + m_method.RunMsg);
                }
            }
        }

        /// <summary>
        /// 关闭线程
        /// </summary>
        private void ThreadClose()
        {
            ThreaAbort();
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lsb_Do.Items.Clear();
            lsb_Log.Items.Clear();
        }

        private Socket client;
        Thread client_thread;

        bool IsTCPConnectOK = false;

        void TCPConnectService(string IpString, string port)// IpString "127.0.0.1" ; port "9050"
        {

            try
            {
                ///创建客户端
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                ///IP地址
                IPAddress ip = IPAddress.Parse(IpString);
                ///端口号
                IPEndPoint endPoint = new IPEndPoint(ip, int.Parse(port));
                ///建立与服务器的远程连接
                try
                {
                    client.Connect(endPoint);
                    IsTCPConnectOK = true;
                    UpdateDoLog("PLC","通信已连接");
                }
                catch (Exception)
                {
                    UpdateDoLog("PLC","通信连接失败");
                    IsTCPConnectOK = false;
                    return;
                }

                ///线程问题
                client_thread = new Thread(ReciveMsg);
                client_thread.IsBackground = true;
                client_thread.Start(client);

                // https://www.cnblogs.com/sclu/p/12217905.html
            }
            catch (Exception)
            {

            }
        }

        private void ReciveMsg(object o)
        {
            Socket client = o as Socket;
            while (true)
            {
                try
                {
                    ///定义客户端接收到的信息大小
                    byte[] arrList = new byte[1024 * 1024];
                    ///接收到的信息大小(所占字节数)
                    int length = client.Receive(arrList);
                    //string msg = DateTime.Now + Encoding.UTF8.GetString(arrList, 0, length);
                    string msg = Encoding.UTF8.GetString(arrList, 0, length);

                    // 解析收到数据进行判断
                    string[] dasdas = msg.Split('\0');

                    //  switch (recMsg)
                    switch (msg)
                    {

                        case "1":
                            triggerCamera0SendMessage();
                            break;

                        case "2":
                            triggerCamera0SendMessage();
                            break;

                        case "3":
                            triggerCamera0SendMessage();
                            break;

                        case "4":
                            triggerCamera0SendMessage();
                            break;

                        case "5":
                            triggerCamera0SendMessage();
                            break;

                        default:
                            break;
                    }
                    Thread.Sleep(20);
                }
                catch (Exception)
                {

                    ///关闭客户端
                    client.Close();
                    Thread.Sleep(50);

                }
            }
        }

        private void SendMsg(string str)
        {
            byte[] arrMsg = Encoding.UTF8.GetBytes(str);
            if (client != null) 
            {
                try {
                    client.Send(arrMsg);
                }
                catch {
                    UpdateDoLog("服务器","数据发送失败");
                }

            }
        }

        private HTuple sendHtupleByClient(int ccd)
        {
            string msg = m_method.m_ListModuleMethod[ccd].SendMsg;
            msg = msg.Remove(0, 1);
            msg = msg.Remove(msg.Length - 1, 1);
            string[] box = msg.Split(new char[] { ','});
            return msg;
        }

        private void triggerCamera0SendMessage()
        {
            if (m_EnumAcq == EnumAcq.自动运行)
            {
                if (m_method.m_ArrayCameraMethod[0].ifccdConnected)
                {
                    m_method.m_ArrayCameraMethod[0].SendSoftwareExecute();
                }
            }

        }

        private void 软触发测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_method.m_ArrayCameraMethod[0].ifccdConnected)
            {
                m_method.m_ArrayCameraMethod[0].SendSoftwareExecute();
            }

            SendMsg("a\\b\\c\\d");
        }

        private void 相机参数设置ToolStripMenuItem_Click(object sender,EventArgs e)
        {
            CameraSettingForm cameraSetting = new CameraSettingForm();
            cameraSetting.method = m_method;
            cameraSetting.triggerModeChanged += new EventHandler(
                (sender1,e1) => {
                    m_method.Paras.EnumTriggerMode = cameraSetting.triggerMode;
                    for (int i = 0; i < m_method.m_ArrayCameraMethod.Length; i++)
                    {
                        if (m_method.m_ArrayCameraMethod[i] != null)
                        {
                            if (cameraSetting.triggerMode == EnumTrigger.硬触发)
                            {
                                m_method.m_ArrayCameraMethod[i].SetExternTrigger();
                            }
                            else
                            {
                                m_method.m_ArrayCameraMethod[i].SetSoftwareTrigger();
                            }
                        }
                    }
                }
            );

            cameraSetting.ccdParasChanged += new EventHandler(
                (sender2,e2) => {
                    SvsMVSCamera.CameraAPI currentCCD = m_method.m_ArrayCameraMethod[cameraSetting.indexOfCCD - 1];
                    if(currentCCD != null) {
                        if(currentCCD.ifccdConnected) {
                            currentCCD.setExposureTime((long)cameraSetting.exposeTime);
                            currentCCD.setGain((long)cameraSetting.gain);
                            m_method.Paras.ArrayModulePara[cameraSetting.indexOfCCD - 1].cameraExposureTime = cameraSetting.exposeTime;
                            m_method.Paras.ArrayModulePara[cameraSetting.indexOfCCD - 1].cameraGain = cameraSetting.gain;
                            currentCCD.SetFreerun();
                        }
                    }
                }
            );
            cameraSetting.ShowDialog(this);
        }

        private void 服务器重连ToolStripMenuItem_Click(object sender,EventArgs e)
        {
            TCPConnectService(m_serviceIP,m_servicePort);
        }
    }
}
