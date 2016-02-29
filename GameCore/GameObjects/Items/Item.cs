using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingShoot.GameCore
{
    abstract class Item : GameObject
    {
        public Item (Point pt)
            : base(pt)
        {

        }
    }
}
