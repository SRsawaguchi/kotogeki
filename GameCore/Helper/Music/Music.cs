using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingShoot.GameCore.Helper.Music
{
    class Music
    {
        private string _Title;
        private string _FilePath;
        private bool _IsBackGroundMusic;

        public string Title
        {
            get
            {
                return _Title;
            }
        }

        public string FilePath
        {
            get
            {
                return _FilePath;
            }
        }

        public bool IsBackGroundMusic
        {
            get
            {
                return _IsBackGroundMusic;
            }
        }

        public Music(string title, string filePath, bool isBGM)
        {
            _Title = title;
            _FilePath = filePath;
            _IsBackGroundMusic = isBGM;
        }
    }
}
