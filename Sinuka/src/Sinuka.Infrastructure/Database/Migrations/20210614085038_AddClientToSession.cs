using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinuka.Infrastructure.Database.Migrations
{
    public partial class AddClientToSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Sessions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "HostName",
                table: "Clients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PostLoginRedirect",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PostLogoutRedirect",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ClientId",
                table: "Sessions",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Clients_ClientId",
                table: "Sessions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Clients_ClientId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ClientId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "HostName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PostLoginRedirect",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PostLogoutRedirect",
                table: "Clients");
        }
    }
}
