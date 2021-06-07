using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace raimbow
{
    class Write_Excel
    {
        string link;
        // Создаём экземпляр нашего приложения
        Excel.Application excelApp;
        // Создаём экземпляр рабочий книги Excel
        Excel.Workbook workBook;
        // Создаём экземпляр листа Excel
        Excel.Worksheet workSheet;
        bool first_work_sheet;
        public Write_Excel(string link)
        {
            this.link = link;
            excelApp = new Excel.Application();
            workBook = excelApp.Workbooks.Add();
            workBook.Application.DisplayAlerts = false; 
            first_work_sheet = true;
        }
        public void create_sheet(string name_sheet)
        {
            if (first_work_sheet)
            {
                workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
                first_work_sheet = false;
            }
            else
            {
                workSheet = (Excel.Worksheet)workBook.Sheets.Add(After: workBook.ActiveSheet);
            }
            workSheet.Name = name_sheet;
        }

        public void write_excel(int x, int y, string[,] mass)
        {
            for(int row = 0; row < mass.GetLength(0); row++)
            {
                for(int column = 0; column < mass.GetLength(1); column++)
                {
                    workSheet.Cells[x + row, y + column] = mass[row, column];
                }
            }
        }
        public void driving_borders()
        {
            // Найти последнюю заполненую строку
            int lastUsedRow = workSheet.Cells.Find("*", System.Reflection.Missing.Value,
                                           System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                           Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious,
                                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

            // Найти последний заполненый столбец
            int lastUsedColumn = workSheet.Cells.Find("*", System.Reflection.Missing.Value,
                                           System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                           Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious,
                                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;

            var cells = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[lastUsedRow, lastUsedColumn]];
            cells.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous; // внутренние вертикальные
            cells.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous; // внутренние горизонтальные            
            cells.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous; // верхняя внешняя
            cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous; // правая внешняя
            cells.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous; // левая внешняя
            cells.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
        }
        public void sheet_merge(int start_x, int start_y, int end_x, int end_y, object deving)
        {
            workSheet.Range[workSheet.Cells[start_x, start_y], workSheet.Cells[end_x, end_y]].Merge();
            workSheet.Cells[start_x, start_y] = deving.ToString();
        }
        public void write_excel(int x, int y, object deving)
        {
            workSheet.Cells[x, y] = deving.ToString();
        }
        public void save_excel(string name_excel)
        {
            first_work_sheet = false;
            excelApp.Application.ActiveWorkbook.SaveAs(link + '\\' + name_excel);
            workBook.Close(true);
            excelApp.Quit(); 
            workSheet = null;
            workBook = null;
            excelApp = null;
        }
    }
}
