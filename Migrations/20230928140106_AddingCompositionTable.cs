using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampagneApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingCompositionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChampagneId = table.Column<int>(type: "int", nullable: false),
                    GrapeVarietyId = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compositions_Champagnes_ChampagneId",
                        column: x => x.ChampagneId,
                        principalTable: "Champagnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compositions_GrapeVarieties_GrapeVarietyId",
                        column: x => x.GrapeVarietyId,
                        principalTable: "GrapeVarieties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compositions_ChampagneId",
                table: "Compositions",
                column: "ChampagneId");

            migrationBuilder.CreateIndex(
                name: "IX_Compositions_GrapeVarietyId",
                table: "Compositions",
                column: "GrapeVarietyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compositions");
        }
    }
}
