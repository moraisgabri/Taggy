namespace Taggy.Domain.Entities;

public class UserVehicle
{
    public Guid UserId { get; set; }
    public Guid VehicleId { get; set; }

    required public User User { get; set; }
    required public Vehicle Vehicle { get; set; }
}