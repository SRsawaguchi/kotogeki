using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingShoot.GameCore.Helper.RomanConverter
{
    class JpRoman
    {
        public readonly string JpKana;
        public readonly int PtnCount;
        public readonly string[] Romans;

        public JpRoman ()
        {
        }

        public JpRoman (string jpKana, int ptnCount, string[] romans)
        {
            this.JpKana = jpKana;
            this.PtnCount = ptnCount;
            this.Romans = romans;
        }
    }
}
