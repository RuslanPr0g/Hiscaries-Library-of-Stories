using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HC.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryRating",
                table: "StoryRating");

            migrationBuilder.DropIndex(
                name: "IX_StoryRating_StoryId",
                table: "StoryRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryBookMark",
                table: "StoryBookMark");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StoryRating");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StoryBookMark");

            migrationBuilder.AddColumn<int>(
                name: "SubscribersCount",
                table: "Libraries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryRating",
                table: "StoryRating",
                columns: new[] { "StoryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryBookMark",
                table: "StoryBookMark",
                columns: new[] { "StoryId", "PlatformUserId" });

            migrationBuilder.CreateTable(
                name: "PlatformUserToLibrarySubscriptions",
                columns: table => new
                {
                    PlatformUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LibraryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EditedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformUserToLibrarySubscriptions", x => new { x.LibraryId, x.PlatformUserId });
                    table.ForeignKey(
                        name: "FK_PlatformUserToLibrarySubscriptions_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformUserToLibrarySubscriptions_PlatformUsers_PlatformUs~",
                        column: x => x.PlatformUserId,
                        principalTable: "PlatformUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformUserToLibrarySubscriptions_PlatformUserId",
                table: "PlatformUserToLibrarySubscriptions",
                column: "PlatformUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlatformUserToLibrarySubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryRating",
                table: "StoryRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryBookMark",
                table: "StoryBookMark");

            migrationBuilder.DropColumn(
                name: "SubscribersCount",
                table: "Libraries");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "StoryRating",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "StoryBookMark",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryRating",
                table: "StoryRating",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryBookMark",
                table: "StoryBookMark",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StoryRating_StoryId",
                table: "StoryRating",
                column: "StoryId");
        }
    }
}
