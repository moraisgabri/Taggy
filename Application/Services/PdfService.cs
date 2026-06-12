using Taggy.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Taggy.Domain.Interfaces;
using Taggy.Domain.Entities;

namespace Taggy.Application.Services;

class PdfService: IPdfService

{
  private readonly IUserRepository userRepository;

  public PdfService(IUserRepository _userRepository)
  
    {
      userRepository = _userRepository;
      QuestPDF.Settings.License = LicenseType.Community;

    }

  public async Task<byte[]> GeneratePdf(int frequency, decimal fuel, decimal emission, decimal paper)
  {
    return Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(32);
            page.DefaultTextStyle(t => t.FontFamily("Arial").FontSize(10));
 
            // ── Cabeçalho ──────────────────────────────────────────
            page.Header()
                .Background("#1B5E20")
                .Padding(20)
                .Column(col =>
                {
                    col.Item()
                       .Text("RELATÓRIO ESG 2026 – TAGGY")
                       .Bold().FontSize(16).FontColor("#FFFFFF");
 
                    col.Item()
                       .Text("Complexo Operacional: Tecnologia Taggy & Matriz de Materialidade")
                       .FontSize(9).FontColor("#A5D6A7");
                });
 
            // ── Conteúdo ───────────────────────────────────────────
            page.Content()
                .PaddingVertical(16)
                .Column(col =>
                {
                    col.Spacing(14);
 
                    // Mensagem da Liderança
                    col.Item().Text("1. Mensagem da Liderança")
                       .Bold().FontSize(12).FontColor("#1B5E20");
 
                    col.Item()
                       .Background("#E8F5E9")
                       .Border(1).BorderColor("#43A047")
                       .Padding(12)
                       .Text("\"A tecnologia RFID da Taggy mitiga o impacto ambiental em 100% das rodovias pedagiadas " +
                             "e em mais de 1.000 estacionamentos no Brasil. Compromisso Net Zero até 2050.\"")
                       .Italic().FontSize(10).FontColor("#2E7D32");
 
                    // Escopo Ambiental
                    col.Item().Text("2. Escopo Ambiental (E – Environmental)")
                       .Bold().FontSize(12).FontColor("#1B5E20");
 
                    col.Item()
                       .Text("Redução estimada de até 25% nas emissões de CO₂ por passagem, " +
                             "baseada na eliminação do ciclo de desaceleração, marcha lenta (idling) e aceleração brusca.");
 
                    // Métricas individuais
                    col.Item().Text("3.1. Visão do Usuário Individual")
                       .Bold().FontSize(11).FontColor("#2E7D32");

                    col.Item()
                       .Text("Focado na experiência do usuário, na tangibilização do esforço ecológico e em dinâmicas de conscientização e gamificação.");
 
                    var metricasIndividual = new[]
                    {
                        ("Emissões de CO₂ Evitadas",   $"{emission} kg de CO₂ evitados — equivalente a [Y] árvores plantadas"),
                        ("Economia de Combustível",     $"{fuel} litros economizados (Gasolina/Etanol/Diesel)"),
                        ("Resíduos de Papel Eliminados",$"{paper} recibos físicos evitados"),
                        ("Passagens em Zonas Ecológicas",$"{frequency} acessos em áreas de TPA (ex: Ubatuba/SP, Bombinhas/SC)"),
                    };
 
                    foreach (var (titulo, descricao) in metricasIndividual)
                    {
                        col.Item().Row(row =>
                        {
                            row.ConstantItem(12).Text("●").FontSize(8).FontColor("#43A047");
                            row.RelativeItem().Text(t =>
                            {
                                t.Span($"{titulo}: ").Bold();
                                t.Span(descricao);
                            });
                        });
                    }
 
 
                    // Tabela de indicadores
                    col.Item().Text("4. Tabulação de Indicadores")
                       .Bold().FontSize(12).FontColor("#1B5E20");
 
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn(2);
                            c.RelativeColumn(3);
                            c.RelativeColumn(2);
                            c.RelativeColumn(2);
                        });
 
                        table.Header(h =>
                        {
                            foreach (var label in new[] { "Categoria", "Métrica", "Individual", "            " })
                                h.Cell().Background("#1B5E20").Padding(6)
                                 .Text(label).Bold().FontSize(9).FontColor("#FFFFFF");
                        });
 
                        var linhas = new[]
                        {
                            ("Clima",          "Massa de CO₂ evitada",        $"{emission} kg"),
                            ("Fósseis",        "Combustível poupado",          $"{fuel} L"),
                            ("Circularidade",  "Papel térmico evitado",        $"{paper} gramas"),
                            ("Operação",       "Total de transações",          $"{frequency} unid."),
                        };
 
                        bool par = false;
                        foreach (var (cat, metrica, ind) in linhas)
                        {
                            var bg = par ? "#F1F8E9" : "#FFFFFF";
                            par = !par;
                            table.Cell().Background(bg).BorderBottom(1).BorderColor("#E0E0E0").Padding(6).Text(cat).FontSize(9);
                            table.Cell().Background(bg).BorderBottom(1).BorderColor("#E0E0E0").Padding(6).Text(metrica).FontSize(9);
                            table.Cell().Background(bg).BorderBottom(1).BorderColor("#E0E0E0").Padding(6).AlignCenter().Text(ind).FontSize(9).FontColor("#2E7D32");
                            table.Cell().Background(bg).BorderBottom(1).BorderColor("#E0E0E0").Padding(6).AlignCenter().Text("       ").FontSize(9).FontColor("#2E7D32");
                        };
                    });
 
                    // Governança
                    col.Item().Text("5. Governança (G – Governance)")
                       .Bold().FontSize(12).FontColor("#1B5E20");
 
                    col.Item().Text("A calculadora de carbono mitiga o risco de greenwashing com metodologia baseada " +
                                    "em engenharia de tráfego e fatores de emissão oficiais, transformando a Taggy " +
                                    "em ativo estratégico de conformidade ESG.");
                });
 
            // ── Rodapé ─────────────────────────────────────────────
            page.Footer()
                .Background("#1B5E20")
                .PaddingHorizontal(20)
                .PaddingVertical(8)
                .Row(row =>
                {
                    row.RelativeItem()
                       .Text("Edenred | Taggy — Relatório ESG 2026")
                       .FontSize(8).FontColor("#A5D6A7");
 
                    row.ConstantItem(60).AlignRight().Text(t =>
                    {
                        t.CurrentPageNumber().FontSize(8).FontColor("#FFFFFF");
                    });
                });
        });
    })
    .GeneratePdf();
  }
}