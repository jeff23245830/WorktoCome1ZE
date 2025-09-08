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
        private readonly AppState _appState;
        string filePath = AppPaths.RecipePath;
        public UcProgram(AppState appState)
        {
            InitializeComponent();
            LoadProductList();
            _appState = appState;
        }
        #region 非事件方法
        public void LoadDefaultRecipe()
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show(
                    "找不到 Recipe.json。\n請到「程式」頁面建立一個新的 Recipe 檔。",
                    "找不到檔案",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {


                //先打讀JSOMN檔案
                string jsonString = JsonFunction.LoadJson(filePath);

                // 3) 檔案內容被清空（空字串/只有空白）→ 自訂訊息，不崩潰
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    MessageBox.Show(
                        "Recipe.json 目前是空的。\n請到「程式 → 載入」頁面重新選擇並儲存一個配方。",
                        "沒有內容",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4) 解析 JSON（如果格式壞掉，會丟 JsonException）
                RootObject rootObject;
                try
                {
                    rootObject = JsonSerializer.Deserialize<RootObject>(jsonString);
                }
                catch (JsonException)
                {
                    MessageBox.Show(
                        "Recipe.json 內容格式不正確。\n請到「程式 → 載入」頁面重新產生或覆蓋此檔。",
                        "格式錯誤",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (rootObject == null)
                {
                    MessageBox.Show("讀到的資料為空，請重新建立 Recipe。", "資料錯誤",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //5) 取得 Default_Recipe 並檢查是否存在於 Products
                string productName = rootObject.Default_Recipe;
                if (string.IsNullOrWhiteSpace(productName))
                {
                    MessageBox.Show("尚未設定預設配方（Default_Recipe）。請先在「程式 → 載入」選一個配方。", "未設定預設",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 4) 檢查是否存在於 Products
                if (rootObject.Products == null || !rootObject.Products.ContainsKey(productName))
                {
                    MessageBox.Show($"找不到產品 '{productName}' 的資料。請用「載入」重新選擇。", "資料缺漏",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 5) 套用到狀態 + UI
                _appState.RootObject = rootObject;
                _appState.CurrentProducTitle = productName;
                _appState.CurrentRecipe = rootObject.Products[productName];
                txtCurrentProduct.Text = productName;

                MessageBox.Show($"已載入預設配方：{productName}", "完成",
           MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取失敗：" + ex.Message, "錯誤",
           MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }


        public event EventHandler RequestCreateRecipeJson;
        private void GoCreateRecipeJson()
        {
            RequestCreateRecipeJson?.Invoke(this, EventArgs.Empty);
        }

        #endregion
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
                        if (File.Exists(filePath))
                        {
                            string jsonString = File.ReadAllText(filePath);

                            // ✅ 檔案存在但內容被清空 → 當作新檔處理
                            if (string.IsNullOrWhiteSpace(jsonString))
                            {
                                rootObject = new RootObject { Products = new Dictionary<string, Recipe>() };
                            }
                            else
                            {
                                try
                                {
                                    rootObject = JsonSerializer.Deserialize<RootObject>(jsonString);
                                    // 反序列化成功但 Products 可能是 null，補一個空字典
                                    if (rootObject.Products == null)
                                        rootObject.Products = new Dictionary<string, Recipe>();
                                }
                                catch (JsonException)
                                {
                                    // ✅ JSON 壞掉（不是空）→ 問是否用新內容覆蓋
                                    var ans = MessageBox.Show(
                                        "Recipe.json 格式錯誤，是否以全新內容覆蓋？",
                                        "格式錯誤", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                    if (ans == DialogResult.Yes)
                                        rootObject = new RootObject { Products = new Dictionary<string, Recipe>() };
                                    else
                                        return; // 使用者拒絕覆蓋 → 中止建立
                                }
                            }
                        }
                        else
                        {
                            // 檔案不存在 → 新建
                            rootObject = new RootObject { Products = new Dictionary<string, Recipe>() };
                        }

                        // ★(1) 反序列化後先保險：Products 不可為 null
                        if (rootObject.Products == null)
                            rootObject.Products = new Dictionary<string, Recipe>();



                        if (rootObject.Products.ContainsKey(productName))
                        {
                            // 如果存在，提醒使用者
                            MessageBox.Show($"產品 '{productName}' 已存在。請使用其他名稱。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // 終止後續的程式碼
                        }

                        // 建立新的食譜 (Recipe) 物件
                        // 根據您的結構，需要初始化 Motions 和 DioFunctions 字典
                        var newRecipe = new Recipe
                        {
                            Motions = new Dictionary<string, Motion>(),
                            DioFunctions = new Dictionary<string, Dio>()
                        };

                        // 將新的產品名稱與食譜物件加入字典
                        rootObject.Products.Add(productName, newRecipe);

                        if (string.IsNullOrWhiteSpace(rootObject.Default_Recipe))
                            rootObject.Default_Recipe = productName;

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
               _appState.SelectedProductTitle = selectedProductName;
            }
            else
            {
                // 如果沒有選取項目，則清空文字框
                txtSelectedProduct.Text = string.Empty;
                _appState.SelectedProductTitle = null;
            }
        }
        
        private void btnLoadParameters_Click(object sender, EventArgs e)
        {
            string SelectedProduct = _appState.SelectedProductTitle;
            if (string.IsNullOrWhiteSpace(SelectedProduct))
            {
                MessageBox.Show("請先選擇一個產品以載入參數。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtCurrentProduct.Text = txtSelectedProduct.Text;

            _appState.CurrentProducTitle = txtCurrentProduct.Text;


            //讀出JSON檔案
            string jsonString = JsonFunction.LoadJson(filePath);
            var rootObject = JsonSerializer.Deserialize<RootObject>(jsonString);

            _appState.RootObject = rootObject;


            string productName = _appState.CurrentProducTitle;
            if (_appState.RootObject.Products.ContainsKey(productName))
            {
                _appState.CurrentRecipe = _appState.RootObject.Products[productName];

                //存進DEFAULT RECIPE
                _appState.RootObject.Default_Recipe = _appState.CurrentProducTitle;
                JsonFunction.SaveJson(filePath, _appState.RootObject);
            }
            else
            {
                MessageBox.Show($"找不到產品 '{productName}' 的資料。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show($"產品 {txtCurrentProduct.Text} 的參數已載入。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void btnRecipeRename_Click(object sender, EventArgs e)
        {
            // 步驟 1: 檢查是否有選定產品
            string oldProductName = _appState.SelectedProductTitle;
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
                        _appState.SelectedProductTitle = newProductName;
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
            string productToDelete = _appState.SelectedProductTitle;
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
                    _appState.SelectedProductTitle = null; // 清空選定的產品
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
