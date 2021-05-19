using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Physics
{
    public partial class AddResultForm : Form
    {
        int score { get; set; }
        bool isHard;
        private string FILE_PATH = "Leaders.txt";
        public AddResultForm(int score, bool isHard)
        {
            InitializeComponent();
            this.score = score;
            this.isHard = isHard;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;

            if(!isHard)
                File.AppendAllText(FILE_PATH, $"{Name}:{score}\n");
            else
                File.AppendAllText(FILE_PATH, $"{Name}:{score} HARD\n");
            MainForm mf = new MainForm();
            mf.Show();
            mf.Activate();
            this.Dispose();
            this.Close();
        }
    }
}
