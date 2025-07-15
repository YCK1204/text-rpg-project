using Newtonsoft.Json;
using System.Reflection;
using TextRPG.Utils;

namespace TextRPG.Data
{
    public class DataManager
    {
        public static DataManager Instance = new DataManager();
        const string CommonPath = "../../../Data";
        const string JsonPath = CommonPath + "/Json";
        const string MonsterPath = "Monsters.json";
        const string ItemPath = "Items.json";
        const string PlayerPath = "Player.json";
        const string CharacterStatusPath = "CharacterStatuses.json";
        private DataManager() { }

        public Dictionary<int, Monster> Monsters = new Dictionary<int, Monster>();
        Dictionary<int, MonsterLoot> MonsterLoots = new Dictionary<int, MonsterLoot>();
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
            Random random = new Random();
            Monsters = MakeDict<Monsters, Monster>($"{JsonPath}/{MonsterPath}", (t1) =>
            {
                for (int i = 0; i < t1._Monsters.Count; i++)
                {
                    var monster = t1._Monsters[i];
                    var status = monster.Status;
                    status.Level = random.Next(monster.Status.RandomLevelRange[0], monster.Status.RandomLevelRange[1] + 1);
                    status.Health += (status.Level * 10);
                    status.Attack += (status.Level * 3);
                    monster.Status = status;
                    var reward = monster.Reward;
                    reward.Gold += (status.Level * 10);
                    reward.Exp += (status.Level * 10);
                    monster.Reward = reward;
                    t1._Monsters[i] = monster;
                }
                return t1._Monsters.ToDictionary(m => m.Id, m => m);
            });
            foreach (var monster in Monsters)
            {
                Console.WriteLine($"Monster ID: {monster.Key}, Name: {monster.Value.Status.Name}, Health: {monster.Value.Status.Health}");
            }
            MonsterLoots = MakeDict<MonsterLoots, MonsterLoot>($"{JsonPath}/{MonsterPath}", (t1) => { return t1._MonsterLoots.ToDictionary(m => m.Id, m => m); });
        }
        public MonsterLoot GenRandomLoot(LootType type)
        {
            List<MonsterLoot> filteredLoots = MonsterLoots.Values
                .Where(loot => loot.Type == type)
                .ToList();

            Random random = new Random();
            return filteredLoots[random.Next(0, filteredLoots.Count)];
        }
        public void SaveData()
        {

        }
    }
}
