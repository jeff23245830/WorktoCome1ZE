
using EtherCATFunction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorktoCome1
{
    public partial class UcControl : UserControl
    {

        private List<ushort> slaveNodeIdList = new List<ushort>();
        private List<ushort> slaveSlotIdList = new List<ushort>();
        CheckBox[] g_pOutputLab = new CheckBox[16];
        Label[] g_pInputLab = new Label[16];
        // 將原本的
        // private EtherCATFunction.PPMove;
        // 修正為正確的欄位宣告，假設 EtherCATFunction.PPMove 是型別名稱
        private MotorMove ppmove;
        private IOControl iOControl;

        public ushort nESCExistCards;

        public void SetNodeIDtoCombobox(List<ushort> nodeId)
        {
            this.slaveNodeIdList = nodeId;
            // 你可以在這裡更新 UI，例如 ComboBox
            foreach (var slave in slaveNodeIdList)
            {
                CbNodeId.Items.Add(slave);
                CbDoNodeId.Items.Add(slave);
                CbDINodeId.Items.Add(slave);
            }
            if (CbNodeId.Items.Count > 0)
                CbNodeId.SelectedIndex = 0;
            if (CbDoNodeId.Items.Count > 0)
                CbDoNodeId.SelectedIndex = 0;
           if (CbDINodeId.Items.Count > 0)
                CbDINodeId.SelectedIndex = 0;

        }

        public void SetSlotIDtoCombobox(List<ushort> slotId)
        {
            this.slaveSlotIdList = slotId;
            // 你可以在這裡更新 UI，例如 ComboBox
            foreach (var slave in slaveSlotIdList)
            {
                CbSlotId.Items.Add(slave);
                CbDoSlotId.Items.Add(slave);
                CbDISlotId.Items.Add(slave);
            }
            if (CbSlotId.Items.Count > 0)
                CbSlotId.SelectedIndex = 0;
            if (CbDoSlotId.Items.Count > 0)
                CbDoSlotId.SelectedIndex = 0;
            if (CbDISlotId.Items.Count > 0)
                CbDISlotId.SelectedIndex = 0;

        }



        public UcControl()
        {
            ppmove = new MotorMove();
            iOControl = new IOControl();

            iOControl.g_nESCExistCards = nESCExistCards;

            InitializeComponent();

            TimCheckStatus.Interval = 100;
            TimCheckStatus.Enabled = true;

            ChkBit00.Enabled = false;
            ChkBit01.Enabled = false;
            ChkBit02.Enabled = false;
            ChkBit03.Enabled = false;
            ChkBit04.Enabled = false;
            ChkBit05.Enabled = false;
            ChkBit06.Enabled = false;
            ChkBit07.Enabled = false;
            ChkBit08.Enabled = false;
            ChkBit09.Enabled = false;
            ChkBit10.Enabled = false;
            ChkBit11.Enabled = false;
            ChkBit12.Enabled = false;
            ChkBit13.Enabled = false;
            ChkBit14.Enabled = false;
            ChkBit15.Enabled = false;

            g_pOutputLab[0] = ChkBit00;
            g_pOutputLab[1] = ChkBit01;
            g_pOutputLab[2] = ChkBit02;
            g_pOutputLab[3] = ChkBit03;
            g_pOutputLab[4] = ChkBit04;
            g_pOutputLab[5] = ChkBit05;
            g_pOutputLab[6] = ChkBit06;
            g_pOutputLab[7] = ChkBit07;
            g_pOutputLab[8] = ChkBit08;
            g_pOutputLab[9] = ChkBit09;
            g_pOutputLab[10] = ChkBit10;
            g_pOutputLab[11] = ChkBit11;
            g_pOutputLab[12] = ChkBit12;
            g_pOutputLab[13] = ChkBit13;
            g_pOutputLab[14] = ChkBit14;
            g_pOutputLab[15] = ChkBit15;

            g_pInputLab[0] = LabBit00;
            g_pInputLab[1] = LabBit01;
            g_pInputLab[2] = LabBit02;
            g_pInputLab[3] = LabBit03;
            g_pInputLab[4] = LabBit04;
            g_pInputLab[5] = LabBit05;
            g_pInputLab[6] = LabBit06;
            g_pInputLab[7] = LabBit07;
            g_pInputLab[8] = LabBit08;
            g_pInputLab[9] = LabBit09;
            g_pInputLab[10] = LabBit10;
            g_pInputLab[11] = LabBit11;
            g_pInputLab[12] = LabBit12;
            g_pInputLab[13] = LabBit13;
            g_pInputLab[14] = LabBit14;
            g_pInputLab[15] = LabBit15;

            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("就緒", "");
            dataGridView1.Rows.Add("切入", "");
            dataGridView1.Rows.Add("允許運轉", "");
            dataGridView1.Rows.Add("故障", "");
            dataGridView1.Rows.Add("電壓關閉", "");
            dataGridView1.Rows.Add("快速停止", "");
            dataGridView1.Rows.Add("關閉", "");
            dataGridView1.Rows.Add("警告", "");
            dataGridView1.Rows.Add("無狀態", "");
            dataGridView1.Rows.Add("遠端", "");
            dataGridView1.Rows.Add("目標達成", "");
            dataGridView1.Rows.Add("內部極限", "");
            dataGridView1.Rows.Add("負極限", "");
            dataGridView1.Rows.Add("正極限", "");
            dataGridView1.Rows.Add("原點", "");
        }

        private void RbSrveroOn_CheckedChanged(object sender, EventArgs e)
        {
            ushort ESCNodeID = (ushort)CbNodeId.SelectedItem;
            ushort ESCSlotID = (ushort)CbSlotId.SelectedItem;
            if (RbSrveroOn.Checked == true)
            {
                ppmove.ServoOnOrOff(true, ESCNodeID, ESCSlotID);
            }
            else
            {
                ppmove.ServoOnOrOff(false, ESCNodeID, ESCSlotID);
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnMoveRight_Click(object sender, EventArgs e)
        { 

            uint ConstVel = Convert.ToUInt32(TxtConstVel.Text); ;
            uint Acceleration = Convert.ToUInt32(TxtAcceleration.Text); 
            uint Deceleration = Convert.ToUInt32(TxtDeceleration.Text); 
            ppmove.AxisMove(1, false , 1000 , ConstVel , Acceleration, Deceleration);//順時針

        }

        private void BtnMoveLeft_Click(object sender, EventArgs e)
        {
            uint ConstVel = Convert.ToUInt32(TxtConstVel.Text); ;
            uint Acceleration = Convert.ToUInt32(TxtAcceleration.Text);
            uint Deceleration = Convert.ToUInt32(TxtDeceleration.Text);
            ppmove.AxisMove(0, false, 1000, ConstVel, Acceleration, Deceleration); //逆時針
        }

        private void ChkBit_CheckedChanged(object sender, EventArgs e)
        {
            ushort uOutputStatus = 0;
            int nOutBit = 0, nStat = 0x0;
            for (nOutBit = 0; nOutBit < 16; nOutBit++)
            {
                if (g_pOutputLab[nOutBit].Checked == true)
                {
                    nStat = nStat + (0x1 << nOutBit);
                    g_pOutputLab[nOutBit].BackColor = Color.Green;
                }
                else
                {
                    g_pOutputLab[nOutBit].BackColor = Color.Red;
                }
            }

            uOutputStatus = (ushort)nStat;//


            //進行輸出狀態設定 到dll檔裡面
            //iOControl.g_uESCNodeID = (ushort)CbNodeId.SelectedItem;
            ushort ESCNodeID = (ushort)CbDoNodeId.SelectedItem;
            ushort ESCSlotID = (ushort)CbDoSlotId.SelectedItem;

            //之後要加入Set_nESCExistCards
            iOControl.DOcontorlOutOrOff(uOutputStatus, ESCNodeID , ESCSlotID);


            
        }

        private void CbDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbDoNodeId.SelectedIndex > -1 && CbDoSlotId.SelectedIndex > -1 )
            {
                ChkBit00.Enabled = true;
                ChkBit01.Enabled = true;
                ChkBit02.Enabled = true;
                ChkBit03.Enabled = true;
                ChkBit04.Enabled = true;
                ChkBit05.Enabled = true;
                ChkBit06.Enabled = true;
                ChkBit07.Enabled = true;
                ChkBit08.Enabled = true;
                ChkBit09.Enabled = true;
                ChkBit10.Enabled = true;
                ChkBit11.Enabled = true;
                ChkBit12.Enabled = true;
                ChkBit13.Enabled = true;
                ChkBit14.Enabled = true;
                ChkBit15.Enabled = true;
            }
        }

        private void TimCheckStatus_Tick(object sender, EventArgs e)
        {
            ushort  uValue = 0, uRet = 0;
            bool bRet ;
            ushort g_uESCNodeID , g_uESCSlotID , g_uESCCardNo = 0;
            int nBit = 0;
            if (nESCExistCards > 0)
            {

                //亮亮
                if ( true)
                {

                    //g_uESCCardNo = Set_nESCExistCards;
                    g_uESCNodeID = (ushort)CbDINodeId.SelectedItem;
                    g_uESCSlotID = (ushort)CbDISlotId.SelectedItem;

                    bRet = iOControl.DIcontrolInOrOff( (ushort)CbDINodeId.SelectedItem ,(ushort)CbDISlotId.SelectedItem ,ref uValue);

                    if (bRet)
                    {
                        for (nBit = 0; nBit < 16; nBit++)
                        {
                            if ((uValue & (0x1 << nBit)) == (0x1 << nBit))
                                g_pInputLab[nBit].BackColor = Color.Green;
                            else
                                g_pInputLab[nBit].BackColor = Color.Red;
                        }
                    }


                }
                 
            }
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt_cheak_Click(object sender, EventArgs e)
        {
            ushort uValue = 0;
            iOControl.DOcontrolRead((ushort)CbDoNodeId.SelectedItem, (ushort)CbDoSlotId.SelectedItem, ref uValue);
        }
    }
}
