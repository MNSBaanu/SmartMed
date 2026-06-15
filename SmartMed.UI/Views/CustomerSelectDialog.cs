using System;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public partial class CustomerSelectDialog : Form
    {
        private readonly CustomerService _service = new CustomerService();
        public int? SelectedCustomerId { get; private set; }

        public CustomerSelectDialog()
        {
            InitializeComponent();
            UiFactory.StyleAuthForm(this);
            UiFactory.CenterControlOnForm(this, panelCard);
        }

        private void CustomerSelectDialog_Load(object sender, EventArgs e)
        {
            panelCard.Width = 400;
            panelHeader.BackColor = ClinicalPrecisionTheme.Primary;
            lblHeader.Font = ClinicalPrecisionTheme.LabelFont;
            lblHeader.ForeColor = ClinicalPrecisionTheme.OnPrimary;
            ClinicalPrecisionUiHelper.StyleFilterCard(panelBody);
            ClinicalPrecisionUiHelper.ApplyFieldLabel(lblCustomer);
            ClinicalPrecisionUiHelper.ApplyPrimaryAccentButton(btnOk);
            ClinicalPrecisionUiHelper.ApplySecondaryAccentButton(btnCancel);
            UiFactory.ApplyComboBoxStyle(cmbCustomer, 360);

            if (UiFactory.IsDesignMode(this)) return;

            foreach (var c in _service.GetAll())
                cmbCustomer.Items.Add(new ComboItem(c.CustomerID, c.Name));
            if (cmbCustomer.Items.Count > 0) cmbCustomer.SelectedIndex = 0;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedItem is ComboItem item)
            {
                SelectedCustomerId = item.Id;
                DialogResult = DialogResult.OK;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

