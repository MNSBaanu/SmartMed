using System.Drawing;

namespace SmartMed.UI.Theming
{
    /// <summary>Design tokens from the Stitch SmartMed Pharmacy UI spec.</summary>
    internal static class AppTheme
    {
        public const int ContainerPadding = 24;
        public const int StackGap = 16;
        public const int InputHeight = 40;
        public const int CardWidth = 420;
        public const int RegistrationCardWidth = 450;
        public const int NavWidth = 280;
        public const int TopBarHeight = 48;

        public static readonly Color Primary = Color.FromArgb(0, 31, 102);
        public static readonly Color OnPrimary = Color.White;
        public static readonly Color OnPrimaryMuted = Color.FromArgb(180, 255, 255, 255);
        public static readonly Color OnPrimaryFaint = Color.FromArgb(128, 255, 255, 255);

        public static readonly Color Surface = Color.FromArgb(249, 249, 249);
        public static readonly Color SurfaceContainerLowest = Color.White;
        public static readonly Color SurfaceContainer = Color.FromArgb(238, 238, 238);

        public static readonly Color OnSurface = Color.FromArgb(27, 27, 27);
        public static readonly Color OnSurfaceVariant = Color.FromArgb(68, 70, 82);
        public static readonly Color Outline = Color.FromArgb(116, 118, 132);
        public static readonly Color OutlineVariant = Color.FromArgb(196, 197, 212);
        public static readonly Color CardBorder = Color.FromArgb(229, 231, 235);

        public static readonly Color SecondaryContainer = Color.FromArgb(184, 207, 254);
        public static readonly Color OnSecondaryContainer = Color.FromArgb(66, 88, 128);
        public static readonly Color Error = Color.FromArgb(186, 26, 26);
        public static readonly Color Success = Color.FromArgb(22, 163, 74);

        public static readonly Color Placeholder = Color.Gray;
        public static readonly Color HeaderHover = Color.FromArgb(40, 255, 255, 255);

        public static Font LabelFont => FontManager.Get(9f, FontStyle.Bold);
        public static Font BodyFont => FontManager.Get(9.25f);
        public static Font AppTitleFont => FontManager.Get(11.25f, FontStyle.Bold);
        public static Font SectionHeaderFont => FontManager.Get(12f, FontStyle.Bold);
        public static Font StatValueFont => FontManager.Get(18f, FontStyle.Bold);
        public static Font LinkFont => FontManager.Get(8.25f, FontStyle.Bold);
        public static Font VersionFont => FontManager.Get(8f, FontStyle.Italic);
        public static Font IconFont => new Font("Segoe MDL2 Assets", 11f);
        public static Font IconFontSmall => new Font("Segoe MDL2 Assets", 10f);
    }
}
