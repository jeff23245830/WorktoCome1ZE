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
        /// 角色全域變數
        /// </summary>
        public static string UserLevel { get; set; }

        public static string LoginAccount { get; set; }


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
                        LoginAccount = loginForm.Account as string;
                         UserLevel = loginForm.Level as string;
                        using (var mainForm = new Form1(LoginAccount, UserLevel))
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
