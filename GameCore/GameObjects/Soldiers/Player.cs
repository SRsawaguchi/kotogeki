using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingShoot.GameCore.GameObjects.Items.Bullets;
using TypingShoot.GameCore.Helper.RomanConverter;

namespace TypingShoot.GameCore.GameObjects.Soldiers
{

    class Player : Soldier
    {


        public Player (Point myPosition)
            :base(myPosition)
        {
        }

        public override void OnCreate ()
        {
            Manager.RequestAddColliders(this);
        }

        public override void OnUpdate (UpdateEventArg e)
        {
        }

        public override void OnDestroy ()
        {
        }

        public override void OnCollision (GameObject obj)
        {
        }
    }
}
