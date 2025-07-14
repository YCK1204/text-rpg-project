using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace TextRPG
{
    public class Item
    {
        public string Name = "";
        public int Attack = 0;
        public int Defence = 0;
        public string Description = "";
        public int Price = 0;
        public ItemRarity Rarity { get; set; }
    }

    class Potion : Item, IUsable
    {
        public int Heal{  get; set; }
    }
    class Armor : Item
    {

    }

    class Weapon : Item
    {

    }
  
    public interface IUsable
    {
        public void Use()
        {

        }
    }
    public interface IEquippable
    {
        public void Equip()
        {

        }
    }


    public class ItemDataBase
    {
        public static List<Item> itemdatalist { get; set; }
        static ItemDataBase()
        {
            itemdatalist = new List<Item>();
            {
                itemdatalist.Add(new Potion
                {
                    Name = "HP 포션",
                    Description = "",
                    Price = 500,
                    Heal=20
                });
                itemdatalist.Add(new Potion
                {

                    Name = "MP 포션",
                    Description = "",
                    Price = 500,
                    Heal=20
                });
                itemdatalist.Add(new Armor
                {
                    Name = "수련자 갑옷",
                    Attack = 0,
                    Defence = 5,
                    Description = "수련에 도움을 주는 갑옷입니다",
                    Price = 1000,
                });

                itemdatalist.Add(new Armor
                {
                    Name = "무쇠갑옷",
                    Attack = 0,
                    Defence = 9,
                    Description = "무쇠로 만들어져 튼튼한 갑옷입니다. ",
                    Price = 1200,
                });
                itemdatalist.Add(new Armor
                {
                    Name = "스파르타의 갑옷",
                    Attack = 0,
                    Defence = 15,
                    Description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                    Price = 3500,
                });

                itemdatalist.Add(new Armor
                {
                    Name = "낡은 검",
                    Attack = 2,
                    Defence = 0,
                    Description = "쉽게 볼 수 있는 낡은 검 입니다.",
                    Price = 600,
                });
                itemdatalist.Add(new Weapon
                {
                    Name = "청동 도끼",
                    Attack = 5,
                    Defence = 0,
                    Description = " 어디선가 사용됐던거 같은 도끼입니다.",
                    Price = 1500,
                });
                itemdatalist.Add(new Weapon
                {
                    Name = "스파르타의 창",
                    Attack = 7,
                    Defence = 0,
                    Description = "스파르타의 전사들이 사용했다는 전설의 창입니다",
                    Price = 3500

                });

            }
        }
    }
}
    
    


