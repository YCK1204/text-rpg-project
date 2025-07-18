
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;
using TextRPG.Utils.DataModel.Creature;
using static System.Formats.Asn1.AsnWriter;

namespace ConsoleApp1
{
    public class QuestManager
    {
        private static QuestManager instance;
        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuestManager();
                return instance;
            }


        }
        Quest earnMoney = new EarnMoneyQuest();
        Quest levelUp = new LevelUpQuest();
        Quest potionUse= new PotionUseQuest();

        
        

        public void LoadQuestScene()
        {
            Console.WriteLine("=========================퀘스트 보드================================");
            Console.WriteLine("어느 퀘스트를 수락하시겠습니까?");
            Console.WriteLine($"1. '{earnMoney.QuestName}'  보상: {earnMoney.QuestReward} G");
            Console.WriteLine($"2. '{levelUp.QuestName}'  보상: {levelUp.QuestReward} G");
            Console.WriteLine($"3. '{potionUse.QuestName}'  보상: {potionUse.QuestReward} G");
            Console.WriteLine("");
            Console.WriteLine("5. 나가기");
            Console.WriteLine(">>>");
            if (int.TryParse(Console.ReadLine(), out int key))
            {
                if (key == 1)
                {
                    earnMoney.AcceptQuest();
                }
                else if (key == 2)
                {
                    levelUp.AcceptQuest();
                }
                else if (key == 3)
                {
                    potionUse.AcceptQuest();
                }
                else if (key == 4)
                {
                    //메인메뉴로 가기
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                }
            }




        }


    }
}
