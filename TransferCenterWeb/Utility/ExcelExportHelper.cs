using ClosedXML.Excel;
using System.Collections;
using System.Reflection;
using System;
namespace TransferCenterWeb.Utility
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

                // Step 2: Add sheets sequentially to the workbook
                using var workbook = new XLWorkbook();
                foreach (var task in sheetDataTasks)
                {
                    var (sheetName, type, data, rows) = task.Result;
                    AddSheetFromRows(workbook, sheetName, rows);
                }

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

        // Prepares the header and row data for a sheet
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

        // Adds a sheet to the workbook from prepared rows
        private static void AddSheetFromRows(XLWorkbook workbook, string sheetName, (string[] headers, List<string[]> rows) sheetData)
        {
            var ws = workbook.Worksheets.Add(sheetName);
            var headers = sheetData.headers;
            var rows = sheetData.rows;
            for (int i = 0; i < headers.Length; i++)
            {
                ws.Cell(1, i + 1).Value = headers[i];
                ws.Cell(1, i + 1).Style.Font.Bold = true;
            }
            for (int row = 0; row < rows.Count; row++)
            {
                for (int col = 0; col < headers.Length; col++)
                {
                    ws.Cell(row + 2, col + 1).Value = rows[row][col];
                }
            }
            ws.Columns().AdjustToContents();
        }

        private static void CreateSheet(XLWorkbook workbook, string sheetName, Type type, IEnumerable data,
            HashSet<string> excludeFields = null)
        {
            try
            {
                var ws = workbook.Worksheets.Add(sheetName);
                PropertyInfo[] properties = null;
                object firstItem = null;
                bool hasData = false;
                if (data != null)
                {
                    var enumerator = data.GetEnumerator();
                    if (enumerator.MoveNext() && enumerator.Current != null)
                    {
                        firstItem = enumerator.Current;
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
                // Header
                for (int i = 0; i < properties.Length; i++)
                {
                    ws.Cell(1, i + 1).Value = properties[i].Name;
                    ws.Cell(1, i + 1).Style.Font.Bold = true;
                }

                int row = 2;
                if (hasData)
                {
                    // Write the first item
                    for (int col = 0; col < properties.Length; col++)
                    {
                        var value = properties[col].GetValue(firstItem) ?? string.Empty;
                        ws.Cell(row, col + 1).Value = value.ToString();
                    }
                    row++;
                    // Write the rest
                    while (data is IEnumerable enumerable)
                    {
                        var enumerator = enumerable.GetEnumerator();
                        bool skippedFirst = false;
                        while (enumerator.MoveNext())
                        {
                            var item = enumerator.Current;
                            if (!skippedFirst) { skippedFirst = true; continue; }
                            if (item == null) continue;
                            for (int col = 0; col < properties.Length; col++)
                            {
                                var value = properties[col].GetValue(item) ?? string.Empty;
                                ws.Cell(row, col + 1).Value = value.ToString();
                            }
                            row++;
                        }
                        break;
                    }
                }
                ws.Columns().AdjustToContents();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }


        public static HashSet<string> GetExcelExportExcludeFields(this IConfiguration? config, string key)
        {
            if (config == null || string.IsNullOrEmpty(key))
                return [];
            return (config[key] ?? "").Split(',',
                    System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries)
                .ToHashSet(System.StringComparer.OrdinalIgnoreCase);
        }

    }
}
