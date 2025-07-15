using Newtonsoft.Json;

namespace TextRPG.Utils
{
    #region Monster
    public struct MonsterStatus
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Level { get; set; }
        public int[] RandomLevelRange { get; set; }
        public void AttackPlayer()
        {
            Console.WriteLine($"{Name}이 플레이어를 공격하였습니다");
            //플레이어 체력-=미니언 공격
        }
        public void UpdateHealth(int damage)
        {
            this.Health -= damage;
        }
    }
    public struct MonsterReward
    {
        public int Gold { get; set; }
        public int Exp { get; set; }
        public ItemRarity ItemRarity { get; set; }
    }
    public struct Monster
    {
        public int Id { get; set; }
        public MonsterStatus Status { get; set; }
        public MonsterReward Reward { get; set; }
    }
    public struct MonsterLoot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemRarity Type { get; set; }
        public int Price { get; set; }
    }
    public struct MonsterLoots
    {
        [JsonProperty("MonsterLoots")]
        public List<MonsterLoot> _MonsterLoots { get; set; }
    }
    public struct Monsters
    {
        [JsonProperty("Monsters")]
        public List<Monster> _Monsters { get; set; }
    }
    #endregion
    public interface IUsable
    {
        void Use();
    }
    public interface IEquippable
    {
        bool Equipped { get; set; }
        void Equip();
        void Unequip();
    }
    public struct Potion : Item, IUsable
    {
        public int Heal { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ItemRarity Rarity { get; set; }

        public void Display()
        {
            // 포션 정보 출력
        }

        public void Use()
        {
            // 플레이어 체력 회복
        }
    }
    public struct Armor : Item, IEquippable
    {
        public int Attack { get; set; }
        public bool Equipped { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ItemRarity Rarity { get; set; }

        public void Display()
        {
            // 방어구 정보 출력
        }

        public void Equip()
        {
            // 플레이어 공격력 추가
            // 착용중인 Weapon이 있다면 Unequip() 호출
            // Equipped = true; // 장비 착용 상태로 변경
        }

        public void Unequip()
        {
            // 플레이어 공격력 제거
            // Equipped = false; // 장비 착용 상태로 변경
        }
    }
    public struct Weapon : Item, IEquippable
    {
        public int Defense { get; set; }
        public bool Equipped { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ItemRarity Rarity { get; set; }

        public void Display()
        {
            // 무기 정보 출력
        }

        public void Equip()
        {
            // 플레이어 방어력 추가
            // 착용중인 Armor가 있다면 Unequip() 호출
            //Equipped = true; // 장비 착용 상태로 변경
        }

        public void Unequip()
        {
            // 플레이어 방어력 제거
            //Equipped = false; // 장비 착용 상태로 변경
        }
    }
    public interface Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ItemRarity Rarity { get; set; }
        public void Display();
    }
    public struct Inventory
    {
        public List<Item> Items { get; set; }
        public void UseItem(int idx)
        {
            if (idx < 0 || idx > Items.Count) return;
            if (Items[idx - 1] is IUsable usableItem)
            {
                usableItem.Use();
                Items.RemoveAt(idx);
            }
            else if (Items[idx - 1] is IEquippable equippableItem)
            {
                equippableItem.Equip(); // 장비 Item에서 Equipped 처리
            }
            else;// 사실상 없는 경우
        }
        public void UnUseItem(int idx)
        {
            if (idx < 0 || idx > Items.Count) return;
            if (Items[idx - 1] is IEquippable equippableItem && equippableItem.Equipped)
            {
                equippableItem.Unequip(); // 장비 Item에서 Equipped 처리
            }
            else;// 사실상 없는 경우
        }
        public void DisplayInventory()
        {
            Console.WriteLine("------------------- Item List -------------------");
            Console.WriteLine(
@"0. 뒤로가기
1. 아이템 사용
2. 아이템 팔기");
            foreach (var item in Items)
                item.Display();
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    return; // 뒤로가기
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    // 사용할 아이템 입력
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    // 판매할 아이템 입력
                    break;
            }
        }
    }
    public struct Items
    {
        [JsonProperty("Items")]
        public List<Item> _Items { get; set; }
    }
    public struct CharacterStatus
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public CharacterClass Class { get; set; }
        public string ClassName { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int NeedExp { get; set; }
        public int MaxHealth { get; set; }
    }
    public struct CharacterStatuses
    {
        [JsonProperty("Statuses")]
        public List<CharacterStatus> _PlayerStatuses { get; set; }
    }
    public struct PlayerCharacter
    {
        [JsonProperty("Status")]
        public CharacterStatus Status { get; set; }
        public Inventory Inventory { get; set; }
        public int Id { get; set; }
    }
    public struct PlayerCharacters
    {
        [JsonProperty("PlayerCharacters")]
        public List<PlayerCharacter> _PlayerCharacters { get; set; }
    }
}