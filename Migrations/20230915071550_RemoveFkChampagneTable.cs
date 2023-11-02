using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampagneApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFkChampagneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Champagnes_Sizes_SizeId",
                table: "Champagnes");

            migrationBuilder.DropIndex(
                name: "IX_Champagnes_SizeId",
                table: "Champagnes");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Champagnes");
        }

        /// <inheritdoc />
        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.AddColumn<int>(
        //        name: "SizeId",
        //        table: "Champagnes",
        //        type: "int",
        //        nullable: false,
        //        defaultValue: 0);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Champagnes_SizeId",
        //        table: "Champagnes",
        //        column: "SizeId");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_Champagnes_Sizes_SizeId",
        //        table: "Champagnes",
        //        column: "SizeId",
        //        principalTable: "Sizes",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);
        //}
    }
}
