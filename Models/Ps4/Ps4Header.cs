using System.IO;

namespace Save_Editor.Models.Ps4 {
    public class Ps4Header : NotifyPropertyChangedImpl {
        public char[]          prefix     { get; }
        public uint            unk1       { get; }
        public uint            unk2       { get; }
        public uint            unk3       { get; }
        public Ps4SlotHeader[] slotHeader { get; } // Count for this is between unk2 & unk3.
        public SaveData[]      saveData   { get; } // Must match the slot header size.

        public Ps4Header(BinaryReader reader) {
            prefix = reader.ReadChars(8);
            unk1   = reader.ReadUInt32();
            unk2   = reader.ReadUInt32();
            var slotSize = reader.ReadUInt32();
            unk3 = reader.ReadUInt32();

            slotHeader = new Ps4SlotHeader[slotSize];
            saveData   = new SaveData[slotSize];

            for (var i = 0; i < slotSize; i++) {
                slotHeader[i] = reader.ReadPs4SlotHeader();
            }

            for (var i = 0; i < slotSize; i++) {
                reader.BaseStream.Seek(slotHeader[i].saveOffset, SeekOrigin.Begin);
                saveData[i] = reader.ReadSaveData(true, slotHeader[i].saveSize);
            }
        }
    }

    public static partial class Extensions {
        public static Ps4Header ReadPs4Header(this BinaryReader reader) {
            return new Ps4Header(reader);
        }

        public static void Write(this BinaryWriter writer, Ps4Header ps4Header) {
            writer.Write(ps4Header.prefix);
            writer.Write(ps4Header.unk1);
            writer.Write(ps4Header.unk2);
            writer.Write((uint) ps4Header.slotHeader.Length);
            writer.Write(ps4Header.unk3);

            foreach (var slotHeader in ps4Header.slotHeader) {
                writer.Write(slotHeader);
            }

            foreach (var saveData in ps4Header.saveData) {
                writer.Write(saveData);
            }
        }
    }
}