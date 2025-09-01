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
    public partial class TransferForm : Form
    {
        public string SelectedDestinationMotion { get; private set; }
        public TransferForm(List<string> motionNames)
        {
            InitializeComponent();
            cbDestinationMotion.Items.AddRange(motionNames.ToArray());
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cbDestinationMotion.SelectedItem == null)
            {
                MessageBox.Show("請選擇一個目的地區域。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None; // 阻止視窗關閉
            }
            else
            {
                SelectedDestinationMotion = cbDestinationMotion.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
