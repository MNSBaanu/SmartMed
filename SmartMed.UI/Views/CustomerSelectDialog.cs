using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public class CustomerSelectDialog : Form
    {
        private ComboBox cmb;
        private readonly CustomerService _service = new CustomerService();
        public int? SelectedCustomerId { get; private set; }

        public CustomerSelectDialog()
        {
            UiFactory.StyleAuthForm(this);
            Text = "Select Customer";

            var card = UiFactory.CreateCardPanel(340, 130);
            Controls.Add(card);
            UiFactory.CenterControlOnForm(this, card);

            var lbl = UiFactory.CreateFieldLabel("Customer");
            lbl.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, ClinicalPrecisionTheme.StackMd);
            card.Controls.Add(lbl);

            cmb = new ComboBox { Location = new Point(ClinicalPrecisionTheme.ContainerPadding, 36), DropDownStyle = ComboBoxStyle.DropDownList };
            UiFactory.ApplyComboBoxStyle(cmb, 280);
            foreach (var c in _service.GetAll())
                cmb.Items.Add(new ComboItem(c.CustomerID, c.Name));
            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
            card.Controls.Add(cmb);

            var btnOk = UiFactory.CreatePrimaryButton("OK", 80);
            var btnCancel = UiFactory.CreateSecondaryButton("Cancel", 80);
            btnOk.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, 76);
            btnCancel.Location = new Point(ClinicalPrecisionTheme.ContainerPadding + 90, 76);
            btnOk.Click += (s, e) =>
            {
                if (cmb.SelectedItem is ComboItem item)
                {
                    SelectedCustomerId = item.Id;
                    DialogResult = DialogResult.OK;
                }
            };
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
            card.Controls.Add(btnOk);
            card.Controls.Add(btnCancel);
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
