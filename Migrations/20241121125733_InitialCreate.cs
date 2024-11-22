using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SOA_CA2_Cian_Nojus.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    genre = table.Column<string>(type: "TEXT", nullable: false),
                    release_year = table.Column<int>(type: "INTEGER", nullable: false),
                    developer_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    manufacturer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Developer",
                columns: new[] { "Id", "country", "name" },
                values: new object[,]
                {
                    { 1, "USA", "Microsoft" },
                    { 2, "Japan", "Nintendo" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "developer_id", "genre", "release_year", "title" },
                values: new object[,]
                {
                    { 1, 1, "Shooter", 2001, "Halo" },
                    { 2, 1, "Shooter", 2006, "Gears of War" },
                    { 3, 1, "Racing", 2023, "Forza Horizon" },
                    { 4, 2, "Action-Adventure", 2024, "The Legend of Zelda: Breath of the Wild" },
                    { 5, 2, "Platformer", 2008, "Super Mario Odyssey" },
                    { 6, 2, "Racing", 2020, "Mario Kart 8 Deluxe" }
                });

            migrationBuilder.InsertData(
                table: "Platform",
                columns: new[] { "Id", "manufacturer", "name" },
                values: new object[,]
                {
                    { 1, "Microsoft", "Xbox" },
                    { 2, "Nintendo", "Nintendo Switch" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Platform");
        }
    }
}
