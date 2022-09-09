using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UxxLog
{
    public class DataProcess : IEnumerable<UxxType>
    {
        //public Dictionary<string, Word[]> Data { get; set; } = new Dictionary<string, Word[]>();
        //private Dictionary<string, Word[]> _subData = new Dictionary<string, Word[]>();
        public List<UxxType> Uxxs { get; set; }
        private List<bool> _mask;
        private Random random;


        public DataProcess(string packPath)
        {
            Uxxs = new List<UxxType>();
            random = new Random();
            foreach (var uxxPath in Directory.GetFiles(packPath))
            {
                if (File.Exists(uxxPath) && uxxPath.EndsWith(".txt"))
                {
                    var lines = File.ReadAllLines(uxxPath);

                    var uxx = lines[0];
                    var words = new Word[lines.Length - 1];
                    var uxxType = new UxxType(uxx);

                    for (int i = 1; i < lines.Length; i++)
                    {
                        words[i - 1] = new Word(lines[i], uxxType);
                    }
                    uxxType.Words = words;
                    Uxxs.Add(uxxType);
                }
            }
            _mask = Enumerable.Repeat<bool>(true, Uxxs.Count()).ToList();
        }

        public void Save(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            foreach (var uxx in Uxxs)
            {
                StringBuilder text = new StringBuilder();
                text.AppendLine(uxx.Uxx);
                foreach (var word in uxx)
                {
                    text.AppendLine(word.ToWrite());
                }
                var fileName = Path.Combine(path, uxx.Uxx + ".txt");
                File.WriteAllText(fileName, text.ToString());
            }
        }

        public bool SetMask(IEnumerable<bool> mask)
        {
            if (mask.Count() == this.Uxxs.Count())
            {
                _mask = mask.ToList();
                return true;
            }
            else
            {
                return false;
            }

        }

        public Word Next()
        {
            int maskSum = 0;
            foreach (var item in this)
            {
                maskSum += item.Sum;
            }

            var take = random.Next(maskSum);
            UxxType currUxx = this[0];
            Word currWord = currUxx[0];

            foreach (var item in this)
            {
                currUxx = item;
                if (take < currUxx.Sum)
                    break;
                else
                    take -= currUxx.Sum;
            }

            foreach (var item in currUxx)
            {
                currWord = item;
                if (take < currWord.Score)
                    break;
                else
                    take -= currWord.Score;
            }

            return currWord;
        }

        public UxxType this[int index]
        {
            get => Uxxs[index];
        }

        public IEnumerator<UxxType> GetEnumerator()
        {
            for (int i = 0; i < this.Uxxs.Count(); i++)
            {
                if (_mask[i])
                {
                    yield return Uxxs[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
