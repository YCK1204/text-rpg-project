using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using TextRPG.Utils;
using TextRPG.Utils.DataModel.Item;
using TextRPG.Utils.DataModel.Creature;
using TextRPG.Utils.DataModel.Skill;

namespace TextRPG.Data
{
    public class DataManager
    {
        public static DataManager Instance = new DataManager();
        const string CommonPath = "../../../Data";
        const string JsonPath = CommonPath + "/Json";
        const string MonsterPath = "Monsters.json";
        const string ItemPath = "Items.json";
        const string SkillPath = "Skills.json";
        const string PlayerPath = "PlayerCharacters.json";
        const string CharacterClassDataPath = "CharacterClassData.json";
        private DataManager() { LoadData(); }

        public Dictionary<int, Monster> Monsters = new Dictionary<int, Monster>();
        Dictionary<int, Item> MonsterLoots = new Dictionary<int, Item>();
        public Dictionary<int, Item> Items = new Dictionary<int, Item>();
        public Dictionary<int, Skill> Skills = new Dictionary<int, Skill>();
        public Dictionary<int, CharacterClassData> CharacterClassData = new Dictionary<int, CharacterClassData>();
        public Dictionary<int, Character> PlayerCharacters = new Dictionary<int, Character>();
        Dictionary<int, T2> MakeDict<T1, T2>(string path, Func<T1, Dictionary<int, T2>> func)
        {
            string json = File.ReadAllText(path);
            T1 t1 = JsonConvert.DeserializeObject<T1>(json);
            try
            {
                if (t1 == null) { throw new Exception($"{path} 데이터가 비어있습니다."); }

                return func.Invoke(t1);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Environment.Exit(1);
                return null;
            }
        }
        public void LoadData()
        {
            #region Monster
            Random random = new Random();
            Monsters = MakeDict<Monsters, Monster>($"{JsonPath}/{MonsterPath}", (t1) =>
            {
                for (int i = 0; i < t1._Monsters.Count; i++)
                {
                    var monster = t1._Monsters[i];
                    monster.Level = random.Next(monster.RandomLevelRange[0], monster.RandomLevelRange[1] + 1);
                    monster.HP += (monster.Level * 5);
                    monster.MaxHP = monster.HP;
                    monster.Attack += (monster.Level);
                    monster.Reward.Gold += (monster.Level * 10);
                    monster.Reward.Exp += (monster.Level * 10);
                }
                return t1._Monsters.ToDictionary(m => m.Id, m => m);
            });
            #endregion

            #region Item
            string json = File.ReadAllText($"{JsonPath}/{ItemPath}");
            Items t1 = JsonConvert.DeserializeObject<Items>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            Items = t1._Items.ToDictionary(i => i.Id, i => i);
            #endregion

            #region Skills
            Skills = MakeDict<Skills, Skill>($"{JsonPath}/{SkillPath}", (t1) =>
            {
                return t1._Skills.ToDictionary(s => s.Id, s => s);
            });
            #endregion

            #region CharacterClassData
            CharacterClassData = MakeDict<CharactersClassData, CharacterClassData>($"{JsonPath}/{CharacterClassDataPath}", (t1) =>
            {
                return t1._CharactersClassData.ToDictionary(c => c.Id, c => c);
            });
            #endregion

            #region PlayerCharacters
            PlayerCharacters = MakeDict<PlayerCharacters, Character>($"{JsonPath}/{PlayerPath}", (t1) =>
            {
                return t1._PlayerCharacters.ToDictionary(c => c.Id, c => c);
            });
            #endregion
        }
        public Item GenRandomLoot(ItemRarity rarity)
        {
            List<Item> filteredLoots = MonsterLoots.Values
                .Where(loot => loot.Rarity == rarity)
                .ToList();

            Random random = new Random();
            return filteredLoots[random.Next(0, filteredLoots.Count)];
        }
        public void SaveData()
        {
            if (Player.Instance == null)
            {
                Console.Clear();
                Console.WriteLine("게임 플레이어가 설정되지 않았습니다.");
                return;
            }
            string json = $"{{\"Characters\":{JsonConvert.SerializeObject(PlayerCharacters.Values)}}}";
            File.WriteAllText($"{JsonPath}/{PlayerPath}", json);
            Console.Clear();
            Console.WriteLine("게임 데이터가 저장되었습니다.");
            Console.ReadKey(true);
        }
        public int GenerateLastId()
        {
            if (PlayerCharacters.Count == 0)
                return 0;
            return PlayerCharacters.Keys.Max() + 1;
        }
    }
}
