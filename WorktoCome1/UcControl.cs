
using EtherCATFunction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WorktoCome1
{
    public partial class UcControl : UserControl
    {
        private readonly AppState _appState;
        private string jsonFilePath = AppPaths.RecipePath;


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

        public void LoadDIOJson(string filePath)
        {
             
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
            if (CbDONodeId.SelectedIndex > -1 && CbDOSlotId.SelectedIndex > -1 )
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

        private void btnDISave_Click(object sender, EventArgs e)
        {

        }

        private void btnDOSave_Click(object sender, EventArgs e)
        {
            // 1) 基本檢查
            string currentProducTitle = _appState.CurrentProducTitle;
            string selectedFunctionName = CbDOfunction.Text;

            if (string.IsNullOrWhiteSpace(currentProducTitle) || string.IsNullOrWhiteSpace(selectedFunctionName))
            {
                MessageBox.Show("請先載入產品並輸入或選擇一個功能組。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) 取得 RootObject 與目前產品的 Recipe
            var root = _appState.RootObject; // 你專案已有：載入/反序列化後放進這裡使用
            if (root == null || root.Products == null || !root.Products.TryGetValue(currentProducTitle, out var recipe))
            {
                MessageBox.Show($"找不到產品 '{currentProducTitle}' 的資料。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3) 取得/建立 DioFunctions["DO"]
            if (!recipe.DioFunctions.TryGetValue("DO", out var doSection))
            {
                doSection = new Dio();
                recipe.DioFunctions["DO"] = doSection;
            }

            // 4) 取得/建立 指定功能組
            if (!doSection.NodeGroups.TryGetValue(selectedFunctionName, out var group))
            {
                group = new DioGroup();
                doSection.NodeGroups[selectedFunctionName] = group;
            }


            // 5) 從 UI 讀取 NodeID / SlotID
            int nodeId = 0, slotId = 0;
            int.TryParse(CbDONodeId.Text, out nodeId);
            int.TryParse(CbDOSlotId.Text, out slotId);
            group.NodeID = nodeId;
            group.SlotID = slotId;

            // 6) 把 00..15 的功能字串收集進 Dictionary（保留前導 0）
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

            // 你可以選擇：保留空字串，或過濾掉空白鍵
            var func = new Dictionary<string, string>();
            foreach (var kv in textBoxes)
            {
                var v = kv.Value?.Text?.Trim() ?? "";
                // 若不想存空白就改成：if (!string.IsNullOrEmpty(v)) func[kv.Key] = v;
                func[kv.Key] = v;
            }
            group.Function = func; // 整包覆蓋

            // 7) 寫回 Recipe.json 
            JsonFunction.SaveJson(jsonFilePath, root);
              

            MessageBox.Show($"DO 功能組「{selectedFunctionName}」已儲存。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);


            LoadFunctionGroups();




        }

        private void btnDISave_Click_1(object sender, EventArgs e)
        {
            // 1) 基本檢查
            string currentProducTitle = _appState.CurrentProducTitle;
            string selectedFunctionName = CbDIfunction.Text;

            if (string.IsNullOrWhiteSpace(currentProducTitle) || string.IsNullOrWhiteSpace(selectedFunctionName))
            {
                MessageBox.Show("請先載入產品並輸入或選擇一個功能組。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) 取得 RootObject 與目前產品的 Recipe
            var root = _appState.RootObject; // 你專案已有：載入/反序列化後放進這裡使用
            if (root == null || root.Products == null || !root.Products.TryGetValue(currentProducTitle, out var recipe))
            {
                MessageBox.Show($"找不到產品 '{currentProducTitle}' 的資料。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3) 取得/建立 DioFunctions["DO"]
            if (!recipe.DioFunctions.TryGetValue("DI", out var doSection))
            {
                doSection = new Dio();
                recipe.DioFunctions["DI"] = doSection;
            }

            // 4) 取得/建立 指定功能組
            if (!doSection.NodeGroups.TryGetValue(selectedFunctionName, out var group))
            {
                group = new DioGroup();
                doSection.NodeGroups[selectedFunctionName] = group;
            }


            // 5) 從 UI 讀取 NodeID / SlotID
            int nodeId = 0, slotId = 0;
            int.TryParse(CbDINodeId.Text, out nodeId);
            int.TryParse(CbDISlotId.Text, out slotId);
            group.NodeID = nodeId;
            group.SlotID = slotId;

            // 6) 把 00..15 的功能字串收集進 Dictionary（保留前導 0）
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

            // 你可以選擇：保留空字串，或過濾掉空白鍵
            var func = new Dictionary<string, string>();
            foreach (var kv in textBoxes)
            {
                var v = kv.Value?.Text?.Trim() ?? "";
                // 若不想存空白就改成：if (!string.IsNullOrEmpty(v)) func[kv.Key] = v;
                func[kv.Key] = v;
            }
            group.Function = func; // 整包覆蓋

            // 7) 寫回 Recipe.json 
            JsonFunction.SaveJson(jsonFilePath, root);


            MessageBox.Show($"DI 功能組「{selectedFunctionName}」已儲存。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);





            LoadFunctionGroups();

        }
         

        private void CbDOfunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearDOtextBox();

            string selectedGroup = CbDOfunction.Text;
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

            // 3) 取出 DO 區域
            if (!recipe.DioFunctions.TryGetValue("DO", out var doSection))
                return;

            // 4) 取出選定的功能組
            if (!doSection.NodeGroups.TryGetValue(selectedGroup, out var group) || group == null)
                return;

            // 5) 回填 NodeID / SlotID（如果你的下拉選單是固定選項，也可以用 SelectedItem/SelectedValue）
            CbDONodeId.Text = group.NodeID.ToString();
            CbDOSlotId.Text = group.SlotID.ToString();

            // 6) 回填 00..15 的功能字串
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
                if (group.Function != null)
                    group.Function.TryGetValue(kv.Key, out val);
                kv.Value.Text = val ?? string.Empty; // 沒存過就留空
            }
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
    }
}
