using EtherCAT_DLL;
using EtherCAT_DLL_Err;
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
        // 將原本的
        // private EtherCATFunction.PPMove;
        // 修正為正確的欄位宣告，假設 EtherCATFunction.PPMove 是型別名稱
        private EtherCATFunction.PPMove ppmove;
        private EtherCATFunction.IOControl iOControl;

        public ushort Set_nESCExistCards;

        public void SetNodeIDList(List<ushort> nodeId)
        {
            this.slaveNodeIdList = nodeId;
            // 你可以在這裡更新 UI，例如 ComboBox
            foreach (var slave in slaveNodeIdList)
            {
                CbNodeId.Items.Add(slave);
                CbDINodeId.Items.Add(slave);
            }
            if (CbNodeId.Items.Count > 0)
                CbNodeId.SelectedIndex = 0;
            if (CbDINodeId.Items.Count > 0)
                CbDINodeId.SelectedIndex = 0;
        }

        public void SetSlotIDList(List<ushort> slotId)
        {
            this.slaveSlotIdList = slotId;
            // 你可以在這裡更新 UI，例如 ComboBox
            foreach (var slave in slaveSlotIdList)
            {
                CbSlotId.Items.Add(slave);
                CbDISlotId.Items.Add(slave);
            }
            if (CbSlotId.Items.Count > 0)
                CbSlotId.SelectedIndex = 0;
            if (CbDISlotId.Items.Count > 0)
                CbDISlotId.SelectedIndex = 0;

        }



        public UcControl()
        {
            ppmove = new EtherCATFunction.PPMove();
            iOControl = new EtherCATFunction.IOControl();

            iOControl.g_nESCExistCards = Set_nESCExistCards;

            InitializeComponent();

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
            ppmove.TargetPos = TxtTargetPos.Text;
            ppmove.ConstVel = "1000";
            ppmove.Acceleration = TxtAcceleration.Text;
            ppmove.Deceleration = TxtDeceleration.Text;
            ppmove.AxisMove(1, false); // 1 代表正方向移動

        }

        private void BtnMoveLeft_Click(object sender, EventArgs e)
        {
            ppmove.TargetPos = TxtTargetPos.Text;
            ppmove.ConstVel = "1000";
            ppmove.Acceleration = TxtAcceleration.Text;
            ppmove.Deceleration = TxtDeceleration.Text;
            ppmove.AxisMove(0, false); 
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
            ushort ESCNodeID = (ushort)CbDINodeId.SelectedItem;
            ushort ESCSlotID = (ushort)CbDISlotId.SelectedItem;


            iOControl.DOcontorlOutOrOff(uOutputStatus, ESCNodeID , ESCSlotID);


            
        }

        private void CbDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbDINodeId.SelectedIndex > -1 && CbDISlotId.SelectedIndex > -1 )
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

        /*
         if(CbDINodeId.SelectedIndex > 0 &&)
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
        */
    }
}
