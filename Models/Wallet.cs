using System.IO;

namespace Save_Editor.Models {
    public class Wallet : NotifyPropertyChangedImpl {
        public int coins    { get; set; }
        public int diamonds { get; set; }
        public int tokens   { get; set; }
    }

    public static partial class Extensions {
        public static Wallet ReadWallet(this BinaryReader reader) {
            var wallet = new Wallet {
                coins    = reader.ReadInt32(),
                diamonds = reader.ReadInt32(),
                tokens   = reader.ReadInt32()
            };

            return wallet;
        }

        public static void Write(this BinaryWriter writer, Wallet wallet) {
            writer.Write(wallet.coins);
            writer.Write(wallet.diamonds);
            writer.Write(wallet.tokens);
        }
    }
}