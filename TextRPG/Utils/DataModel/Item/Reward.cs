using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Utils.DataModel.Item
{
    public abstract class Reward
    {
        public int Gold { get; set; }
        public int Exp { get; set; }
    }
}
