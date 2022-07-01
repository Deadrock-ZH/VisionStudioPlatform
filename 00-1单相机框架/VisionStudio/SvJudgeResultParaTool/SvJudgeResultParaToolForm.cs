using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ParaResultAll;

namespace SvJudgeResultParaTool
{
    /// <summary>
    /// 内 容:本类是数据判断界面
    /// 作 者:wyp
    /// 时 间：2021/5/14
    /// </summary>
    public partial class SvJudgeResultParaToolForm : Form
    {
        /// <summary>
        /// 方法类
        /// </summary>
        public SvJudgeResultParaToolMethod m_JudgeMethod = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public SvJudgeResultParaToolForm()
        {
            InitializeComponent();
        }

        // 加载
        private void SvJudgeResultParaToolForm_Load(object sender, EventArgs e)
        {
            // 更新传入数据
            UpdateInputeData(cmb_Data1, Cmb1_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data2, Cmb2_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data3, Cmb3_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data4, Cmb4_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data5, Cmb5_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data6, Cmb6_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data7, Cmb7_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data8, Cmb8_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data9, Cmb9_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data10, Cmb10_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data11, Cmb11_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data12, Cmb12_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data13, Cmb13_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data14, Cmb14_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data15, Cmb15_Data_SelectedIndexChanged);
            UpdateInputeData(cmb_Data16, Cmb16_Data_SelectedIndexChanged);

            // 更新下拉框选择
            UpdateCmbSelect(cmb_Data1, 0);
            UpdateCmbSelect(cmb_Data2, 1);
            UpdateCmbSelect(cmb_Data3, 2);
            UpdateCmbSelect(cmb_Data4, 3);
            UpdateCmbSelect(cmb_Data5, 4);
            UpdateCmbSelect(cmb_Data6, 5);
            UpdateCmbSelect(cmb_Data7, 6);
            UpdateCmbSelect(cmb_Data8, 7);
            UpdateCmbSelect(cmb_Data9, 8);
            UpdateCmbSelect(cmb_Data10, 9);
            UpdateCmbSelect(cmb_Data11, 10);
            UpdateCmbSelect(cmb_Data12, 11);
            UpdateCmbSelect(cmb_Data13, 12);
            UpdateCmbSelect(cmb_Data14, 13);
            UpdateCmbSelect(cmb_Data15, 14);
            UpdateCmbSelect(cmb_Data16, 15);

            // 将数据更新至控件
            UpdateParaToCtrl();
            tsp_Msg.Text = m_JudgeMethod.RunMsg;
            tsp_Time.Text = m_JudgeMethod.RunTime.ToString("F1");
        }

        /// <summary>
        /// 将数据更新至控件
        /// </summary>
        private void UpdateParaToCtrl()
        {
            num_K1.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].K;
            num_K2.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].K;
            num_K3.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].K;
            num_K4.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].K;
            num_K5.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].K;
            num_K6.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].K;
            num_K7.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].K;
            num_K8.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].K;
            num_K9.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].K;
            num_K10.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].K;
            num_K11.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].K;
            num_K12.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].K;
            num_K13.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].K;
            num_K14.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].K;
            num_K15.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].K;
            num_K16.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].K;

            num_Offset1.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].Offset;
            num_Offset2.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].Offset;
            num_Offset3.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].Offset;
            num_Offset4.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].Offset;
            num_Offset5.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].Offset;
            num_Offset6.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].Offset;
            num_Offset7.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].Offset;
            num_Offset8.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].Offset;
            num_Offset9.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].Offset;
            num_Offset10.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].Offset;
            num_Offset11.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].Offset;
            num_Offset12.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].Offset;
            num_Offset13.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].Offset;
            num_Offset14.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].Offset;
            num_Offset15.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].Offset;
            num_Offset16.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].Offset;

            txt_Split.Text = m_JudgeMethod.ParasSvJudgeResultParaTool.SplitChar;

            num_Max1.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].MaxRange;
            num_Min1.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].MinRange;
            num_Max2.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].MaxRange;
            num_Min2.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].MinRange;
            num_Max3.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].MaxRange;
            num_Min3.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].MinRange;
            num_Max4.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].MaxRange;
            num_Min4.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].MinRange;
            num_Max5.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].MaxRange;
            num_Min5.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].MinRange;
            num_Max6.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].MaxRange;
            num_Min6.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].MinRange;
            num_Max7.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].MaxRange;
            num_Min7.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].MinRange;
            num_Max8.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].MaxRange;
            num_Min8.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].MinRange;
            num_Max9.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].MaxRange;
            num_Min9.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].MinRange;
            num_Max10.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].MaxRange;
            num_Min10.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].MinRange;
            num_Max11.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].MaxRange;
            num_Min11.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].MinRange;
            num_Max12.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].MaxRange;
            num_Min12.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].MinRange;
            num_Max13.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].MaxRange;
            num_Min13.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].MinRange;
            num_Max14.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].MaxRange;
            num_Min14.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].MinRange;
            num_Max15.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].MaxRange;
            num_Min15.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].MinRange;
            num_Max16.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].MaxRange;
            num_Min16.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].MinRange;

            chk_Send10.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].SendState;
            chk_Send9.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].SendState;
            chk_Send8.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].SendState;
            chk_Send7.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].SendState;
            chk_Send6.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].SendState;
            chk_Send5.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].SendState;
            chk_Send4.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].SendState;
            chk_Send3.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].SendState;
            chk_Send2.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].SendState;
            chk_Send1.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].SendState;
            chk_Send16.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].SendState;
            chk_Send15.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].SendState;
            chk_Send14.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].SendState;
            chk_Send13.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].SendState;
            chk_Send12.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].SendState;
            chk_Send11.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].SendState;

            chk_输入数据10.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].SelectState;
            chk_输入数据9.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].SelectState;
            chk_输入数据8.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].SelectState;
            chk_输入数据7.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].SelectState;
            chk_输入数据6.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].SelectState;
            chk_输入数据5.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].SelectState;
            chk_输入数据4.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].SelectState;
            chk_输入数据3.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].SelectState;
            chk_输入数据2.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].SelectState;
            chk_输入数据1.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].SelectState;
            chk_输入数据16.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].SelectState;
            chk_输入数据15.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].SelectState;
            chk_输入数据14.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].SelectState;
            chk_输入数据13.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].SelectState;
            chk_输入数据12.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].SelectState;
            chk_输入数据11.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].SelectState;

            num_NG.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.NG;
            num_OK.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.OK;
            chk_NG.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.NGForwardState;
            chk_OK.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.OKForwardState;
            num_NGBack.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.NGBack;
            num_OKBack.Value = (decimal)m_JudgeMethod.ParasSvJudgeResultParaTool.OKBack;
            chk_NGBack.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.NGBackState;
            chk_OKBack.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.OKBackState;

            num_BlobHaveBefore.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.HaveOrNotForward;
            num_BlobAfter.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.HaveOrNotBack;
            chk_BlobHaveAfter.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.HaveOrNotBackState;
            chk_BlobHaveBefore1.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.HaveOrNotForwardState;

            if (chk_BlobHaveAfter.Checked || chk_BlobHaveBefore1.Checked)
            {
                chk_BlobHave1.Enabled = true;
                chk_BlobHave2.Enabled = true;
                chk_BlobHave3.Enabled = true;
                chk_BlobHave4.Enabled = true;
                chk_BlobHave5.Enabled = true;
                chk_BlobHave6.Enabled = true;
                chk_BlobHave7.Enabled = true;
                chk_BlobHave8.Enabled = true;
                chk_BlobHave9.Enabled = true;
                chk_BlobHave10.Enabled = true;
                chk_BlobHave11.Enabled = true;
                chk_BlobHave12.Enabled = true;
                chk_BlobHave13.Enabled = true;
                chk_BlobHave14.Enabled = true;
                chk_BlobHave15.Enabled = true;
                chk_BlobHave16.Enabled = true;
            }
            else
            {
                chk_BlobHave1.Enabled = false;
                chk_BlobHave2.Enabled = false;
                chk_BlobHave3.Enabled = false;
                chk_BlobHave4.Enabled = false;
                chk_BlobHave5.Enabled = false;
                chk_BlobHave6.Enabled = false;
                chk_BlobHave7.Enabled = false;
                chk_BlobHave8.Enabled = false;
                chk_BlobHave9.Enabled = false;
                chk_BlobHave10.Enabled = false;
                chk_BlobHave11.Enabled = false;
                chk_BlobHave12.Enabled = false;
                chk_BlobHave13.Enabled = false;
                chk_BlobHave14.Enabled = false;
                chk_BlobHave15.Enabled = false;
                chk_BlobHave16.Enabled = false;
            }

            chk_BlobHave16.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].HaveOrNotState;
            chk_BlobHave15.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].HaveOrNotState;
            chk_BlobHave14.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].HaveOrNotState;
            chk_BlobHave13.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].HaveOrNotState;
            chk_BlobHave12.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].HaveOrNotState;
            chk_BlobHave11.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].HaveOrNotState;
            chk_BlobHave10.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].HaveOrNotState;
            chk_BlobHave9.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].HaveOrNotState;
            chk_BlobHave8.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].HaveOrNotState;
            chk_BlobHave7.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].HaveOrNotState;
            chk_BlobHave6.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].HaveOrNotState;
            chk_BlobHave5.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].HaveOrNotState;
            chk_BlobHave4.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].HaveOrNotState;
            chk_BlobHave3.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].HaveOrNotState;
            chk_BlobHave2.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].HaveOrNotState;
            chk_BlobHave1.Checked = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].HaveOrNotState;

            num_decimal1.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].DecimalPlaces;
            num_decimal2.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].DecimalPlaces;
            num_decimal3.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].DecimalPlaces;
            num_decimal4.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].DecimalPlaces;
            num_decimal5.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].DecimalPlaces;
            num_decimal6.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].DecimalPlaces;
            num_decimal7.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].DecimalPlaces;
            num_decimal8.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].DecimalPlaces;
            num_decimal9.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].DecimalPlaces;
            num_decimal10.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].DecimalPlaces;
            num_decimal11.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].DecimalPlaces;
            num_decimal12.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].DecimalPlaces;
            num_decimal13.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].DecimalPlaces;
            num_decimal14.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].DecimalPlaces;
            num_decimal15.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].DecimalPlaces;
            num_decimal16.Value = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].DecimalPlaces;
            UpdateUseState();
        }

        /// <summary>
        /// 更新启用状态
        /// </summary>
        private void UpdateUseState()
        {
            num_Offset1.Enabled = chk_输入数据1.Checked;
            num_K1.Enabled = chk_输入数据1.Checked;
            chk_Send1.Enabled = chk_输入数据1.Checked;
            num_Offset2.Enabled = chk_输入数据2.Checked;
            num_K2.Enabled = chk_输入数据2.Checked;
            chk_Send2.Enabled = chk_输入数据2.Checked;
            num_Offset3.Enabled = chk_输入数据3.Checked;
            num_K3.Enabled = chk_输入数据3.Checked;
            chk_Send3.Enabled = chk_输入数据3.Checked;
            num_Offset4.Enabled = chk_输入数据4.Checked;
            num_K4.Enabled = chk_输入数据4.Checked;
            chk_Send4.Enabled = chk_输入数据4.Checked;
            num_Offset5.Enabled = chk_输入数据5.Checked;
            num_K5.Enabled = chk_输入数据5.Checked;
            chk_Send5.Enabled = chk_输入数据5.Checked;
            num_Offset6.Enabled = chk_输入数据6.Checked;
            num_K6.Enabled = chk_输入数据6.Checked;
            chk_Send6.Enabled = chk_输入数据6.Checked;
            num_Offset7.Enabled = chk_输入数据7.Checked;
            num_K7.Enabled = chk_输入数据7.Checked;
            chk_Send7.Enabled = chk_输入数据7.Checked;
            num_Offset8.Enabled = chk_输入数据8.Checked;
            num_K8.Enabled = chk_输入数据8.Checked;
            chk_Send8.Enabled = chk_输入数据8.Checked;
            num_Offset9.Enabled = chk_输入数据9.Checked;
            num_K9.Enabled = chk_输入数据9.Checked;
            chk_Send9.Enabled = chk_输入数据9.Checked;
            num_Offset10.Enabled = chk_输入数据10.Checked;
            num_K10.Enabled = chk_输入数据10.Checked;
            chk_Send10.Enabled = chk_输入数据10.Checked;
            num_Offset11.Enabled = chk_输入数据11.Checked;
            num_K11.Enabled = chk_输入数据11.Checked;
            chk_Send11.Enabled = chk_输入数据11.Checked;
            num_Offset12.Enabled = chk_输入数据12.Checked;
            num_K12.Enabled = chk_输入数据12.Checked;
            chk_Send12.Enabled = chk_输入数据12.Checked;
            num_Offset13.Enabled = chk_输入数据13.Checked;
            num_K13.Enabled = chk_输入数据13.Checked;
            chk_Send13.Enabled = chk_输入数据13.Checked;
            num_Offset14.Enabled = chk_输入数据14.Checked;
            num_K14.Enabled = chk_输入数据14.Checked;
            chk_Send14.Enabled = chk_输入数据14.Checked;
            num_Offset15.Enabled = chk_输入数据15.Checked;
            num_K15.Enabled = chk_输入数据15.Checked;
            chk_Send15.Enabled = chk_输入数据15.Checked;
            num_Offset16.Enabled = chk_输入数据16.Checked;
            num_K16.Enabled = chk_输入数据16.Checked;
            chk_Send16.Enabled = chk_输入数据16.Checked;

            num_Max1.Enabled = chk_输入数据1.Checked;
            cmb_Data1.Enabled = chk_输入数据1.Checked;
            num_Data1.Enabled = chk_输入数据1.Checked;
            num_Min1.Enabled = chk_输入数据1.Checked;
            cmb_Data2.Enabled = chk_输入数据2.Checked;
            num_Data2.Enabled = chk_输入数据2.Checked;
            num_Min2.Enabled = chk_输入数据2.Checked;
            num_Max2.Enabled = chk_输入数据2.Checked;
            cmb_Data3.Enabled = chk_输入数据3.Checked;
            num_Data3.Enabled = chk_输入数据3.Checked;
            num_Min3.Enabled = chk_输入数据3.Checked;
            num_Max3.Enabled = chk_输入数据3.Checked;
            cmb_Data4.Enabled = chk_输入数据4.Checked;
            num_Data4.Enabled = chk_输入数据4.Checked;
            num_Min4.Enabled = chk_输入数据4.Checked;
            num_Max4.Enabled = chk_输入数据4.Checked;
            cmb_Data5.Enabled = chk_输入数据5.Checked;
            num_Data5.Enabled = chk_输入数据5.Checked;
            num_Min5.Enabled = chk_输入数据5.Checked;
            num_Max5.Enabled = chk_输入数据5.Checked;
            cmb_Data6.Enabled = chk_输入数据6.Checked;
            num_Data6.Enabled = chk_输入数据6.Checked;
            num_Min6.Enabled = chk_输入数据6.Checked;
            num_Max6.Enabled = chk_输入数据6.Checked;
            cmb_Data7.Enabled = chk_输入数据7.Checked;
            num_Data7.Enabled = chk_输入数据7.Checked;
            num_Min7.Enabled = chk_输入数据7.Checked;
            num_Max7.Enabled = chk_输入数据7.Checked;
            cmb_Data8.Enabled = chk_输入数据8.Checked;
            num_Data8.Enabled = chk_输入数据8.Checked;
            num_Min8.Enabled = chk_输入数据8.Checked;
            num_Max8.Enabled = chk_输入数据8.Checked;
            cmb_Data9.Enabled = chk_输入数据9.Checked;
            num_Data9.Enabled = chk_输入数据9.Checked;
            num_Min9.Enabled = chk_输入数据9.Checked;
            num_Max9.Enabled = chk_输入数据9.Checked;
            cmb_Data10.Enabled = chk_输入数据10.Checked;
            num_Data10.Enabled = chk_输入数据10.Checked;
            num_Min10.Enabled = chk_输入数据10.Checked;
            num_Max10.Enabled = chk_输入数据10.Checked;
            num_Max11.Enabled = chk_输入数据11.Checked;
            cmb_Data11.Enabled = chk_输入数据11.Checked;
            num_Data11.Enabled = chk_输入数据11.Checked;
            num_Min11.Enabled = chk_输入数据11.Checked;
            cmb_Data12.Enabled = chk_输入数据12.Checked;
            num_Data12.Enabled = chk_输入数据12.Checked;
            num_Min12.Enabled = chk_输入数据12.Checked;
            num_Max12.Enabled = chk_输入数据12.Checked;
            cmb_Data13.Enabled = chk_输入数据13.Checked;
            num_Data13.Enabled = chk_输入数据13.Checked;
            num_Min13.Enabled = chk_输入数据13.Checked;
            num_Max13.Enabled = chk_输入数据13.Checked;
            cmb_Data14.Enabled = chk_输入数据14.Checked;
            num_Data14.Enabled = chk_输入数据14.Checked;
            num_Min14.Enabled = chk_输入数据14.Checked;
            num_Max14.Enabled = chk_输入数据14.Checked;
            cmb_Data15.Enabled = chk_输入数据15.Checked;
            num_Data15.Enabled = chk_输入数据15.Checked;
            num_Min15.Enabled = chk_输入数据15.Checked;
            num_Max15.Enabled = chk_输入数据15.Checked;
            cmb_Data16.Enabled = chk_输入数据16.Checked;
            num_Data16.Enabled = chk_输入数据16.Checked;
            num_Min16.Enabled = chk_输入数据16.Checked;
            num_Max16.Enabled = chk_输入数据16.Checked;
        }

        /// <summary>
        /// 更新下拉框对应索引
        /// </summary>
        /// <param name="cmb_Data"></param>
        /// <param name="iRangeIndex"></param>
        private void UpdateCmbSelect(System.Windows.Forms.ComboBox cmb_Data, int iRangeIndex)
        {
            for (int i = 0; i < cmb_Data.Items.Count; i++)
            {
                if (cmb_Data.Items[i].ToString().Contains(m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[iRangeIndex].SelectName))
                {
                    cmb_Data.SelectedIndex = i;
                }
            }
        }

        /// <summary>
        /// 更新传入数据
        /// </summary>
        private void UpdateInputeData(System.Windows.Forms.ComboBox cmb_Data, EventHandler Cmb_Data_SelectedIndexChanged)
        {
            cmb_Data.Items.Clear();
            if (m_JudgeMethod.ListParaResultAll.Count > 0)
            {
                for (int item = 0; item < m_JudgeMethod.ListParaResultAll.Count; item++)
                {
                    if (m_JudgeMethod.ListParaResultAll[item].ToolName.Contains("匹配工具" + item))
                    {
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Row_" + m_JudgeMethod.ListParaResultAll[item].PatMaxResultPara.Row);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Col_" + m_JudgeMethod.ListParaResultAll[item].PatMaxResultPara.Col);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.PatMaxResultPara.Angle_" + m_JudgeMethod.ListParaResultAll[item].PatMaxResultPara.Angle);
                    }
                    if (m_JudgeMethod.ListParaResultAll[item].ToolName.Contains("Blob工具" + item))
                    {
                        for (int i = 0; i < m_JudgeMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; i++)
                        {
                            cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Row_" + m_JudgeMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Row);
                            cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Col_" + m_JudgeMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Col);
                            cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.BlobResultPara.ListBlobResultPara[" + i + "].Area_" + m_JudgeMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara[i].Area);
                        }
                    }
                    if (m_JudgeMethod.ListParaResultAll[item].ToolName.Contains("找线工具" + item))
                    {
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowStart_" + m_JudgeMethod.ListParaResultAll[item].FindLineResultPara.RowStart);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColStart_" + m_JudgeMethod.ListParaResultAll[item].FindLineResultPara.ColStart);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowEnd_" + m_JudgeMethod.ListParaResultAll[item].FindLineResultPara.RowEnd);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColEnd_" + m_JudgeMethod.ListParaResultAll[item].FindLineResultPara.ColEnd);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.RowMiddle_" + m_JudgeMethod.ListParaResultAll[item].FindLineResultPara.RowMiddle);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.FindLineResultPara.ColMiddle_" + m_JudgeMethod.ListParaResultAll[item].FindLineResultPara.ColMiddle);
                    }
                    if (m_JudgeMethod.ListParaResultAll[item].ToolName.Contains("线线角度工具" + item))
                    {
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParaParaAngleLL.Row_" + m_JudgeMethod.ListParaResultAll[item].ParaParaAngleLL.Row);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParaParaAngleLL.Col_" + m_JudgeMethod.ListParaResultAll[item].ParaParaAngleLL.Col);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParaParaAngleLL.Angle_" + m_JudgeMethod.ListParaResultAll[item].ParaParaAngleLL.Angle);
                    }
                    if (m_JudgeMethod.ListParaResultAll[item].ToolName.Contains("找圆工具" + item))
                    {
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Row_" + m_JudgeMethod.ListParaResultAll[item].FindCircleResultPara.Row);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Col_" + m_JudgeMethod.ListParaResultAll[item].FindCircleResultPara.Col);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.FindCircleResultPara.Radius_" + m_JudgeMethod.ListParaResultAll[item].FindCircleResultPara.Radius);
                    }
                    if (m_JudgeMethod.ListParaResultAll[item].ToolName.Contains("点线距离工具" + item))
                    {
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Row_" + m_JudgeMethod.ListParaResultAll[item].ParaDistancePointLines.Row);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Col_" + m_JudgeMethod.ListParaResultAll[item].ParaDistancePointLines.Col);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointLines.Distance_" + m_JudgeMethod.ListParaResultAll[item].ParaDistancePointLines.Distance);
                    }
                    if (m_JudgeMethod.ListParaResultAll[item].ToolName.Contains("点点距离工具" + item))
                    {
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Distance_" + m_JudgeMethod.ListParaResultAll[item].ParaDistancePointPoints.Distance);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Row_" + m_JudgeMethod.ListParaResultAll[item].ParaDistancePointPoints.Row);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParaDistancePointPoints.Col_" + m_JudgeMethod.ListParaResultAll[item].ParaDistancePointPoints.Col);
                    }
                    if (m_JudgeMethod.ListParaResultAll[item].ToolName.Contains("AngleLX工具" + item))
                    {
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParasAngleLXResult.Angle_" + m_JudgeMethod.ListParaResultAll[item].ParasAngleLXResult.Angle);
                    }
                    if (m_JudgeMethod.ListParaResultAll[item].ToolName.Contains("公式示教工具" + item))
                    {
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParasVisualCorrection.X1_" + m_JudgeMethod.ListParaResultAll[item].ParasVisualCorrection.X1);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParasVisualCorrection.Y1_" + m_JudgeMethod.ListParaResultAll[item].ParasVisualCorrection.Y1);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParasVisualCorrection.X2_" + m_JudgeMethod.ListParaResultAll[item].ParasVisualCorrection.X2);
                        cmb_Data.Items.Add(m_JudgeMethod.ListParaResultAll[item].ToolName + "item.ParasVisualCorrection.Y2_" + m_JudgeMethod.ListParaResultAll[item].ParasVisualCorrection.Y2);
                    }
                }
                cmb_Data.SelectedIndexChanged += Cmb_Data_SelectedIndexChanged;
            }
        }
        /// <summary>
        /// 获取下拉列表选中值
        /// </summary>
        private decimal getComboxValue(string strName) 
        {
            decimal d = 0;
            if (strName != null && strName.Contains("匹配工具"))
            {
                if (strName.Contains("item.PatMaxResultPara.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.PatMaxResultPara.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.PatMaxResultPara.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("线线角度工具"))
            {
                if (strName.Contains("item.ParaParaAngleLL.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaParaAngleLL.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("找线工具"))
            {
                if (strName.Contains("item.FindLineResultPara.RowStart"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.RowEnd"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColEnd"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColStart"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.ColMiddle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindLineResultPara.RowMiddle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("找圆工具"))
            {
                if (strName.Contains("item.FindCircleResultPara.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindCircleResultPara.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.FindCircleResultPara.Radius"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("Blob工具"))
            {
                for (int item = 0; item < m_JudgeMethod.ListParaResultAll.Count; item++)
                {
                    for (int iCount = 0; iCount < m_JudgeMethod.ListParaResultAll[item].BlobResultPara.ListBlobResultPara.Count; iCount++)
                    {
                        string str = "item.BlobResultPara.ListBlobResultPara[" + iCount + "].Col";
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Row"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Col"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                        if (strName.Contains("item.BlobResultPara.ListBlobResultPara[" + iCount + "].Area"))
                        {
                            int i = strName.LastIndexOf('_');
                            string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                            d = decimal.Parse(strData, NumberStyles.Any);
                        }
                    }
                }
            }
            if (strName != null && strName.Contains("点线距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointLines.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointLines.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointLines.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("点点距离工具"))
            {
                if (strName.Contains("item.ParaDistancePointPoints.Distance"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointPoints.Row"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParaDistancePointPoints.Col"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("AngleLX工具"))
            {
                if (strName.Contains("item.ParasAngleLXResult.Angle"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            if (strName != null && strName.Contains("公式示教工具"))
            {
                if (strName.Contains("item.ParasVisualCorrection.X1"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParasVisualCorrection.Y1"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParasVisualCorrection.X2"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
                if (strName.Contains("item.ParasVisualCorrection.Y2"))
                {
                    int i = strName.LastIndexOf('_');
                    string strData = strName.Substring((i + 1), strName.Length - (strName.LastIndexOf('_') + 1));
                    d = decimal.Parse(strData, NumberStyles.Any);
                }
            }
            return d;
        }

        // 数据1选择
        private void Cmb1_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data1.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].DecimalPlaces;
            num_Data1.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据2选择
        private void Cmb2_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data2.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].DecimalPlaces;
            num_Data2.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据3
        private void Cmb3_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data3.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].DecimalPlaces;
            num_Data3.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据4
        private void Cmb4_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data4.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].DecimalPlaces;
            num_Data4.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据5
        private void Cmb5_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data5.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].DecimalPlaces;
            num_Data5.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据6
        private void Cmb6_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data6.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].DecimalPlaces;
            num_Data6.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据7
        private void Cmb7_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data7.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].DecimalPlaces;
            num_Data7.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据8
        private void Cmb8_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data8.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].DecimalPlaces;
            num_Data8.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据9
        private void Cmb9_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data9.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].DecimalPlaces;
            num_Data9.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据10
        private void Cmb10_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data10.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].DecimalPlaces;
            num_Data10.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据11选择
        private void Cmb11_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data11.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].DecimalPlaces;
            num_Data11.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据12选择
        private void Cmb12_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data12.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].DecimalPlaces;
            num_Data12.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据13
        private void Cmb13_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data13.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].DecimalPlaces;
            num_Data13.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据14
        private void Cmb14_Data_SelectedIndexChanged(object sender,EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data14.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].DecimalPlaces;
            num_Data14.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }

        // 数据15
        private void Cmb15_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data15.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].DecimalPlaces;
            num_Data15.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        // 数据16
        private void Cmb16_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComBoxName = (ComboBox)sender;
            string strName = ComBoxName.Text;
            decimal d = 0;
            d = getComboxValue(strName);
            num_Data16.DecimalPlaces = m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].DecimalPlaces;
            num_Data16.Value = d;
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].Data = (double)Math.Round(d, m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].DecimalPlaces);
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].SelectName = ComBoxName.Text.Trim().Substring(0, ComBoxName.Text.Trim().LastIndexOf('_'));
        }


        private void num_Min1_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].MinRange = (double)num_Min1.Value;
        }

        private void num_Max1_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].MaxRange = (double)num_Max1.Value;
        }

        private void num_Min2_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].MinRange = (double)num_Min2.Value;
        }

        private void num_Max2_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].MaxRange = (double)num_Max2.Value;
        }

        private void num_Min3_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].MinRange = (double)num_Min3.Value;
        }

        private void num_Max3_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].MaxRange = (double)num_Max3.Value;
        }

        private void num_Min4_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].MinRange = (double)num_Min4.Value;
        }

        private void num_Max4_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].MaxRange = (double)num_Max4.Value;
        }

        private void num_Min5_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].MinRange = (double)num_Min5.Value;
        }

        private void num_Max5_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].MaxRange = (double)num_Max5.Value;
        }

        private void num_Min6_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].MinRange = (double)num_Min6.Value;
        }

        private void num_Max6_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].MaxRange = (double)num_Max6.Value;
        }

        private void num_Min7_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].MinRange = (double)num_Min7.Value;
        }

        private void num_Max7_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].MaxRange = (double)num_Max7.Value;
        }

        private void num_Min8_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].MinRange = (double)num_Min8.Value;
        }

        private void num_Max8_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].MaxRange = (double)num_Max8.Value;

        }

        private void num_Min9_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].MinRange = (double)num_Min9.Value;
        }

        private void num_Max9_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].MaxRange = (double)num_Max9.Value;
        }

        private void num_Min10_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].MinRange = (double)num_Min10.Value;
        }

        private void num_Max10_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].MaxRange = (double)num_Max10.Value;
        }

        private void chk_输入数据1_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].SelectState = chk_输入数据1.Checked;
            UpdateUseState();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].SelectState = chk_输入数据2.Checked;
            UpdateUseState();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].SelectState = chk_输入数据3.Checked;
            UpdateUseState();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].SelectState = chk_输入数据4.Checked;
            UpdateUseState();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].SelectState = chk_输入数据5.Checked;
            UpdateUseState();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].SelectState = chk_输入数据6.Checked;
            UpdateUseState();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].SelectState = chk_输入数据7.Checked;
            UpdateUseState();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].SelectState = chk_输入数据8.Checked;
            UpdateUseState();

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].SelectState = chk_输入数据9.Checked;
            UpdateUseState();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].SelectState = chk_输入数据10.Checked;
            UpdateUseState();
        }

        private void tsp_run_Click(object sender, EventArgs e)
        {
            tsp_Msg.Text = string.Empty;
            tsp_Time.Text = string.Empty;
            m_JudgeMethod.Run();
            tsp_Msg.Text = m_JudgeMethod.RunMsg;
            tsp_Time.Text = m_JudgeMethod.RunTime.ToString("F1") + "ms";
            string strData = string.Empty;
            UpdateSendData(out strData);
            tsp_SendData.Text = "发送数据：" + strData;
            UpdateResultData();
        }

        /// <summary>
        /// 更新发送数据
        /// </summary>
        /// <param name="strMsg">发送数据</param>
        private void UpdateSendData(out string strMsg)
        {
            strMsg = string.Empty;
            for (int i = 0; i < m_JudgeMethod.ListSendDataMsg.Count; i++)
            {
                strMsg += m_JudgeMethod.ListSendDataMsg[i] + ",";
            }
        }

        /// <summary>
        /// 更新结果
        /// </summary>
        private void UpdateResultData()
        {
            num_Result1.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[0];
            num_Result2.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[1];
            num_Result3.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[2];
            num_Result4.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[3];
            num_Result5.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[4];
            num_Result6.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[5];
            num_Result7.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[6];
            num_Result8.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[7];
            num_Result9.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[8];
            num_Result10.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[9];
            num_Result11.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[10];
            num_Result12.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[11];
            num_Result13.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[12];
            num_Result14.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[13];
            num_Result15.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[14];
            num_Result16.Value = (decimal)m_JudgeMethod.JudgeArrayDispResultData[15];

            txt_Data1.Text = m_JudgeMethod.DispArrayBoolJudgeState[0].ToString();
            txt_Data2.Text = m_JudgeMethod.DispArrayBoolJudgeState[1].ToString();
            txt_Data3.Text = m_JudgeMethod.DispArrayBoolJudgeState[2].ToString();
            txt_Data4.Text = m_JudgeMethod.DispArrayBoolJudgeState[3].ToString();
            txt_Data5.Text = m_JudgeMethod.DispArrayBoolJudgeState[4].ToString();
            txt_Data6.Text = m_JudgeMethod.DispArrayBoolJudgeState[5].ToString();
            txt_Data7.Text = m_JudgeMethod.DispArrayBoolJudgeState[6].ToString();
            txt_Data8.Text = m_JudgeMethod.DispArrayBoolJudgeState[7].ToString();
            txt_Data9.Text = m_JudgeMethod.DispArrayBoolJudgeState[8].ToString();
            txt_Data10.Text = m_JudgeMethod.DispArrayBoolJudgeState[9].ToString();
            txt_Data11.Text = m_JudgeMethod.DispArrayBoolJudgeState[10].ToString();
            txt_Data12.Text = m_JudgeMethod.DispArrayBoolJudgeState[11].ToString();
            txt_Data13.Text = m_JudgeMethod.DispArrayBoolJudgeState[12].ToString();
            txt_Data14.Text = m_JudgeMethod.DispArrayBoolJudgeState[13].ToString();
            txt_Data15.Text = m_JudgeMethod.DispArrayBoolJudgeState[14].ToString();
            txt_Data16.Text = m_JudgeMethod.DispArrayBoolJudgeState[15].ToString();
        }


        private void txt_Split_TextChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.SplitChar = txt_Split.Text.Trim();
        }

        private void num_Offset1_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].Offset = (double)num_Offset1.Value;
        }

        private void num_Offset2_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].Offset = (double)num_Offset2.Value;
        }

        private void num_Offset3_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].Offset = (double)num_Offset3.Value;
        }

        private void num_Offset4_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].Offset = (double)num_Offset4.Value;
        }

        private void num_Offset5_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].Offset = (double)num_Offset5.Value;
        }

        private void num_Offset6_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].Offset = (double)num_Offset6.Value;
        }

        private void num_Offset7_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].Offset = (double)num_Offset7.Value;
        }

        private void num_Offset8_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].Offset = (double)num_Offset8.Value;
        }

        private void num_Offset9_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].Offset = (double)num_Offset9.Value;
        }

        private void num_Offset10_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].Offset = (double)num_Offset10.Value;
        }

        private void num_K1_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].K = (double)num_K1.Value;
        }

        private void num_K2_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].K = (double)num_K2.Value;
        }

        private void num_K3_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].K = (double)num_K3.Value;
        }

        private void num_K4_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].K = (double)num_K4.Value;
        }

        private void num_K5_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].K = (double)num_K5.Value;
        }

        private void num_K6_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].K = (double)num_K6.Value;
        }

        private void num_K7_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].K = (double)num_K7.Value;
        }

        private void num_K8_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].K = (double)num_K8.Value;
        }

        private void num_K9_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].K = (double)num_K9.Value;
        }

        private void num_K10_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].K = (double)num_K10.Value;
        }

        private void chk_Send1_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].SendState = chk_Send1.Checked;
        }

        private void chk_Send2_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].SendState = chk_Send2.Checked;
        }

        private void chk_Send3_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].SendState = chk_Send3.Checked;
        }

        private void chk_Send4_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].SendState = chk_Send4.Checked;

        }

        private void chk_Send5_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].SendState = chk_Send5.Checked;
        }

        private void chk_Send6_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].SendState = chk_Send6.Checked;
        }

        private void chk_Send7_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].SendState = chk_Send7.Checked;
        }

        private void chk_Send8_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].SendState = chk_Send8.Checked;
        }

        private void chk_Send9_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].SendState = chk_Send9.Checked;
        }

        private void chk_Send10_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].SendState = chk_Send10.Checked;
        }

        private void num_OK_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.OK = (int)num_OK.Value;
        }

        private void num_NG_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.NG = (int)num_NG.Value;
        }

        private void chk_OK_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.OKForwardState = chk_OK.Checked;
        }

        private void chk_NG_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.NGForwardState = chk_NG.Checked;
        }

        private void chk_OKBack_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.OKBackState = chk_OKBack.Checked;
        }

        private void chk_NGBack_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.NGBackState = chk_NGBack.Checked;
        }

        private void num_OKBack_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.OKBack = (int)num_OKBack.Value;
        }

        private void num_NGBack_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.NGBack = (int)num_NGBack.Value;
        }

        private void num_decimal1_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].DecimalPlaces = (int)num_decimal1.Value;
        }

        private void num_decimal2_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].DecimalPlaces = (int)num_decimal2.Value;
        }

        private void num_decimal3_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].DecimalPlaces = (int)num_decimal3.Value;
        }

        private void num_decimal4_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].DecimalPlaces = (int)num_decimal4.Value;
        }

        private void num_decimal5_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].DecimalPlaces = (int)num_decimal5.Value;
        }

        private void num_decimal6_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].DecimalPlaces = (int)num_decimal6.Value;
        }

        private void num_decimal7_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].DecimalPlaces = (int)num_decimal7.Value;
        }

        private void num_decimal8_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].DecimalPlaces = (int)num_decimal8.Value;
        }

        private void num_decimal9_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].DecimalPlaces = (int)num_decimal9.Value;
        }

        private void num_decimal10_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].DecimalPlaces = (int)num_decimal10.Value;
        }

        private void num_Offset11_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].Offset = (double)num_Offset11.Value;
        }

        private void num_Offset12_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].Offset = (double)num_Offset12.Value;
        }

        private void num_Offset13_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].Offset = (double)num_Offset13.Value;
        }

        private void num_Offset14_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].Offset = (double)num_Offset14.Value;
        }

        private void num_Offset15_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].Offset = (double)num_Offset15.Value;
        }

        private void num_Offset16_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].Offset = (double)num_Offset16.Value;
        }

        private void num_K16_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].K = (double)num_K16.Value;
        }

        private void num_K15_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].K = (double)num_K15.Value;
        }

        private void num_K14_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].K = (double)num_K14.Value;
        }

        private void num_K13_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].K = (double)num_K13.Value;
        }

        private void num_K12_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].K = (double)num_K12.Value;
        }

        private void num_K11_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].K = (double)num_K11.Value;
        }

        private void num_Min16_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].MinRange = (double)num_Min16.Value;
        }

        private void num_Min15_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].MinRange = (double)num_Min15.Value;
        }

        private void num_Min14_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].MinRange = (double)num_Min14.Value;
        }

        private void num_Min13_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].MinRange = (double)num_Min13.Value;
        }

        private void num_Min12_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].MinRange = (double)num_Min12.Value;
        }

        private void num_Min11_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].MinRange = (double)num_Min11.Value;
        }

        private void num_Max11_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].MinRange = (double)num_Min11.Value;
        }

        private void num_Max12_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].MinRange = (double)num_Min12.Value;
        }

        private void num_Max13_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].MinRange = (double)num_Min13.Value;
        }

        private void num_Max14_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].MinRange = (double)num_Min14.Value;
        }

        private void num_Max15_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].MinRange = (double)num_Min15.Value;
        }

        private void num_Max16ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].MinRange = (double)num_Min16.Value;
        }

        private void num_decimal16_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].DecimalPlaces = (int)num_decimal16.Value;
        }

        private void num_decimal15_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].DecimalPlaces = (int)num_decimal15.Value;
        }

        private void num_decimal14_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].DecimalPlaces = (int)num_decimal14.Value;
        }

        private void num_decimal13_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].DecimalPlaces = (int)num_decimal13.Value;
        }

        private void num_decimal12_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].DecimalPlaces = (int)num_decimal12.Value;
        }

        private void num_decimal11_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].DecimalPlaces = (int)num_decimal11.Value;
        }

        private void chk_Send11_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].SendState = chk_Send11.Checked;
        }

        private void chk_Send12_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].SendState = chk_Send12.Checked;
        }

        private void chk_Send13_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].SendState = chk_Send13.Checked;
        }

        private void chk_Send14_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].SendState = chk_Send14.Checked;
        }

        private void chk_Send15_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].SendState = chk_Send15.Checked;
        }

        private void chk_Send16_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].SendState = chk_Send16.Checked;
        }

        private void chk_Data11_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].SelectState = chk_输入数据11.Checked;
            UpdateUseState();
        }

        private void chk_data12_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].SelectState = chk_输入数据12.Checked;
            UpdateUseState();
        }

        private void chk_data13_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].SelectState = chk_输入数据13.Checked;
            UpdateUseState();
        }

        private void chk_data14_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].SelectState = chk_输入数据14.Checked;
            UpdateUseState();
        }

        private void chk_data15_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].SelectState = chk_输入数据15.Checked;
            UpdateUseState();
        }

        private void chk_data16_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].SelectState = chk_输入数据16.Checked;
            UpdateUseState();
        }

        private void num_BlobHaveBefore_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.HaveOrNotForward = (int)num_BlobHaveBefore.Value;
        }

        private void num_BlobAfter_ValueChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.HaveOrNotBack = (int)num_BlobAfter.Value;
        }

        private void chk_BlobHaveAfter_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.HaveOrNotBackState = chk_BlobHaveAfter.Checked;
            chk_BlobHave1.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave2.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave3.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave4.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave5.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave6.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave7.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave8.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave9.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave10.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave11.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave12.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave13.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave14.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave15.Enabled = chk_BlobHaveAfter.Checked;
            chk_BlobHave16.Enabled = chk_BlobHaveAfter.Checked;
        }

        private void chk_BlobHave1_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[0].HaveOrNotState = chk_BlobHave1.Checked;
        }

        private void chk_BlobHave2_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[1].HaveOrNotState = chk_BlobHave2.Checked;
        }

        private void chk_BlobHave3_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[2].HaveOrNotState = chk_BlobHave3.Checked;
        }

        private void chk_BlobHave4_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[3].HaveOrNotState = chk_BlobHave4.Checked;
        }

        private void chk_BlobHave5_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[4].HaveOrNotState = chk_BlobHave5.Checked;
        }

        private void chk_BlobHave6_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[5].HaveOrNotState = chk_BlobHave6.Checked;
        }

        private void chk_BlobHave7_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[6].HaveOrNotState = chk_BlobHave7.Checked;
        }

        private void chk_BlobHave8_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[7].HaveOrNotState = chk_BlobHave8.Checked;
        }

        private void chk_BlobHave9_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[8].HaveOrNotState = chk_BlobHave9.Checked;
        }

        private void chk_BlobHave10_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[9].HaveOrNotState = chk_BlobHave10.Checked;
        }

        private void chk_BlobHave11_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[10].HaveOrNotState = chk_BlobHave11.Checked;
        }

        private void chk_BlobHave12_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[11].HaveOrNotState = chk_BlobHave12.Checked;
        }

        private void chk_BlobHave13_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[12].HaveOrNotState = chk_BlobHave13.Checked;
        }

        private void chk_BlobHave14_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[13].HaveOrNotState = chk_BlobHave14.Checked;
        }

        private void chk_BlobHave15_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[14].HaveOrNotState = chk_BlobHave15.Checked;
        }

        private void chk_BlobHave16_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.ListSelectRangePara[15].HaveOrNotState = chk_BlobHave16.Checked;
        }

        private void chk_BlobHaveBefore1_CheckedChanged(object sender, EventArgs e)
        {
            m_JudgeMethod.ParasSvJudgeResultParaTool.HaveOrNotForwardState = chk_BlobHaveBefore1.Checked;
            chk_BlobHave1.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave2.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave3.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave4.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave5.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave6.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave7.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave8.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave9.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave10.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave11.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave12.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave13.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave14.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave15.Enabled = chk_BlobHaveBefore1.Checked;
            chk_BlobHave16.Enabled = chk_BlobHaveBefore1.Checked;
        }
    }
}
