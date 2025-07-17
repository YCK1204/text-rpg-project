using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_RPG
{
    internal class Skills
    {
        /*
         * 필요 요소들: 스킬 id, 이름, 설명, 스킬 분류(타겟팅), 배율, 효과 
         * 효과 id: (임시)
         * 1: 공격력 변동 - 자신
         * 2: 방어력 변동 - 자신
         * 3: 체력 변동 - 자신
         * 4: 마나(스테미나) 변동 - 자신
         * 5: 급소율 변동 - 자신
         * 6: 상태이상(+) - 상대...대상지정(자동)
         * 직업 리스트: 
         * 전사/마법사/궁수/도적/해적
         * 
         * 상태이상:
         * 1: 화상 - 처음 입힌 데미지만큼 고정 데미지(매턴)
         * 2: 중독 - 최대체력비례 데미지(매턴)(-10%)
         * 3: 출혈 - 공격력 비례 데미지(매턴)(atk*0.5)
         * 4: 마비 - 행동 불가(20% 확률, 최대 5턴), 회피 불가
         * 5: 침묵 - 스킬 사용 불가(특정 턴 동안)
         * 6: 빙결 - 행동 불가(특정 턴 동안)
         * 7: 혼란 - 자해를 포함한 랜덤 행동(자해 확률 33%, 최대 5턴)
         * 8: 즉사 - 확률에 의한 즉사(체력 최대치만큼 즉시 데미지)
         * 상태이상 적용 시: [효과 id, 이상id, 적용 확률, 적용 턴]
         */
        private Random rand = new();
        public int id { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
        public string description { get; set; }
        public float coefficient { get; set; }
        public Type type { get; set; }
        public List<int[]>? effect {  get; set; } // 2차원 배열, [[효과id, 적용 배율, 적용 턴, 대상id], ...]
        public enum Type
        {
            all, enemy, self, any, random, player
        }
        public List<Skills> skillList { get; } = new();

        Skills()
        {
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            }); // 공격
            skillList.Add(new Skills()
            {
                id = 1,
                name = "방어",
                cost = 0,
                description = "적의 눈을 바라보고, 동태를 살피고, 몸을 지킨다.",
                coefficient = 0,
                type = Type.self,
                effect = new List<int[]> { new int[] { 2, 50, 1 }, new int[] { 4, 30, 1 } }
            }); // 방어
            // 전사
            skillList.Add(new Skills()
            {
                id = 2,
                name = "강타",
                cost = 10,
                description = "=Smite",
                coefficient = 150,
                type = Type.enemy,
                effect = null
            }); // 전사 1 - 1
            skillList.Add(new Skills()
            {
                id = 3,
                name = "전사의 포효",
                cost = 30,
                description = "마음 속 깊은 곳에서부터 끓어오르는 함성을 전방을 향해 지른다.",
                coefficient = 0,
                type = Type.self,
                effect = new List<int[]> { new int[] { 1, 30, 3 } }
            }); // 전사 2 - 5
            skillList.Add(new Skills()
            {
                id = 4,
                name = "투구깨기",
                cost = 50,
                description = "대검을 머리 위로 높게 들어, 적의 머리를 향해 내려친다. 단순하나, 그만큼 강력한 일격.",
                coefficient = 250,
                type = Type.enemy,
                effect = new List<int[]> { new int[] { 6, 3, 70, 3 } }
            }); // 전사 3 - 10
            // 궁수
            skillList.Add(new Skills()
            {
                id = 5,
                name = "곡사",
                cost = 30,
                description = "하늘을 향해 화살을 쏘아 광범위한 비를 만든다. 다만, 평소보다 위력이 약할 뿐.",
                coefficient = 70,
                type = Type.all,
                effect = null
            }); // 궁수 1 - 1
            skillList.Add(new Skills()
            {
                id = 6,
                name = "강사",
                cost = 40,
                description = "정신을 그러모아 집중, 상대의 급소를 꿰뚫는다.",
                coefficient = 200,
                type = Type.enemy,
                effect = new List<int[]> { new int[] { 5, 30, 1 } }
            }); // 궁수 2 - 5
            skillList.Add(new Skills()
            {
                id = 7,
                name = "필중의 각오",
                cost = 3,
                description = "눈 앞의 적에게 집중해 움직임을 읽는다. 다음 공격에 급소를 맞추기를 빌면서.",
                coefficient = 0,
                type = Type.self,
                effect = new List<int[]> { new int[] { 5, 20, 3 } }
            }); // 궁수 3 - 10
            // 법사
            skillList.Add(new Skills()
            {
                id = 8,
                name = "파이어볼",
                cost = 30,
                description = "손에서 불덩이를 만들어 적에게 던진다. 끔찍하긴 하지만 맞추면 고기타는 냄새가 난다.",
                coefficient = 80,
                type = Type.enemy,
                effect = new List<int[]> { new int[] { 6, 1, 100, 3 } }
            }); // 법사 1 - 1
            skillList.Add(new Skills()
            {
                id = 9,
                name = "아이시클 랜스",
                cost = 30,
                description = "손 위에 얼음 창을 만들어 적에게 던진다. 마법으로 훨씬 경화된 물이다. 잘 깨지지 않는다.",
                coefficient = 200,
                type = Type.enemy,
                effect = new List<int[]> { new int[] { 6, 6, 50, 3 } }
            }); // 법사 2 - 5
            skillList.Add(new Skills()
            {
                id = 10,
                name = "힐",
                cost = 40,
                description = "따스한 빛을 비추어 체력을 회복한다. 이 빛은 마법일까, 기적일까.",
                coefficient = 0,
                type = Type.self,
                effect = new List<int[]> { new int[] { 3, 30, 1 } }
            }); // 법사 3 - 10
            // 도적
            skillList.Add(new Skills()
            {
                id = 11,
                name = "유체화",
                cost = 30,
                description = "그림자에 숨어 기회를 노린다. 방심할 때의 급습은 치명타로 이어지겠지.",
                coefficient = 0,
                type = Type.self,
                effect = new List<int[]> { new int[] { 5, 20, 3 } }
            }); // 도적 1 - 1
            skillList.Add(new Skills()
            {
                id = 12,
                name = "맹독",
                cost = 50,
                description = "상대를 향해 맹독이 담긴 구슬을 던진다. 저 구슬 하나면 사람 수천은 능히 죽일 수 있다.",
                coefficient = 0,
                type = Type.all,
                effect = new List<int[]> { new int[] { 6, 2, 80, 5 } }
            }); // 도적 2 - 5
            skillList.Add(new Skills()
            {
                id = 13,
                name = "급습",
                cost = 50,
                description = "아무도 눈치채지 못할때, 가장 방심했을 때. 그 한 순간만을 노리고 심장에 칼을 박아넣는다.",
                coefficient = 200,
                type = Type.enemy,
                effect = new List<int[]> { new int[] { 6, 8, 5, 1 } }
            }); // 도적 3 - 10
            // 해적
            skillList.Add(new Skills()
            {
                id = 14,
                name = "약탈 선언",
                cost = 40,
                description = "해적은 바다의 무법자. 약탈할 수 있다면 약탈하는 것이 그들의 모토.",
                coefficient = 0,
                type = Type.self,
                effect = new List<int[]> { new int[] { 1, 10, 3 }, new int[] { 2, 10, 3 }, new int[] { 5, 10, 3 } }
            }); // 해적 1 - 1
            skillList.Add(new Skills()
            {
                id = 15,
                name = "집중포화",
                cost = 30,
                description = "보통이라면 산개할 탄환을 한데 그러모아 단 하나의 표적만을 노린다.",
                coefficient = 200,
                type = Type.enemy,
                effect = null
            }); // 해적 2 - 5
            skillList.Add(new Skills()
            {
                id = 16,
                name = "파상포진",
                cost = 60,
                description = "파상풍과 대상포진을 한번에! 이번만 특별 서비스입니다.",
                coefficient = 100,
                type = Type.enemy,
                effect = new List<int[]> { new int[] { 6, 3, 100, 5 }, new int[] { 6, 8, 1, 1 }, }
            }); // 해적 3 - 10
            // 적
            skillList.Add(new Skills()
            {
                id = 16,
                name = "물기",
                cost = 20,
                description = "",
                coefficient = 150,
                type = Type.player,
                effect = null
            }); // 적 1
            skillList.Add(new Skills()
            {
                id = 17,
                name = "깨물어부수기",
                cost = 30,
                description = "",
                coefficient = 150,
                type = Type.player,
                effect = effect = new List<int[]> { new int[] { 6, 3, 30, 3 } }
            }); // 적 2
            skillList.Add(new Skills()
            {
                id = 18,
                name = "베르세르크",
                cost = 30,
                description = "+체력 소모형 버프: 스스로의 공격력에 비례한 피해를 입고 공격력 증가",
                coefficient = 50,
                type = Type.self,
                effect = effect = new List<int[]> { new int[] { 1, 60, 3 } }
            }); // 적 3
            skillList.Add(new Skills()
            {
                id = 19,
                name = "저주파",
                cost = 30,
                description = "마비&혼란",
                coefficient = 0,
                type = Type.player,
                effect = effect = new List<int[]> { new int[] { 6, 4, 30 }, new int[] { 6, 7, 30 } }
            }); // 적 4
            skillList.Add(new Skills()
            {
                id = 20,
                name = "괴력난신",
                cost = 50,
                description = "",
                coefficient = 250,
                type = Type.player,
                effect = null
            }); // 적 5
            // 공통
            skillList.Add(new Skills()
            {
                id = 21,
                name = "재장전",
                cost = 10,
                description = "모든 걸 버리고, 단 한 발을 위해.",
                coefficient = 0,
                type = Type.self,
                effect = effect = new List<int[]> { new int[] { 1, 100, 2} }
            }); // 재장전
            skillList.Add(new Skills()
            {
                id = 22,
                name = "공격",
                cost = 0,
                description = "혼란용 공격",
                coefficient = 100,
                type = Type.self,
                effect = null
            }); // 혼란 상태이상용 공격
            // 궁극기
            skillList.Add(new Skills() 
            {
                id = 23,
                name = "마탄 장전",
                cost = 50,
                description = "네 말대로, 뭐든 맞출 수 있는 탄환이로구나",
                coefficient = 30,
                type = Type.self,
                effect = effect = new List<int[]> { new int[] { 1, 200, 2 } }
            }); // 해적 4 - 15
            skillList.Add(new Skills()
            {
                id = 24,
                name = "참모아베기",
                cost = 70,
                description = "육중한 검의 무게와, 회전이 섞인 대운동. 용을 격퇴하는 일격이라고도 일컬어진다.",
                coefficient = 500,
                type = Type.enemy,
                effect = effect = new List<int[]> { new int[] { 3, -50 , 1 } } // 역회심
            }); // 전사 4 - 15
            skillList.Add(new Skills()
            {
                id = 25,
                name = "제네시스",
                cost = 80,
                description = "처음, 그 무엇도 구분되지 않았던 시절을 이 일격에 담아.",
                coefficient = 350,
                type = Type.all,
                effect = effect = new List<int[]> { new int[] { 6, 1, 30, 3 }, new int[] { 6, 2, 30, 3 }, new int[] { 6, 3, 30, 3 }, new int[] { 6, 4, 30, 3 }, new int[] { 6, 5, 30, 3 }, new int[] { 6, 6, 30, 3 } }
            }); // 법사 4 - 15
            skillList.Add(new Skills()
            {
                id = 26,
                name = "시어 하트 어택",
                cost = 80,
                description = "그냥 툭 건드리는 것 같지만 사실은 굉장히 섬세한 기술. 심장만을 노려 터뜨리는, 도적의 극의.",
                coefficient = 100,
                type = Type.enemy,
                effect = effect = new List<int[]> { new int[] { 6, 8, 50, 1 } }
            }); // 도적 4 - 15
            skillList.Add(new Skills()
            {
                id = 26,
                name = "원 포 원",
                cost = 70,
                description = "남아있는 모든 화살을, 단 한번의 일격을 위해",
                coefficient = 300,
                type = Type.enemy,
                effect = effect = new List<int[]> { new int[] { 1, 100, 2 } } //회심 최대적용
            }); // 궁수 4 - 15
        }
       
        public int ActivateSkill(int skillId, object activer, object passiver) // 스킬 사용 메소드: (스킬 id, 사용 객체, 효과&공격 대상 객체)
        {
            Skills? skill = skillList.FirstOrDefault(s => s.id == skillId); // errorbound
            if (skill == null)
            {
                Console.WriteLine("해당 스킬이 존재하지 않습니다.");
                return 0;
            }
            // 코스트 감소
            if (activer.cost < skill.cost)
            {
                Console.WriteLine("코스트가 부족합니다.");
                return 0;
            }
            activer.Mana -= skill.cost;
            // 스킬 효과 적용
            if (skill.effect != null) // 인스턴스 이름 수정 
            {
                foreach (var eff in skill.effect)
                {
                    switch (eff[0])
                    {
                        case 1: // 공격력 변동
                            passiver.Attack += (int)(passiver.Attack * eff[1] / 100.0f);
                            break;
                        case 2: // 방어력 변동
                            passiver.Defense += (int)(passiver.Defense * eff[1] / 100.0f);
                            break;
                        case 3: // 체력 변동
                            passiver.Health += (int)(passiver.MaxHealth * eff[1] / 100.0f);
                            break;
                        case 4: // 코스트 변동
                            passiver.Mana += eff[1];
                            break;
                        case 5: // 치명 변동
                            passiver.CriticalChance += eff[1];
                            break;
                        case 6: // 상태이상 적용
                            //if (rand.Next(100) < eff[2]) // 확률 적용
                            //    ApplyStatusEffect(passiver, eff[1], eff[3]); // 차후 메소드 추가요청
                            break;
                    }
                }
            }
            return (int)(activer.Attack * skill.coefficient/100.0f); // 데미지 반환
        }
    
        public void PrintSkillEffect(int effectId, object passiver, int? StatusId=null, bool increased = true)
        {
            switch (effectId)
            {
                case 1: // 공격력 변동
                    Console.WriteLine($"{passiver.Name}의 공격력이 {(increased? "상승했다":"하락했다")}!");
                    break;
                case 2: // 방어력 변동
                    Console.WriteLine($"{passiver.Name}의 방어력이 {(increased? "상승했다" : "하락했다")}!");
                    break;
                case 3: // 체력 변동
                    Console.WriteLine($"{passiver.Name}의 체력을 {/*recover(차후 입력요망)*/}만큼{(increased? "회복했다" : "지불했다")}!");
                    break;
                case 4: // 코스트 변동
                    Console.WriteLine($"{passiver.Name}의 코스트가 {(increased? "상승했다" : "하락했다")}!");
                    break;
                case 5: // 치명타 확률 변동
                    Console.WriteLine($"{passiver.Name}의 치명타 확률이 {(increased? "상승했다" : "하락했다")}!");
                    break;
                case 6: // 상태이상 적용
                    swtich(StatusId)
                    {
                        case 1: // 화상
                            Console.WriteLine($"{passiver.Name}이(가) 화상에 걸렸다!");
                            break;
                        case 2: // 중독
                            Console.WriteLine($"{passiver.Name}이(가) 중독에 걸렸다!");
                            break;
                        case 3: // 출혈
                            Console.WriteLine($"{passiver.Name}이(가) 출혈에 걸렸다!");
                            break;
                        case 4: // 마비
                            Console.WriteLine($"{passiver.Name}이(가) 마비에 걸렸다!");
                            break;
                        case 5: // 침묵
                            Console.WriteLine($"{passiver.Name}이(가) 침묵에 걸렸다!");
                            break;
                        case 6: // 빙결
                            Console.WriteLine($"{passiver.Name}이(가) 빙결에 걸렸다!");
                            break;
                        case 7: // 혼란
                            Console.WriteLine($"{passiver.Name}이(가) 혼란에 빠졌다!");
                            break;
                        case 8: // 즉사
                            Console.WriteLine($"일격필살! {passiver.Name}이(가) 즉사했다!");
                            break;
                        default:
                            Console.WriteLine("알 수 없는 상태이상입니다.");
                            break;
                        }
                        default:
                    Console.WriteLine("알 수 없는 효과입니다.");
                    break;
                }
            }
        }

        public void PrintEffects(int damage, object passiver) // 스킬 효과 출력 메소드
        {
            if (passiver != null)
            {
                foreach (var eff in passiver.effect)
                {
                    switch(passiver.effect[0])
                    {
                        // 상태이상 발동시 출력
                    case 1: // 화상
                        Console.WriteLine($"{passiver.Name}은(는) 화상으로 인해 {damage}의 피해를 입었다!");
                        break;
                    case 2: // 중독
                        Console.WriteLine($"{passiver.Name}은(는) 독으로 인해 {passiver.MaxHealth/10}의 피해를 입었다!");
                        break;
                    case 3: // 출혈
                        Console.WriteLine($"{passiver.Name}은(는) 출혈으로 인해 {passiver.Attack/2}의 피해를 입었다!");
                        break;
                    case 4: // 마비
                        Console.WriteLine($"{passiver.Name}은(는) 몸이 저려 움직일 수 없다!");
                        break;
                    case 5: // 침묵
                        Console.WriteLine($"{passiver.Name}은(는) 기술을 쓸 수 없다!");
                        break;
                    case 6: // 빙결
                        Console.WriteLine($"{passiver.Name}은(는) 얼어버려 움직일 수 없다!");
                        break;
                    case 7: // 혼란
                        Console.WriteLine($"{passiver.Name}은(는) 혼란에 빠져 영문도 모른 채 {damage}의 피해를 입었다!");
                    }
                }
            }
        }
    }
