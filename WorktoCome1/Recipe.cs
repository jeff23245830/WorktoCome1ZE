using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
namespace WorktoCome1
{

    public class RootObject
    {
        public Dictionary<string, Recipe> Products { get; set; }
    }

    // 食譜 (Recipe) 類別
    public class Recipe
    { 
        public Motion MotionName { get; set; }
         
        public Dictionary<string, Dictionary<string, string>> DIOName { get; set; }
    }

    // 運動 (Motion) 類別，用來處理多個 Group
    public class Motion
    {
        // 使用 Dictionary<string, Group> 來儲存 Group1, Group2...
         
        public Dictionary<string, Group> Groups { get; set; }
    }

    // 群組 (Group) 類別
    public class Group
    { 
        public int X_NodeId { get; set; }
         
        public int Y_NodeId { get; set; }
         
        public int Z_NodeId { get; set; }
         
        public int R_NodeId { get; set; }
         
        public Dictionary<string, Point> Point { get; set; }
    }

    // 點 (Point) 類別
    public class Point
    { 
        public double X { get; set; }
         
        public double Y { get; set; }
         
        public double Z { get; set; }
         
        public double R { get; set; }
    }


    /*
    public class YourMainClass
    {
        public void SaveCustomData(
        string productName,
        double x, double y, double z, double r,
        string dioFunctionName)
        {
            // 1. 建立 PointData 物件並帶入自訂參數
            var customPoint = new PointData
            {
                X = x,
                Y = y,
                Z = z,
                R = r
            };

            // 2. 建立 MotionPoint 物件，並帶入 PointData
            var customMotionPoint = new MotionPoint
            {
                X_NodeId = 0, // 假設 NodeId 也是自訂參數
                Y_NodeId = 0,
                Z_NodeId = 0,
                R_NodeId = 0,
                Point = new Dictionary<string, PointData>
                    {
                        {"1", customPoint}
                    }
            };

            // 3. 建立 MotionInfo 物件，並帶入 MotionPoint
            var customMotionInfo = new MotionInfo
            {
                Points = new Dictionary<string, MotionPoint>
                    {
                        {"安全等待點", customMotionPoint}
                    }
            };

            // 4. 建立 DioFunction 物件，並帶入自訂參數
            var customDioFunction = new DioFunction
            {
                NodeGroups = new Dictionary<string, Dictionary<string, string>>
                    {
                        {"NODE第1組", new Dictionary<string, string>
                            {
                                {"1", dioFunctionName}
                            }
                        }
                    }
            };

            // 5. 建立 ProductContent 物件，帶入 Motion 和 Dio 資料
            var customProductContent = new ProductContent
            {
                Motion = new Dictionary<string, MotionInfo>
                    {
                        {"供料", customMotionInfo}
                    },
                Dio = new Dictionary<string, DioFunction>
                    {
                        {"DO功能1", customDioFunction}
                    }
            };

            // 6. 建立最外層的 Product 物件
            var product = new Product
            {
                Recipes = new Dictionary<string, ProductContent>
                    {
                        { productName, customProductContent }
                    }
            };

            // 7. 將整個 Product 物件寫入檔案
            string filePath = $"{productName}.json"; // 檔案名稱可以根據產品名稱來命名
            SaveData(product, filePath); // 呼叫之前寫好的 SaveData 函數
        }
        public void SaveData(Product productData, string filePath)
        {
            try
            {
                // 設定序列化選項，讓 JSON 格式化以便閱讀
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // 啟用縮排
                };

                // 將 C# 物件序列化成 JSON 字串
                string jsonString = JsonSerializer.Serialize(productData, options);

                // 將 JSON 字串寫入指定的檔案
                File.WriteAllText(filePath, jsonString);

                MessageBox.Show("資料已成功儲存！", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"儲存檔案時發生錯誤：{ex.Message}", "錯誤");
            }
        }
    }
    */
}
