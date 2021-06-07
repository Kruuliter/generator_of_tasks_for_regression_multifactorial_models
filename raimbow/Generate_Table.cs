using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace raimbow
{
    class Generate_Table
    {
        private int number = 0;
        private int ygr = 0;
        private int ygr_summ = 0;
        private int resh = 0; 
        private int colich_variant = 0;

        public int Colich_variant {
            get {
                return colich_variant;
            }

            set
            {
                colich_variant = value;
                Row = (int)Math.Pow(2, colich_variant);
                Column = colich_variant * 3 + 4;
            }
        }
        public int Column { get; private set; }
        public int Row { get; private set; }
        public int y_min { get; private set; }
        public int y_max { get; private set; }
        public double tT { get; private set; }
        public string[,] Get_table_for_teacher { get; private set; }
        public string[,] Get_table_for_student { get; private set; }
        public string uravn { get; private set; }
        public double [] ss { get; private set; }
        public string[,] b_var { get; private set; }

        private int[,] randomize_matric(int column, int row)
        {
            Random rnd = new Random();
            int[,] matr = new int[row, column];
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
                for (int j = 0; j < column; j++)
                {
                    matr[i, j] = rmatr[len];
                    len = len + 1;
                }
            }
            rnd = null;
            rmatr = null;

            return matr;
        }

        private char[,] preobraz(int column, int row)
        {
            char[,] preobr = new char[row, column];
            int b = column;
            string dv = Convert.ToString(b, 2);
            while (true)
            {
                if (dv.Length == (column + 1))
                {
                    break;
                }
                b = b + 1;
                dv = Convert.ToString(b, 2);
            }
            int z = 0;
            for (int i = 0; i < row; i++)
            {
                z = b + i;
                dv = Convert.ToString(z, 2);
                dv = dv.Substring(1);
                dv = dv.Replace('1', '+');
                dv = dv.Replace('0', '-');
                for (int j = 0; j < column; j++)
                {
                    preobr[i, j] = dv[(column - 1) - j];
                }
            }
            return preobr;
        }

        private int[,] randomize_Y(int column, int row)
        {
            int[,] r_y = new int[row, column];
            Random rnd = new Random();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    r_y[i, j] = rnd.Next(y_min, y_max);
                }
            }

            return r_y;
        }

        private double Sunifarmular(int [] simular, double ys)
        {
            double arbers = 0;
            foreach (int sss in simular)
            {
                double arb = (ys - sss) * (ys - sss);
                arbers += arb;
                arb = 0;
            }
            return Math.Round(arbers/(Colich_variant-1), 2);
        }

        private string generat_formula(double tt, string[,] mass_str)
        {
            string uravnenye = "Y = ";
            for(int i = 0; i < mass_str.GetLength(1); i++)
            {
                double a = Convert.ToDouble(mass_str[2, i]);
                if(a > tt)
                {
                    if(mass_str[0, i] != "b0")
                    {
                        if (Convert.ToDouble(mass_str[1, i]) > 0)
                        {
                            uravnenye += " +" + mass_str[1, i] + "*" + mass_str[0, i].Replace("b", "x");
                        }
                        else
                        {
                            uravnenye += mass_str[1, i] + "*" + mass_str[0, i].Replace("b", "x");
                        }
                    }
                    else
                    {
                        uravnenye += mass_str[1, i];
                    }
                }
            }
            return uravnenye;
        }

        private string[,] for_student(int column, int row, string[,] table_teacher, int percents)
        {
            string[,] tableString = new string[row, column];
            Random rand = new Random();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    int percent1 = rand.Next(0, 100);
                    if (percent1 <= percents && number <= j && j < ygr)
                    {
                        tableString[i, j] = "?";
                    }
                    else
                    {
                        tableString[i, j] = table_teacher[i, j];
                    }
                }
            }
            return tableString;
        }

        private string[,] for_teacher(int column, int row, char[,] pr, int[,] rmatr, int[,] rand_y)
        {
            string[,] tableString = new string[row, column];
            double[] summ_rand_y = new double[row];
            double[] sumrack = new double[row];
            int row_y = 0;
            int column_y = 0;
            double summers = 0;
            int[] mass_y = new int[Colich_variant];
            foreach(int ys in rand_y)
            {
                if (row_y == rand_y.GetLength(1))
                {
                    summ_rand_y[column_y] = Math.Round(summers / Colich_variant, 2);
                    sumrack[column_y] = Sunifarmular(mass_y, summ_rand_y[column_y]);
                    mass_y = new int[Colich_variant];
                    row_y = 0;
                    summers = 0;
                    column_y += 1;
                }

                if (row_y < rand_y.GetLength(1))
                {
                    summers += ys;
                    mass_y[row_y] = ys;
                }
                row_y += 1;
            }
            if (row_y == rand_y.GetLength(1))
            {
                summ_rand_y[column_y] = Math.Round(summers / Colich_variant, 2);
                sumrack[column_y] = Sunifarmular(mass_y, summ_rand_y[column_y]);
            }

            ss[0] = Math.Round(sumrack.Sum()/row, 2);
            ss[1] = Math.Round(ss[0] / colich_variant, 2);
            ss[2] = Math.Round(ss[1] / row, 2);
            ss[3] = Math.Round(Math.Pow(ss[2], 0.5), 2);

            b_var = injectors(pr, summ_rand_y);
            for(int i = 0; i < b_var.GetLength(1); i++)
            {
                b_var[2, i] = Math.Round(Convert.ToDouble(b_var[1, i]) / ss[3], 2).ToString();
                b_var[0, i] = "b" + b_var[0, i];
            }

            number = 2 + colich_variant;
            ygr = number + colich_variant;
            ygr_summ = column - 2;
            resh = column - 1;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (j == 0)
                    {
                        tableString[i, j] = (i + 1).ToString();
                        continue;
                    }
                    if (j == 1)
                    {
                        tableString[i, j] = "+";
                        continue;
                    }
                    if (j >= 2 && j < number)
                    {
                        tableString[i, j] = pr[i, j - 2].ToString();
                        continue;
                    }
                    if (j >= number && j < ygr)
                    {
                        tableString[i, j] = rmatr[i, j - number].ToString();
                        continue;
                    }
                    if (j >= ygr && j < ygr_summ)
                    {
                        tableString[i, j] = rand_y[i, j - ygr].ToString();
                        continue;
                    }
                    if (j == ygr_summ)
                    {
                        tableString[i, j] = summ_rand_y[i].ToString();
                        continue;
                    }
                    if (j == resh)
                    {
                        tableString[i, j] = sumrack[i].ToString();
                        continue;
                    }
                }
            }

            return tableString;
        }
        private string[,] copys(string[,] mass)
        {
            string[,] mass2 = new string[mass.GetLength(0), mass.GetLength(1)];
            for (int row = 0; row < mass.GetLength(0); row++)
            {
                for (int column = 0; column < mass.GetLength(1); column++)
                {
                    mass2[row, column] = mass[row, column];
                }
            }
            return mass2;
        }

        private int factorial(int f)
        {
            int summ = 1;
            for (int i = 1; i <= f; i++)
            {
                summ *= i;
            }
            return summ;
        }

        private int sochet(int n, int k)
        {
            return factorial(n) / (factorial(k)*factorial(n - k));
        }

        private string[] alph_method()
        {
            string[] alph_mass = new string[Colich_variant];
            for(int i = 1; i <= alph_mass.Length; i++)
            {
                alph_mass[i - 1] = i.ToString();
            }
            return alph_mass;
        }

        private string[] sochet_variants(int a)
        {
            string[] mart = new string[sochet(Colich_variant, a)];
            string[] alph = alph_method();
            switch (a){
                case 1:
                    {
                        mart = new string[sochet(Colich_variant+1, a)];
                        for (int i = 0; i <mart.Length; i++)
                        {
                            mart[i] = i.ToString();
                        }
                    }
                    break;
                case 2:
                    {
                        int s = 0;
                        for(int i = 0; i < alph.Length; i++)
                        {
                            for (int j = i+1; j < alph.Length; j++)
                            {
                                mart[s] = alph[i] + alph[j];
                                s++;
                            }
                        }
                    }break;
                case 3:
                    {
                        int s = 0;
                        for (int i = 0; i < alph.Length; i++)
                        {
                            for (int j = i + 1; j < alph.Length; j++)
                            {
                                for(int z = j + 1; z < alph.Length; z++)
                                {
                                    mart[s] = alph[i] + alph[j] + alph[z];
                                    s++;
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        int s = 0;
                        for (int i = 0; i < alph.Length; i++)
                        {
                            for (int j = i + 1; j < alph.Length; j++)
                            {
                                for (int z = j + 1; z < alph.Length; z++)
                                {
                                    for(int d = z + 1; d < alph.Length; d++)
                                    {
                                        mart[s] = alph[i] + alph[j] + alph[z] + alph[d];
                                        s++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 5:
                    {
                        int s = 0;
                        for (int i = 0; i < alph.Length; i++)
                        {
                            for (int j = i + 1; j < alph.Length; j++)
                            {
                                for (int z = j + 1; z < alph.Length; z++)
                                {
                                    for (int d = z + 1; d < alph.Length; d++)
                                    {
                                        for(int g = d + 1; g < alph.Length; g++)
                                        {
                                            mart[s] = alph[i] + alph[j] + alph[z] + alph[d] + alph[g];
                                            s++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            return mart;
        }

        private double verofly(string str, string[,] pm, double[] summy)
        {
            double a = 0;
            if (str != "0")
            {
                char[] charls = str.ToCharArray();
                for (int i = 0; i < pm.GetLength(1); i++)
                {
                    double summ2 = 1;
                    for(int j = 0; j < charls.Length; j++)
                    {
                        int z = Int32.Parse(charls[j].ToString()) - 1;
                        summ2 *= Int32.Parse(pm[z, i]);
                    }
                    a += summ2 * summy[i];
                }
            }
            else
            {
                a = summy.Sum();
            }
            return a;
        }

        private string [,] injectors(char[,] preob, double[] summ_y)
        {
            string[,] p_or_m = new string[preob.GetLength(1), preob.GetLength(0)];
            for(int i = 0; i < preob.GetLength(1); i++)
            {
                for(int j = 0; j < preob.GetLength(0); j++)
                {
                    p_or_m[i,j] = preob[j, i].ToString() + "1";
                }
            }

            int summ = 0;
            for(int i = 1; i <= Colich_variant; i++)
            {
                if(i == 1)
                {
                    summ += sochet(Colich_variant+1, i);
                }
                else
                {
                    summ += sochet(Colich_variant, i);
                }
            }
            string[,] problems = new string[3, summ];
            int asp = 0;
            for(int i = 1; i <= Colich_variant; i++)
            {
                string[] mrt = sochet_variants(i);
                for(int j = 0; j < mrt.Length; j++)
                {
                    problems[0, asp] = mrt[j];
                    asp++;
                }
            }
            for(int i = 0; i < problems.GetLength(1); i++)
            {
                problems[1, i] = verofly(problems[0, i], p_or_m, summ_y).ToString();
            }

            return problems;
        }

        private void start_worked()
        {
            char[,] preobr = preobraz(Colich_variant, Row);
            int[,] rmatre = randomize_matric(Colich_variant, Row);
            int[,] rand_y = randomize_Y(Colich_variant, Row);

            string[,] tableStringTeacher = for_teacher(Column, Row, preobr, rmatre, rand_y);
            string[,] tableStringStudent = for_student(Column, Row, tableStringTeacher, 40);

            Get_table_for_teacher = copys(tableStringTeacher);
            Get_table_for_student = copys(tableStringStudent);

            uravn = generat_formula(tT, b_var);

            rmatre = new int[0,0];
            rand_y = new int[0, 0];
            preobr = new char[0, 0];
            tableStringTeacher = new string[0, 0];
            tableStringStudent = new string[0, 0];
        }

        public Generate_Table(int col_factor, int y_mins, int y_maxs, double t)
        {
            ss = new double[4];
            Colich_variant = col_factor;
            y_min = y_mins;
            y_max = y_maxs;
            tT = t;
            start_worked();
        }
    }
}
