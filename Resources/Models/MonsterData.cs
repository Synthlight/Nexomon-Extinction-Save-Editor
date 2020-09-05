using System.Collections.Generic;

namespace Save_Editor.Resources.Models {
    public class MonsterData {
        public string                    element;
        public int                       rarity;
        public int                       evolves_at;
        public short                     evolves_to;
        public int                       hp;
        public int                       sta;
        public int                       atk;
        public int                       def;
        public int                       spd;
        public List<string>              foods;
        public Dictionary<int, int>      skill_tree; // level : skillId
        public Dictionary<string, float> body;
        public Dictionary<string, float> shadow;
    }
}