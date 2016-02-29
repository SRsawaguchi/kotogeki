using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TypingShoot.GameCore.GameObjects.Soldiers;
using TypingShoot.GameCore.Helper.RomanConverter;
using TypingShoot.GameCore.Helper.RingBuffer;
using TypingShoot.GameCore.Helper.Music;

namespace TypingShoot.GameCore
{
    class GameManager
    {
        private static GameManager Me = null;
        private static int BUFFER_CAPACITY = 100; //十分すぎるバッファ。1フレームに100文字を入力することはできない。

        private List<GameObject> DestroyNextFrame;
        private List<GameObject> VisibleNextFrame;
        private List<GameObject> VisibleGameObjects;
        private List<GameObject> Colliders;
        private Music _BGM;


        private RingBuffer<char> InputBuffer;

        private bool             _IsGameOver;

        public bool IsGameOver
        {
            get
            {
                return _IsGameOver;
            }
        }

        public void SetBGM (Music music)
        {
            _BGM = music;
        }

        public static GameManager GetObject ()
        {
            if ( Me == null ) {
                Me = new GameManager();
            }

            return Me;
        }

        private GameManager ()
        {
            DestroyNextFrame   = new List<GameObject>();
            VisibleNextFrame   = new List<GameObject>();
            VisibleGameObjects = new List<GameObject>();
            Colliders          = new List<GameObject>();
            InputBuffer = new RingBuffer<char>(BUFFER_CAPACITY, char.MaxValue,
                //equivalent test
                (char c1,char c2) => {
                    return c1 == c2;
                });

            _IsGameOver         = false;
        }

        public void ResetGame ()
        {
            DestroyNextFrame.Clear();
            VisibleNextFrame.Clear();
            VisibleGameObjects.Clear();
            Colliders.Clear();

            _IsGameOver = false;
        }

        public void UpdateFrame (Size frameSize, Graphics g)
        {
            if (_IsGameOver == true) {
                return;
            }
                AddVisibleObjects();
                DestroyObject();
                CallCollision();
                CallUpdate(frameSize);
                DrawFrame(g);
        }

        private void DestroyObject ()
        {
            foreach ( var obj in DestroyNextFrame ) {
                obj.OnDestroy();
                obj.IsDestroyed = true;
                VisibleGameObjects.Remove(obj);
                if ( Colliders.Contains(obj) ) {
                    Colliders.Remove(obj);
                }
                obj.Dispose();
            }

            DestroyNextFrame.Clear();
        }

        private void AddVisibleObjects ()
        {
            foreach ( var obj in VisibleNextFrame ) {
                obj.OnCreate();
                VisibleGameObjects.Add(obj);
            }

            VisibleNextFrame.Clear();
        }

        private void CallUpdate (Size frameSize)
        {
            var inputList = new List<char>();

            char ch;
            while (InputBuffer.Read(out ch)) {
                inputList.Add(ch);
            }

            var arg = new UpdateEventArg(frameSize,inputList);

            foreach ( var obj in VisibleGameObjects ) {
                obj.OnUpdate(arg);
            }
        }

        private void CallCollision ()
        {
            foreach ( var tpl in CollisionDetection() ) {
                tpl.Item1.OnCollision(tpl.Item2);
                tpl.Item2.OnCollision(tpl.Item1);
            }
        }

        private void DrawFrame (Graphics g)
        {
            foreach ( var obj in VisibleGameObjects ) {
                g.DrawImage(obj.Image, obj.Position);
            }
        }

        private List<Tuple<GameObject, GameObject>> CollisionDetection ()
        {
            var result = new List<Tuple<GameObject, GameObject>>(Colliders.Count);
            for ( int i = 0; i<Colliders.Count - 1; i++ ) {
                for ( int j = i + 1; j < Colliders.Count; j++ ) {
                    if ( IsHit(Colliders[i], Colliders[j]) ) {
                        result.Add(new Tuple<GameObject, GameObject>(Colliders[i], Colliders[j]));
                    }
                }
            }

            return result;
        }

        private bool IsHit (GameObject obj1, GameObject obj2)
        {
            var obj1Center = obj1.GetCenterPoint();
            var obj2Center = obj2.GetCenterPoint();

            int diameter = (int)Math.Pow(obj1.Radius + obj2.Radius, 2);
            int distance = (int)( Math.Pow(obj1Center.X - obj2Center.X, 2) + Math.Pow(obj1Center.Y - obj2Center.Y, 2) );

            return distance < diameter;
        }

        public void RequestAddColliders (GameObject caller)
        {
            if ( Colliders.Contains(caller) == false ) {
                Colliders.Add(caller);
            }
        }

        public void RequestRemoveInColliders (GameObject caller)
        {
            if ( Colliders.Contains(caller) ) {
                Colliders.Remove(caller);
            }
        }

        public void RequestDestroy (GameObject caller)
        {
            if ( DestroyNextFrame.Contains(caller) == false ) {
                DestroyNextFrame.Add(caller);
                caller.IsRequestedDestroy = true;
            }
        }

        public void RequestVisible (GameObject obj)
        {
            if ( VisibleGameObjects.Contains(obj) == false ||
                 VisibleNextFrame.Contains(obj)   == false) {
                VisibleNextFrame.Add(obj);
                obj.IsRequestedDestroy = false;
            }
        }

        public void AddKeyboardInputChar (char ch)
        {
            //KeyboardInputChars.Add(ch);
            InputBuffer.Write(ch);
        }

        public void NortifyLose (Soldier caller)
        {
            //現時点では、Enemyの敗北以外は無視。
            if ( ( caller is Enemy )) {
                _IsGameOver = true;
                return;
            }
            //未実装。
        }
    }
}
