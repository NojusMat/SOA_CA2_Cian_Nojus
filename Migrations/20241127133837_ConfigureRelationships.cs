using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOA_CA2_Cian_Nojus.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Games_developer_id",
                table: "Games",
                column: "developer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Developer_developer_id",
                table: "Games",
                column: "developer_id",
                principalTable: "Developer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Developer_developer_id",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_developer_id",
                table: "Games");
        }
    }
}
