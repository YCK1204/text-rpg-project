using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Utils.DataModel.Skill
{
    public class Skill : GameObject
    {
        public int Cost { get; set; }
        public string Description { get; set; }
        public float Coefficient { get; set; }
        public SkillType Type { get; set; }
        public List<int[]>? Effect { get; set; } // 2차원 배열, [[효과id, 적용 배율, 적용 턴, 대상id], ...]

    }
    public struct Skills
    {
        [JsonProperty("Skills")]
        public List<Skill> _Skills { get; set; }
    }
}
