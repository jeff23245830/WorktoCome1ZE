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
    public partial class ProductNameForm : Form
    {
        public string NewProductName { get; private set; }
        public string OldProductName;
        public ProductNameForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNewProductName.Text))
            {
                MessageBox.Show("新產品名稱不可為空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 將文字框內容賦予屬性
            this.NewProductName = tbNewProductName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
