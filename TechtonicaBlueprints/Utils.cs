using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TechtonicaBlueprints
{
    public static class Utils
    {
        [STAThread]
        public static string getClipboardText() {
            return Clipboard.GetText();
        }

        [STAThread]
        public static void setClipboardText(string text) {
            Clipboard.SetText(text);
        }
    }
}
