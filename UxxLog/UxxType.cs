using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UxxLog
{
    public class UxxType : IEnumerable<Word>
    {
        private int _sum;
        private Word[] _words;
        public string Uxx { get; }
        public int Sum
        {
            get => _sum;
        }
        public string[] Variants
        {
            get => Uxx.Split("-");
        }

        public Word[] Words
        {
            get { return _words; }
            set
            {
                _words = value;
                for (int i = 0; i < Words.Length; i++)
                {
                    _sum += Words[i].Score;
                }
            }
        }

        public UxxType(string uxx)
        {
            Uxx = uxx;
            _words = new Word[0];
            _sum = 0;
        }

        public Word this[int index]
        {
            get => Words[index];
        }

        public void Add(int diff)
        {
            _sum += diff;
        }

        public override string ToString()
        {
            return "[" + Uxx.ToString() + "]";
        }

        public IEnumerator<Word> GetEnumerator()
        {
            for (int i = 0; i < Words.Length; i++)
            {
                yield return Words[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
