using System;
using System.Collections.Generic;
using System.Text;

namespace Progetto1
{
    // File: Models/AppSettings.cs
    public class UserSettings
    {

        public bool LaunchAtStartup { get; set; }

        
        public List<HotkeyMap> Hotkeys { get; set; } = new();

        // Questa è la lista che mancava!
        public List<UserProfile> SavedProfiles { get; set; } = new();

        // Valori del profilo custom attuale (quello degli slider)
        public double CustomGamma { get; set; } = 1.0;
        public double CustomBrightness { get; set; } = 1.0;
        public double CustomContrast { get; set; } = 1.0;
    }

    public class CustomPreset
    {
        public string Name { get; set; } = "";
        public double Gamma { get; set; }
        public double Brightness { get; set; }
        public double Contrast { get; set; }
    }

    public class HotkeyMap
    {
        public string ActionName { get; set; } = "";
        public string KeyCombo { get; set; } = "";
    }
    public class UserProfile // Definizione della classe che mancava
    {
        public string Name { get; set; } = "";
        public double Gamma { get; set; } = 1.0;
        public double Brightness { get; set; } = 1.0;
        public double Contrast { get; set; } = 1.0;
    }
}
