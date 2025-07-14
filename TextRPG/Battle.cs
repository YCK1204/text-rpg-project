using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Battle
    {
        List<dynamic> order;
        public Battle(List<dynamic> order)
        { this.order = order; }

        public void Combat(int active)
        {
            if (this.order[active].GetType() == "Player")
            {
                /*
                * Player & Monster 클래스 공통:
                * int Damage(): 현재 공격력에 기반한 최종 데미지 반환
                * void UpdateHealth(int DeltaHealth): 특정 행동에 대한 체력 변경치 적용  
                */
                PrintCombatMain();
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine();
                Console.WriteLine("원하는 행동을 입력해주세요.");
                switch (RPGsys.getInput())
                {
                    case "1":
                        Console.WriteLine();
                        Console.WriteLine("0. 취소");
                        Console.WriteLine();
                        Console.WriteLine("공격 대상을 선택해주세요.");
                        int input;
                        bool success = int.TryParse(RPGsys.getInput(), out input);
                        if (success)
                        {

                            if (input < order.Count)
                            {
                                int damage = order[0].Attack();
                                Console.WriteLine();
                                Console.WriteLine($"{order[0].Name}의 공격!");
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
                Console.WriteLine($"{order[0].Name}에게 {damage}데미지!.");
                order[0].UpdateHealth(damage);
                Console.WriteLine($"HP: {order[0].Health + damage} => {(order[0].Health <= 0 ? "Dead" : order[0].Health)}");
            }
        }

        public void PrintCombatMain()
        {
            for (int i = 1; i < order.Count; i++)
            {
                if (order[i].Health <= 0)
                { 
                    Console.ForegroundColor = ConsoleColor.Green;
                    RPGsys.ArrangePrint("Lv." + order[i].Level.ToString + " " + order[i].Name, 25);
                    Console.WriteLine($"| HP:{order[i].Health}");
                }
                else
                { 
                    Console.ForegroundColor = ConsoleColor.Gray;
                    RPGsys.ArrangePrint("Lv." + order[i].Level.ToString + " " + order[i].Name, 25);
                    Console.WriteLine("| Dead");
                }
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"[{order[0].Name}]");
            RPGsys.ArrangePrint($"Lv.{order[0].Level.ToString()}", 6);
            RPGsys.ArrangePrint($"|   {order[0].Name} ({order[0].Job})", 20);
            Console.WriteLine();
            Console.WriteLine($" HP:{order[0].CurrentHealth}/{order[0].Health}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.WriteLine("원하는 행동을 입력해주세요.");
        }
        public void PrintAttack()
        {
            for (int i = 1; i < order.Count; i++)
            {
                if (order[i].Health <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    RPGsys.ArrangePrint("Lv." + order[i].Level.ToString + " " + order[i].Name, 25);
                    Console.WriteLine($"| HP:{order[i].Health}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    RPGsys.ArrangePrint("Lv." + order[i].Level.ToString + " " + order[i].Name, 25);
                    Console.WriteLine("| Dead");
                }
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"[{order[0].Name}]");
            RPGsys.ArrangePrint("Lv." + order[0].Level.ToString + " " + order[1].Name, 25);
            Console.WriteLine();
            Console.WriteLine($" HP:{order[0].CurrentHealth}/{order[0].Health}");
        }

        /*TODO:
         * 오더 흐름에 따라 턴제 채용
         * 플레이어 순서는 객체 스피드에 따라 차등 적용...가능하면.
         * 플레이어 id: 0 
         * 에너미 id (취소. id 대신 에너미 객체 자체를 끼워넣는다)
         * 에너미 id는 위에서부터 하나하나.
         * 에너미 AI (추가기능 시 스킬 선택 추가) ** 차후 시간 남으면 추가.
         * 회피기동(+)
         * 급소(+)
         * 
         */





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
