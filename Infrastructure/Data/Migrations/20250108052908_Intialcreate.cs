using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Intialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Product_Description = table.Column<string>(type: "varchar(3000)", maxLength: 3000, nullable: false),
                    Product_Type = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Product_Brand = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Picture_Url = table.Column<string>(type: "varchar(3000)", maxLength: 3000, nullable: false),
                    Product_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity_In_Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
