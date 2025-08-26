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
    public partial class InputProductNameForm : Form
    {
        public string ProductName { get; private set; }
        public InputProductNameForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbProductName.Text))
            {
                MessageBox.Show("產品名稱不可為空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.ProductName = tbProductName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
