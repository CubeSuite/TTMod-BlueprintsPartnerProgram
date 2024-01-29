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

namespace TechtonicaBlueprints.Windows
{
    /// <summary>
    /// Interaction logic for GetYesNoWindow.xaml
    /// </summary>
    public partial class GetYesNoWindow : Window
    {
        public GetYesNoWindow() {
            InitializeComponent();
        }

        // Events

        private void onYesClicked(object sender, EventArgs e) {
            result = true;
            Close();
        }

        private void onNoClicked(object sender, EventArgs e) {
            result = false;
            Close();
        }

        // Return Functions

        private bool result;
        private bool getResult() { return result; }
        public static bool getYesNo(string title, string description) {
            GetYesNoWindow window = new GetYesNoWindow();
            window.titleLabel.Content = title;
            window.descriptionLabel.Text = description;
            window.ShowDialog();
            return window.getResult();
        }
    }
}
