using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Utils.DataModel.Item
{
    public class Inventory
    {
        public List<Item> Items { get; set; }
        public void UseItem(int idx)
        {
            if (idx < 0 || idx > Items.Count) return;
            if (Items[idx - 1] is IUsable usableItem)
            {
                usableItem.Use();
                Items.RemoveAt(idx);
            }
            else if (Items[idx - 1] is IEquippable equippableItem)
            {
                equippableItem.Equip();
            }
            else;// 사실상 없는 경우
        }
        public void UnUseItem(int idx)
        {
            if (idx < 0 || idx > Items.Count) return;
            if (Items[idx - 1] is IEquippable equippableItem && equippableItem.Equipped)
            {
                equippableItem.Unequip(); // 장비 Item에서 Equipped 처리
            }
            else;// 사실상 없는 경우
        }
        public void DisplayInventory()
        {
            Console.WriteLine("------------------- Item List -------------------");
            Console.WriteLine(
@"0. 뒤로가기
1. 아이템 사용
2. 아이템 팔기");
            foreach (var item in Items)
                item.Display();
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    return; // 뒤로가기
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    // 사용할 아이템 입력
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    // 판매할 아이템 입력
                    break;
            }
        }
        public void UnEquipItemArmor()
        {
            if (Items.Count == 0) return;
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] is Armor armor && armor.Equipped)
                {
                    armor.Unequip();
                    Items[i] = armor;
                    break;
                }
            }
        }
        public void UnEquipItemWeapon()
        {
            if (Items.Count == 0) return;
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] is Weapon weapon && weapon.Equipped)
                {
                    weapon.Unequip();
                    Items[i] = weapon;
                    break;
                }
            }
        }
    }
}
