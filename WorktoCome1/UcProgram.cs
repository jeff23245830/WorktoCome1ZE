using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml;



namespace WorktoCome1
{
    public partial class UcProgram : UserControl
    {
        string filePath = "Recipe.json";
        public UcProgram()
        {
            InitializeComponent();
            LoadProductList();

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        public void LoadProductList()
        {
            //string filePath = "recipes.json";

            // 清空現有的清單
            LbProducts.Items.Clear();

            if (File.Exists(filePath))
            {
                try
                {
                    string jsonString = JsonFunction.LoadJson(filePath);

                    //// 確保您的 RootObject 類別與之前的定義一致
                    var rootObject = JsonSerializer.Deserialize<RootObject>(jsonString);

                    //RootObject rootObject = JsonFunction.LoadJson(filePath);


                    // 如果檔案中有產品資料
                    if (rootObject != null && rootObject.Products != null)
                    {
                        // 遍歷字典中的每個產品名稱，並加入到 ListBox
                        foreach (var productName in rootObject.Products.Keys)
                        {
                            LbProducts.Items.Add(productName);
                        }
                    }
                }
                catch (JsonException ex)
                {
                    // 處理 JSON 格式錯誤
                    MessageBox.Show($"讀取 JSON 檔案時發生格式錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // 處理其他檔案讀取錯誤
                    MessageBox.Show($"讀取檔案時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btCreateRecipe_Click(object sender, EventArgs e)
        {
            using (var inputForm = new InputProductNameForm())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string productName = inputForm.ProductName; 

                    RootObject rootObject;

                    try
                    {
                        // 檢查檔案是否存在
                        if (File.Exists(filePath))
                        {
                            // 檔案存在，讀取並反序列化
                            string jsonString = File.ReadAllText(filePath);
                            rootObject = JsonSerializer.Deserialize<RootObject>(jsonString);
                        }
                        else
                        {
                            // 檔案不存在，建立新的物件
                            rootObject = new RootObject { Products = new Dictionary<string, Recipe>() };
                        }

                        // 建立新的食譜 (Recipe) 物件
                        // 這裡您可以根據需要填入預設的資料
                        var newRecipe = new Recipe();

                        // 將新的產品名稱與食譜物件加入字典
                        // 如果字典已包含該產品名稱，會被覆蓋
                        rootObject.Products[productName] = newRecipe;

                        // 設定 JSON 序列化選項，讓輸出更美觀
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true // 讓 JSON 格式化，方便閱讀
                        };

                        // 將更新後的物件序列化為 JSON 字串
                        string updatedJsonString = JsonSerializer.Serialize(rootObject, options);

                        // 將 JSON 字串寫入檔案
                        File.WriteAllText(filePath, updatedJsonString);

                        MessageBox.Show($"已成功新增產品：{productName}，並寫入 {filePath}");

                        LoadProductList();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"新增產品時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // 若取消則不做事
            }
        }

        private void LbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 檢查是否有選取的項目
            if (LbProducts.SelectedItem != null)
            {
                // 取得選取的產品名稱
                string selectedProductName = LbProducts.SelectedItem.ToString();

                // 將選取的產品名稱顯示到「產品選擇」文字框中
                txtSelectedProduct.Text = selectedProductName;

                // 同時更新全域變數，供其他地方使用
                AppState.SelectedProduct = selectedProductName;
            }
            else
            {
                // 如果沒有選取項目，則清空文字框
                txtSelectedProduct.Text = string.Empty;
                AppState.SelectedProduct = null;
            }
        }

        private void btnLoadParameters_Click(object sender, EventArgs e)
        {
            // 檢查「產品選擇」文字框是否有內容
            if (string.IsNullOrWhiteSpace(txtSelectedProduct.Text))
            {
                MessageBox.Show("請先選擇一個產品以載入參數。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 將「產品選擇」文字框的值，賦予「目前產品」文字框
            txtCurrentProduct.Text = txtSelectedProduct.Text;

            // 同時更新全域變數，這一步非常重要，
            // 因為這表示這個產品現在是整個軟體的「目前產品」
            AppState.CurrentProduct = txtCurrentProduct.Text;

            MessageBox.Show($"產品 {txtCurrentProduct.Text} 的參數已載入。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnRecipeRename_Click(object sender, EventArgs e)
        {
            // 步驟 1: 檢查是否有選定產品
            string oldProductName = AppState.SelectedProduct;
            if (string.IsNullOrEmpty(oldProductName))
            {
                MessageBox.Show("請先選擇一個產品以重新命名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 步驟 2: 顯示重新命名視窗
            using (var renameForm = new ProductNameForm())
            {
                // 預設將舊名稱填入文字框，方便使用者編輯
                renameForm.OldProductName = oldProductName;

                if (renameForm.ShowDialog() == DialogResult.OK)
                {
                    string newProductName = renameForm.NewProductName;

                    // 檢查新名稱是否與舊名稱相同
                    if (oldProductName.Equals(newProductName, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("新舊名稱相同，無需更動。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 步驟 3: 處理後端 JSON 檔案
                    try
                    {
                        //string filePath = "recipes.json";
                        if (!File.Exists(filePath))
                        {
                            MessageBox.Show("JSON 檔案不存在。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 讀取 JSON 檔案內容並反序列化
                        string jsonString = File.ReadAllText(filePath);
                        var rootObject = JsonSerializer.Deserialize<RootObject>(jsonString);

                        // 檢查新名稱是否已存在
                        if (rootObject.Products.ContainsKey(newProductName))
                        {
                            MessageBox.Show("新產品名稱已存在，請選擇其他名稱。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // 檢查舊名稱是否存在，以防萬一
                        if (!rootObject.Products.ContainsKey(oldProductName))
                        {
                            MessageBox.Show("找不到舊的產品名稱，無法重新命名。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 將舊名稱的資料複製到新名稱
                        var productData = rootObject.Products[oldProductName];
                        rootObject.Products.Add(newProductName, productData);

                        // 移除舊名稱的鍵值對
                        rootObject.Products.Remove(oldProductName);

                        // 將更新後的物件序列化並寫回檔案
                        var options = new JsonSerializerOptions { WriteIndented = true };
                        string updatedJsonString = JsonSerializer.Serialize(rootObject, options);
                        File.WriteAllText(filePath, updatedJsonString);

                        MessageBox.Show($"產品名稱已從 {oldProductName} 重新命名為 {newProductName}。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 步驟 4: 更新介面
                        // 假設您有一個 LoadProductList() 方法來更新清單
                        LoadProductList();
                        // 順便更新 AppState 中的選定產品名稱
                        AppState.SelectedProduct = newProductName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"重新命名產品時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDeleteRecipe_Click(object sender, EventArgs e)
        {
            // 步驟 1: 檢查是否有選定的產品
            string productToDelete = AppState.SelectedProduct;
            if (string.IsNullOrEmpty(productToDelete))
            {
                MessageBox.Show("請先選擇一個產品以刪除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 步驟 2: 顯示確認對話框
            DialogResult result = MessageBox.Show(
                $"您確定要永久刪除產品：{productToDelete} 嗎？此操作無法復原。",
                "確認刪除",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // 步驟 3: 處理後端 JSON 檔案
                try
                {
                    //string filePath = "recipes.json";
                    if (!File.Exists(filePath))
                    {
                        MessageBox.Show("JSON 檔案不存在。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 讀取 JSON 檔案內容並反序列化
                    string jsonString = File.ReadAllText(filePath);
                    var rootObject = JsonSerializer.Deserialize<RootObject>(jsonString);

                    // 檢查要刪除的產品是否存在
                    if (!rootObject.Products.ContainsKey(productToDelete))
                    {
                        MessageBox.Show("找不到要刪除的產品。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 從字典中移除該產品
                    rootObject.Products.Remove(productToDelete);

                    // 將更新後的物件序列化並寫回檔案
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string updatedJsonString = JsonSerializer.Serialize(rootObject, options);
                    File.WriteAllText(filePath, updatedJsonString);

                    MessageBox.Show($"產品 {productToDelete} 已成功刪除。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 步驟 4: 更新介面與軟體狀態
                    LoadProductList(); // 重新整理產品清單
                    AppState.SelectedProduct = null; // 清空選定的產品
                                                     // 假設您還有「目前產品」的文字框，也一併清空
                                                     // txtCurrentProduct.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"刪除產品時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
