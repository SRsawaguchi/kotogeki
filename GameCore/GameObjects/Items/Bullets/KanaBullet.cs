using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingShoot.GameCore.GameObjects.Soldiers;
using TypingShoot.GameCore.Helper.FontTools;

namespace TypingShoot.GameCore.GameObjects.Items.Bullets
{
    class KanaBullet : LinerBullet
    {
        private char _Kana;

        public char Kana
        {
            get
            {
                return _Kana;
            }
        }

        public KanaBullet (Point pt, int power, int speed, Point targetPosition,char kana)
            : base(pt, power, speed, targetPosition)
        {
            var imageCreator = new FontImageCreator();
            var charImage    = imageCreator.CharToBitmap(kana, new Size(30, 30));

            this.Image = imageCreator.CharToBitmap(kana);
            this.Radius = this.Image.Width / 2;
            _Kana = kana;
        }

        public KanaBullet (Point pt, int power, int speed, Point targetPosition, char kana,FontImageCreator fontImageCreator)
            : base(pt, power, speed, targetPosition)
        {
            this.Image = fontImageCreator.CharToBitmap(kana);
            this.Radius = this.Image.Width / 2;
            _Kana = kana;
        }

        public override void OnCollision (GameObject obj)
        {
            if ( obj is Soldier ) {
                Manager.RequestDestroy(this);
            }
        }
    }
}
