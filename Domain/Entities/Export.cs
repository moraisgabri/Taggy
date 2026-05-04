namespace Taggy.Domain.Entities;

public class Export
{
    public Guid Id { get; set; }
    required public string FileType { get; set; }
    required public string FileUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}