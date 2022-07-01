using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace SvsPLC
{
    public partial class PLCForm : Form
    {
        /// <summary>
        /// 方法类
        /// </summary>
        public PLCMethod m_Method = null;

        /// <summary>
        /// 保存路径
        /// </summary>
        string m_strPath;

        public PLCForm(string Path)
        {
            m_strPath = Path;
            InitializeComponent();
        }

        private void PLCForm_Load(object sender, EventArgs e)
        {
            // 将数据更新到控件
            updateParaToCtrl();
        }

        /// <summary>
        /// 将数据更新至控件
        /// </summary>
        void updateParaToCtrl()
        {
            if (null != m_Method)
            {
                paraPLCip.Text = m_Method.Para.Ip;
                paraPLCport.Value = m_Method.Para.Port;
                paraPCip.Value = m_Method.Para.PCIp;

                if (m_Method.Para.Brand == "基恩士")
                {
                    radioButton_基恩士.Checked = true;
                }
                else if (m_Method.Para.Brand == "三菱")
                {
                    radioButton_三菱.Checked = true;
                }
                else if (m_Method.Para.Brand == "欧姆龙")
                {
                    radioButton_欧姆龙.Checked = true;
                }
                paraPLCip.Text = m_Method.Para.Ip.Trim();
                paraPCip.Value = m_Method.Para.PCIp;
                paraPLCport.Value = m_Method.Para.Port;
                UpdateParaToDgvData();
                //    if (m_Method.Para.listNumbers.Count != 0)
                //    {
                //        Camera_dataGridView.Rows.Clear();
                //        for (int iNum = 0; iNum < m_Method.Para.listNumbers.Count; iNum++)
                //        {
                //            iNum = Camera_dataGridView.Rows.Add();
                //            Camera_dataGridView.Rows[iNum].Cells[0].Value = iNum + 1;
                //            if (m_Method.Para.listRegister.Count != 0)
                //            {
                //                Camera_dataGridView.Rows[iNum].Cells[1].Value = m_Method.Para.listRegister[iNum];
                //            }
                //            if (m_Method.Para.listBegin.Count != 0)
                //            {
                //                Camera_dataGridView.Rows[iNum].Cells[2].Value = m_Method.Para.listBegin[iNum];
                //            }
                //            if (m_Method.Para.listNumbers.Count != 0)
                //            {
                //                Camera_dataGridView.Rows[iNum].Cells[3].Value = m_Method.Para.listNumbers[iNum];
                //            }
                //        }
                //    }
                //    else
                //    {
                //        Camera_dataGridView.Rows.Clear();
                //        for (int iNum = 0; iNum < 1; iNum++)
                //        {
                //            iNum = Camera_dataGridView.Rows.Add();
                //            Camera_dataGridView.Rows[iNum].Cells[0].Value = iNum + 1;
                //        }
                //    }
            }
        }

        private void 确定_button_Click(object sender, EventArgs e)
        {
            m_Method.Para.Ip = paraPLCip.Text;
            m_Method.Para.Port = (int)paraPLCport.Value;
            m_Method.Save(m_Method.Para, m_strPath);
        }

        private void Brand_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_三菱.Checked)
            {
                m_Method.Para.Brand = radioButton_三菱.Text;
            }
            else if (radioButton_基恩士.Checked)
            {
                m_Method.Para.Brand = radioButton_基恩士.Text;
            }
            else
            {
                m_Method.Para.Brand = radioButton_欧姆龙.Text;
            }
        }

        private void button添加工位_Click(object sender, EventArgs e)
        {
            string strRegister = "DM";
            int iStartAddress = 100;
            int iSendDataNum = 4;
            m_Method.Para.listRegister.Add(strRegister);
            m_Method.Para.listBegin.Add(iStartAddress);
            m_Method.Para.listNumbers.Add(iSendDataNum);
            UpdateParaToDgvData();
        }

        /// <summary>
        /// 添加工位减少工位数据更新
        /// </summary>
        private void UpdateParaToDgvData()
        {
            Camera_dataGridView.Rows.Clear();
            for (int i = 0; i < m_Method.Para.listRegister.Count; i++)
            {
                int index = this.Camera_dataGridView.Rows.Add();
                this.Camera_dataGridView.Rows[i].Cells[0].Value = index + 1;
                this.Camera_dataGridView.Rows[i].Cells[1].Value = m_Method.Para.listRegister[i];
                this.Camera_dataGridView.Rows[i].Cells[2].Value = m_Method.Para.listBegin[i];
                this.Camera_dataGridView.Rows[i].Cells[3].Value = m_Method.Para.listNumbers[i];
            }
        }

        private void button减少工位_Click(object sender, EventArgs e)
        {
            int iIndex = Camera_dataGridView.CurrentRow.Index;
            m_Method.Para.listRegister.RemoveAt(iIndex);
            m_Method.Para.listBegin.RemoveAt(iIndex);
            m_Method.Para.listNumbers.RemoveAt(iIndex);
            UpdateParaToDgvData();
            //int dr = this.Camera_dataGridView.RowCount;
            //if (dr > 1)
            //{
            //    this.Camera_dataGridView.Rows.RemoveAt(dr - 1);
            //}
        }

        private void 保存_Click(object sender, EventArgs e)
        {
            m_Method.Para.listRegister.Clear();
            m_Method.Para.listBegin.Clear();
            m_Method.Para.listNumbers.Clear();
            for (int iNum = 0; iNum < Camera_dataGridView.RowCount; iNum++)
            {
                m_Method.Para.listRegister.Add(Convert.ToString(Camera_dataGridView.Rows[iNum].Cells[1].Value));
                m_Method.Para.listBegin.Add(Convert.ToInt32(Camera_dataGridView.Rows[iNum].Cells[2].Value));
                m_Method.Para.listNumbers.Add(Convert.ToInt32(Camera_dataGridView.Rows[iNum].Cells[3].Value));
            }
            m_Method.Save(m_Method.Para, m_strPath);
        }

        private void tsp_connect_Click(object sender, EventArgs e)
        {
            if (!m_Method.ConnectState)
            {
                string strConnect = m_Method.Connect();
                tsp_Msg.Text = strConnect;
            }
            else
            {
                tsp_Msg.Text = "PLC已连接！";
            }
        }

        private void Camera_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Camera_dataGridView.CurrentRow != null)
            {
                Camera_dataGridView.Rows[Camera_dataGridView.RowCount - 1].Selected = false;
                int i = Camera_dataGridView.CurrentRow.Index;
                // 当某行数据都有值时更改
                if (Camera_dataGridView.Rows.Count > 0 &&
                    Camera_dataGridView.Rows[i].Cells[0].Value != null &&
                    Camera_dataGridView.Rows[i].Cells[1].Value != null
                    && Camera_dataGridView.Rows[i].Cells[2].Value != null &&
                    Camera_dataGridView.Rows[i].Cells[3].Value != null)
                {
                    m_Method.Para.listRegister[i] = string.Empty;
                    m_Method.Para.listRegister[i] = Camera_dataGridView.Rows[i].Cells[1].Value.ToString();
                    m_Method.Para.listBegin[i] = int.Parse(Camera_dataGridView.Rows[i].Cells[2].Value.ToString());
                    m_Method.Para.listNumbers[i] = int.Parse(Camera_dataGridView.Rows[i].Cells[3].Value.ToString());
                }
            }

        }

        // 提交未提交控件的更改
        private void dgv_Tool_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (Camera_dataGridView.IsCurrentCellDirty)
            {
                Camera_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void tsp_Run_Click(object sender, EventArgs e)
        {
            HTuple hvSend = new HTuple();
            hvSend[0] = 10;
            hvSend[1] = 100;
            hvSend[2] = 110;
            hvSend[3] = 210;
            m_Method.SendData(1, hvSend);
        }

        private void paraPLCip_TextChanged(object sender, EventArgs e)
        {
            m_Method.Para.Ip = paraPLCip.Text.Trim();
        }

        private void paraPLCport_ValueChanged(object sender, EventArgs e)
        {
            m_Method.Para.Port = (int)paraPLCport.Value;
        }

        private void paraPCip_ValueChanged(object sender, EventArgs e)
        {
            m_Method.Para.PCIp = (int)paraPCip.Value;
        }
    }
}
