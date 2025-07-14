using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class GameManager
    {
        public Player player;

        public void run()
        {
            player = new Player();

            while (true)
            {
                ShowMainmenu();

            }

        }
        private void ShowMainmenu()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분을 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리 보기");
            Console.WriteLine("3. 상점 보기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해 주세요:");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    player.playerinfo();
                    break;
                case "2":
                    break;
                case "3":
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");

                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해 주세요. 아무키나 다시 입력해주세요.");
                    Console.ReadKey();
                    break;


            }
        }
    }
}
