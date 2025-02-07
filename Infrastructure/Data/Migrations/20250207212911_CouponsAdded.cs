using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CouponsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "Orders",
                newName: "Sub_Total");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "Order_Date");

            migrationBuilder.RenameColumn(
                name: "Buyeremail",
                table: "Orders",
                newName: "Buyer_Email");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Sub_Total",
                table: "Orders",
                newName: "Product_Id");

            migrationBuilder.RenameColumn(
                name: "Order_Date",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "Buyer_Email",
                table: "Orders",
                newName: "Buyeremail");
        }
    }
}
