using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HC.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Library_PlatformUsers_PlatformUserId",
                table: "Library");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Library_LibraryId",
                table: "Stories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Library",
                table: "Library");

            migrationBuilder.RenameTable(
                name: "Library",
                newName: "Libraries");

            migrationBuilder.RenameIndex(
                name: "IX_Library_PlatformUserId",
                table: "Libraries",
                newName: "IX_Libraries_PlatformUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libraries",
                table: "Libraries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_PlatformUsers_PlatformUserId",
                table: "Libraries",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Libraries_LibraryId",
                table: "Stories",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_PlatformUsers_PlatformUserId",
                table: "Libraries");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Libraries_LibraryId",
                table: "Stories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Libraries",
                table: "Libraries");

            migrationBuilder.RenameTable(
                name: "Libraries",
                newName: "Library");

            migrationBuilder.RenameIndex(
                name: "IX_Libraries_PlatformUserId",
                table: "Library",
                newName: "IX_Library_PlatformUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library",
                table: "Library",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Library_PlatformUsers_PlatformUserId",
                table: "Library",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Library_LibraryId",
                table: "Stories",
                column: "LibraryId",
                principalTable: "Library",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
