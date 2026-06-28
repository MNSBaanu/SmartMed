using System;
using System.IO;
using PdfSharp.Fonts;

namespace SmartMed.UI
{
    internal sealed class HankenGroteskFontResolver : IFontResolver
    {
        internal const string RegularFace = "HankenGrotesk-Regular";
        internal const string SemiBoldFace = "HankenGrotesk-SemiBold";
        internal const string BoldFace = "HankenGrotesk-Bold";

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (!UiTheme.FontFamilyName.Equals(familyName, StringComparison.OrdinalIgnoreCase))
                return null;

            if (isBold)
                return new FontResolverInfo(BoldFace);
            return new FontResolverInfo(RegularFace);
        }

        public byte[] GetFont(string faceName) =>
            FontAssets.GetFontBytes(faceName + ".ttf");
    }
}
