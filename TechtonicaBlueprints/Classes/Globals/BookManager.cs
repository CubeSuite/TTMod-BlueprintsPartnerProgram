using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechtonicaBlueprints.Classes;

namespace TechtonicaBlueprints
{
    public static class BookManager
    {
        // Objects & Variables
        private static Dictionary<int, BlueprintBook> books = new Dictionary<int, BlueprintBook>();

        // Public Functions

        public static int addBook(BlueprintBook book, bool shouldSave = true) {
            if(book.id == -1) book.id = getNewBookID();
            books.Add(book.id, book);
            if (shouldSave) saveData();

            return book.id;
        }

        public static void updateBook(BlueprintBook book) {
            if (doesBookExist(book)) {
                books[book.id] = book;
                saveData();
            }
        }

        public static bool doesBookExist(int id) {
            return books.ContainsKey(id);
        }

        public static BlueprintBook getBook(int id) {
            if (doesBookExist(id)) return books[id];
            else return null;
        }

        public static List<BlueprintBook> getAllBooks() {
            return books.Values.ToList();
        }

        public static BlueprintBook getRootBook() {
            return books[0];
        }

        public static BlueprintBook getCurrentBook() {
            return books[ProgramData.currentBookID];
        }

        public static void deleteBook(BlueprintBook book) {
            if (!doesBookExist(book.id)) return;

            foreach(Slot slot in book.slots) {
                if (slot.getType() == SlotType.blueprint) BlueprintManager.deleteBlueprint(slot.blueprintID);
                else deleteBook(slot.bookID);
            }

            books.Remove(book.id);
        }

        // Private Functions

        private static int getNewBookID() {
            if (books.Count == 0) return 0;
            else return books.Keys.Max() + 1;
        }

        // Data Functions

        public static void saveData() {
            string json = JsonConvert.SerializeObject(getAllBooks());
            File.WriteAllText(ProgramData.Paths.booksFile, json);
        }

        public static void loadData() {
            if (!File.Exists(ProgramData.Paths.booksFile)) {
                addBook(ProgramData.rootBook);
                return;
            }

            string json = File.ReadAllText(ProgramData.Paths.booksFile);
            List<BlueprintBook> booksFromFile = JsonConvert.DeserializeObject<List<BlueprintBook>>(json);
            foreach (BlueprintBook book in booksFromFile) {
                addBook(book, false);
            }
        }

        #region Overloads

        public static bool doesBookExist(BlueprintBook book) {
            return books.ContainsKey(book.id);
        }

        public static void deleteBook(int id) {
            deleteBook(getBook(id));
        }

        #endregion
    }
}
