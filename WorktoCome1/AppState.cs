using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktoCome1
{
    public class AppState
    {
        public static string SelectedProductTitle { get; set; }
        public static string CurrentProducTitle { get; set; }

        public static RootObject RootObject { get; set; } = new RootObject();

        public static Recipe CurrentRecipe { get; set; } 


    }

   

}
