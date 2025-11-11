using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Skynetz.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncludedMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerMinute = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "IncludedMinutes", "Name" },
                values: new object[,]
                {
                    { 1, 30, "FaleMais 30" },
                    { 2, 60, "FaleMais 60" },
                    { 3, 120, "FaleMais 120" }
                });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "DestinationCode", "OriginCode", "PricePerMinute" },
                values: new object[,]
                {
                    { 1, "016", "011", 1.90m },
                    { 2, "011", "016", 2.90m },
                    { 3, "017", "011", 1.70m },
                    { 4, "011", "017", 2.70m },
                    { 5, "018", "011", 0.90m },
                    { 6, "011", "018", 1.90m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
