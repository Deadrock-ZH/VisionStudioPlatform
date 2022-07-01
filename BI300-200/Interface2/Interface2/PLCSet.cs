using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface2
{
    public partial class PLCSet : Form
    {

        MainPara Para_MainPara = new MainPara();

        public event EventHandler SendMsgToForm1_Event;

        internal void EventFromForm1Chaned(object sender, EventArgs e)
        {

            //取得子界面参数
            Para_MainPara = e as MainPara;

        }

        public PLCSet()
        {

            InitializeComponent();
        
            //委托注册
            DelegateDeclaration();
        
        }

        private void PLCSet_Load(object sender, EventArgs e)
        {
            label_Read.Text = string.Empty;
            label3.Text = string.Empty;
            this.Text = "PLC通信设置";
         
            cmb_PLCSelect.Items.Clear();
            cmb_PLCSelect.Items.Add("欧姆龙");
            cmb_PLCSelect.Items.Add("基恩士");


            updataToCtrl2();
            updataToCtrl();
        }


        #region Ctrl

        private void cmb_PLCSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
           Para_MainPara.PLCSelect=  cmb_PLCSelect.Text ;
        }

        //***********欧姆龙********************
        private void num_Port_orm_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.PLC_Port_omr = (int)num_Port_orm.Value;
            SendMsgToForm1_Event(null, Para_MainPara);

        }

        private void txt_PLCIP_orm_TextChanged(object sender, EventArgs e)
        {
            Para_MainPara.PLC_IP_omr = txt_PLCIP_orm.Text;
            SendMsgToForm1_Event(null, Para_MainPara);

        }

        private void num_ComFinalIP_orm_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.PC_finalIP_omr = (int)num_ComFinalIP_orm.Value;

            SendMsgToForm1_Event(null, Para_MainPara);
        }


        //******************基恩士***********************

        private void num_Port_kv_ValueChanged(object sender, EventArgs e)
        {
            Para_MainPara.PLC_Port_kv = (int)num_Port_kv.Value;
            SendMsgToForm1_Event(null, Para_MainPara);

        }

        private void txt_PLCIP_kv_TextChanged(object sender, EventArgs e)
        {
            Para_MainPara.PLC_IP_kv = txt_PLCIP_kv.Text;
            SendMsgToForm1_Event(null, Para_MainPara);

        }


        void updataToCtrl()
        {
            try
            {

                cmb_PLCSelect.Text = Para_MainPara.PLCSelect;

                num_Port_orm.Value = Para_MainPara.PLC_Port_omr;
                txt_PLCIP_orm.Text = Para_MainPara.PLC_IP_omr;
                num_ComFinalIP_orm.Value = Para_MainPara.PC_finalIP_omr;

                num_Port_kv.Value = Para_MainPara.PLC_Port_kv;
                txt_PLCIP_kv.Text = Para_MainPara.PLC_IP_kv;

                num_NumDisp.Value= Para_MainPara.NumDisp;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        void updataToCtrl2()
        {
            try
            {
                //****************欧姆龙***************************
                num_MemoryWrite_orm.Value = 130;

                num_NOWrite_orm.Value = 100;

                num_quantityWrite_orm.Value = 1;

                num_MemoryRead_orm.Value = 130;

                num_NORead_orm.Value = 100;

                num_quantityRead_orm.Value = 1;

                //**************基恩士*******************
                num_MemoryWrite_kv.Value = 6006;

                num_quantityWrite_kv.Value = 1;


                num_MemoryRead_kv.Value = 6006;

                num_quantityRead_kv.Value = 1;


            }
            catch (Exception)
            {

            }


        }


        #endregion

        private void btn_Write_orm_Click(object sender, EventArgs e)
        {

            PassData.strContent = num_MemoryWrite_orm.Value.ToString();
            PassData.strContent2 = num_NOWrite_orm.Value.ToString();
            PassData.strContent3 = num_quantityWrite_orm.Value.ToString();
            PassData.strContent4 = txt_write_orm.Text;
          
            PassData.cameraSetAbout = "欧姆龙写入";
            PassData.cameraSet++;


        }

        private void btn_Read_orm_Click(object sender, EventArgs e)
        {

            PassData.strContent = num_MemoryRead_orm.Value.ToString();
            PassData.strContent2 = num_NORead_orm.Value.ToString();
            PassData.strContent3 = num_quantityRead_orm.Value.ToString();

            PassData.cameraSetAbout = "欧姆龙读取";
            PassData.cameraSet++;

        }

        private void btn_Write_kv_Click(object sender, EventArgs e)
        {
            PassData.strContent = num_MemoryWrite_kv.Value.ToString();
            PassData.strContent2 = num_quantityWrite_kv.Value.ToString();
            PassData.strContent3 = txt_write_kv.Text;

            PassData.cameraSetAbout ="基恩士写入";
            PassData.cameraSet++;

        }

        private void btn_Read_kv_Click(object sender, EventArgs e)
        {
            PassData.strContent = num_MemoryRead_kv.Value.ToString();
            PassData.strContent2 = num_quantityRead_kv.Value.ToString();
            PassData.strContent3 = txt_write_kv.Text;

            PassData.cameraSetAbout = "基恩士读取";
            PassData.cameraSet++;

        }



        //委托事件函数
        void DelegateDeclaration()
        {

            PassData.delegate_PLC_Form = delegate_PLC_Form;

        }


        void delegate_PLC_Form()
        {
            try
            {
                switch (PassData.plc_about)
                {
                    case "omr_Read":
                      
                        label_Read.Text = string.Empty;
                        label_Read.Text = PassData.strContent4;

                        break;

                    case "kv_Read":
                       
                        label3.Text = string.Empty;
                        label3.Text = PassData.strContent3;

                        break;

                    default:
                        break;
                }



            }
            catch (Exception)
            {

               
            }
        
        }

        private void num_NumDisp_ValueChanged(object sender, EventArgs e)
        {
            if (ChB_IsNumDisp.Checked)
            {
                Para_MainPara.NumDisp = (int)num_NumDisp.Value;
                SendMsgToForm1_Event(null, Para_MainPara);
                PassData.cameraSetAbout = "相机个数更改";
                PassData.cameraSet++;
            }

        }

       
    }
}
