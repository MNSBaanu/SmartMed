using System;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public partial class ReportsView : UserControl
    {
        private readonly OrderService _service = new OrderService();

        public ReportsView()
        {
            InitializeComponent();
            UiFactory.ApplyViewChrome(this);
        }

        private void ReportsView_Load(object sender, EventArgs e)
        {
            StitchUiHelper.SetupPageHeader(pageHeader, "Generate Reports");
            StitchUiHelper.StyleGridCard(gridCard);
            StitchUiHelper.ApplyPrimaryAccentButton(btnSales);
            StitchUiHelper.ApplyPrimaryAccentButton(btnStock);
            StitchUiHelper.ApplyPrimaryAccentButton(btnHistory);

            UiFactory.ApplyDataGridStyle(grid);
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (UiFactory.IsDesignMode(this)) return;
            grid.DataSource = _service.GetSalesReport();
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            grid.DataSource = _service.GetSalesReport();
        }

        private void BtnStock_Click(object sender, EventArgs e)
        {
            grid.DataSource = _service.GetStockReport();
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
