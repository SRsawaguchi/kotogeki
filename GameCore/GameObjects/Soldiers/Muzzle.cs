using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TypingShoot.GameCore.GameObjects.Soldiers
{
    class Muzzle
    {
        public Bitmap Image { get; set;}
        public Point Position { set; get; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public int Speed { get; set; }

        public Muzzle(Bitmap image, Point position,int offsetX,int offsetY)
        {
            Image = image;
            Position = position;
            OffsetX = offsetX;
            OffsetY = offsetY;
        }
    }
}
