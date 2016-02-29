using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingShoot.GameCore.Helper.MismatchCounter
{
    class MismatchCounter
    {
        private const string ALPHABET_DEFAULT = "abcdefghijklmnopqrstuvwxyz";

        private string _Alphabet;
        private List<SimpleCounter<char>> _MismatchTable;

        public MismatchCounter (string alphabet = ALPHABET_DEFAULT)
        {
            _Alphabet = alphabet;
            _MismatchTable = new List<SimpleCounter<char>>(alphabet.Length);

            //カウンタの生成。
            foreach ( var ch in _Alphabet ) {
                var simpleCounter = new SimpleCounter<char>(ch);
                _MismatchTable.Add(simpleCounter);
            }
        }

        public int CountUp (char ch)
        {
            //テーブルから探す。
            for ( int i = 0; i < _MismatchTable.Count; i++ ) {
                if ( _MismatchTable[i].Tag == ch ) {
                    _MismatchTable[i].CountUp();
                    return _MismatchTable[i].Count;
                }
            }

            return -1;
        }

        public void ResetAll ()
        {
            for ( int i = 0; i < _MismatchTable.Count; i++ ) {
                _MismatchTable[i].CountReset();
            }
        }

        public SimpleCounter<char>[] ToSortedArray_OrderByDescending ()
        {
            var sorted = new List<SimpleCounter<char>>(_MismatchTable.Count);
            var linq_q = _MismatchTable.OrderByDescending(s => s.Count);

            return linq_q.ToArray();
        }

    }
}
