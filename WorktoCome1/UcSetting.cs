using EtherCATFunction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WorktoCome1
{
    public partial class UcSetting : UserControl
    {
        private readonly AppState _appState;
        private string jsonFilePath = AppPaths.RecipePath;
        private List<ushort> slaveNodeIdList = new List<ushort>();
        private List<ushort> slaveSlotIdList = new List<ushort>();
        private bool _suppressNodeCheck = false;
        private EtherCATFunction.MotorMove cATFunction = new MotorMove();




        public UcSetting(AppState appState  )
        {
            InitializeComponent();
            ClearData();
            _appState = appState;
        }


        #region 特別(非介面事件)方法


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


        public void SetNodeID(List<ushort> nodeId)
        {
            this.slaveNodeIdList = nodeId;
            CbX_NodeId.Items.Clear();
            CbY_NodeId.Items.Clear();
            CbZ_NodeId.Items.Clear();
            CbR_NodeId.Items.Clear();




            CbX_NodeId.Items.Add(0);
            CbY_NodeId.Items.Add(0);
            CbZ_NodeId.Items.Add(0);
            CbR_NodeId.Items.Add(0);
            foreach (var slave in slaveNodeIdList)
            {
                CbX_NodeId.Items.Add(slave);
                CbY_NodeId.Items.Add(slave);
                CbZ_NodeId.Items.Add(slave);
                CbR_NodeId.Items.Add(slave);
            }
        }

        public void SetSlotID(List<ushort> slotId)
        {
            this.slaveSlotIdList = slotId;

        }


        public void EnabledServoOnOffBtn()
        {
            BtnSVON.Enabled = true;
            BtnSVOFF.Enabled = true;
        }

        public void ClearData()
        {
            DgMotionPoint.Rows.Clear();
            CbArea.Items.Clear();
            CbArea.Text = string.Empty;
        }


        #endregion





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
                        //讀取其他
                        double strVel = Convert.ToDouble(row.Cells["StrVel"].Value);
                        double constVel = Convert.ToDouble(row.Cells["ConstVel"].Value);
                        double endVel = Convert.ToDouble(row.Cells["EndVel"].Value);
                        double tacc = Convert.ToDouble(row.Cells["Tacc"].Value);
                        double tdec = Convert.ToDouble(row.Cells["Tdec"].Value);
                        bool sCurve = row.Cells["SCurve"].Value is bool b1 ? b1  : Convert.ToBoolean(row.Cells["SCurve"].Value ?? false);
                        bool isAbs = row.Cells["IsAbs"].Value is bool b2 ? b2 : Convert.ToBoolean(row.Cells["IsAbs"].Value ?? false);


                        // 建立新的 Group 和 Point 物件
                        var newGroup = new Group();
                        var newPoint = new Point();

                        newPoint.X = x;
                        newPoint.Y = y;
                        newPoint.Z = z;
                        newPoint.R = r;
                        newPoint.StrVel = strVel;
                        newPoint.ConstVel = constVel;
                        newPoint.EndVel = endVel;
                        newPoint.Tacc = tacc;
                        newPoint.Tdec = tdec;
                        newPoint.SCurve = sCurve;
                        newPoint.IsAbs = isAbs;



                        // 將點位加入 Group 的 Point 字典中
                        newGroup.Point.Add("1", newPoint);

                        // 將 Group 加入 Motion 的 Groups 字典中
                        currentMotion.Groups.Point.Add(pointName, newPoint);
                    }
                }

                // 6. 將更新後的 AppState.RootObject 序列化並寫入檔案
                JsonFunction.SaveJson(jsonFilePath, _appState.RootObject);

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
                        // 假設您的 DataGridView 欄位順序為：點位名稱, X, Y, Z, R ,馬達需要的參數
                        //int StrVel,             // 起始速度 (pps)
                        //int ConstVel,           // 最大/恆速 (pps)
                        //int EndVel,             // 結束速度 (pps)
                        //double Tacc,            // 加速時間 (sec)
                        //double Tdec,            // 減速時間 (sec)
                        //ushort SCurve,          // 1=S 曲線, 0=梯形
                        //ushort IsAbs            // 1=絕對, 0=相對
                        // 您可以根據需求自行調整

                        DgMotionPoint.Rows.Add(pointName, pointData.X, pointData.Y, pointData.Z, pointData.R, 
                            pointData.StrVel,
                            pointData.ConstVel,
                            pointData.EndVel,
                            pointData.Tacc,
                            pointData.Tdec,
                            pointData.SCurve,
                            pointData.IsAbs
                            );








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
                        JsonFunction.SaveJson(jsonFilePath, _appState.RootObject); 

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
                JsonFunction.SaveJson(jsonFilePath, _appState.RootObject);

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
       

        

        private void BtnSVON_Click(object sender, EventArgs e)
        {
            BtnStartMove.Enabled = true;
            var nodeIds = new List<ushort>();
            var slotIds = new List<ushort>(); // 全 0 就好

            if (TryGetNodeId(CbX_NodeId, out var xId, skipZero: true)) { nodeIds.Add(xId); slotIds.Add(0); }
            if (TryGetNodeId(CbY_NodeId, out var yId, skipZero: true)) { nodeIds.Add(yId); slotIds.Add(0); }
            if (TryGetNodeId(CbZ_NodeId, out var zId, skipZero: true)) { nodeIds.Add(zId); slotIds.Add(0); }
            if (TryGetNodeId(CbR_NodeId, out var rId, skipZero: true)) { nodeIds.Add(rId); slotIds.Add(0); }

            if (nodeIds.Count == 0)
            {
                MessageBox.Show("至少選一軸的 NodeID（且不是 0）才能上伺服～", "提醒",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ★ 軸數直接用陣列長度，最準
            int g_nSelectAxesCount = nodeIds.Count;

            // 丟進 DLL 需要的是陣列
            ushort[] g_uESCNodeID = nodeIds.ToArray();
            ushort[] g_uESCSlotID = slotIds.ToArray(); // 都是 0

            cATFunction.MultiServoOnOrOff(true, g_nSelectAxesCount, g_uESCNodeID, g_uESCSlotID);

        }

        private void BtnStartMove_Click(object sender, EventArgs e)
        {
            // 先把勾選/編輯中的值提交
            DgMotionPoint.CommitEdit(DataGridViewDataErrorContexts.Commit);
            DgMotionPoint.EndEdit();

            var row = DgMotionPoint.CurrentRow;
            if (row == null) return;

            var nodeIds = new List<ushort>();
            var slotIds = new List<ushort>();
            var moveVals = new List<int>();

            // 讀 XYZR（注意大小寫）
            int x = Convert.ToInt32(row.Cells["X"].Value);
            int y = Convert.ToInt32(row.Cells["Y"].Value);
            int z = Convert.ToInt32(row.Cells["Z"].Value);
            int r = Convert.ToInt32(row.Cells["R"].Value);

            // 只在成功取得且 nodeId>0 時才加入
            if (TryGetNodeId(CbX_NodeId, out var nx)) { nodeIds.Add(nx); slotIds.Add(0); moveVals.Add(x); }
            if (TryGetNodeId(CbY_NodeId, out var ny)) { nodeIds.Add(ny); slotIds.Add(0); moveVals.Add(y); }
            if (TryGetNodeId(CbZ_NodeId, out var nz)) { nodeIds.Add(nz); slotIds.Add(0); moveVals.Add(z); }
            if (TryGetNodeId(CbR_NodeId, out var nr)) { nodeIds.Add(nr); slotIds.Add(0); moveVals.Add(r); }

            if (nodeIds.Count == 0)
            {
                MessageBox.Show("至少選一軸的 NodeID（且不是 0）才能移動", "提醒",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 其他參數
            int nStrVel = Convert.ToInt32(row.Cells["StrVel"].Value);
            int nConstVel = Convert.ToInt32(row.Cells["ConstVel"].Value);
            int nEndVel = Convert.ToInt32(row.Cells["EndVel"].Value);
            double dTAcc = Convert.ToDouble(row.Cells["Tacc"].Value);
            double dTDec = Convert.ToDouble(row.Cells["Tdec"].Value);
            bool bSCurve = row.Cells["SCurve"].Value is bool b1 && b1;
            bool bkAbsMove = row.Cells["IsAbs"].Value is bool b2 && b2;

            int nDir = 1; // 相對移動才會用到



            ushort[] nodeIdsArray = new ushort[nodeIds.Count]; 
            // 2. 使用 for 迴圈來手動轉換並複製元素
            for (int i = 0; i < nodeIds.Count; i++)
            {
                nodeIdsArray[i] = nodeIds[i];
            }

            ushort[] slotIdsArray = new ushort[slotIds.Count];
            // 2. 使用 for 迴圈來手動轉換並複製元素
            for (int i = 0; i < slotIds.Count; i++)
            {
                slotIdsArray[i] = slotIds[i];
            }

            int[] moveValsArray = new int[moveVals.Count];
            // 2. 使用 for 迴圈來手動轉換並複製元素
            for (int i = 0; i < moveVals.Count; i++)
            {
                moveValsArray[i] = moveVals[i];
            }

            // 呼叫（用 ToArray 確保長度一致）
            ushort rt = cATFunction.MultiAxesMove(
                nDir,
                nodeIdsArray,
                slotIdsArray,
                moveValsArray,
                nStrVel, nConstVel, nEndVel,
                dTAcc, dTDec,
                bSCurve, bkAbsMove
            );


        }

        private static bool TryGetNodeId(ComboBox cb, out ushort nodeId, bool skipZero = true)
        {
            nodeId = 0;
            if (cb == null || cb.SelectedIndex < 0 || cb.SelectedItem == null) return false;

            // SelectedItem 可能是 ushort / int / string（甚至 "NodeID:3 - SlotID:0"）
            var item = cb.SelectedItem;
            switch (item)
            {
                case ushort u: nodeId = u; break;
                case int i: nodeId = (ushort)i; break;
                default:
                    var s = item.ToString();
                    // 嘗試直接 parse
                    if (!ushort.TryParse(s, out nodeId))
                    {
                        // 如果是 "NodeID:3 - SlotID:0" 這種，抓第一個數字
                        var m = System.Text.RegularExpressions.Regex.Match(s, @"\d+");
                        if (!m.Success || !ushort.TryParse(m.Value, out nodeId))
                            return false;
                    }
                    break;
            }
            if (skipZero && nodeId == 0) return false; // <<< 關鍵：跳過 0
            return true;
        }


        private void BtnSVOFF_Click(object sender, EventArgs e)
        {
            BtnStartMove.Enabled = false;

            int g_nSelectAxesCount = 0;

            //X有選擇的時候
            if (CbX_NodeId.SelectedIndex >= 0 && CbY_NodeId.SelectedIndex < 0 && CbZ_NodeId.SelectedIndex < 0 && CbR_NodeId.SelectedIndex < 0)
            {
                g_nSelectAxesCount += 1;
            }
            //Y有選擇的時候
            else if (CbX_NodeId.SelectedIndex < 0 && CbY_NodeId.SelectedIndex >= 0 && CbZ_NodeId.SelectedIndex < 0 && CbR_NodeId.SelectedIndex < 0)
            {
                g_nSelectAxesCount += 1;
            }
            //Z有選擇的時候
            else if (CbX_NodeId.SelectedIndex < 0 && CbY_NodeId.SelectedIndex < 0 && CbZ_NodeId.SelectedIndex >= 0 && CbR_NodeId.SelectedIndex < 0)
            {
                g_nSelectAxesCount += 1;
            }
            //R有選擇的時候
            else if (CbX_NodeId.SelectedIndex < 0 && CbY_NodeId.SelectedIndex < 0 && CbZ_NodeId.SelectedIndex < 0 && CbR_NodeId.SelectedIndex >= 0)
            {
                g_nSelectAxesCount += 1;
            }
            var nodeIds = new List<ushort>();

            if (CbX_NodeId.SelectedIndex >= 0) nodeIds.Add(Convert.ToUInt16(CbX_NodeId.SelectedItem.ToString()));
            if (CbY_NodeId.SelectedIndex >= 0) nodeIds.Add(Convert.ToUInt16(CbY_NodeId.SelectedItem.ToString()));
            if (CbZ_NodeId.SelectedIndex >= 0) nodeIds.Add(Convert.ToUInt16(CbZ_NodeId.SelectedItem.ToString()));
            if (CbR_NodeId.SelectedIndex >= 0) nodeIds.Add(Convert.ToUInt16(CbR_NodeId.SelectedItem.ToString()));

            if (nodeIds.Count == 0)
            {
                MessageBox.Show("至少選一軸的 NodeID 才能上伺服～", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // SlotID 先全 0（長度要一致）
            ushort[] g_uESCNodeID = nodeIds.ToArray();
            ushort[] g_uESCSlotID = new ushort[nodeIds.Count]; // 預設全 0

            cATFunction.MultiServoOnOrOff(false, g_nSelectAxesCount, g_uESCNodeID, g_uESCSlotID);

        }

        private void CbX_NodeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressNodeCheck) return;

            ComboBox cb = sender as ComboBox;
            if (cb == null) return;

            int x = CbX_NodeId.SelectedIndex;
            int y = CbY_NodeId.SelectedIndex;
            int z = CbZ_NodeId.SelectedIndex;
            int r = CbR_NodeId.SelectedIndex;

            // 還沒選好就先不檢查
            //if (x < 0 || y < 0 || z < 0 || r < 0) return;

            // 任兩個一樣就視為重複
            if (x == y || x == z || x == r )
            {
                _suppressNodeCheck = true;
                MessageBox.Show("Node 選擇重複", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cb.SelectedIndex = -1;     // 回到初始（未選取）
                _suppressNodeCheck = false;
            }
        }

        private void CbY_NodeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressNodeCheck) return;

            ComboBox cb = sender as ComboBox;
            if (cb == null) return;

            int x = CbX_NodeId.SelectedIndex;
            int y = CbY_NodeId.SelectedIndex;
            int z = CbZ_NodeId.SelectedIndex;
            int r = CbR_NodeId.SelectedIndex;

            // 還沒選好就先不檢查
            //if (x < 0 || y < 0 || z < 0 || r < 0) return;

            // 任兩個一樣就視為重複
            if (y == x || y == z || y == r)
            {
                _suppressNodeCheck = true;
                MessageBox.Show("Node 選擇重複", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cb.SelectedIndex = -1;     // 回到初始（未選取）
                _suppressNodeCheck = false;
            }
        }

        private void CbZ_NodeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressNodeCheck) return;

            ComboBox cb = sender as ComboBox;
            if (cb == null) return;

            int x = CbX_NodeId.SelectedIndex;
            int y = CbY_NodeId.SelectedIndex;
            int z = CbZ_NodeId.SelectedIndex;
            int r = CbR_NodeId.SelectedIndex;

            // 還沒選好就先不檢查
            //if (x < 0 || y < 0 || z < 0 || r < 0) return;

            // 任兩個一樣就視為重複
            if (z == x || z == y || z == r)
            {
                _suppressNodeCheck = true;
                MessageBox.Show("Node 選擇重複", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cb.SelectedIndex = -1;     // 回到初始（未選取）
                _suppressNodeCheck = false;
            }
        }

        private void CbR_NodeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressNodeCheck) return;

            ComboBox cb = sender as ComboBox;
            if (cb == null) return;

            int x = CbX_NodeId.SelectedIndex;
            int y = CbY_NodeId.SelectedIndex;
            int z = CbZ_NodeId.SelectedIndex;
            int r = CbR_NodeId.SelectedIndex;

            // 還沒選好就先不檢查
            //if (x < 0 || y < 0 || z < 0 || r < 0) return;

            // 任兩個一樣就視為重複
            if (r == x || r == z || r == y)
            {
                _suppressNodeCheck = true;
                MessageBox.Show("Node 選擇重複", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cb.SelectedIndex = -1;     // 回到初始（未選取）
                _suppressNodeCheck = false;
            }
        }
    }
}
