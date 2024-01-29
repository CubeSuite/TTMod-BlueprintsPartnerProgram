using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechtonicaBlueprints.Panels;

namespace TechtonicaBlueprints.Classes
{
    public class SharableBook
    {
        public string name;
        public List<SharableSlot> slots = new List<SharableSlot>();

        public SharableBook(){}
        public SharableBook(BlueprintBook book) {
            name = book.name;
            foreach(Slot slot in book.slots) {
                slots.Add(new SharableSlot(slot));
            }
        }
    }

    public class SharableSlot {
        public Blueprint blueprint;
        public SharableBook blueprintBook;

        public SharableSlot(){}
        public SharableSlot(Slot slot) {
            blueprint = BlueprintManager.tryGetBlueprint(slot.blueprintID);
            BlueprintBook book = BookManager.tryGetBook(slot.bookID);
            if(book != null) blueprintBook = new SharableBook(book);
        }
    }
}
