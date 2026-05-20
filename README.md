# 🎮 BetterArc (Arc Raiders Visual Optimizer)

**BetterArc** è un'applicazione nativa per Windows sviluppata in **.NET MAUI Blazor** e **Tailwind CSS**. È progettata specificamente per ottimizzare l'esperienza visiva in-game (ispirata a giochi come *Arc Raiders*), permettendo di regolare in tempo reale e a basso livello i parametri di **Luminosità, Gamma e Contrasto** del monitor tramite le API native di Windows (`SetDeviceGammaRamp`).

L'applicazione include anche un sistema intelligente di ottimizzazione automatica basato sulla scansione dello schermo.

---

## ✨ Funzionalità Principali

* **🚀 Modalità Gamma (Presets):** Cambia istantaneamente il profilo visivo del monitor tra diversi preset ottimizzati (*Normal*, *Bright*, *Brighter*).
* **🎛️ Configurazione Avanzata (Custom):** Regola finemente i valori di Luminosità, Gamma e Contrasto tramite slider reattivi o utilizzando la rotella del mouse sopra di essi per modifiche di precisione.
* **💾 Gestione Profili:** Salva le tue configurazioni personalizzate con un nome e ricaricale in qualsiasi momento dal menu a tendina.
* **🤖 Algoritmo Auto-Brightness Attivo:** Un sistema adattivo (*Adaptive Vision System*) che analizza lo schermo, rileva la percentuale di ombre in tempo reale e bilancia dinamicamente la luminosità e il contrasto per garantire la massima visibilità nelle zone scure.
* **⌨️ Hotkeys Globali:** Configura scorciatoie da tastiera (es. tasti del Numpad) per cambiare modalità o attivare l'auto-brightness al volo mentre sei in-game, grazie all'integrazione con *SharpHook*.
* **⚙️ Integrazione Windows:** Opzioni per avviare l'app automaticamente all'avvio di Windows e per minimizzarla direttamente nel System Tray (accanto all'orologio) quando si preme la `X` di chiusura.

---

## 🛠️ Stack Tecnologico

* **Framework UI:** .NET MAUI Blazor (.NET 8.0)
* **Interfaccia:** HTML5, Tailwind CSS (Design Scuro / Cyberpunk)
* **Interoperabilità Nativa (P/Invoke):** `gdi32.dll` e `user32.dll` per la manipolazione della rampa di gamma del monitor.
* **Gestione Input Globale:** SharpHook per intercettare le scorciatoie a livello di sistema operativo.

---

## 🚀 Come Scaricare ed Eseguire (.EXE Singolo)

Non è necessario installare Visual Studio o compilare il codice per usare l'applicazione. Viene fornito un eseguibile unico "Standalone":

1. Vai nella sezione **[Releases](https://github.com/IL_TUO_USER_GITHUB/IL_TUO_REPO/releases)** a destra di questa pagina.
2. Scarica l'ultimo file `.exe` disponibile (es. `BetterArc.exe`).
3. Fai doppio click sul file appena scaricato per avviare l'applicazione.

> ⚠️ **Nota su Windows SmartScreen:** Trattandosi di un software indipendente non firmato con un certificato commerciale costoso, Windows potrebbe mostrare un avviso al primo avvio. Clicca su *"Maggiori informazioni"* e poi su *"Esegui comunque"*.

---
