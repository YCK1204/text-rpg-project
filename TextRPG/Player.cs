using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Data;
using TextRPG.Utils;
using TextRPG.Utils.DataModel.Creature;
using TextRPG.Utils.DataModel.Skill;

namespace TextRPG
{
    public class Player : Character
    {
        static Player instance { get; } = new Player();
        public static Player Instance { get { return instance; } }
        public int ActivateSkill(int skillId, Creature activer, Creature passiver) // 스킬 사용 메소드: (스킬 id, 사용 객체, 효과&공격 대상 객체)
        {
            if (DataManager.Instance.Skills.ContainsKey(skillId) == false)
            {
                Console.WriteLine("해당 스킬이 존재하지 않습니다.");
                return 0;
            }
            Skill skill = DataManager.Instance.Skills[skillId];
            // 코스트 감소
            if (activer.MP < skill.Cost)
            {
                Console.WriteLine("코스트가 부족합니다.");
                return 0;
            }
            activer.MP -= skill.Cost;
            // 스킬 효과 적용
            if (skill.Effect != null) // 인스턴스 이름 수정 
            {
                foreach (var eff in skill.Effect)
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
                            passiver.HP += (int)(passiver.MaxHP * eff[1] / 100.0f);
                            break;
                        case 4: // 코스트 변동
                            passiver.MP += eff[1];
                            break;
                        case 5: // 치명 변동
                            //passiver.CriticalChance += eff[1];
                            break;
                        case 6: // 상태이상 적용
                            //if (rand.Next(100) < eff[2]) // 확률 적용
                            //    ApplyStatusEffect(passiver, eff[1], eff[3]); // 차후 메소드 추가요청
                            break;
                    }
                }
            }
            return (int)(activer.Attack * skill.Coefficient / 100.0f); // 데미지 반환
        }

        public void PrintSkillEffect(int effectId, Creature passiver, int? StatusId = null, bool increased = true)
        {
            switch (effectId)
            {
                case 1: // 공격력 변동
                    Console.WriteLine($"{passiver.Name}의 공격력이 {(increased ? "상승했다" : "하락했다")}!");
                    break;
                case 2: // 방어력 변동
                    Console.WriteLine($"{passiver.Name}의 방어력이 {(increased ? "상승했다" : "하락했다")}!");
                    break;
                case 3: // 체력 변동
                    //Console.WriteLine($"{passiver.Name}의 체력을 {/*recover(차후 입력요망)*/}만큼{(increased ? "회복했다" : "지불했다")}!");
                    break;
                case 4: // 코스트 변동
                    Console.WriteLine($"{passiver.Name}의 코스트가 {(increased ? "상승했다" : "하락했다")}!");
                    break;
                case 5: // 치명타 확률 변동
                    Console.WriteLine($"{passiver.Name}의 치명타 확률이 {(increased ? "상승했다" : "하락했다")}!");
                    break;
                case 6: // 상태이상 적용
                    switch (StatusId)
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
                    break;
                default:
                    Console.WriteLine("알 수 없는 효과입니다.");
                    break;
            }
        }

        public void PrintEffects(int damage, Creature passiver) // 스킬 효과 출력 메소드
        {
            //if (passiver != null)
            //{
            //    foreach (var eff in passiver.effect)
            //    {
            //        switch (passiver.effect[0])
            //        {
            //            // 상태이상 발동시 출력
            //            case 1: // 화상
            //                Console.WriteLine($"{passiver.Name}은(는) 화상으로 인해 {damage}의 피해를 입었다!");
            //                break;
            //            case 2: // 중독
            //                Console.WriteLine($"{passiver.Name}은(는) 독으로 인해 {passiver.MaxHP / 10}의 피해를 입었다!");
            //                break;
            //            case 3: // 출혈
            //                Console.WriteLine($"{passiver.Name}은(는) 출혈으로 인해 {passiver.Attack / 2}의 피해를 입었다!");
            //                break;
            //            case 4: // 마비
            //                Console.WriteLine($"{passiver.Name}은(는) 몸이 저려 움직일 수 없다!");
            //                break;
            //            case 5: // 침묵
            //                Console.WriteLine($"{passiver.Name}은(는) 기술을 쓸 수 없다!");
            //                break;
            //            case 6: // 빙결
            //                Console.WriteLine($"{passiver.Name}은(는) 얼어버려 움직일 수 없다!");
            //                break;
            //            case 7: // 혼란
            //                Console.WriteLine($"{passiver.Name}은(는) 혼란에 빠져 영문도 모른 채 {damage}의 피해를 입었다!");
            //                break;
            //        }
            //    }
            //}
        }
        public void playerinfo()
        {
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Attack: {Attack}");
            Console.WriteLine($"Defense:{Defense}");
            Console.WriteLine($"HP: {HP}/{MaxHP}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Exp: {Exp}/{NeedExp}");
            Console.WriteLine("아무키나 입력시 시작화면으로 돌아갑니다.");
            Console.ReadKey();
        }
    }
}
