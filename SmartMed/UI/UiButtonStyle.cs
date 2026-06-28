namespace SmartMed.UI
{
    public sealed class UiButtonStyle
    {
        public string Key { get; }

        private UiButtonStyle(string key) => Key = key;

        public static readonly UiButtonStyle Primary = new UiButtonStyle("Primary");
        public static readonly UiButtonStyle Success = new UiButtonStyle("Success");
        public static readonly UiButtonStyle Danger = new UiButtonStyle("Danger");
        public static readonly UiButtonStyle Warning = new UiButtonStyle("Warning");
        public static readonly UiButtonStyle Secondary = new UiButtonStyle("Secondary");
    }
}
