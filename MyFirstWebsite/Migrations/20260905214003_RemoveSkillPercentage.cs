using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebsite.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSkillPercentage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Skills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
