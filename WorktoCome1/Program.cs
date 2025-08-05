using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorktoCome1
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*
            LoginForm loginForm = new LoginForm();  
            if (loginForm.ShowDialog() == DialogResult.OK)
            { 
                Application.Run(new Form1());
            }
            */
            Application.Run(new Form1());
        }
    }
}
