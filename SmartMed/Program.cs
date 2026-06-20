using System;
using System.Windows.Forms;
using SmartMed.Resources;

namespace SmartMed.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            FontManager.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
