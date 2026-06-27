using System.Drawing;
using System.Drawing.Drawing2D;

namespace SmartMedNew.UI
{
    internal static class ClinicalIcons
    {
        public static Bitmap CreateVisibilityIcon(bool visible, Color color, int size = 20)
        {
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (var pen = new Pen(color, 1.6f))
                using (var brush = new SolidBrush(color))
                {
                    var eye = new RectangleF(size * 0.08f, size * 0.28f, size * 0.84f, size * 0.44f);
                    g.DrawEllipse(pen, eye);

                    if (visible)
                    {
                        var pupil = new RectangleF(size * 0.38f, size * 0.42f, size * 0.24f, size * 0.24f);
                        g.FillEllipse(brush, pupil);
                    }
                    else
                    {
                        g.DrawLine(pen, size * 0.16f, size * 0.68f, size * 0.84f, size * 0.32f);
                    }
                }
            }

            return bitmap;
        }
    }
}
