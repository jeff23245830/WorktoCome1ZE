using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorktoCome1
{
    public static class AppPaths
    {
        public static readonly string DataDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
                         "WorktoCome1");

        public static string UsersPath => Path.Combine(DataDir, "Users.json");
        public static string RecipePath => Path.Combine(DataDir, "Recipe.json");

        public static void EnsureDataDir()
        {
            Directory.CreateDirectory(DataDir); // 沒有就建
        }
    }
    public static class JsonFunction
    {

        public static string LoadJson(string FilePath)
        {
           
            if (!File.Exists(FilePath))
            {
                // 如果檔案不存在，您可以選擇拋出例外或是其他處理方式
                throw new FileNotFoundException("指定的檔案不存在。", FilePath);
            }
            string jsonString = File.ReadAllText(FilePath);
            
            //test1

            return jsonString;
        }
        public static void SaveJson(string FilePath, string JSON)
        {
            File.WriteAllText(FilePath, JSON);
        }
        public static void SaveJson(string FilePath, Object Obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // JSON 縮排
            };
            string jsonString = JsonSerializer.Serialize(Obj, options);
            File.WriteAllText(FilePath, jsonString);
        }
        
    }

}
