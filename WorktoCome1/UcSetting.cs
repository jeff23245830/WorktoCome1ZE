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
    public partial class UcSetting : UserControl
    {
        public UcSetting()
        {
            InitializeComponent();
        }

        public void LoadRecipe()
        {
            //1.載入存在APPSTATE的CurrentRecipe
            string CurrentProduc = AppState.CurrentProducTitle;
            if (string.IsNullOrWhiteSpace(CurrentProduc))
            {
                MessageBox.Show("請先選擇一個產品以載入參數。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (AppState.CurrentRecipe.Motions != null)
            {
                CbArea.Items.Clear();   
                foreach (var recipe in AppState.CurrentRecipe.Motions.Keys)
                {
                    CbArea.Items.Add(recipe);
                }
            }
            else
            {
                CbArea.Items.Clear();
            }
            

            //2.CbArea載入CurrentRecipe的Motions

            //3.DgMotionPoint載入選定的Motion的Grops

            //4.如果都是空的就不載入，由使用者新增並按下btnMotionSave

        }


        private void btnMotionSave_Click(object sender, EventArgs e)
        {
            //1.儲存使用者在DgMotionPoint新增的Groups
        }

        private void CbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            //1.載入選定的Motion的Grops
        }
    }
}
