using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorktoCome1
{
    public partial class UcSetting : UserControl
    {
        private readonly AppState _appState;
        string filePath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
    "WorktoCome1",                 // 建議放一個你的資料夾，不要直接丟在根目錄
    "Recipe.json"
);
        public UcSetting(AppState appState  )
        {
            InitializeComponent();
            ClearData();
            _appState = appState;
        }
        public void ClearData()
        {
            DgMotionPoint.Rows.Clear();
            CbArea.Items.Clear();
            CbArea.Text = string.Empty;
        }
        public void LoadRecipe()
        {
            
            //1.載入存在APPSTATE的CurrentRecipe
            string CurrentProduc = _appState.CurrentProductTitle;
            if (string.IsNullOrWhiteSpace(CurrentProduc))
            {
                MessageBox.Show("請先選擇一個產品以載入參數。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (_appState.CurrentRecipe.Motions != null)
            {
                //2.CbArea載入CurrentRecipe的Motions
                CbArea.Items.Clear();   
                foreach (var recipe in _appState.CurrentRecipe.Motions.Keys)
                {
                    CbArea.Items.Add(recipe);
                }
            }
            else
            {
                CbArea.Items.Clear();
            }
        }


        private void btnMotionSave_Click(object sender, EventArgs e)
        {
            //1.先確認
            string currentProducTitle = _appState.CurrentProductTitle;
            string selectedMotionName = CbArea.Text;  

            if (string.IsNullOrWhiteSpace(currentProducTitle) || string.IsNullOrWhiteSpace(selectedMotionName))
            {
                MessageBox.Show("請先載入產品並輸入或選擇一個區域別。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                // 2. 確保 AppState.RootObject 已經載入
                if (_appState.RootObject == null)
                {
                    MessageBox.Show("資料尚未載入，請先載入產品。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3. 取得目前產品的 Recipe 物件
                if (!_appState.RootObject.Products.TryGetValue(currentProducTitle, out Recipe currentRecipe))
                {
                    MessageBox.Show($"找不到產品 '{currentProducTitle}' 的資料。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4. 取得或建立新的 Motion 物件
                if (!currentRecipe.Motions.TryGetValue(selectedMotionName, out Motion currentMotion))
                {
                    // 如果 Motion 不存在，就建立一個新的
                    currentMotion = new Motion();
                    currentRecipe.Motions.Add(selectedMotionName, currentMotion);
                }

                // 5. 遍歷 DataGridView，讀取每一行的點位資料並更新 Motion
                currentMotion.Groups.Point.Clear(); // 先清空舊資料，再重新寫入，確保同步

                foreach (DataGridViewRow row in DgMotionPoint.Rows)
                {
                    // 檢查是否為新行，並確保點位名稱不為空
                    if (!row.IsNewRow && row.Cells["點位名稱"].Value != null)
                    {
                        string pointName = row.Cells["點位名稱"].Value.ToString();

                        // 讀取 XY Z R 座標
                        double x = Convert.ToDouble(row.Cells["X"].Value);
                        double y = Convert.ToDouble(row.Cells["Y"].Value);
                        double z = Convert.ToDouble(row.Cells["Z"].Value);
                        double r = Convert.ToDouble(row.Cells["R"].Value);

                        // 建立新的 Group 和 Point 物件
                        var newGroup = new Group();
                        var newPoint = new Point();

                        newPoint.X = x;
                        newPoint.Y = y;
                        newPoint.Z = z;
                        newPoint.R = r;

                        // 將點位加入 Group 的 Point 字典中
                        newGroup.Point.Add("1", newPoint);

                        // 將 Group 加入 Motion 的 Groups 字典中
                        currentMotion.Groups.Point.Add(pointName, newPoint);
                    }
                }

                // 6. 將更新後的 AppState.RootObject 序列化並寫入檔案
                JsonFunction.SaveJson(filePath, _appState.RootObject);

                MessageBox.Show($"產品 '{currentProducTitle}' 的區域 '{selectedMotionName}' 已成功儲存。", "儲存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRecipe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"儲存時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void CbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            //1.DgMotionPoint載入選定的Motion的Grops
            // 1.清空 DgMotionPoint 的舊內容
            DgMotionPoint.Rows.Clear();

            // 檢查是否有選定的產品和 Motion 名稱
            //if (AppState.CurrentRecipe == null || CbArea.SelectedItem == null)
            //{
            //    return;
            //}

            // 取得選定的 Motion 名稱
            string selectedMotionName = CbArea.SelectedItem.ToString();

            // 從 AppState.CurrentRecipe 中找到選定的 Motion
            if (_appState.CurrentRecipe.Motions.TryGetValue(selectedMotionName, out Motion selectedMotion))
            {
                // 檢查 Groups 字典是否為空
                if (selectedMotion.Groups != null)
                {

                    CbX_NodeId.Text = selectedMotion.Groups.X_NodeId.ToString();
                    CbY_NodeId.Text = selectedMotion.Groups.Y_NodeId.ToString();
                    CbZ_NodeId.Text = selectedMotion.Groups.Z_NodeId.ToString();
                    CbR_NodeId.Text = selectedMotion.Groups.R_NodeId.ToString();

                   

                    // 遍歷 Groups 字典，將資料載入到 DataGridView
                    foreach (var pointEntry in selectedMotion.Groups.Point)
                    {

                        string pointName = pointEntry.Key;
                        Point pointData = pointEntry.Value;

                        // 根據您的 DataGridView 欄位，新增一列並填入資料
                        // 假設您的 DataGridView 欄位順序為：點位名稱, X, Y, Z, R
                        // 您可以根據需求自行調整

                        DgMotionPoint.Rows.Add( pointName, pointData.X, pointData.Y, pointData.Z, pointData.R);







                    }
                }
            }
        }
 

        private void CbArea_TextChanged(object sender, EventArgs e)
        {
            if (CbArea.FindStringExact(CbArea.Text) == -1)
            {
                // 如果使用者輸入了新文字，就清空 DataGridView
                // 這樣可以讓使用者可以從頭開始輸入新點位
                DgMotionPoint.Rows.Clear();
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            // 檢查是否有選定的行
            if (DgMotionPoint.SelectedRows.Count == 0)
            {
                return;
            }

            // 取得選定行的索引
            int selectedIndex = DgMotionPoint.SelectedRows[0].Index;

            // 如果是第一行，則無法上移
            if (selectedIndex > 0)
            {
                // 取得上一行的索引
                int previousIndex = selectedIndex - 1;

                // 取得選定行和上一行的資料
                DataGridViewRow selectedRow = DgMotionPoint.Rows[selectedIndex];
                DataGridViewRow previousRow = DgMotionPoint.Rows[previousIndex];

                // 移除兩行
                DgMotionPoint.Rows.Remove(selectedRow);
                DgMotionPoint.Rows.Remove(previousRow);

                // 重新插入，位置對調
                DgMotionPoint.Rows.Insert(previousIndex, selectedRow);
                DgMotionPoint.Rows.Insert(selectedIndex, previousRow);

                // 重新選取原本的行，保持使用者介面的一致性
                DgMotionPoint.Rows[previousIndex].Selected = true;
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {

            // 檢查是否有選定的行
            if (DgMotionPoint.SelectedRows.Count == 0)
            {
                return;
            }

            // 取得選定行的索引
            int selectedIndex = DgMotionPoint.SelectedRows[0].Index;

            // 如果是最後一行或新行，則無法下移
            if (selectedIndex < DgMotionPoint.Rows.Count - 2) // -2 是因為最後一行是新行
            {
                // 取得下一行的索引
                int nextIndex = selectedIndex + 1;

                // 取得選定行和下一行的資料
                DataGridViewRow selectedRow = DgMotionPoint.Rows[selectedIndex];
                DataGridViewRow nextRow = DgMotionPoint.Rows[nextIndex];

                // 移除兩行
                DgMotionPoint.Rows.Remove(selectedRow);
                DgMotionPoint.Rows.Remove(nextRow);

                // 重新插入，位置對調
                DgMotionPoint.Rows.Insert(selectedIndex, nextRow);
                DgMotionPoint.Rows.Insert(nextIndex, selectedRow);

                // 重新選取原本的行
                DgMotionPoint.Rows[nextIndex].Selected = true;
            }
        }

        private void btnMoveTop_Click(object sender, EventArgs e)
        {
            // 檢查是否有選定的行
            if (DgMotionPoint.SelectedRows.Count == 0)
            {
                return;
            }

            int selectedIndex = DgMotionPoint.SelectedRows[0].Index;

            // 如果已經在最上面，則不做任何事
            if (selectedIndex > 0)
            {
                // 取得選定行的資料
                DataGridViewRow selectedRow = DgMotionPoint.Rows[selectedIndex];

                // 移除選定的行
                DgMotionPoint.Rows.Remove(selectedRow);

                // 將其插入到最上面 (索引 0)
                DgMotionPoint.Rows.Insert(0, selectedRow);

                // 重新選取
                DgMotionPoint.Rows[0].Selected = true;
            }
        }

        private void btnMoveBottom_Click(object sender, EventArgs e)
        {
            // 檢查是否有選定的行
            if (DgMotionPoint.SelectedRows.Count == 0)
            {
                return;
            }

            int selectedIndex = DgMotionPoint.SelectedRows[0].Index;
            int totalRows = DgMotionPoint.Rows.Count;

            // 如果已經在最下面，則不做任何事 (-2 是因為最後一行是新行)
            if (selectedIndex < totalRows - 2)
            {
                // 取得選定行的資料
                DataGridViewRow selectedRow = DgMotionPoint.Rows[selectedIndex];

                // 移除選定的行
                DgMotionPoint.Rows.Remove(selectedRow);

                // 將其插入到倒數第二行 (總行數 - 2)
                DgMotionPoint.Rows.Insert(totalRows - 2, selectedRow);

                // 重新選取
                DgMotionPoint.Rows[totalRows - 2].Selected = true;
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            // 1. 檢查是否有選定的點位
            if (DgMotionPoint.SelectedRows.Count == 0)
            {
                MessageBox.Show("請先選擇要轉移的點位。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. 取得現有的所有 Motion 區域名稱
            List<string> motionNames = new List<string>(_appState.CurrentRecipe.Motions.Keys);
            string sourceMotionName = CbArea.Text;
            motionNames.Remove(sourceMotionName); // 從列表中移除來源區域

            if (motionNames.Count == 0)
            {
                MessageBox.Show("沒有其他可轉移的目的地。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // 3. 顯示轉移視窗
            using (var transferForm = new TransferForm(motionNames))
            {
                if (transferForm.ShowDialog() == DialogResult.OK)
                {
                    // 4. 取得使用者選擇的目的地
                    string destinationMotionName = transferForm.SelectedDestinationMotion;

                    // 5. 執行資料轉移
                    try
                    {
                        var sourceMotion = _appState.CurrentRecipe.Motions[sourceMotionName];
                        var destinationMotion = _appState.CurrentRecipe.Motions[destinationMotionName];

                        // 準備要轉移的點位組別
                        var pointsToTransfer = new Dictionary<string, Point>();
                        foreach (DataGridViewRow row in DgMotionPoint.SelectedRows)
                        {
                            string pointName = row.Cells["點位名稱"].Value.ToString();
                            // 從來源 Motion 的 Groups.Point 字典中取得完整的 Point 物件
                            Point point = sourceMotion.Groups.Point[pointName];
                            pointsToTransfer.Add(pointName, point);
                        }

                        // 將點位從來源 Motion 的 Point 字典移除，並加入目的 Motion 的 Point 字典
                        foreach (var pointEntry in pointsToTransfer)
                        {
                            // 從來源 Point 字典移除點位
                            sourceMotion.Groups.Point.Remove(pointEntry.Key);
                            // 將點位加入目的 Point 字典
                            destinationMotion.Groups.Point.Add(pointEntry.Key, pointEntry.Value);
                        }

                        // 6. 更新 DataGridView
                        foreach (DataGridViewRow row in DgMotionPoint.SelectedRows)
                        {
                            DgMotionPoint.Rows.Remove(row);
                        }

                        // 7. 儲存 JSON 檔案
                        JsonFunction.SaveJson(filePath, _appState.RootObject); 

                        MessageBox.Show($"已成功將選定的點位組別轉移到 '{destinationMotionName}'。", "轉移完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"轉移時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DgMotionPoint_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSaveNodeId_Click(object sender, EventArgs e)
        {
            //1.先確認
            string currentProducTitle = _appState.CurrentProductTitle;
            string selectedMotionName = CbArea.Text;

            if (string.IsNullOrWhiteSpace(currentProducTitle) || string.IsNullOrWhiteSpace(selectedMotionName))
            {
                MessageBox.Show("請先載入產品並輸入或選擇一個區域別。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                // 2. 確保 AppState.RootObject 已經載入
                if (_appState.RootObject == null)
                {
                    MessageBox.Show("資料尚未載入，請先載入產品。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3. 取得目前產品的 Recipe 物件
                if (!_appState.RootObject.Products.TryGetValue(currentProducTitle, out Recipe currentRecipe))
                {
                    MessageBox.Show($"找不到產品 '{currentProducTitle}' 的資料。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4. 取得或建立新的 Motion 物件
                if (!currentRecipe.Motions.TryGetValue(selectedMotionName, out Motion currentMotion))
                {
                    // 如果 Motion 不存在，就建立一個新的
                    currentMotion = new Motion();
                    currentRecipe.Motions.Add(selectedMotionName, currentMotion);
                }

                // 5) 讀取四個 NodeId（用 TryParse 比較穩）
                if (!int.TryParse(CbX_NodeId.Text, out int xNode) ||
                    !int.TryParse(CbY_NodeId.Text, out int yNode) ||
                    !int.TryParse(CbZ_NodeId.Text, out int zNode) ||
                    !int.TryParse(CbR_NodeId.Text, out int rNode))
                {
                    MessageBox.Show("NodeID 必須是整數。", "格式錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 6) 寫回 Group 的 NodeId 欄位（不清空、不改動 Points）
                if (currentMotion.Groups == null)
                    currentMotion.Groups = new Group();

                currentMotion.Groups.X_NodeId = xNode;
                currentMotion.Groups.Y_NodeId = yNode;
                currentMotion.Groups.Z_NodeId = zNode;
                currentMotion.Groups.R_NodeId = rNode;

                 
                // 6. 將更新後的 AppState.RootObject 序列化並寫入檔案
                JsonFunction.SaveJson(filePath, _appState.RootObject);

                MessageBox.Show($"產品 '{currentProducTitle}' 的區域 '{selectedMotionName}' 已成功儲存。", "儲存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRecipe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"儲存時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





        }

        private void DgMotionPoint_SelectionChanged(object sender, EventArgs e)
        {
            if (DgMotionPoint.CurrentRow != null && DgMotionPoint.CurrentRow.Cells["點位名稱"].Value != null)
            {
                txtPointName.Text = DgMotionPoint.CurrentRow.Cells["點位名稱"].Value.ToString();
            }
            else
            {
                txtPointName.Text = string.Empty;
            }
            //txtPointName.Text = DgMotionPoint.CurrentRow?.Cells["點位名稱"].Value?.ToString() ?? string.Empty;
        }
    }
}
