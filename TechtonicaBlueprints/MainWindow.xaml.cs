using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using TechtonicaBlueprints.Classes;
using TechtonicaBlueprints.Panels;

namespace TechtonicaBlueprints
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();
        }

        // Objects & Variables
        public static MainWindow current => (MainWindow)System.Windows.Application.Current.MainWindow;
        public static DispatcherTimer commandTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(100) };

        // Events

        private void onProgramLoaded(object sender, RoutedEventArgs e) {
            ProgramData.Paths.createFolderStructure();
            loadData();
            showBook(BookManager.getRootBook());

            commandTimer.Tick += commandTimerTick;
            commandTimer.Start();
        }

        private void commandTimerTick(object sender, EventArgs e) {
            if (File.Exists(ProgramData.Paths.showFile)) {
                File.Delete(ProgramData.Paths.showFile);
                Show();
            }
            else if (File.Exists(ProgramData.Paths.hideFile)) {
                File.Delete(ProgramData.Paths.hideFile);
                Hide();
            }

            Process[] techtonicaProcess = Process.GetProcessesByName("Techtonica");
            if(techtonicaProcess.Length == 0) {
                Close();
            }
        }

        private void onProgramClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            saveData();
        }

        private void onNewBlueprintClicked(object sender, EventArgs e) {
            if (!File.Exists(ProgramData.Paths.currentBlueprintFile)) {
                return; // ToDo: Warn
            }
            
            string name = GetStringWindow.getString("Enter Name:", "New Blueprint Name...");
            if (name == GetStringWindow.canceledResult) return;

            string json = File.ReadAllText(ProgramData.Paths.currentBlueprintFile);
            Blueprint blueprint = JsonConvert.DeserializeObject<Blueprint>(json);
            blueprint.name = name;
            BlueprintManager.addBlueprint(blueprint);

            BlueprintBook currentBook = BookManager.getCurrentBook();
            currentBook.addBlueprint(blueprint);
            BookManager.updateBook(currentBook);
            showBook(currentBook);
        }

        private void onNewBlueprintBookClicked(object sender, EventArgs e) {
            string name = GetStringWindow.getString("Enter Name:", "New Blueprint Name...");
            if (name == GetStringWindow.canceledResult) return;

            BlueprintBook newBook = new BlueprintBook() {
                parentId = ProgramData.currentBookID,
                name = name
            };
            BookManager.addBook(newBook);

            BlueprintBook currentBook = BookManager.getCurrentBook();
            currentBook.addBook(newBook);
            BookManager.updateBook(currentBook);

            showBook(newBook);
        }

        private void onImportBlueprintClicked(object sender, EventArgs e) {
            string json = Utils.getClipboardText();
            try {
                Blueprint blueprint = JsonConvert.DeserializeObject<Blueprint>(json);
                BlueprintManager.addBlueprint(blueprint);

                BlueprintBook currentBook = BookManager.getCurrentBook();
                currentBook.addBlueprint(blueprint);
                BookManager.updateBook(currentBook);
                showBook(ProgramData.currentBookID);
            }
            catch {
                MessageBox.Show("Blueprint JSON parsing failed");
            }
        }

        private void onImportBlueprintBookClicked(object sender, EventArgs e) {
            string json = Utils.getClipboardText();
            try {
                BlueprintBook book = JsonConvert.DeserializeObject<BlueprintBook>(json);
                int newBookID = BookManager.addBook(book);
                
                BlueprintBook currentBook = BookManager.getCurrentBook();
                currentBook.addBook(book);
                BookManager.updateBook(currentBook);
             
                showBook(newBookID);
            }
            catch {
                MessageBox.Show("Blueprint Book JSON parsing failed");
            }
        }

        private void onCloseClicked(object sender, EventArgs e) {
            Hide();
        }

        // Public Functions

        public void showBook(int id) {
            showBook(BookManager.getBook(id));
        }

        public void showBook(BlueprintBook book) {
            ProgramData.currentBookID = book.id;
            blueprintBookBorder.Child = new BookPanel(book);
        }

        // Data Functions

        public void saveData() {
            BlueprintManager.saveData();
            BookManager.saveData();
        }

        public void loadData() {
            BlueprintManager.loadData();
            BookManager.loadData();
        }
    }
}
