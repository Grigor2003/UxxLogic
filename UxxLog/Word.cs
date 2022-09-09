using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UxxLog
{
    public class Word
    {
        public UxxType Type { get; }
        public string C { get; }
        public byte Answer { get; }
        private int _score;
        private int load_score;

        public static Word Empty
        {
            get => new Word("empty,0,0", new UxxType("empty"));
        }
        public string Right
        {
            get => C.Replace("_", Type.Variants[Answer]);
        }
        public int Score
        {
            get { return _score; }
            set { _score = Math.Clamp(value, 1, 10000); }
        }
        public Word(string data_row, UxxType type)
        {
            // "bar_v,1,100;"
            var splitten = data_row.Split(",");
            C = splitten[0];
            Answer = byte.Parse(splitten[1]);
            Score = int.Parse(splitten[2]);
            load_score = Score;
            Type = type;
        }

        public void Effect(bool error, double right = 0.25f, double left = 0.2f)
        {
            int new_score = (int)Math.Round(Score * (1 + (error ? -left : right)));
            this.Type.Add(new_score - Score);
            Score = new_score;
        }
        public string ToWrite()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(C);
            sb.Append(',');
            sb.Append(Answer);
            sb.Append(',');
            sb.Append(Score);
            return sb.ToString();
        }
        public override string ToString()
        {
            return $"{Right} : {load_score} -> {Score}";
        }
    }
}
