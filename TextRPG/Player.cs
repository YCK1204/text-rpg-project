using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Player
    {
        public int Level = 1;
        public string Name = "용사";
        public string Class = "모루의 용사";
        public int Attack = 11;
        public int Defense = 100;
        public int Health = 1000;
        public int Gold = 500;

        public void playerinfo()
        {
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Attack: {Attack}");
            Console.WriteLine($"Defense:{Defense}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Gold: {Gold}");
            Console.ReadKey();
        }

    }
}
