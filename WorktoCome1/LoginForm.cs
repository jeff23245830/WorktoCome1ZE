using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO; 
using System.Windows.Forms;

namespace WorktoCome1
{
    public partial class LoginForm : Form
    {
        private string jsonFilePath = "users.json";
        private Users allUsers;

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
                string jsonString = File.ReadAllText(jsonFilePath);
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
                allUsers.Data.Add("admin", new UserInfo { Level = "1", Password = "admin123" });
                allUsers.Data.Add("test", new UserInfo { Level = "2", Password = "test123" });

                // 序列化並儲存這個預設的 Users 物件到檔案
                SaveUsersData();

                MessageBox.Show("找不到使用者資料檔，已自動建立預設檔案。", "提示");
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
            if (tbCheckPassword != tbPassword)
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

            // 3. 建立一個新的 UserInfo 物件
            UserInfo newUser = new UserInfo
            {
                Level = tbLevel.Text.Trim(),
                Password = tbPassword.Text.Trim()
            };

            // 4. 將新使用者加入到字典中
            allUsers.Data.Add(tbAccount.Text.Trim(), newUser);

            // 5. 將更新後的資料寫回檔案
            SaveUsersData();

            MessageBox.Show($"使用者 {tbAccount.Text.Trim()} 已成功新增！", "新增成功");

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
                var options = new JsonSerializerOptions { WriteIndented = true };

                // 使用 JsonSerializer.Serialize 將 Users 物件轉換為 JSON 字串
                string jsonString = JsonSerializer.Serialize(allUsers, options);

                // 寫入檔案
                File.WriteAllText(jsonFilePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"寫入 JSON 檔案時發生錯誤：{ex.Message}", "錯誤");
            }
        }
    }
}
