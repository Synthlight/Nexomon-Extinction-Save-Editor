using System.Collections.ObjectModel;
using System.IO;

namespace Save_Editor.Models {
    public class Monster : NotifyPropertyChangedImpl {
        public string                      Name            => Data.MONSTER_NAMES_BY_ID[monsterId];
        public short                       monsterId       { get; set; }
        public string                      nickname        { get; set; }
        public short                       level           { get; set; }
        public int                         EvolutionLevel  => Data.MONSTERS_BY_ID[monsterId].evolves_at;
        public string                      EvolutionTarget => Data.MONSTERS_BY_ID[monsterId].evolves_to > 0 ? Data.MONSTER_NAMES_BY_ID[Data.MONSTERS_BY_ID[monsterId].evolves_to] : "";
        public short                       hp              { get; set; }
        public short                       sta             { get; set; }
        public short                       exp             { get; set; }
        public ObservableCollection<Skill> skills          { get; } = new ObservableCollection<Skill>();
        public ObservableCollection<Item>  cores           { get; } = new ObservableCollection<Item>();
        public bool                        useGridAdapter  { get; set; }
        public string                      gridAdapter1    { get; set; }
        public int                         gridAdapter2    { get; set; }
        public int                         gridAdapter3    { get; set; }
        public bool                        gridAdapter4    { get; set; }
        public bool                        cosmic          { get; set; }
        public byte                        harmony         { get; set; }
    }

    public static partial class Extensions {
        public static Monster ReadMonster(this BinaryReader reader) {
            var monster = new Monster {
                monsterId = reader.ReadInt16()
            };

            if (reader.ReadBoolean()) monster.nickname = reader.ReadString();

            monster.level = reader.ReadInt16();
            monster.hp    = reader.ReadInt16();
            monster.sta   = reader.ReadInt16();
            monster.exp   = reader.ReadInt16();

            for (var b = reader.ReadByte(); b > 0; b--) {
                monster.skills.Add(reader.ReadInt32());
            }

            for (var b = reader.ReadByte(); b > 0; b--) {
                monster.cores.Add(reader.ReadInt32());
            }

            monster.useGridAdapter = reader.ReadBoolean();

            if (monster.useGridAdapter) {
                monster.gridAdapter1 = reader.ReadString();
                monster.gridAdapter2 = reader.ReadInt32();
                monster.gridAdapter3 = reader.ReadInt32();
                monster.gridAdapter4 = reader.ReadBoolean();
            }

            monster.cosmic  = reader.ReadBoolean();
            monster.harmony = reader.ReadByte();

            return monster;
        }

        public static void Write(this BinaryWriter writer, Monster monster) {
            writer.Write(monster.monsterId);

            if (string.IsNullOrEmpty(monster.nickname)) {
                writer.Write(false);
            } else {
                writer.Write(true);
                writer.Write(monster.nickname);
            }

            writer.Write(monster.level);
            writer.Write(monster.hp);
            writer.Write(monster.sta);
            writer.Write(monster.exp);

            writer.Write((byte) monster.skills.Count);
            foreach (var skill in monster.skills) {
                writer.Write(skill);
            }

            writer.Write((byte) monster.cores.Count);
            foreach (var code in monster.cores) {
                writer.Write(code);
            }

            writer.Write(monster.useGridAdapter);

            if (monster.useGridAdapter) {
                writer.Write(monster.gridAdapter1);
                writer.Write(monster.gridAdapter2);
                writer.Write(monster.gridAdapter3);
                writer.Write(monster.gridAdapter4);
            }

            writer.Write(monster.cosmic);
            writer.Write(monster.harmony);
        }
    }
}