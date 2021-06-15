using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinuka.Infrastructure.Database.Migrations
{
    public partial class AddRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Sessions");

            migrationBuilder.AddColumn<Guid>(
                name: "RefreshTokenId",
                table: "Sessions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "SessionTokenId",
                table: "Sessions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SessionToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionToken", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_RefreshTokenId",
                table: "Sessions",
                column: "RefreshTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionTokenId",
                table: "Sessions",
                column: "SessionTokenId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_RefreshToken_RefreshTokenId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_SessionToken_SessionTokenId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "SessionToken");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_RefreshTokenId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_SessionTokenId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "RefreshTokenId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "SessionTokenId",
                table: "Sessions");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "Sessions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Sessions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
