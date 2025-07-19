using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Team_RPG;
using TextRPG.Data;
using TextRPG.Utils.DataModel.Creature;
using TextRPG.Utils.DataModel.Skill;

namespace TextRPG
{
    class Battle
    {
        List<Creature> order;
        List<Monster> EnemyList;
        Player player = Player.Instance;
        
        public Battle(List<Monster> battlefield) // 인자로 받는 건 전투에 참여하는 모든 몬스터들
        {
            // battlefield는 List<Creature> 타입으로 몬스터 객체들이 들어옴
            // 플레이어는 Player.Instance로 접근
            this.EnemyList = battlefield;
            this.order = new List<Creature>(battlefield);
            this.order.Add(player); // 플레이어를 전투 참여 객체에 추가
        }
        public void GamePlay() // 플레이어가 죽던가, 모든 적을 처치할 때까지 반복되는 메소드(전투 풀)
        {
            Console.WriteLine("당신은 어둑한 던전의 입구에 도착했다.");
            Console.WriteLine("희미한 횃불 아래, 알 수 없는 소리가 들려온다.");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("던전 깊은 곳을 조심스럽게 걷다가 뒤를 돌아보니");
            Console.WriteLine("바닥에서 꿈틀거리는 몬스터들이 모습을 드러낸다.");
            Console.WriteLine("몬스터들은 기괴한 소리를 내며 주위를 맴돈다.");
            Console.ReadKey(true);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("이곳에서 살아나가려면,");
            Console.WriteLine("이들을 모두 쓰러뜨려야 한다!");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine($"{EnemyList[0].Name}들을 만났다!");
            Console.WriteLine($"=================================================");
            order = NewOrder(); // 턴 순서 초기화
            while (true)
            {
                
                foreach (var creature in order)
                {
                    if (IsEnemiesDead()) // 플레이어만 남았을 때
                    {
                        Console.Clear();
                        Console.WriteLine("==============전투 승리!===================");
                        Console.WriteLine();
                        Console.WriteLine();
                        int[] rewards = Reward(); // 보상 지급 메소드 호출
                        Console.WriteLine();
                        Console.WriteLine($"{rewards[0]}경험치와, {rewards[1]}골드를 얻었다!");
                        player.AddItem(rewards[2]);
                        Console.WriteLine($"{DataManager.Instance.Items[rewards[2]].Name}을 획득했다!");
                        Console.ReadKey(true);
                        return;
                    }
                    if (player.HP <= 0) // 플레이어가 죽었을 때
                    {
                        Console.WriteLine("플레이어가 죽었습니다. 게임 오버.");
                        Console.ReadKey(true);
                        return;
                    }
                    if (creature is Player) // 플레이어 턴
                    {
                        MainScript(); // 플레이어 전투 시작용 메소드 호출
                        Console.ReadKey(true);
                    }
                    else if (creature is Monster enemy) // 몬스터 턴
                    {
                        EnemyTurn(enemy); // 몬스터 턴 메소드 호출
                    }
                }
                foreach (var creature in order) // 턴이 끝난 후 상태이상 효과 적용
                {
                    if (creature.StatusEffect != null)
                    {
                        for (int i = 0; i < creature.StatusEffect.Length; i++)
                        {
                            if (creature.StatusEffect[i][0] == 0) { continue; } // 상태이상 효과가 없는 경우는 무시
                            creature.ApplyStatusEffect(i + 1);
                        }
                    }
                }
                order = NewOrder(); // 턴 순서 재설정
            }
        }
        public void MainScript() //플레이어 전투 시작용
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (EnemyList[i].HP <= 0) // 적이 죽었을 경우
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    RPGsys.ArrangePrint($"Lv.{EnemyList[i].Level} {EnemyList[i].Name}", 25);
                    Console.WriteLine($"| Dead");
                }
                else // 적이 살아있을 경우
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    RPGsys.ArrangePrint($"Lv.{EnemyList[i].Level} {EnemyList[i].Name}", 25);
                    Console.WriteLine($"| HP:{EnemyList[i].HP}/{EnemyList[i].MaxHP}   ATK:{EnemyList[i].TotalAttack}");
                }
                Console.ResetColor();
            }
            Console.WriteLine("=================================================");
            Console.WriteLine();
            Console.WriteLine("플레이어 정보:");
            Console.WriteLine($"{player.Name} ({player.ClassName})   ATK:{player.TotalAttack}");
            Console.WriteLine($"HP: {player.HP}/{player.MaxHP}     |     MP:{player.MP}/{player.MaxMP}");
            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("무엇을 할까?");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 방어");
            Console.WriteLine("3. 스킬");
            Console.WriteLine("4. 아이템");
            
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Attack();
                        break;
                    case 2:
                        Defend();
                        break;
                    case 3:
                        UseSkill();
                        break;
                    case 4:
                        UseItem();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.(숫자 범위 벗어남)"); // 잘못된 입력 범위
                        MainScript(); // 재귀 호출로 다시 입력 받기
                        break;
                }
            }
            else // 입력이 숫자가 아닐 경우
            {
                Console.WriteLine("잘못된 입력입니다.(숫자가 아님)");
                MainScript(); // 재귀 호출로 다시 입력 받기
            }
        }
        public void Attack()
        {
            Console.WriteLine($"=================================================");
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (EnemyList[i].HP <= 0) // 적이 죽었을 경우
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    RPGsys.ArrangePrint($"{i + 1}. Lv.{EnemyList[i].Level} {EnemyList[i].Name}", 25);
                    Console.WriteLine($"| Dead");
                }
                else // 적이 살아있을 경우
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    RPGsys.ArrangePrint($"{i+1}. Lv.{EnemyList[i].Level} {EnemyList[i].Name}", 25);
                    Console.WriteLine($"| HP:{EnemyList[i].HP}/{EnemyList[i].MaxHP}");
                }
                Console.ResetColor();
            }// 적 출력
            Console.WriteLine("=================================================");
            Console.WriteLine("누구를 공격할까?");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                var enemy = EnemyList[--choice]; // 선택한 몬스터
                try
                {
                    if(enemy.HP <= 0)
                    {
                        Console.WriteLine("이미 죽은 몬스터입니다. 다시 시도해주세요.");
                        MainScript();
                        return;
                    }
                    int damage = player.CalculateDamage(0, enemy);
                    //if (damage == 0) // 오류 판정
                    //{
                    //    Attack(); // 데미지가 0일 경우(오류가 났을 경우) 다시 공격 입력 받기
                    //    return;
                    //}
                    int oldHP = enemy.HP; // 공격 전 HP 저장
                    Console.WriteLine($"{player.Name}의 공격!");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{enemy.Name}에게 {damage}만큼의 데미지를 입혔다!");
                    Console.ResetColor();
                    enemy.ChangeHP(-damage); // 데미지 적용
                    Console.WriteLine($"{enemy.Name}의 HP: {oldHP} -> {(enemy.HP > 0 ? enemy.HP : "Dead")}");
                }
                catch (ArgumentOutOfRangeException) // 인덱스 에러바운딩
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    MainScript(); // 재귀 호출로 다시 입력 받기
                }
            }
            else // 입력이 숫자가 아닐 경우
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                MainScript(); // 재귀 호출로 다시 입력 받기
            }
        }
        public void Defend()
        {
            Console.WriteLine($"{player.Name}은(는) 방어 자세를 취했다!");
            player.ActivateSkill(1, player); // 방어 스킬 사용
        }
        public void UseSkill()
        {
            Console.WriteLine("사용할 스킬을 선택하세요.");
            for (int i = 0; i < player.Skills.Count-2; i++)
            {
                RPGsys.ArrangePrint($"{i + 1}. {player.Skills[i + 2].Name} |", 15);
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write($"MP: {player.Skills[i + 2].Cost}  ");
                Console.ResetColor();
                Console.WriteLine($" {player.Skills[i + 2].Description}");
            }
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                var skill = player.Skills[++choice]; // 선택한 스킬
                try
                {
                    if (skill.Type == Utils.SkillType.all)
                    {
                        Console.WriteLine(choice);
                        foreach (var enemy in EnemyList)
                        {
                            int oldHP = enemy.HP; // 공격 전 HP 저장
                            int damage = player.ActivateSkill(skill.Id, enemy); // 스킬 사용
                            if (damage < 0) // 오류 판정
                            {
                                Console.WriteLine("알 수 없는 오류");
                                MainScript(); // 데미지가 0보다 작을 경우(오류가 났을 경우) 다시 스킬 입력 받기
                                return;
                            }
                            Console.WriteLine($"{player.Name}의 {skill.Name}!");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{enemy.Name}에게 {damage}만큼의 데미지를 입혔다!");
                            Console.ResetColor();
                            Console.WriteLine($"{enemy.Name}의 HP: {oldHP} -> {(enemy.HP > 0 ? enemy.HP : "Dead")}");
                        }
                    }
                    else if (skill.Type == Utils.SkillType.enemy)
                    {
                        for (int i = 0; i < EnemyList.Count; i++)
                        {
                            if (EnemyList[i].HP <= 0) // 적이 죽었을 경우
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                RPGsys.ArrangePrint($"{i + 1}. Lv.{EnemyList[i].Level} {EnemyList[i].Name}", 25);
                                Console.WriteLine($"| Dead");
                            }
                            else // 적이 살아있을 경우
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                RPGsys.ArrangePrint($"{i + 1}. Lv.{EnemyList[i].Level} {EnemyList[i].Name}", 25);
                                Console.WriteLine($"| HP:{EnemyList[i].HP}/{EnemyList[i].MaxHP}");
                            }
                            Console.ResetColor();
                        } // 적 출력
                        Console.WriteLine("=================================================");
                        Console.WriteLine("누구를 공격할까?");
                        if (int.TryParse(Console.ReadLine(), out int choice2) && EnemyList[--choice2].HP > 0)
                        {
                            var enemy = EnemyList[choice2]; // 선택한 몬스터
                            try
                            {
                                int oldHP = enemy.HP; // 공격 전 HP 저장
                                int damage = player.ActivateSkill(skill.Id, enemy);
                                if (damage == 0) // 오류 판정
                                {
                                    MainScript(); // 데미지가 0일 경우(오류가 났을 경우) 다시 공격 입력 받기
                                    return;
                                }
                                Console.WriteLine($"{player.Name}의 {skill.Name}!");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{enemy.Name}에게 {damage}만큼의 데미지를 입혔다!"); // 데미지 적용
                                Console.ResetColor();
                                Console.WriteLine($"{enemy.Name}의 HP: {oldHP} -> {(enemy.HP > 0 ? enemy.HP : "Dead")}");
                            }
                            catch (ArgumentOutOfRangeException) // 인덱스 에러바운딩
                            {
                                Console.WriteLine("잘못된 입력1입니다. 다시 시도해주세요.");
                                MainScript(); // 재귀 호출로 다시 입력 받기
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("이미 죽은 몬스터입니다. 다시 시도해주세요.");
                            MainScript(); // 재귀 호출로 다시 입력 받기
                            return;
                        }
                    }
                    else if (skill.Type == Utils.SkillType.self)
                    {
                        Console.WriteLine($"{player.Name}의 {skill.Name}!");
                        int damage = player.ActivateSkill(skill.Id, player); // 스킬 사용
                        if (damage == 0) // 오류 판정
                        {
                            MainScript(); // 데미지가 0일 경우(오류가 났을 경우) 다시 스킬 입력 받기
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 스킬 타입입니다."); // 잘못된 스킬 타입일 경우의 에러 메세지
                        return;
                    }
                }
                catch (ArgumentOutOfRangeException) // 인덱스 에러바운딩
                {
                    Console.WriteLine("잘못된 입력2입니다. 다시 시도해주세요.");
                    MainScript(); // 재귀 호출로 다시 입력 받기
                }
            }
            else // 입력이 숫자가 아닐 경우
            {
                Console.WriteLine("잘못된 입력3입니다. 다시 시도해주세요.");
                MainScript(); // 재귀 호출로 다시 입력 받기
            }
        }
        public void UseItem()
        {
            Console.WriteLine("사용할 아이템을 선택하세요.");
            player.ShowInventory(); // 플레이어의 인벤토리와 아이템 사용 메소드 호출
        }
        public void EnemyTurn(Monster enemy) // 몬스터 턴
        {
            if (enemy is not Monster) { return; }// 몬스터가 아닌 객체는 무시

            if (enemy.HP <= 0) { return; }
            // 이미 죽은 몬스터는 무시
            // 여기에 죽은 몬스터의 카운팅 이벤트 코드 추가 가능
            //
            int skillId = new Random().Next(16, 24); // 랜덤으로 스킬 선택
            if (skillId > 21) { skillId -= 22; } // 스킬 ID 범위 조정

            Skill skill = DataManager.Instance.Skills[skillId];
            int damage = enemy.CalculateDamage(skillId, player);
            int oldHP = player.HP; // 공격 전 플레이어 HP 저장
            Console.WriteLine();
            Console.WriteLine($"{enemy.Name}의 {skill.Name}!");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{player.Name} 은 {damage}만큼의 데미지를 입었다!");
            Console.ResetColor();
            player.ChangeHP(-damage); // 데미지 적용
            Console.WriteLine($"{player.Name}의 HP: {oldHP} -> {(player.HP > 0 ? player.HP : "Dead")}");
            Console.WriteLine();
            Console.ReadKey(true);
        }
        public int SpeedDice(Creature obj) // 속도값 판정: (보정 = 최소: +0 최대 +5)
        {
            Random random = new Random();
            return random.Next(0, 6) + obj.Speed;
        }
        public List<Creature> NewOrder() // 턴 시작 시 호출 요망: order 초기화 및 재설정
        {
            List<Creature> newOrder = new List<Creature>();
            int[] speedList = new int[order.Count];
            for (int i = 0; i < order.Count; i++)
            {
                speedList[0] = (SpeedDice(order[i]));
            }
            for (int i = 0; i < order.Count; i++)
            {
                int index = Array.IndexOf(speedList, speedList.Max());
                newOrder.Add(order[index]);
                speedList[index] = int.MinValue; // 이미 사용된 속도값은 최소치로 변경
            }
            return newOrder;
        }
        private bool IsEnemiesDead()
        {
            foreach (var enemy in EnemyList)
            {
                if (enemy.HP > 0) return false; // 적이 아직 살아있으면 false 반환
            }
            return true; // 모든 적이 죽었으면 true 반환
        }

        public int[] Reward()
        {
            int totalExp = 0;
            int totalGold = 0;
            foreach (var enemy in EnemyList)
            {
                totalExp += enemy.Reward.Exp;
                totalGold += enemy.Reward.Gold;// 적의 경험치 합산
            }
            player.ChangeExp(totalExp); // 플레이어 경험치 증가
            player.ChangeGold(totalGold);
            int dropitemId = 1;
            Random rand = new Random();
            for (int i=0; i<EnemyList.Count; i++)
            {
                if (rand.Next(0, 100) < 40)
                    dropitemId = rand.Next(1, 9); //40% 확률로 아이템 변환
            }
            return [totalExp, totalGold, dropitemId]; // 경험치와 골드 반환
        }
    }

    internal class RPGsys
    {
        public static void ArrangePrint(string text, int columnWidth) // 문자열 출력 길이 통합용 메소드
        {
            int visualLength = 0;

            foreach (char c in text)
            {
                if (char.IsDigit(c) || char.IsLetter(c) || char.IsPunctuation(c) || char.IsWhiteSpace(c))
                    visualLength += 1;
                else if (c >= '\uAC00' && c <= '\uD7AF') // 유니코드 한글 범위
                    visualLength += 1;
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
