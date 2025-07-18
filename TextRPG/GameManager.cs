using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Data;
using TextRPG.Utils.DataModel.Creature;

namespace TextRPG
{
    public class GameManager
    {
        //public Battle battle;
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("1. 캐릭터 생성");
            Console.WriteLine("2. 기존 캐릭터 선택");
            if (int.TryParse(Console.ReadLine(), out int key))
            {
                if (key == 1)
                {
                    CreateCharacter();
                }
                else if (key == 2)
                {
                    SelectCharacter();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Console.ReadKey(true);
                    StartGame();
                }
            }
            run();
        }
        void CreateCharacter()
        {
            Console.WriteLine(
@"캐릭터 선택
1. 전사
2. 궁수
3. 마법사
4. 도적
5. 해적
0. 뒤로가기");
            try
            {
                if (int.TryParse(Console.ReadLine(), out int key))
                {
                    if (key == 0)
                    {
                        StartGame();
                        return;
                    }
                    CharacterClassData data = Data.DataManager.Instance.CharacterClassData[key - 1];
                    if (key < 0 || key > Data.DataManager.Instance.CharacterClassData.Count)
                        throw new FormatException();
                    string name = SetCharacterName();
                    data.Name = name;
                    Player.Instance = new Player(data);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("다시 시도해주세요.");
                StartGame();
                return;
            }
        }
        string SetCharacterName()
        {
            Console.Clear();
            Console.Write("캐릭터 이름을 입력해주세요: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("이름은 비워둘 수 없습니다. 다시 입력해주세요.");
                return SetCharacterName();
            }
            return name;
        }
        void SelectCharacter()
        {
            if (DataManager.Instance.PlayerCharacters.Count == 0)
            {
                Console.WriteLine("저장된 캐릭터가 없습니다. 캐릭터를 생성해주세요.");
                Console.ReadKey(true);
                StartGame();
                return;
            }

            Console.WriteLine("-----------저장된 캐릭터 목록---------");
            var list = DataManager.Instance.PlayerCharacters.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var character = list[i].Value;
                Console.WriteLine($"{i + 1}. {character.Name} - {DataManager.Instance.CharacterClassData[character.CharacterClassId].ClassName}");
            }
            Console.WriteLine("0. 뒤로가기");
            try
            {
                if (int.TryParse(Console.ReadLine(), out int key))
                {
                    if (key == 0)
                    {
                        StartGame();
                        return;
                    }
                    var data = Data.DataManager.Instance.PlayerCharacters[key - 1];
                    if (key < 0 || key > Data.DataManager.Instance.CharacterClassData.Count)
                        throw new FormatException();
                    Player.Instance = new Player(data);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("다시 시도해주세요.");
                SelectCharacter();
                return;
            }
        }
        void run()
        {
            var json = JsonConvert.SerializeObject(Player.Instance);
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
            Console.WriteLine("0. 게임 저장");
            Console.WriteLine("1. 상태 보기"); // 1 입력시 Player 스크립트에 입력된 상태 호출
            Console.WriteLine("2. 인벤토리 보기"); //
            Console.WriteLine("3. 전투 시작"); // 전투 화면을 불러옴
            Console.WriteLine("4. 게임 종료");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해 주세요:"); // 1~3까지 숫자 입력 시 맞는 화면이 출력됨
            string input = Console.ReadLine(); //유저가 번호를 입력하는 곳

            switch (input)
            {
                case "0":
                    DataManager.Instance.SaveData();
                    break;
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

                    List<Monster> battlefield = new List<Monster>();
                    Random rand2 = new Random();
                    int y = rand2.Next(1, 5);
                    //몬스터가 1~4마리까지 필요하고 반복문을 적용한다.
                    //랜덤값을 부여하고 이걸 토대로 랜덤값을 먼저 부여하고 그 다음 반복문으로 몬스터가 생성될지 말지를 정한다.
                    // 랜덤에서 나온 값 만큼 몬스터 생성을 반복한다.
                    for (int i = 0; i < y; i++)
                    {
                        Random rand = new();
                        int x = rand.Next(1, 4);
                        battlefield.Add(DataManager.Instance.Monsters[x].DeepClone());//깊은 복사 얇은 복사 이 키워드가 문제를 해결하는 힌트
                                                                                      //(DataManager.Instance.Monsters[x])  이거에 대한 복사본을 만들어 클래스로 만든객체가 통일화되는걸 막는다.(깊은복사검색)           
                    }

                    //여기다가 몬스터를 추가해야 한다.
                    //몬스터가 몇마리가 나오는지 1~4마리까지 그럼 배열을 1,5? 0,4? 해야하나
                    // 몬스터가 중복해서 나오는지 확인하기. 객체만 같다면 이름은 같아도 상관 없음
                    // 인스턴스 몬스터에서 어떻게 가져와서 넣을지 


                    //x라는 숫자에 랜덤을 부여하기 이게 배열값이 된다.
                    Battle battle = new Battle(battlefield); //Battle클래스 battle변수이름 = new새 객체 Battle객체(battlefield)함수;
                    battle.GamePlay();

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
