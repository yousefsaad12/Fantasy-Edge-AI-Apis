using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class PlayerPerdictionsmodeldeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPredictions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerPredictions",
                columns: table => new
                {
                    PlayerPredictionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assistsPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    avgBonusPoints = table.Column<double>(type: "float", nullable: false),
                    cleanSheetPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    goalsPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    percentageChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    playerName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    pointsPerWeek = table.Column<double>(type: "float", nullable: false),
                    predictedPoints = table.Column<double>(type: "float", nullable: false),
                    trend = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPredictions", x => x.PlayerPredictionID);
                });

            migrationBuilder.CreateIndex(
                name: "idx_playerName",
                table: "PlayerPredictions",
                column: "playerName");
        }
    }
}
