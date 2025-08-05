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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 這裡寫入驗證邏輯
            string username = tbLoginAccount.Text;
            string password = tbLoginPassword.Text;

            // 假設使用者名稱是 "admin"，密碼是 "12345"
            if (username == "admin" && password == "12345")
            {
                // 驗證成功，設定 DialogResult 為 OK
                this.DialogResult = DialogResult.OK;
                this.Close(); // 關閉登入視窗
            }
            else
            {
                MessageBox.Show("使用者名稱或密碼錯誤！", "登入失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // 關閉登入視窗
        }
    }
}
