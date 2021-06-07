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

namespace raimbow
{
    public partial class Form1 : Form
    {
        int col_variant = 0;
        private Dictionary<int, Save_date> dictors;
        Form2 form2;
        public Form1()
        {
            InitializeComponent();
            dictors = new Dictionary<int, Save_date>(1);
            col_variant = (int)numericUpDown2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dictors = null;
            dictors = new Dictionary<int, Save_date>();
            form2 = new Form2((int)numericUpDown2.Value, ref dictors);
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dictors = form2.dict;
            if(dictors.Count == 0)
            {
                MessageBox.Show("Настройки не созданы", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (dictors.Count > 0)
            {
                string subpath1 = @"ПФЭ для преподавателя";
                string subpath2 = @"ПФЭ для студентов";
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Вы не указали путь где сгенирируются файлы", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(textBox1.Text);
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }
                    dirInfo.CreateSubdirectory(subpath1);
                    dirInfo.CreateSubdirectory(subpath2);
                    string[] who_is_tables = new string[2] { textBox1.Text + "\\ПФЭ для преподавателя", textBox1.Text + "\\ПФЭ для студентов" };
                    string vid = FormatItem.Text;
                    MessageBox.Show("Подождите некоторое время,\nгенерация решений требует времени", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    switch (vid)
                    {
                        case "XLSX":
                            {
                                ReadExcelFormuls readExcelFormuls = new ReadExcelFormuls();
                                MessageBox.Show(readExcelFormuls.reading(who_is_tables, dictors), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "TXT":
                            {
                                WriteTXT writeTXT = new WriteTXT();
                                MessageBox.Show(writeTXT.reading(who_is_tables, dictors), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;

                    }
                }
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            col_variant = (int)numericUpDown2.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = false;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xLSXToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormatItem.Text = ((ToolStripMenuItem)sender).Text;
        }
    }
}
