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
                Items.Remove(item);
        }
    }
}
