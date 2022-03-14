using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raimbow
{
    class ReadExcelFormuls
    {

        public void reading(string[] texter, int var, Save_date saves)
        {
            try
            {
                Generate_Table tables = new Generate_Table(saves.col_factory, saves.y_min, saves.y_max, saves.tT);
                foreach (string link in texter)
                {
                    Write_Excel reads = new Write_Excel(link);

                    reads.create_sheet("Вариант " + var.ToString());

                    reads.sheet_merge(1, 1, 2, 1, "Условие\n\r проведе\n\rния экспери\n\rмента");
                    reads.sheet_merge(1, 2, 1, 2 + saves.max_var.Length - 1, "Натуральные значения \n\ri-го фактора");
                    int ins = 2 + saves.max_var.Length;
                    reads.sheet_merge(1, ins, 1, ins + saves.max_var.Length - 1, "Натуральные значения \n\ri-го фактора");
                    for (int j = 0; j < saves.max_var.Length; j++)
                    {
                        reads.write_excel(2, 2 + j, "X" + (j + 1).ToString());
                        reads.write_excel(2, ins + j, "X" + (j + 1).ToString());
                    }
                    reads.write_excel(6, 1, "Нижний\n\rуровень\n\rфактора");
                    reads.write_excel(5, 1, "Верхний\n\rуровень\n\rфактора");
                    reads.write_excel(4, 1, "Интерва\n\rл варьиро\n\rвания \n\rфактора");
                    reads.write_excel(3, 1, "Основно\n\rй \n\rуровень\n\rфактора");
                    for (int j = 0; j < saves.max_var.Length; j++)
                    {
                        reads.write_excel(6, 2 + j, saves.min_var[j].ToString());
                        reads.write_excel(6, ins + j, "-1");
                        reads.write_excel(5, 2 + j, saves.max_var[j].ToString());
                        reads.write_excel(5, ins + j, "+1");

                        double sens = (saves.max_var[j] - saves.min_var[j]) / 2;

                        reads.write_excel(4, 2 + j, sens.ToString());
                        reads.write_excel(4, ins + j, "1");
                        reads.write_excel(3, 2 + j, (saves.min_var[j] + sens).ToString());
                        reads.write_excel(3, ins + j, "0");
                    }
                    int start_write_column = 0;
                    int start_write_row = 8;
                    reads.sheet_merge(start_write_row, 1, (start_write_row + 1), 1, "№");
                    reads.write_excel((start_write_row + 1), 2, "X0");
                    for (int z = 0; z < saves.col_factory; z++)
                    {
                        start_write_column = 3 + z;
                        reads.write_excel((start_write_row + 1), start_write_column, "X" + (z + 1).ToString());
                    }
                    reads.sheet_merge(start_write_row, 2, start_write_row, start_write_column, "Факторы");
                    reads.sheet_merge(start_write_row, (1 + start_write_column), start_write_row, (start_write_column + saves.col_factory), "Номер серии");
                    for (int z = 1; z <= saves.col_factory; z++)
                    {
                        start_write_column = start_write_column + 1;
                        reads.write_excel((start_write_row + 1), start_write_column, z.ToString());
                    }
                    reads.sheet_merge(start_write_row, (1 + start_write_column), start_write_row, (start_write_column + saves.col_factory), "y");
                    for (int z = 1; z <= saves.col_factory; z++)
                    {
                        start_write_column = start_write_column + 1;
                        reads.write_excel((start_write_row + 1), start_write_column, "y" + z.ToString());
                    }
                    start_write_column = start_write_column + 1;
                    reads.sheet_merge(start_write_row, start_write_column, (start_write_row + 1), start_write_column, "Ys");
                    start_write_column = start_write_column + 1;
                    reads.sheet_merge(start_write_row, start_write_column, (start_write_row + 1), start_write_column, "S(o)");

                    if (link.Equals(texter[texter.Length - 1]))
                    {
                        reads.write_excel((start_write_row + 2), 1, tables.Get_table_for_student);
                    }
                    else
                    {
                        reads.write_excel((start_write_row + 2), 1, tables.Get_table_for_teacher);
                    }

                    start_write_row = start_write_row + tables.Row + 4;
                    reads.write_excel(start_write_row, 1, "S^2{Y} = ");
                    reads.write_excel(start_write_row + 1, 1, "S^2{Ys} = ");
                    reads.write_excel(start_write_row + 2, 1, "S^2{bi} = ");
                    reads.write_excel(start_write_row + 3, 1, "S{bi} = ");
                    reads.write_excel(start_write_row + 4, 1, "tT = ");
                    reads.write_excel(start_write_row + 4, 2, tables.tT);

                    for (int j = 0; j < tables.ss.Length; j++)
                    {
                        if (link.Equals(texter[texter.Length - 1]))
                        {
                            reads.write_excel(start_write_row, 2, "?");
                        }
                        else
                        {
                            reads.write_excel(start_write_row, 2, tables.ss[j]);
                        }
                        start_write_row++;
                    }
                    start_write_row += 2;

                    for (int j = 0; j < tables.b_var.GetLength(1); j++)
                    {
                        if (link.Equals(texter[texter.Length - 1]))
                        {
                            reads.write_excel(start_write_row, 1, (tables.b_var[0, j] + " ="));
                            reads.write_excel(start_write_row, 2, "?");
                            reads.write_excel(start_write_row, 4, ("t{" + tables.b_var[0, j] + "} ="));
                            reads.write_excel(start_write_row, 5, "?");
                        }
                        else
                        {
                            reads.write_excel(start_write_row, 1, (tables.b_var[0, j] + " ="));
                            reads.write_excel(start_write_row, 2, tables.b_var[1, j]);
                            reads.write_excel(start_write_row, 4, ("t{" + tables.b_var[0, j] + "} ="));
                            reads.write_excel(start_write_row, 5, tables.b_var[2, j]);
                        }

                        start_write_row++;
                    }

                    if (link.Equals(texter[texter.Length - 1]))
                    {
                        reads.write_excel(start_write_row + 2, 1, "Y = ?");
                    }
                    else
                    {
                        reads.write_excel(start_write_row + 2, 1, tables.uravn);
                    }

                    reads.save_excel("Вариант " + var.ToString());
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Что-то пошло не так", "Внимание", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }
    }
}
