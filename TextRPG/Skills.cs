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
         * 5: 급소율 변동
         * 6: 상태이상(+)
         * 직업 리스트: Not Available Yet
         * 전사/마법사/궁수/도적/해적
         * 
         * 상태이상:
         * 1: 화상
         * 2: 중독
         * 3: 출혈
         * 4: 마비
         * 5: 침묵
         * 6: 빙결
         * 7: 혼란
         * 8: 즉사(턴 대신 확률)
         * 상태이상 적용 시: [효과 id, 이상id, 적용 확률, 적용 턴]
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
                description = "정신을 한 데 그러모아 크게 한 방 지른다.",
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
                id = 4,
                name = "투구깨기",
                cost = 40,
                description = "대검을 머리 위로 높게 들어, 적의 머리를 향해 내려친다. 단순하나, 그만큼 강력한 일격.",
                coefficient = 250,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 5,
                name = "곡사",
                cost = 30,
                description = "하늘을 향해 화살을 쏘아 광범위한 비를 만든다. 다만, 평소보다 위력이 약할 뿐.",
                coefficient = 70,
                type = Type.all,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 6,
                name = "강사",
                cost = 40,
                description = "정신을 그러모아 집중, 상대의 급소를 꿰뚫는다.",
                coefficient = 200,
                type = Type.enemy,
                effect = new List<int[]>{ new int[] { 5, 30, 1 } }
            }
            });
            skillList.Add(new Skills()
            {
                id = 7,
                name = "필중의 각오",
                cost = 3,
                description = "눈 앞의 적에게 집중해 움직임을 읽는다. 다음 공격에 급소를 맞추기를 빌면서.",
                coefficient = 0,
                type = Type.self,
                effect = new List<int[]> { new int[] { 5, 20, 3 } }
            });
            skillList.Add(new Skills()
            {
                id = 8,
                name = "파이어볼",
                cost = 30,
                description = "손에서 불덩이를 만들어 적에게 던진다. 잘 하면 철도 녹이는 불이다.",
                coefficient = 120,
                type = Type.enemy,
                effect = new List<int[]> { new int[] { 6, 1, 100, 3 } }
            });
            skillList.Add(new Skills()
            {
                id = 9,
                name = "아이시클 랜스",
                cost = 30,
                description = "손 위에 얼음 창을 만들어 적에게 던진다. 마법으로 훨씬 경화된 물이다. 잘 깨지지 않는다.",
                coefficient = 200,
                type = Type.enemy,
                effect = new List<int[]> { new int[] { 6, 6, 50, 3 } }
            });
            skillList.Add(new Skills()
            {
                id = 10,
                name = "힐",
                cost = 40,
                description = "따스한 빛을 비추어 체력을 회복한다. 이 빛은 마법일까, 기적일까.",
                coefficient = 0,
                type = Type.self,
                effect = new List<int[]> { new int[] { 3, 30, 1 } }
            });
            skillList.Add(new Skills()
            {
                id = 11,
                name = "은신",
                cost = 30,
                description = "그림자에 숨어 기회를 노린다. 방심할 때의 급습은 치명타로 이어지겠지.",
                coefficient = 0,
                type = Type.self,
                effect = new List<int[]> { new int[] { 5, 20, 3 } }
            });
            skillList.Add(new Skills()
            {
                id = 12,
                name = "맹독",
                cost = 50,
                description = "상대를 향해 맹독이 담긴 구슬을 던진다. 저 구슬 하나면 사람 수천은 능히 죽일 수 있다.",
                coefficient = 0,
                type = Type.all,
                effect = new List<int[]> { new int[] { 6, 2, 80, 5 } }
            });
            skillList.Add(new Skills()
            {
                id = 13,
                name = "급습",
                cost = 50,
                description = "아무도 눈치채지 못할때, 가장 방심했을 때. 그 한 순간만을 노리고 심장에 칼을 박아넣는다.",
                coefficient = 200,
                type = Type.enemy,
                effect = new List<int[]> { new int[] { 6, 7, 5, 1 } }
            });
            skillList.Add(new Skills()
            {
                id = 14,
                name = "약탈 선언",
                cost = 40,
                description = "해적은 바다의 무법자. 약탈할 수 있다면 약탈하는 것이 그들의 모토.",
                coefficient = 0,
                type = Type.self,
                effect = new List<dynamic> { new int[] { 1, 10, 3 }, new int[] { 2, 10, 3 }, new int[] { 5, 10, 3 } }
            });
            skillList.Add(new Skills()
            {
                id = 15,
                name = "집중사격",
                cost = 30,
                description = "보통이라면 산개할 탄환을 한데 그러모아 단 하나의 표적만을 노린다.",
                coefficient = 200,
                type = Type.enemy,
                effect = null
            });
            skillList.Add(new Skills()
            {
                id = 16,
                name = "대출혈",
                cost = 60,
                description = "상처를 벌리고 찢고 헤집어. 그 상처에서 피가 멎을 일이 없도록.",
                coefficient = 100,
                type = Type.enemy,
                effect = new List<int[]> { new int[] { 6, 3, 100, 5 }, new int[] { 6, 7, 1, 1 }, }
            });

        }
    }
}
