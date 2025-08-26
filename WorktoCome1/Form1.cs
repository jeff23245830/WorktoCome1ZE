using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WorktoCome1
{

    public partial class Form1 : Form
    {
        private EtherCATFunction.Initial cardManager;
        private string _loginAccount;
        private string _loginLevel;
        //private ushort g_nESCExistCards = 0, g_uESCCardNo = 0, g_uESCNodeID = 0, g_uESCSlotID;
        private List<ushort> slaveNodeIdList = new List<ushort>();
        private List<ushort> slaveSlotIdList = new List<ushort>();

        UcProgram ucProgram = new UcProgram();
        UcMain ucMain = new UcMain();
        UcSetting ucSetting = new UcSetting();
        UcControl ucControl = new UcControl();
        UcInfo ucInfo = new UcInfo();
        UcLanguage ucLanguage = new UcLanguage();
        
        public Form1(string loginAccount,string loginLevel)
        {
            InitializeComponent();
            _loginAccount = loginAccount;
            _loginLevel = loginLevel;
            TimCheckStatus.Interval = 100;
            TimCheckStatus.Enabled = true;
            btnControl.Enabled = false; 
        }
        private void loadUserControl(UserControl userControl)
        {
            pnlContent.Controls.Clear();  // 清空 pnlContent 中的所有控制項
            userControl.Dock = DockStyle.Fill; // 讓 UserControl 填滿容器
            pnlContent.Controls.Add(userControl); // 將 UserControl 加入容器
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnProgram_Click(object sender, EventArgs e)
        {
            loadUserControl(ucProgram);
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            
            loadUserControl(ucMain);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            loadUserControl(ucSetting);
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            ucControl.SetNodeIDtoCombobox(slaveNodeIdList);
            ucControl.SetSlotIDtoCombobox(slaveSlotIdList);
            ucControl.nESCExistCards = cardManager.CardCount;
            loadUserControl(ucControl);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            loadUserControl(ucInfo);
        }

        private void btnLanguage_Click(object sender, EventArgs e)
        {
            loadUserControl(ucLanguage);
        }

        private void BtnFindSlave_Click(object sender, EventArgs e)
        {

        }

        private void cmbSlaves_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Close(); 
        }

        private void btnInitial_Click(object sender, EventArgs e)
        {
            cardManager = new EtherCATFunction.Initial();//要注意這問題

            bool success = cardManager.Initial_Card();

            //g_nESCExistCards = cardManager.CardCount;
            
            if (success)
            {
                // AddErrMsg("所有卡片已成功初始化！", false);
                txtResult.Text = "所有卡片已成功初始化！";
            }
            else
            {
                // AddErrMsg("初始化失敗，請檢查卡片連線。", true);
                txtResult.Text = "初始化失敗，請檢查卡片連線。";
            }

            bool FindSlave_success = cardManager.Card_FindSlave();
            if (FindSlave_success)
            {
                //g_uESCNodeID = cardManager.g_ESCNodeID_u;
                //g_uESCSlotID = cardManager.g_ESCSlotID_u;
                btnControl.Enabled = true;
                tbError.Text = "所有卡片已成功FindSlave！";
            }
            else
            {
                // AddErrMsg("初始化失敗，請檢查卡片連線。", true);
                tbError.Text = "FindSlave失敗，請檢查卡片連線。";
            }
             
            foreach (var slave in cardManager.FoundSlaves)
            {
                slaveNodeIdList.Add(slave.NodeID);
                slaveSlotIdList.Add(slave.SlotID);
            }
             
        }

        private void Bt_Logout_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;

            // 關閉目前的主視窗，程式控制權會回到 Program.cs 的迴圈
            this.Close();
        }

        private void btnUserInfo_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm();
            if(_loginLevel == "管理員")
                loginForm.IsManageMode = 1;
            else
                loginForm.IsManageMode = 2;
            loginForm.ShowDialog();
        }
    }
}
