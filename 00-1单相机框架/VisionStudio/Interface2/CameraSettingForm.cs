using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionStudio
{
    public partial class CameraSettingForm : Form
    {
        public CameraSettingForm()
        {
            InitializeComponent();
        }

        private void CameraSettingForm_Load(object sender,EventArgs e)
        {
            if(m_method.Paras.EnumTriggerMode == EnumTrigger.硬触发) {
                triggermode_comboBox.Text = "硬触发";
            } else {
                triggermode_comboBox.Text = "软触发";
            }

            int cameraNum = m_method.Paras.CameraNum;
            for (int i = 0; i< cameraNum; i++) 
            {
                ccd_index_comboBox.Items.Add((i+1).ToString());
            } 
            indexOfCCD = 1;
            ccd_index_comboBox.Text = indexOfCCD.ToString();

        }

        private Method m_method = new Method();
        public Method method 
        {
            set { m_method = value; }
        }
        private EnumTrigger m_triggerMode;
        public EnumTrigger triggerMode
        {
            get { return m_triggerMode; }
            set { m_triggerMode = value; }
        }

        private float m_exposeTime;
        public float exposeTime 
        {
            set { m_exposeTime = value; }
            get { return m_exposeTime; }
        }
        private float m_gain;
        public float gain 
        {
            set { m_gain = value; }
            get { return m_gain; }
        }

        private int m_indexOfCCD;
        public int indexOfCCD 
        {
            set { m_indexOfCCD = value; }
            get { return m_indexOfCCD; }
        }


        public event EventHandler triggerModeChanged;
        public event EventHandler ccdParasChanged;

        private void triggermode_comboBox_SelectedIndexChanged(object sender,EventArgs e)
        {
            if (triggerModeChanged != null)
            {
                switch(triggermode_comboBox.Text)
                {
                    case "硬触发":
                        triggerMode = EnumTrigger.硬触发;
                        break;
                    case "软触发":
                        triggerMode = EnumTrigger.软触发;
                        break;
                    default:
                        break;
                }
                triggerModeChanged(this,e);
            }
        }

        private void ccd_index_comboBox_SelectedIndexChanged(object sender,EventArgs e)
        {
            int CCDIndex = int.Parse(ccd_index_comboBox.Text);
            indexOfCCD = CCDIndex;
            SvsMVSCamera.CameraAPI currentCCD = m_method.m_ArrayCameraMethod[CCDIndex - 1];
            if (currentCCD != null)
            {
                if (currentCCD.ifccdConnected)
                {
                    float ccdExposeTime;
                    float ccdGain;
                    currentCCD.GetExposureTime(out ccdExposeTime);
                    currentCCD.GetGain(out ccdGain);
                    CCD_ExposeTime.Value = (decimal)ccdExposeTime;
                    CCD_Gain.Value = (decimal)ccdGain;
                }
            }
        }

        private void CCD_ExposeTime_ValueChanged(object sender,EventArgs e)
        {
            if(ccdParasChanged != null) {
                exposeTime = (float)CCD_ExposeTime.Value;
                ccdParasChanged(this,e);
            }
        }

        private void CCD_Gain_ValueChanged(object sender,EventArgs e)
        {
            if (ccdParasChanged != null)
            {
                gain = (float)CCD_Gain.Value;
                ccdParasChanged(this,e);
            }
        }
    }
}
