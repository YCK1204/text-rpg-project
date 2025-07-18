using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Utils.DataModel.Creature;

namespace TextRPG.Utils.DataModel.Item
{
    public interface IUsable
    {
    }
    public interface IEquippable
    {
    }
    public abstract class Potion : Item, IUsable
    {
        public int Heal { get; set; }
    }
    public class HpPotion : Potion
    {
        public override void Display()
        {
            Console.WriteLine($"{Name.PadRight(20)} | 회복력: {Heal.ToString().PadRight(5)} | {Description}");
        }
    }
    public class MpPotion : Potion
    {
        public override void Display()
        {
            Console.WriteLine($"{Name.PadRight(20)} | 회복력: {Heal.ToString().PadRight(5)} | {Description}");
        }
    }
    public class Armor : Item, IEquippable
    {
        public int Defense { get; set; }
        public override void Display()
        {
            Console.WriteLine($"{Name.PadRight(20)} | 방어력: {Defense.ToString().PadRight(5)} | {Description}");
        }
    }
    public class Weapon : Item, IEquippable
    {
        public int Attack { get; set; }
        public override void Display()
        {
            Console.WriteLine($"{Name.PadRight(20)} | 공격력: {Attack.ToString().PadRight(5)} | {Description}");
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
