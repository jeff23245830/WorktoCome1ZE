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
using EtherCAT_DLL;
using EtherCAT_DLL_Err;

namespace WorktoCome1
{

    public partial class Form1 : Form
    {

        bool g_bInitialFlag = false;
        ushort g_uRet = 0;
        ushort g_nESCExistCards = 0, g_uESCCardNo = 0, g_uESCNodeID = 0, g_uESCSlotID;
        ushort[] g_uESCCardNoList = new ushort[32];
        CheckBox[] g_pOutputLab = new CheckBox[16];
        public Form1()
        {
            InitializeComponent();
            TimCheckStatus.Interval = 100;
            TimCheckStatus.Enabled = true;
            g_pOutputLab[0] = ChkBit00;
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
            EtherCATFunction.Initial cardManager = new EtherCATFunction.Initial();

            bool success = cardManager.Initial_Card();



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



        }

        private void ChkBit00_CheckedChanged(object sender, EventArgs e)
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
            uOutputStatus = (ushort)nStat;

            if (g_nESCExistCards > 0)
            {
                g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Set_Output_Value(g_uESCCardNo, g_uESCNodeID, g_uESCSlotID, uOutputStatus);

                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    //AddErrMsg("_ECAT_Slave_DIO_Set_Output, ErrorCode = " + g_uRet.ToString(), true);
                }
            }
        }
    }
}
