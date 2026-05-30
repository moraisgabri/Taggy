using Taggy.Application.DTOs;
namespace Taggy.Application.Interfaces;


public interface IPdfService
{
  Task<byte[]> GeneratePdf();
}