using Microsoft.EntityFrameworkCore.Migrations;

namespace MyIoTService.Infrastructure.EF.Migrations
{
    public partial class UpdateDevicesAddEnabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "devices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "devices");
        }
    }
}
