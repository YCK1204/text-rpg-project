using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.Utils.DataModel.Item;

namespace TextRPG.Utils.DataModel.Creature
{
    public class MonsterReward : Reward
    {
        public ItemRarity ItemRarity { get; set; }
        public Item.Item Item { get; set; }
    }
    public class Monster : Creature
    {
        public int[] RandomLevelRange { get; set; }
        public MonsterReward Reward { get; set; }
        public event Action MonsterDied;
        public Monster DeepClone()
        {
            var json = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Monster>(json);
        }
    }
    public struct Monsters
    {
        [JsonProperty("Monsters")]
        public List<Monster> _Monsters { get; set; }
    }
}
