using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingShoot.GameCore.Helper;

namespace TypingShoot.GameCore.GameObjects.Items.Bullets
{
    abstract class Bullet : Item
    {
        public int Power { get; set; }
        public Trajectory Trajectries { get; set; }

        public Bullet (Point myPoint, int power, int speed, Point targetPoint)
            : base(myPoint)
        {
            this.Power = power;
            Trajectries = new Trajectory(targetPoint, myPoint, speed);
        }
    }
}
