using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingShoot.GameCore.Helper.RingBuffer
{
    class RingBuffer<T>
    {
        private int _Capacity;
        private T _NullValue;
        private LoopCounter _ReadPointer;
        private LoopCounter _WritePointer;
        private T[] _Buffer;

        private Func<T, T, bool> _Compare; //イコールならtrueを、異なっていたらfalseを返すような関数。


        public RingBuffer(int capacity, T nullValue, Func<T,T,bool> compare)
        {
            _NullValue = nullValue;
            _Capacity = capacity;
            _Buffer = new T[capacity];

            for (int i = 0; i < _Buffer.Length; i++) {
                _Buffer[i] = _NullValue;
            }

            _ReadPointer = new LoopCounter(0, capacity - 1);
            _WritePointer = new LoopCounter(0, capacity - 1);
            _Compare = compare;
        }

        public void Write(T value)
        {
            _Buffer[_WritePointer.Count] = value;
            _WritePointer.CountUp();
        }

        //戻り値は値を変数に格納できたかどうか。
        public bool Read(out T variable)
        {
            bool isEmpty = true;

            variable = _Buffer[_ReadPointer.Count];

            if (_Compare(_NullValue,variable) == false) {
                isEmpty = false;
                _Buffer[_ReadPointer.Count] = _NullValue;
                _ReadPointer.CountUp();
            }

            return !isEmpty;
        }
    }
}
