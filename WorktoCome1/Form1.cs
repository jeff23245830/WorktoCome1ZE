using EtherCAT_DLL;
using EtherCAT_DLL_Err;
using EtherCATFunction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WorktoCome1
{

    public partial class Form1 : Form
    {
        private EtherCATFunction.Initial cardManager;
        bool g_bInitialFlag = false;
        ushort g_uRet = 0;
        ushort g_nESCExistCards = 0, g_uESCCardNo = 0, g_uESCNodeID = 0, g_uESCSlotID;
        ushort[] g_uESCCardNoList = new ushort[32];
        CheckBox[] g_pOutputLab = new CheckBox[16];
        private List<ushort> slaveNodeIdList = new List<ushort>();
        private List<ushort> slaveSlotIdList = new List<ushort>();
        public Form1()
        {
            InitializeComponent();
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
            UcProgram ucProgram = new UcProgram();
            loadUserControl(ucProgram);
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            UcMain ucMain = new UcMain();
            loadUserControl(ucMain);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            UcSetting ucSetting = new UcSetting();  
            loadUserControl(ucSetting);
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            UcControl ucControl = new UcControl();
            ucControl.SetNodeIDList(slaveNodeIdList);
            ucControl.SetSlotIDList(slaveSlotIdList);
            ucControl.Set_nESCExistCards = cardManager.CardCount  ;

            loadUserControl(ucControl);

            

        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            UcInfo ucInfo = new UcInfo();    
            loadUserControl(ucInfo);
        }

        private void btnLanguage_Click(object sender, EventArgs e)
        {
            UcLanguage ucLanguage = new UcLanguage();
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

            //確認東西歸位
            //if
            //正確關閉
            this.Close(); 
            //else
            //跳警告
        }

        private void btnInitial_Click(object sender, EventArgs e)
        {
            //Initial_Card();
            //EtherCATFunction.Initial cardManager = new EtherCATFunction.Initial();
            cardManager = new EtherCATFunction.Initial();

            bool success = cardManager.Initial_Card();

            g_nESCExistCards = cardManager.CardCount;
            
            


            //ucMain1.CardCount = g_nESCExistCards;
            // 根據結果來顯示訊息
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
            //ucMain1.CardCount = g_nESCExistCards;
            // 根據結果來顯示訊息
            if (FindSlave_success)
            {
                g_uESCNodeID = cardManager.g_ESCNodeID_u;
                g_uESCSlotID = cardManager.g_ESCSlotID_u;
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

         
    }
}
