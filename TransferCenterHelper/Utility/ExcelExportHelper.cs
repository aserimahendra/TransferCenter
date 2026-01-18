using System.Collections;
using System.Reflection;
using ClosedXML.Excel;

namespace TransferCenterHelper.Utility
{
    public static class ExcelExportHelper
    {
        public static byte[] ExportToExcel(Dictionary<string, (Type type, IEnumerable data)> sheets,
            HashSet<string> excludeFields = null)
        {
            try
            {
                // Step 1: Prepare all sheet data in parallel
                var sheetDataTasks = sheets.Select(sheet => Task.Run(() =>
                {
                    var (sheetName, (type, data)) = (sheet.Key, sheet.Value);
                    return (sheetName, type, data, rows: PrepareSheetRows(type, data, excludeFields));
                })).ToArray();
                Task.WaitAll(sheetDataTasks);

                // Step 2: Combine all data into a single sheet
                using var workbook = new XLWorkbook();
                var allRows = new List<(string SheetName, string[] Headers, List<string[]> Rows)>();
                foreach (var task in sheetDataTasks)
                {
                    var (sheetName, type, data, rows) = task.Result;
                    allRows.Add((sheetName, rows.headers, rows.rows));
                }
                // Combine headers
                var combinedHeaders = allRows.SelectMany(r => r.Headers.Select(h => $"{r.SheetName}_{h}")).ToArray();
                var maxRows = allRows.Max(r => r.Rows.Count);
                var combinedRows = new List<string[]>();
                for (int i = 0; i < maxRows; i++)
                {
                    var row = new List<string>();
                    foreach (var r in allRows)
                    {
                        if (i < r.Rows.Count)
                            row.AddRange(r.Rows[i]);
                        else
                            row.AddRange(Enumerable.Repeat(string.Empty, r.Headers.Length));
                    }
                    combinedRows.Add(row.ToArray());
                }
                var ws = workbook.Worksheets.Add("ExportedData");
                for (int i = 0; i < combinedHeaders.Length; i++)
                {
                    ws.Cell(1, i + 1).Value = combinedHeaders[i];
                    ws.Cell(1, i + 1).Style.Font.Bold = true;
                }
                for (int row = 0; row < combinedRows.Count; row++)
                {
                    for (int col = 0; col < combinedHeaders.Length; col++)
                    {
                        ws.Cell(row + 2, col + 1).Value = combinedRows[row][col];
                    }
                }
                ws.Columns().AdjustToContents();
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static (string[] headers, List<string[]> rows) PrepareSheetRows(Type type, IEnumerable data, HashSet<string> excludeFields = null)
        {
            PropertyInfo[] properties = null;
            object firstItem = null;
            bool hasData = false;
            List<object> dataList = new List<object>();
            if (data != null)
            {
                var enumerator = data.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current != null)
                        dataList.Add(enumerator.Current);
                }
                if (dataList.Count > 0)
                {
                    firstItem = dataList[0];
                    hasData = true;
                    var runtimeType = firstItem.GetType();
                    properties = runtimeType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => excludeFields == null || !excludeFields.Contains(p.Name)).ToArray();
                }
            }
            if (properties == null)
            {
                properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => excludeFields == null || !excludeFields.Contains(p.Name)).ToArray();
            }
            var headers = properties.Select(p => p.Name).ToArray();
            var rows = new List<string[]>();
            foreach (var item in dataList)
            {
                var row = new string[properties.Length];
                for (int col = 0; col < properties.Length; col++)
                {
                    var value = properties[col].GetValue(item) ?? string.Empty;
                    row[col] = value.ToString();
                }
                rows.Add(row);
            }
            return (headers, rows);
        }
    }
}
