using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampagneApi.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingSizesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sizes",
                newName: "NameFR");

            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                table: "Sizes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEN",
                table: "Sizes");

            migrationBuilder.RenameColumn(
                name: "NameFR",
                table: "Sizes",
                newName: "Name");
        }
    }
}
