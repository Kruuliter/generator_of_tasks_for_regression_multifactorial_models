using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raimbow
{
    class Generate_Table
    {
        private int number = 0;
        private int ygr = 0;
        private int ygr_summ = 0;
        private int resh = 0;

        public int one { private get; set; }
        public int y_min { private get; set; }
        public int y_max { private get; set; }

        private int[,] randomize_matric(int row, int ones)
        {
            Random rnd = new Random();
            int[,] matr = new int[row, ones];
            int[] rmatr = new int[matr.Length];
            for (int i = 0; i < rmatr.Length; i++)
            {
                int r = rnd.Next(1, rmatr.Length + 1);
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
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < ones; j++)
                {
                    matr[i, j] = rmatr[len];
                    len = len + 1;
                }
            }
            rnd = null;
            rmatr = null;

            return matr;
        }

        private char[,] preobraz(int r, int g)
        {
            char[,] preobr = new char[r, g];
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
            return preobr;
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
            for (int i = 1; i <= n; i++)
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

        private int[,] randomize_Y(int column, int row)
        {
            int[,] r_y = new int[column, row];
            Random rnd = new Random();

            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    r_y[i, j] = rnd.Next(y_min, y_max);
                }
            }

            return r_y;
        }

        public string[,] oglav(int column, int row)
        {
            string[,] tableString = new string[column, row];
            char[,] preobr = preobraz(row, one);
            int[,] rmatre = randomize_matric(one, row - 1);
            int[,] rand_y = randomize_Y(one, row - 1);
            int[] summ_rand_y = new int[row - 1];

            for (int i = 0; i < one; i++)
            {
                for (int j = 0; j < row - 1; j++)
                {
                    summ_rand_y[i] = summ_rand_y[i] + rand_y[i, j];
                }
            }

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
                            tableString[i, j] = rand_y[i - ygr, j - 1].ToString();
                            continue;
                        }
                        if (i == ygr_summ)
                        {
                            tableString[i, j] = summ_rand_y[i].ToString();
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

            rmatre = new int[0,0];
            rand_y = new int[0, 0];
            summ_rand_y = new int[0];
            preobr = new char[0, 0];

            return tableString;
        }
    }
}
