using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TypingShoot.GameCore.Helper.RingBuffer;
using TypingShoot.GameCore.Helper.Music;
using TypingShoot.GameCore.GameObjects.Items.Bullets;

namespace TypingShoot.GameCore.GameObjects.Soldiers
{
    class PlayerGirl : Player
    {
        private const int     BULLET_SPEED = 10;
        private const int     FAKE_WEIGHT = 10;

        private Enemy        _Enemy;
        private Muzzle[]     _Muzzles;
        private Muzzle       _CurrentMuzzle;
        private LoopCounter  _MuzzleIndex;
        private EventCounter _MuzzleSwitcher;
        private Bitmap       _BulletImage;

        public PlayerGirl(Point myPosition, Enemy enemy,Muzzle[] muzzles,Bitmap bulletImage)
            :base(myPosition)
        {
            this.Position = myPosition;
            _Enemy = enemy;
            _Muzzles = muzzles;
            _BulletImage = bulletImage;

            _MuzzleIndex = new LoopCounter(0, _Muzzles.Length - 1);
            _MuzzleSwitcher = new EventCounter(_Muzzles.Length - 1, () => { SwitchMuzzle(); });
            SwitchMuzzle();

        }

        private void SwitchMuzzle()
        {
            _MuzzleIndex.CountUp();

            _CurrentMuzzle = _Muzzles[_MuzzleIndex.Count];
            Image = _CurrentMuzzle.Image;
        }

        public override void OnUpdate(UpdateEventArg e)
        {
            //文字が入力されていたらBulletを生成。
            if (e.KeyboardInputChars.Count > 0) {
                _MuzzleSwitcher.CountUp();

                foreach (var ch in e.KeyboardInputChars) {
                    var bullet = new FakeBullet(_CurrentMuzzle.Position, _Enemy.GetCenterPoint(),
                        _CurrentMuzzle.OffsetX, _CurrentMuzzle.OffsetY, BULLET_SPEED, FAKE_WEIGHT,new Bitmap(_BulletImage), ch);

                    Manager.RequestVisible(bullet);
                }
            }
        }




    }
}
