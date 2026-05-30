using Taggy.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Taggy.Infrastructure.Repositories;
using Taggy.Domain.Interfaces;
using Taggy.Domain.Entities;

namespace Taggy.Application.Services;

class PdfService: IPdfService

{
  private readonly IUserRepository userRepository;

  public PdfService(UserRepository _userRepository)
  
    {
      userRepository = _userRepository;
      QuestPDF.Settings.License = LicenseType.Community;

    }

  public async Task<byte[]> GeneratePdf(string userId)
  {
    User user = await userRepository.GetByIdAsync(Guid.Parse(userId)) ?? throw new Exception("User not found");

    return Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Margin(30);

            page.Header()
                .Text("Relatório ESG - Taggy")
                .FontSize(20);

            page.Content()
                .Text($"PDF gerado sob demanda de: {user.Name}");

            page.Content()
                .Text($"Email: {user.Email}");

            page.Content()
                .Text($"Data de geração: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
            page.Content()
                .Text($"{user.Password}");

        });
    })
    .GeneratePdf();
  }
}