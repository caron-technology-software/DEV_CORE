using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ProRob.Documents
{
    public class SpreadSheet
    {
        public static byte[] WriteXls(string sheetName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var wb = new XSSFWorkbook();

                wb.CreateSheet(sheetName);

                wb.Write(ms);

                wb.Close();

                Console.WriteLine(ms.ToArray().Count());

                return ms.ToArray();
            }
        }

        public static List<List<string>> GetMatrixFromExcelFile(string path, int indexSheet = 0)
        {
            var matrix = new List<List<string>>();

            using (FileStream fs = File.OpenRead(path))
            {
                var wb = new XSSFWorkbook(fs);
                var sheet = wb.GetSheetAt(indexSheet);
                int nRows = sheet.LastRowNum - sheet.FirstRowNum + 1;

                var rowEnumerator = sheet.GetRowEnumerator();
                while (rowEnumerator.MoveNext())
                {
                    var row = (IRow)rowEnumerator.Current;

                    if (row != null)
                    {
                        matrix.Add(new List<string>());

                        for (int col = 0; col < row.LastCellNum; col++)
                        {
                            var element = row.GetCell(col);

                            string value = string.Empty;

                            if (element != null)
                            {
                                value = element.StringCellValue.Trim(new char[] { ' ' });
                            }

                            matrix.Last().Add(value);
                        }
                    }
                }
            }

            return matrix;
        }
    }
}
