using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Save_Editor.Models {
    public class SaveData : NotifyPropertyChangedBase {
        public int                                 magic;
        public string                              buildDate       { get; set; }
        public string                              buildTarget     { get; set; }
        public string                              buildVersion    { get; set; }
        public string                              saveDateUtc     { get; set; }
        public string                              playerName      { get; set; }
        public string                              playerBody      { get; set; }
        public string                              petBody         { get; set; }
        public int                                 playtimeSeconds { get; set; }
        public int                                 mapId           { get; set; }
        public int                                 playerX         { get; set; }
        public int                                 playerY         { get; set; }
        public string                              playerDirection { get; set; }
        public int                                 checkpointMapId { get; set; }
        public int                                 checkpointX     { get; set; }
        public int                                 checkpointY     { get; set; }
        public int                                 volumeBGM       { get; set; }
        public int                                 volumeSFX       { get; set; }
        public bool                                autoSaveEnabled { get; set; }
        public string                              languageId      { get; set; }
        public ObservableCollection<Monster>       party           { get; }      = new ObservableCollection<Monster>();
        public ObservableCollection<Box>           storage         { get; }      = new ObservableCollection<Box>();
        public ObservableCollection<InventoryItem> items           { get; }      = new ObservableCollection<InventoryItem>();
        public Wallet                              wallet          { get; set; } = new Wallet();

        public List<byte> remainderBytes;

        //public         BeatenTamers                    beatenTamers;
        //public         Mining                          mining;
        //public         Rematcher                       rematcher;
        //public         Achievements                    achievements;
        //public         PermanentlyDestroyedEntities    permanentlyKilledEntities;
        //private        Dictionary<int, bool>           switches;
        //private        Dictionary<int, List<string>>   permanentlyKilledFlags;
        //private        Dictionary<string, int>         variables;
        //public         HashSet<DatabaseMonsters.Entry> seenMonsters;
        //public         HashSet<DatabaseMonsters.Entry> ownedMonsters;
        //private static List<int>                       cadiumMapsWithZieglerMiasma;
    }

    public static partial class Extensions {
        public static SaveData ReadSaveData(this BinaryReader reader) {
            var saveData = new SaveData();

            saveData.magic           = reader.ReadInt32();
            saveData.buildDate       = reader.ReadString();
            saveData.buildTarget     = reader.ReadString();
            saveData.buildVersion    = reader.ReadString();
            saveData.saveDateUtc     = reader.ReadString();
            saveData.playerName      = reader.ReadString();
            saveData.playerBody      = reader.ReadString();
            saveData.playtimeSeconds = reader.ReadInt32();
            saveData.petBody         = reader.ReadString();
            saveData.mapId           = reader.ReadInt32();
            saveData.playerX         = reader.ReadInt32();
            saveData.playerY         = reader.ReadInt32();
            saveData.playerDirection = reader.ReadString();
            saveData.checkpointMapId = reader.ReadInt32();
            saveData.checkpointX     = reader.ReadInt32();
            saveData.checkpointY     = reader.ReadInt32();
            saveData.volumeBGM       = reader.ReadInt32();
            saveData.volumeSFX       = reader.ReadInt32();
            saveData.autoSaveEnabled = reader.ReadBoolean();
            saveData.languageId      = reader.ReadString();

            for (var i = reader.ReadInt32(); i > 0; i--) {
                saveData.party.Add(reader.ReadMonster());
            }

            for (var i = reader.ReadInt32(); i > 0; i--) {
                saveData.storage.Add(reader.ReadBox());
            }

            for (var i = reader.ReadInt32(); i > 0; i--) {
                saveData.items.Add(reader.ReadItem());
            }

            saveData.wallet = reader.ReadWallet();

            saveData.remainderBytes = reader.ReadRemainderAsByteArray();

            return saveData;
        }

        public static void Write(this BinaryWriter writer, SaveData saveData) {
            writer.Write(saveData.magic);
            writer.Write(saveData.buildDate);
            writer.Write(saveData.buildTarget);
            writer.Write(saveData.buildVersion);
            writer.Write(saveData.saveDateUtc);
            writer.Write(saveData.playerName);
            writer.Write(saveData.playerBody);
            writer.Write(saveData.playtimeSeconds);
            writer.Write(saveData.petBody);
            writer.Write(saveData.mapId);
            writer.Write(saveData.playerX);
            writer.Write(saveData.playerY);
            writer.Write(saveData.playerDirection);
            writer.Write(saveData.checkpointMapId);
            writer.Write(saveData.checkpointX);
            writer.Write(saveData.checkpointY);
            writer.Write(saveData.volumeBGM);
            writer.Write(saveData.volumeSFX);
            writer.Write(saveData.autoSaveEnabled);
            writer.Write(saveData.languageId);

            writer.Write(saveData.party.Count);
            foreach (var monster in saveData.party) {
                writer.Write(monster);
            }

            writer.Write(saveData.storage.Count);
            foreach (var box in saveData.storage) {
                writer.Write(box);
            }

            writer.Write(saveData.items.Count);
            foreach (var item in saveData.items) {
                writer.Write(item);
            }

            writer.Write(saveData.wallet);

            foreach (var b in saveData.remainderBytes) {
                writer.Write(b);
            }
        }
    }
}