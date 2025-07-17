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
        public CharacterClass Class { get; set; }
        public string ClassName { get; set; }
        public int ItemDefense { get; set; }
        public int ItemAttack { get; set; }
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
