
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace Progetto1
{
    public class ScreenScanner
    {
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int nIndex);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;

        public (double shadows, double midtones) GetDetailedBrightness()
        {
            try
            {
                // SM_CXSCREEN = 0, SM_CYSCREEN = 1
                int width = GetSystemMetrics(0);
                int height = GetSystemMetrics(1);

                if (width <= 0 || height <= 0) return (0.1, 0.4); // Valori di fallback

                using System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height);
                using System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                // Cattura lo schermo
                g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(width, height));

                var rect = new System.Drawing.Rectangle(0, 0, width, height);
                var data = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                int bytes = data.Stride * height;
                byte[] buffer = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(data.Scan0, buffer, 0, bytes);
                bitmap.UnlockBits(data);

                int shadowPixels = 0, midPixels = 0, total = 0;

                // Analizziamo il buffer (step di 400 per essere ultra-veloci e non bloccare la UI)
                for (int i = 0; i < buffer.Length - 4; i += 400)
                {
                    // BGRA: buffer[i] = Blu, [i+1] = Verde, [i+2] = Rosso
                    double lum = (0.2126 * buffer[i + 2] + 0.7152 * buffer[i + 1] + 0.0722 * buffer[i]) / 255.0;

                    if (lum < 0.15) shadowPixels++;
                    else if (lum < 0.50) midPixels++;

                    total++;
                }

                if (total == 0) return (0.1, 0.4);

                return ((double)shadowPixels / total, (double)midPixels / total);
            }
            catch (Exception ex)
            {
                // Se vedi 0% fisso, controlla la console di Visual Studio per questo errore
                System.Diagnostics.Debug.WriteLine($"SCANNER ERROR: {ex.Message}");
                return (0.1, 0.4); // Ritorna valori neutri invece di 0 per evitare il blackout
            }
        }
    }
}
