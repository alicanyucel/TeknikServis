using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_serviceactionid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop FK and index for the duplicate ServiceActionId1 then drop the column
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceLineActions_ServiceActions_ServiceActionId1",
                table: "ServiceLineActions");

            migrationBuilder.DropIndex(
                name: "IX_ServiceLineActions_ServiceActionId1",
                table: "ServiceLineActions");

            migrationBuilder.DropColumn(
                name: "ServiceActionId1",
                table: "ServiceLineActions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ServiceActionId1",
                table: "ServiceLineActions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceLineActions_ServiceActionId1",
                table: "ServiceLineActions",
                column: "ServiceActionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceLineActions_ServiceActions_ServiceActionId1",
                table: "ServiceLineActions",
                column: "ServiceActionId1",
                principalTable: "ServiceActions",
                principalColumn: "Id");
        }
    }
}
