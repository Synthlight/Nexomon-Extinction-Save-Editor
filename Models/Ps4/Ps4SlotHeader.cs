using System.IO;

namespace Save_Editor.Models.Ps4 {
    public class Ps4SlotHeader : NotifyPropertyChangedImpl {
        public SlotPosition slotPosition { get; }
        public uint         unk1         { get; }
        public uint         saveSize     { get; }
        public uint         saveOffset   { get; }

        public Ps4SlotHeader(BinaryReader reader) {
            slotPosition = (SlotPosition) reader.ReadUInt32();
            unk1         = reader.ReadUInt32();
            saveSize     = reader.ReadUInt32();
            saveOffset   = reader.ReadUInt32();
        }
    }

    public static partial class Extensions {
        public static Ps4SlotHeader ReadPs4SlotHeader(this BinaryReader reader) {
            return new Ps4SlotHeader(reader);
        }

        public static void Write(this BinaryWriter writer, Ps4SlotHeader ps4Header) {
            writer.Write((uint) ps4Header.slotPosition);
            writer.Write(ps4Header.unk1);
            writer.Write(ps4Header.saveSize);
            writer.Write(ps4Header.saveOffset);
        }
    }
}