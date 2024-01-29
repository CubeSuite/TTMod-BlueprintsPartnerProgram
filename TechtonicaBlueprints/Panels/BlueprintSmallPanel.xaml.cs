using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechtonicaBlueprints.Classes;
using TechtonicaBlueprints.Windows;

namespace TechtonicaBlueprints.Panels
{
    /// <summary>
    /// Interaction logic for BlueprintSmallPanel.xaml
    /// </summary>
    public partial class BlueprintSmallPanel : UserControl
    {
        public BlueprintSmallPanel() {
            InitializeComponent();
        }

        public BlueprintSmallPanel(Blueprint blueprintToShow) {
            InitializeComponent();
            showBlueprint(blueprintToShow);
        }

        public BlueprintSmallPanel(int id) {
            InitializeComponent();
            showBlueprint(BlueprintManager.getBlueprint(id));
        }

        // Objects & Variables

        public Blueprint blueprint;

        // Events

        private void onUseClicked(object sender, EventArgs e) {
            string json = JsonConvert.SerializeObject(blueprint);
            File.WriteAllText(ProgramData.Paths.currentBlueprintFile, json);
            MainWindow.current.Hide();
        }

        private void onExportClicked(object sender, EventArgs e) {
            Utils.setClipboardText(JsonConvert.SerializeObject(blueprint));
        }

        private void onDeleteClicked(object sender, EventArgs e) {
            if(GetYesNoWindow.getYesNo("Delete Blueprint?", "Are you sure you want to delete this blueprint? This cannot be undone")) {
                BlueprintManager.deleteBlueprint(blueprint);
                WrapPanel panel = (WrapPanel)Parent;
                panel.Children.Remove(this);
            }
        }

        // Public Functions

        public void showBlueprint(Blueprint blueprintToShow) {
            blueprint = blueprintToShow;
            nameLabel.Content = blueprint.name;
        }
    }
}
