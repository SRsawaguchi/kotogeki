using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypingShoot.GameCore.GameObjects.Items.Bullets;
using TypingShoot.GameCore.Helper.RomanConverter;
using TypingShoot.GameCore.TypingData;
using TypingShoot.GameCore.Helper.MismatchCounter;

namespace TypingShoot.GameCore.GameObjects.Soldiers
{
    class Enemy : Soldier
    {
        public static readonly float TEXT_SIZE_DEFAULT = 18.0f;
        public static readonly string DEFAULT_FONT_NAME = "ＭＳ ゴシック";

        private int                    _RestSentenceCount;
        private TypingSentence[]       _Sentences;
        private int                    _CurrentSentencePointer;
        private RomanAutomaton         _Automaton;
        private JpRomanTable           _JpRomanTable;
        private Font                   _Font;
        private MismatchCounter        _MismatchCounter;
        private StringBuilder          _CorrectCharacters;

        public int RestSentenceCount
        {
            get
            {
                return _RestSentenceCount;
            }
        }

        public int CurrentSentencePointer
        {
            get
            {
                return _CurrentSentencePointer;
            }
        }

        public TypingSentence CurrentSentence
        {
            get
            {
                return _Sentences[_CurrentSentencePointer];
            }
        }

        public string SamplePath
        {
            get
            {
                return _Automaton.ToString();
            }
        }

        public MismatchCounter MismatchCount
        {
            get
            {
                return _MismatchCounter;
            }
        }

        public Enemy (Point pt, TypingSentence[] sentensces,JpRomanTable tbl)
            :base(pt)
        {
            _Sentences              = sentensces;
            _RestSentenceCount      = sentensces.Length;
            _CurrentSentencePointer = 0;
            _JpRomanTable           = tbl;
            _Font                   = new Font(DEFAULT_FONT_NAME, TEXT_SIZE_DEFAULT);
            _CorrectCharacters      = new StringBuilder();
            _MismatchCounter        = new MismatchCounter();
            Radius = 50;
        }

        private Bitmap DrawSentenceImage ()
        {
            var targetSentence = new StringBuilder();

            targetSentence.Append(CurrentSentence.DisplayString);
            targetSentence.Append("\n");
            targetSentence.Append(CurrentSentence.KanaString);
            targetSentence.Append("\n");
            targetSentence.Append(_CorrectCharacters.ToString());

            return DrawSentenceImage(targetSentence.ToString());
        }

        private Bitmap DrawSentenceImage (string str)
        {
            var bmpSize = TextRenderer.MeasureText(str, _Font);
            var bmp     = new Bitmap(bmpSize.Width, bmpSize.Height);

            using ( Graphics g = Graphics.FromImage(bmp) ) {
                TextRenderer.DrawText(g, str ,_Font, new Point(0, 0), Color.Black);
            }

            return bmp;
        }

        //オーバーライドしやすいようにメソッド分けしておく。
        private void OnCollisionKanaBullet (char inputChar)
        {
            var state = _Automaton.Input(inputChar);

            switch ( state ) {
                case AutomatonState.Accept:
                    OnAutomatonStateAccept(inputChar);
                    break;
                case AutomatonState.Correct:
                    OnAutomatonStateCorrect(inputChar);
                    break;
                case AutomatonState.MissMatch:
                    OnAutomatonStateMissMath(inputChar);
                    break;
                default :
                    break;
            }
        }

        private void OnAutomatonStateAccept (char inputChar)
        {
            
            _RestSentenceCount--;

            if ( _RestSentenceCount <= 0 ) {
                this.Image = DrawSentenceImage("ゲームオーバー");
                Manager.NortifyLose(this);
            } else {
                _CurrentSentencePointer++;
                _Automaton = new RomanAutomaton(_Sentences[_CurrentSentencePointer].KanaString,
                                                _JpRomanTable);
                _CorrectCharacters.Clear();
                this.Image = DrawSentenceImage();
                
            }
        }

        private void OnAutomatonStateCorrect (char inputChar)
        {
            _CorrectCharacters.Append(inputChar);
        }

        private void OnAutomatonStateMissMath (char inputChar)
        {
            _MismatchCounter.CountUp(inputChar);
        }


        public override void OnCreate ()
        {
            _Automaton = new RomanAutomaton(_Sentences[_CurrentSentencePointer].KanaString,
                                            _JpRomanTable);
            this.Image = DrawSentenceImage();
            Manager.RequestAddColliders(this);
        }

        public override void OnUpdate (UpdateEventArg e)
        {
            this.Image = DrawSentenceImage();
        }

        public override void OnDestroy ()
        {
        }

        public override void OnCollision (GameObject obj)
        {
            if ( obj is KanaBullet ) {
                var kanaBullet = (KanaBullet)obj;
                OnCollisionKanaBullet(kanaBullet.Kana);
            }
        }

    }
}
