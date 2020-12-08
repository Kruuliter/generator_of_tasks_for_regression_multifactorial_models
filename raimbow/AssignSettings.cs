using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace raimbow
{
    public partial class AssignSettings : UserControl
    {
        int one = 0;
        int col_variant = 0;
        public AssignSettings()
        {
            InitializeComponent();
        }

        private void AssignSettings_Load(object sender, EventArgs e)
        {
            one = (int)numericUpDown1.Value;
            col_variant = (int)numericUpDown2.Value;
            generate_table();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            one = (int)numericUpDown1.Value;
            Control.ControlCollection cont = Controls;
            int len = cont.Count - 1;
            for (int i = len; i >= 0; i--)
            {
                if (cont[i].Tag != null)
                {
                    int a = (int)cont[i].Tag;
                    if (a == 1)
                    {
                        this.Controls.Remove(cont[i]);
                    }
                }
            }

            generate_table();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            col_variant = (int)numericUpDown2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (linker != null)
            {
                linker.level = col_variant;
                linker.one = one;
                linker.max_v = max_var();
                linker.min_v = min_var();
                linker.generik();
            }
        }

        private double[] max_var()
        {
            double[] mv = new double[one];
            int j = 0;
            Control.ControlCollection cont = Controls;

            int len = cont.Count - 1;
            for (int i = len; i >= 0; i--)
            {
                if (cont[i].Tag != null)
                {
                    int a = (int)cont[i].Tag;
                    if (a == 4)
                    {
                        mv[j] = Convert.ToDouble(cont[i].Text);
                    }
                }
            }
            return mv;
        }

        private double[] min_var()
        {
            double[] mv = new double[one];
            int j = 0;
            Control.ControlCollection cont = Controls;

            int len = cont.Count - 1;
            for (int i = len; i >= 0; i--)
            {
                if (cont[i].Tag != null)
                {
                    int a = (int)cont[i].Tag;
                    if (a == 3)
                    {
                        mv[j] = Convert.ToDouble(cont[i].Text);
                    }
                }
            }
            return mv;
        }

        private void generate_table()
        {
            TableLayoutPanel table = new TableLayoutPanel();
            table.Name = "min_max_value_table";
            table.Tag = 1;
            table.Location = new Point(12, 120);
            int row = one + 1;
            int column = 3;
            for (int i = 0; i <column; i++)
            {
                for (int j = 0; j<row; j++)
                {
                    Label label = add_labels(i, j);
                    TextBox minText = min_text(i, j);
                    TextBox maxText = max_text(i, j);

                    if (label != null)
                    {
                        table.Controls.Add(label, i, j);
                    }
                    if(minText != null)
                    {
                        table.Controls.Add(minText, i, j);
                    }
                    if(maxText != null)
                    {
                        table.Controls.Add(maxText, i, j);
                    }
                }
            }
            table.Width = column * 110;
            table.Height = row * 30;
            this.Controls.Add(table);
        }

        private TextBox min_text(int column, int row)
        {
            TextBox text = new TextBox();
            text.Name = "min_" + row.ToString();
            text.Tag = 3;
            if(column == 2 && row >= 1)
            {
                text.Text = "0";
                return text;
            }
            return null;
        }

        private TextBox max_text(int column, int row)
        {
            TextBox text = new TextBox();
            text.Name = "max_" + row.ToString();
            text.Tag = 4;
            if (column == 1 && row >= 1)
            {
                text.Text = "0";
                return text;
            }
            return null;
        }

        private Label add_labels(int column, int row)
        {
            Label l = new Label();
            l.Name = "level_" + row.ToString();

            if (row == 0 && column == 1)
            {
                l.Text = "Верxний уровень";
                return l;
            }

            if (row == 0 && column == 2)
            {
                l.Text = "Нижний уровень";
                return l;
            }

            if (row != 0 && column == 0)
            {
                l.Text = "X" + row.ToString();
                return l;
            }
            return null;
        }

        public GenerikTable linker { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = false;
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
