using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingShoot.GameCore.GameObjects.Items.Bullets
{
    class LinerBullet : Bullet
    {
        public LinerBullet (Point pt, int power, int speed, Point targetPosition)
            : base(pt, power, speed, targetPosition)
        {
        }

        public override void OnCreate ()
        {
            //サウンドを鳴らすなり、画像を光らせるなり。
            Manager.RequestAddColliders(this);
        }

        public override void OnUpdate (UpdateEventArg e)
        {
            //フレームが更新されるたびに呼ばれる。
            var nextPoint = Trajectries.ApproachTarget();
            Position = nextPoint;
        }

        public override void OnDestroy ()
        {
            //破棄されるときにコールされる。
           // System.Diagnostics.Debug.Print("LinerBullet Was Destroyed...");
        }

        public override void OnCollision (GameObject obj)
        {
        }
    }
}
