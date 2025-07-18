using Newtonsoft.Json;
using TextRPG.Utils.DataModel.Item;

namespace TextRPG.Utils.DataModel.Creature
{
    public class CharacterClassData : Creature
    {
        public string ClassName { get; set; }
        public int[] SkillsId { get; set; } // 스킬 ID 배열
    }
    public struct CharactersClassData
    {
        [JsonProperty("Data")]
        public List<CharacterClassData> _CharactersClassData { get; set; }
    }
    public class Character : Creature
    {
        public int? ArmorItemId { get; set; } // 장비 아이템 ID
        public int? WeaponItemId { get; set; } // 장비 아이템 ID
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int NeedExp { get; set; }
        public List<int> ItemsId { get; set; } = new List<int>(); // 아이템 ID 배열
        public int CharacterClassId { get; set; } // 캐릭터 클래스 ID
        [JsonIgnore]
        public Inventory Inventory { get; set; } = new Inventory();
        public event Action PlayerDied;
    }
    public struct PlayerCharacters
    {
        [JsonProperty("Characters")]
        public List<Character> _PlayerCharacters { get; set; }
    }
}