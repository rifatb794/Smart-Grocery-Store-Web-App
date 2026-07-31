using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Smart_Grocery_Store_Web_App.Models;
using System.Linq;

namespace Smart_Grocery_Store_Web_App.Models
{
    public class InvoiceDocument : IDocument
    {
        private readonly Order _order;

        public InvoiceDocument(Order order)
        {
            _order = order;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text("Thank you for shopping with Smart Grocery");
            });
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("SMART GROCERY STORE")
                        .FontSize(20)
                        .Bold();

                    col.Item().Text("Premium Online Grocery Shop");
                });

                row.ConstantItem(200).Column(col =>
                {
                    col.Item().Text($"Invoice #: {_order.Id}");
                    col.Item().Text($"Date: {_order.OrderDate:dd MMM yyyy}");
                    col.Item().Text($"Status: {_order.Status}");
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(col =>
            {
                col.Item().Text("Customer Information").Bold();

                col.Item().Text($"Name: {_order.CustomerName}");
                col.Item().Text("");

                col.Item().Element(ComposeTable);

                col.Item().PaddingTop(15).AlignRight().Column(total =>
                {
                    total.Item().Text($"Sub Total: {_order.SubTotal} ৳");
                    total.Item().Text($"VAT (5%): {_order.Vat} ৳");
                    total.Item().Text($"Discount: {_order.Discount} ৳").FontColor(Colors.Green.Darken1);
                    total.Item().Text($"Grand Total: {_order.GrandTotal} ৳")
                        .Bold()
                        .FontSize(14);
                });
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Item");
                    header.Cell().Element(CellStyle).Text("Price");
                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");

                    static IContainer CellStyle(IContainer c) =>
                        c.DefaultTextStyle(x => x.Bold())
                         .PaddingVertical(5)
                         .BorderBottom(1);
                });

                foreach (var item in _order.Items)
                {
                    table.Cell().Element(Cell).Text(item.Name);
                    table.Cell().Element(Cell).Text($"{item.Price} ৳");
                    table.Cell().Element(Cell).Text(item.Quantity.ToString());
                    table.Cell().Element(Cell).Text($"{item.Total} ৳");

                    static IContainer Cell(IContainer c) =>
                        c.PaddingVertical(5)
                         .BorderBottom(0.5f);
                }
            });
        }
    }
}
