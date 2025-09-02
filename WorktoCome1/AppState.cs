using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorktoCome1
{
    public class AppState
    {

        public  string SelectedProductTitle { get; set; }
        public  string CurrentProducTitle { get; set; }

        public RootObject RootObject { get; set; } = new RootObject();

        public Recipe CurrentRecipe { get; set; } 


    }

   

}
