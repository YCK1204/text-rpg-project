using Newtonsoft.Json;

namespace TextRPG.Utils
{
    #region Monster
    public struct MonsterStatus
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Level { get; set; }
        public int[] RandomLevelRange { get; set; }
        public void AttackPlayer()
        {
            Console.WriteLine($"{Name}이 플레이어를 공격하였습니다");
            //플레이어 체력-=미니언 공격
        }
        public void UpdateHealth(int damage)
        {
            this.Health -= damage;
        }
    }
    public struct MonsterReward
    {
        public int Gold { get; set; }
        public int Exp { get; set; }
        public LootType LootType { get; set; }
    }
    public struct Monster
    {
        public int Id { get; set; }
        public MonsterStatus Status { get; set; }
        public MonsterReward Reward { get; set; }
    }
    public struct MonsterLoot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LootType Type { get; set; }
        public int Price { get; set; }
    }
    public struct MonsterLoots
    {
        [JsonProperty("MonsterLoots")]
        public List<MonsterLoot> _MonsterLoots { get; set; }
    }
    public struct Monsters
    {
        [JsonProperty("Monsters")]
        public List<Monster> _Monsters { get; set; }
    }
    #endregion

    public struct CharacterStatus
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public CharacterClass Class { get; set; }
        public string ClassName { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
    }
    public struct CharacterStatuses
    {
        [JsonProperty("CharacterStatuses")]
        public List<CharacterStatus> _PlayerStatuses { get; set; }
    }
    public struct PlayerCharacter
    {
        public CharacterStatus Character { get; set; }
        public int Id { get; set; }
    }
    public struct PlayerCharacters
    {
        [JsonProperty("PlayerCharacters")]
        public List<PlayerCharacter> _PlayerCharacters { get; set; }
    }
}