using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Smart_Grocery_Store_Web_App.Models;

public class InvoiceController : Controller
{
    public IActionResult Download(int id)
    {
        var order = FakeDb.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null) return NotFound();

        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);

                page.Content().Column(col =>
                {
                    col.Item().Text("Smart Grocery Invoice")
                        .FontSize(22).Bold();

                    col.Item().Text($"Order ID: #{order.Id}");
                    col.Item().Text($"Status: {order.Status}");

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                            c.ConstantColumn(50);
                            c.ConstantColumn(80);
                        });

                        table.Header(h =>
                        {
                            h.Cell().Text("Product").Bold();
                            h.Cell().Text("Qty").Bold();
                            h.Cell().Text("Total").Bold();
                        });

                        foreach (var i in order.Items)
                        {
                            table.Cell().Text(i.Name);
                            table.Cell().Text(i.Quantity.ToString());
                            table.Cell().Text(i.Total + " ৳");
                        }
                    });

                    col.Item().Text($"Grand Total: {order.GrandTotal} ৳")
                        .Bold().FontSize(16);
                });
            });
        });

        return File(pdf.GeneratePdf(),
            "application/pdf",
            $"Invoice_{order.Id}.pdf");
    }
}
