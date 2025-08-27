using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktoCome1
{
    public class AppState
    {
        public static string SelectedProduct { get; set; }
        public static string CurrentProduct { get; set; }
    }

    public class CurrentRespcie
    {
        public string ProductName { get; set; }
        public string MotionName { get; set; }
        public string GroupName { get; set; }
        public string PointName { get; set; }
        public string DioFunctionName { get; set; }
    }


}
