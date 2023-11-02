using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampagneApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingAttributeToSizesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Sizes",
                newName: "DescriptionFR");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEN",
                table: "Sizes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEN",
                table: "Sizes");

            migrationBuilder.RenameColumn(
                name: "DescriptionFR",
                table: "Sizes",
                newName: "Description");
        }
    }
}
