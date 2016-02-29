using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypingShoot.GameCore.Helper.FontTools
{
    class FontBlank
    {
        private Color _BackGroundColor;
        private Color _FontColor;
        private int _Left;
        private int _Right;
        private int _Top;
        private int _Bottom;
        private int _Width;
        private int _Height;
        private Rectangle _Rect;
        private Size _OriginalSize;


        /* Properties */
        public Color BackGroundColor
        {
            get
            {
                return _BackGroundColor;
            }
        }

        public Color FontColor
        {
            get
            {
                return _FontColor;
            }
        }

        public int LeftBlank
        {
            get
            {
                return _Left;
            }
        }

        public int RightBlank
        {
            get
            {
                return _Right;
            }
        }

        public int TopBlank
        {
            get
            {
                return _Top;
            }
        }

        public int BottomBlank
        {
            get
            {
                return _Bottom;
            }
        }

        public int TrimBlankWidth
        {
            get
            {
                return _Width;
            }
        }

        public int TrimBlankHeight
        {
            get
            {
                return _Height;
            }
        }

        public Size TrimBlankSize
        {
            get
            {
                return new Size(_Width, _Height);
            }
        }

        public Rectangle TrimBlankArea
        {
            get
            {
                return _Rect;
            }
        }

        public Size OriginalSize
        {
            get
            {
                return _OriginalSize;
            }
        }

        /* Constructors */
        public FontBlank (Bitmap img, Color bgColor, Color fntColor)
        {
            _BackGroundColor = bgColor;
            _FontColor = fntColor;
            _OriginalSize = img.Size;

            _Left = getBlankLeft(img);
            _Right = getBlankRight(img);
            _Top = getBlankTop(img);
            _Bottom = getBlankBottom(img);

            _Width = img.Width - _Left - _Right;
            _Height = img.Height - _Top - _Bottom;
            _Rect = new Rectangle(_Left, _Top, _Width, _Height);
        }


        private FontBlank (Size orgSize, int l, int r, int t, int b, Color bgColor, Color fntColor)
        {
            _OriginalSize = orgSize;
            _BackGroundColor = bgColor;
            _FontColor = fntColor;

            _Left = l;
            _Right = r;
            _Top = t;
            _Bottom = b;

            _Width = orgSize.Width - _Left - _Right;
            _Height = orgSize.Height - _Top - _Bottom;
            _Rect = new Rectangle(_Left, _Top, _Width, _Height);
        }


        /* Methods */
        public static FontBlank GetMinBlank (ImageList imgLst, Color bgColor, Color fntColor)
        {
            int left = imgLst.ImageSize.Width;
            int right = imgLst.ImageSize.Width;
            int top = imgLst.ImageSize.Height;
            int bottom = imgLst.ImageSize.Height;

            foreach ( Bitmap bmp in imgLst.Images ) {
                var blk = new FontBlank(bmp, bgColor, fntColor);
                left = Math.Min(left, blk.LeftBlank);
                right = Math.Min(right, blk.RightBlank);
                top = Math.Min(top, blk.TopBlank);
                bottom = Math.Min(bottom, blk.BottomBlank);
            }
            return new FontBlank(imgLst.ImageSize, left, right, top, bottom, bgColor, fntColor);
        }


        private static bool isSameColor (Color c1, Color c2)
        {
            return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
        }


        private int getBlankLeft (Bitmap bmp)
        {
            for ( int x = 0; x < bmp.Width; x++ ) {
                for ( int y = 0; y < bmp.Height; y++ ) {
                    if ( isSameColor(bmp.GetPixel(x, y), FontColor) ) {
                        return x;
                    }
                }
            }
            return bmp.Width;
        }


        private int getBlankRight (Bitmap bmp)
        {
            for ( int x = bmp.Width - 1; x >= 0; x-- ) {
                for ( int y = 0; y<bmp.Height; y++ ) {
                    if ( isSameColor(bmp.GetPixel(x, y), FontColor) ) {
                        return bmp.Width - x - 1;
                    }
                }
            }
            return bmp.Width;
        }


        private int getBlankTop (Bitmap bmp)
        {
            for ( int y = 0; y<bmp.Height; y++ ) {
                for ( int x = 0; x < bmp.Width; x++ ) {
                    if ( isSameColor(bmp.GetPixel(x, y), FontColor) ) {
                        return y;
                    }
                }
            }
            return bmp.Height;
        }


        private int getBlankBottom (Bitmap bmp)
        {
            for ( int y = bmp.Height - 1; y >= 0; y-- ) {
                for ( int x = 0; x < bmp.Width; x++ ) {
                    if ( isSameColor(bmp.GetPixel(x, y), FontColor) ) {
                        return bmp.Height - y - 1;
                    }
                }
            }
            return bmp.Height;
        }
    }
}
