using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingShoot.GameCore.Helper.RomanConverter
{
    public enum AutomatonState
    {
        Init,
        Correct,
        MissMatch,
        Accept
    }

    class RomanAutomaton
    {
        private const int MAX_RANGE = 4;   //一気に入力できる最大文字数。ex->っちゃ(は３文字一気に入力できる)
        private enum FState { Equal, NotEqual, Accept };

        private List<List<string>>             PatternList;
        private List<List<Func<char, FState>>> StateTransitionFunctions;
        private int                            _Cursor;
        private string[]                       _FootPrint;
        private string                         _CurrentPattern;
        private StringBuilder                  _InputLog;

        public AutomatonState State;


        private RomanAutomaton ()
        {
            this._Cursor = 0;
            this.State = AutomatonState.Init;
            this._InputLog = new StringBuilder();
        }

        public RomanAutomaton (string kanaString, JpRomanTable tbl)
            : this()
        {
            if ( String.IsNullOrEmpty(kanaString) )
                throw new AutomatonKanaStringIsNullOrEmptyException();

            this.PatternList = MakePatternList(kanaString, tbl, MAX_RANGE);
            this.StateTransitionFunctions = PatternList2Closure(PatternList);
            this._FootPrint = new string[PatternList.Count];
            this._FootPrint[0] = PatternList[0][0];
            this._CurrentPattern = PatternList[0][0];
        }

        public int SegmentCount
        {
            get
            {
                return PatternList.Count;
            }
        }

        public int CurrentSegment
        {
            get
            {
                return _Cursor;
            }
        }

        public string CurrentPattern
        {
            get
            {
                return _CurrentPattern;
            }
        }

        public string[] FootPrint
        {
            get
            {
                return _FootPrint;
            }
        }

        public string InputLog
        {
            get
            {
                return _InputLog.ToString();
            }
        }


        //for debug
        public string[] CurrentPatter2arr
        {
            get
            {
                return PatternList[_Cursor].ToArray();
            }
        }

        private List<List<string>> MakePatternList (string kanaString, JpRomanTable tbl, int analyzeRange)
        {
            var patterns = new List<List<string>>();
            var i = 0;

            while ( i < kanaString.Length ) {
                var range = i + analyzeRange <= kanaString.Length ? analyzeRange : kanaString.Length - i;
                var tpl = Analyze(kanaString.Substring(i, range), tbl);
                patterns.Add(tpl.Item2);
                i += tpl.Item1;
            }

            return patterns;
        }

        private Tuple<int, List<string>> Analyze (string kanaString, JpRomanTable tbl)
        {
            if ( String.IsNullOrEmpty(kanaString) )
                return new Tuple<int, List<string>>(0, new List<string>());

            var maxGroupSize = kanaString.Length;
            var patterns = new List<string>();
            var rmObj = new JpRoman();


            while ( tbl.TryGetJpRomanObject(kanaString.Substring(0, maxGroupSize), out rmObj) == false ) {
                maxGroupSize--;
            }

            patterns.AddRange(rmObj.Romans);

            if ( maxGroupSize > 1 ) {
                patterns.AddRange(RestPatterns(kanaString.Substring(0, maxGroupSize), tbl));
            }

            return new Tuple<int, List<string>>(maxGroupSize, patterns);
        }

        private List<string> AddEachElems (List<string> lst1, List<string> lst2)
        {
            var result = new List<string>();

            if ( lst1.Count <= 0 || lst2.Count <= 0 ) {
                result.AddRange(lst1.Count <= 0 ? lst2 : lst1);
            } else {
                foreach ( var l in lst1 )
                    foreach ( var r in lst2 )
                        result.Add(l + r);
            }
            return result;
        }

        private List<string> AddEachElems (string[] strs1, string[] strs2)
        {
            var result = new List<string>();

            if ( strs1.Length <= 0 || strs2.Length <= 0 ) {
                result.AddRange(strs1.Length <= 0 ? strs2 : strs1);
            } else {
                foreach ( var l in strs1 )
                    foreach ( var r in strs2 )
                        result.Add(l + r);
            }

            return result;
        }

        private List<string> ConcPtns (List<List<string>> patterns, int idx = 0)
        {
            if ( idx >= patterns.Count ) {
                return new List<string>();
            }

            var left = patterns[idx];
            var right = ConcPtns(patterns, idx + 1);

            return AddEachElems(left, right);
        }

        private List<string> RestPatterns (string kanaString, JpRomanTable tbl)
        {
            var result = new List<string>();

            //右結合 んじゃー＞ん　＋　じゃ
            var l_end = tbl.GetJpRomanObject(kanaString[0]);
            var right = ConcPtns(
                MakePatternList(kanaString.Substring(1, kanaString.Length - 1), tbl, kanaString.Length - 1));
            result.AddRange(AddEachElems(l_end.Romans, right.ToArray()));

            //左結合 んじゃー＞　んじ　＋　ゃ
            var r_end = tbl.GetJpRomanObject(kanaString[kanaString.Length - 1]);
            var left  = ConcPtns(
                MakePatternList(kanaString.Substring(0, kanaString.Length - 1), tbl, kanaString.Length - 1));
            var tmp = AddEachElems(left.ToArray(), r_end.Romans);
            foreach ( var x in tmp ) {
                if ( result.Contains(x) == false ) {
                    result.Add(x);
                }
            }

            return result;
        }

        public AutomatonState Input (char ch)
        {
            var results = ApplyTransitionFunctions(ch);

            //Acceptなパターンがあるか？
            for ( int i = 0; i < results.Length; i++ ) {
                if ( results[i] == FState.Accept ) {
                    _InputLog.Append(" ");
                    _InputLog.Append(_CurrentPattern);

                    if ( _Cursor >= PatternList.Count - 1 ) {
                        State = AutomatonState.Accept;
                        return AutomatonState.Accept;
                    } else {
                        _Cursor++;//最後のセグメントでないので、カーソルを進める。
                        State = AutomatonState.Correct;
                        return State;
                    }
                }
            }

            //Equalだったパターンはあるか？
            for ( int i = 0; i < results.Length; i++ ) {
                if ( results[i] == FState.Equal ) {
                    RemoveNotEqualPattern(results);
                    State = AutomatonState.Correct;
                    return State;
                }
            }

            //すべてのパターンがNotEqualだった。
            State = AutomatonState.MissMatch;
            return State;
        }

        private FState[] ApplyTransitionFunctions (char ch)
        {
            var results = new FState[StateTransitionFunctions[_Cursor].Count];

            for ( int i = 0; i < results.Length; i++ ) {
                results[i] = StateTransitionFunctions[_Cursor][i](ch);
            }

            return results;
        }

        private void RemoveNotEqualPattern (FState[] states)
        {
            var fns = StateTransitionFunctions[_Cursor].ToArray();

            for ( int i = 0; i < states.Length; i++ ) {
                if ( states[i] == FState.NotEqual )
                    StateTransitionFunctions[_Cursor].Remove(fns[i]);
            }
        }

        private List<List<Func<char, FState>>> PatternList2Closure (List<List<string>> ptns)
        {
            var lst = new List<List<Func<char, FState>>>(ptns.Count);

            for ( int i = 0; i < ptns.Count; i++ ) {
                var tmpLst = new List<Func<char, FState>>(ptns[i].Count);
                for ( int j = 0; j < ptns[i].Count; j++ ) {
                    tmpLst.Add(MakeClosure(ptns[i][j]));
                }
                lst.Add(tmpLst);
            }
            return lst;
        }

        private Func<char, FState> MakeClosure (string ptn)
        {
            int idx = 0;

            return (char ch) => {
                if ( idx >= ptn.Length )
                    return FState.Accept;

                if ( ch == ptn[idx] ) {
                    _FootPrint[_Cursor] = ptn;
                    _CurrentPattern = ptn;
                    idx++;
                    if ( idx >= ptn.Length )
                        return FState.Accept;
                    else
                        return FState.Equal;
                } else {
                    return FState.NotEqual;
                }
            };
        }

        //てきとう。
        public string GetShortestPath ()
        {
            string str = "";

            foreach ( var lst in PatternList ) {
                int minlen = int.MaxValue;
                string ptn = "";
                foreach ( var elem in lst ) {
                    if ( elem.Length < minlen ) {
                        minlen = elem.Length;
                        ptn = elem;
                    }
                }
                str += ptn;
            }
            return str;
        }

        //てきとう。
        public override string ToString ()
        {
            string str = "";
            for ( int i = 0; i < PatternList.Count; i++ ) {
                str += PatternList[i][0];
            }
            return str;
        }

        private class AutomatonKanaStringIsNullOrEmptyException : Exception
        {
            public const string error_message = "ひらがな文字列がnullまたは空文字列です。";
            public AutomatonKanaStringIsNullOrEmptyException (string msg)
                : base(error_message + Environment.NewLine + msg)
            {
            }

            public AutomatonKanaStringIsNullOrEmptyException ()
                : base(error_message)
            { }
        }
    }
}
