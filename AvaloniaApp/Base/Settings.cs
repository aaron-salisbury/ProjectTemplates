﻿using System;

namespace AvaloniaApp.Base
{
    [Serializable]
    public class Settings
    {
        public string? ThemeMode { get; set; }
        public double? BackgroundOpacity { get; set; }
    }
}
