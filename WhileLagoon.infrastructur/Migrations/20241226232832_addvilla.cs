using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhileLagoon.infrastructur.Migrations
{
    public partial class addvilla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<double>(type: "float", nullable: false),
                    sqft = table.Column<int>(type: "int", nullable: false),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    ImageUel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VillaNubmers",
                columns: table => new
                {
                    Villa_Number = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    SpacialDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNubmers", x => x.Villa_Number);
                    table.ForeignKey(
                        name: "FK_VillaNubmers_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "CreateDate", "Description", "ImageUel", "Name", "Occupancy", "UpdateDate", "price", "sqft" },
                values: new object[] { 1, null, "Fusce 11 tincidunt maximus leo ,sed scelerisque massa auctor sit", "https://licensed-image - Copy", "Royal Villa", 4, null, 200.0, 100 });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "CreateDate", "Description", "ImageUel", "Name", "Occupancy", "UpdateDate", "price", "sqft" },
                values: new object[] { 2, null, "Fusce 11 tincidunt maximus leo ,sed scelerisque massa auctor sit", "https://images", "Royalk Villa", 4, null, 202.0, 103 });

            migrationBuilder.InsertData(
                table: "VillaNubmers",
                columns: new[] { "Villa_Number", "SpacialDetails", "VillaId" },
                values: new object[,]
                {
                    { 101, null, 1 },
                    { 103, null, 1 },
                    { 204, null, 2 },
                    { 205, null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VillaNubmers_VillaId",
                table: "VillaNubmers",
                column: "VillaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNubmers");

            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}
