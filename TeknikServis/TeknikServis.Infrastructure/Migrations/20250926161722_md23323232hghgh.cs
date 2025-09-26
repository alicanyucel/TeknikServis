using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class md23323232hghgh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppUsers",
                type: "bit",
                nullable: false,
                defaultValue: false); //
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppUsers");
        }
    }
}
