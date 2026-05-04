using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taggy.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_type = table.Column<string>(type: "text", nullable: false),
                    file_url = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fuels",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    efficiency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    co2_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fuels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "time_scales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    label = table.Column<string>(type: "text", nullable: false),
                    multiplier = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_scales", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    engine_type = table.Column<string>(type: "text", nullable: false),
                    consumption = table.Column<decimal>(type: "numeric", nullable: false),
                    co2_emission = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "waste_calculations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    toll_name = table.Column<string>(type: "text", nullable: false),
                    lanes = table.Column<int>(type: "integer", nullable: false),
                    vehicles_per_day = table.Column<int>(type: "integer", nullable: false),
                    non_print_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    ticket_weight = table.Column<decimal>(type: "numeric", nullable: false),
                    time_scale_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_waste_kg = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_waste_calculations", x => x.id);
                    table.ForeignKey(
                        name: "FK_waste_calculations_time_scales_time_scale_id",
                        column: x => x.time_scale_id,
                        principalTable: "time_scales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emission_calculations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fuel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    time_scale_id = table.Column<Guid>(type: "uuid", nullable: false),
                    period_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    period_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_emission = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emission_calculations", x => x.id);
                    table.ForeignKey(
                        name: "FK_emission_calculations_fuels_fuel_id",
                        column: x => x.fuel_id,
                        principalTable: "fuels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_emission_calculations_time_scales_time_scale_id",
                        column: x => x.time_scale_id,
                        principalTable: "time_scales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_emission_calculations_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_vehicles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_vehicles", x => new { x.user_id, x.vehicle_id });
                    table.ForeignKey(
                        name: "FK_user_vehicles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_vehicles_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_fuels",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fuel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    adjusted_consumption = table.Column<decimal>(type: "numeric", nullable: false),
                    adjusted_emission = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_fuels", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicle_fuels_fuels_fuel_id",
                        column: x => x.fuel_id,
                        principalTable: "fuels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vehicle_fuels_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emission_calculations_fuel_id",
                table: "emission_calculations",
                column: "fuel_id");

            migrationBuilder.CreateIndex(
                name: "IX_emission_calculations_time_scale_id",
                table: "emission_calculations",
                column: "time_scale_id");

            migrationBuilder.CreateIndex(
                name: "IX_emission_calculations_vehicle_id",
                table: "emission_calculations",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_vehicles_vehicle_id",
                table: "user_vehicles",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_fuels_fuel_id",
                table: "vehicle_fuels",
                column: "fuel_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_fuels_vehicle_id",
                table: "vehicle_fuels",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_waste_calculations_time_scale_id",
                table: "waste_calculations",
                column: "time_scale_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emission_calculations");

            migrationBuilder.DropTable(
                name: "exports");

            migrationBuilder.DropTable(
                name: "user_vehicles");

            migrationBuilder.DropTable(
                name: "vehicle_fuels");

            migrationBuilder.DropTable(
                name: "waste_calculations");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "fuels");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "time_scales");
        }
    }
}
