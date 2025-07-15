using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Utils;

namespace TextRPG
{
    public class Player
    {
        static Player Instance { get; } = new Player();
        PlayerCharacter _character;
        public static PlayerCharacter Character 
        {
            get { return Instance._character; }
            set { Instance._character = value; }
        }
        #region Status
        public static int ID { get { return Character.Id; } }
        public static int Level 
        {
            get
            {
                return Character.Status.Level; 
            }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.Level = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static string Name 
        {
            get { return Character.Status.Name; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.Name = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static CharacterClass Class 
        {
            get { return Character.Status.Class; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.Class = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static int Attack 
        {
            get { return Character.Status.Attack; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.Attack = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static int Defense 
        {
            get { return Character.Status.Defense; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.Defense = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static int Health 
        {
            get { return Character.Status.Health; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.Health = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static int MaxHealth 
        {
            get { return Character.Status.MaxHealth; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.MaxHealth = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static int Gold 
        {
            get { return Character.Status.Gold; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.Gold = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static int Exp 
        {
            get { return Character.Status.Exp; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.Exp = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static int NeedExp 
        {
            get { return Character.Status.NeedExp; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.NeedExp = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static int ItemAttack 
        {
            get { return Character.Status.ItemAttack; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.ItemAttack = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static int ItemDefense 
        {
            get { return Character.Status.ItemDefense; }
            set
            {
                var playerCharacter = Character;
                var status = playerCharacter.Status;
                status.ItemDefense = value;
                playerCharacter.Status = status;
                Character = playerCharacter;
            }
        }
        public static int TotalAttack
        {
            get { return Attack + ItemAttack; }
        }
        public static int TotalDefense
        {
            get { return Defense + ItemDefense; }
        }
        #endregion
        public static Inventory Inventory
        {
            get { return Character.Inventory; }
            set
            {
                var playerCharacter = Character;
                playerCharacter.Inventory = value;
                Character = playerCharacter;
            }
        }
        public static void UnEquipItemArmor()
        {
            Inventory.UnEquipItemArmor();
        }
        public static void UnEquipItemWeapon()
        {
            Inventory.UnEquipItemWeapon();
        }
        public void playerinfo()
        {
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Attack: {Attack}");
            Console.WriteLine($"Defense:{Defense}");
            Console.WriteLine($"Health: {Health}/{MaxHealth}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Exp: {Exp}/{NeedExp}");
            Console.WriteLine("아무키나 입력시 시작화면으로 돌아갑니다.");
            Console.ReadKey();
        }
    }
}
