using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = textBox1.Text;
            string groupBy = textBox2.Text;

            List<string> data1 = File.ReadAllLines(fileName)
                .Select(line => line.Split(','))
                .Select(parts => parts[0] + " - " + parts[1])
                .ToList();

            List<string> data2 = File.ReadAllLines(fileName)
                .Select(line => line.Split(','))
                .Select(parts => parts[2] + " - " + parts[3] + " - " + parts[4] + " - " + parts[5])
                .ToList();

            var query = data1.Join(data2,
                                   d1 => d1.Split('-')[0].Trim(),
                                   d2 => d2.Split('-')[1].Trim(),
                                   (d1, d2) => d1 + " # " + d2)
                             .GroupBy(item => item.Split('#')[1]);

            listBox1.Items.Clear();
            foreach (var group in query)
            {
                listBox1.Items.Add("Группа: " + group.Key);
                foreach (var item in group)
                {
                    listBox1.Items.Add(item);
                }
            }
        }
    }
}