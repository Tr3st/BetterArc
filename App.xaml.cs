using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Devices;

// Queste direttive servono solo su Windows
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Microsoft.UI.Dispatching;
#endif
using Progetto1;

namespace Progetto1
{
    public partial class App : Application
    {

       
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Creiamo la pagina Blazor
            var mainPage = new MainPage();

            var window = new Window(mainPage);
            // Inizializza la Page della finestra qui invece di usare Application.MainPage (deprecated)
            window.Page = mainPage;
            window.Destroying += (s, e) =>
            {
                // 1. Recuperiamo il servizio
                var gammaService = Handler?.MauiContext?.Services.GetService<GammaService>();

                if (gammaService != null)
                {
                    // 2. RESET AGGRESSIVO
                    // Inviamo il reset più volte in un ciclo velocissimo per "sovrascrivere" il timer
                    for (int i = 0; i < 10; i++)
                    {
                        gammaService.ApplicaSettaggi(1.0, 1.0, 1.0, 1.0, 1.0);
                    }

                    // 3. Fermiamo il processo immediatamente
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            };

            window.Title = "BETTER-BRIGHT ARC";

            // 1. Definiamo le dimensioni
            const int width = 750;
            const int height = 600;

            window.Width = width;
            window.Height = height; 

            // 2. Blocchiamo il ridimensionamento
            window.MinimumWidth = window.MaximumWidth = width;
            window.MinimumHeight = window.MaximumHeight = height;
#if WINDOWS
            window.HandlerChanged += (sender, e) =>
            {
                if (window.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
                {
                    // 1. Avvia il Tray immediatamente
                    Platforms.Windows.WindowsTrayService.Initialize();

                    var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                    var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
                    var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

                    // 2. QUESTA È LA PARTE CRUCIALE: Intercettiamo la chiusura nativa
                    appWindow.Closing += (s, args) =>
                    {
                        var settingsJson = Microsoft.Maui.Storage.Preferences.Default.Get("user_settings", "");

                        // Se lo switch è attivo
                        if (settingsJson.Contains("\"MinimizeToTrayOnClose\":true"))
                        {
                            args.Cancel = true; // ANNULLA la chiusura di Windows
                            appWindow.Hide();   // Nasconde solo la finestra
                        }
                        // Se lo switch è spento, args.Cancel rimane false e l'app muore normalmente
                    };

                    if (windowHandle != IntPtr.Zero)
                    {

                        if (appWindow != null)
                        {
                            var displayArea = Microsoft.UI.Windowing.DisplayArea.Primary;
                            if (displayArea != null)
                            {
                                // Calcolo centratura
                                int x = (displayArea.WorkArea.Width - (int)(width * 1.25)) / 2;
                                int y = (displayArea.WorkArea.Height - (int)(height * 1.25)) / 2;

                                appWindow.Move(new Windows.Graphics.PointInt32(x, y));
                            }
                        }
                    }
                }
            };
#endif
            return window;
        }
    }
}
