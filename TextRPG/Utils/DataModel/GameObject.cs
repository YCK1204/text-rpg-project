using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Utils.DataModel
{
    public abstract class GameObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
