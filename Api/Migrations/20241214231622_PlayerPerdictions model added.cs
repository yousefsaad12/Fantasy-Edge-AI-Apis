using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class PlayerPerdictionsmodeladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerPredictions",
                columns: table => new
                {
                    PlayerPredictionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    playerName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    predictedPoints = table.Column<double>(type: "float", nullable: false),
                    percentageChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    avgBonusPoints = table.Column<double>(type: "float", nullable: false),
                    pointsPerWeek = table.Column<double>(type: "float", nullable: false),
                    assistsPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    goalsPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cleanSheetPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPredictions");
        }
    }
}
