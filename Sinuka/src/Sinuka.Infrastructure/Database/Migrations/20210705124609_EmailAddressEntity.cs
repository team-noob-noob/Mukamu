using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinuka.Infrastructure.Database.Migrations
{
    public partial class EmailAddressEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_RefreshToken_RefreshTokenId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_SessionToken_SessionTokenId",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionToken",
                table: "SessionToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "SessionToken",
                newName: "SessionTokens");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailId",
                table: "Users",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionTokens",
                table: "SessionTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmailAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VerificationString = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddresses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailId",
                table: "Users",
                column: "EmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_RefreshTokens_RefreshTokenId",
                table: "Sessions",
                column: "RefreshTokenId",
                principalTable: "RefreshTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_SessionTokens_SessionTokenId",
                table: "Sessions",
                column: "SessionTokenId",
                principalTable: "SessionTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_EmailAddresses_EmailId",
                table: "Users",
                column: "EmailId",
                principalTable: "EmailAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_RefreshTokens_RefreshTokenId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_SessionTokens_SessionTokenId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_EmailAddresses_EmailId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "EmailAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmailId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionTokens",
                table: "SessionTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "SessionTokens",
                newName: "SessionToken");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionToken",
                table: "SessionToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_RefreshToken_RefreshTokenId",
                table: "Sessions",
                column: "RefreshTokenId",
                principalTable: "RefreshToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_SessionToken_SessionTokenId",
                table: "Sessions",
                column: "SessionTokenId",
                principalTable: "SessionToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
