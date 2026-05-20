namespace Progetto1.Services;

public class LanguageService
{
    // Dizionario che contiene tutte le scritte
    // In futuro potrai caricarlo da un file JSON esterno per cambiare lingua
    private readonly Dictionary<string, string> _strings = new()
    {
        // Titoli
        { "Settings_Title", "Impostazioni" },
        { "App_Name", "BETTER ARC" },

        // Switch Avvio Automatico
        { "Startup_Label", "Avvio Automatico" },
        { "Startup_Desc", "Avvia BetterArc all'avvio del PC" },

        // Switch Tray
        { "Tray_Label", "Riduci nel Tray" },
        { "Tray_Desc", "Nascondi l'app quando premi la X" },

        // Menu Tray
        { "Tray_Open", "Apri BETTER ARC" },
        { "Tray_Exit", "Esci" },

        //tabs
        { "Tab_Presets_Title", "modalità gamma" },
        { "Tab_presets_Subtitle", "Arc Raiders Profiles" },
        { "Tab_presets_1", "Normal" },
        { "Tab_presets_2", "Bright" },
        { "Tab_presets_3", "Brughter" },
        { "Tab_presets_4", "Custom" },

        // Altri testi...
        { "Save_Success", "Impostazioni salvate!" }
    };

    // Metodo per ottenere il testo
    public string Get(string key)
    {
        return _strings.TryGetValue(key, out var value) ? value : $"[{key}]";
    }
}