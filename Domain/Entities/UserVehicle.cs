namespace Taggy.Domain.Entities;

public class UserVehicle
{
    public Guid UserId { get; set; }
    public Guid VehicleId { get; set; }

    public User User { get; set; }
    public Vehicle Vehicle { get; set; }
}