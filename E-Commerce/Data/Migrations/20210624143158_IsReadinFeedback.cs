using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Commerce.Data.Migrations
{
    public partial class IsReadinFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Feedbacks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Feedbacks");
        }
    }
}
