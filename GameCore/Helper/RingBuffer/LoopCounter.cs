using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingShoot.GameCore.Helper.RingBuffer
{
    class LoopCounter
    {
        private int _Begin;
        private int _End;
        private int _Count;

        public int Count
        {
            get
            {
                return _Count;
            }
        }

        public LoopCounter(int begin, int end)
        {
            _Begin = begin;
            _End = end;
            _Count = begin;
        }

        public int CountUp()
        {
            if (_Count >= _End) {
                _Count = _Begin;
                return _Count;
            }

            _Count++;
            return _Count;
        }

    }
}
