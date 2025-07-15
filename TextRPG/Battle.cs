using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Battle
    {
        List<dynamic> order;
        int PlayerIndex;
        public Battle(List<dynamic> battlefield) // 전투 참여 객체 전부를 인자로 받음(플레이어, 몬스터 둘 다 포함)
        {
            this.order = battlefield;
            this.PlayerIndex = order.FindIndex( p => p == order.OfType<Player>().FirstOrDefault());
        }

        public void Combat(int active) // for이나 foreach문으로 order에서 하나씩 빼와서 반복 돌리시면 됩니다: 인터페이스 기초작업은 다 해뒀고 스킬 선택이랑 아이템 선택만 추가하시면 돼요.
        {
            if (active == PlayerIndex)
            {
                PrintCombatMain();
                switch (RPGsys.getInput())
                {
                    case "1":
                        PrintAttack();
                        int input;
                        bool success = int.TryParse(RPGsys.getInput(), out input);
                        if (success)
                        {

                            if (input < order.Count)
                            {
                                int damage = order[PlayerIndex].Attack();
                                Console.WriteLine();
                                Console.WriteLine($"{order[active].Name}의 공격!");
                                Console.WriteLine($"Lv.{order[input].Level} {order[input].Name}에게 {damage}데미지!.");
                                order[input].UpdateHealth(damage);
                                Console.WriteLine($"HP: {order[input].Health + damage} => {(order[input].Health <= 0 ? "Dead" : order[input].Health)}");
                                Console.WriteLine();
                                Console.WriteLine("0. 다음");

                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                        }
                            default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
            else
            {
                int damage = order[active].Attack();
                Console.WriteLine();
                Console.WriteLine($"{order[active].Name}의 공격!");
                Console.WriteLine($"{order[PlayerIndex].Name}에게 {damage}데미지!.");
                order[PlayerIndex].UpdateHealth(damage);
                Console.WriteLine($"HP: {order[PlayerIndex].Health + damage} => {(order[PlayerIndex].Health <= 0 ? "Dead" : order[PlayerIndex].Health)}");
            }
        }
        public void Reward()
        {
            int TotalGold = 0, TotalExp = 0;
            for (int i = 1; i < order.Count; i++)
            {
                TotalGold += order[i].DropGold;
                TotalExp += order[i].DropExp;
            }
            order[PlayerIndex].UpdateGold(TotalGold);
            order[PlayerIndex].UpdateExp(TotalExp);
        }
        public void PrintCombatMain()
        {
            foreach (var i in order)
            {
                if (i is Player)
                    continue;
                if (i.Health <= 0)
                { 
                    Console.ForegroundColor = ConsoleColor.Green;
                    RPGsys.ArrangePrint("Lv." + i.Level.ToString + " " + i.Name, 25);
                    Console.WriteLine($"| HP:{i.Health}");
                }
                else
                { 
                    Console.ForegroundColor = ConsoleColor.Gray;
                    RPGsys.ArrangePrint("Lv." + i.Level.ToString + " " + i.Name, 25);
                    Console.WriteLine("| Dead");
                }
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"[{order[PlayerIndex].Name}]");
            RPGsys.ArrangePrint($"Lv.{order[PlayerIndex].Level.ToString()}", 6);
            RPGsys.ArrangePrint($"|   {order[PlayerIndex].Name} ({order[PlayerIndex].Job})", 20);
            Console.WriteLine();
            Console.WriteLine($" HP:{order[PlayerIndex].CurrentHealth}/{order[PlayerIndex].Health}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.WriteLine("원하는 행동을 입력해주세요.");
        }
        public void PrintAttack()
        {
            foreach (var i in order)
            {
                if (i is Player)
                    continue;
                if (i.Health <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    RPGsys.ArrangePrint("Lv." + i.Level.ToString + " " + i.Name, 25);
                    Console.WriteLine($"| HP:{i.Health}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    RPGsys.ArrangePrint("Lv." + i.Level.ToString + " " + i.Name, 25);
                    Console.WriteLine("| Dead");
                }
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"[{order[PlayerIndex].Name}]");
            RPGsys.ArrangePrint($"Lv.{order[PlayerIndex].Level.ToString()}", 6);
            RPGsys.ArrangePrint($"|   {order[PlayerIndex].Name} ({order[PlayerIndex].Job})", 20);
            Console.WriteLine();
            Console.WriteLine($" HP:{order[PlayerIndex].CurrentHealth}/{order[PlayerIndex].Health}");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine("공격 대상을 선택해주세요.");
        }
        //public int SkillActivation()
        //{

        //}
        /*TODO:
         * 오더 흐름에 따라 턴제 채용
         * 플레이어 순서는 객체 스피드에 따라 차등 적용(constructor에서 입력)
         * 에너미 AI (추가기능 시 스킬 선택 추가) ** 차후 시간 남으면 추가.
         * 회피기동(?: spd 값 차에 따른 판정 변주)
         * 급소(?: LUK 값 또는 일정한 급소 값과 스킬로 인한 변동치에 따른 변주)
         * 
         *
         *
         *
         *
         */
        public int SpeedDice(object obj) // 속도값 판정: (보정 = 최소: +0 최대 +5)
        {
            Random random = new Random();
            return random.Next(0, 6) + obj.Speed; 
        }
        public List<dynamic> NewOrder() // 턴 시작 시 호출 요망: order 초기화 및 재설정
        {
            List<dynamic> newOrder = new List<dynamic>();
            Int[] speedList = new int[order.Count];
            for (int i = 0; i < order.Count; i++)
            {
                speedList.Add(SpeedDice(order[i]));
            }
            foreach (var i in order)
            {
                int index = speedList.IndexOf(speedList.Max());
                newOrder.Add(order[index]);
                speedList[index] = int.MinValue; // 이미 사용된 속도값은 최소치로 변경
            }
            return newOrder;
        }

    }

    internal class RPGsys
    {
        public static string getInput() // numpad = dpad 통일용 키입력 받는 메소드
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    return "0";
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    return "1";
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    return "2";
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    return "3";
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    return "4";
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    return "5";
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    return "6";
                case ConsoleKey.D7:
                case ConsoleKey.NumPad7:
                    return "7";
                case ConsoleKey.D8:
                case ConsoleKey.NumPad8:
                    return "8";
                case ConsoleKey.D9:
                case ConsoleKey.NumPad9:
                    return "9";
                default:
                    return keyInfo.KeyChar.ToString(); // fallback: 입력된 문자 자체
            }
        }

        public static void ArrangePrint(string text, int columnWidth) // 문자열 출력 길이 통합용 메소드
        {
            int visualLength = 0;

            foreach (char c in text)
            {
                if (char.IsDigit(c) || char.IsLetter(c) || char.IsPunctuation(c) || char.IsWhiteSpace(c))
                    visualLength += 1;
                else if (c >= '\uAC00' && c <= '\uD7AF') // 유니코드 한글 범위
                    visualLength += 2;
                else
                    visualLength += 1; // 기타 문자
            }

            int padding = columnWidth - visualLength;
            if (padding < 0) padding = 0;

            Console.Write(text);
            Console.Write(new string(' ', padding));
        }

    }
}
