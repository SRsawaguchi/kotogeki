using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingShoot.GameCore.Helper.Music
{
    class EventCounter
    {
        private int _Count;
        private int _Interval;
        private Action _Event;

        public EventCounter(int interval, Action eventAction)
        {
            _Count = 0;
            _Interval = interval;
            _Event = eventAction;
        }

        public void CountUp()
        {
            _Count++;

            if (_Count > _Interval) {
                _Event();
                _Count = 0;
            }
        }

        public void CountReset()
        {
            _Count = 0;
        }

    }
}
