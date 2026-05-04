namespace Taggy.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    required public string Name { get; set; }
    required public string Email { get; set; }
    required public string Password { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<UserVehicle> ?UserVehicles { get; set; }
}