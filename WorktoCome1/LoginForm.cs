using System;
using System.Collections.Generic;
using System.Data;
using System.IO; 
using System.Text.Json;
using System.Windows.Forms;

namespace WorktoCome1
{
    public partial class LoginForm : Form
    {
        private string jsonFilePath = AppPaths.UsersPath;


        private Users allUsers;
        public string Account;
        public string Level;
        public int IsManageMode { get; set; } = 0;
        #region User的JSON定義
        public class UserInfo
        {
            public string Level { get; set; }
            public string Password { get; set; }
        } 
        public class Users
        { 
            public Dictionary<string, UserInfo> Data { get; set; } = new Dictionary<string, UserInfo>();
        }
        #endregion

        public LoginForm()
        {
            InitializeComponent();
            LoadUsersData();
            
        }


        private void LoadUsersData()
        {
            if (File.Exists(jsonFilePath))
            {
                string jsonString = JsonFunction.LoadJson(jsonFilePath);
                try
                {
                    // 將 JSON 字串反序列化成 Users 物件
                    allUsers = JsonSerializer.Deserialize<Users>(jsonString);

                    // 處理檔案內容為空的情況
                    if (allUsers == null)
                    {
                        allUsers = new Users();
                        MessageBox.Show("使用者資料檔內容為空，已建立新的資料結構。", "提示");
                    }
                    // 檔案存在且成功讀取
                    if (allUsers != null)
                    {
                        // 建立一個新的 DataTable
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Account", typeof(string));
                        dt.Columns.Add("Level", typeof(string));

                        // 迴圈遍歷 allUsers.Data 字典
                        foreach (var userEntry in allUsers.Data)
                        {
                            // userEntry.Key 是帳號 (Account)
                            // userEntry.Value 是 UserInfo 物件
                            dt.Rows.Add(userEntry.Key, userEntry.Value.Level);
                        }

                        // 將 DataTable 設為 DataGridView 的資料來源
                        DgAccount.DataSource = dt;
                    }

                }
                catch (JsonException ex)
                {
                    MessageBox.Show($"讀取 JSON 檔案時發生錯誤：{ex.Message}", "錯誤");
                    // 發生錯誤時，建立空的 Users 物件，防止程式崩潰
                    allUsers = new Users();
                    
                }
            }
            else
            {
                // 如果檔案不存在，則建立一個新的 Users 物件並加入預設使用者
                allUsers = new Users();
                allUsers.Data.Add("admin", new UserInfo { Level = "管理員", Password = "admin123" });
                allUsers.Data.Add("test", new UserInfo { Level = "工程師", Password = "test123" });

                // 序列化並儲存這個預設的 Users 物件到檔案
                SaveUsersData();

                MessageBox.Show("找不到使用者資料檔，已自動建立預設檔案。", "提示");

                LoadUsersData();
            }
        }





        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 這裡寫入驗證邏輯 
            string inputAccount = tbLoginAccount.Text.Trim(); // 取得使用者輸入的帳號
            string inputPassword = tbLoginPassword.Text.Trim(); // 取得使用者輸入的密碼

            // 檢查使用者輸入是否為空
            if (string.IsNullOrEmpty(inputAccount) || string.IsNullOrEmpty(inputPassword))
            {
                MessageBox.Show("帳號或密碼不能為空。", "錯誤");
                return;
            }


            if (allUsers.Data.ContainsKey(inputAccount))
            {
                // 取得對應的使用者資訊
                UserInfo userInfo = allUsers.Data[inputAccount];

                // 比對密碼
                if (userInfo.Password == inputPassword)
                {
                    // 登入成功
                    MessageBox.Show($"登入成功！您的等級是：{userInfo.Level}", "登入成功");
                    this.Account = inputAccount;
                    this.Level = userInfo.Level;
                    // 在這裡可以執行登入後的動作，例如開啟主視窗
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // 密碼錯誤
                    MessageBox.Show("密碼錯誤。", "登入失敗");
                }
            }
            else
            {
                // 帳號不存在
                MessageBox.Show("帳號不存在。", "登入失敗");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // 關閉登入視窗
        }

        //新增使用者JSON按鈕
        private void btnCreaterUser_Click(object sender, EventArgs e)
        {
            if (tbCheckPassword.Text != tbPassword.Text)
            {
                MessageBox.Show("密碼與確認密碼不相同", "錯誤");
                return;
            } 

            if (string.IsNullOrEmpty(tbAccount.Text.Trim()) || string.IsNullOrEmpty(tbPassword.Text.Trim()) || string.IsNullOrEmpty(tbLevel.Text.Trim()))
            {
                MessageBox.Show("帳號、密碼或等級不能為空。", "錯誤");
                return;
            }

            // 2. 檢查帳號是否已經存在
            if (allUsers.Data.ContainsKey(tbAccount.Text.Trim()))
            {
                MessageBox.Show("此帳號已存在，請使用其他帳號。", "新增失敗");
                return;
            }
            UserInfo newUser;

            switch (tbLevel.Value)
            {
                case 1:
                    newUser = new UserInfo
                    {
                        Level = "管理員",
                        Password = tbPassword.Text.Trim()
                    };
                    break;
                case 2:
                    newUser = new UserInfo
                    {
                        Level = "工程師",
                        Password = tbPassword.Text.Trim()
                    };
                    break;
                default:
                    newUser = new UserInfo
                    {
                        Level = "一般人員",
                        Password = tbPassword.Text.Trim()
                    };
                    break;
            }
            // 3. 建立一個新的 UserInfo 物件
            

            // 4. 將新使用者加入到字典中
            allUsers.Data.Add(tbAccount.Text.Trim(), newUser);

            // 5. 將更新後的資料寫回檔案
            SaveUsersData();

            MessageBox.Show($"使用者 {tbAccount.Text.Trim()} 已成功新增！", "新增成功");
            LoadUsersData();
            // 清空輸入框
            tbAccount.Clear();
            tbPassword.Clear();
            tbLevel.Value =  1;
            tbCheckPassword.Clear();
        }

        private void SaveUsersData()
        {
            try
            {
                AppPaths.EnsureDataDir(); 
                JsonFunction.SaveJson(jsonFilePath, allUsers);
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"寫入 JSON 檔案時發生錯誤：{ex.Message}", "錯誤");
            }
        }

        private void btnDeletUser_Click(object sender, EventArgs e)
        {
            string accountToDelete = tbAccount.Text.Trim();

            if (string.IsNullOrEmpty(accountToDelete))
            {
                MessageBox.Show("請輸入要刪除的帳號。", "錯誤");
                return;
            }

            if (!allUsers.Data.ContainsKey(accountToDelete))
            {
                MessageBox.Show("找不到此帳號。", "錯誤");
                return;
            }

            if (accountToDelete == "admin")
            {
                MessageBox.Show("管理員帳號不可刪除。", "錯誤");
                return;
            }

            var result = MessageBox.Show($"確定要刪除帳號 {accountToDelete} 嗎？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                allUsers.Data.Remove(accountToDelete);
                SaveUsersData();
                LoadUsersData();
                MessageBox.Show($"帳號 {accountToDelete} 已刪除。", "成功");
                tbAccount.Clear();
                tbPassword.Clear();
                tbLevel.Value = 1;
                tbCheckPassword.Clear();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            switch(IsManageMode)
            { 
                case 1:
                    // 鎖住登出功能
                    btnLogin.Enabled = false;
                    btnCancel.Enabled = false;
                    // 啟用新增、刪除
                    btnCreaterUser.Enabled = true;
                    btnDeleteUser.Enabled = true;
                    break;
                case 2:
                    // 鎖住登出功能
                    btnLogin.Enabled = false;
                    btnCancel.Enabled = false;
                    // 啟用新增、刪除
                    btnCreaterUser.Enabled = false;
                    btnDeleteUser.Enabled = false;
                    break;
                default:
                    btnLogin.Enabled = true;
                    btnCancel.Enabled = true;
                    btnCreaterUser.Enabled = false;
                    btnDeleteUser.Enabled = false;
                    break;
            } 
             
        }

        private void DgAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 取得點擊的列 (Row)
                DataGridViewRow selectedRow = DgAccount.Rows[e.RowIndex];

                // 取得該列「Account」欄位的資料，並轉換成字串
                string accountName = selectedRow.Cells["Account"].Value.ToString();

                // 將取得的帳號名稱帶入到 txtAccount 文字方塊
                tbLoginAccount.Text = accountName;
            }
        }
    }
}
