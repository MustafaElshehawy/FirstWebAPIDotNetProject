using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIDotNet.Migrations
{
    /// <inheritdoc />
    public partial class editProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Departments_CategoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Departments_DepartmentId",
                table: "Products",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Departments_DepartmentId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_DepartmentId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Departments_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
