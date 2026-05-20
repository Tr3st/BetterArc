using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Progetto1.Services;
using Windows.Foundation.Metadata;

namespace Progetto1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })

                .ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                    events.AddWindows(windows => windows.OnWindowCreated(window =>
                    {
                        // Inizializza il Tray nativo
                        Progetto1.Platforms.Windows.WindowsTrayService.Initialize();

                        window.Closed += (s, e) =>
                        {
                            // Recuperiamo l'impostazione dalle preferenze
                            var settingsJson = Microsoft.Maui.Storage.Preferences.Default.Get("user_settings", "");

                            // Se l'utente vuole minimizzare nel tray
                            if (settingsJson.Contains("\"MinimizeToTrayOnClose\":true"))
                            {
                                e.Handled = true; // Blocca la chiusura definitiva
                                window.AppWindow.Hide(); // Nasconde la finestra (Metodo nativo WinUI 3)
                            }
                        };
                    }));
#endif
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<LanguageService>();
            builder.Services.AddSingleton<GammaService>();
            builder.Services.AddSingleton<ScreenScanner>();
            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
