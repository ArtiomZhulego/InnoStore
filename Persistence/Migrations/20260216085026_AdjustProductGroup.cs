using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustProductGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroupLocalizations_ProductGroups_ProductGroupId",
                table: "ProductGroupLocalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGroups",
                table: "ProductGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGroupLocalizations",
                table: "ProductGroupLocalizations");

            migrationBuilder.RenameTable(
                name: "ProductGroups",
                newName: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductGroupLocalizations",
                newName: "ProductCategoryLocalizations");

            migrationBuilder.RenameColumn(
                name: "ProductGroupId",
                table: "ProductCategoryLocalizations",
                newName: "ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGroupLocalizations_ProductGroupId_LanguageISOCode",
                table: "ProductCategoryLocalizations",
                newName: "IX_ProductCategoryLocalizations_ProductCategoryId_LanguageISOC~");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategoryLocalizations",
                table: "ProductCategoryLocalizations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoryLocalizations_ProductCategories_ProductCateg~",
                table: "ProductCategoryLocalizations",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoryLocalizations_ProductCategories_ProductCateg~",
                table: "ProductCategoryLocalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategoryLocalizations",
                table: "ProductCategoryLocalizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductCategoryLocalizations",
                newName: "ProductGroupLocalizations");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "ProductGroups");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryId",
                table: "ProductGroupLocalizations",
                newName: "ProductGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategoryLocalizations_ProductCategoryId_LanguageISOC~",
                table: "ProductGroupLocalizations",
                newName: "IX_ProductGroupLocalizations_ProductGroupId_LanguageISOCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGroupLocalizations",
                table: "ProductGroupLocalizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGroups",
                table: "ProductGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroupLocalizations_ProductGroups_ProductGroupId",
                table: "ProductGroupLocalizations",
                column: "ProductGroupId",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
