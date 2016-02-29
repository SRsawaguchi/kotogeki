using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingShoot.GameCore.Helper
{
    class Trajectory
    {
        //public List<Point> Trajectories { get; set; }
        public Point TargetPosition { get; set; }
        public Point MyPosition { get; set; }
        public int Speed { get; set; }

        public Trajectory (Point targetPoint, Point myPoint, int speed)
        {
            //this.Trajectories = new List<Point>();
            this.TargetPosition = targetPoint;
            this.MyPosition = myPoint;
            this.Speed = speed;

            //Trajectories.Add(myPoint);
        }

        public Point ApproachTarget ()
        {
            var angle = Math.Atan2(TargetPosition.Y - MyPosition.Y, TargetPosition.X - MyPosition.X);
            var nextX = (int)(MyPosition.X + Math.Cos(angle) * Speed);
            var nextY = (int)(MyPosition.Y + Math.Sin(angle) * Speed);
            var nextP = new Point(nextX, nextY);

            //Trajectories.Add(nextP);
            MyPosition = nextP;
            return nextP;
        }

        public Point AwayFromTarget ()
        {
            var angle = Math.Atan2(TargetPosition.Y - MyPosition.Y, TargetPosition.X - MyPosition.X);
            var nextX = (int)( MyPosition.X - Math.Cos(angle) * Speed );
            var nextY = (int)( MyPosition.Y - Math.Sin(angle) * Speed );
            var nextP = new Point(nextX, nextY);

            //Trajectories.Add(nextP);
            MyPosition = nextP;
            return nextP;
        }
    }
}
