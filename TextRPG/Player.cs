using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Player
    {
        public int Level = 1;
        public string Name = "탐험가";
        public string Class = "모험가";
        public int Attack = 11;
        public int Defense = 12;
        public int Health = 100;
        public int MaxHealth = 100;
        public int Gold = 500;
        public int Exp = 0;
        public int NeedExp = 0;

        public Player() 
        {
            MaxHealth = 100;
            NeedLevelUPExp();
        } 
        public void playerinfo()
        {
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Attack: {Attack}");
            Console.WriteLine($"Defense:{Defense}");
            Console.WriteLine($"Health: {Health}/{MaxHealth}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Exp: {Exp}/{NeedExp}");
            Console.WriteLine("아무키나 입력시 시작화면으로 돌아갑니다.");
            Console.ReadKey();
        }
        private void LevelUP() // 레벨업 시 능력치를 증가시키는 메서드
        {
            Level++; // 레벨업
            Attack += 2; // 공격력 2 증가
            Defense += 3; // 방어력 3씩 증가
            MaxHealth += 20; // 최대 체력 20씩 증가
            Health = MaxHealth; // 레벨업 시 체력이 최대치로 회복됨
            NeedLevelUPExp();
        }
        private void NeedLevelUPExp()
        {
            NeedExp = Level * 500;
        }
        


        

    }
}
