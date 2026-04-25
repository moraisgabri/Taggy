namespace Taggy.Domain.Entities;

public class Export
{
    public Guid Id { get; set; }
    public string FileType { get; set; }
    public string FileUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}