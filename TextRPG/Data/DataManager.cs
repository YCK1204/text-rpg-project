using Newtonsoft.Json;
using System.Reflection;

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

        public Dictionary<int, TextRPG.Utils.Monster> Monsters = new Dictionary<int, TextRPG.Utils.Monster>();
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
            Monsters = MakeDict<TextRPG.Utils.Monsters, TextRPG.Utils.Monster>($"{JsonPath}/{MonsterPath}", (t1) => 
            {
                for (int i = 0; i < t1._Monsters.Count; i++)
                {
                    var monster = t1._Monsters[i];
                    var status = monster.Status;
                    status.Level = random.Next(monster.Status.RandomLevelRange[0], monster.Status.RandomLevelRange[1] + 1);
                    status.Health += (status.Level * 10);
                    status.Attack += (status.Level * 3);
                    status.DropGold += (status.Level * 10);
                    status.DropExp += (status.Level * 10);
                    monster.Status = status;
                    t1._Monsters[i] = monster;
                }
                return t1._Monsters.ToDictionary(m => m.Id, m => m);
            });
            foreach (var monster in Monsters)
            {
                Console.WriteLine($"Monster ID: {monster.Key}, Name: {monster.Value.Status.Name}, Health: {monster.Value.Status.Health}");
            }
        }
        public void SaveData()
        {

        }
    }
}
