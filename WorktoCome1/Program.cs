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




            while (true)
            {
                using (var loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        string loginAccount = loginForm.Account as string;
                        string loginLevel = loginForm.Level as string;
                        using (var mainForm = new Form1(loginAccount, loginLevel))
                        {
                            if (mainForm.ShowDialog() != DialogResult.Retry)
                                break; // 不是登出就結束程式
                        }
                    }
                    else
                    {
                        break; // 取消登入就結束程式
                    }
                }
            }

                //Application.Run(new Form1());
            }
    }
}
