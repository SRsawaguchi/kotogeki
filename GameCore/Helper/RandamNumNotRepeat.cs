using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingShoot.GameCore.Helper
{
    class RandamNumNotRepeat
    {
        int[] RdmTable;
        int NextIndex;

        // Random クラスのインスタンスを生成する
        System.Random cRandom = new System.Random();

        //コンストラクター
        public RandamNumNotRepeat(int n)
        {
            RdmTable = new int[n + 1];
            NextIndex = 0;
            SetTable(RdmTable);
        }

        //乱数を取得する
        public int Next()
        {
            if (NextIndex > RdmTable.Length - 1) return -1;  //テーブル末尾以降は-1を返す
            else return RdmTable[NextIndex++];
        }

        public void Reset()
        {
            NextIndex = 0;
            SetTable(RdmTable);
        }

        //インデクサー
        public int this[int m]
        {
            get
            {
                if (m < RdmTable.Length) return RdmTable[m];
                else return -1;
            }
            private set { ;}
        }

        private void SetJudgeAry(int[] ary)
        {
            for (int i = 0; i < ary.Length; i++) ary[i] = i;
        }

        private void SetRdmNum(int[] ary, int o)
        {
            if (o > 1) {
                int r = cRandom.Next(o);
                if (r != o) OnceRotateAry(ary, o, r);
                SetRdmNum(ary, o - 1);
            }
        }

        private void OnceRotateAry(int[] ary, int n, int m)
        {
            int tmp = ary[m];
            for (int i = m; i < n - 1; i++) ary[i] = ary[i + 1];
            ary[n - 1] = tmp;
        }

        private void SetTable(int[] ary)
        {
            SetJudgeAry(ary);
            SetRdmNum(ary, ary.Length);
        }
    }
}
