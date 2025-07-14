using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
    internal class MonsterLoot
    {
        public string Name {  get; set; }
        public ItemRarity Rarity { get; set; }
        public int Price { get; set; }

        List<MonsterLoot> loot=new List<MonsterLoot>();
        MonsterLoot()
        {
            loot.Add(new MonsterLoot()
            {
                Name = "평범한 몬스터 껍질",
                Rarity = ItemRarity.Common,
                Price = 10
            });

            loot.Add(new MonsterLoot()
            {
                Name = "평범한 몬스터 꼬리 ",
                Rarity = ItemRarity.Common,
                Price = 10
            });
            loot.Add(new MonsterLoot()
            {
                Name = "평범한 몬스터 발바닥",
                Rarity = ItemRarity.Common,
                Price = 10
            });
            loot.Add(new MonsterLoot()
            {
                Name = "평범한 몬스터 ",
                Rarity = ItemRarity.Common,
                Price = 10
            });
            loot.Add(new MonsterLoot()
            {
                Name = "",
                Rarity = ItemRarity.Rare,
            });
            loot.Add(new MonsterLoot()
            {
                Name = "",
                Rarity = ItemRarity.Rare,
            });
            loot.Add(new MonsterLoot()
            {
                Name = "",
                Rarity = ItemRarity.Rare,
            });
            loot.Add(new MonsterLoot()
            {
                Name = "",
                Rarity = ItemRarity.Epic,
            });
            loot.Add(new MonsterLoot()
            {
                Name = "",
                Rarity = ItemRarity.Epic,
            });
            loot.Add(new MonsterLoot()
            {
                Name = "전설의 엑스칼리버",
                Rarity = ItemRarity.Legendary,
            });
        }
    }
}
