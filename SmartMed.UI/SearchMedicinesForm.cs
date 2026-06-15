using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;

namespace SmartMed.UI
{
    public class SearchMedicinesForm : Form
    {
        private DataGridView grid;
        private TextBox txtName, txtCategory, txtMinPrice, txtMaxPrice;
        private readonly MedicineService _service = new MedicineService();

        public SearchMedicinesForm()
        {
            Text = "Search Medicines";
            Size = new Size(800, 480);
            StartPosition = FormStartPosition.CenterParent;

            int y = 20;
            Controls.Add(new Label { Text = "Name:", Location = new Point(20, y + 3), AutoSize = true });
            txtName = new TextBox { Location = new Point(100, y), Width = 150 };
            Controls.Add(new Label { Text = "Category:", Location = new Point(270, y + 3), AutoSize = true });
            txtCategory = new TextBox { Location = new Point(350, y), Width = 150 };
            Controls.Add(new Label { Text = "Min Price:", Location = new Point(520, y + 3), AutoSize = true });
            txtMinPrice = new TextBox { Location = new Point(600, y), Width = 60 };
            Controls.Add(new Label { Text = "Max:", Location = new Point(670, y + 3), AutoSize = true });
            txtMaxPrice = new TextBox { Location = new Point(710, y), Width = 60 };
            y += 40;

            var btnSearch = new Button { Text = "Search", Location = new Point(20, y), Width = 100 };
            var btnShowAll = new Button { Text = "Show All", Location = new Point(130, y), Width = 100 };
            btnSearch.Click += BtnSearch_Click;
            btnShowAll.Click += (s, e) => grid.DataSource = _service.GetAll();
            y += 45;

            grid = new DataGridView { Location = new Point(20, y), Size = new Size(750, 300), ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            Controls.AddRange(new Control[] { txtName, txtCategory, txtMinPrice, txtMaxPrice, btnSearch, btnShowAll, grid });
            grid.DataSource = _service.GetAll();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            decimal? min = decimal.TryParse(txtMinPrice.Text, out decimal mn) ? mn : (decimal?)null;
            decimal? max = decimal.TryParse(txtMaxPrice.Text, out decimal mx) ? mx : (decimal?)null;
            grid.DataSource = _service.Search(txtName.Text, txtCategory.Text, min, max);
        }
    }
}
