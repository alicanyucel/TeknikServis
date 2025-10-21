using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg34764376473643763412212121212132167622323 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_District_Province_ProvinceId",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_Neighbourhood_District_DistrictId",
                table: "Neighbourhood");

            migrationBuilder.DropForeignKey(
                name: "FK_Province_Country_CountryId",
                table: "Province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Province",
                table: "Province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Neighbourhood",
                table: "Neighbourhood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_District",
                table: "District");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.RenameTable(
                name: "Province",
                newName: "Provinces");

            migrationBuilder.RenameTable(
                name: "Neighbourhood",
                newName: "Neighbourhoods");

            migrationBuilder.RenameTable(
                name: "District",
                newName: "Districts");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameIndex(
                name: "IX_Province_CountryId",
                table: "Provinces",
                newName: "IX_Provinces_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Neighbourhood_DistrictId",
                table: "Neighbourhoods",
                newName: "IX_Neighbourhoods_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_District_ProvinceId",
                table: "Districts",
                newName: "IX_Districts_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_District_PostalCode",
                table: "Districts",
                newName: "IX_Districts_PostalCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Neighbourhoods",
                table: "Neighbourhoods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Districts",
                table: "Districts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Provinces_ProvinceId",
                table: "Districts",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Neighbourhoods_Districts_DistrictId",
                table: "Neighbourhoods",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Countries_CountryId",
                table: "Provinces",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Provinces_ProvinceId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Neighbourhoods_Districts_DistrictId",
                table: "Neighbourhoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_CountryId",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Neighbourhoods",
                table: "Neighbourhoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Districts",
                table: "Districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "Province");

            migrationBuilder.RenameTable(
                name: "Neighbourhoods",
                newName: "Neighbourhood");

            migrationBuilder.RenameTable(
                name: "Districts",
                newName: "District");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_CountryId",
                table: "Province",
                newName: "IX_Province_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Neighbourhoods_DistrictId",
                table: "Neighbourhood",
                newName: "IX_Neighbourhood_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_ProvinceId",
                table: "District",
                newName: "IX_District_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_PostalCode",
                table: "District",
                newName: "IX_District_PostalCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Province",
                table: "Province",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Neighbourhood",
                table: "Neighbourhood",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_District",
                table: "District",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_District_Province_ProvinceId",
                table: "District",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Neighbourhood_District_DistrictId",
                table: "Neighbourhood",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Province_Country_CountryId",
                table: "Province",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
