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
        public Battle battle;
        public void run()
        {
            player = new Player();
            //battle = new Battle(order);
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
            Console.WriteLine("1. 상태 보기"); // 1 입력시 Player 스크립트에 입력된 상태 호출
            Console.WriteLine("2. 인벤토리 보기"); // 2 입력시 인벤토리 화면을 불러옴
            Console.WriteLine("3. 던전 입장"); // 3 입력시 전투 시작 화면을 불러옴
            Console.WriteLine("4. 게임 종료");// 4 입력시 게임을 종료시킴
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해 주세요:"); // 1~3까지 숫자 입력 시 맞는 화면이 출력됨
            string input = Console.ReadLine(); //유저가 번호를 입력하는 곳

            switch (input)
            {
                case "1":
                    player.playerinfo(); // 플레이어의 정보를 보여주는 화면
                    // 현재 플레이어의 레벨 이름 능력치, 경험치 등을 보여준다.
                    break;
                case "2":
                    break;
                    //플레이어의 인벤토리를 보여줍니다.
                    //인벤토리는 아직 완성되지 않음
                case "3":
                    Console.Clear();
                    Console.WriteLine("던전에 입장하였습니다."); // 3번 입력시 전투 화면을 불러옴 플레이어vs몬스터
                    Console.WriteLine("몬스터와 전투에서 승리하세요.");// 전투 시작 화면을 알려주세요!
                    Console.WriteLine(); // 띄어쓰기
                    Console.WriteLine("몬스터가 등장하였습니다!"); // 추가 메시지
                    Console.WriteLine(); // 띄어쓰기
                    // 이 후로는 배틀 스크립트에서 불러오는 내용이 출력되어야함 전투시작 몬스터 호출 등등
                    //battle.Startbattle(player.monster)
                    break;
                case "4":
                    Console.WriteLine("게임을 종료합니다.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해 주세요. 아무키나 다시 입력해주세요.");
                    Console.ReadKey(); // 1,2,3,4 외에 다른 숫자 입력시 아무키나 입력하면 시작화면으로 돌아감
                    break;


            }
        }
    }
}
