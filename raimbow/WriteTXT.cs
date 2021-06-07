using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raimbow
{
    class WriteTXT
    {
        public string reading(string[] texter, Dictionary<int, Save_date> dictors)
        {
            try
            {
                int i = 0;
                while(dictors.Count > i)
                {
                    string link1 = texter[0] + "\\Вариант " + (i+1).ToString() + ".txt";
                    string link2 = texter[1] + "\\Вариант " + (i + 1).ToString() + ".txt";
                    if (File.Exists(link1) != true)
                    {
                        using (StreamWriter sw = new StreamWriter(new FileStream(link1, FileMode.Create, FileAccess.Write)))
                        {
                            string lines = "Условие проведения эксперимента";
                            for(int j = 0; j < dictors[i + 1].max_var.Length*2; j++)
                            {
                                lines += "\tX" + (j + 1);
                            }
                            sw.WriteLine(lines);
                            lines = "Основной уровень фактора";
                            for (int j = 0; j < dictors[i + 1].max_var.Length*2; j++)
                            {
                                double a;
                                if (j < dictors[i + 1].max_var.Length)
                                {
                                    a = dictors[i + 1].min_var[j] + (dictors[i + 1].max_var[j] - dictors[i + 1].min_var[j]) / 2;
                                }
                                else
                                {
                                    a = 0;
                                }
                                lines += "\t" + a.ToString();
                            }
                            sw.WriteLine(lines);
                            lines = "Интервал варьирования\t";
                            for (int j = 0; j < dictors[i + 1].max_var.Length * 2; j++)
                            {
                                double a;
                                if (j < dictors[i + 1].max_var.Length)
                                {
                                    a = (dictors[i + 1].max_var[j] - dictors[i + 1].min_var[j]) / 2;
                                }
                                else
                                {
                                    a = 1;
                                }
                                lines += "\t" + a.ToString();
                            }
                            sw.WriteLine(lines);
                            lines = "Верхний уровень фактора\t";
                            for (int j = 0; j < dictors[i + 1].max_var.Length * 2; j++)
                            {
                                double a;
                                if (j < dictors[i + 1].max_var.Length)
                                {
                                    a = dictors[i + 1].max_var[j];
                                }
                                else
                                {
                                    a = 1;
                                }
                                lines += "\t+" + a.ToString();
                            }
                            sw.WriteLine(lines);
                            lines = "Нижний уровень фактора\t";
                            for (int j = 0; j < dictors[i + 1].max_var.Length * 2; j++)
                            {
                                double a;
                                if (j < dictors[i + 1].max_var.Length)
                                {
                                    a = dictors[i + 1].min_var[j];
                                }
                                else
                                {
                                    a = 1;
                                }
                                lines += "\t-" + a.ToString();
                            }
                            sw.WriteLine(lines);
                            lines = "№\tX0\t";
                            for(int z = 0; z < dictors[i+1].col_factory; z++)
                            {
                                lines += "X" + (z + 1).ToString() + "\t";
                            }
                            for (int z = 0; z < dictors[i + 1].col_factory; z++)
                            {
                                lines += (z + 1).ToString() + "\t";
                            }
                            for (int z = 0; z < dictors[i + 1].col_factory; z++)
                            {
                                lines += "y" + (z + 1).ToString() + "\t";
                            }
                            lines += "Ys\tS^2{Y}";
                            sw.WriteLine(lines);
                            Generate_Table tables = new Generate_Table(dictors[i + 1].col_factory, dictors[i + 1].y_min, dictors[i + 1].y_max, dictors[i + 1].tT);
                            for(int z = 0; z < tables.Get_table_for_teacher.GetLength(0); z++)
                            {
                                lines = "";
                                for(int f = 0; f < tables.Get_table_for_teacher.GetLength(1); f++)
                                {
                                    lines += tables.Get_table_for_teacher[z, f] + "\t";
                                }
                                sw.WriteLine(lines);
                            }
                            sw.WriteLine();
                            sw.WriteLine("S^2{Y} = " + tables.ss[0].ToString());
                            sw.WriteLine("S^2{Ys} = " + tables.ss[1].ToString());
                            sw.WriteLine("S^2{bi} = " + tables.ss[2].ToString());
                            sw.WriteLine("S{bi} = " + tables.ss[3].ToString());
                            sw.WriteLine("tT = " + tables.tT.ToString());
                            sw.WriteLine();
                            for (int j = 0; j < tables.b_var.GetLength(1); j++)
                            {
                                sw.WriteLine(tables.b_var[0, j] + " = " + tables.b_var[1, j]);
                            }
                            for (int j = 0; j < tables.b_var.GetLength(1); j++)
                            {
                                sw.WriteLine("t{" + tables.b_var[0, j] + "} = " + tables.b_var[2, j]);
                            }
                            sw.WriteLine();
                            sw.WriteLine(tables.uravn);
                        }

                        using (StreamWriter sw = new StreamWriter(new FileStream(link2, FileMode.Create, FileAccess.Write)))
                        {
                            string lines = "Условие проведения эксперимента";
                            for (int j = 0; j < dictors[i + 1].max_var.Length * 2; j++)
                            {
                                lines += "\tX" + (j + 1);
                            }
                            sw.WriteLine(lines);
                            lines = "Основной уровень фактора";
                            for (int j = 0; j < dictors[i + 1].max_var.Length * 2; j++)
                            {
                                double a;
                                if (j < dictors[i + 1].max_var.Length)
                                {
                                    a = dictors[i + 1].min_var[j] + (dictors[i + 1].max_var[j] - dictors[i + 1].min_var[j]) / 2;
                                }
                                else
                                {
                                    a = 0;
                                }
                                lines += "\t" + a.ToString();
                            }
                            sw.WriteLine(lines);
                            lines = "Интервал варьирования\t";
                            for (int j = 0; j < dictors[i + 1].max_var.Length * 2; j++)
                            {
                                double a;
                                if (j < dictors[i + 1].max_var.Length)
                                {
                                    a = (dictors[i + 1].max_var[j] - dictors[i + 1].min_var[j]) / 2;
                                }
                                else
                                {
                                    a = 1;
                                }
                                lines += "\t" + a.ToString();
                            }
                            sw.WriteLine(lines);
                            lines = "Верхний уровень фактора\t";
                            for (int j = 0; j < dictors[i + 1].max_var.Length * 2; j++)
                            {
                                double a;
                                if (j < dictors[i + 1].max_var.Length)
                                {
                                    a = dictors[i + 1].max_var[j];
                                }
                                else
                                {
                                    a = 1;
                                }
                                lines += "\t+" + a.ToString();
                            }
                            sw.WriteLine(lines);
                            lines = "Нижний уровень фактора\t";
                            for (int j = 0; j < dictors[i + 1].max_var.Length * 2; j++)
                            {
                                double a;
                                if (j < dictors[i + 1].max_var.Length)
                                {
                                    a = dictors[i + 1].min_var[j];
                                }
                                else
                                {
                                    a = 1;
                                }
                                lines += "\t-" + a.ToString();
                            }
                            lines = "№\tX0\t";
                            for (int z = 0; z < dictors[i + 1].col_factory; z++)
                            {
                                lines += "X" + (z + 1).ToString() + "\t";
                            }
                            for (int z = 0; z < dictors[i + 1].col_factory; z++)
                            {
                                lines += (z + 1).ToString() + "\t";
                            }
                            for (int z = 0; z < dictors[i + 1].col_factory; z++)
                            {
                                lines += "y" + (z + 1).ToString() + "\t";
                            }
                            lines += "Ys\tS^2{Y}";
                            sw.WriteLine(lines);
                            Generate_Table tables = new Generate_Table(dictors[i + 1].col_factory, dictors[i + 1].y_min, dictors[i + 1].y_max, dictors[i + 1].tT);
                            for (int z = 0; z < tables.Get_table_for_student.GetLength(0); z++)
                            {
                                lines = "";
                                for (int f = 0; f < tables.Get_table_for_student.GetLength(1); f++)
                                {
                                    lines += tables.Get_table_for_student[z, f] + "\t";
                                }
                                sw.WriteLine(lines);
                            }
                            sw.WriteLine();
                            sw.WriteLine("S^2{Y} = ?");
                            sw.WriteLine("S^2{Ys} = ?");
                            sw.WriteLine("S^2{bi} = ?");
                            sw.WriteLine("S{bi} = ?");
                            sw.WriteLine("tT = ?");
                            sw.WriteLine();
                            for (int j = 0; j < tables.b_var.GetLength(1); j++)
                            {
                                sw.WriteLine(tables.b_var[0, j] + " = ?");
                            }
                            for (int j = 0; j < tables.b_var.GetLength(1); j++)
                            {
                                sw.WriteLine("t{" + tables.b_var[0, j] + "} = ?");
                            }
                            sw.WriteLine();
                            sw.WriteLine("Y = ?");
                        }
                    }
                    else
                    {

                    }
                    i++;
                }
            }
            catch
            {
                return "Произошла непредвиденная ошибка";
            }
            return "Файлы созданы";
        }
    }
}
