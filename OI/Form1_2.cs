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

        private void update_move()
        {
            Stopwatch sw = Stopwatch.StartNew();
            currWord = data.Next();
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
        }

        private void move_maked(int answer = -1)
        {
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
            log(currWord);
        }

        private void log(object row)
        {
            listBox1.Items.Insert(0, DateTime.Now.ToString() + "| " +
                            row.ToString());
        }
    }
}
