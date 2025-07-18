using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Utils.DataModel.Item
{
    public class Inventory
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public void RemoveItem(Item item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
            else
            {
                Console.WriteLine("아이템이 인벤토리에 없습니다.");
            }
        }
    }
}
