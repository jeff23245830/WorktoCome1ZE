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
        public string Default_Recipe { get; set; } = "";
        // 產品清單，鍵為產品名稱 (如 "8號產品")
        public Dictionary<string, Recipe> Products { get; set; } = new Dictionary<string, Recipe>();
    }

    public class Recipe
    {
        // 這裡使用字典來處理 "供料" 或 "MotionName" 這類動態鍵
        public Dictionary<string, Motion> Motions { get; set; } = new Dictionary<string, Motion>();

        // 這裡使用字典來處理 "DO功能1" 或 "DIOName" 這類動態鍵
        public Dictionary<string, Dio> DioFunctions { get; set; } = new Dictionary<string, Dio>();
    }

    public class Motion
    {
        public Group Groups { get; set; } = new Group();
    }


    public class Group
    {
        public int X_NodeId { get; set; } = 0;
        public int Y_NodeId { get; set; } = 0;
        public int Z_NodeId { get; set; } = 0;
        public int R_NodeId { get; set; } = 0;

        // 這裡使用字典來處理 "Point" 這類動態鍵
        public Dictionary<string, Point> Point { get; set; } = new Dictionary<string, Point>();
    }

    public class Point
    {
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get; set; } = 0;
        public double R { get; set; } = 0;

        public double StrVel { get; set; } = 0;
        public double ConstVel { get; set; } = 0;
        public double EndVel { get; set; } = 0;
        public double Tacc { get; set; } = 0;

        public double Tdec { get; set; } = 0;
        public bool SCurve { get; set; } = false;
        public bool IsAbs { get; set; } = false; 

    }

    // 新增 DIO 類別以處理 DO 功能
    public class Dio
    {
        public Dictionary<string, Dictionary<string, string>> NodeGroups { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    }

}
