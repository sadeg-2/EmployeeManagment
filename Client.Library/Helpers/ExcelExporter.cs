using ClosedXML.Excel;
using System.Reflection;
namespace Client.Library.Helpers
{
    public static class ExcelExporter
    {
        public static byte[] ExportToExcel<T>(IEnumerable<T> data, string sheetName = "Sheet1")
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => IsSimpleType(p.PropertyType))
                .ToList();

            // Header
            for (int i = 0; i < properties.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
            }

            // Data rows
            int row = 2;
            foreach (var item in data)
            {
                for (int col = 0; col < properties.Count; col++)
                {
                    var value = properties[col].GetValue(item);
                    worksheet.Cell(row, col + 1).Value = value?.ToString() ?? string.Empty;
                }
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        private static bool IsSimpleType(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            return underlyingType.IsPrimitive
                   || underlyingType.IsEnum
                   || underlyingType == typeof(string)
                   || underlyingType == typeof(DateTime)
                   || underlyingType == typeof(decimal)
                   || underlyingType == typeof(Guid)
                   || underlyingType == typeof(TimeSpan);
        }
    }

}
