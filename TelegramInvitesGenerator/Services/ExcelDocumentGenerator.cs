using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using TelegramInvitesGenerator.Services.Abstractions;

namespace TelegramInvitesGenerator.Services
{
    public class ExcelDocumentGenerator : IDocumentGenerator
    {
        public async Task<byte[]> GenerateFromStringsAsync(string[][] table)
        {
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Worksheet");
            var rowsCount = table.GetLength(0);
            var columnsCount = table.GetLength(1);
            
            for (var rowIndex = 0; rowIndex < table.Length; rowIndex++)
            {
                for (var colIndex = 0; colIndex < table[rowIndex].Length; colIndex++)
                {
                    var row = rowIndex + 1;
                    var col = colIndex + 1;
                    var cellText = table[rowIndex][colIndex];

                    worksheet.Cells[row, col].Value = cellText;
                }
            }

            var tableRange = worksheet.Cells[1, 1, rowsCount, columnsCount];
            tableRange.AutoFitColumns();
            worksheet.Tables.Add(tableRange, "Table");

            return await package.GetAsByteArrayAsync();
        }
        
        public async Task<byte[]> GenerateFromObjectsAsync<T>(IEnumerable<T> objects)
        {
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Worksheet");

            var type = typeof(T);
            var typeProperties = type.GetProperties();

            var objectsArray = objects.ToArray();

            var rowsCount = objectsArray.Length + 1;
            var columnsCount = typeProperties.Length;

            for (var i = 0; i < typeProperties.Length; i++)
            {
                var col = i + 1;
                var property = typeProperties[i];
                var cellContent = property.Name;
                worksheet.Cells[1, col].Value = cellContent;
            }
            
            for (var objectIndex = 0; objectIndex < objectsArray.Length; objectIndex++)
            {
                for (var propertyIndex = 0; propertyIndex < typeProperties.Length; propertyIndex++)
                {
                    var row = objectIndex + 2;
                    var col = propertyIndex + 1;
                    var property = typeProperties[propertyIndex];
                    var @object = objectsArray[objectIndex];
                    var cellContent = property.GetValue(@object);

                    worksheet.Cells[row, col].Value = cellContent;
                }
            }

            var tableRange = worksheet.Cells[1, 1, rowsCount, columnsCount];
            tableRange.AutoFitColumns();
            worksheet.Tables.Add(tableRange, "Table");

            return await package.GetAsByteArrayAsync();
        }
    }
}