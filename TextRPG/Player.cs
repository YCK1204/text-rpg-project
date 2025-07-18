using Newtonsoft.Json;
using TextRPG.Data;
using TextRPG.Utils;
using TextRPG.Utils.DataModel.Creature;
using TextRPG.Utils.DataModel.Item;
using TextRPG.Utils.DataModel.Skill;

namespace TextRPG
{
    public class Player : Character
    {
        public static Player Instance;

        private Random rand = new Random();
        [JsonIgnore]
        public int TotalAttack { get { return Attack + ItemAttack; } }
        [JsonIgnore]
        public int TotalDefense { get { return Defense + ItemDefense; } }
        [JsonIgnore]
        int ItemDefense { get; set; }
        [JsonIgnore]
        int ItemAttack { get; set; }

        [JsonIgnore]
        public List<Skill> Skills { get; set; } = new List<Skill>();
        [JsonIgnore]
        public string ClassName { get; set; }
        [JsonIgnore]
        Armor _armor;
        [JsonIgnore]
        Armor Armor
        {
            get
            {
                return _armor;
            }
            set
            {
                _armor = value;
                if (value == null)
                {
                    ItemDefense = 0;
                    return;
                }

                ItemDefense = Armor.Defense;
            }
        }
        [JsonIgnore]
        Weapon _weapon;
        [JsonIgnore]
        Weapon Weapon
        {
            get
            {
                return _weapon;
            }
            set
            {
                _weapon = value;
                if (value == null)
                {
                    ItemAttack = 0;
                    return;
                }

                ItemAttack = Weapon.Attack;
            }
        }
        public event Action EarnMoneyEvent;
        public event Action LevelUpEvent;
        public event Action UsePotionEvent;
        public Player(CharacterClassData data) // 캐릭터 생성으로 인한 플레이어 생성
        {
            Id = DataManager.Instance.GenerateLastId(); // 새로운 ID 생성
            CharacterClassId = data.Id;
            ClassName = data.ClassName;
            HP = data.MaxHP;
            MaxHP = data.MaxHP;
            MP = data.MaxMP;
            MaxMP = data.MaxMP;
            Speed = data.Speed;
            Attack = data.Attack;
            Defense = data.Defense;
            Name = data.Name;

            Inventory.Items.Add(new HpPotion() { Name = "소형 HP회복 물약", Heal = 50, Description = "체력을 50만큼 회복합니다.", Price = 100, Rarity = ItemRarity.Common });
            Inventory.Items.Add(new MpPotion() { Name = "소형 MP회복 물약", Heal = 50, Description = "마나를 50만큼 회복합니다.", Price = 100, Rarity = ItemRarity.Common });
            Inventory.Items.Add(new Armor() { Name = "초급 방어구", Defense = 10, Description = "방어력을 10만큼 증가시킵니다.", Price = 200, Rarity = ItemRarity.Common });
            Inventory.Items.Add(new Weapon() { Name = "초급 검", Attack = 10, Description = "공격력을 10만큼 증가시킵니다.", Price = 200, Rarity = ItemRarity.Common });

            foreach (var skillId in data.SkillsId)
                Skills.Add(DataManager.Instance.Skills[skillId]);

            DataManager.Instance.PlayerCharacters.Add(Id, this); // 플레이어 캐릭터 목록에 추가
            //DataManager.Instance.SaveData(); // 데이터 저장
            this.UpdateTotalStat(TotalAttack, TotalDefense); // 총 공격력과 방어력 업데이트
        }
        public Player(Character data) // 기존 캐릭터 데이터 로드로 인한 플레이어 생성
        {
            Id = data.Id;
            Name = data.Name;
            Level = data.Level;
            Gold = data.Gold;
            Exp = data.Exp;
            NeedExp = data.NeedExp;
            ItemsId = data.ItemsId;
            CharacterClassId = data.CharacterClassId;
            ClassName = DataManager.Instance.CharacterClassData[data.CharacterClassId].Name;
            HP = data.HP;
            MaxHP = data.MaxHP;
            MP = data.MP;
            MaxMP = data.MaxMP;
            Speed = data.Speed;
            Attack = data.Attack;
            Defense = data.Defense;

            if (data.ArmorItemId != null)
                Armor = DataManager.Instance.Items[data.ArmorItemId.Value] as Armor;
            if (data.WeaponItemId != null)
                Weapon = DataManager.Instance.Items[data.WeaponItemId.Value] as Weapon;

            var classData = DataManager.Instance.CharacterClassData[CharacterClassId];

            foreach (var skillId in classData.SkillsId)
                Skills.Add(DataManager.Instance.Skills[skillId]);
            ClassName = classData.ClassName;
            foreach (var itemId in data.ItemsId)
                Inventory.Items.Add(DataManager.Instance.Items[itemId]);

            DataManager.Instance.PlayerCharacters[Id] = this;
            //DataManager.Instance.PlayerCharacters.Add(Id, this); // 플레이어 캐릭터 목록에 추가
        }
        public void ChangeExp(int exp)
        {
            Exp += exp;
            LevelUp(Exp);
        }
        private void LevelUp(int levelexp)
        {
            int neededExp = Level * Level * 100;
            while (Exp >= neededExp)
            {
                Exp -= neededExp;
                Level += 1;
               
                Console.WriteLine($"레벨업! 플레이어 레벨이{Level} 이 되었습니다");
                if (Level >= 3)
                {
                    LevelUpEvent?.Invoke();
                }
                Console.ReadKey();
                neededExp = Level * Level * 100;
            }


        }

        public void ChangeGold(int gold)
        {
            Gold += gold;
            if( Gold >= 1000 )
            {
                EarnMoneyEvent?.Invoke();
            }
            if (Gold < 0) Gold = 0; // 금액이 음수가 되지 않도록
        }

        public override int CalculateDamage(int SkillId, Creature passive) // TotalAtack 적용을 위한 오버라이딩
        {
            Random rand = new Random();
            TextRPG.Utils.DataModel.Skill.Skill skill = DataManager.Instance.Skills[SkillId];
            
            // 테스트용
            //Console.WriteLine($"스킬 출력: {skill.Name}, {skill.Description}, mult:{skill.Coefficient}, damage:{TotalAttack} {(int)((TotalAttack * skill.Coefficient) / 100)} {-passive.TotalDefense}");
            
            // 스킬에 따른 데미지 계산 로직: 스킬 배율*공격력 - 피격체 방어력
            int damage = (int)((this.TotalAttack * skill.Coefficient) / 100 - passive.Defense);
            if (damage < 0) damage = 0; // 방어력에 의해 데미지가 0 이하로 떨어지지 않도록: 최소 데미지 = 0
            // 치명타 확률 적용
            if (rand.Next(0, 100) <= 20 + BuffDebuff[2][1]) // 치명타 확률: 기본 20% + 버프로 인해 증가한 수치
            {
                damage = (int)(damage * 2); // 치명타 데미지: 2배
            }
            return damage; // 계산된 데미지 반환
        }

        public int ActivateSkill(int skillId, Creature passiver) // 스킬 사용 메소드: (스킬 id, 사용 객체, 효과&공격 대상 객체)
        {
            if (DataManager.Instance.Skills.ContainsKey(skillId) == false)
            {
                Console.WriteLine("해당 스킬이 존재하지 않습니다.");
                return 0;
            }
            Skill skill = DataManager.Instance.Skills[skillId];
            // 코스트 감소
            if (this.MP < skill.Cost)
            {
                Console.WriteLine("코스트가 부족합니다.");
                return 0;
            }
            this.MP -= skill.Cost;
            // 스킬 효과 적용

            int damage;

            if (skill.Effect != null)
            {
                foreach (var i in skill.Effect)
                {
                    int id = 0;
                    if (i[0] == 1 || i[0] == 2 || i[0] == 5) //
                    {
                        id = this.UpdateBuffDebuff(i[0], i[1], i[2]); // 버프/디버프 적용
                        this.PrintSkillEffect(i[0], this, i[1], true); // 버프/디버프 효과 출력
                    }
                    if (i[0] == 3)
                    // 체력 회복
                    {
                        this.ChangeHP(i[1] * this.MaxHP / 100);
                        this.PrintSkillEffect(3, this, (i[1] * this.MaxHP / 100), true); // 버프/디버프 효과 출력
                    }
                    if (i[0] == 4)
                    // 마나 회복
                    {
                        this.ChangeMP(i[1] * this.MaxMP / 100);
                        this.PrintSkillEffect(4, this, (i[1] * this.MaxMP / 100), true); // 버프/디버프 효과 출력
                    }
                } // 버프 적용

                damage = CalculateDamage(skillId, passiver);
                Console.WriteLine(damage);
                passiver.ChangeHP(-damage);
            }
            else
            {
                damage = CalculateDamage(skillId, passiver);
                passiver.ChangeHP(-damage);
                return damage;
            }

            foreach (var i in skill.Effect)
            {
                if (i[0] == 6 && rand.Next(0, 100) <= i[2]) // 상태이상 적용 확률 체크
                {
                    passiver.UpdateStatusEffect(i[1], i[3], damage);
                    PrintSkillEffect(i[0], passiver,i[1]); // 상태이상 효과 출력
                }
            }
            return damage;
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
                    Console.WriteLine($"{passiver.Name}의 체력을 {StatusId}만큼{(increased ? "회복했다" : "빼았겼다")}!");
                    break;
                case 4: // 코스트 변동
                    Console.WriteLine($"{passiver.Name}의 마나가 {StatusId}만큼 {(increased ? "회복했다" : "뺴았겼다")}!");
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
                    Console.WriteLine("알 수 없는 효과 코드입니다.");
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
            Console.Clear();
            string itemDefense = ItemDefense > 0 ? $"(+{ItemDefense.ToString()})" : "";
            string itemAttack = ItemAttack > 0 ? $"(+{ItemAttack.ToString()})" : "";

            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {ClassName}");
            Console.WriteLine($"Attack: {Attack}{itemAttack}");
            Console.WriteLine($"Defense:{Defense}{itemDefense}");
            Console.WriteLine($"HP: {HP}/{MaxHP}");
            Console.WriteLine($"MP: {MP}/{MaxMP}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Exp: {Exp}/{NeedExp}");
            Console.WriteLine($"Speed: {Speed}");
            Console.WriteLine("아무키나 입력시 시작화면으로 돌아갑니다.");
            Console.ReadKey();
        }
        public void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine("------------------- Item List -------------------");
            for (int i = 0; i < Inventory.Items.Count; i++)
            {
                string e = "";
                if (Inventory.Items[i] == Armor || Inventory.Items[i] == Weapon)
                    e = "E ";
                Console.Write($"{i + 1}. {e}");
                Inventory.Items[i].Display();
            }
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("아이템 번호 입력 시 사용/장착(해제) 합니다.");

            try
            {
                if (int.TryParse(Console.ReadLine(), out int val))
                {
                    if (val == 0)
                        return;

                    if (val < 0 || val > Inventory.Items.Count)
                    {
                        throw new FormatException();
                    }
                    else
                    {
                        var item = Inventory.Items[val - 1];
                        if (item is Potion potion)
                        {
                            if (potion is HpPotion)
                            {
                                HP = Math.Clamp(HP + potion.Heal, 0, MaxHP);
                                UsePotionEvent?.Invoke();
                            }
                            else if (potion is MpPotion)
                            {
                                MP = Math.Clamp(MP + potion.Heal, 0, MaxMP);
                            }
                            Inventory.RemoveItem(potion);
                        }
                        else if (item is Armor armor)
                        {
                            if (Armor == item)
                                Armor = null;
                            else
                                Armor = armor;
                        }
                        else if (item is Weapon weapon)
                        {
                            if (Weapon == item)
                                Weapon = null;
                            else
                                Weapon = weapon;
                        }
                    }
                }
            }
            catch (FormatException)
            {
                HandleInputError();
            }
            ShowInventory();
            this.UpdateTotalStat(TotalAttack, TotalDefense); // 총 공격력과 방어력 업데이트
        }
        void HandleInputError()
        {
            Console.Clear();
            Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
            Console.ReadKey(true);
        }
        void AddItem(Item item)
        {
            Inventory.Items.Add(item);
            ItemsId.Add(item.Id);
        }
        public void AddItem(int id)
        {
            AddItem(DataManager.Instance.Items[id]);
        }
    }
}
