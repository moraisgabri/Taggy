using Taggy.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Taggy.Application.Services;

class PdfService: IPdfService
{
  public PdfService()
    {
      QuestPDF.Settings.License = LicenseType.Community;
    }

  public async Task<byte[]> GeneratePdf()
  {
    return Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Margin(30);

            page.Header()
                .Text("Relatório ESG - Taggy")
                .FontSize(20);

            page.Content()
                .Text("PDF gerado sob demanda.");

        });
    })
    .GeneratePdf();
  }
}