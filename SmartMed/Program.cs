using System;
using System.Windows.Forms;

namespace SmartMed.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UiTheme.Init();
            Application.Run(new LoginForm());
        }
    }
}