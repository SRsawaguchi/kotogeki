using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingShoot.GameCore.GameObjects.Soldiers
{
    abstract class Soldier : GameObject
    {
        public Soldier (Point pt)
            : base(pt)
        {

        }
    }
}
