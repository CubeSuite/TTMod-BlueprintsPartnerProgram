using Newtonsoft.Json;
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
using TechtonicaBlueprints.Windows;

namespace TechtonicaBlueprints.Panels
{
    /// <summary>
    /// Interaction logic for BookPanel.xaml
    /// </summary>
    public partial class BookPanel : UserControl
    {
        public BookPanel() {
            InitializeComponent();
        }

        public BookPanel(BlueprintBook bookToShow) {
            InitializeComponent();
            showBook(bookToShow);
        }

        // Properties

        // Objects & Variables

        public BlueprintBook book;

        // Custom Events

        // Events

        private void onBackClicked(object sender, EventArgs e) {
            MainWindow.current.showBook(book.parentId);
        }

        private void onChangeNameClicked(object sender, EventArgs e) {
            string name = GetStringWindow.getString("Enter Name:", "New Blueprint Book Name...");
            if (name == GetStringWindow.canceledResult) return;

            book.name = name;
            BookManager.updateBook(book);
        }

        private void onExportBookClicked(object sender, EventArgs e) {
            Utils.setClipboardText(JsonConvert.SerializeObject(book));
        }

        private void onDeleteBookClicked(object sender, EventArgs e) {
            if(GetYesNoWindow.getYesNo("Delete Book?", "Are you sure you want to delete this book? All child books and blueprints will be deleted. This cannot be undone.")) {
                BookManager.deleteBook(book);
                MainWindow.current.showBook(0);
            }
        }

        // Public Functions

        public void showBook(BlueprintBook bookToShow) {
            book = bookToShow;
            if (book.id == 0) {
                buttonsGrid.Children.Remove(deleteButton);
                buttonsGrid.Children.Remove(backButton);
            }

            pathLabel.Content = book.getPath();

            slotsPanel.Children.Clear();
            foreach(Slot slot in book.slots) {
                if(slot.getType() == SlotType.book) {
                    slotsPanel.Children.Add(new BookSmallPanel(slot.bookID) { 
                        Margin = new Thickness(5),
                        MinWidth = 150
                    });
                }
                else {
                    slotsPanel.Children.Add(new BlueprintSmallPanel(slot.blueprintID) { 
                        Margin = new Thickness(5),
                        MinWidth = 150
                    });
                }
            }
        }
    }
}
