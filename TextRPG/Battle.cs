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
            order = NewOrder(); // 턴 순서 초기화
            while (true)
            {
                foreach (var creature in order)
                {
                    if (IsEnemiesDead()) // 플레이어만 남았을 때
                    {
                        Console.WriteLine("승리!");
                        return;
                    }
                    if (player.HP <= 0) // 플레이어가 죽었을 때
                    {
                        Console.WriteLine("플레이어가 죽었습니다. 게임 오버.");
                        return;
                    }
                    if (creature is Player) // 플레이어 턴
                    {
                        MainScript(); // 플레이어 전투 시작용 메소드 호출
                    }
                    else if (creature is Monster enemy) // 몬스터 턴
                    {
                        EnemyTurn(enemy); // 몬스터 턴 메소드 호출
                    }
                }
                order = NewOrder(); // 턴 순서 재설정
            }
        }
        public void MainScript() //플레이어 전투 시작용
        {
            Console.WriteLine($"{order[0]}들을 만났다!");
            Console.WriteLine($"=================================================");
            for (int i = 0; i < order.Count; i++)
            {
                if (order[i] is not Player)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    RPGsys.ArrangePrint($"Lv.{order[i].Level} {order[i].Name}", 25);
                    Console.WriteLine($"| HP:{order[i].HP}/{order[i].MaxHP}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    RPGsys.ArrangePrint($"Lv.{order[i].Level} {order[i].Name}", 25);
                    Console.WriteLine($"| Dead");
                }
                Console.ResetColor();
            }
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
                        Console.WriteLine("잘못된 입력입니다."); // 잘못된 입력 범위
                        MainScript(); // 재귀 호출로 다시 입력 받기
                        break;
                }
            }
            else // 입력이 숫자가 아닐 경우
            {
                Console.WriteLine("잘못된 입력입니다.");
                MainScript(); // 재귀 호출로 다시 입력 받기
            }
        }
        public void Attack()
        {
            Console.WriteLine($"=================================================");
            for (int i = 0; i < order.Count; i++)
            {
                if (order[i] is not Player)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    RPGsys.ArrangePrint($"{i+1}. Lv.{order[i].Level} {order[i].Name}", 25);
                    Console.WriteLine($"| HP:{order[i].HP}/{order[i].MaxHP}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    RPGsys.ArrangePrint($"Lv.{order[i].Level} {order[i].Name}", 25);
                    Console.WriteLine($"| Dead");
                }
                Console.ResetColor();
            }
            Console.WriteLine("=================================================");
            Console.WriteLine("누구를 공격할까?");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                try
                {
                    int damage = player.CalculateDamage(0, EnemyList[--choice]);
                    if (damage == 0) // 오류 판정
                    {
                        Attack(); // 데미지가 0일 경우(오류가 났을 경우) 다시 공격 입력 받기
                        return;
                    }
                    int oldHP = EnemyList[choice].HP; // 공격 전 HP 저장
                    Console.WriteLine($"{player.Name}의 공격!");
                    Console.WriteLine($"{EnemyList[choice].Name}에게 {damage}만큼의 데미지를 입혔다!");
                    EnemyList[choice].ChangeHP(-damage); // 데미지 적용
                    Console.WriteLine($"{EnemyList[choice].Name}의 HP: {oldHP} -> {(EnemyList[choice].HP>0 ? EnemyList[choice].HP : "Dead")}");
                }
                catch (IndexOutOfRangeException) // 인덱스 에러바운딩
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Attack(); // 재귀 호출로 다시 입력 받기
                }
            }
            else // 입력이 숫자가 아닐 경우
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                Attack(); // 재귀 호출로 다시 입력 받기
            }
        }
        public void Defend()
        {
            Console.WriteLine($"{player.Name}은(는) 방어 자세를 취했다!");
            Console.WriteLine($"{player.Name}의 방어력이 잠시 올랐다!");
            Console.WriteLine($"{player.Name}의 MP가 조금 회복되었다!");
            player.ActivateSkill(1, player); // 방어 스킬 사용
        }
        public void UseSkill()
                    {
            Console.WriteLine("사용할 스킬을 선택하세요.");
            for (int i = 0; i < player.Skills.Count; i++)
            {
                RPGsys.ArrangePrint($"{i + 1}. {player.Skills[i+2].Name} | {player.Skills[i].Description}", 100);
            }
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                try
                {
                    if (player.Skills[--choice].Type == Utils.SkillType.all)
                    {
                        foreach (var enemy in EnemyList)
                        {
                            int damage = player.ActivateSkill(player.Skills[choice].Id, enemy); // 스킬 사용
                            if (damage == 0) // 오류 판정
                            {
                                UseSkill(); // 데미지가 0일 경우(오류가 났을 경우) 다시 스킬 입력 받기
                                return;
                            }
                            int oldHP = enemy.HP; // 공격 전 HP 저장
                            Console.WriteLine($"{player.Name}의 {player.Skills[choice].Name}!");
                            Console.WriteLine($"{enemy.Name}에게 {damage}만큼의 데미지를 입혔다!");
                            Console.WriteLine($"{EnemyList[choice].Name}의 HP: {oldHP} -> {(EnemyList[choice].HP > 0 ? EnemyList[choice].HP : "Dead")}");
                        }
                    }
                    else if (player.Skills[choice].Type == Utils.SkillType.enemy)
                    {
                        Console.WriteLine("=================================================");
                        Console.WriteLine("누구를 공격할까?");
                        if (int.TryParse(Console.ReadLine(), out int choice2))
                        {
                            try
                            {
                                int damage = player.ActivateSkill(player.Skills[--choice2].Id, EnemyList[choice2]);
                                if (damage == 0) // 오류 판정
                                {
                                    Attack(); // 데미지가 0일 경우(오류가 났을 경우) 다시 공격 입력 받기
                                    return;
                                }
                                int oldHP = EnemyList[choice2].HP; // 공격 전 HP 저장
                                Console.WriteLine($"{player.Name}의 {player.Skills[choice].Name}!");
                                Console.WriteLine($"{EnemyList[choice2].Name}에게 {damage}만큼의 데미지를 입혔다!");
                                EnemyList[choice].ChangeHP(-damage); // 데미지 적용
                                Console.WriteLine($"{EnemyList[choice2].Name}의 HP: {oldHP} -> {(EnemyList[choice2].HP > 0 ? EnemyList[choice2].HP : "Dead")}");
                            }
                            catch (IndexOutOfRangeException) // 인덱스 에러바운딩
                            {
                                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                                UseSkill(); // 재귀 호출로 다시 입력 받기
                                return;
                            }
                        }
                    }
                    else if (player.Skills[choice].Type == Utils.SkillType.self)
                    {
                        Console.WriteLine($"{player.Name}의 {player.Skills[++choice].Name}!");
                        int damage = player.ActivateSkill(player.Skills[choice].Id, player); // 스킬 사용
                        if (damage == 0) // 오류 판정
                        {
                            UseSkill(); // 데미지가 0일 경우(오류가 났을 경우) 다시 스킬 입력 받기
                            return;
                        }
                    }
                    else
                                            {
                        Console.WriteLine("잘못된 스킬 타입입니다."); // 잘못된 스킬 타입일 경우의 에러 메세지
                        return;
                    }
                }
                catch (IndexOutOfRangeException) // 인덱스 에러바운딩
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    UseSkill(); // 재귀 호출로 다시 입력 받기
                }
            }
            else // 입력이 숫자가 아닐 경우
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                UseSkill(); // 재귀 호출로 다시 입력 받기
            }
        }
        public void UseItem()
        {
            Console.WriteLine("사용할 아이템을 선택하세요.");
            for (int i = 0; i < player.Inventory.Items.Count; i++)
            {
                RPGsys.ArrangePrint($"{i + 1}. {player.Inventory.Items[i].Name} | {player.Inventory.Items[i].Description}", 100);
            }
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                // 아이템 사용 출력
            }
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
            Console.WriteLine($"{enemy.Name}의 {skill.Name}!");
            Console.WriteLine($"{player.Name}에게 {damage}만큼의 데미지를 입혔다!");
            player.ChangeHP(-damage); // 데미지 적용
            Console.WriteLine($"{player.Name}의 HP: {oldHP} -> {(player.HP > 0 ? player.HP : "Dead")}");
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
