using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mukamu.Infrastructure.Database.Migrations
{
    public partial class AddColumnsToSubComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommenterId",
                table: "SubCommmnts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_SubCommmnts_CommenterId",
                table: "SubCommmnts",
                column: "CommenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCommmnts_Users_CommenterId",
                table: "SubCommmnts",
                column: "CommenterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCommmnts_Users_CommenterId",
                table: "SubCommmnts");

            migrationBuilder.DropIndex(
                name: "IX_SubCommmnts_CommenterId",
                table: "SubCommmnts");

            migrationBuilder.DropColumn(
                name: "CommenterId",
                table: "SubCommmnts");
        }
    }
}
