using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSalesSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixVatReturnTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VATAmount",
                table: "VatReturns",
                newName: "VatAmount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VatReturns",
                newName: "VatReturnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VatAmount",
                table: "VatReturns",
                newName: "VATAmount");

            migrationBuilder.RenameColumn(
                name: "VatReturnId",
                table: "VatReturns",
                newName: "Id");
        }
    }
}
