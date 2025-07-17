using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Utils;
using TextRPG.Utils.DataModel.Creature;

namespace TextRPG
{
    public class Player : Character
    {
        static Player instance { get; } = new Player();
        public static Player Instance { get { return instance; } }
        public void playerinfo()
        {
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Attack: {Attack}");
            Console.WriteLine($"Defense:{Defense}");
            Console.WriteLine($"Health: {HP}/{MaxHP}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Exp: {Exp}/{NeedExp}");
            Console.WriteLine("아무키나 입력시 시작화면으로 돌아갑니다.");
            Console.ReadKey();
        }
    }
}
