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




namespace WorktoCome1
{

    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            TimCheckStatus.Interval = 100;
            TimCheckStatus.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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
           






        }
    }
}
