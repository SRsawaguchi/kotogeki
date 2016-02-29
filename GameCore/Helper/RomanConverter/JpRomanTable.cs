using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace TypingShoot.GameCore.Helper.RomanConverter
{
    class JpRomanTable
    {
        private const int KANA = 0;
        private const int CNT = 1;
        private const int START_ROMANS = 2;

        private SortedDictionary<string, JpRoman> tbl;

        private JpRomanTable (SortedDictionary<string, JpRoman> tbl)
        {
            this.tbl = tbl;
        }

        public static JpRomanTable MakeFromFile (string csvFilePath, string encordingType)
        {
            var tbl = new SortedDictionary<string, JpRoman>();

            using ( TextFieldParser parser =
                new TextFieldParser(csvFilePath, System.Text.Encoding.GetEncoding(encordingType)) ) {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while ( !parser.EndOfData ) {
                    string[] row = parser.ReadFields();
                    tbl.Add(GetJpKanaFromSvcRow(row),
                        new JpRoman(GetJpKanaFromSvcRow(row),
                                    GetPtnCountFromSvcRow(row),
                                    GetRomansFromSvcRow(row)));
                }

                //CSVの特性上コンマはデータに含めないので、プログラムで追加する。
                var comma_JpRoman =  new JpRoman("、", 1, new string[] { "," });
                tbl.Add(comma_JpRoman.JpKana, comma_JpRoman);
            }

            return new JpRomanTable(tbl);
        }


        public JpRoman GetJpRomanObject (string jpkana)
        {
            JpRoman jr = new JpRoman();

            if ( tbl.TryGetValue(jpkana, out jr) ) {
                return jr;
            } else {
                throw new PatternNotFoundException(
                    "ひらがな[" + jpkana + "] はローマ字変換テーブルに含まれていません。");
            }
        }

        public JpRoman GetJpRomanObject (char kana)
        {
            return GetJpRomanObject(kana.ToString());
        }

        public bool TryGetJpRomanObject (string jpKana, out JpRoman obj)
        {
            return tbl.TryGetValue(jpKana, out obj);
        }

        public bool TryGetJpRomanObject (char kana, out JpRoman obj)
        {
            return TryGetJpRomanObject(kana.ToString(), out obj);
        }

        public Boolean ContainsKana (String jpKana)
        {
            return tbl.ContainsKey(jpKana);
        }

        public override string ToString ()
        {
            var cnt = 0;
            var str = "";

            foreach ( var x in tbl.Values ) {
                cnt++;
                str += cnt + "|+" + x.JpKana + "  " + x.PtnCount + " [ ";
                for ( int i = 0; i < x.PtnCount; i++ )
                    str += x.Romans[i] + " ";
                str += " ]" + Environment.NewLine;
            }
            return str;
        }

        private static string GetJpKanaFromSvcRow (string[] row)
        {
            return row[KANA];
        }

        private static int GetPtnCountFromSvcRow (string[] row)
        {
            return int.Parse(row[CNT]);
        }

        private static string[] GetRomansFromSvcRow (string[] row)
        {
            int ptnCnt = GetPtnCountFromSvcRow(row);
            string[] romans = new String[ptnCnt];

            for ( int i = 0; i < ptnCnt; i++ ) {
                romans[i] = row[i + START_ROMANS];
            }

            return romans;
        }
    }

    class PatternNotFoundException : Exception
    {
        public PatternNotFoundException (string msg)
            : base(msg)
        {
        }
    }
}
