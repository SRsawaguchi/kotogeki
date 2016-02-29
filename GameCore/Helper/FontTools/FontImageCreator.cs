using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypingShoot.GameCore.Helper.FontTools
{
    class FontImageCreator
    {
        public static readonly string ZenkakuText = "龍";
        public static readonly string HankakuText = "A";
        public static readonly float TextSizeDefault = 9.0f;
        public static readonly string DefaultFontName = "ＭＳ ゴシック";
        public static readonly Color BackGroundColorDefault = Color.White;
        public static readonly Color FontColorDefault = Color.Black;

        //Properties
        private Color _FontColor;
        private Color _BackGroundColor;
        private Font _Font;

        public Color BackGroundColor
        {
            get
            {
                return _BackGroundColor;
            }
            set
            {
                _BackGroundColor = value;
            }
        }

        public Color FontColor
        {
            get
            {
                return _FontColor;
            }
            set
            {
                _FontColor = value;
            }
        }

        public Font Font
        {
            get
            {
                return _Font;
            }
            set
            {
                Font = value;
            }
        }

        public Size ZenkakuSize
        {
            get
            {
                return TextRenderer.MeasureText(ZenkakuText, _Font);
            }
        }

        public Size HankakuSize
        {
            get
            {
                return TextRenderer.MeasureText(HankakuText, _Font);
            }
        }


        //Constructors
        public FontImageCreator ()
        {
            _BackGroundColor = BackGroundColorDefault;
            _Font = new Font(DefaultFontName, TextSizeDefault);
            this.FontColor = FontColorDefault;
        }

        public FontImageCreator (Font font, Color bgColor, Color fntColor)
        {
            _BackGroundColor = bgColor;
            _Font = font;
        }



        //Methods
        public Bitmap CharToBitmap (char ch)
        {
            return CharToBitmap(ch, GetSize(ch));
        }


        public Bitmap CharToBitmap (char ch, Size bmpSize)
        {
            var canvas = new Bitmap(bmpSize.Width, bmpSize.Height);

            using ( Graphics g = Graphics.FromImage(canvas) ) {
                g.Clear(_BackGroundColor);
                TextRenderer.DrawText(g, ch.ToString(), _Font, new Point(0, 0), _FontColor);
            }

            return canvas;
        }


        public ImageList StringToBitmap (string str, Size bmpSize)
        {
            var result = new ImageList();
            result.ImageSize = bmpSize;

            foreach ( char ch in str ) {
                result.Images.Add(CharToBitmap(ch, bmpSize));
            }

            return result;
        }


        public Size GetSize (char ch)
        {
            return TextRenderer.MeasureText(ch.ToString(), _Font);
        }


        public static Size GetSize (char ch, Font ft)
        {
            return TextRenderer.MeasureText(ch.ToString(), ft);
        }


        public Bitmap Trimming (Bitmap bmp, Rectangle rect)
        {
            var result = new Bitmap(rect.Width, rect.Height, bmp.PixelFormat);

            using ( Graphics g = Graphics.FromImage(result) )
                g.DrawImage(bmp, 0, 0, rect, GraphicsUnit.Pixel);
            {
            }
            return result;
        }


        public ImageList TrimmingList (ImageList imgLst, Rectangle rect)
        {
            var result = new ImageList();
            result.ImageSize = rect.Size;

            foreach ( Bitmap bmp in imgLst.Images ) {
                result.Images.Add(Trimming(bmp, rect));
            }
            return result;
        }


        public Bitmap TrimmingBlank (Bitmap bmp)
        {
            var blk = new FontBlank(bmp, _BackGroundColor, FontColor);
            return Trimming(bmp, blk.TrimBlankArea);
        }


        public ImageList TrimmingBlankList (ImageList imgList)
        {
            var blk = FontBlank.GetMinBlank(imgList, _BackGroundColor, FontColor);

            return TrimmingList(imgList, blk.TrimBlankArea);
        }


        public Bitmap AddMargin (Bitmap bmp, int margin)
        {
            var result = new Bitmap(bmp.Width + margin * 2,
                                    bmp.Height + margin * 2);

            using ( Graphics g = Graphics.FromImage(result) ) {
                g.Clear(_BackGroundColor);
                g.DrawImage(bmp, 1, 1, bmp.Width, bmp.Height);
            }

            return result;
        }


        public ImageList AddMarginList (ImageList imgList, int margin)
        {
            var result = new ImageList();
            var size = new Size(imgList.ImageSize.Width + margin * 2,
                                imgList.ImageSize.Height + margin * 2);


            result.ImageSize = size;
            foreach ( Bitmap bmp in imgList.Images ) {
                var canvas = new Bitmap(size.Width, size.Height);
                using ( Graphics g = Graphics.FromImage(canvas) ) {
                    g.Clear(_BackGroundColor);
                    g.DrawImage(bmp, 1, 1, bmp.Width, bmp.Height);
                }
                result.Images.Add(canvas);
            }

            return result;
        }

    }
}
