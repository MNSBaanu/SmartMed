using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI.Theming
{
    internal static class ClinicalPrecisionTheme
    {
        public static readonly Color Primary = Color.FromArgb(0, 31, 102);
        public static readonly Color PrimaryContainer = Color.FromArgb(0, 50, 150);
        public static readonly Color SecondaryContainer = Color.FromArgb(184, 207, 254);
        public static readonly Color OnSecondaryContainer = Color.FromArgb(66, 88, 128);
        public static readonly Color NavActiveCustomer = Color.FromArgb(135, 162, 255);
        public static readonly Color AccentBlue = Color.FromArgb(180, 203, 249);
        public static readonly Color Surface = Color.FromArgb(249, 249, 249);
        public static readonly Color SurfaceContainer = Color.FromArgb(238, 238, 238);
        public static readonly Color SurfaceContainerLow = Color.FromArgb(243, 243, 243);
        public static readonly Color ModalOverlay = Color.FromArgb(149, 162, 190);
        public static readonly Color SurfaceContainerLowest = Color.White;
        public static readonly Color SurfaceContainerHigh = Color.FromArgb(232, 232, 232);
        public static readonly Color SurfaceVariant = Color.FromArgb(226, 226, 226);
        public static readonly Color OnSurface = Color.FromArgb(27, 27, 27);
        public static readonly Color OnSurfaceVariant = Color.FromArgb(68, 70, 82);
        public static readonly Color OnPrimary = Color.White;
        public static readonly Color Outline = Color.FromArgb(116, 118, 132);
        public static readonly Color OutlineVariant = Color.FromArgb(196, 197, 212);
        public static readonly Color Error = Color.FromArgb(186, 26, 26);

        public static readonly Color StatusPendingBg = Color.FromArgb(254, 243, 199);
        public static readonly Color StatusPendingFg = Color.FromArgb(180, 83, 9);
        public static readonly Color StatusReadyBg = Color.FromArgb(219, 234, 254);
        public static readonly Color StatusReadyFg = Color.FromArgb(30, 64, 175);
        public static readonly Color StatusDeliveredBg = Color.FromArgb(220, 252, 231);
        public static readonly Color StatusDeliveredFg = Color.FromArgb(22, 101, 52);

        public const int NavWidth = 420;
        public const int NavItemHeight = 40;
        public const int HeaderHeight = 48;
        public const int ContainerPadding = 24;
        public const int StackMd = 16;
        public const int StackSm = 8;
        public const int ButtonHeight = 36;

        public static Font AppTitleFont => FontManager.Get(18.6f, FontStyle.Bold);
        public static Font SectionHeaderFont => FontManager.Get(16f, FontStyle.Bold);
        public static Font LabelFont => FontManager.Get(12f, FontStyle.Bold);
        public static Font BodyFont => FontManager.Get(14.6f, FontStyle.Regular);
        public static Font DataGridFont => FontManager.Get(13f, FontStyle.Regular);
        public static Font SmallFont => FontManager.Get(8.25f, FontStyle.Bold);
        public static Font VersionFont => FontManager.Get(7.5f, FontStyle.Italic);
    }
}
