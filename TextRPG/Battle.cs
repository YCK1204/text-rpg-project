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

        public 

        /*TODO:
         * 오더 흐름에 따라 턴제 채용
         * 플레이어 순서는 객체 스피드에 따라 차등 적용...가능하면.
         * 플레이어 id: 0 
         * 에너미 id: 1~4
         * 에너미 id는 위에서부터 하나하나.
         * 에너미 AI (추가기능 시 스킬 선택 추가) ** 차후 시간 남으면 추가.
         * 회피기동(+)
         * 급소(+)
         * 
         */





    }

    internal class RPGsys
    {
        public static string getInput()  // 숫자의 경우 기본 숫자 반환 | 이외의 경우 키 정보 그대로 반
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
                    return keyInfo.KeyChar.ToString();
            }
        }
    }
}
