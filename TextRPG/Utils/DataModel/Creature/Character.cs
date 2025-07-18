using Newtonsoft.Json;
using TextRPG.Utils.DataModel.Item;

namespace TextRPG.Utils.DataModel.Creature
{
    public class CharacterClassData : Creature
    {
        public int? ArmorItemId { get; set; } // 장비 아이템 ID
        public int? WeaponItemId { get; set; } // 장비 아이템 ID
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
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int NeedExp { get; set; }
        public Inventory Inventory { get; set; }
        public event Action PlayerDied;
    }
    public struct Characters
    {
        [JsonProperty("Characters")]
        public List<Character> _Characters { get; set; }
    }
    public struct PlayerCharacters
    {
        [JsonProperty("PlayerCharacters")]
        public List<Character> _PlayerCharacters { get; set; }
    }
}
