using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HC.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class ImproveReadHistoryPKToBeCK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadHistory_Stories_StoryId",
                table: "ReadHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadHistory",
                table: "ReadHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReadHistory_StoryId",
                table: "ReadHistory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReadHistory");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StoryPage",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "StoryPage",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "StoryPage",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadHistory",
                table: "ReadHistory",
                columns: new[] { "StoryId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadHistory",
                table: "ReadHistory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StoryPage");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "StoryPage");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "StoryPage");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ReadHistory",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadHistory",
                table: "ReadHistory",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReadHistory_StoryId",
                table: "ReadHistory",
                column: "StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadHistory_Stories_StoryId",
                table: "ReadHistory",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
