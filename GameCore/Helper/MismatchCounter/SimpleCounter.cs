using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingShoot.GameCore.Helper.MismatchCounter
{
    class SimpleCounter<T>
    {
        private T   _Tag;
        private int _Count;

        public T Tag
        {
            get
            {
                return _Tag;
            }

            set
            {
                _Tag = value;
            }
        }

        public int Count
        {
            get
            {
                return _Count;
            }
        }

        public SimpleCounter ()
        {
            _Count = 0;
        }

        public SimpleCounter (T tag)
        {
            _Tag = tag;
            _Count = 0;
        }

        public int CountUp ()
        {
            _Count++;
            return _Count;
        }

        public void CountReset ()
        {
            _Count = 0;
        }
    }
}
