using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSalesSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchasesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VatAmount",
                table: "VatReturns",
                newName: "VATAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VATAmount",
                table: "VatReturns",
                newName: "VatAmount");
        }
    }
}
