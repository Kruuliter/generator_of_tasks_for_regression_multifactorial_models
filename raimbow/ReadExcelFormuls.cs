using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raimbow
{
    class ReadExcelFormuls
    {
        public string reading(string [] texter, Dictionary<int, Save_date> dictors)
        {
            try
            {
                Write_Excel reads = new Write_Excel(texter[0]);
                int i = 0;
                int l = 0;
                while (dictors.Count > i)
                {
                    Write_Excel reads_student = new Write_Excel(texter[1]);
                    if (i % 10 == 0 && i != 0)
                    {
                        reads.save_excel("Варианты " + (l + 1).ToString() + "-" + (i + 1).ToString());
                        l = i;
                        reads = new Write_Excel(texter[0]);
                    }
                    reads.create_sheet("Вариант " + (i + 1).ToString());
                    reads_student.create_sheet("Вариант " + (i + 1).ToString());
                    Generate_Table tables = new Generate_Table(dictors[i + 1].col_factory, dictors[i + 1].y_min, dictors[i + 1].y_max, dictors[i + 1].tT);

                    reads.sheet_merge(1, 1, 2, 1, "Условие\n\r проведе\n\rния экспери\n\rмента");
                    reads_student.sheet_merge(1, 1, 2, 1, "Условие\n\r проведе\n\rния экспери\n\rмента");
                    reads.sheet_merge(1, 2, 1, 2 + dictors[i + 1].max_var.Length - 1, "Натуральные значения \n\ri-го фактора");
                    reads_student.sheet_merge(1, 2, 1, 2 + dictors[i + 1].max_var.Length - 1, "Натуральные значения \n\ri-го фактора");
                    int ins = 2 + dictors[i + 1].max_var.Length;
                    reads.sheet_merge(1, ins, 1, ins + dictors[i + 1].max_var.Length - 1, "Натуральные значения \n\ri-го фактора");
                    reads_student.sheet_merge(1, ins, 1, ins + dictors[i + 1].max_var.Length - 1, "Натуральные значения \n\ri-го фактора");
                    for (int j = 0; j < dictors[i + 1].max_var.Length; j++)
                    {
                        reads.write_excel(2, 2 + j, "X" + (j + 1).ToString());
                        reads.write_excel(2, ins + j, "X" + (j + 1).ToString());

                        reads_student.write_excel(2, 2 + j, "X" + (j + 1).ToString());
                        reads_student.write_excel(2, ins + j, "X" + (j + 1).ToString());
                    }
                    reads.write_excel(6, 1, "Нижний\n\rуровень\n\rфактора");
                    reads.write_excel(5, 1, "Верхний\n\rуровень\n\rфактора");
                    reads.write_excel(4, 1, "Интерва\n\rл варьиро\n\rвания \n\rфактора");
                    reads.write_excel(3, 1, "Основно\n\rй \n\rуровень\n\rфактора");

                    reads_student.write_excel(6, 1, "Нижний\n\rуровень\n\rфактора");
                    reads_student.write_excel(5, 1, "Верхний\n\rуровень\n\rфактора");
                    reads_student.write_excel(4, 1, "Интерва\n\rл варьиро\n\rвания \n\rфактора");
                    reads_student.write_excel(3, 1, "Основно\n\rй \n\rуровень\n\rфактора");
                    for (int j = 0; j < dictors[i + 1].max_var.Length; j++)
                    {
                        reads.write_excel(6, 2 + j, dictors[i + 1].min_var[j].ToString());
                        reads.write_excel(6, ins + j, "-1");
                        reads.write_excel(5, 2 + j, dictors[i + 1].max_var[j].ToString());
                        reads.write_excel(5, ins + j, "+1");

                        reads_student.write_excel(6, 2 + j, dictors[i + 1].min_var[j].ToString());
                        reads_student.write_excel(6, ins + j, "-1");
                        reads_student.write_excel(5, 2 + j, dictors[i + 1].max_var[j].ToString());
                        reads_student.write_excel(5, ins + j, "+1");

                        double sens = (dictors[i + 1].max_var[j] - dictors[i + 1].min_var[j])/2;

                        reads.write_excel(4, 2 + j, sens.ToString());
                        reads.write_excel(4, ins + j, "1");
                        reads.write_excel(3, 2 + j, (dictors[i + 1].min_var[j] + sens).ToString());
                        reads.write_excel(3, ins + j, "0");

                        reads_student.write_excel(4, 2 + j, sens.ToString());
                        reads_student.write_excel(4, ins + j, "1");
                        reads_student.write_excel(3, 2 + j, (dictors[i + 1].min_var[j] + sens).ToString());
                        reads_student.write_excel(3, ins + j, "0");
                    }
                    int start_write_column = 0;
                    int start_write_row = 8;
                    reads.sheet_merge(start_write_row, 1, (start_write_row+1), 1, "№");
                    reads_student.sheet_merge(start_write_row, 1, (start_write_row+1), 1, "№");
                    reads.write_excel((start_write_row+1), 2, "X0");
                    reads_student.write_excel((start_write_row+1), 2, "X0");
                    for (int z = 0; z < dictors[i + 1].col_factory; z++)
                    {
                        start_write_column = 3 + z;
                        reads.write_excel((start_write_row+1), start_write_column, "X" + (z + 1).ToString());
                        reads_student.write_excel((start_write_row+1), start_write_column, "X" + (z + 1).ToString());
                    }
                    reads.sheet_merge(start_write_row, 2, start_write_row, start_write_column, "Факторы");
                    reads_student.sheet_merge(start_write_row, 2, start_write_row, start_write_column, "Факторы");
                    reads.sheet_merge(start_write_row, (1 + start_write_column), start_write_row, (start_write_column + dictors[i + 1].col_factory), "Номер серии");
                    reads_student.sheet_merge(start_write_row, (1 + start_write_column), start_write_row, (start_write_column + dictors[i + 1].col_factory), "Номер серии");
                    for (int z = 1; z <= dictors[i + 1].col_factory; z++)
                    {
                        start_write_column = start_write_column + 1;
                        reads.write_excel((start_write_row+1), start_write_column,  z.ToString());
                        reads_student.write_excel((start_write_row+1), start_write_column, z.ToString());
                    }
                    reads.sheet_merge(start_write_row, (1 + start_write_column), start_write_row, (start_write_column + dictors[i + 1].col_factory), "y");
                    reads_student.sheet_merge(start_write_row, (1 + start_write_column), start_write_row, (start_write_column + dictors[i + 1].col_factory), "y");
                    for (int z = 1; z <= dictors[i + 1].col_factory; z++)
                    {
                        start_write_column = start_write_column + 1;
                        reads.write_excel((start_write_row+1), start_write_column, "y" + z.ToString());
                        reads_student.write_excel((start_write_row+1), start_write_column, "y" + z.ToString());
                    }
                    start_write_column = start_write_column + 1;
                    reads.sheet_merge(start_write_row, start_write_column, (start_write_row+1), start_write_column, "Ys");
                    reads_student.sheet_merge(start_write_row, start_write_column, (start_write_row+1), start_write_column, "Ys");
                    start_write_column = start_write_column + 1;
                    reads.sheet_merge(start_write_row, start_write_column, (start_write_row+1), start_write_column, "S(o)");
                    reads_student.sheet_merge(start_write_row, start_write_column, (start_write_row+1), start_write_column, "S(o)");
                    reads.write_excel((start_write_row + 2), 1, tables.Get_table_for_teacher);
                    reads_student.write_excel((start_write_row + 2), 1, tables.Get_table_for_student);
                    start_write_row = start_write_row + tables.Row + 4;
                    reads.write_excel(start_write_row, 1, "S^2{Y} = ");
                    reads.write_excel(start_write_row + 1, 1, "S^2{Ys} = ");
                    reads.write_excel(start_write_row + 2, 1, "S^2{bi} = ");
                    reads.write_excel(start_write_row + 3, 1, "S{bi} = ");
                    reads.write_excel(start_write_row + 4, 1, "tT = ");
                    reads.write_excel(start_write_row + 4, 2, tables.tT);

                    reads_student.write_excel(start_write_row, 1, "S^2{Y} = ");
                    reads_student.write_excel(start_write_row + 1, 1, "S^2{Ys} = ");
                    reads_student.write_excel(start_write_row + 2, 1, "S^2{bi} = ");
                    reads_student.write_excel(start_write_row + 3, 1, "S{bi} = ");
                    reads_student.write_excel(start_write_row + 4, 1, "tT = ");
                    reads_student.write_excel(start_write_row + 4, 2, tables.tT);

                    for (int j = 0; j < tables.ss.Length; j++)
                    {
                        reads.write_excel(start_write_row, 2, tables.ss[j]);
                        reads_student.write_excel(start_write_row, 2, "?");
                        start_write_row++;
                    }
                    start_write_row += 2;

                    for(int j = 0; j < tables.b_var.GetLength(1); j++)
                    {
                        reads.write_excel(start_write_row, 1, (tables.b_var[0, j] + " ="));
                        reads.write_excel(start_write_row, 2, tables.b_var[1, j]);
                        reads.write_excel(start_write_row, 4, ("t{" + tables.b_var[0, j] + "} ="));
                        reads.write_excel(start_write_row, 5, tables.b_var[2, j]);

                        reads_student.write_excel(start_write_row, 1, (tables.b_var[0, j] + " ="));
                        reads_student.write_excel(start_write_row, 2, "?");
                        reads_student.write_excel(start_write_row, 4, ("t{" + tables.b_var[0, j] + "} ="));
                        reads_student.write_excel(start_write_row, 5, "?");
                        start_write_row++;
                    }

                    reads.write_excel(start_write_row + 2, 1, tables.uravn);
                    reads_student.write_excel(start_write_row + 2, 1, "Y = ?");

                    reads_student.save_excel("Вариант " + (i + 1).ToString());
                    i++;
                }
                if (i > l)
                {
                    reads.save_excel("Варианты " + (l + 1).ToString() + "-" + (i).ToString());
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
