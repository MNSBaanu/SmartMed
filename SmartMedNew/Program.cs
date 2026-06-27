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

            // Forms will be added one by one. Verify database connectivity at startup.
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

            MessageBox.Show(
                "SmartMedNew scaffold is ready.\n\nAdd forms to the Forms folder when you are ready.",
                "SmartMedNew",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
