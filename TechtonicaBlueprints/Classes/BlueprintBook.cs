using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechtonicaBlueprints.Classes
{
    public class BlueprintBook
    {
        public int id = -1;
        public int parentId;
        public string name;
        public List<Slot> slots = new List<Slot>();

        // Public Functions

        public void addBlueprint(int id) {
            slots.Add(new Slot() { blueprintID = id });
        }

        public void addBlueprint(Blueprint blueprint) {
            addBlueprint(blueprint.id);
        }

        public void addBook(int id) {
            slots.Add(new Slot() { bookID = id });
        }

        public void addBook(BlueprintBook book) {
            addBook(book.id);
        }

        public void removeBlueprint(int id) {
            List<Slot> matchingSlots = slots.Where(thisSlot => thisSlot.blueprintID == id).ToList();
            if(matchingSlots.Count == 1) {
                slots.Remove(matchingSlots[0]);
            }
        }

        public void removeBook(int id) {
            List<Slot> matchingSlots = slots.Where(thisSlot => thisSlot.bookID == id).ToList();
            if(matchingSlots.Count == 1) {
                slots.Remove(matchingSlots[0]);
            }
        }

        public BlueprintBook getParent() {
            if (parentId != -1 && BookManager.doesBookExist(parentId)) {
                return BookManager.tryGetBook(parentId);
            }
            
            return null;
        }

        public string getPath() {
            List<string> names = new List<string>() { name };
            BlueprintBook currentBook = this;
            while(currentBook.parentId != -1) {
                BlueprintBook parent = currentBook.getParent();
                names.Insert(0, parent.name);
                currentBook = parent;
            }

            return string.Join(" > ", names);
        }
    }

    public class Slot {
        public int blueprintID = -1;
        public int bookID = -1;

        public SlotType getType() {
            if (blueprintID != -1) return SlotType.blueprint;
            else if (bookID != -1) return SlotType.book;
            else return SlotType.none;
        }
    }

    public enum SlotType {
        none,
        blueprint,
        book
    }
}
