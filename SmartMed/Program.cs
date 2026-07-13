using System;
using System.Windows.Forms;
using SmartMed.Data;
using SmartMed.UI;

namespace SmartMed
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UiTheme.Init();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "SmartMed could not connect to the database.\n\n" + ex.Message,
                    "SmartMed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            Application.Run(new SmartMedApplicationContext());
        }
    }
}
