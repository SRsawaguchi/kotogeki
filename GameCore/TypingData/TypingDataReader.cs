using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingShoot.GameCore.TypingData
{
    static class TypingDataReader
    {
        private const int DISPLAY_STRING = 0;
        private const int KANA_STRING    = 1;

        public static List<TypingSentence> MakeFromCsv (string csvFilePath, string encordingType)
        {
            var sentenceList = new List<TypingSentence>();

            using ( TextFieldParser parser =
                new TextFieldParser(csvFilePath, System.Text.Encoding.GetEncoding(encordingType)) ) {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while ( !parser.EndOfData ) {
                    string[] row = parser.ReadFields();
                    var sentence = new TypingSentence(row[DISPLAY_STRING], row[KANA_STRING]);

                    sentenceList.Add(sentence);
                }
            }

            return sentenceList;
        }
    }
}
