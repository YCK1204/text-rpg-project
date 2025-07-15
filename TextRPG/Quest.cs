using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{

    public class Quest
    {
        private Monster monster;
        int targetcount = 5;
        int currentcount = 0;
        public void Subscribe(Monster monster)
        {
            this.monster = monster;
            monster.MonsterDied += OnMonsterDied;
        }

        private void OnMonsterDied()
        {
            currentcount++;
            if (currentcount == targetcount)
            {
                {
                    Console.WriteLine("공허충 처치 퀘스트 완료");
                    Unsubscribe();
                }
            }
        }
        private void Unsubscribe()
        {
            monster.MonsterDied -= OnMonsterDied;
        }
    }
    public class QuestBoard
    {

        public void VoidCatcherQuest()
        {
            Quest quest = new Quest();
            VoidMinion voidMinion = new VoidMinion();
            quest.Subscribe(voidMinion);
        }
    }




}
