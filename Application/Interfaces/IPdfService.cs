using Taggy.Application.DTOs;
namespace Taggy.Application.Interfaces;


public interface IPdfService

// Comment for git example 
{
  Task<byte[]> GeneratePdf(int frequency, decimal fuel, decimal emission, decimal paper);
}