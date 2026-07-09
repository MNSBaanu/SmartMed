using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    internal sealed class PaymentCheckoutDialog : Form
    {
        private readonly decimal _total;
        private readonly RadioButton _rbCard;
        private readonly RadioButton _rbCash;
        private readonly RadioButton _rbBank;
        private readonly Panel _panelCard;
        private readonly Panel _panelBank;
        private readonly Panel _panelCash;
        private readonly TextBox _txtCardNumber;
        private readonly TextBox _txtCardName;
        private readonly TextBox _txtCardExpiry;
        private readonly TextBox _txtCardCvv;
        private readonly TextBox _txtBankName;
        private readonly TextBox _txtDepositorName;
        private readonly TextBox _txtBankReference;
        private readonly TextBox _txtTransferDate;

        public PaymentResult Result { get; private set; }

        public PaymentCheckoutDialog(decimal total)
        {
            _total = total;

            Text = "Payment";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(480, 540);
            MaximizeBox = false;
            MinimizeBox = false;
            Font = UiTheme.UiFont;
            BackColor = UiTheme.AdminSurface;

            var lblTitle = new Label
            {
                Text = "Complete Payment",
                Left = 20,
                Top = 16,
                AutoSize = true,
                Font = UiTheme.UiFontBold,
                ForeColor = UiTheme.AdminOnSurface,
                BackColor = UiTheme.AdminSurface
            };

            var lblAmount = new Label
            {
                Text = $"Amount payable: LKR {_total:N2}",
                Left = 20,
                Top = 42,
                AutoSize = true,
                ForeColor = UiTheme.AdminTeal,
                Font = UiTheme.UiFontBold,
                BackColor = UiTheme.AdminSurface
            };

            var lblMethod = new Label
            {
                Text = "Payment method",
                Left = 20,
                Top = 72,
                AutoSize = true,
                ForeColor = UiTheme.AdminMuted,
                BackColor = UiTheme.AdminSurface
            };

            _rbCard = CreateRadio(PaymentService.MethodCard, 20, 96, true);
            _rbCash = CreateRadio(PaymentService.MethodCashOnPickup, 20, 122, false);
            _rbBank = CreateRadio(PaymentService.MethodBankTransfer, 20, 148, false);

            var panelHost = new Panel
            {
                Left = 20,
                Top = 178,
                Width = 440,
                Height = 280,
                BackColor = Color.FromArgb(238, 245, 244)
            };

            _panelCard = CreateDetailPanel();
            _panelBank = CreateDetailPanel();
            _panelCash = CreateDetailPanel();

            _txtCardNumber = AddField(_panelCard, ValidationService.RequiredLabel("Card Number"), 0,
                "Enter 16-digit card number", 280);
            _txtCardNumber.MaxLength = 19;
            _txtCardNumber.KeyPress += CardNumber_KeyPress;

            _txtCardName = AddField(_panelCard, ValidationService.RequiredLabel("Cardholder Name"), 1,
                "Name as printed on card", 280);

            _txtCardExpiry = AddField(_panelCard, ValidationService.RequiredLabel("Expiry Date (MM/YY)"), 2,
                "MM/YY", 100);
            _txtCardExpiry.MaxLength = 5;
            _txtCardExpiry.KeyPress += Expiry_KeyPress;

            _txtCardCvv = AddField(_panelCard, ValidationService.RequiredLabel("CVV / Security Code"), 3,
                "3 or 4 digits", 100);
            _txtCardCvv.MaxLength = 4;
            _txtCardCvv.PasswordChar = '•';
            _txtCardCvv.KeyPress += DigitsOnly_KeyPress;

            AddInfoLabel(_panelCard, 4,
                "Your card details are validated locally for this academic demo. No real charge is made.");

            _panelBank.Controls.Add(CreateBankDetailsLabel());

            _txtBankName = AddField(_panelBank, ValidationService.RequiredLabel("Your Bank Name"), 2,
                "e.g. Bank of Ceylon", 280);
            _txtDepositorName = AddField(_panelBank, ValidationService.RequiredLabel("Account Holder Name"), 3,
                "Name on your bank account", 280);
            _txtBankReference = AddField(_panelBank, ValidationService.RequiredLabel("Transaction Reference"), 4,
                "Reference from bank receipt / slip", 280);
            _txtTransferDate = AddField(_panelBank, "Transfer Date", 5,
                "DD/MM/YYYY (optional)", 120);

            _panelCash.Controls.Add(new Label
            {
                Text = "Pay in cash when you collect your order from the pharmacy counter.",
                Left = 12,
                Top = 16,
                Width = 400,
                Height = 40,
                ForeColor = UiTheme.AdminOnSurface,
                BackColor = Color.FromArgb(238, 245, 244)
            });
            _panelCash.Controls.Add(new Label
            {
                Text = "No online payment is required now. Your order will remain Pending until collection.",
                Left = 12,
                Top = 60,
                Width = 400,
                Height = 40,
                ForeColor = UiTheme.AdminMuted,
                BackColor = Color.FromArgb(238, 245, 244)
            });

            panelHost.Controls.Add(_panelCard);
            panelHost.Controls.Add(_panelBank);
            panelHost.Controls.Add(_panelCash);

            var btnPay = AdminUiHelpers.CreateWinButton("Pay & Place Order", true, 140);
            btnPay.Left = 320;
            btnPay.Top = 492;
            btnPay.Click += BtnPay_Click;

            var btnCancel = AdminUiHelpers.CreateWinButton("Cancel", false, 90);
            btnCancel.Left = 220;
            btnCancel.Top = 492;
            btnCancel.DialogResult = DialogResult.Cancel;

            Controls.AddRange(new Control[]
            {
                lblTitle, lblAmount, lblMethod,
                _rbCard, _rbCash, _rbBank,
                panelHost, btnPay, btnCancel
            });

            AcceptButton = btnPay;
            CancelButton = btnCancel;

            _rbCard.CheckedChanged += (s, e) => UpdateDetailsPanel();
            _rbCash.CheckedChanged += (s, e) => UpdateDetailsPanel();
            _rbBank.CheckedChanged += (s, e) => UpdateDetailsPanel();
            UpdateDetailsPanel();
        }

        private static Panel CreateDetailPanel()
        {
            return new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(238, 245, 244),
                Visible = false
            };
        }

        private Label CreateBankDetailsLabel()
        {
            return new Label
            {
                Text = "Transfer to SmartMed Pharmacy" + Environment.NewLine
                    + $"Bank: {PaymentService.PharmacyBankName}" + Environment.NewLine
                    + $"Account Name: {PaymentService.PharmacyAccountName}" + Environment.NewLine
                    + $"Account No: {PaymentService.PharmacyAccountNumber}" + Environment.NewLine
                    + $"Branch: {PaymentService.PharmacyBranch}",
                Left = 12,
                Top = 8,
                Width = 400,
                Height = 72,
                ForeColor = UiTheme.AdminOnSurface,
                Font = UiTheme.UiFontBold,
                BackColor = Color.FromArgb(238, 245, 244)
            };
        }

        private static TextBox AddField(Panel panel, string labelText, int rowIndex, string hint, int width)
        {
            const int rowHeight = 56;
            var top = 8 + rowIndex * rowHeight;

            var lbl = new Label
            {
                Text = labelText,
                Left = 12,
                Top = top,
                Width = 400,
                Height = 16,
                ForeColor = UiTheme.AdminOnSurface,
                Font = UiTheme.UiFontBold,
                BackColor = Color.FromArgb(238, 245, 244)
            };

            var box = new TextBox
            {
                Left = 12,
                Top = top + 18,
                Width = width
            };
            UiTheme.StyleTextBox(box);

            if (!string.IsNullOrWhiteSpace(hint))
            {
                panel.Controls.Add(new Label
                {
                    Text = hint,
                    Left = 12,
                    Top = top + 40,
                    Width = 400,
                    Height = 14,
                    ForeColor = UiTheme.AdminMuted,
                    Font = new Font(UiTheme.UiFont.FontFamily, 8f),
                    BackColor = Color.FromArgb(238, 245, 244)
                });
            }

            panel.Controls.Add(lbl);
            panel.Controls.Add(box);
            return box;
        }

        private static void AddInfoLabel(Panel panel, int rowIndex, string text)
        {
            const int rowHeight = 56;
            panel.Controls.Add(new Label
            {
                Text = text,
                Left = 12,
                Top = 8 + rowIndex * rowHeight,
                Width = 400,
                Height = 32,
                ForeColor = UiTheme.AdminMuted,
                BackColor = Color.FromArgb(238, 245, 244)
            });
        }

        private static RadioButton CreateRadio(string text, int left, int top, bool selected)
        {
            return new RadioButton
            {
                Text = text,
                Left = left,
                Top = top,
                AutoSize = true,
                Checked = selected,
                ForeColor = UiTheme.AdminOnSurface,
                BackColor = UiTheme.AdminSurface,
                Font = UiTheme.UiFont
            };
        }

        private void UpdateDetailsPanel()
        {
            _panelCard.Visible = _rbCard.Checked;
            _panelBank.Visible = _rbBank.Checked;
            _panelCash.Visible = _rbCash.Checked;
        }

        private string SelectedMethod()
        {
            if (_rbCard.Checked) return PaymentService.MethodCard;
            if (_rbCash.Checked) return PaymentService.MethodCashOnPickup;
            if (_rbBank.Checked) return PaymentService.MethodBankTransfer;
            return null;
        }

        private void BtnPay_Click(object sender, EventArgs e)
        {
            try
            {
                Result = PaymentService.Process(new PaymentRequest
                {
                    Method = SelectedMethod(),
                    CardNumber = _txtCardNumber.Text,
                    CardName = _txtCardName.Text,
                    CardExpiry = _txtCardExpiry.Text,
                    CardCvv = _txtCardCvv.Text,
                    BankName = _txtBankName.Text,
                    DepositorName = _txtDepositorName.Text,
                    BankReference = _txtBankReference.Text,
                    TransferDate = _txtTransferDate.Text
                }, _total);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payment Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void DigitsOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void CardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            var digits = new string(_txtCardNumber.Text.Where(char.IsDigit).ToArray());
            if (digits.Length >= 16)
                e.Handled = true;
        }

        private void Expiry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '/')
            {
                e.Handled = true;
                return;
            }

            var text = _txtCardExpiry.Text;
            if (char.IsDigit(e.KeyChar) && text.Length == 2 && !text.Contains("/"))
            {
                _txtCardExpiry.Text = text + "/";
                _txtCardExpiry.SelectionStart = _txtCardExpiry.Text.Length;
            }
        }
    }
}
