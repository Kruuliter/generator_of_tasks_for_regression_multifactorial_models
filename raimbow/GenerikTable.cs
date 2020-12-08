using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace raimbow
{
    public partial class GenerikTable : UserControl
    {
        private int number = 0;
        private int ygr = 0;
        private int ygr_summ = 0;
        private int resh = 0;
        private char[,] preobr = new char[0,0];
        private Dictionary<string, string[,]> dict;
        public GenerikTable()
        {
            InitializeComponent();
            
        }

        private string[,] oglav(int column, int row, int levels)
        {
            string[,] tableString = new string[column, row];
            int[,] rmatre = randomize_matric(one, row - 1);
            //int[,] rmatre = logical_matrix(one, row - 1, levels);

            number = 2 + one;
            ygr = number + one;
            ygr_summ = column - 2;
            resh = column - 1;

            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (j == 0)
                    {
                        if (i == 0)
                        {
                            tableString[i, j] = "U";
                            continue;
                        }
                        if (i == 1)
                        {
                            tableString[i, j] = "X0";
                            continue;
                        }
                        if (i >= 2 && i < number)
                        {
                            tableString[i, j] = "X" + (i - 1).ToString();
                            continue;
                        }
                        if (i >= number && i < ygr)
                        {
                            tableString[i, j] = (i + 1 - number).ToString();
                            continue;
                        }
                        if (i >= ygr && i < ygr_summ)
                        {
                            tableString[i, j] = "Y" + (i + 1 - ygr).ToString();
                            continue;
                        }
                        if (i == ygr_summ)
                        {
                            tableString[i, j] = "Ys";
                            continue;
                        }
                        if (i == resh)
                        {
                            tableString[i, j] = "S";
                            continue;
                        }
                    }

                    if (j > 0)
                    {
                        if (i == 0)
                        {
                            tableString[i, j] = j.ToString();
                            continue;
                        }
                        if (i == 1)
                        {
                            tableString[i, j] = "+";
                            continue;
                        }
                        if (i >= 2 && i < number)
                        {
                            tableString[i, j] = preobr[j - 1, i - 2].ToString();
                            continue;
                        }
                        if (i >= number && i < ygr)
                        {
                            tableString[i, j] = rmatre[i - number, j - 1].ToString();
                            continue;
                        }
                        if (i >= ygr && i < ygr_summ)
                        {
                            tableString[i, j] = "?";
                            continue;
                        }
                        if (i == ygr_summ)
                        {
                            tableString[i, j] = "?";
                            continue;
                        }
                        if (i == resh)
                        {
                            tableString[i, j] = "?";
                            continue;
                        }
                    }
                }
            }
            return tableString;
        }

        private int sochet(int n, int k)
        {
            if (k == 0)
            {
                return 1;
            }
            double a = 1;
            double b = n - k;
            double c = 1;
            double f = 1;
            for (int i = 1; i <=n; i++)
            {
                a = a * i; 
            }
            for (int i = 1; i <= b; i++)
            {
                c = c * i;
            }
            for (int i = 1; i <= k; i++)
            {
                f = f * i;
            }
            c = c * f;
            f = a / c;
            return (int)f;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string level_for_table_string = listBox1.SelectedItem.ToString();
            label1.Text = level_for_table_string;
            string[,] table_string = dict[level_for_table_string];
            create_table(table_string);
        }

        private void preobraz(int r, int g)
        {
            preobr = new char[r, g];
            int b = g;
            string dv = Convert.ToString(b, 2);
            while (true)
            {
                if (dv.Length == (g + 1))
                {
                    break;
                }
                b = b + 1;
                dv = Convert.ToString(b, 2);
            }
            int z = 0;
            for (int i = 0; i < r; i++)
            {
                z = b + i;
                dv = Convert.ToString(z, 2);
                dv = dv.Substring(1);
                dv = dv.Replace('1', '+');
                dv = dv.Replace('0', '-');
                for (int j = 0; j < g; j++)
                {
                    preobr[i, j] = dv[(g - 1) - j];
                }
            }
        }

        public int level { get; set; }
        public int one { get; set; }
        public double[] min_v { get; set; }
        public double[] max_v { get; set; }

        private void create_table(String[,] string_for_table)
        {
            Control.ControlCollection cont = Controls;
            int len = cont.Count - 1;
            for (int i = len; i >= 0; i--)
            {
                if (cont[i].Tag != null)
                {
                    int a = (int)cont[i].Tag;
                    if (a == 2)
                    {
                        this.Controls.Remove(cont[i]);
                    }
                }
            }

            len = cont.Count - 1;
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

            int column = one * 3 + 4;
            int row = 1;
            for (int i = 0; i <= one; i++)
            {
                row = row + sochet(one, i);
            }

            TableLayoutPanel table = new TableLayoutPanel();
            table.Name = "table_randomz";
            table.Tag = 1;
            table.Location = new Point(122, 30);

            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    Label lb = new Label();
                    lb.Name = "Oglav" + column.ToString() + row.ToString();
                    lb.Tag = 2;
                    lb.Width = 40;
                    lb.Text = string_for_table[i, j];
                    table.Controls.Add(lb, i, j);
                }
            }
            table.Width = column * 110;
            table.Height = row * 30;

            this.Controls.Add(table);
        }

        public void generik()
        {
            dict = null;
            listBox1.Items.Clear();

            int column = one * 3 + 4;
            int row = 1;
            for (int i = 0; i <= one; i++)
            {
                row = row + sochet(one, i);
            }

            preobraz(row, one);
            dict = new Dictionary<string, string[,]>(level);
            ListBox lb = listBox1;
            for (int i = 1; i <= level; i++)
            {
                string[,] create_table_string = oglav(column, row, i);
                string str = "Вариант " + i;
                dict.Add(str, create_table_string);
                lb.Items.Add(str);
            }
        }

        private int[,] logical_matrix(int a, int b, int lev)
        {
            int[,] matr = new int[a, b];
            int logic = lev;
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    if (logic > matr.Length)
                    {
                        logic = 1;
                    }
                    matr[i, j] = logic;
                    logic = logic + 1;
                }
            }
            return matr;
        }

        private int[,] randomize_matric(int a, int b)
        {
            Thread.Sleep(100);
            Random rnd = new Random();
            int[,] matr = new int[a, b];
            int[] rmatr = new int[matr.Length];
            for (int i = 0; i < rmatr.Length; i++)
            {
                int r = rnd.Next(1, rmatr.Length+1);
                if (!rmatr.Contains(r))
                {
                    rmatr[i] = r;
                }
                else
                {
                    i--;
                }
            }
            int len = 0;
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    matr[i, j] = rmatr[len];
                    len = len + 1;
                }
            }

            return matr;
        }
    }
}
