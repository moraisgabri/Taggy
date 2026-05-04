using Microsoft.EntityFrameworkCore;
using Taggy.Domain.Entities;

namespace Taggy.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Fuel> Fuels => Set<Fuel>();
    public DbSet<UserVehicle> UserVehicles => Set<UserVehicle>();
    public DbSet<VehicleFuel> VehicleFuels => Set<VehicleFuel>();
    public DbSet<TimeScale> TimeScales => Set<TimeScale>();
    public DbSet<EmissionCalculation> EmissionCalculations => Set<EmissionCalculation>();
    public DbSet<WasteCalculation> WasteCalculations => Set<WasteCalculation>();
    public DbSet<Export> Exports => Set<Export>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("users");
            e.HasKey(u => u.Id);
            e.Property(u => u.Id).HasColumnName("id");
            e.Property(u => u.Name).HasColumnName("name");
            e.Property(u => u.Email).HasColumnName("email").IsRequired();
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Password).HasColumnName("password");
            e.Property(u => u.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<Vehicle>(e =>
        {
            e.ToTable("vehicles");
            e.HasKey(v => v.Id);
            e.Property(v => v.Id).HasColumnName("id");
            e.Property(v => v.Name).HasColumnName("name");
            e.Property(v => v.EngineType).HasColumnName("engine_type");
            e.Property(v => v.Consumption).HasColumnName("consumption");
            e.Property(v => v.Co2Emission).HasColumnName("co2_emission");
            e.Property(v => v.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<Fuel>(e =>
        {
            e.ToTable("fuels");
            e.HasKey(f => f.Id);
            e.Property(f => f.Id).HasColumnName("id");
            e.Property(f => f.Name).HasColumnName("name");
            e.Property(f => f.Type).HasColumnName("type");
            e.Property(f => f.EfficiencyFactor).HasColumnName("efficiency_factor");
            e.Property(f => f.Co2Factor).HasColumnName("co2_factor");
            e.Property(f => f.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<UserVehicle>(e =>
        {
            e.ToTable("user_vehicles");
            e.HasKey(uv => new { uv.UserId, uv.VehicleId });
            e.Property(uv => uv.UserId).HasColumnName("user_id");
            e.Property(uv => uv.VehicleId).HasColumnName("vehicle_id");
            e.HasOne(uv => uv.User).WithMany(u => u.UserVehicles).HasForeignKey(uv => uv.UserId);
            e.HasOne(uv => uv.Vehicle).WithMany(v => v.UserVehicles).HasForeignKey(uv => uv.VehicleId);
        });

        modelBuilder.Entity<VehicleFuel>(e =>
        {
            e.ToTable("vehicle_fuels");
            e.HasKey(vf => vf.Id);
            e.Property(vf => vf.Id).HasColumnName("id");
            e.Property(vf => vf.VehicleId).HasColumnName("vehicle_id");
            e.Property(vf => vf.FuelId).HasColumnName("fuel_id");
            e.Property(vf => vf.AdjustedConsumption).HasColumnName("adjusted_consumption");
            e.Property(vf => vf.AdjustedEmission).HasColumnName("adjusted_emission");
            e.HasOne(vf => vf.Vehicle).WithMany(v => v.VehicleFuels).HasForeignKey(vf => vf.VehicleId);
            e.HasOne(vf => vf.Fuel).WithMany(f => f.VehicleFuels).HasForeignKey(vf => vf.FuelId);
        });

        modelBuilder.Entity<TimeScale>(e =>
        {
            e.ToTable("time_scales");
            e.HasKey(t => t.Id);
            e.Property(t => t.Id).HasColumnName("id");
            e.Property(t => t.Label).HasColumnName("label");
            e.Property(t => t.Multiplier).HasColumnName("multiplier");
            e.Property(t => t.Description).HasColumnName("description");
        });

        modelBuilder.Entity<EmissionCalculation>(e =>
        {
            e.ToTable("emission_calculations");
            e.HasKey(ec => ec.Id);
            e.Property(ec => ec.Id).HasColumnName("id");
            e.Property(ec => ec.VehicleId).HasColumnName("vehicle_id");
            e.Property(ec => ec.FuelId).HasColumnName("fuel_id");
            e.Property(ec => ec.TimeScaleId).HasColumnName("time_scale_id");
            e.Property(ec => ec.PeriodStart).HasColumnName("period_start");
            e.Property(ec => ec.PeriodEnd).HasColumnName("period_end");
            e.Property(ec => ec.TotalEmission).HasColumnName("total_emission");
            e.Property(ec => ec.CreatedAt).HasColumnName("created_at");
            e.HasOne(ec => ec.Vehicle).WithMany(v => v.EmissionCalculations).HasForeignKey(ec => ec.VehicleId);
            e.HasOne(ec => ec.Fuel).WithMany(f => f.EmissionCalculations).HasForeignKey(ec => ec.FuelId);
            e.HasOne(ec => ec.TimeScale).WithMany(t => t.EmissionCalculations).HasForeignKey(ec => ec.TimeScaleId);
        });

        modelBuilder.Entity<WasteCalculation>(e =>
        {
            e.ToTable("waste_calculations");
            e.HasKey(wc => wc.Id);
            e.Property(wc => wc.Id).HasColumnName("id");
            e.Property(wc => wc.TollName).HasColumnName("toll_name");
            e.Property(wc => wc.Lanes).HasColumnName("lanes");
            e.Property(wc => wc.VehiclesPerDay).HasColumnName("vehicles_per_day");
            e.Property(wc => wc.NonPrintRate).HasColumnName("non_print_rate");
            e.Property(wc => wc.TicketWeight).HasColumnName("ticket_weight");
            e.Property(wc => wc.TimeScaleId).HasColumnName("time_scale_id");
            e.Property(wc => wc.TotalWasteKg).HasColumnName("total_waste_kg");
            e.Property(wc => wc.CreatedAt).HasColumnName("created_at");
            e.HasOne(wc => wc.TimeScale).WithMany(t => t.WasteCalculations).HasForeignKey(wc => wc.TimeScaleId);
        });

        modelBuilder.Entity<Export>(e =>
        {
            e.ToTable("exports");
            e.HasKey(ex => ex.Id);
            e.Property(ex => ex.Id).HasColumnName("id");
            e.Property(ex => ex.FileType).HasColumnName("file_type");
            e.Property(ex => ex.FileUrl).HasColumnName("file_url");
            e.Property(ex => ex.CreatedAt).HasColumnName("created_at");
        });
    }
}
