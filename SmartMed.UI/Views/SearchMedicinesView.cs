using System;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public partial class SearchMedicinesView : UserControl
    {
        private readonly MedicineService _service = new MedicineService();

        public SearchMedicinesView()
        {
            InitializeComponent();
            UiFactory.ApplyViewChrome(this);
        }

        private void SearchMedicinesView_Load(object sender, EventArgs e)
        {
            StitchUiHelper.SetupPageHeaderWithRightLabel(
                pageHeader, "Search Medicines", $"Last updated: {DateTime.Now:h:mm tt}");
            StitchUiHelper.StyleFilterCard(filterPanel);
            StitchUiHelper.StyleGridCard(gridCard);
            StitchUiHelper.ApplyPrimaryAccentButton(btnSearch);
            StitchUiHelper.ApplySecondaryAccentButton(btnShowAll);
            StitchUiHelper.ApplyFieldLabel(lblName);
            StitchUiHelper.ApplyFieldLabel(lblCategory);
            StitchUiHelper.ApplyFieldLabel(lblMin);
            StitchUiHelper.ApplyFieldLabel(lblMax);

            UiFactory.ApplyTextBoxStyle(txtName, 140);
            UiFactory.ApplyTextBoxStyle(txtCategory, 140);
            UiFactory.ApplyTextBoxStyle(txtMinPrice, 60);
            UiFactory.ApplyTextBoxStyle(txtMaxPrice, 60);
            UiFactory.ApplyDataGridStyle(grid);
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (UiFactory.IsDesignMode(this)) return;
            grid.DataSource = _service.GetAll();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            decimal? min = decimal.TryParse(txtMinPrice.Text, out decimal mn) ? mn : (decimal?)null;
            decimal? max = decimal.TryParse(txtMaxPrice.Text, out decimal mx) ? mx : (decimal?)null;
            grid.DataSource = _service.Search(txtName.Text, txtCategory.Text, min, max);
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            grid.DataSource = _service.GetAll();
        }
    }
}
