using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyIoTService.Infrastructure.EF.Migrations
{
    public partial class AddDeviceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "datatypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RangeFrom = table.Column<int>(type: "int", nullable: false),
                    RangeTo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datatypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "device_data_incoming",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_data_incoming", x => new { x.DeviceId, x.TimeStamp, x.DataTypeId });
                    table.ForeignKey(
                        name: "FK_device_data_incoming_datatypes_DataTypeId",
                        column: x => x.DataTypeId,
                        principalTable: "datatypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_device_data_incoming_devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "device_data_outgoing",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_data_outgoing", x => new { x.DeviceId, x.TimeStamp, x.DataTypeId });
                    table.ForeignKey(
                        name: "FK_device_data_outgoing_datatypes_DataTypeId",
                        column: x => x.DataTypeId,
                        principalTable: "datatypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_device_data_outgoing_devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_device_data_incoming_DataTypeId",
                table: "device_data_incoming",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_device_data_outgoing_DataTypeId",
                table: "device_data_outgoing",
                column: "DataTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "device_data_incoming");

            migrationBuilder.DropTable(
                name: "device_data_outgoing");

            migrationBuilder.DropTable(
                name: "datatypes");
        }
    }
}
