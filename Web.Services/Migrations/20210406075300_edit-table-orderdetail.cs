using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Services.Migrations
{
    public partial class edittableorderdetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "OrderDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderDetails");
        }
    }
}
