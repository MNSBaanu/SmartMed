using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;

namespace SmartMed.UI
{
    public class ReportsForm : Form
    {
        private DataGridView grid;
        private readonly OrderService _service = new OrderService();

        public ReportsForm()
        {
            Text = "Generate Reports";
            Size = new Size(800, 480);
            StartPosition = FormStartPosition.CenterParent;

            grid = new DataGridView { Location = new Point(20, 70), Size = new Size(740, 350), ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

            var btnSales = new Button { Text = "Sales Report", Location = new Point(20, 20), Width = 120 };
            var btnStock = new Button { Text = "Stock Report", Location = new Point(150, 20), Width = 120 };
            var btnHistory = new Button { Text = "Customer Order History", Location = new Point(280, 20), Width = 180 };

            btnSales.Click += (s, e) => { grid.DataSource = _service.GetSalesReport(); };
            btnStock.Click += (s, e) => { grid.DataSource = _service.GetStockReport(); };
            btnHistory.Click += BtnHistory_Click;

            Controls.AddRange(new Control[] { btnSales, btnStock, btnHistory, grid });
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

    public class CustomerSelectDialog : Form
    {
        private ComboBox cmb;
        private readonly CustomerService _service = new CustomerService();
        public int? SelectedCustomerId { get; private set; }

        public CustomerSelectDialog()
        {
            Text = "Select Customer";
            Size = new Size(350, 150);
            StartPosition = FormStartPosition.CenterParent;
            cmb = new ComboBox { Location = new Point(20, 20), Width = 290, DropDownStyle = ComboBoxStyle.DropDownList };
            foreach (var c in _service.GetAll())
                cmb.Items.Add(new ComboItem(c.CustomerID, c.Name));
            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
            var btnOk = new Button { Text = "OK", Location = new Point(120, 60), Width = 80 };
            btnOk.Click += (s, e) => { SelectedCustomerId = ((ComboItem)cmb.SelectedItem).Id; DialogResult = DialogResult.OK; };
            Controls.AddRange(new Control[] { cmb, btnOk });
        }

        private class ComboItem
        {
            public int Id { get; }
            public string Name { get; }
            public ComboItem(int id, string name) { Id = id; Name = name; }
            public override string ToString() => Name;
        }
    }
}
