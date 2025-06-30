using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using FoodScrap.Domain.Entities;

namespace FoodScrap.Infrastructure.Reports
{
    public class ExcelReportService
    {
        public byte[] GenerateDishReport(List<Dish> dishes)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Platos");

            // Headers
            worksheet.Cell(1, 1).Value = "Nombre";
            worksheet.Cell(1, 2).Value = "Restaurante";
            worksheet.Cell(1, 3).Value = "Tipo";
            worksheet.Cell(1, 4).Value = "Precio";
            worksheet.Cell(1, 5).Value = "Rating Promedio";

            // Estilo encabezados
            var headerRange = worksheet.Range("A1:E1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.Cyan;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            int row = 2;
            foreach (var dish in dishes)
            {
                worksheet.Cell(row, 1).Value = dish.Name;
                worksheet.Cell(row, 2).Value = dish.Restaurant?.Name ?? "(sin restaurante)";
                worksheet.Cell(row, 3).Value = dish.Type ?? "-";
                worksheet.Cell(row, 4).Value = dish.Price;
                worksheet.Cell(row, 5).Value = dish.Reviews.Any()
                    ? dish.Reviews.Average(r => r.Rating ?? 0)
                    : 0;

                // Estilos fila
                for (int col = 1; col <= 5; col++)
                {
                    var cell = worksheet.Cell(row, col);
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                }

                row++;
            }

            // Ajustar columnas
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
