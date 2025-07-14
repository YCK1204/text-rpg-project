namespace Team_RPG
{
    internal class Skills
    {
        /*
         * 필요 요소들: 스킬 id, 이름, 설명, 스킬 분류(타겟팅), 배율, 효과 
         * 
         * 
         * 
         */

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float coefficient { get; set; }
        public Type type { get; set; }

        public List<dynamic>? effect {  get; set; } // 0: 효과 id, 1: 효과 배율
        public enum Type
        {
            none, enemy, self, any, random
        }

        public Skills(int id, string name, string description, float coefficient, Type type, List<dynamic>? effect = null)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.coefficient = coefficient;
            this.type = type;
            this.effect = effect;
        }
    }
}
