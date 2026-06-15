using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public class SearchMedicinesView : UserControl
    {
        private DataGridView grid;
        private TextBox txtName, txtCategory, txtMinPrice, txtMaxPrice;
        private readonly MedicineService _service = new MedicineService();

        public SearchMedicinesView()
        {
            BackColor = ClinicalPrecisionTheme.Surface;
            Dock = DockStyle.Fill;

            var header = UiFactory.CreateSectionHeader("Search Medicines");
            header.Dock = DockStyle.Top;

            var filterPanel = new Panel { Dock = DockStyle.Top, Height = 80, Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, 0) };

            int y = 0;
            filterPanel.Controls.Add(UiFactory.CreateFieldLabel("Name:")); filterPanel.Controls[filterPanel.Controls.Count - 1].Location = new Point(0, y);
            txtName = new TextBox(); UiFactory.ApplyTextBoxStyle(txtName, 140); txtName.Location = new Point(50, y); filterPanel.Controls.Add(txtName);
            filterPanel.Controls.Add(UiFactory.CreateFieldLabel("Category:")); filterPanel.Controls[filterPanel.Controls.Count - 1].Location = new Point(210, y);
            txtCategory = new TextBox(); UiFactory.ApplyTextBoxStyle(txtCategory, 140); txtCategory.Location = new Point(280, y); filterPanel.Controls.Add(txtCategory);
            filterPanel.Controls.Add(UiFactory.CreateFieldLabel("Min:")); filterPanel.Controls[filterPanel.Controls.Count - 1].Location = new Point(440, y);
            txtMinPrice = new TextBox(); UiFactory.ApplyTextBoxStyle(txtMinPrice, 60); txtMinPrice.Location = new Point(480, y); filterPanel.Controls.Add(txtMinPrice);
            filterPanel.Controls.Add(UiFactory.CreateFieldLabel("Max:")); filterPanel.Controls[filterPanel.Controls.Count - 1].Location = new Point(550, y);
            txtMaxPrice = new TextBox(); UiFactory.ApplyTextBoxStyle(txtMaxPrice, 60); txtMaxPrice.Location = new Point(590, y); filterPanel.Controls.Add(txtMaxPrice);
            y += 35;

            var btnSearch = UiFactory.CreatePrimaryButton("Search", 100);
            var btnShowAll = UiFactory.CreateSecondaryButton("Show All", 100);
            btnSearch.Location = new Point(0, y);
            btnShowAll.Location = new Point(110, y);
            btnSearch.Click += BtnSearch_Click;
            btnShowAll.Click += (s, e) => grid.DataSource = _service.GetAll();
            filterPanel.Controls.Add(btnSearch);
            filterPanel.Controls.Add(btnShowAll);

            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UiFactory.ApplyDataGridStyle(grid);
            Controls.Add(grid);
            Controls.Add(filterPanel);
            Controls.Add(header);
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
