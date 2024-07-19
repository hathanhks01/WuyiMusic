using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuyiDAL.Migrations
{
    /// <inheritdoc />
    public partial class up2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLikedSongs",
                table: "UserLikedSongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFollowArtists",
                table: "UserFollowArtists");

            migrationBuilder.AddColumn<Guid>(
                name: "UserLikedSongId",
                table: "UserLikedSongs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserFollowArtistId",
                table: "UserFollowArtists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLikedSongs",
                table: "UserLikedSongs",
                column: "UserLikedSongId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFollowArtists",
                table: "UserFollowArtists",
                column: "UserFollowArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedSongs_UserId",
                table: "UserLikedSongs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowArtists_UserId",
                table: "UserFollowArtists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLikedSongs",
                table: "UserLikedSongs");

            migrationBuilder.DropIndex(
                name: "IX_UserLikedSongs_UserId",
                table: "UserLikedSongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFollowArtists",
                table: "UserFollowArtists");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowArtists_UserId",
                table: "UserFollowArtists");

            migrationBuilder.DropColumn(
                name: "UserLikedSongId",
                table: "UserLikedSongs");

            migrationBuilder.DropColumn(
                name: "UserFollowArtistId",
                table: "UserFollowArtists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLikedSongs",
                table: "UserLikedSongs",
                columns: new[] { "UserId", "SongId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFollowArtists",
                table: "UserFollowArtists",
                columns: new[] { "UserId", "ArtistId" });
        }
    }
}
