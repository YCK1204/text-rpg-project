using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class Monster
    {
        public string Name { get; set; }
        public int Health {  get; set; }
        public int Attack {  get; set; }
        public int DropGold { get; set; }
        public int DropExp { get; set; }

        
    }
    public class MInion:Monster
    {
        MInion()
        {
            Name = "미니언";
            Health = 0;
            Attack = 0;
            DropGold = 0;
            DropExp= 0;
        }
    }
    public class CannonMinion:Monster
    {
        CannonMinion()
        {
            Name = "미니언";
            Health = 0;
            Attack = 0;
            DropGold = 0;
            DropExp = 0;
        }
    }
    public class VoidMinion : Monster
    {
        VoidMinion()
        {
            Name = "미니언";
            Health = 0;
            Attack = 0;
            DropGold = 0;
            DropExp = 0;
        }
    }
}
