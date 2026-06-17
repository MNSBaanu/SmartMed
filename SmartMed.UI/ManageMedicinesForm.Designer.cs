namespace SmartMed.UI
{
    partial class ManageMedicinesForm
    {
        private Controls.ManageMedicinesPanel medicinesPanel;

        private void InitializeContentPanel()
        {
            this.medicinesPanel = new Controls.ManageMedicinesPanel();
            this.medicinesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.medicinesPanel.Name = "medicinesPanel";
            this.medicinesPanel.TabIndex = 0;
            this.panelContent.AutoScroll = true;
            this.panelContent.Controls.Add(this.medicinesPanel);
        }
    }
}
