using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingShoot.GameCore
{
    class UpdateEventArg
    {
        public Size FrameSize;
        public List<char> KeyboardInputChars;

        public UpdateEventArg (Size frameSize,List<char> keyboardInputChars)
        {
            this.FrameSize = frameSize;
            this.KeyboardInputChars = keyboardInputChars;
        }
    }
}
