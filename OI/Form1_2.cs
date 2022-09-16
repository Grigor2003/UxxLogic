using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UxxLog;
using System.Diagnostics;

namespace OI
{
    partial class Form1 : Form
    {
        private void resize_buttons()
        {
            button1.Width = panel1.Width / 3;
            button2.Width = panel1.Width / 3;
            button3.Width = panel1.Width / 3;
        }

        private string update_move()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            currWord = data.Next();
            sw.Stop();
            UxxType uxx = currWord.Type;

            textBox1.Text = currWord.C;
            if (uxx.Variants.Count() > 2)
            {
                button1.Text = uxx.Variants[0];
                button2.Text = uxx.Variants[1];
                button3.Text = uxx.Variants[2];
            }
            else
            {
                button1.Text = uxx.Variants[0];
                button2.Text = "";
                button3.Text = uxx.Variants[1];
            }
            return sw.Elapsed.TotalMilliseconds.ToString();
        }

        private void move_maked(int answer = -1)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            UxxType uxx = currWord.Type;
            bool is_right;
            if (uxx.Variants.Count() > 2)
            {
                is_right = currWord.Answer == answer;
            }
            else
            {
                is_right = currWord.Answer == answer || (answer == 2 && currWord.Answer == 1);
            }
            currWord.Effect(is_right);
            sw.Stop();
            log(currWord,
                "next: " + update_move(),
                "right: " + sw.Elapsed.TotalMilliseconds);
        }

        private void log(params object[] row)
        {
            StringBuilder text = new StringBuilder();
            text.Append(DateTime.Now.ToString("mm:ss.ff"));
            text.Append("| ");

            text.Append(string.Join(", ", row));

            listBox1.Items.Insert(0, text.ToString());
        }
    }
}
