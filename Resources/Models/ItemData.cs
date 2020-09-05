using System.Collections.Generic;
using Newtonsoft.Json;

namespace Save_Editor.Resources.Models {
    public class ItemData {
        public string category;
        // ReSharper disable once StringLiteralTypo
        [JsonProperty("effectvalues")]
        public List<int> effectValues;
        [JsonProperty("serialization")]
        public int itemId;
    }
}