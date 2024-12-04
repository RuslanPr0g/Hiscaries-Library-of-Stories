using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HC.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class MakeTableNamesPlural : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_PlatformUsers_PlatformUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Stories_StoryId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_PlatformUserToLibrarySubscriptions_PlatformUsers_PlatformUs~",
                table: "PlatformUserToLibrarySubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingHistory_PlatformUsers_PlatformUserId",
                table: "ReadingHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingHistory_Stories_StoryId",
                table: "ReadingHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_PlatformUsers_PlatformUserId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryAudio_Stories_StoryId",
                table: "StoryAudio");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryBookMark_PlatformUsers_PlatformUserId",
                table: "StoryBookMark");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryPage_Stories_StoryId",
                table: "StoryPage");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryRating_PlatformUsers_PlatformUserId",
                table: "StoryRating");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryRating_Stories_StoryId",
                table: "StoryRating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_RefreshToken_RefreshTokenId",
                table: "UserAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryRating",
                table: "StoryRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryPage",
                table: "StoryPage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryBookMark",
                table: "StoryBookMark");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryAudio",
                table: "StoryAudio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingHistory",
                table: "ReadingHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlatformUserToLibrarySubscriptions",
                table: "PlatformUserToLibrarySubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "StoryRating",
                newName: "StoryRatings");

            migrationBuilder.RenameTable(
                name: "StoryPage",
                newName: "StoryPages");

            migrationBuilder.RenameTable(
                name: "StoryBookMark",
                newName: "StoryBookMarks");

            migrationBuilder.RenameTable(
                name: "StoryAudio",
                newName: "StoryAudios");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "ReadingHistory",
                newName: "ReadingHistories");

            migrationBuilder.RenameTable(
                name: "PlatformUserToLibrarySubscriptions",
                newName: "PlatformUserToLibrarySubscription");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_StoryRating_PlatformUserId",
                table: "StoryRatings",
                newName: "IX_StoryRatings_PlatformUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryBookMark_PlatformUserId",
                table: "StoryBookMarks",
                newName: "IX_StoryBookMarks_PlatformUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryAudio_StoryId",
                table: "StoryAudios",
                newName: "IX_StoryAudios_StoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_PlatformUserId",
                table: "Reviews",
                newName: "IX_Reviews_PlatformUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingHistory_PlatformUserId",
                table: "ReadingHistories",
                newName: "IX_ReadingHistories_PlatformUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PlatformUserToLibrarySubscriptions_PlatformUserId",
                table: "PlatformUserToLibrarySubscription",
                newName: "IX_PlatformUserToLibrarySubscription_PlatformUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_StoryId",
                table: "Comments",
                newName: "IX_Comments_StoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PlatformUserId",
                table: "Comments",
                newName: "IX_Comments_PlatformUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryRatings",
                table: "StoryRatings",
                columns: new[] { "StoryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryPages",
                table: "StoryPages",
                columns: new[] { "StoryId", "Page" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryBookMarks",
                table: "StoryBookMarks",
                columns: new[] { "StoryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryAudios",
                table: "StoryAudios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                columns: new[] { "LibraryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingHistories",
                table: "ReadingHistories",
                columns: new[] { "StoryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlatformUserToLibrarySubscription",
                table: "PlatformUserToLibrarySubscription",
                columns: new[] { "LibraryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_PlatformUsers_PlatformUserId",
                table: "Comments",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stories_StoryId",
                table: "Comments",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformUserToLibrarySubscription_PlatformUsers_PlatformUse~",
                table: "PlatformUserToLibrarySubscription",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingHistories_PlatformUsers_PlatformUserId",
                table: "ReadingHistories",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingHistories_Stories_StoryId",
                table: "ReadingHistories",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_PlatformUsers_PlatformUserId",
                table: "Reviews",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryAudios_Stories_StoryId",
                table: "StoryAudios",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryBookMarks_PlatformUsers_PlatformUserId",
                table: "StoryBookMarks",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryPages_Stories_StoryId",
                table: "StoryPages",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryRatings_PlatformUsers_PlatformUserId",
                table: "StoryRatings",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryRatings_Stories_StoryId",
                table: "StoryRatings",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_RefreshTokens_RefreshTokenId",
                table: "UserAccounts",
                column: "RefreshTokenId",
                principalTable: "RefreshTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_PlatformUsers_PlatformUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Stories_StoryId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_PlatformUserToLibrarySubscription_PlatformUsers_PlatformUse~",
                table: "PlatformUserToLibrarySubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingHistories_PlatformUsers_PlatformUserId",
                table: "ReadingHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingHistories_Stories_StoryId",
                table: "ReadingHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_PlatformUsers_PlatformUserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryAudios_Stories_StoryId",
                table: "StoryAudios");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryBookMarks_PlatformUsers_PlatformUserId",
                table: "StoryBookMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryPages_Stories_StoryId",
                table: "StoryPages");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryRatings_PlatformUsers_PlatformUserId",
                table: "StoryRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryRatings_Stories_StoryId",
                table: "StoryRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_RefreshTokens_RefreshTokenId",
                table: "UserAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryRatings",
                table: "StoryRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryPages",
                table: "StoryPages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryBookMarks",
                table: "StoryBookMarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryAudios",
                table: "StoryAudios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingHistories",
                table: "ReadingHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlatformUserToLibrarySubscription",
                table: "PlatformUserToLibrarySubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "StoryRatings",
                newName: "StoryRating");

            migrationBuilder.RenameTable(
                name: "StoryPages",
                newName: "StoryPage");

            migrationBuilder.RenameTable(
                name: "StoryBookMarks",
                newName: "StoryBookMark");

            migrationBuilder.RenameTable(
                name: "StoryAudios",
                newName: "StoryAudio");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "ReadingHistories",
                newName: "ReadingHistory");

            migrationBuilder.RenameTable(
                name: "PlatformUserToLibrarySubscription",
                newName: "PlatformUserToLibrarySubscriptions");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_StoryRatings_PlatformUserId",
                table: "StoryRating",
                newName: "IX_StoryRating_PlatformUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryBookMarks_PlatformUserId",
                table: "StoryBookMark",
                newName: "IX_StoryBookMark_PlatformUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryAudios_StoryId",
                table: "StoryAudio",
                newName: "IX_StoryAudio_StoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_PlatformUserId",
                table: "Review",
                newName: "IX_Review_PlatformUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingHistories_PlatformUserId",
                table: "ReadingHistory",
                newName: "IX_ReadingHistory_PlatformUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PlatformUserToLibrarySubscription_PlatformUserId",
                table: "PlatformUserToLibrarySubscriptions",
                newName: "IX_PlatformUserToLibrarySubscriptions_PlatformUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_StoryId",
                table: "Comment",
                newName: "IX_Comment_StoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PlatformUserId",
                table: "Comment",
                newName: "IX_Comment_PlatformUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryRating",
                table: "StoryRating",
                columns: new[] { "StoryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryPage",
                table: "StoryPage",
                columns: new[] { "StoryId", "Page" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryBookMark",
                table: "StoryBookMark",
                columns: new[] { "StoryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryAudio",
                table: "StoryAudio",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                columns: new[] { "LibraryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingHistory",
                table: "ReadingHistory",
                columns: new[] { "StoryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlatformUserToLibrarySubscriptions",
                table: "PlatformUserToLibrarySubscriptions",
                columns: new[] { "LibraryId", "PlatformUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_PlatformUsers_PlatformUserId",
                table: "Comment",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Stories_StoryId",
                table: "Comment",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformUserToLibrarySubscriptions_PlatformUsers_PlatformUs~",
                table: "PlatformUserToLibrarySubscriptions",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingHistory_PlatformUsers_PlatformUserId",
                table: "ReadingHistory",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingHistory_Stories_StoryId",
                table: "ReadingHistory",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_PlatformUsers_PlatformUserId",
                table: "Review",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryAudio_Stories_StoryId",
                table: "StoryAudio",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryBookMark_PlatformUsers_PlatformUserId",
                table: "StoryBookMark",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryPage_Stories_StoryId",
                table: "StoryPage",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryRating_PlatformUsers_PlatformUserId",
                table: "StoryRating",
                column: "PlatformUserId",
                principalTable: "PlatformUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryRating_Stories_StoryId",
                table: "StoryRating",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_RefreshToken_RefreshTokenId",
                table: "UserAccounts",
                column: "RefreshTokenId",
                principalTable: "RefreshToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
