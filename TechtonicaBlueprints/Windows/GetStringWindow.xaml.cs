using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TechtonicaBlueprints
{
    /// <summary>
    /// Interaction logic for GetStringWindow.xaml
    /// </summary>
    public partial class GetStringWindow : Window
    {
        public GetStringWindow() {
            InitializeComponent();
        }

        // Events

        private void onConfirmClicked(object sender, EventArgs e) {
            result = inputBox.Input;
            Close();
        }

        private void onCancelClicked(object sender, EventArgs e) {
            result = canceledResult;
            Close();
        }

        // Return Functions

        public const string canceledResult = "InputWasCancelled";

        private string result;
        private string getResult() { return result; }
        public static string getString(string title, string hint) {
            GetStringWindow window = new GetStringWindow();
            window.titleLabel.Content = title;
            window.inputBox.Hint = hint;
            window.ShowDialog();
            return window.getResult();
        }
    }
}
