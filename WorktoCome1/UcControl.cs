
using EtherCATFunction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WorktoCome1
{
    public partial class UcControl : UserControl
    {
        private readonly AppState _appState;
        private string jsonFilePath = AppPaths.RecipePath;
        private bool _updating = false;

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

        
        public UcControl(AppState appState)
        {
            _appState = appState;

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
        #region 非事件方法

        public void UserRole()
        {
            btnDOSave.Enabled = false;
            btnDISave.Enabled = false;
            txtDO00.Enabled = false;
            txtDO01.Enabled = false;
            txtDO02.Enabled = false;
            txtDO03.Enabled = false;
            txtDO04.Enabled = false;
            txtDO05.Enabled = false;
            txtDO06.Enabled = false;
            txtDO07.Enabled = false;
            txtDO08.Enabled = false;
            txtDO09.Enabled = false;
            txtDO10.Enabled = false;
            txtDO11.Enabled = false;
            txtDO12.Enabled = false;
            txtDO13.Enabled = false;
            txtDO14.Enabled = false;
            txtDO15.Enabled = false;
            txtDI00.Enabled = false;
            txtDI01.Enabled = false;
            txtDI02.Enabled = false;
            txtDI03.Enabled = false;
            txtDI04.Enabled = false;
            txtDI05.Enabled = false;
            txtDI06.Enabled = false;
            txtDI07.Enabled = false;
            txtDI08.Enabled = false;
            txtDI09.Enabled = false;
            txtDI10.Enabled = false;
            txtDI11.Enabled = false;
            txtDI12.Enabled = false;
            txtDI13.Enabled = false;
            txtDI14.Enabled = false;
            txtDI15.Enabled = false;
            CbDONodeId.DropDownStyle = ComboBoxStyle.DropDownList;
            CbDOSlotId.DropDownStyle = ComboBoxStyle.DropDownList;
            CbDINodeId.DropDownStyle = ComboBoxStyle.DropDownList;  
            CbDISlotId.DropDownStyle = ComboBoxStyle.DropDownList;



        }

        public void EngerRole()
        {

        }

        private void LoadDIByNodeSlot(int nodeId, int slotId)
        {
            try
            {
                _updating = true;

                // 1) 清空舊值
                ClearDItextBox();

                // 2) 取目前產品與配方
                var product = _appState?.CurrentProducTitle;
                if (string.IsNullOrWhiteSpace(product)) return;

                var root = _appState.RootObject;
                if (root?.Products == null || !root.Products.TryGetValue(product, out var recipe)) return;

                // 3) 取 DO 區
                if (!recipe.DioFunctions.TryGetValue("DI", out var doSection)) return;
                if (doSection?.NodeGroups == null || doSection.NodeGroups.Count == 0) return;

                // 4) 找到 (NodeID, SlotID) 完全匹配的組別
                string matchGroupName = null;
                DioGroup matchGroup = null;

                // 先嘗試使用目前功能組選擇（若它剛好符合 node/slot，優先使用）
                if (!string.IsNullOrWhiteSpace(CbDIfunction.Text) &&
                    doSection.NodeGroups.TryGetValue(CbDIfunction.Text, out var maybeGroup) &&
                    maybeGroup.NodeID == nodeId && maybeGroup.SlotID == slotId)
                {
                    matchGroupName = CbDIfunction.Text;
                    matchGroup = maybeGroup;
                }
                else
                {
                    // 否則就掃描所有群組找第一個吻合的
                    foreach (var kv in doSection.NodeGroups)
                    {
                        var g = kv.Value;
                        if (g != null && g.NodeID == nodeId && g.SlotID == slotId)
                        {
                            matchGroupName = kv.Key;
                            matchGroup = g;
                            break;
                        }
                    }
                }

                if (matchGroup == null)
                {
                    // 沒有匹配就保持空白（或你也可以提示）
                    return;
                }

                // 5) 同步功能組下拉顯示（避免 UI 不一致）
                int idx = CbDIfunction.Items.IndexOf(matchGroupName);
                if (idx >= 0) CbDIfunction.SelectedIndex = idx; else CbDIfunction.Text = matchGroupName;

                // 6) 回填 00..15
                var map = new Dictionary<string, TextBox>
                {
                    ["00"] = txtDI00,
                    ["01"] = txtDI01,
                    ["02"] = txtDI02,
                    ["03"] = txtDI03,
                    ["04"] = txtDI04,
                    ["05"] = txtDI05,
                    ["06"] = txtDI06,
                    ["07"] = txtDI07,
                    ["08"] = txtDI08,
                    ["09"] = txtDI09,
                    ["10"] = txtDI10,
                    ["11"] = txtDI11,
                    ["12"] = txtDI12,
                    ["13"] = txtDI13,
                    ["14"] = txtDI14,
                    ["15"] = txtDI15
                };

                foreach (var kv in map)
                {
                    string val = null;
                    matchGroup.Function?.TryGetValue(kv.Key, out val);
                    kv.Value.Text = val ?? string.Empty;
                }
            }
            finally
            {
                _updating = false;
            }
        }
        private void LoadDoByNodeSlot(int nodeId, int slotId)
        {
            try
            {
                _updating = true;

                // 1) 清空舊值
                ClearDOtextBox();

                // 2) 取目前產品與配方
                var product = _appState?.CurrentProducTitle;
                if (string.IsNullOrWhiteSpace(product)) return;

                var root = _appState.RootObject;
                if (root?.Products == null || !root.Products.TryGetValue(product, out var recipe)) return;

                // 3) 取 DO 區
                if (!recipe.DioFunctions.TryGetValue("DO", out var doSection)) return;
                if (doSection?.NodeGroups == null || doSection.NodeGroups.Count == 0) return;

                // 4) 找到 (NodeID, SlotID) 完全匹配的組別
                string matchGroupName = null;
                DioGroup matchGroup = null;

                // 先嘗試使用目前功能組選擇（若它剛好符合 node/slot，優先使用）
                if (!string.IsNullOrWhiteSpace(CbDOfunction.Text) &&
                    doSection.NodeGroups.TryGetValue(CbDOfunction.Text, out var maybeGroup) &&
                    maybeGroup.NodeID == nodeId && maybeGroup.SlotID == slotId)
                {
                    matchGroupName = CbDOfunction.Text;
                    matchGroup = maybeGroup;
                }
                else
                {
                    // 否則就掃描所有群組找第一個吻合的
                    foreach (var kv in doSection.NodeGroups)
                    {
                        var g = kv.Value;
                        if (g != null && g.NodeID == nodeId && g.SlotID == slotId)
                        {
                            matchGroupName = kv.Key;
                            matchGroup = g;
                            break;
                        }
                    }
                }

                if (matchGroup == null)
                {
                    // 沒有匹配就保持空白（或你也可以提示）
                    return;
                }

                // 5) 同步功能組下拉顯示（避免 UI 不一致）
                int idx = CbDOfunction.Items.IndexOf(matchGroupName);
                if (idx >= 0) CbDOfunction.SelectedIndex = idx; else CbDOfunction.Text = matchGroupName;

                // 6) 回填 00..15
                var map = new Dictionary<string, TextBox>
                {
                    ["00"] = txtDO00,
                    ["01"] = txtDO01,
                    ["02"] = txtDO02,
                    ["03"] = txtDO03,
                    ["04"] = txtDO04,
                    ["05"] = txtDO05,
                    ["06"] = txtDO06,
                    ["07"] = txtDO07,
                    ["08"] = txtDO08,
                    ["09"] = txtDO09,
                    ["10"] = txtDO10,
                    ["11"] = txtDO11,
                    ["12"] = txtDO12,
                    ["13"] = txtDO13,
                    ["14"] = txtDO14,
                    ["15"] = txtDO15
                };

                foreach (var kv in map)
                {
                    string val = null;
                    matchGroup.Function?.TryGetValue(kv.Key, out val);
                    kv.Value.Text = val ?? string.Empty;
                }
            }
            finally
            {
                _updating = false;
            }
        }
        public void LoadFunctionGroups()
        {

            //1.載入存在APPSTATE的CurrentRecipe
            string CurrentProduc = _appState.CurrentProducTitle;
            if (string.IsNullOrWhiteSpace(CurrentProduc))
            {
                MessageBox.Show("請先選擇一個產品以載入參數。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_appState.CurrentRecipe?.DioFunctions != null)
            {

                ClearDItextBox();
                ClearDOtextBox();
                CbDOfunction.Items.Clear();
                CbDIfunction.Items.Clear();

                // 載入 DO 功能組
                if (_appState.CurrentRecipe.DioFunctions.ContainsKey("DO"))
                {
                    var doGroups = _appState.CurrentRecipe.DioFunctions["DO"].NodeGroups;
                    foreach (var groupName in doGroups.Keys)
                    {
                        CbDOfunction.Items.Add(groupName);
                    }
                }

                // 載入 DI 功能組
                if (_appState.CurrentRecipe.DioFunctions.ContainsKey("DI"))
                {
                    var diGroups = _appState.CurrentRecipe.DioFunctions["DI"].NodeGroups;
                    foreach (var groupName in diGroups.Keys)
                    {
                        CbDIfunction.Items.Add(groupName);
                    }
                }
            }
            else
            {
                ClearDItextBox();
                ClearDOtextBox();
                CbDOfunction.Items.Clear();
                CbDIfunction.Items.Clear();
            }
        }

        void ClearDItextBox()
        {
            txtDI00.Text = "";
            txtDI01.Text = "";
            txtDI02.Text = "";
            txtDI03.Text = "";
            txtDI04.Text = "";
            txtDI05.Text = "";
            txtDI06.Text = "";
            txtDI07.Text = "";
            txtDI08.Text = "";
            txtDI09.Text = "";
            txtDI10.Text = "";
            txtDI11.Text = "";
            txtDI12.Text = "";
            txtDI13.Text = "";
            txtDI14.Text = "";
            txtDI15.Text = "";
        }
        void ClearDOtextBox()
        {
            txtDO00.Text = "";
            txtDO01.Text = "";
            txtDO02.Text = "";
            txtDO03.Text = "";
            txtDO04.Text = "";
            txtDO05.Text = "";
            txtDO06.Text = "";
            txtDO07.Text = "";
            txtDO08.Text = "";
            txtDO09.Text = "";
            txtDO10.Text = "";
            txtDO11.Text = "";
            txtDO12.Text = "";
            txtDO13.Text = "";
            txtDO14.Text = "";
            txtDO15.Text = "";
        }

        public void SetNodeIDtoCombobox(List<ushort> nodeId)
        {
            CbDINodeId.Items.Clear();
            CbDONodeId.Items.Clear();   
            CbNodeId.Items.Clear();
            this.slaveNodeIdList = nodeId;
            // 你可以在這裡更新 UI，例如 ComboBox
            foreach (var slave in slaveNodeIdList)
            {
                CbNodeId.Items.Add(slave);
                CbDONodeId.Items.Add(slave);
                CbDINodeId.Items.Add(slave);
            }
            if (CbNodeId.Items.Count > 0)
                CbNodeId.SelectedIndex = 0;
            if (CbDONodeId.Items.Count > 0)
                CbDONodeId.SelectedIndex = 0;
            if (CbDINodeId.Items.Count > 0)
                CbDINodeId.SelectedIndex = 0;
        }

        public void SetSlotIDtoCombobox(List<ushort> slotId)
        {
            CbSlotId.Items.Clear();
            CbDOSlotId.Items.Clear();
            CbDISlotId.Items.Clear();

            this.slaveSlotIdList = slotId;
            // 你可以在這裡更新 UI，例如 ComboBox
            foreach (var slave in slaveSlotIdList)
            {
                CbSlotId.Items.Add(slave);
                CbDOSlotId.Items.Add(slave);
                CbDISlotId.Items.Add(slave);
            }

            if (CbSlotId.Items.Count > 0)
                CbSlotId.SelectedIndex = 0;
            if (CbDOSlotId.Items.Count > 0)
                CbDOSlotId.SelectedIndex = 0;
            if (CbDISlotId.Items.Count > 0)
                CbDISlotId.SelectedIndex = 0;
        }

       
        #endregion
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
            int TargetNumb = 0;

            if (rb_TargetNumb.Checked == true)
            {
                TargetNumb = (int)NudTargetNumb.Value;
            }
            if (rb_Target0_01.Checked == true)
            {
                TargetNumb = 10;
            }
            if (rb_Target0_1.Checked == true)
            {
                TargetNumb = 100;
            }
            if (rb_Target1.Checked == true)
            {
                TargetNumb = 1000;
            }

            ppmove.AxisMove(1, false, 1000, ConstVel, Acceleration, Deceleration, (ushort)CbNodeId.SelectedItem, (ushort)CbSlotId.SelectedItem);

            bool check = false;
            do
            {
                check = false;//cATFunction.checkDone();

                // 可以加個延遲，避免吃滿 CPU
            } while (check);
        }

        private void BtnMoveLeft_Click(object sender, EventArgs e)
        {
            uint ConstVel = Convert.ToUInt32(TxtConstVel.Text); ;
            uint Acceleration = Convert.ToUInt32(TxtAcceleration.Text);
            uint Deceleration = Convert.ToUInt32(TxtDeceleration.Text);
            int TargetNumb = 0;

            if (rb_TargetNumb.Checked == true) 
            {
                TargetNumb = (int)NudTargetNumb.Value;
            }
            if(rb_Target0_01.Checked == true)
            {
                TargetNumb = 10;
            }
            if (rb_Target0_1.Checked == true)
            {
                TargetNumb = 100;
            }
            if (rb_Target1.Checked == true)
            {
                TargetNumb = 1000;
            }

            ppmove.AxisMove(0, false, TargetNumb, ConstVel, Acceleration, Deceleration, (ushort)CbNodeId.SelectedItem, (ushort)CbSlotId.SelectedItem);

            bool check = false;
            do
            {
                check = false;//cATFunction.checkDone();

                // 可以加個延遲，避免吃滿 CPU
            } while (check);


        }

        private void ChkBit_CheckedChanged(object sender, EventArgs e)
        {
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
             
            //之後要加入Set_nESCExistCards
            bool Ret = iOControl.DOcontorlOutOrOff((ushort)nStat, (ushort)CbDONodeId.SelectedItem, (ushort)CbDOSlotId.SelectedItem);
        }

        private void CbDI_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TimCheckStatus_Tick(object sender, EventArgs e)
        {
            bool bRet ;
            ushort uValue = 0;
            int nBit = 0;
            if (nESCExistCards > 0)
            {
                //亮亮
                if (true)
                {
                    bRet = iOControl.DIcontrolInOrOff((ushort)CbDINodeId.SelectedItem, (ushort)CbDISlotId.SelectedItem, ref uValue);

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

        private void btn_check_Click(object sender, EventArgs e)
        {
            ushort uValue = 0;
            iOControl.DOcontrolRead((ushort)CbDONodeId.SelectedItem, (ushort)CbDOSlotId.SelectedItem, ref uValue);
        }

        private void BtHome_Click(object sender, EventArgs e)
        {
            uint Deceleration = Convert.ToUInt32(TxtDeceleration.Text);
            uint FV = Convert.ToUInt32(txtFv.Text);
            uint SV = Convert.ToUInt32(txtSV.Text);
            ushort Mode = (ushort)NudHomeMod.Value;

            ppmove.MoveHome((ushort)CbNodeId.SelectedItem, (ushort)CbSlotId.SelectedItem, Mode, int.Parse(TxtOffset.Text), FV, SV, Deceleration);
        }

        private void bt_check_Click(object sender, EventArgs e)
        {
            if(ppmove.checkDone((ushort)CbNodeId.SelectedItem, (ushort)CbSlotId.SelectedItem))
            {
                MessageBox.Show("到位");
            }
            else
            {
                MessageBox.Show("未到位");
            }
        }

        

        private void btnDOSave_Click(object sender, EventArgs e)
        {
            // 1) 基本檢查
            string currentProductTitle = _appState.CurrentProducTitle;
            if (string.IsNullOrWhiteSpace(currentProductTitle))
            {
                MessageBox.Show("請先載入產品。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) 讀 Node / Slot（優先 SelectedItem）
            int nodeId, slotId;

            if (CbDONodeId.SelectedItem is ushort nodeSel)
                nodeId = nodeSel;
            else if (!int.TryParse(CbDONodeId.Text, out nodeId))
            {
                MessageBox.Show("請選擇正確的 Node-ID。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CbDOSlotId.SelectedItem is ushort slotSel)
                slotId = slotSel;
            else if (!int.TryParse(CbDOSlotId.Text, out slotId))
            {
                MessageBox.Show("請選擇正確的 Slot-ID。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3) 取 Root/Recipe
            var root = _appState.RootObject;
            if (root?.Products == null || !root.Products.TryGetValue(currentProductTitle, out var recipe))
            {
                MessageBox.Show($"找不到產品 '{currentProductTitle}' 的資料。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4) 取/建 DioFunctions["DO"]
            if (!recipe.DioFunctions.TryGetValue("DO", out var doSection) || doSection == null)
            {
                doSection = new Dio();
                recipe.DioFunctions["DO"] = doSection;
            }
            if (doSection.NodeGroups == null)
            {
                doSection.NodeGroups = new Dictionary<string, DioGroup>();
            }

            // 5) key = "NodeID-SlotID"
            string key = $"{nodeId}-{slotId}"; 
            // 6) 取/建群組並回填 Node/Slot
            if (!doSection.NodeGroups.TryGetValue(key, out var group) || group == null)
            {
                group = new DioGroup();
                doSection.NodeGroups[key] = group;
            }
            group.NodeID = nodeId;
            group.SlotID = slotId;

            // 7) 收集 00..15
            var textBoxes = new Dictionary<string, TextBox>
            {
                ["00"] = txtDO00,
                ["01"] = txtDO01,
                ["02"] = txtDO02,
                ["03"] = txtDO03,
                ["04"] = txtDO04,
                ["05"] = txtDO05,
                ["06"] = txtDO06,
                ["07"] = txtDO07,
                ["08"] = txtDO08,
                ["09"] = txtDO09,
                ["10"] = txtDO10,
                ["11"] = txtDO11,
                ["12"] = txtDO12,
                ["13"] = txtDO13,
                ["14"] = txtDO14,
                ["15"] = txtDO15
            };
            var func = new Dictionary<string, string>();
            foreach (var kv in textBoxes)
                func[kv.Key] = kv.Value?.Text?.Trim() ?? string.Empty;
            group.Function = func;

            // 8) 存檔
            JsonFunction.SaveJson(jsonFilePath, root);

            // 9) 更新功能組下拉：顯示 NodeID-SlotID
            LoadFunctionGroups();
            int idx = CbDOfunction.Items.IndexOf(key);
            if (idx >= 0) CbDOfunction.SelectedIndex = idx; else CbDOfunction.Text = key;

            MessageBox.Show($"DO 功能組「{key}」已儲存。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnDISave_Click(object sender, EventArgs e)
        {
            // 1) 基本檢查
            string currentProductTitle = _appState.CurrentProducTitle;
            if (string.IsNullOrWhiteSpace(currentProductTitle))
            {
                MessageBox.Show("請先載入產品。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) 讀 Node / Slot（優先 SelectedItem）
            int nodeId, slotId;

            if (CbDINodeId.SelectedItem is ushort nodeSel)
                nodeId = nodeSel;
            else if (!int.TryParse(CbDINodeId.Text, out nodeId))
            {
                MessageBox.Show("請選擇正確的 Node-ID。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CbDISlotId.SelectedItem is ushort slotSel)
                slotId = slotSel;
            else if (!int.TryParse(CbDISlotId.Text, out slotId))
            {
                MessageBox.Show("請選擇正確的 Slot-ID。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3) 取 Root/Recipe
            var root = _appState.RootObject;
            if (root?.Products == null || !root.Products.TryGetValue(currentProductTitle, out var recipe))
            {
                MessageBox.Show($"找不到產品 '{currentProductTitle}' 的資料。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4) 取/建 DioFunctions["DO"]
            if (!recipe.DioFunctions.TryGetValue("DI", out var doSection) || doSection == null)
            {
                doSection = new Dio();
                recipe.DioFunctions["DI"] = doSection;
            }
            if (doSection.NodeGroups == null)
            {
                doSection.NodeGroups = new Dictionary<string, DioGroup>();
            }

            // 5) key = "NodeID-SlotID"
            string key = $"{nodeId}-{slotId}";
            // 6) 取/建群組並回填 Node/Slot
            if (!doSection.NodeGroups.TryGetValue(key, out var group) || group == null)
            {
                group = new DioGroup();
                doSection.NodeGroups[key] = group;
            }
            group.NodeID = nodeId;
            group.SlotID = slotId;

            // 7) 收集 00..15
            var textBoxes = new Dictionary<string, TextBox>
            {
                ["00"] = txtDI00,
                ["01"] = txtDI01,
                ["02"] = txtDI02,
                ["03"] = txtDI03,
                ["04"] = txtDI04,
                ["05"] = txtDI05,
                ["06"] = txtDI06,
                ["07"] = txtDI07,
                ["08"] = txtDI08,
                ["09"] = txtDI09,
                ["10"] = txtDI10,
                ["11"] = txtDI11,
                ["12"] = txtDI12,
                ["13"] = txtDI13,
                ["14"] = txtDI14,
                ["15"] = txtDI15
            };
            var func = new Dictionary<string, string>();
            foreach (var kv in textBoxes)
                func[kv.Key] = kv.Value?.Text?.Trim() ?? string.Empty;
            group.Function = func;

            // 8) 存檔
            JsonFunction.SaveJson(jsonFilePath, root);

            // 9) 更新功能組下拉：顯示 NodeID-SlotID
            LoadFunctionGroups();
            int idx = CbDIfunction.Items.IndexOf(key);
            if (idx >= 0) CbDIfunction.SelectedIndex = idx; else CbDIfunction.Text = key;

            MessageBox.Show($"DI 功能組「{key}」已儲存。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }




        private void CbDIfunction_SelectedIndexChanged(object sender, EventArgs e)
        {

            ClearDItextBox();

            string selectedGroup = CbDIfunction.Text;
            // 你的狀態來源可能是 DI 的 _appState，也可能是靜態 AppState，兩個都嘗試一下
            string productTitle = _appState?.CurrentProducTitle;
            if (string.IsNullOrWhiteSpace(productTitle))
                productTitle = _appState.CurrentProducTitle;  // 備援

            // 1) 基本檢查
            if (string.IsNullOrWhiteSpace(productTitle) || string.IsNullOrWhiteSpace(selectedGroup))
                return;

            // 2) 取出目前 RootObject 與該產品的 Recipe
            var root = _appState.RootObject; // 專案有把反序列化結果放進這裡用
            if (root == null || root.Products == null || !root.Products.TryGetValue(productTitle, out var recipe))
                return;

            // 3) 取出 DI 區域
            if (!recipe.DioFunctions.TryGetValue("DI", out var doSection))
                return;

            // 4) 取出選定的功能組
            if (!doSection.NodeGroups.TryGetValue(selectedGroup, out var group) || group == null)
                return;

            // 5) 回填 NodeID / SlotID（如果你的下拉選單是固定選項，也可以用 SelectedItem/SelectedValue）
            CbDINodeId.Text = group.NodeID.ToString();
            CbDISlotId.Text = group.SlotID.ToString();

            // 6) 回填 00..15 的功能字串
            var map = new Dictionary<string, TextBox>
            {
                ["00"] = txtDI00,
                ["01"] = txtDI01,
                ["02"] = txtDI02,
                ["03"] = txtDI03,
                ["04"] = txtDI04,
                ["05"] = txtDI05,
                ["06"] = txtDI06,
                ["07"] = txtDI07,
                ["08"] = txtDI08,
                ["09"] = txtDI09,
                ["10"] = txtDI10,
                ["11"] = txtDI11,
                ["12"] = txtDI12,
                ["13"] = txtDI13,
                ["14"] = txtDI14,
                ["15"] = txtDI15
            };

            foreach (var kv in map)
            {
                string val = null;
                if (group.Function != null)
                    group.Function.TryGetValue(kv.Key, out val);
                kv.Value.Text = val ?? string.Empty; // 沒存過就留空
            }
        }

        private void CbDONodeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updating) return;
            if (!int.TryParse(CbDONodeId.Text, out var nodeId)) return;
            if (!int.TryParse(CbDOSlotId.Text, out var slotId)) slotId = 0;   // 沒選到 Slot 就當 0
            LoadDoByNodeSlot(nodeId, slotId);
        }
        private void CbDOSlotId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updating) return;
            if (!int.TryParse(CbDONodeId.Text, out var nodeId)) return;
            if (!int.TryParse(CbDOSlotId.Text, out var slotId)) return;
            LoadDoByNodeSlot(nodeId, slotId);
        }

        private void CbDINodeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updating) return;
            if (!int.TryParse(CbDINodeId.Text, out var nodeId)) return;
            if (!int.TryParse(CbDISlotId.Text, out var slotId)) slotId = 0;   // 沒選到 Slot 就當 0
            LoadDIByNodeSlot(nodeId, slotId);
        }

        private void CbDISlotId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updating) return;
            if (!int.TryParse(CbDINodeId.Text, out var nodeId)) return;
            if (!int.TryParse(CbDISlotId.Text, out var slotId)) return;
            LoadDIByNodeSlot(nodeId, slotId);
        }

        private void btnDODelete_Click(object sender, EventArgs e)
        {

        }
    }
}
