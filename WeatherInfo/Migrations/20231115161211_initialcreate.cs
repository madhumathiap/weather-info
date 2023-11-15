using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherInfo.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentTemperature = table.Column<double>(type: "REAL", nullable: false),
                    MinimumTemperature = table.Column<double>(type: "REAL", nullable: false),
                    MaximumTemperature = table.Column<double>(type: "REAL", nullable: false),
                    Humidity = table.Column<int>(type: "INTEGER", nullable: false),
                    Sunrise = table.Column<string>(type: "TEXT", nullable: false),
                    Sunset = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherDetails");
        }
    }
}
