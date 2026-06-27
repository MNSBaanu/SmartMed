using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SmartMedNew.UI
{
    internal static class FontAssets
    {
        private const string ResourcePrefix = "SmartMedNew.Assets.Fonts.";
        private const uint FrPrivate = 0x10;
        private static readonly string[] BundledFontFiles =
        {
            "HankenGrotesk-Regular.ttf",
            "HankenGrotesk-SemiBold.ttf",
            "HankenGrotesk-Bold.ttf"
        };
        private static bool _gdiRegistered;

        [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int AddFontResourceEx(string lpszFilename, uint fl, IntPtr pdv);

        public static void RegisterProcessFonts()
        {
            if (_gdiRegistered) return;
            _gdiRegistered = true;

            foreach (var fileName in BundledFontFiles)
            {
                try
                {
                    var path = GetFontFilePath(fileName);
                    AddFontResourceEx(path, FrPrivate, IntPtr.Zero);
                }
                catch
                {
                    // Continue with PrivateFontCollection fallback in UiTheme.
                }
            }
        }

        public static string GetFontFilePath(string fileName)
        {
            var fromDisk = TryGetFontFilePath(fileName);
            if (fromDisk != null)
                return fromDisk;

            var bytes = TryReadEmbedded(fileName)
                ?? throw new FileNotFoundException(
                    "Hanken Grotesk font file not found on disk or as an embedded resource.",
                    fileName);

            var cacheDir = Path.Combine(Path.GetTempPath(), "SmartMedNew", "Fonts");
            Directory.CreateDirectory(cacheDir);
            var cachedPath = Path.Combine(cacheDir, fileName);
            if (!File.Exists(cachedPath) || new FileInfo(cachedPath).Length != bytes.Length)
                File.WriteAllBytes(cachedPath, bytes);

            return cachedPath;
        }

        public static byte[] GetFontBytes(string fileName) =>
            File.ReadAllBytes(GetFontFilePath(fileName));

        public static string TryGetFontFilePath(string fileName)
        {
            foreach (var dir in GetSearchDirectories())
            {
                var path = Path.Combine(dir, fileName);
                if (File.Exists(path))
                    return path;
            }

            return null;
        }

        private static byte[] TryReadEmbedded(string fileName)
        {
            var assembly = typeof(FontAssets).Assembly;
            var resourceName = ResourcePrefix + fileName;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var memory = new MemoryStream())
                    {
                        stream.CopyTo(memory);
                        return memory.ToArray();
                    }
                }
            }

            var suffix = "." + fileName;
            foreach (var name in assembly.GetManifestResourceNames())
            {
                if (!name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                    continue;

                using (var stream = assembly.GetManifestResourceStream(name))
                {
                    if (stream == null) continue;
                    using (var memory = new MemoryStream())
                    {
                        stream.CopyTo(memory);
                        return memory.ToArray();
                    }
                }
            }

            return null;
        }

        private static string[] GetSearchDirectories()
        {
            var dirs = new System.Collections.Generic.List<string>();
            void AddDir(string dir)
            {
                if (string.IsNullOrWhiteSpace(dir)) return;
                try
                {
                    dir = Path.GetFullPath(dir);
                    if (dirs.Contains(dir, StringComparer.OrdinalIgnoreCase)) return;
                    dirs.Add(dir);
                }
                catch
                {
                    // Ignore invalid paths.
                }
            }

            AddDir(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Fonts"));

            try
            {
                var assemblyDir = Path.GetDirectoryName(typeof(FontAssets).Assembly.Location);
                AddDir(Path.Combine(assemblyDir ?? string.Empty, "Assets", "Fonts"));
            }
            catch
            {
                // Ignore assembly path issues.
            }

            var cursor = AppDomain.CurrentDomain.BaseDirectory;
            for (var i = 0; i < 6 && !string.IsNullOrEmpty(cursor); i++)
            {
                AddDir(Path.Combine(cursor, "Assets", "Fonts"));
                AddDir(Path.Combine(cursor, "SmartMedNew", "Assets", "Fonts"));
                var parent = Directory.GetParent(cursor);
                cursor = parent?.FullName;
            }

            return dirs.ToArray();
        }
    }
}
