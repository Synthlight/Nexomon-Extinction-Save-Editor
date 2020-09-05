using System.IO;

namespace Save_Editor.Models {
    public class InventoryItem : NotifyPropertyChangedBase {
        public int Id       { get; set; }
        public int Quantity { get; set; }
    }

    public static partial class Extensions {
        public static InventoryItem ReadItem(this BinaryReader reader) {
            var inventoryItem = new InventoryItem();

            inventoryItem.Id       = reader.ReadInt32();
            inventoryItem.Quantity = reader.ReadInt32();

            return inventoryItem;
        }

        public static void Write(this BinaryWriter writer, InventoryItem item) {
            writer.Write(item.Id);
            writer.Write(item.Quantity);
        }
    }
}