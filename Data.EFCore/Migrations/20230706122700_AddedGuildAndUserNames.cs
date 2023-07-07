using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedGuildAndUserNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_GuildData_GuildId",
                table: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuildName",
                table: "GuildData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_GuildData_GuildId",
                table: "Tags",
                column: "GuildId",
                principalTable: "GuildData",
                principalColumn: "GuildId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_GuildData_GuildId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserData");

            migrationBuilder.DropColumn(
                name: "GuildName",
                table: "GuildData");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_GuildData_GuildId",
                table: "Tags",
                column: "GuildId",
                principalTable: "GuildData",
                principalColumn: "GuildId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
