namespace Save_Editor.Models {
    public class Item : NotifyPropertyChangedImpl {
        public string Name => Data.ITEM_NAMES_BY_ID.TryGet(Id);
        public int    Id   { get; set; }

        public Item(int id) {
            Id = id;
        }

        public static implicit operator int(Item item)    => item.Id;
        public static implicit operator Item(int value)   => new Item(value);
        public override                 string ToString() => Name;
    }
}