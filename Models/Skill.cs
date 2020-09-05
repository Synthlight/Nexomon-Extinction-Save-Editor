namespace Save_Editor.Models {
    public class Skill : NotifyPropertyChangedBase {
        public string Name => Data.SKILL_NAMES_BY_ID.TryGet(Id);
        public int    Id   { get; set; }

        public Skill(int id) {
            Id = id;
        }

        public static implicit operator int(Skill skill)  => skill.Id;
        public static implicit operator Skill(int value)  => new Skill(value);
        public override                 string ToString() => Name;
    }
}