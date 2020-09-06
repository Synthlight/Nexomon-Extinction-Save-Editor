using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Save_Editor.Models;
using Save_Editor.Resources;

namespace Save_Editor {
    public partial class MainWindow {
        private const string SAVE_FILE_FILTER = "Windows (DAT)|*.dat|Switch (slot-*)|slot-*";

        public SaveData saveData { get; private set; }
        public string   targetFile;

        public MainWindow() {
            if (!LoadFile()) {
                Application.Current.Shutdown();
                return;
            }

            InitializeComponent();

            SetupAppWideBinding(new KeyGesture(Key.S, ModifierKeys.Control), SaveFile); // Ctrl+S.

#if DEBUG
            SetupAppWideBinding(new KeyGesture(Key.D, ModifierKeys.Control), SetTo80); // Ctrl+D.
            SetupAppWideBinding(new KeyGesture(Key.J, ModifierKeys.Control), SetCores); // Ctrl+J.
            SetupAppWideBinding(new KeyGesture(Key.L, ModifierKeys.Control | ModifierKeys.Alt), Resort); // Ctrl+Alt+L.
#endif
        }

        private bool LoadFile() {
            var target = GetOpenTarget();
            if (string.IsNullOrEmpty(target)) {
                Application.Current.Shutdown();
                return false;
            }

            targetFile = target;

            using var reader = new BinaryReader(File.Open(target, FileMode.Open, FileAccess.Read, FileShare.Read));

            saveData = reader.ReadSaveData();

            return true;
        }

        private void SaveFile() {
            var target = GetSaveTarget();
            if (string.IsNullOrEmpty(target)) return;

            using var writer = new BinaryWriter(File.Open(target, FileMode.Create, FileAccess.Write, FileShare.Read));

            writer.Write(saveData);
        }

        private string GetOpenTarget() {
            var ofdResult = new OpenFileDialog {
                Filter           = SAVE_FILE_FILTER,
                Multiselect      = false,
                InitialDirectory = Path.GetDirectoryName(targetFile) ?? string.Empty
            };
            ofdResult.ShowDialog();

            return ofdResult.FileName;
        }

        private string GetSaveTarget() {
            var sfdResult = new SaveFileDialog {
                Filter           = SAVE_FILE_FILTER,
                FileName         = $"{Path.GetFileNameWithoutExtension(targetFile)}",
                AddExtension     = true,
                InitialDirectory = Path.GetDirectoryName(targetFile) ?? string.Empty
            };
            return sfdResult.ShowDialog() == true ? sfdResult.FileName : null;
        }

        private void SetupAppWideBinding(KeyGesture keyGesture, Action onPress) {
            // Setup App-wide Binding.
            var command = new RoutedCommand();
            var ib      = new InputBinding(command, keyGesture);
            InputBindings.Add(ib);
            // Bind handler.
            var cb = new CommandBinding(command);
            cb.Executed += (sender, args) => onPress.Invoke();
            CommandBindings.Add(cb);
        }

#if DEBUG
        private void SetTo80() {
            foreach (var item in saveData.items) {
                if (item.Quantity >= 50 && item.Quantity < 80) item.Quantity = 80;
            }
        }

        private void SetCores() {
            foreach (var monster in saveData.party) {
                foreach (var core in monster.cores) {
                    core.Id = ItemConst.Destruction_Core_III_;
                }

                var toAdd = 4 - monster.cores.Count;
                for (var i = 0; i < toAdd; i++) {
                    monster.cores.Add(ItemConst.Destruction_Core_III_);
                }

                monster.harmony = 100;
            }

            foreach (var box in saveData.storage) {
                foreach (var monster in box.slots) {
                    if (monster == null) continue;
                    monster.harmony = 100;
                }
            }

            // Uncomment if you want to set the cores in storage too. Game doesn't normally allow this though.
            //foreach (var box in saveData.storage) {
            //    foreach (var monster in box.slots) {
            //        if (monster == null) continue;

            //        foreach (var core in monster.cores) {
            //            core.Id = 1161;
            //        }

            //        var toAdd = 4 - monster.cores.Count;
            //        for (var i = 0; i < toAdd; i++) {
            //            monster.cores.Add(1161);
            //        }
            //    }
            //}
        }

        private void Resort() {
            using var monsterLists = GetAllMonsters()
                                     .OrderBy(monster => monster.monsterId)
                                     .ChunkIntoToLists(Box.SIZE)
                                     .GetEnumerator();

            foreach (var box in saveData.storage) {
                int remainder;

                // if we run out of monsters, we need to null the whole box.
                if (!monsterLists.MoveNext()) {
                    remainder = Box.SIZE;
                    goto nullTheRemainder;
                }

                var monsterList = monsterLists.Current;

                for (var m = 0; m < monsterList!.Count; m++) {
                    box.slots[m] = monsterList[m];
                }

                remainder = Box.SIZE - monsterList!.Count; // If there were 57 in the list, remainder is 3 (60 - 57).

                if (remainder == 0) continue;

                nullTheRemainder:
                for (var i = Box.SIZE - remainder; i < Box.SIZE; i++) { // Start at 57 and to to 60.
                    box.slots[i] = null;
                }

                box.OnPropertyChanged(nameof(box.slots));
            }
        }
#endif

        private IEnumerable<Monster> GetAllMonsters() {
            return from box in saveData.storage
                   from monster in box.slots
                   where monster != null
                   select monster;
        }
    }
}