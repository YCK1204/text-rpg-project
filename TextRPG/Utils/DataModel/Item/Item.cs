using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Utils.DataModel.Item
{
    public interface IUsable
    {
        void Use();
        void Use<T>(T target) where T : GameObject;
    }
    public interface IEquippable
    {
        bool Equipped { get; set; }
        void Equip();
        void Unequip();
    }
    public class Potion : Item, IUsable
    {
        public int Heal { get; set; }
        public override void Display()
        {
            Console.WriteLine($"{Name.PadRight(10)} | 회복력: {Heal.ToString().PadRight(5)} | {Description}");
        }
        public void Use()
        {
            //Math.Clamp(Player.HP + Heal, 0, Player.MaxHP);
        }

        public void Use<T>(T target) where T : GameObject
        {
            // target 체력 회복
        }
    }
    public class Armor : Item, IEquippable
    {
        public int Defense { get; set; }
        public bool Equipped { get; set; }
        public override void Display()
        {
            Console.WriteLine($"{Name.PadRight(10)} | 방어력: {Defense.ToString().PadRight(5)} | {Description}");
        }

        public void Equip()
        {
            //Player.UnEquipItemArmor();
            //Player.ItemDefense += Defense;
            Equipped = true;
        }

        public void Unequip()
        {
            //Player.ItemDefense -= Defense;
            Equipped = false;
        }
    }
    public class Weapon : Item, IEquippable
    {
        public int Attack { get; set; }
        public bool Equipped { get; set; }
        public override void Display()
        {
            Console.WriteLine($"{Name.PadRight(10)} | 방어력: {Attack.ToString().PadRight(5)} | {Description}");
        }
        public void Equip()
        {
            //Player.UnEquipItemWeapon();
            //Player.ItemAttack += Attack;
            Equipped = true;
        }
        public void Unequip()
        {
            //Player.ItemAttack -= Attack;s
            Equipped = false;
        }
    }
    public abstract class Item : GameObject
    {
        public string Description { get; set; }
        public int Price { get; set; }
        public ItemRarity Rarity { get; set; }
        public abstract void Display();
    }
    public struct Items
    {
        [JsonProperty("Items")]
        public List<Item> _Items { get; set; }
    }
}
