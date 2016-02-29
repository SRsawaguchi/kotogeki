using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingShoot.GameCore.Helper.Music
{
    class PlayList
    {

        private List<Music> _MusicList;
        private int         _CurrentIndex;

        public PlayList()
        {
            _MusicList = new List<Music>();
            _CurrentIndex = 0;
        }

        public void AppendMusic(Music music)
        {
            if(music.IsBackGroundMusic)
                _MusicList.Add(music);

        }

        public Music MoveNextMusic()
        {
            if (_CurrentIndex < _MusicList.Count) {
                _CurrentIndex++;
                return _MusicList[_CurrentIndex];
            }

            return null;
        }

        public Music GetCurrentMusic()
        {
            return _MusicList[_CurrentIndex];
        }

        public void Reset()
        {
            _CurrentIndex = 0;
        }
    }
}
