using UxxLog;
using System.Diagnostics;

namespace OI
{
    public partial class Form1 : Form
    {
        DataProcess data;
        Word currWord = Word.Empty;

        public Form1()
        {
            InitializeComponent();
            data = new DataProcess("data");
            this.resize_buttons();
            update_move();
            foreach (var item in data)
            {
                checkedListBox1.Items.Add(item.Uxx, true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.move_maked(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.move_maked(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.move_maked(2);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.resize_buttons();
        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            checkedListBox1.SelectedIndex = -1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool[] mask = new bool[data.Uxxs.Count()];
            for (int i = 0; i < data.Uxxs.Count(); i++)
            {
                mask[i] = checkedListBox1.GetItemChecked(i);
            }
            if (mask.Contains(true))
            {
                Stopwatch sw = Stopwatch.StartNew();
                sw.Start();
                if (data.SetMask(mask))
                {
                    sw.Stop();
                    log(String.Join(", ", data), sw.Elapsed.TotalMilliseconds);
                    if (!data.Contains(currWord.Type))
                    {
                        update_move();
                    }
                }

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            data.Save("dataSave");
            stopwatch.Stop();
            log("save: " + stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}