using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class Monster
    {
        public string Name { get; protected set; }
        public int Health {  get; protected set; }
        public int Attack {  get; protected set; }
        public int Level {  get; protected set; }
        public int DropGold { get; protected set; }
        public int DropExp { get; protected set; }

        public abstract void AttackPlayer();
        
            //플레이어 체력-=미니언 공격
        
        
    }
    public class MInion : Monster
    {


        public MInion()
        {
            Random random = new Random();
            int randomLevel = random.Next(1, 5);
            Name = "미니언";
            Health = 50 + Level * 10;
            Attack = 10 + Level * 3;
            Level = randomLevel;
            DropGold = 100 + Level * 10;
            DropExp = 100 + Level * 10;


        }
        public override void AttackPlayer()
        {
            //플레이어 체력-=미니언 공격
        }
    }
        


    public class CannonMinion:Monster
    {
        public CannonMinion()
        {
            Random random = new Random();
            int randomLevel = random.Next(1, 5);
            Name = "대포 미니언";
            Health = 70 + Level * 10;
            Attack = 20 + Level * 3;
            Level = randomLevel;
            DropGold = 200 + Level * 10;
            DropExp = 200 + Level * 10;
        }
        public override void AttackPlayer()
        {
            //플레이어 체력-=미니언 공격
        }
    }
    public class VoidMinion : Monster
    {
        public VoidMinion()
        {
            Random random = new Random();
            int randomLevel = random.Next(1, 5);
            Name = "미니언";
            Health = 60 + Level * 10;
            Attack = 15 + Level * 3;
            Level = randomLevel;
            DropGold = 150 + Level * 10;
            DropExp = 150 + Level * 10;
        }
        public override void AttackPlayer()
        {
            //플레이어 체력-=미니언 공격
        }
    }
}
