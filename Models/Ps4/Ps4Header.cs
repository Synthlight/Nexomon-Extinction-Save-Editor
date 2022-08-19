using System.IO;

namespace Save_Editor.Models.Ps4 {
    public class Ps4Header : NotifyPropertyChangedImpl {
        public char[]          prefix     { get; }
        public uint            unk1       { get; }
        public uint            unk2       { get; }
        public uint            unk3       { get; }
        public Ps4SlotHeader[] slotHeader { get; } // Count for this is between unk2 & unk3.
        public SaveData[]      saveData   { get; } // Must match the slot header size.
        public byte[][]        unkData    { get; } // For the 'UNK' slot type since it's not a save.

        public Ps4Header(BinaryReader reader) {
            prefix = reader.ReadChars(8);
            unk1   = reader.ReadUInt32();
            unk2   = reader.ReadUInt32();
            var slotSize = reader.ReadUInt32();
            unk3 = reader.ReadUInt32();

            slotHeader = new Ps4SlotHeader[slotSize];
            saveData   = new SaveData[slotSize];
            unkData    = new byte[slotSize][];

            for (var i = 0; i < slotSize; i++) {
                slotHeader[i] = reader.ReadPs4SlotHeader();
            }

            for (var i = 0; i < slotSize; i++) {
                reader.BaseStream.Seek(slotHeader[i].saveOffset, SeekOrigin.Begin);

                if (slotHeader[i].slotPosition == SlotPosition.Unk) {
                    unkData[i] = reader.ReadBytes((int) slotHeader[i].saveSize);
                } else {
                    saveData[i] = reader.ReadSaveData(true, slotHeader[i].saveSize);
                }
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

            for (var i = 0; i < ps4Header.slotHeader.Length; i++) {
                if (ps4Header.slotHeader[i].slotPosition == SlotPosition.Unk) {
                    writer.Write(new byte[] {0x46, 0x41, 0x4C, 0x4C, 0x45, 0x4E, 0x00, 0x00}); // FALLEN
                    writer.Write(ps4Header.unkData[i]);
                } else {
                    writer.Write(ps4Header.saveData[i]);
                }
            }
        }
    }
}