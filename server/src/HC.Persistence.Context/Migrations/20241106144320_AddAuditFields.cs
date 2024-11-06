using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HC.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountCreated",
                table: "Users",
                newName: "EditedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "StoryAudio",
                newName: "EditedAt");

            migrationBuilder.RenameColumn(
                name: "DatePublished",
                table: "Stories",
                newName: "EditedAt");

            migrationBuilder.RenameColumn(
                name: "DateEdited",
                table: "Stories",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DateLastRead",
                table: "ReadHistory",
                newName: "EditedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Comment",
                newName: "EditedAt");

            migrationBuilder.RenameColumn(
                name: "CommentedAt",
                table: "Comment",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "UserStoryBookMark",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "UserStoryBookMark",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StoryRating",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "StoryRating",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "StoryRating",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "StoryAudio",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Stories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Review",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "Review",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Review",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "RefreshTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ReadHistory",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ReadHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Genres",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "Genres",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Genres",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Comment",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "UserStoryBookMark");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "UserStoryBookMark");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StoryRating");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "StoryRating");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "StoryRating");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "StoryAudio");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ReadHistory");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ReadHistory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "EditedAt",
                table: "Users",
                newName: "AccountCreated");

            migrationBuilder.RenameColumn(
                name: "EditedAt",
                table: "StoryAudio",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "EditedAt",
                table: "Stories",
                newName: "DatePublished");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Stories",
                newName: "DateEdited");

            migrationBuilder.RenameColumn(
                name: "EditedAt",
                table: "ReadHistory",
                newName: "DateLastRead");

            migrationBuilder.RenameColumn(
                name: "EditedAt",
                table: "Comment",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Comment",
                newName: "CommentedAt");
        }
    }
}
