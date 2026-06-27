using System;
using System.Windows.Forms;
using SmartMedNew.Data;

namespace SmartMedNew.UI
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
                    "SmartMedNew could not connect to the database.\n\n" + ex.Message,
                    "SmartMedNew",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            Application.Run(new SmartMedApplicationContext());
        }
    }
}
