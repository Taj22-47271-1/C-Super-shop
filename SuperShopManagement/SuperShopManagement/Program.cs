using System;
using System.Windows.Forms;

namespace SuperShopManagement
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DatabaseHelper.InitializeDatabase();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}