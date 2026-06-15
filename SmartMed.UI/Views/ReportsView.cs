using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public class ReportsView : UserControl
    {
        private DataGridView grid;
        private readonly OrderService _service = new OrderService();

        public ReportsView()
        {
            BackColor = ClinicalPrecisionTheme.Surface;
            Dock = DockStyle.Fill;

            var header = UiFactory.CreateSectionHeader("Generate Reports");
            header.Dock = DockStyle.Top;

            var toolbar = new Panel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, 0) };

            var btnSales = UiFactory.CreatePrimaryButton("Sales Report", 120);
            var btnStock = UiFactory.CreatePrimaryButton("Stock Report", 120);
            var btnHistory = UiFactory.CreatePrimaryButton("Customer Order History", 180);
            btnSales.Location = new Point(0, 8);
            btnStock.Location = new Point(130, 8);
            btnHistory.Location = new Point(260, 8);
            btnSales.Click += (s, e) => { grid.DataSource = _service.GetSalesReport(); };
            btnStock.Click += (s, e) => { grid.DataSource = _service.GetStockReport(); };
            btnHistory.Click += BtnHistory_Click;
            toolbar.Controls.AddRange(new Control[] { btnSales, btnStock, btnHistory });

            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UiFactory.ApplyDataGridStyle(grid);
            Controls.Add(grid);
            Controls.Add(toolbar);
            Controls.Add(header);
            grid.DataSource = _service.GetSalesReport();
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            using (var dlg = new CustomerSelectDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK && dlg.SelectedCustomerId.HasValue)
                    grid.DataSource = _service.GetCustomerOrderHistory(dlg.SelectedCustomerId.Value);
            }
        }
    }
}
