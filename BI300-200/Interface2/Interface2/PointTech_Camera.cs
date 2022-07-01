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


namespace Interface2
{
    public partial class PointTech_Camera : Form
    {

        #region 传值事件

        MainPara Para_MainPara = new MainPara();

        public event EventHandler SendMsgToForm1_Event;

        internal void EventFromForm1Chaned(object sender, EventArgs e)
        {

            //取得子界面参数
            Para_MainPara = e as MainPara;

        }

        #endregion

        public PointTech_Camera()
        {
            InitializeComponent();
            mdispLoad();
            DelegateDeclaration();
        }

        private void PointTech_Camera_Load(object sender, EventArgs e)
        {

            this.Text = "相机调试设置";
            cmb_Image_Add();
            cmb_ShowImage_Add();
            cmbSet();

            update();
            toolTip();
        }

        private void PointTech_Camera_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            SetCameraStop("ccd1");
            SetCameraStop("ccd2");
            SetCameraStop("ccd3_1");
          
            if (numCamera>=5)
            {
            SetCameraStop("ccd3_2");
            }
           
            SetCameraStop("ccd4");

            PassData.OnlyUse1_FreeShow = "NO";
            PassData.OnlyUse1_HardTrigger = "NO";
        }


        #region 显示窗口加载

        private GVS.HalconDisp.Control.HWindow_Final m_Disp = new GVS.HalconDisp.Control.HWindow_Final();


        void mdispLoad()
        {

            panel3.Controls.Add(m_Disp);
            m_Disp.Dock = DockStyle.Fill;


        }


        int numCamera;
        void cmbSet()
        {
            numCamera = PassData.NumDisp;
            try
            {
                cB_camera.Items.Clear();
                if (numCamera>=5)
                {
                    cB_camera.Items.Add("ccd1");
                    cB_camera.Items.Add("ccd2");
                    cB_camera.Items.Add("ccd3_1");
                    cB_camera.Items.Add("ccd3_2");
                    cB_camera.Items.Add("ccd4");

                }
                else
                {
                    cB_camera.Items.Add("ccd1");
                    cB_camera.Items.Add("ccd2");
                    cB_camera.Items.Add("ccd3_1");
                    cB_camera.Items.Add("ccd4");
                }
                cB_camera.Text = (string)cB_camera.Items[0];

            }
            catch (Exception ex)
            {
              
            }
        
        }

        #endregion


        private void btn_CamFree_Click(object sender, EventArgs e)
        {
            SetCameraFree(cB_camera.Text);

        }

        private void btn_CamPause_Click(object sender, EventArgs e)
        {
            SetCameraStop(cB_camera.Text);

        }

        private void btn_CamTake_Click(object sender, EventArgs e)
        {
            SetCameraTake(cB_camera.Text);
        }

        private void btn_CamDetect_Click(object sender, EventArgs e)
        {
            //软触发试教
            SetCameraDetect(cB_camera.Text);

        }

        private void btn_HardTrigger_Click(object sender, EventArgs e)
        {
            //该硬触发用于示教，打开所有相机硬触发，当前使用相机硬触发时，显示在主、子界面，其他相机硬触发时显示在主界面
            SetCameraHardTriggerPT("ccd1");
            SetCameraHardTriggerPT("ccd2");
            SetCameraHardTriggerPT("ccd3_1");
            if (numCamera >= 5)
            {
                SetCameraHardTriggerPT("ccd3_2");
            }
            SetCameraHardTriggerPT("ccd4");

        }

        void SetCameraFree(string NoCam)
        {
            try
            {
                switch (NoCam)
                {
                    case "ccd1":
                        PassData.OnlyUse1_FreeShow = "ccd1";
                        PassData.m1_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd1_Free";
                        PassData.cameraSet++;

                        break;

                    case "ccd2":
                        PassData.OnlyUse1_FreeShow = "ccd2";

                        PassData.m2_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd2_Free";
                        PassData.cameraSet++;

                        break;

                    case "ccd3_1"://3-1
                        PassData.OnlyUse1_FreeShow = "ccd3_1";

                        PassData.m3_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd3_1_Free";
                        PassData.cameraSet++;
                        break;

                    case "ccd3_2"://3-2
                        PassData.OnlyUse1_FreeShow = "ccd3_2";

                        PassData.m4_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd3_2_Free";
                        PassData.cameraSet++;

                        break;

                    case "ccd4"://4
                        PassData.OnlyUse1_FreeShow = "ccd4";

                        PassData.m5_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd4_Free";
                        PassData.cameraSet++;

                        break;
                    default:
                        break;
                }




            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.ToString());
               
            }
        
        }

        void SetCameraStop(string NoCam)
        {
            try
            {
                switch (NoCam)
                {
                    case "ccd1":
                        PassData.m1_disp = m_Disp;

                        PassData.cameraSetAbout = "ccd1_Stop";
                        PassData.cameraSet++;

                        break;

                    case "ccd2":
                        PassData.m2_disp = m_Disp;

                        PassData.cameraSetAbout = "ccd2_Stop";
                        PassData.cameraSet++;

                        break;

                    case "ccd3_1"://3-1
                        PassData.m3_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd3_1_Stop";
                        PassData.cameraSet++;
                        break;

                    case "ccd3_2"://3-2
                        PassData.m4_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd3_2_Stop";
                        PassData.cameraSet++;

                        break;

                    case "ccd4"://4
                        PassData.m5_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd4_Stop";
                        PassData.cameraSet++;

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

        void SetCameraTake(string NoCam)
        {
            try
            {
                switch (NoCam)
                {
                    case "ccd1":
                        PassData.m1_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd1_SoftTriggerDo";
                        PassData.cameraSet++;

                        break;

                    case "ccd2":
                        PassData.m2_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd2_SoftTriggerDo";
                        PassData.cameraSet++;

                        break;

                    case "ccd3_1"://3-1
                        PassData.m3_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd3_1_SoftTriggerDo";
                        PassData.cameraSet++;

                        break;

                    case "ccd3_2"://3-2
                        PassData.m4_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd3_2_SoftTriggerDo";
                        PassData.cameraSet++;
                        break;

                    case "ccd4"://4
                        PassData.m5_disp = m_Disp;
                        PassData.cameraSetAbout = "ccd4_SoftTriggerDo";
                        PassData.cameraSet++;
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

        void SetCameraDetect(string NoCam)

        {
            try
            {
                switch (NoCam)
                {
                    case "ccd1":

                        PassData.cameraSetAbout = "ccd1_SoftTriggerPT";
                        PassData.cameraSet++;

                        break;

                    case "ccd2":
                        PassData.cameraSetAbout = "ccd2_SoftTriggerPT";
                        PassData.cameraSet++;

                        break;

                    case "ccd3_1"://3-1
                        PassData.cameraSetAbout = "ccd3_1_SoftTriggerPT";
                        PassData.cameraSet++;

                        break;

                    case "ccd3_2"://3-2
                        PassData.cameraSetAbout = "ccd3_2_SoftTriggerPT";
                        PassData.cameraSet++;

                        break;

                    case "ccd4"://4
                        PassData.cameraSetAbout = "ccd4_SoftTriggerPT";
                        PassData.cameraSet++;

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

        void SetCameraHardTriggerPT(string NoCam)

        {
            try
            {
                switch (NoCam)
                {
                    case "ccd1":

                        PassData.cameraSetAbout = "ccd1_HardTrigger_PT";
                        PassData.cameraSet++;

                        break;

                    case "ccd2":
                        PassData.cameraSetAbout = "ccd2_HardTrigger_PT";
                        PassData.cameraSet++;

                        break;

                    case "ccd3_1"://3-1
                        PassData.cameraSetAbout = "ccd3_1_HardTrigger_PT";
                        PassData.cameraSet++;

                        break;

                    case "ccd3_2"://3-2
                        PassData.cameraSetAbout = "ccd3_2_HardTrigger_PT";
                        PassData.cameraSet++;

                        break;

                    case "ccd4"://4
                        PassData.cameraSetAbout = "ccd4_HardTrigger_PT";
                        PassData.cameraSet++;

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
       
        #region Ctrl

        private void numExposeCCD1_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.expose1 = (int)numExposeCCD1.Value;
            SendMsgToForm1_Event(null, Para_MainPara);
            if (cB_CameraSet.Checked == true)
            {
                PassData.cameraSetAbout = "运行相机1曝光";
                PassData.cameraSet++;
            }
        }

        private void numgainCCD1_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.gain1 = (int)numgainCCD1.Value;
            SendMsgToForm1_Event(null, Para_MainPara); 
            if (cB_CameraSet.Checked == true)
            {
                PassData.cameraSetAbout = "运行相机1增益";
                PassData.cameraSet++;
            }
        }

        private void numExposeCCD2_ValueChanged(object sender, EventArgs e)
        {

            PassData.m1_disp = m_Disp;

            Para_MainPara.expose2 = (int)numExposeCCD2.Value;
            SendMsgToForm1_Event(null, Para_MainPara);
            if (cB_CameraSet.Checked == true)
            {
                PassData.cameraSetAbout = "运行相机2曝光";
                PassData.cameraSet++;
            } 

        }

        private void numgainCCD2_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.gain2 = (int)numgainCCD2.Value;
            SendMsgToForm1_Event(null, Para_MainPara);
            if (cB_CameraSet.Checked == true)
            {
                PassData.cameraSetAbout = "运行相机2增益";
                PassData.cameraSet++;
            }
        }

        private void numExposeCCD3_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.expose3 = (int)numExposeCCD3.Value;
            SendMsgToForm1_Event(null, Para_MainPara);
            if (cB_CameraSet.Checked == true)
            {
                PassData.cameraSetAbout = "运行相机3曝光";
                PassData.cameraSet++;
            }
        }

        private void numgainCCD3_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.gain3 = (int)numgainCCD3.Value;
            SendMsgToForm1_Event(null, Para_MainPara);
            if (cB_CameraSet.Checked == true)
            {
                PassData.cameraSetAbout = "运行相机3增益";
                PassData.cameraSet++;
            }
        }

        private void numExposeCCD4_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.expose4 = (int)numExposeCCD4.Value;
            SendMsgToForm1_Event(null, Para_MainPara);
            if (cB_CameraSet.Checked == true)
            {
                PassData.cameraSetAbout = "运行相机4曝光";
                PassData.cameraSet++;
            }
        }

        private void numgainCCD4_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.gain4 = (int)numgainCCD4.Value;
            SendMsgToForm1_Event(null, Para_MainPara);
            if (cB_CameraSet.Checked == true)
            {
                PassData.cameraSetAbout = "运行相机4增益";
                PassData.cameraSet++;
            }
        }

        private void numExposeCCD5_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.expose5 = (int)numExposeCCD5.Value;
            SendMsgToForm1_Event(null, Para_MainPara);
            if (cB_CameraSet.Checked == true)
            {
                PassData.cameraSetAbout = "运行相机5曝光";
                PassData.cameraSet++;
            }
        }

        private void numgainCCD5_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.gain5 = (int)numgainCCD5.Value;
            SendMsgToForm1_Event(null, Para_MainPara);
            if (cB_CameraSet.Checked == true)
            {
                PassData.cameraSetAbout = "运行相机5增益";
                PassData.cameraSet++;
            }
        }

        void update()
        {
            try
            {
                numExposeCCD1.Value = Para_MainPara.expose1;
                numgainCCD1.Value=Para_MainPara.gain1 ;
                numExposeCCD2.Value = Para_MainPara.expose2;
                numgainCCD2.Value = Para_MainPara.gain2;
                numExposeCCD3.Value = Para_MainPara.expose3;
                numgainCCD3.Value = Para_MainPara.gain3;
                numExposeCCD4.Value = Para_MainPara.expose4;
                numgainCCD4.Value = Para_MainPara.gain4;
                numExposeCCD5.Value = Para_MainPara.expose5;
                numgainCCD5.Value = Para_MainPara.gain5;

                //图像保存
                cmb_gw1_ImageSave.Text = Para_MainPara.ImageRunSave1 ;
                cmb_gw2_ImageSave.Text = Para_MainPara.ImageRunSave2 ;
                cmb_gw3_ImageSave.Text = Para_MainPara.ImageRunSave3 ;
                cmb_gw4_ImageSave.Text = Para_MainPara.ImageRunSave4 ;
                cmb_gw5_ImageSave.Text = Para_MainPara.ImageRunSave5 ;

                //显示图像保存
                cmb_gw1_ShowImageSave.Text= Para_MainPara.ImageShowSave1;
                  cmb_gw2_ShowImageSave.Text= Para_MainPara.ImageShowSave2;
                  cmb_gw3_ShowImageSave.Text= Para_MainPara.ImageShowSave3;
                  cmb_gw4_ShowImageSave.Text= Para_MainPara.ImageShowSave4;
                  cmb_gw5_ShowImageSave.Text= Para_MainPara.ImageShowSave5;

            }
            catch (Exception)
            {

               
            }

        
        
        }

        private void cmb_gw1_ImageSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            Para_MainPara.ImageRunSave1 = cmb_gw1_ImageSave.Text;
            SendMsgToForm1_Event(null, Para_MainPara);

        }

        private void cmb_gw2_ImageSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            Para_MainPara.ImageRunSave2 = cmb_gw2_ImageSave.Text;

            SendMsgToForm1_Event(null, Para_MainPara);
        }

        private void cmb_gw3_ImageSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            Para_MainPara.ImageRunSave3 = cmb_gw3_ImageSave.Text;
            SendMsgToForm1_Event(null, Para_MainPara);

        }

        private void cmb_gw4_ImageSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            Para_MainPara.ImageRunSave4 = cmb_gw4_ImageSave.Text;
            SendMsgToForm1_Event(null, Para_MainPara);

        }

        private void cmb_gw5_ImageSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            Para_MainPara.ImageRunSave5 = cmb_gw5_ImageSave.Text;
            SendMsgToForm1_Event(null, Para_MainPara);
        }

        void cmb_Image_Add()
        {
            try
            {
                cmb_gw1_ImageSave.Items.Clear();
                cmb_gw1_ImageSave.Items.Add("不保存");
                cmb_gw1_ImageSave.Items.Add("仅保存NG");
                cmb_gw1_ImageSave.Items.Add("仅保存OK");
                cmb_gw1_ImageSave.Items.Add("保存所有");

                cmb_gw2_ImageSave.Items.Clear();
                cmb_gw2_ImageSave.Items.Add("不保存");
                cmb_gw2_ImageSave.Items.Add("仅保存NG");
                cmb_gw2_ImageSave.Items.Add("仅保存OK");
                cmb_gw2_ImageSave.Items.Add("保存所有");


                cmb_gw3_ImageSave.Items.Clear();
                cmb_gw3_ImageSave.Items.Add("不保存");
                cmb_gw3_ImageSave.Items.Add("仅保存NG");
                cmb_gw3_ImageSave.Items.Add("仅保存OK");
                cmb_gw3_ImageSave.Items.Add("保存所有");


                cmb_gw4_ImageSave.Items.Clear();
                cmb_gw4_ImageSave.Items.Add("不保存");
                cmb_gw4_ImageSave.Items.Add("仅保存NG");
                cmb_gw4_ImageSave.Items.Add("仅保存OK");
                cmb_gw4_ImageSave.Items.Add("保存所有");


                cmb_gw5_ImageSave.Items.Clear();
                cmb_gw5_ImageSave.Items.Add("不保存");
                cmb_gw5_ImageSave.Items.Add("仅保存NG");
                cmb_gw5_ImageSave.Items.Add("仅保存OK");
                cmb_gw5_ImageSave.Items.Add("保存所有");
            }
            catch (Exception)
            {


            }

        }

        private void cmb_gw1_ShowImageSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            Para_MainPara.ImageShowSave1 = cmb_gw1_ShowImageSave.Text;
            SendMsgToForm1_Event(null, Para_MainPara);
        }

        private void cmb_gw2_ShowImageSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            Para_MainPara.ImageShowSave2 = cmb_gw2_ShowImageSave.Text;
            SendMsgToForm1_Event(null, Para_MainPara);
        }

        private void cmb_gw3_ShowImageSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            Para_MainPara.ImageShowSave3 = cmb_gw3_ShowImageSave.Text;
            SendMsgToForm1_Event(null, Para_MainPara);
        }

        private void cmb_gw4_ShowImageSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            Para_MainPara.ImageShowSave4 = cmb_gw4_ShowImageSave.Text;
            SendMsgToForm1_Event(null, Para_MainPara);
        }

        private void cmb_gw5_ShowImageSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            Para_MainPara.ImageShowSave5 = cmb_gw5_ShowImageSave.Text;
            SendMsgToForm1_Event(null, Para_MainPara);
        }

        void cmb_ShowImage_Add()
        {
            try
            {
                cmb_gw1_ShowImageSave.Items.Clear();
                cmb_gw1_ShowImageSave.Items.Add("不保存");
                cmb_gw1_ShowImageSave.Items.Add("仅保存NG");
                cmb_gw1_ShowImageSave.Items.Add("仅保存OK");
                cmb_gw1_ShowImageSave.Items.Add("保存所有");

                cmb_gw2_ShowImageSave.Items.Clear();
                cmb_gw2_ShowImageSave.Items.Add("不保存");
                cmb_gw2_ShowImageSave.Items.Add("仅保存NG");
                cmb_gw2_ShowImageSave.Items.Add("仅保存OK");
                cmb_gw2_ShowImageSave.Items.Add("保存所有");

                cmb_gw3_ShowImageSave.Items.Clear();
                cmb_gw3_ShowImageSave.Items.Add("不保存");
                cmb_gw3_ShowImageSave.Items.Add("仅保存NG");
                cmb_gw3_ShowImageSave.Items.Add("仅保存OK");
                cmb_gw3_ShowImageSave.Items.Add("保存所有");

                cmb_gw4_ShowImageSave.Items.Clear();
                cmb_gw4_ShowImageSave.Items.Add("不保存");
                cmb_gw4_ShowImageSave.Items.Add("仅保存NG");
                cmb_gw4_ShowImageSave.Items.Add("仅保存OK");
                cmb_gw4_ShowImageSave.Items.Add("保存所有");

                cmb_gw5_ShowImageSave.Items.Clear();
                cmb_gw5_ShowImageSave.Items.Add("不保存");
                cmb_gw5_ShowImageSave.Items.Add("仅保存NG");
                cmb_gw5_ShowImageSave.Items.Add("仅保存OK");
                cmb_gw5_ShowImageSave.Items.Add("保存所有");

            }
            catch (Exception)
            {


            }

        }



        #endregion

        //切换时，记录切换前的使用相机情况
        string nowCam = "ccd1";
        private void cB_camera_SelectedIndexChanged(object sender, EventArgs e)
        {
            //相机切换时，当前实时模式相机需要关闭
            try
            {
                nowCam = cB_camera.Text;
                switch (nowCam)
                {

                    case "ccd1":
                        PassData.m1_disp = m_Disp;
                        //PassData.cameraSetAbout = "ccd1_Stop";
                        //PassData.cameraSet++;
                       
                        PassData.OnlyUse1_HardTrigger = "ccd1";
                        PassData.OnlyUse1_HardTrigger = "ccd1";

                        PassData.OnlyUse1_FreeShow = "ccd1";
                        PassData.cameraSetAbout = "实时模式显示切换";
                        PassData.cameraSet++;
                        break;

                    case "ccd2":
                        PassData.m2_disp = m_Disp;
                        //PassData.cameraSetAbout = "ccd2_Stop";
                        //PassData.cameraSet++;
                      
                        PassData.OnlyUse1_HardTrigger = "ccd2";
                        PassData.OnlyUse1_HardTrigger = "ccd2";

                        PassData.OnlyUse1_FreeShow = "ccd2";
                        PassData.cameraSetAbout = "实时模式显示切换";
                        PassData.cameraSet++;
                        break;

                    case "ccd3_1"://3-1
                        PassData.m3_disp = m_Disp;
                        //PassData.cameraSetAbout = "ccd3_1_Stop";
                        //PassData.cameraSet++;
                       
                        PassData.OnlyUse1_HardTrigger = "ccd3_1";
                        PassData.OnlyUse1_HardTrigger = "ccd3_1";

                        PassData.OnlyUse1_FreeShow = "ccd3_1";
                        PassData.cameraSetAbout = "实时模式显示切换";
                        PassData.cameraSet++;
                        break;

                    case "ccd3_2"://3-2
                        PassData.m4_disp = m_Disp;
                        //PassData.cameraSetAbout = "ccd3_2_Stop";
                        //PassData.cameraSet++;
                       
                        PassData.OnlyUse1_HardTrigger = "ccd3_2";
                        PassData.OnlyUse1_HardTrigger = "ccd3_2";
                       
                        PassData.OnlyUse1_FreeShow = "ccd3_2";
                        PassData.cameraSetAbout = "实时模式显示切换";
                        PassData.cameraSet++;
                        break;

                    case "ccd4"://4
                        PassData.m5_disp = m_Disp;
                        //PassData.cameraSetAbout = "ccd4_Stop";
                        //PassData.cameraSet++;
                        PassData.OnlyUse1_HardTrigger = "ccd4";
                        PassData.OnlyUse1_HardTrigger = "ccd4";

                        PassData.OnlyUse1_FreeShow = "ccd4";
                        PassData.cameraSetAbout = "实时模式显示切换";
                        PassData.cameraSet++;
                        break;
                    default:
                        break;
                }

              //  nowCam = cB_camera.Text;


            }
            catch (Exception ex) { }




           // cB_camera_SelectedIndexChanged(null,null);



        }

        
       



        //委托事件函数
        void DelegateDeclaration()
        {

            PassData.delegate_CameraPT_Form = delegate_CameraPT_Form;

        }

        void delegate_CameraPT_Form()
        {
            try
            {
                txt_Result.Text=string.Empty;
                double X=0;
                double Y=0;
                switch (PassData.CameraPT_about)
                {

                    case "ccd1PT":
                         X = double.Parse(PassData.strContent) ;
                         Y = double.Parse(PassData.strContent2);

                        BeginInvoke(new Action(() =>
                        {
                            txt_Result.Text = "ccd1ΔX,ΔY(mm):" + "\r\n"
                            + X.ToString("0.000") + "," + Y.ToString("0.000");
                        }));

                        break;

                    case "ccd2PT":
                         X = double.Parse(PassData.strContent);
                         Y = double.Parse(PassData.strContent2);

                        BeginInvoke(new Action(() =>
                        {
                            txt_Result.Text = "ccd2ΔX,ΔY(mm):" + "\r\n"
                               + X.ToString("0.000") + "," + Y.ToString("0.000");
                        }));

                           break;

                    case "ccd3_1PT":
                        X = double.Parse(PassData.strContent);
                        Y = double.Parse(PassData.strContent2);
                        
                        BeginInvoke(new Action(() =>
                        {
                        txt_Result.Text = "ccd3_1ΔX,ΔY(mm):" + "\r\n"
                            + X.ToString("0.000") + "," + Y.ToString("0.000");
                        }));
                        break;

                    case "ccd3_2PT":
                        X = double.Parse(PassData.strContent);
                        Y = double.Parse(PassData.strContent2);
                        
                        BeginInvoke(new Action(() =>
                        {
                        txt_Result.Text = "ccd3_2ΔX,ΔY(mm):" + "\r\n"
                            + X.ToString("0.000") + "," + Y.ToString("0.000");
                        }));
                        break;

                    case "ccd4PT":
                        X = double.Parse(PassData.strContent);
                        Y = double.Parse(PassData.strContent2);
                        BeginInvoke(new Action(() =>
                        {
                            txt_Result.Text = "ccd4ΔX,ΔY(mm):" + "\r\n"
                            + X.ToString("0.000") + "," + Y.ToString("0.000");
                        }));
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

        private void btn_SendMsgToPLC_Click(object sender, EventArgs e)
        {
            try
            {
                //https://www.cnblogs.com/wcl2017/p/9145611.html
                string qw = txt_Result.Text;
                if (qw != string.Empty && qw != null && qw != "")
                {

                    string[] qwe = qw.Split(',');

                    double[] qwr = new double[2];
                    qwr[0] = (int)(double.Parse(qwe[0]) * 1000);
                    qwr[1] = (int)(double.Parse(qwe[1]) * 1000);

                    string[] qwe2 = new string[2];
                    qwe2[0] = qwr[0].ToString();
                    qwe2[1] = qwr[1].ToString();


                    PassData.strContent = qwe2[0];
                    PassData.strContent2 = qwe2[1];

                    switch (cB_camera.Text)
                    {
                        case "ccd1":
                            PassData.strContent3 = "ccd1";
                            break;

                        case "ccd2":
                            PassData.strContent3 = "ccd2";
                            break;

                        case "ccd3_1":
                            PassData.strContent3 = "ccd3_1";
                            break;

                        case "ccd3_2":
                            PassData.strContent3 = "ccd3_2";
                            break;

                        case "ccd4":
                            PassData.strContent3 = "ccd4";
                            break;

                        default:
                            break;
                    }

                    PassData.cameraSetAbout = "示教页面发送数据";
                    PassData.cameraSet++;

                }
                else
                {
                    MessageBox.Show("输入框为空");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           

        }



        void toolTip()
        {
            try
            {

                toolTip1.IsBalloon = true;
                toolTip1.AutoPopDelay = 32000;

                //******************************************
                toolTip1.SetToolTip(this.btn_SendMsgToPLC,
                    "发送数据需把输入框清空，格式为“1.234,5.678”，顶格输入，不要加任何特殊符号，" + Environment.NewLine +
                    "鼠标双击输入框，可清空输入框" + Environment.NewLine +
                    "注意发送的PLC品牌是否选择与实际相符");


                //toolTip1.SetToolTip(this.btn_NullDetect_Set,
                //    "注意：对于空盘的情况输出的结果必须是“OK”");


                //角度比较值******************************************
                //toolTip1.SetToolTip(this.label12,
                //    "使用“角度比较值”时，把“角度补正值”设置成999；" + Environment.NewLine +
                //    "记录标准Mark点检测角度值，填入角度比较值即可"
                //    );


            }
            catch (Exception)
            {

            }



        }

     

        private void txt_Result_DoubleClick(object sender, EventArgs e)
        {
            txt_Result.Text = string.Empty;
        }

        private void ckB_IsCameraHardTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (ckB_IsCameraHardTrigger.Checked)
            {
                PassData.OnlyUse1_TestCameraHardTrigger = true;
            }
            else
            {
                PassData.OnlyUse1_TestCameraHardTrigger = false;
            }
        }
    }



}
