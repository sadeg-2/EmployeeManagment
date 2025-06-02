using ClosedXML.Excel;
using System.Reflection;
namespace Server.Library.Helpers
{
    public static class ExcelExportHelper
    {
        // Generic method to export any list to Excel byte array
        public static byte[] ExportToExcel<T>(IEnumerable<T> data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            // Get properties of T for headers
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Write headers
            for (int i = 0; i < props.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = props[i].Name;
            }

            int row = 2;
            foreach (var item in data)
            {
                for (int col = 0; col < props.Length; col++)
                {
                    var val = props[col].GetValue(item);

                    // Support nested objects, e.g. Country.Name
                    if (val != null && !IsSimple(val.GetType()))
                    {
                        // For nested properties, just write ToString()
                        worksheet.Cell(row, col + 1).Value = val.ToString();
                    }
                    else
                    {
                        val = "";
                        worksheet.Cell(row, col + 1).Value = val.ToString();
                    }
                }
                row++;
            }

            using var ms = new MemoryStream();
            workbook.SaveAs(ms);
            return ms.ToArray();
        }

        // Helper to check if type is primitive/simple (string, int, etc)
        private static bool IsSimple(System.Type type)
        {
            return
                type.IsPrimitive ||
                new System.Type[] {
                typeof(string),
                typeof(decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid)
                }.Contains(type) ||
                (Nullable.GetUnderlyingType(type) != null && IsSimple(Nullable.GetUnderlyingType(type)));
        }
    }

}
