using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Save_Editor.Models {
    public class Box : NotifyPropertyChangedImpl {
        public const int SIZE = 60;

        public string    name { get; set; }
        public int       size1;
        public int       size2;
        public Monster[] slots { get; set; }

        // TODO: Replace with custom (derived) ObservableCollection class or something to manage this.
        public ObservableCollection<Monster> slotsAsMonsterList => new ObservableCollection<Monster>(slots.Where(slot => slot != null));
    }

    public static partial class Extensions {
        public static Box ReadBox(this BinaryReader reader, int saveVersion) {
            var box = new Box {
                name  = reader.ReadString(),
                size1 = reader.ReadInt32(),
                size2 = reader.ReadInt32()
            };

            box.slots = new Monster[box.size1];

            for (var i = 0; i < box.size1; i++) {
                if (reader.ReadBoolean()) box.slots[i] = reader.ReadMonster(saveVersion);
            }

            return box;
        }

        public static void Write(this BinaryWriter writer, Box box, int saveVersion) {
            writer.Write(box.name);
            writer.Write(box.size1);
            writer.Write(box.size2);

            for (var i = 0; i < box.size1; i++) {
                if (box.slots[i] == null) {
                    writer.Write(false);
                } else {
                    writer.Write(true);
                    writer.Write(box.slots[i], saveVersion);
                }
            }
        }
    }
}