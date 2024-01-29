using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static int addSharableBook(SharableBook book, int parentId = -10) {
            BlueprintBook newBook = new BlueprintBook() { name = book.name };
            newBook.id = getNewBookID();
            if(parentId != -10) newBook.parentId = parentId;
            addBook(newBook);

            foreach (SharableSlot slot in book.slots) {
                if (slot.blueprint != null) {
                    slot.blueprint.id = -1;
                    slot.blueprint.parentID = newBook.id;
                    int id = BlueprintManager.addBlueprint(slot.blueprint);
                    newBook.addBlueprint(id);
                }
                else if (slot.blueprintBook != null) {
                    int childBookID = addSharableBook(slot.blueprintBook, newBook.id);
                    newBook.addBook(childBookID);
                }
            }

            updateBook(newBook);
            return newBook.id;
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

        public static BlueprintBook tryGetBook(int id) {
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

        public static void deleteBook(BlueprintBook book, bool removeFromParent = false) {
            if (book == null) return;
            if (!doesBookExist(book.id)) return;

            if (doesBookExist(book.parentId) && removeFromParent) {
                BlueprintBook parent = tryGetBook(book.parentId);
                parent.removeBook(book.id);
            }

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
            deleteBook(tryGetBook(id));
        }

        #endregion
    }
}
