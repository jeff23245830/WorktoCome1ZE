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

        // 將原本的
        // private EtherCATFunction.PPMove;
        // 修正為正確的欄位宣告，假設 EtherCATFunction.PPMove 是型別名稱
        private EtherCATFunction.PPMove ppmove;
        public void SetNodeIDList(List<ushort> nodeId)
        {
            this.slaveNodeIdList = nodeId;
            // 你可以在這裡更新 UI，例如 ComboBox
            foreach (var slave in slaveNodeIdList)
            {
                CbNodeId.Items.Add(slave);
            }
        }

        public void SetSlotIDList(List<ushort> slotId)
        {
            this.slaveSlotIdList = slotId;
            // 你可以在這裡更新 UI，例如 ComboBox
            foreach (var slave in slaveSlotIdList)
            {
                CbSlotId.Items.Add(slave);
            }
        }



        public UcControl()
        {
            ppmove = new EtherCATFunction.PPMove();



            InitializeComponent();
             

            

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
    }
}
