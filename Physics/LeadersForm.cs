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
    public partial class LeadersForm : Form
    {
        private string FILE_PATH = "Leaders.txt";
        public LeadersForm()
        {
            InitializeComponent();
            ParseLeadres();
        }

        private void ParseLeadres()
        {
            string info = File.ReadAllText(FILE_PATH);
            string[] Leaders = info.Split('\n');

            foreach(var leader in Leaders)
            {
                textBox1.Text += leader + "\n";
            }
        }
    }
}
