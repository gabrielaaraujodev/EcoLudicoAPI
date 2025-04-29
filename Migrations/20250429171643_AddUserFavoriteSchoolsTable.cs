using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoLudicoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFavoriteSchoolsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Users_UserId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_UserId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Schools");

            migrationBuilder.CreateTable(
                name: "UserFavoriteSchools",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteSchools", x => new { x.SchoolId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteSchools_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteSchools_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteSchools_UserId",
                table: "UserFavoriteSchools",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteSchools");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Schools",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_UserId",
                table: "Schools",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Users_UserId",
                table: "Schools",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
