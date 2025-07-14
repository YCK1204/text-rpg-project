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
         * 1: 공격력 변동
         * 2: 방어력 변동
         * 3: 체력 변동
         * 4: 마나(스테미나) 변동
         * 5: 상태이상(+)
         * 직업 리스트:
         * (미정)
         */

        public int id { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
        public string description { get; set; }
        public float coefficient { get; set; }
        public Type type { get; set; }
        public List<dynamic>? effect {  get; set; } // 2차원 배열, [[효과id, 적용 배율, 적용 턴], ...]
        public enum Type
        {
            all, enemy, self, any, random
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
            });
            skillList.Add(new Skills()
            {
                id = 1,
                name = "방어",
                cost = 0,
                description = "적의 눈을 바라보고, 동태를 살피고, 몸을 지킨다.",
                coefficient = 0,
                type = Type.self,
                effect = new List<dynamic> { new int[] { 2, 50, 1 }, new int[] { 4, 30, 1 } }
            });
            skillList.Add(new Skills()
            {
                id = 2,
                name = "강타",
                cost = 10,
                description = "정신을 한 데 그러모아 크게 한 방 날린다.",
                coefficient = 150,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 3,
                name = "전사의 포효",
                cost = 30,
                description = "마음 속 깊은 곳에서부터 끓어오르는 함성을 전방을 향해 지른다.",
                coefficient = 0,
                type = Type.self,
                effect = new List<dynamic> { new int[] { 1, 30, 3} }
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 0,
                name = "공격",
                cost = 0,
                description = "공격한다. 주먹을 휘두르든, 무기를 휘두르든.",
                coefficient = 100,
                type = Type.enemy,
                effect = null
            });

        }
    }
}
