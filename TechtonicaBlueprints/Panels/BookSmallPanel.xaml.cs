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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechtonicaBlueprints.Classes;

namespace TechtonicaBlueprints.Panels
{
    /// <summary>
    /// Interaction logic for BookSmallPanel.xaml
    /// </summary>
    public partial class BookSmallPanel : UserControl
    {
        public BookSmallPanel() {
            InitializeComponent();
        }

        public BookSmallPanel(BlueprintBook bookToShow) {
            InitializeComponent();
            showBook(bookToShow);
        }

        public BookSmallPanel(int id) {
            InitializeComponent();
            showBook(BookManager.getBook(id));
        }

        // Objects & Variables
        public BlueprintBook book;

        // Events

        private void onOpenClicked(object sender, EventArgs e) {
            MainWindow.current.showBook(book);
        }

        // Public Functions

        public void showBook(BlueprintBook bookToShow) {
            book = bookToShow;
            nameLabel.Content = book.name;
        }
    }
}
