
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace ConsoleApp1
{
    public abstract class Quest
    {

        public abstract void AcceptQuest();
        public abstract void DoQuest();
        public string QuestName { get; set; }
        public int QuestReward { get; set; }
        public bool IsAlreadyTaken {  get; set; }

    }
    public class EarnMoneyQuest : Quest
    {
        public EarnMoneyQuest()
        {
            QuestName = "돈을 1000G 까지 벌어보자";
            QuestReward = 800;
            IsAlreadyTaken = false;
        }
        public override void AcceptQuest()
        {
            if (IsAlreadyTaken == true)
            {
                Console.WriteLine("이미 수락했거나 완료한 퀘스트입니다.");
                Console.WriteLine("아무키나 누르면 퀘스트 보드로 다시 돌아갑니다");
                Console.ReadKey(true);
                QuestManager.Instance.LoadQuestScene();
                return;
            }
            Player.Instance.EarnMoneyEvent += DoQuest;
            Console.WriteLine($"'{QuestName}' 퀘스트 수락");
            IsAlreadyTaken=true;
            Console.ReadKey();
            QuestManager.Instance.LoadQuestScene();
        }
        public override void DoQuest()
        {
            var prevColor= Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"'{QuestName}' 퀘스트를 완료하였습니다");
            Player.Instance.EarnMoneyEvent -= DoQuest;
            Console.WriteLine($"퀘스트 완료 보상으로 {QuestReward} G 를 획득하였습니다"); 
            Player.Instance.ChangeGold(QuestReward);
            Console.ForegroundColor = prevColor;

            Console.ReadKey();
            
        }
    }
    public class LevelUpQuest : Quest
    {
        public LevelUpQuest()
        {
            QuestName = "레벨을 3까지 올려보자";
            QuestReward = 500;
            IsAlreadyTaken = false;
        }
        public override void AcceptQuest()
        {
            if (IsAlreadyTaken == true)
            {
                Console.WriteLine("이미 수락했거나 완료한 퀘스트입니다.");
                Console.WriteLine("아무키나 누르면 퀘스트 보드로 다시 돌아갑니다");
                Console.ReadKey(true);
                QuestManager.Instance.LoadQuestScene();
                return;
            }
            Player.Instance.LevelUpEvent += DoQuest;
            Console.WriteLine($"'{QuestName}' 퀘스트 수락");
            IsAlreadyTaken=true;
            Console.ReadKey();
            QuestManager.Instance.LoadQuestScene();
        }
        public override void DoQuest()
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" '{QuestName}' 퀘스트를 완료하였습니다!");
            Player.Instance.LevelUpEvent -= DoQuest;
            Console.WriteLine($"퀘스트 완료 보상으로 {QuestReward} G 를 획득하였습니다");
            Player.Instance.ChangeGold(QuestReward);
            Console.ForegroundColor = prevColor;
            Console.ReadKey();
            
           
        }
    }

    public class PotionUseQuest : Quest
    {
        public PotionUseQuest()
        {
            QuestName = "HP 포션을 사용해보자";
            QuestReward = 300;
            IsAlreadyTaken = false;
        }
        public override void AcceptQuest()
        {
            if(IsAlreadyTaken==true)
            {
                Console.WriteLine("이미 수락했거나 완료한 퀘스트입니다.");
                Console.WriteLine("아무키나 누르면 퀘스트 보드로 다시 돌아갑니다");
                Console.ReadKey(true);
                QuestManager.Instance.LoadQuestScene();
                return;
            }
            Player.Instance.UsePotionEvent += DoQuest;
            Console.WriteLine($"'{QuestName}' 퀘스트 수락");
            IsAlreadyTaken=true;
            Console.ReadKey();
            QuestManager.Instance.LoadQuestScene();
        }
        public override void DoQuest()
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" '{QuestName}' 퀘스트를 완료하였습니다!");
            Player.Instance.UsePotionEvent -= DoQuest;
            Console.WriteLine($"퀘스트 완료 보상으로 {QuestReward} G 를 획득하였습니다");
            Player.Instance.ChangeGold(QuestReward);
            Console.ForegroundColor = prevColor;
            Console.ReadKey();
            
        }
    }


}