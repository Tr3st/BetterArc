using System.Runtime.InteropServices;

namespace Progetto1
{
    internal class GammaService
    {
        public GammaService()
        {
            // Si aggancia all'evento di uscita del processo di Windows
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                ApplicaSettaggi(1.0, 1.0, 1.0, 1.0, 1.0);
            };
        }
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern bool SetDeviceGammaRamp(IntPtr hDC, ref RAMM_STRUCT lpRamp);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RAMM_STRUCT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Red;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Green;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Blue;
        }


        public void ApplicaSettaggi(double gamma, double luminosita, double contrasto, double nitidezza, double vibrance)
        {
            RAMM_STRUCT ramp = new RAMM_STRUCT();
            ramp.Red = new ushort[256];
            ramp.Green = new ushort[256];
            ramp.Blue = new ushort[256];

            for (int i = 0; i < 256; i++)
            {
                double step = i / 255.0;

                // 1. CONTRASTO (Base pulita)
                double c = (((step - 0.5) * contrasto) + 0.5);
                c = Math.Clamp(c, 0, 1);

                // 2. GAMMA (Curva di visibilità)
                double exponent = 1.0 / Math.Max(0.1, gamma);
                // Protezione bianchi (High-end roll-off)
                if (step > 0.8)
                {
                    double factor = (1.0 - step) / 0.2;
                    exponent = 1.0 + ((exponent - 1.0) * factor);
                }
                double baseVal = Math.Pow(c, exponent);

                // 3. LUMINOSITÀ LINEARE (L'effetto "BrightRaider")
                // Applichiamo la luminosità come moltiplicatore finale uniforme
                double finalR = baseVal * luminosita;
                double finalG = baseVal * luminosita;
                double finalB = baseVal * luminosita;

                // 4. VIBRANCE BILANCIATA (Niente più alone rosso)
                if (vibrance != 1.0)
                {
                    // Usiamo un incremento uniforme per non sballare il bilanciamento del bianco
                    double vFactor = (vibrance - 1.0) * 0.5;
                    finalR += vFactor * (finalR - (finalR * 0.3 + finalG * 0.59 + finalB * 0.11));
                    finalG += vFactor * (finalG - (finalR * 0.3 + finalG * 0.59 + finalB * 0.11));
                    finalB += vFactor * (finalB - (finalR * 0.3 + finalG * 0.59 + finalB * 0.11));
                }

                ramp.Red[i] = (ushort)(Math.Clamp(finalR, 0, 1) * 65535);
                ramp.Green[i] = (ushort)(Math.Clamp(finalG, 0, 1) * 65535);
                ramp.Blue[i] = (ushort)(Math.Clamp(finalB, 0, 1) * 65535);
            }

            IntPtr hdc = GetDC(IntPtr.Zero);
            SetDeviceGammaRamp(hdc, ref ramp);
            ReleaseDC(IntPtr.Zero, hdc); // Aggiungi questa riga per evitare memory leak!
        }
    }

    
}
