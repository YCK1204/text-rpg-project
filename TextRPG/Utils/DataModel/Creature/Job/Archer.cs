using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Data;
using TextRPG.Utils.DataModel.Item;

namespace TextRPG.Utils.DataModel.Creature.Job
{
    public class Archer : Player
    {
        public Archer(CharacterClassData data)
        {
            ClassName = data.ClassName;


            int[] skillsId = data.SkillsId;

            foreach (var skillId in skillsId)
            {
                if (DataManager.Instance.Skills.ContainsKey(skillId))
                {
                    Skills.Add(DataManager.Instance.Skills[skillId]);
                }
                else
                {
                    Console.WriteLine($"스킬 ID {skillId}이(가) 존재하지 않습니다.");
                }
            }
        }
    }
}
