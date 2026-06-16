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
            MessageBox.Show(
                "SmartMed UI is being redesigned. Add your new forms and set the startup form here.",
                "SmartMed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
