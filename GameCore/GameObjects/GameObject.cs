using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingShoot.GameCore
{
    abstract class GameObject : IDisposable
    {
        public Point Position { get; set; }
        public GameManager Manager { get; set; }
        public Bitmap Image { get; set; }
        public bool IsRequestedDestroy { get; set; }
        public bool IsDestroyed { get; set; }
        public int Radius { get; set; }

        public GameObject (Point pt)
        {
            this.Position = pt;
            this.Manager = GameManager.GetObject();
            this.IsRequestedDestroy = false;
            this.IsDestroyed = false;

            //デフォルト画像として、四角形を描く。
            var bmp = new Bitmap(20, 20);
            using ( Graphics g = Graphics.FromImage(bmp) ) {
                g.DrawRectangle(Pens.BlueViolet, new Rectangle(0, 0, 19, 19));
            }

            this.Image = bmp;
            

            //衝突半径を求める。
            var centerPt = GetCenterPoint();
            int distance = (int)Math.Sqrt(
                Math.Pow(centerPt.X - this.Position.X, 2) + Math.Pow(centerPt.Y - this.Position.Y, 2));
            this.Radius = distance;
        }

        public GameObject (Point pt, Bitmap img)
        {
            this.Position = pt;
            this.Manager = GameManager.GetObject();
            this.IsRequestedDestroy = false;
            this.IsDestroyed = false;
            this.Image = img;

            //衝突半径を求める。
            var centerPt = GetCenterPoint();
            int distance = (int)Math.Sqrt(
                Math.Pow(centerPt.X - this.Position.X, 2) + Math.Pow(centerPt.Y - this.Position.Y, 2));
            this.Radius = distance;
        }

        public GameObject (Point pt, Bitmap img, int radius)
        {
            this.Position = pt;
            this.Manager = GameManager.GetObject();
            this.IsRequestedDestroy = false;
            this.IsDestroyed = false;
            this.Image = img;
            this.Radius = radius;
        }

        public virtual Point GetCenterPoint()
        {
            //中央点を求める。
            int centerX = Position.X + this.Image.Width / 2;
            int centerY = Position.Y + this.Image.Height / 2;

            return new Point(centerX, centerY);
        }

        abstract public void OnCreate ();

        abstract public void OnUpdate (UpdateEventArg e);

        abstract public void OnCollision (GameObject obj);

        abstract public void OnDestroy ();

        public void Dispose ()
        {
            Image.Dispose();
        }
    }
}
