using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromillePartner_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class hej : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrinkPlan",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkPlan", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "UpdateDrinkPlanData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeDifference = table.Column<double>(type: "float", nullable: false),
                    DrinkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrinkPlanIdentifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateDrinkPlanData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UpdateDrinkPlanData_DrinkPlan_DrinkPlanIdentifier",
                        column: x => x.DrinkPlanIdentifier,
                        principalTable: "DrinkPlan",
                        principalColumn: "Identifier");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpdateDrinkPlanData_DrinkPlanIdentifier",
                table: "UpdateDrinkPlanData",
                column: "DrinkPlanIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpdateDrinkPlanData");

            migrationBuilder.DropTable(
                name: "DrinkPlan");
        }
    }
}
