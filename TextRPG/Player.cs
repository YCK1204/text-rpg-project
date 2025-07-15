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
        public void GainExp(int amount)
        {
            Exp += amount; // 획득한 경험치를 현재 경험치에 더합니다.
            Console.WriteLine($"{amount} 경험치를 획득했습니다. 현재 경험치: {Exp}/{NeedExp}");

            // 현재 경험치가 다음 레벨업에 필요한 경험치보다 많거나 같으면 레벨업을 진행합니다.
            // 한 번에 여러 레벨을 올릴 수도 있으므로 while 루프를 사용합니다.
            while (Exp >= NeedExp)
            {
                Exp -= NeedExp; // 필요한 경험치만큼 차감합니다. (초과 경험치 유지)
                LevelUP();      // 레벨업 메서드를 호출합니다.
            }
        }





    }
}
