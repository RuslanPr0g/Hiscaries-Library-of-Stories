using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HC.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviewUrl",
                table: "Notifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedObjectId",
                table: "Notifications",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewUrl",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "RelatedObjectId",
                table: "Notifications");
        }
    }
}
