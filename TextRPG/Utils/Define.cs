using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Utils
{
    public enum CharacterClass
    {
        Warrior,
        Mage,
    }
    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
    public enum SkillType
    {
        all,
        enemy,
        self,
        any,
        random,
        player
    }
}
