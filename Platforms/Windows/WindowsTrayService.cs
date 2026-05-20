using System.Windows.Forms;
using System.Drawing;

namespace Progetto1.Platforms.Windows;

public static class WindowsTrayService
{
    private static NotifyIcon? _notifyIcon;

    public static void Initialize()
    {
        if (_notifyIcon != null) return;

        _notifyIcon = new NotifyIcon();

        try
        {
            // Cerchiamo il file .ico nella cartella di esecuzione
            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "trayicon.ico");

            if (File.Exists(iconPath))
            {
                // Caricamento diretto dell'icona
                _notifyIcon.Icon = new Icon(iconPath);
            }
            else
            {
                _notifyIcon.Icon = SystemIcons.Application;
                System.Diagnostics.Debug.WriteLine("File trayicon.ico non trovato, uso fallback.");
            }
        }
        catch (Exception ex)
        {
            _notifyIcon.Icon = SystemIcons.Application;
            System.Diagnostics.Debug.WriteLine($"Errore caricamento .ico: {ex.Message}");
        }

        _notifyIcon.Text = "BETTER-BRIGHT ARC";
        _notifyIcon.Visible = true;

        // Menu contestuale
        var menu = new ContextMenuStrip();
        menu.Items.Add("Apri BETTER-BRIGHT ARC", null, (s, e) => Restore());
        menu.Items.Add("Esci", null, (s, e) => {
            _notifyIcon.Dispose();
            Environment.Exit(0);
        });

        _notifyIcon.ContextMenuStrip = menu;
        _notifyIcon.DoubleClick += (s, e) => Restore();
    }

    public static void Restore()
    {
        var window = Microsoft.Maui.Controls.Application.Current?.Windows.FirstOrDefault();
        if (window?.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWin)
        {
            nativeWin.AppWindow.Show();
            nativeWin.Activate();
        }
    }
}