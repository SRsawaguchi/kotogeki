using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TypingShoot.GameCore.GameObjects.Items.Bullets
{
    class FakeBullet : KanaBullet
    {
        private int 　_FakeOffsetY;　　　　//仮のターゲットポイントと、真のターゲットポイントの差分ーーーＹ
        private int 　_FakeOffsetX;　　　　//仮のターゲットポイントと、真のターゲットポイントの差分ーーーＸ
        private Point _TempTargetPoint;   //仮のターゲットポイント
        private Point _TargetPoint;       //真のターゲットポイント
        private int 　_FakeTime;　　　　　//仮のターゲットポイントへ接近する時間。speedの倍数に設定する。
        private bool  _HasSwitched;      //真のターゲットに切り替え済みなら真になる。
        private Bitmap _BulletImage;

        //Point myPoint  => マズルのPoint
        //int offsetX    => _FakeOffsetX
        //int offsetY    => _FakeOffsetY
        //int fakeWeight => _FakeTimeはspeed * fakeWeightで設定される。
        public FakeBullet (Point myPoint,Point targetPoint, int offsetX, int offsetY,int speed,int fakeWeight,Bitmap bulletImage,char kana)
            :base(myPoint,0,speed,targetPoint,kana)
        {
            _TargetPoint = targetPoint;
            _FakeOffsetX = offsetX;
            _FakeOffsetY = offsetY;
            _FakeTime    = speed * fakeWeight;
            _HasSwitched = false;
            _BulletImage = bulletImage;

            //_TempTargetPointの設定
            var x = _TargetPoint.X + offsetX;
            var y = _TargetPoint.Y + offsetY;
            _TempTargetPoint = new Point(x, y);
            this.Trajectries.TargetPosition = _TempTargetPoint;

            using (Graphics g = Graphics.FromImage(_BulletImage)) {

                TextRenderer.DrawText(g, kana.ToString(), new Font("ＭＳ ゴシック", 12.0f,FontStyle.Bold),
                    new Point(2,3), Color.Black);
            }

            Image = _BulletImage;
        }

        public override void OnUpdate (UpdateEventArg e)
        {
            base.OnUpdate(e);

            //切り替え済みなら何もしない
            if ( _HasSwitched ) {
                return;
            }

            if ( _FakeTime > 0 ) {
                _FakeTime -= this.Trajectries.Speed;
            } else {
                //FakeTimeを使い切ったら真のターゲットに切り替える。
                //弾のスピードを1.5倍にすると爽快感がでる？？？
                this.Trajectries.TargetPosition = _TargetPoint;

                this.Trajectries.Speed += this.Trajectries.Speed / 2;
                _HasSwitched = true;
            }
        }

    }
}
