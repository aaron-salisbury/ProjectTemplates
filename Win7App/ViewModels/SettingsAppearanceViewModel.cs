using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using Win7App.Base.Extensions;
using Win7App.Properties;

namespace Win7App.ViewModels
{
    public class SettingsAppearanceViewModel : NotifyPropertyChanged
    {
        private const string PALETTE_METRO = "Metro";
        private const string PALETTE_WP = "Windows Phone";

        private static readonly string _darkTheme = nameof(AppearanceManager.DarkThemeSource).GetFirstWord();
        private static readonly string _lightTheme = nameof(AppearanceManager.LightThemeSource).GetFirstWord();
        private static readonly string _fontSmall = nameof(FontSize.Small);
        private static readonly string _fontLarge = nameof(FontSize.Large);

        // 9 accent colors from metro design principles
        private readonly Color[] _metroAccentColors = new Color[]{
            Color.FromRgb(0x33, 0x99, 0xff),   // blue
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x33, 0x99, 0x33),   // green
            Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
            Color.FromRgb(0xf0, 0x96, 0x09),   // orange
            Color.FromRgb(0xff, 0x45, 0x00),   // orange red
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xff, 0x00, 0x97),   // magenta
            Color.FromRgb(0xa2, 0x00, 0xff),   // purple            
        };

        // 20 accent colors from Windows Phone 8
        private readonly Color[] _wpAccentColors = new Color[]{
            Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            Color.FromRgb(0x60, 0xa9, 0x17),   // green
            Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            Color.FromRgb(0x00, 0x50, 0xef),   // cobalt
            Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            Color.FromRgb(0x64, 0x76, 0x87),   // steel
            Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };

        private string _selectedPalette;
        private Color _selectedAccentColor;
        private Link _selectedTheme;
        private string _selectedFontSize;

        public LinkCollection Themes { get; } = new LinkCollection();

        public SettingsAppearanceViewModel()
        {
            // Add the default themes.
            Themes.Add(new Link { DisplayName = _darkTheme, Source = AppearanceManager.DarkThemeSource });
            Themes.Add(new Link { DisplayName = _lightTheme, Source = AppearanceManager.LightThemeSource });

            _selectedPalette = Settings.Default.SelectedPalette;
            AppearanceManager.Current.LoadAppearancesFromSettings(Settings.Default);
            SyncThemeAndColor();

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        private void SyncThemeAndColor()
        {
            // Synchronize the selected viewmodel appearance values with the actual ones used by the appearance manager.
            SelectedTheme = Themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));
            SelectedAccentColor = AppearanceManager.Current.AccentColor;
            SelectedFontSize = AppearanceManager.Current.FontSize == FontSize.Large ? _fontLarge : _fontSmall;

            SaveSettings();
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                SyncThemeAndColor();
            }
        }

        public string[] FontSizes
        {
            get { return new string[] { _fontSmall, _fontLarge }; }
        }

        public string[] Palettes
        {
            get { return new string[] { PALETTE_METRO, PALETTE_WP }; }
        }

        public Color[] AccentColors
        {
            get
            {
                Color[] accents;

                if (_selectedPalette == PALETTE_METRO)
                {
                    accents = _metroAccentColors;
                }
                else
                {
                    accents = _wpAccentColors;
                }

                return accents;
            }
        }

        public string SelectedPalette
        {
            get { return _selectedPalette; }
            set
            {
                if (_selectedPalette != value)
                {
                    _selectedPalette = value;
                    OnPropertyChanged("AccentColors");

                    SelectedAccentColor = AccentColors.FirstOrDefault();
                }
            }
        }

        public Link SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged("SelectedTheme");

                    // and update the actual theme
                    AppearanceManager.Current.ThemeSource = value.Source;
                }
            }
        }

        public string SelectedFontSize
        {
            get { return _selectedFontSize; }
            set
            {
                if (_selectedFontSize != value)
                {
                    _selectedFontSize = value;
                    OnPropertyChanged("SelectedFontSize");

                    AppearanceManager.Current.FontSize = value == _fontLarge ? FontSize.Large : FontSize.Small;
                }
            }
        }

        public Color SelectedAccentColor
        {
            get { return _selectedAccentColor; }
            set
            {
                if (_selectedAccentColor != value)
                {
                    _selectedAccentColor = value;
                    OnPropertyChanged("SelectedAccentColor");

                    AppearanceManager.Current.AccentColor = value;
                }
            }
        }

        private void SaveSettings()
        {
            Settings.Default.ThemeSource = SelectedTheme.DisplayName;
            Settings.Default.SelectedPalette = SelectedPalette;
            Settings.Default.AccentColor = SelectedAccentColor.ToString();
            Settings.Default.FontSize = SelectedFontSize;

            Settings.Default.Save();
        }
    }
}
