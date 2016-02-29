using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingShoot.GameCore.TypingData
{
    class TypingSentence
    {
        public readonly string DisplayString;
        public readonly string KanaString;
        //public readonly int    Level;


        public TypingSentence (string displayString, string kanaString)
        {
            DisplayString = displayString;
            KanaString    = kanaString;

            //レベルを求めるロジックを追加する。
        }
    }
}
