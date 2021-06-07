using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace raimbow
{
    public partial class Form2 : Form
    {
        private int col_factor = 0;
        private int y_min = 0;
        private int y_max = 10;
        public Dictionary<int, Save_date> dict;
        private int list_table = 1;
        private int max_list_table = 1;
        private double tt = 2.12;

        private int Col_factor { 
            get
            {
                return col_factor;
            }
            set
            {
                col_factor = value;
                numericUpDown1.Value = col_factor;

                min_max_value_table.Controls.Clear();

                generate_table();
            }
        }

        private double TT
        {
            get
            {
                return tt;
            }
            set
            {
                tt = value;
                textBox1.Text = tt.ToString();
            }
        }

        private int Y_minimum
        {
            get
            {
                return y_min;
            }

            set
            {
                y_min = value;
                textBoxMinimum.Text = y_min.ToString();
            }
        }

        private int Y_maximum
        {
            get
            {
                return y_max;
            }

            set
            {
                y_max = value;
                textBoxMaximum.Text = y_max.ToString();
            }
        }

        private int col_variant { get; set; }
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(int col_var)
        {
            InitializeComponent();
            col_variant = col_var;
        }
        public Form2(int col_var, ref Dictionary<int, Save_date> dictors)
        {
            InitializeComponent();
            col_variant = col_var;
            this.Text = "Настройки " + col_var.ToString() + " вариантов";
            dict = dictors;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            col_factor = (int)numericUpDown1.Value;

            min_max_value_table.Controls.Clear();

            generate_table();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Visible = false;
            buttonLeft.Visible = false;
            if (col_variant == 1)
            {
                buttonRight.Visible = false;
                button1.Visible = true;
            }
            Y_maximum = Convert.ToInt32(textBoxMaximum.Text);
            Y_minimum = Convert.ToInt32(textBoxMinimum.Text);
            Col_factor = (int)numericUpDown1.Value;

            dict = new Dictionary<int, Save_date>();

            list_table = 1;
            max_list_table = 1;
            numberstr.Text = "Страница " + list_table;
        }

        private void generate_table()
        {
            TableLayoutPanel table = min_max_value_table;
            table.Location = new Point(90, 90);
            int row = col_factor + 1;
            min_max_value_table.RowCount = row;
            int column = 3;
            min_max_value_table.ColumnCount = column;
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    Label label = add_labels(i, j);
                    TextBox minText = min_text(i, j);
                    TextBox maxText = max_text(i, j);

                    if (label != null)
                    {
                        table.Controls.Add(label, i, j);
                    }
                    if (minText != null)
                    {
                        table.Controls.Add(minText, i, j);
                    }
                    if (maxText != null)
                    {
                        table.Controls.Add(maxText, i, j);
                    }
                }
            }
            table.Width = column * 110;
            table.Height = row * 30;
        }

        private TextBox min_text(int column, int row)
        {
            TextBox text = new TextBox();
            text.Name = "min_" + row.ToString();
            text.Tag = 3;
            if (column == 1 && row >= 1)
            {
                text.Text = "1";
                return text;
            }
            return null;
        }
        private int[] min_text_read()
        {
            int[] min_massive = new int[col_factor];
            TableLayoutPanel table = min_max_value_table;

            for (int i = 1; i < table.RowCount; i++)
            {
                TextBox btn = (TextBox)table.GetControlFromPosition(1, i);
                min_massive[i - 1] = Convert.ToInt32(btn.Text);
            }
            return min_massive;
        }

        private void min_text_write(int[] min_massive)
        {
            TableLayoutPanel table = min_max_value_table;

            for (int i = 1; i < table.RowCount; i++)
            {
                TextBox btn = (TextBox)table.GetControlFromPosition(1, i);
                btn.Text = min_massive[i - 1].ToString();
            }
        }

        private TextBox max_text(int column, int row)
        {
            TextBox text = new TextBox();
            text.Name = "max_" + row.ToString();
            text.Tag = 4;
            if (column == 2 && row >= 1)
            {
                text.Text = "10";
                return text;
            }
            return null;
        }

        private int[] max_text_read()
        {
            int[] max_massive = new int[col_factor];
            TableLayoutPanel table = min_max_value_table;

            for (int i = 1; i < table.RowCount; i++)
            {
                TextBox btn = (TextBox)table.GetControlFromPosition(2, i);
                max_massive[i - 1] = Convert.ToInt32(btn.Text);
            }
            return max_massive;
        }

        private void max_text_write(int[] max_massive)
        {
            TableLayoutPanel table = min_max_value_table;

            for (int i = 1; i < table.RowCount; i++)
            {
                TextBox btn = (TextBox)table.GetControlFromPosition(2, i);
                btn.Text = max_massive[i - 1].ToString();
            }
        }

        private Label add_labels(int column, int row)
        {
            Label l = new Label();
            l.Name = "level_" + row.ToString();

            if (row == 0 && column == 2)
            {
                l.Text = "Верxний уровень";
                return l;
            }

            if (row == 0 && column == 1)
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

        private void textBoxMinimum_Enter(object sender, EventArgs e)
        {
            y_min = Convert.ToInt32(textBoxMinimum.Text);
        }

        private void textBoxMaximum_Enter(object sender, EventArgs e)
        {
            y_max = Convert.ToInt32(textBoxMinimum.Text);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {

            y_min = Convert.ToInt32(textBoxMinimum.Text);
            y_max = Convert.ToInt32(textBoxMaximum.Text);
            tt = Convert.ToDouble(textBox1.Text.Replace(".", ","));

            col_factor = (int)numericUpDown1.Value;
            int[] mass_max = max_text_read();
            int[] mass_min = min_text_read();

            if (list_table < max_list_table)
            {
                dict[list_table].col_factory = col_factor;
                dict[list_table].y_max = y_max;
                dict[list_table].y_min = y_min;
                dict[list_table].max_var = mass_max;
                dict[list_table].min_var = mass_min;
                dict[list_table].tT = tt;

                list_table = list_table + 1;

                Col_factor = dict[list_table].col_factory;
                Y_minimum = dict[list_table].y_min;
                Y_maximum = dict[list_table].y_max;
                TT = dict[list_table].tT;
                max_text_write(dict[list_table].max_var);
                min_text_write(dict[list_table].min_var);
            }
            else
            {
                Save_date save = new Save_date();
                save.col_factory = col_factor;
                save.y_max = y_max;
                save.y_min = y_min;
                save.max_var = mass_max;
                save.min_var = mass_min;
                save.tT = tt;

                dict.Add(list_table, save);

                save = null;
                Col_factor = dict[list_table].col_factory;
                Y_minimum = dict[list_table].y_min;
                Y_maximum = dict[list_table].y_max;
                TT = dict[list_table].tT;
                max_text_write(dict[list_table].max_var);
                min_text_write(dict[list_table].min_var);

                list_table = list_table + 1;
            }

            max_list_table = max_list_table + 1;

            numberstr.Text = "Страница " + list_table;

            if (list_table > 1)
            {
                buttonLeft.Visible = true;
            }
            else
            {
                buttonLeft.Visible = false;
            }

            if (list_table < col_variant)
            {
                buttonRight.Visible = true;
            }
            else
            {
                buttonRight.Visible = false;
                button1.Visible = true;
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            y_min = Convert.ToInt32(textBoxMinimum.Text);
            y_max = Convert.ToInt32(textBoxMaximum.Text);
            tt = Convert.ToDouble(textBox1.Text.Replace(".", ","));
            col_factor = (int)numericUpDown1.Value;
            int[] mass_max = max_text_read();
            int[] mass_min = min_text_read();

            if (list_table == max_list_table)
            {
                Save_date save = new Save_date();
                save.col_factory = col_factor;
                save.y_max = y_max;
                save.y_min = y_min;
                save.max_var = mass_max;
                save.min_var = mass_min;
                save.tT = tt;

                dict.Add(list_table, save);
            }
            else
            {
                dict[list_table].col_factory = col_factor;
                dict[list_table].y_max = y_max;
                dict[list_table].y_min = y_min;
                dict[list_table].max_var = mass_max;
                dict[list_table].min_var = mass_min;
                dict[list_table].tT = tt;
            }

            list_table = list_table - 1;

            Col_factor = dict[list_table].col_factory;
            Y_minimum = dict[list_table].y_min;
            Y_maximum = dict[list_table].y_max;
            TT = dict[list_table].tT;
            max_text_write(dict[list_table].max_var);
            min_text_write(dict[list_table].min_var);

            numberstr.Text = "Страница " + list_table;


            if (list_table > 1)
            {
                buttonLeft.Visible = true;
            }
            else
            {
                buttonLeft.Visible = false;
            }

            if (list_table < col_variant)
            {
                buttonRight.Visible = true;
            }
            else
            {
                buttonRight.Visible = false;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Подождите, идет сохранение настроек", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (col_variant == 1)
                {
                    y_min = Convert.ToInt32(textBoxMinimum.Text);
                    y_max = Convert.ToInt32(textBoxMaximum.Text);
                    tt = Convert.ToDouble(textBox1.Text.Replace(".", ","));
                    col_factor = (int)numericUpDown1.Value;
                    int[] mass_max = max_text_read();
                    int[] mass_min = min_text_read();
                    Save_date save = new Save_date();
                    save.col_factory = col_factor;
                    save.y_max = y_max;
                    save.y_min = y_min;
                    save.max_var = mass_max;
                    save.min_var = mass_min;
                    save.tT = tt;
                    dict.Add(1, save);
                }
                else
                {
                    if (col_variant != dict.Count())
                    {
                        y_min = Convert.ToInt32(textBoxMinimum.Text);
                        y_max = Convert.ToInt32(textBoxMaximum.Text);
                        tt = Convert.ToDouble(textBox1.Text.Replace(".", ","));
                        col_factor = (int)numericUpDown1.Value;
                        int[] mass_max = max_text_read();
                        int[] mass_min = min_text_read();
                        Save_date save = new Save_date();
                        save.col_factory = col_factor;
                        save.y_max = y_max;
                        save.y_min = y_min;
                        save.max_var = mass_max;
                        save.min_var = mass_min;
                        save.tT = tt;
                        dict.Add(col_variant, save);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла непредвиденная ошибка", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
            }
        }
    }
}