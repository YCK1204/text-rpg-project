using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;
using TextRPG.Data;
namespace TextRPG.Utils.DataModel.Creature
{
    public class Creature : GameObject
    {
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int MP { get; set; }
        public int MaxMP { get; set; }
        public int Attack { get; set; }
        private int originalAttack; // 원래 공격력 저장용
        public int Defense { get; set; }
        private int originalDefense; // 원래 방어력 저장용
        public int Level { get; set; }
        public int Speed { get; set; }
        public int[][] StatusEffect { get; } = new int[7][]; // 상태이상 효과 배열
        public int[][] BuffDebuff { get; } = new int[3][]; // 버프/디버프 배열: [0] 공, [1] 방, [2] 치명

        public Creature()
        { 
            originalAttack = Attack; originalDefense = Defense;
            this.StatusEffect[0] = new int[] { 0, 0 }; // 화상 효과: [0] 적용 턴, [1] 데미지
            this.StatusEffect[1] = new int[] { 0 }; // 중독 효과: [0] 적용 턴
            this.StatusEffect[2] = new int[] { 0 }; // 출혈 효과: [0] 적용 턴
            this.StatusEffect[3] = new int[] { 0 }; // 마비 효과: [0] 적용 턴
            this.StatusEffect[4] = new int[] { 0 }; // 침묵 효과: [0] 적용 턴
            this.StatusEffect[5] = new int[] { 0 }; // 빙결 효과: [0] 적용 턴
            this.StatusEffect[6] = new int[] { 0 }; // 혼란 효과: [0] 적용 턴
            this.BuffDebuff[0] = new int[] { 0, 0 }; // 공격력 버프: [0] 버프 수치, [1] 지속 턴
            this.BuffDebuff[1] = new int[] { 0, 0 }; // 방어력 버프: [0] 버프 수치, [1] 지속 턴
            this.BuffDebuff[2] = new int[] { 0, 0 }; // 치명타 확률 버프: [0] 버프 수치, [1] 지속 턴

        } // 기본 생성자: 공격력과 방어력 원본 저장

        public void RollBack()
        {
            Attack = originalAttack; // 공격력 원본으로 되돌리기
            Defense = originalDefense; // 방어력 원본으로 되돌리기
        }
        public void UpdateOriginalStatus()
        {
            Attack = originalAttack; // 현재 공격력을 원본 공격력으로 저장
            Defense = originalDefense; // 현재 방어력을 원본 방어력으로 저장
        }
        public int ChangeHP(int amount)
        {
            int original = HP;
            HP = Math.Clamp(HP - amount, 0, MaxHP);
            return HP;
            if (HP < 0) HP = 0;
            if (HP > MaxHP) HP = MaxHP;
            return ((Math.Abs(HP - original) < Math.Abs(amount) ? HP - original : Math.Abs(amount))); // 변경된 HP 반환(콘솔 표시용)
        }
        public int ChangeMP(int amount)
        {
            int original = MP;
            MP += amount;
            if (MP < 0) MP = 0;
            if (MP > MaxMP) MP = MaxMP;
            return ((Math.Abs(HP - original) < Math.Abs(amount) ? HP - original : Math.Abs(amount))); // 변경된 MP 반환(콘솔 표시용)
        }
        public void ChangeAttack(int amount)
        {
            Attack = originalAttack + amount;
            if (Attack < 0) Attack = 0;
            // 공격력 증감에 한도 없음. + 버프로 인해 증가한 수치 감소용 로직(또는 메소드) 필요
        }
        public void ChangeDefense(int amount)
        {
            Defense = originalDefense + amount;
            if (Defense < 0) Defense = 0;
            // 방어력 증감에 한도 없음. + 버프로 인해 증가한 수치 감소용 로직(또는 메소드) 필요
        }
        public int CalculateDamage(int SkillId, Creature passive)
        { 
            Random rand = new Random();
            TextRPG.Utils.DataModel.Skill.Skill skill = DataManager.Instance.Skills[SkillId];
            // 스킬에 따른 데미지 계산 로직: 스킬 배율*공격력 - 피격체 방어력
            int damage = (int)((Attack * skill.Coefficient)/100 - passive.Defense);
            if (damage < 0) damage = 1; // 방어력에 의해 데미지가 1 이하로 떨어지지 않도록: 최소 데미지 = 1
            // 치명타 확률 적용
            if (rand.Next(0, 100) <= 20 + BuffDebuff[2][1]) // 치명타 확률: 기본 20% + 버프로 인해 증가한 수치
            {
                damage = (int)(damage * 2); // 치명타 데미지: 2배
            }
            return damage; // 계산된 데미지 반환
        }
        public int UpdateBuffDebuff(int buffType, int percent, int turn) // 버프/디버프 변경 메소드
        {
            if (this.BuffDebuff[0][0] == null)
            {
                
            }
            // 버프/디버프 변경 로직
            switch (buffType)
            {
                case 1: // 공격력 버프
                    BuffDebuff[buffType-1][0] = percent; //
                    BuffDebuff[buffType-1][1] = turn; // 버프 지속 턴
                    return 1;
                case 2: // 방어력 버프
                    BuffDebuff[buffType - 1][0] += percent; // 
                    BuffDebuff[buffType - 1][1] = turn; // 버프 지속 턴
                    return 2;
                case 3: // 치명타 확률 버프
                    BuffDebuff[buffType - 1][0] += percent; //
                    BuffDebuff[buffType - 1][1] = turn; // 버프 지속 턴
                    return 5;
                default:
                    throw new ArgumentException("Unknown buff type");
            }
        }
        public int[] ApplyBuffDebuff() // 버프/디버프 적용 및 턴계산 메소드
        {
            RollBack(); // 원래 공격력과 방어력으로 되돌리기(버프/디버프 중첩 방지)
            int[] returnAmount = [0,0]; // 반환값: 적용된 버프/디버프 수치
            // 버프/디버프 적용 로직
            for (int i = 0; i < BuffDebuff.Length; i++)
            {
                if (BuffDebuff[i] != null && BuffDebuff[i][1] > 0) // 버프/디버프가 적용되어 있고, 지속 턴이 남아있을 때
                {
                    BuffDebuff[i][1]--; // 지속 턴 감소
                    switch (i)
                    {
                        case 0: // 공격력 버프
                            returnAmount[0] = originalAttack*BuffDebuff[i][0]/100; // 공격력 버프 수치 반환
                            ChangeAttack(originalAttack * BuffDebuff[i][0] / 100); // 공격력 버프 적용
                            break;
                        case 1: // 방어력 버프
                            returnAmount[1] = BuffDebuff[i][0]; // 방어력 버프 수치 반환
                            break;
                        case 2: // 치명타 확률 버프(수치 반환 값 없음, 수치상 드러나지 않음)
                            break;
                    }
                }
                
            }
            return returnAmount; // 적용된 버프/디버프 수치 반환

        }
        public void UpdateStatusEffect(int effectType, int duration, int damage = 1) // 상태이상 효과 변경 메소드
        {
            // 상태이상 효과 적용 로직: 다른 상태이상 효과가 적용되어 있을 시 덮어씌움.
            switch (effectType)
            {
                case 1:
                    // 화상 효과 적용 로직
                    StatusEffect[0] = new int[] { duration, damage }; // 적용 턴, 데미지(1 턴당 화상 공격으로 입힌 데미지(=가변) )
                    break;
                case 2:
                    // 중독 효과 적용 로직
                    StatusEffect[1] = new int[] { duration }; // 적용 턴만. 데미지 = ( 1 턴당 최대 체력의 10% 데미지(=고정) )
                    break;
                case 3:
                    // 출혈 효과 적용 로직
                    StatusEffect[2] = new int[] { duration }; // 적용 턴만. 데미지 = ( 1 턴당 최대 체력의 10% 데미지(=고정) )
                    break;
                case 4:
                    // 마비 효과 적용 로직
                    StatusEffect[3] = new int[] { duration };
                    break;
                case 5:
                    // 침묵 효과 적용 로직
                    StatusEffect[4] = new int[] { duration };
                    break;
                case 6:
                    // 빙결 효과 적용 로직
                    StatusEffect[5] = new int[] { duration };
                    break;
                case 7:
                    // 혼란 효과 적용 로직
                    StatusEffect[6] = new int[] { duration };
                    break;
                case 8:
                    // 즉사 효과 적용 로직
                    ChangeHP(-MaxHP); // 최대HP 데미지
                    break;
                default:
                    // 오류 메세지
                    throw new ArgumentException("Unknown effect type");
            }
        }
        public bool? ApplyStatusEffect(int EffectId) // 상태이상 효과 적용 및 판정 메소드(판정값은 bool로 반환됨)
        {
            Random rand = new Random();
            // 상태이상 효과 단일 적용 로직
            switch (EffectId)
            {
                case 1: // 화상
                    StatusEffect[0][0]--; // 화상 지속 턴 감소
                    ChangeHP(-StatusEffect[0][1]);
                    break;
                case 2: // 독
                    StatusEffect[1][0]--; // 독 지속 턴 감소
                    HP -= MaxHP / 10;
                    break;
                case 3: // 출혈
                    StatusEffect[2][0]--; // 출혈 지속 턴 감소
                    HP -= Attack;
                    break;
                case 4: // 마비
                    StatusEffect[3][0]--; // 마비 지속 턴 감소
                    return (rand.Next(0, 10) < 3) ? true : false;
                case 5: // 침묵
                    StatusEffect[4][0]--; // 침묵 지속 턴 감소
                    return (rand.Next(0, 10) < 3) ? true : false;
                case 6: // 빙결
                    StatusEffect[5][0]--; // 빙결 지속 턴 감소
                    return true; // 빙결은 무조건 성공
                case 7: // 혼란
                    StatusEffect[6][0]--; // 혼란 지속 턴 감소
                    return (rand.Next(0, 10) < 3) ? true : false;
                default:
                    throw new ArgumentException("Unknown effect type");
            }
            return null; // 턴 스킵 효과가 아닐 시 null 반환
        }

    }
}
