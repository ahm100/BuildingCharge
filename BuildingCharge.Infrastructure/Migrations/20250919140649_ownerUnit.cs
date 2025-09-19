using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingCharge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ownerUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Units",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResidentName",
                table: "Units",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "ResidentName",
                table: "Units");
        }
    }
}
