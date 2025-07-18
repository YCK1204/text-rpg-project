using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Utils.DataModel.Creature;

namespace TextRPG
{
    public class GameManager
    {
        //public Battle battle;
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine(
@"캐릭터 선택
1. 전사
2. 궁수
3. 마법사
4. 도적
5. 해적");
            try
            {
                if (int.TryParse(Console.ReadLine(), out int key))
                {
                    CharacterClassData data = Data.DataManager.Instance.CharacterClassData[key - 1];
                    if (key < 1 || key > Data.DataManager.Instance.CharacterClassData.Count)
                        throw new FormatException();
                    Player.Instance = new Player(data);
                    run();
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("다시 시도해주세요.");
                StartGame();
                return;
            }
        }
         void run()
        {
            var json = JsonConvert.SerializeObject(Player.Instance);
            Console.WriteLine("캐릭터 생성 완료: " + json);
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
            Console.WriteLine("2. 인벤토리 보기"); //
            Console.WriteLine("3. 전투 시작"); // 전투 화면을 불러옴
            Console.WriteLine("4. 게임 종료");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해 주세요:"); // 1~3까지 숫자 입력 시 맞는 화면이 출력됨
            string input = Console.ReadLine(); //유저가 번호를 입력하는 곳

            switch (input)
            {
                case "1":
                    Player.Instance.playerinfo(); // 플레이어의 정보를 보여주는 화면
                    break;
                case "2":
                    Player.Instance.ShowInventory(); // 플레이어의 인벤토리를 보여주는 화면
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("몬스터와 전투를 시작합니다!");
                    // 3번 입력시 전투 화면을 불러옴 플레이어vs몬스터

                    break;
                case "4":
                    Console.WriteLine("게임을 종료합니다.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해 주세요. 아무키나 다시 입력해주세요."); // 다시 메인메뉴로
                    ShowMainmenu();
                    break;
            }
        }
    }
}
