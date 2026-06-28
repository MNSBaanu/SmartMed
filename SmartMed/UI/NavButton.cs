using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    internal sealed class NavButton : Button
    {
        public NavButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            UiTheme.PaintNavButton(this, pevent.Graphics);
        }
    }
}
